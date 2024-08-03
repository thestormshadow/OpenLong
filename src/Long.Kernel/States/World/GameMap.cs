using Long.Database.Entities;
using Long.Game.Network.Ai.Packets;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Processors;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Shared.Mathematics;
using Long.World.Enums;
using Long.World.Map;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;

namespace Long.Kernel.States.World
{
    public class GameMap : GameMapBase<Role, Character>
    {
        public const uint NPC_JAIL_ID = 5000;

        private static readonly ILogger logger = Log.ForContext<GameMap>();

        private readonly List<Passway> passways = new();
        private readonly List<DbRegion> regions = new();

        protected readonly ConcurrentDictionary<uint, Character> users = new();
        protected readonly ConcurrentDictionary<uint, Role> roles = new();

        private readonly DbMap mapEntity;
        protected readonly DbDynamap dynaMapEntity;

        public GameMap(DbMap map)
        {
            mapEntity = map;
        }

        public GameMap(DbDynamap map)
        {
            dynaMapEntity = map;
        }

        public override uint Identity => mapEntity?.Identity ?? dynaMapEntity?.Identity ?? 0;
        public override string Name => mapEntity?.Name ?? dynaMapEntity?.Name ?? "Invalid";
        public uint BaseMapId { get; set; }

        public override uint OwnerIdentity
        {
            get => mapEntity?.OwnerIdentity ?? dynaMapEntity?.OwnerIdentity ?? 0;
            set
            {
                if (mapEntity != null)
                {
                    mapEntity.OwnerIdentity = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.OwnerIdentity = value;
                }
            }
        }

        public override uint MapDoc
        {
            get => mapEntity?.MapDoc ?? dynaMapEntity?.MapDoc ?? 0;
            set
            {
                if (!UpdateMapData(value))
                {
                    return;
                }

                if (mapEntity != null)
                {
                    mapEntity.MapDoc = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.MapDoc = value;
                }
            }
        }

        public override MapTypeFlag Type
        {
            get
            {
                if (mapEntity != null)
                {
                    return (MapTypeFlag)mapEntity.Type;
                }

                if (dynaMapEntity != null)
                {
                    return (MapTypeFlag)dynaMapEntity.Type;
                }

                return MapTypeFlag.Normal;
            }
        }

        public ushort PortalX
        {
            get => (ushort)(mapEntity?.PortalX ?? dynaMapEntity?.PortalX ?? 0);
            set
            {
                if (mapEntity != null)
                {
                    mapEntity.PortalX = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.PortalX = value;
                }
            }
        }

        public ushort PortalY
        {
            get => (ushort)(mapEntity?.PortalY ?? dynaMapEntity?.PortalY ?? 0);
            set
            {
                if (mapEntity != null)
                {
                    mapEntity.PortalY = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.PortalY = value;
                }
            }
        }

        public byte ResLev
        {
            get => mapEntity?.ResourceLevel ?? dynaMapEntity?.ResourceLevel ?? 0;
            set
            {
                if (mapEntity != null)
                {
                    mapEntity.ResourceLevel = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.ResourceLevel = value;
                }
            }
        }

        public uint Light
        {
            get => mapEntity?.Color ?? dynaMapEntity?.Color ?? 0;
            set
            {
                if (mapEntity != null)
                {
                    mapEntity.Color = value;
                }
                else if (dynaMapEntity != null)
                {
                    dynaMapEntity.Color = value;
                }
            }
        }

        public int PlayerCount => users.Count;

        public int Partition { get; private set; }

        public Weather Weather { get; set; }

        public override async Task<bool> InitializeAsync()
        {
            if (!await base.InitializeAsync())
            {
                return false;
            }

            List<DbPassway> passways = PasswayRepository.Get(Identity);
            foreach (DbPassway dbPassway in passways)
            {
                DbPortal portal = PortalRepository.Get(dbPassway.TargetMapId, dbPassway.TargetPortal);
                if (portal == null)
                {
                    logger.Warning("Could not find portal for passway [{Passway}]", dbPassway.Identity);
                    continue;
                }

                this.passways.Add(new Passway
                {
                    Index = (int)dbPassway.MapIndex,
                    TargetMap = dbPassway.TargetMapId,
                    TargetX = (ushort)portal.PortalX,
                    TargetY = (ushort)portal.PortalY
                });
            }

            Weather = new Weather(this);

            List<DbRegion> regions = RegionRepository.Get(Identity);
            this.regions.AddRange(regions);

            if (IsSynMap() || IsFamilyMap() || IsPkField())
            {
                Partition = WorldProcessor.PVP_MAP_GROUP;
                WorldProcessor.Instance.SelectPartition((uint)Partition);
            }
            else
            {
                Partition = (int)WorldProcessor.Instance.SelectPartition();
            }
            return true;
        }

