using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets.Ai;
using System.Xml.Linq;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private Monster GetMonster(int monsterId)
        {
            if (monsterId <= 0)
            {
                return role as Monster;
            }
            return RoleManager.GetRole<Monster>((uint)monsterId);
        }

        [LuaFunction]
        public long GetMonsterInt(int monsterId, int index)
        {
            Monster monster = GetMonster(monsterId);
            if (monster == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_MONSTER_ID: return monster.Identity;
                case G_MONSTER_Type: return monster.Type;
                case G_MONSTER_MapID: return monster.MapIdentity;
                case G_MONSTER_PosX: return monster.X;
                case G_MONSTER_PosY: return monster.Y;
                case G_MONSTER_MaxLife: return monster.MaxLife;
                case G_MONSTER_Life: return monster.Life;
                case G_MONSTER_Level: return monster.Level;
                default: return 0;
            }
        }

        [LuaFunction]
        public string GetMonsterStr(int monsterId, int index)
        {
            Monster monster = GetMonster(monsterId);
            if (monster == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_MONSTER_Name: return monster.Name;
                default: return StrNone;
            }
        }

        [LuaFunction]
        public int GetCountMonster(int mapId, string mode, object value)
        {
            GameMap gameMap = GetGameMap(mapId);
            if (gameMap == null)
            {
                return 0;
            }

            if (mode.Equals("gen_id") && value is int genId)
            {
                return RoleManager
                              .QueryRoles(x => x is Monster mob && mob.GeneratorId == genId && mob.IsAlive)
                              .Count;
            }
            if (mode.Equals("name") && value is string monsterName)
            {
                return RoleManager
                              .QueryRoles(x => x is Monster mob && mob.MapIdentity == mapId && mob.Name.Equals(monsterName) && mob.IsAlive)
                              .Count;
            }
            return 0;
        }

        [LuaFunction]
        public bool killMonsterDropItem(int userId, uint itemType)
        {
            Character user = GetUser(userId);
            Monster monster = role as Monster;
            if (monster == null)
            {
                return false;
            }
            int quality = (int)(itemType % 10);
            if (Item.IsEquipment(itemType) && quality > 5)
            {
                ServerStatisticManager.DropQualityItem(quality);
            }
            else if (itemType == Item.TYPE_METEOR)
            {
                ServerStatisticManager.DropMeteor();
            }
            else if (itemType == Item.TYPE_DRAGONBALL)
            {
                ServerStatisticManager.DropDragonBall();

                if (monster != null)
                {
                    if (user != null)
                    {
                        RoleManager.BroadcastWorldMsgAsync(string.Format(StrDragonBallDropped, user.Name, user.Map.Name), TalkChannel.TopLeft).GetAwaiter().GetResult();
                        monster.SendEffectAsync(user, "darcue").GetAwaiter().GetResult();
                    }
                    else
                    {
                        monster.SendEffectAsync("darcue", false).GetAwaiter().GetResult();
                    }
                }
            }
            else if (Item.IsGem(itemType))
            {
                ServerStatisticManager.DropGem((Item.SocketGem)(itemType % 1000));
            }
            monster.DropItemAsync(itemType, user, MapItem.DropMode.Common).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool killMonsterDropMoney(int userId, uint money)
        {
            Character user = GetUser(userId);
            Monster monster = role as Monster;
            uint idUser = user?.Identity ?? 0u;
            monster.DropMoneyAsync(money, idUser).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool killMonsterAddCulTation(int userId, int levelFrom, int levelTo, int cultivation)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            if (levelFrom > 0 && user.Level < levelFrom)
            {
                return false;
            }

            if (levelTo > 0 && user.Level > levelTo)
            {
                return false;
            }

            List<Character> targets = user.Team?.Members.ToList() ?? new List<Character> { user };
            foreach (var member in targets)
            {
                if (member.Identity != user.Identity)
                {
                    if (member.MapIdentity != user.MapIdentity || user.GetDistance(member) > Screen.VIEW_SIZE * 2)
                    {
                        continue;
                    }
                }
                member.AwardCultivationAsync(cultivation).GetAwaiter().GetResult();
            }
            return true;
        }

        [LuaFunction]
        public bool CreateMonster(int ownerType, uint idOwner, uint idMap, int x, int y, uint idGenerator, uint idType, int data, string name)
        {
            DbMonstertype monstertype = RoleManager.GetMonstertype(idType);
            if (monstertype == null)
            {
                logger.Warning($"CreateMonster invalid monstertype[{idType}]");
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning($"CreateMonster invalid map[{idMap}]");
                return false;
            }
			var msg = new Long.Game.Network.Ai.Packets.MsgAiSpawnNpc
			{
				Mode = AiSpawnNpcMode.Spawn
			};
			msg.List.Add(new MsgAiSpawnNpc<AiClient>.SpawnNpc
			{
				GeneratorId = idGenerator,
				MapId = idMap,
				MonsterType = idType,
				OwnerId = idOwner,
				X = (ushort)x,
				Y = (ushort)y,
				OwnerType = (uint)ownerType,
				Data = (uint)data
			});
			NpcServer.Instance.Send(NpcServer.NpcClient, msg.Encode());			
            return true;
        }

        [LuaFunction]
        public bool DeleteMonster(uint idMap, uint idType, int data, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (Role monster in RoleManager.QueryRoles(x => x is Monster && x.MapIdentity == idMap && x.Name.Equals(name)))
                {
                    monster.LeaveMapAsync().GetAwaiter().GetResult();
                }
            }

            if (idType != 0)
            {
                foreach (Role monster in RoleManager.QueryRoles(x => x is Monster mob && x.MapIdentity == idMap && mob.Type == idType))
                {
                    monster.LeaveMapAsync().GetAwaiter().GetResult();
                }
            }
            return true;
        }
    }
}
