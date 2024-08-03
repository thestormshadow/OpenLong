using Long.Ai.Database.Repositories;
using Long.Ai.Managers;
using Long.Database.Entities;
using Long.Shared.Mathematics;
using Long.World.Enums;
using Long.World.Map;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Ai.States.Role;

namespace Long.Ai.States.World
{
    public sealed class GameMap : GameMapBase<Role, Character>
    {
        public const int VIEW_SIZE = 18;
        public const int BROADCAST_SIZE = 21;

		private static readonly Serilog.ILogger logger = Log.ForContext<GameMap>();

		private readonly List<DbRegion> regions = new();

        private readonly ConcurrentDictionary<uint, Character> users = new();
        private readonly ConcurrentDictionary<uint, Role> roles = new();

        private readonly DbMap mapEntity;
        private readonly DbDynamap dynaMapEntity;

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

        public bool IsInstanceMap => Identity >= 100_000_000;
        public int PlayerCount => users.Count;
        public int Partition { get; private set; }
        public int GeneratorPartition { get; private set; }

        public override async Task<bool> InitializeAsync()
        {
            if (!await base.InitializeAsync())
            {
                return false;
            }

            List<DbRegion> regions = await RegionRepository.GetAsync(Identity);
            this.regions.AddRange(regions);

            Partition = (int)Kernel.Services.Processor.SelectPartition();
            GeneratorPartition = (int)Kernel.Services.GeneratorProcessor.SelectPartition();
            return true;
        }

        #region Position Check

        public async Task<Point> QueryRandomPositionAsync(int sourceX = 0, int sourceY = 0, int radius = 0)
        {
            for (int i = 0; i < 20; i++)
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

        public async Task<Point> QueryRandomPositionAsync(int sourceX = 0, int sourceY = 0, int radiusX = 0, int radiusY = 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int w = radiusX > 0 ? radiusX : Width;
                int h = radiusY > 0 ? radiusY : Height;

                int randX = await NextAsync(w);
                int randY = await NextAsync(h);

                int x = sourceX + randX;
                int y = sourceY + randY;

                if (IsValidPoint(x, y) && IsStandEnable(x, y))
                {
                    return new Point(x, y);
                }
            }
            return default;
        }

        public bool IsSuperPosition(int x, int y)
        {
            return GetBlock(GetBlockX(x), GetBlockY(y))?.RoleSet.Values
                                                       .Any(a => a.IsPlayer() && a.X == x && a.Y == y && a.IsAlive) != false;
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

        public bool IsMoveEnable(int x, int y, FacingDirection dir, int sizeAdd, int climbCap = 0)
        {
            sizeAdd = Math.Min(4, sizeAdd);

            int newX = x + GameMapData.WalkXCoords[(int)dir];
            int newY = y + GameMapData.WalkYCoords[(int)dir];

            if (!IsValidPoint(newX, newY))
                return false;

            if (!IsStandEnable(newX, newY))
                return false;

            if (sizeAdd <= 2 && GetRoleAmount(newX, newY) > sizeAdd)
                return false;

            if (climbCap > 0 && this[newX, newY].Elevation - this[x, y].Elevation > climbCap)
                return false;

            int enableVal = sizeAdd % 2;
            if (sizeAdd is > 0 and <= 2)
            {
                int moreDir = (int)dir % 2 != 0 ? 1 : 2;
                for (int i = -1 * moreDir; i <= moreDir; i++)
                {
                    var dir2 = (FacingDirection)(((int)dir + i + 8) % 8);
                    int newX2 = newX + GameMapData.WalkXCoords[(int)dir2];
                    int newY2 = newY + GameMapData.WalkYCoords[(int)dir2];
                    if (IsValidPoint(newX2, newY2) && GetRoleAmount(newX2, newY2) > enableVal)
                        return false;
                }
            }
            else if (sizeAdd > 2)
            {
                int range = (sizeAdd + 1) / 2;
                for (int i = newX - range; i + newX <= range; i++)
                    for (int j = newY - range; j + newY <= range; j++)
                        if (Calculations.GetDistance(i, j, x, y) > range)
                            if (IsValidPoint(i, j) && GetRoleAmount(i, j) > enableVal)
                                return false;
            }

            return true;
        }

        public int GetRoleAmount(int x, int y)
        {
            MapBlock<Role,Character> block = GetBlock(GetBlockX(x), GetBlockY(y));
            if (block == null)
                return 0;
            return block.RoleSet.Values.Count(role => role.X == x && role.Y == y);
        }

        #endregion

        #region Role Management

        public async Task<bool> AddAsync(Role role)
        {
            if (roles.TryAdd(role.Identity, role))
            {
                EnterBlock(role, role.X, role.Y);

                if (role is Character character)
                {
                    users.TryAdd(character.Identity, character);
                }
                else
                {
                    RoleManager.AddRole(role);
                }
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(uint idRole)
        {
            if (roles.TryRemove(idRole, out Role role))
            {
                users.TryRemove(idRole, out _);
                LeaveBlock(role);

                if (role is not Character)
                {
                    RoleManager.RemoveRole(idRole);
                }
            }

            return false;
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

        #region Status

        public async Task SetStatusAsync(ulong flag, bool add)
        {
            if (add)
            {
                Flag |= flag;
            }
            else
            {
                Flag &= ~flag;
            }
        }

        #endregion
    }
}