        #region Position Check

        public async Task<Point> QueryRandomPositionAsync(int sourceX = 0, int sourceY = 0, int radius = 0)
        {
            for (int i = 0; i < 50; i++)
            {
                int w = radius > 0 ? radius : Width;
                int h = radius > 0 ? radius : Height;

                int randX = await NextAsync(w);
                int randY = await NextAsync(h);

                int x = sourceX + randX;
                int y = sourceY + randY;

                if (IsStandEnable(x, y))
                {
                    return new Point(x, y);
                }
            }
            return default;
        }

        public async Task<Point> QueryRandomPositionAsync(int sourceX, int sourceY, int radiusX, int radiusY)
        {
            for (int i = 0; i < 50; i++)
            {
                int w = radiusX > 0 ? radiusX : Width;
                int h = radiusY > 0 ? radiusY : Height;

                int randX = await NextAsync(w);
                int randY = await NextAsync(h);

                int x = sourceX + randX;
                int y = sourceY + randY;

                if (IsStandEnable(x, y))
                {
                    return new Point(x, y);
                }
            }
            return default;
        }

        public async Task<Point> QueryRandomPositionNoRadiusAsync(int x, int y, int cx, int cy)
        {
            for (int i = 0; i < 50; i++)
            {
                int randX = await NextAsync(x, cx);
                int randY = await NextAsync(y, cy);

                if (IsStandEnable(randX, randY))
                {
                    return new Point(randX, randY);
                }
            }
            return default;
        }

        public bool IsSuperPosition(int x, int y)
        {
            return GetBlock(GetBlockX(x), GetBlockY(y))?.RoleSet.Values.Any(a => a.X == x && a.Y == y && a.IsAlive) != false;
        }

        public bool QueryRegion(RegionType regionType, ushort x, ushort y)
        {
            return regions
                   .Where(re => x > re.BoundX && x < re.BoundX + re.BoundCX && y > re.BoundY &&
                                y < re.BoundY + re.BoundCY).Any(region => region.Type == (int)regionType);
        }

        public List<DbRegion> QueryRegions(RegionType regionType)
        {
            return regions.Where(x => x.Type == (int)regionType).ToList();
        }

        #endregion

        #region Broadcasting

        public async Task SendMapInfoAsync(Character user)
        {
            var action = new MsgAction
            {
                Action = ActionType.MapArgb,
                Identity = 1,
                Command = Light,
                Argument = 0
            };
            await user.SendAsync(action);
            await user.SendAsync(new MsgMapInfo(Identity, MapDoc, (ulong)Type));

            if (Weather.GetType() != Weather.WeatherType.WeatherNone)
            {
                await Weather.SendWeatherAsync(user);
            }
            else
            {
                await Weather.SendNoWeatherAsync(user);
            }
        }

        public async Task BroadcastMsgAsync(IPacket msg, uint exclude = 0)
        {
            byte[] encoded = msg.Encode();
            foreach (Character user in users.Values)
            {
                if (user.Identity == exclude)
                {
                    continue;
                }

                await user.SendAsync(encoded);
            }
        }

        public Task BroadcastMsgAsync(string message, TalkChannel channel = TalkChannel.TopLeft,
                                            Color? color = null)
        {
            return BroadcastMsgAsync(new MsgTalk(channel, color ?? Color.White, message));
        }

        public async Task BroadcastRoomMsgAsync(int x, int y, IPacket msg, uint exclude = 0)
        {
            byte[] encoded = msg.Encode();
            foreach (Character user in users.Values)
            {
                if (user.Identity == exclude ||
                    Calculations.GetDistance(x, y, user.X, user.Y) > Screen.BROADCAST_SIZE)
                {
                    continue;
                }

                if (user.Client != null)
                {
                    await user.SendAsync(encoded);
                }
            }
        }

        public async Task BroadcastRoomMsgAsync(IPacket msg, int x, int y, uint exclude, int distance = Screen.VIEW_SIZE * 2)
        {
            byte[] encoded = msg.Encode();
            foreach (Character user in users.Values)
            {
                if (user.Identity == exclude || Calculations.GetDistance(x, y, user.X, user.Y) > distance)
                {
                    continue;
                }

                await user.SendAsync(encoded);
            }
        }

        #endregion

        #region Role Management

        public virtual async Task<bool> AddAsync(Role role)
        {
            if (roles.TryAdd(role.Identity, role))
            {
                EnterBlock(role, role.X, role.Y);

                if (role is Character character)
                {
                    users.TryAdd(character.Identity, character);
                    await character.Screen.UpdateAsync();
                }
                else
                {
                    RoleManager.AddRole(role);
                    foreach (Character user in users.Values.Where(x => Calculations.GetDistance(x.X, x.Y, role.X, role.Y) <= Screen.BROADCAST_SIZE))
                    {
                        await user.Screen.SpawnAsync(role);
                    }
                }

                return true;
            }

            return false;
        }

        public virtual async Task<bool> RemoveAsync(uint idRole)
        {
            if (roles.TryRemove(idRole, out Role role))
            {
                users.TryRemove(idRole, out _);
                LeaveBlock(role);

                if (role is not Character)
                {
                    RoleManager.RemoveRole(idRole);
                }

                foreach (Character user in users.Values)
                {
                    if (Calculations.GetDistance(role.X, role.Y, user.X, user.Y) > Screen.BROADCAST_SIZE * 2)
                    {
                        continue;
                    }

                    await user.Screen.RemoveAsync(idRole, true);
                }
            }
            return true;
        }

        #endregion

        #region Query Role

        public Character GetUser(uint idUser)
        {
            return users.TryGetValue(idUser, out var user) ? user : null;
        }

        public Role QueryRole(uint target)
        {
            return roles.TryGetValue(target, out Role value) ? value : null;
        }

        public T QueryRole<T>(uint target) where T : Role
        {
            return roles.TryGetValue(target, out Role value) && value is T role ? role : null;
        }

        public T QueryRole<T>(Func<T, bool> pred) where T : Role
        {
            return roles.Values.Where(x => x is T).Cast<T>().FirstOrDefault(pred);
        }

        public Role QueryAroundRole(Role sender, uint target)
        {
            int currentBlockX = GetBlockX(sender.X);
            int currentBlockY = GetBlockY(sender.Y);
            return Query9Blocks(currentBlockX, currentBlockY).FirstOrDefault(x => x.Identity == target);
        }

        public DynamicNpc QueryStatuary(Role sender, uint lookface, uint task)
        {
            return Query9BlocksByPos(sender.X, sender.Y)
                   .Where(x => x is DynamicNpc)
                   .Cast<DynamicNpc>()
                   .FirstOrDefault(x => x.Task0 == task && x.Mesh - x.Mesh % 10 == lookface - lookface % 10);
        }

        public List<Character> QueryPlayers()
        {
            return users.Values.ToList();
        }

        public List<Character> QueryPlayers(Func<Character, bool> pred)
        {
            return users.Values.Where(pred).ToList();
        }

        public List<Role> QueryRoles()
        {
            return roles.Values.ToList();
        }

        public List<Role> QueryRoles(Func<Role, bool> pred)
        {
            return roles.Values.Where(pred).ToList();
        }

        #endregion

        #region Portals and Passages

        public bool GetRebornMap(ref uint idMap, ref Point target)
        {
            idMap = mapEntity?.RebornMap ?? dynaMapEntity.RebornMap;
            GameMap targetMap = MapManager.GetMap(idMap);
            if (targetMap == null)
            {
                logger.Error($"Could not get reborn map [{Identity}]!");
                return false;
            }

            if (mapEntity.LinkX == 0 || mapEntity.LinkY == 0)
            {
                target = new Point(targetMap.PortalX, targetMap.PortalY);
            }
            else
            {
                target = new Point(mapEntity.LinkX, mapEntity.LinkY);
            }

            return true;
        }

        public bool GetPassageById(int idxPassage, ref uint idMap, ref Point target, ref Point source)
        {
            if (!IsValidPoint(source.X, source.Y))
            {
                return false;
            }

            if (idxPassage < 0)
            {
                return false;
            }

            Passway passway = passways.FirstOrDefault(x => x.Index == idxPassage);
            idMap = passway.TargetMap;
            target = new Point(passway.TargetX, passway.TargetY);

            GameMap targetMap = MapManager.GetMap(idMap);
            if (targetMap == null || !targetMap.IsValidPoint(passway.TargetX, passway.TargetY))
            {
                GetRebornMap(ref idMap, ref target);
            }

            return true;
        }

        public bool GetPassageMap(ref uint idMap, ref Point target, ref Point source)
        {
            if (!IsValidPoint(source.X, source.Y))
            {
                return false;
            }

            int idxPassage = idxPassage = mapData.GetPassage(source.X, source.Y);
            if (idxPassage < 0)
            {
                return false;
            }

#if DEBUG
            logger.Debug($"User has jumped into [map:{Identity}:{Name}] portal {idxPassage} ({source.X:000}, {source.Y:000})");
#endif

            if (IsDynamicMap())
            {
                idMap = dynaMapEntity.LinkMap;
                target.X = dynaMapEntity.LinkX;
                target.Y = dynaMapEntity.LinkY;
                return true;
            }

            Passway passway = passways.FirstOrDefault(x => x.Index == idxPassage);
            idMap = passway.TargetMap;
            target = new Point(passway.TargetX, passway.TargetY);

            GameMap targetMap = MapManager.GetMap(idMap);
            if (targetMap == null || !targetMap.IsValidPoint(passway.TargetX, passway.TargetY))
            {
                GetRebornMap(ref idMap, ref target);
            }

            return true;
        }

        #endregion

        #region Items

        public bool IsLayItemEnable(int x, int y)
        {
            return this[x, y].IsAccessible() && roles.All(role => role.Value != null && !(role.Value is MapItem) && !(role.Value is BaseNpc) || role.Value.X != x || role.Value.Y != y);
        }

        public bool FindDropItemCell(int range, ref Point sender)
        {
            if (IsLayItemEnable(sender.X, sender.Y))
            {
                return true;
            }

            int size = range * 2 + 1;
            int bufSize = size ^ 2;

            for (int i = 0; i < 8; i++)
            {
                int newX = sender.X + GameMapData.WalkXCoords[i];
                int newY = sender.Y + GameMapData.WalkYCoords[i];
                if (IsLayItemEnable(newX, newY))
                {
                    sender.X = newX;
                    sender.Y = newY;
                    return true;
                }
            }

            Point pos = sender;
            var setItem = Query9BlocksByPos(sender.X, sender.Y).Where(x => x is MapItem && x.GetDistance(pos.X, pos.Y) <= range).Cast<MapItem>().ToList();
            int nMinRange = range + 1;
            bool ret = false;
            var posFree = new Point();
            for (int i = Math.Max(sender.X - range, 0); i <= sender.X + range && i < Width; i++)
            {
                for (int j = Math.Max(sender.Y - range, 0); j <= sender.Y + range && j < Height; j++)
                {
                    int idx = GameMapData.Pos2Index(i - (sender.X - range), j - (sender.Y - range), size);

                    if (idx >= 0 && idx < bufSize)
                    {
                        if (setItem.FirstOrDefault(x => GameMapData.Pos2Index(x.X - i + range, x.Y - j + range, range) == idx) != null)
                        {
                            continue;
                        }
                    }

                    if (IsLayItemEnable(sender.X, sender.Y))
                    {
                        double nDistance = Calculations.GetDistance(i, j, sender.X, sender.Y);
                        if (nDistance < nMinRange)
                        {
                            nMinRange = (int)nDistance;
                            posFree.X = i;
                            posFree.Y = j;
                            ret = true;
                        }
                    }
                }
            }

            if (ret)
            {
                sender = posFree;
                return true;
            }
            return false;
        }


        #endregion

        #region Status

        public async Task SetStatusAsync(ulong flag, bool add)
        {
            ulong oldFlag = Flag;
            if (add)
            {
                Flag |= flag;
            }
            else
            {
                Flag &= ~flag;
            }

            if (Flag != oldFlag)
            {
                await BroadcastMsgAsync(new MsgMapInfo(Identity, MapDoc, Flag));
            }
        }

        #endregion

        #region Terrain

        public bool AddTerrainObject(uint owner, int x, int y, uint idTerrainType)
        {
            if (mapData.AddTerrainItem(owner, x, y, idTerrainType))
            {
                return true;
            }

            return false;
        }

        public bool DelTerrainObj(uint idOwner)
        {
            if (mapData.DelTerrainItem(idOwner))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Queue

        public void QueueAction(Func<Task> func)
        {
            WorldProcessor.Instance.Queue(Partition, func);
        }

        #endregion

        #region Database

        public async Task<bool> SaveAsync()
        {
            if (mapEntity == null && dynaMapEntity == null)
            {
                return false;
            }

            if (mapEntity != null)
            {
                return await ServerDbContext.UpdateAsync(mapEntity);
            }

            return await ServerDbContext.UpdateAsync(dynaMapEntity);
        }

        public async Task<bool> DeleteAsync()
        {
            if (mapEntity == null && dynaMapEntity == null)
            {
                return false;
            }

            if (mapEntity != null)
            {
                return await ServerDbContext.DeleteAsync(mapEntity);
            }

            return await ServerDbContext.DeleteAsync(dynaMapEntity);
        }

		#endregion

		#region Socket

		public virtual Task SendAddToNpcServerAsync()
		{
			if (dynaMapEntity != null)
				return NpcServer.SendAsync(new MsgAiDynaMap(dynaMapEntity));
			return Task.CompletedTask;
		}

		public Task SendRemoveToNpcServerAsync()
		{
			if (dynaMapEntity != null)
				return NpcServer.SendAsync(new MsgAiDynaMap(Identity));
			return Task.CompletedTask;
		}

		#endregion

		public override string ToString()
        {
            return $"{Identity} - {Name}";
        }

        public struct Passway
        {
            public int Index;
            public uint TargetMap;
            public ushort TargetX;
            public ushort TargetY;
        }
    }
}
