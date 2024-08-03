using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Kernel.States;
using Long.Kernel.Database;
using Long.Kernel.Processors;
using Long.Network.Packets.Ai;
using Long.Kernel.Network.Ai;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        #region Event 2000-2099

        private static async Task<bool> ExecuteActionEventSetstatusAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 3)
            {
                return false;
            }

            uint idMap = uint.Parse(pszParam[0]);
            ulong nStatus = ulong.Parse(pszParam[1]);
            int nFlag = int.Parse(pszParam[2]);

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            if (nFlag == 0)
            {
                map.Flag &= ~nStatus;
            }
            else
            {
                map.Flag |= nStatus;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionEventDelnpcGenidAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            foreach (Role monster in RoleManager
                         .QueryRoles(x => x is Monster monster && monster.GeneratorId == action.Data))
            {
                await monster.LeaveMapAsync();
            }
            return true;
        }

        private static async Task<bool> ExecuteActionEventCompareAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 3)
            {
                return false;
            }

            long nData1 = long.Parse(pszParam[0]), nData2 = long.Parse(pszParam[2]);
            string szOpt = pszParam[1];

            switch (szOpt)
            {
                case "==":
                    return nData1 == nData2;
                case "<":
                    return nData1 < nData2;
                case ">":
                    return nData1 > nData2;
                case "<=":
                    return nData1 <= nData2;
                case ">=":
                    return nData1 >= nData2;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionEventCompareUnsignedAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 3)
            {
                return false;
            }

            ulong nData1 = ulong.Parse(pszParam[0]), nData2 = ulong.Parse(pszParam[2]);
            string szOpt = pszParam[1];

            switch (szOpt)
            {
                case "==":
                    return nData1 == nData2;
                case "<":
                    return nData1 < nData2;
                case ">":
                    return nData1 > nData2;
                case "<=":
                    return nData1 <= nData2;
                case ">=":
                    return nData1 >= nData2;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionEventChangeweatherAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {


            return true;
        }

        private static async Task<bool> ExecuteActionEventCreatepetAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 7) return false;

            uint dwOwnerType = uint.Parse(pszParam[0]);
            uint idOwner = uint.Parse(pszParam[1]);
            uint idMap = uint.Parse(pszParam[2]);
            ushort usPosX = ushort.Parse(pszParam[3]);
            ushort usPosY = ushort.Parse(pszParam[4]);
            uint idGen = uint.Parse(pszParam[5]);
            uint idType = uint.Parse(pszParam[6]);
            uint dwData = 0;
            var szName = "";

            if (pszParam.Length >= 8)
                dwData = uint.Parse(pszParam[7]);
            if (pszParam.Length >= 9)
                szName = pszParam[8];

            DbMonstertype monstertype = RoleManager.GetMonstertype(idType);
            GameMap map = MapManager.GetMap(idMap);

            if (monstertype == null || map == null)
            {
                logger.Warning("ExecuteActionEventCreatepet [{0}] invalid monstertype or map: {1}", action.Id, param);
                return false;
            }

            var msg = new Long.Game.Network.Ai.Packets.MsgAiSpawnNpc			
			{
                Mode = AiSpawnNpcMode.Spawn
            };
            msg.List.Add(new MsgAiSpawnNpc<AiClient>.SpawnNpc
            {
                GeneratorId = idGen,
                MapId = idMap,
                MonsterType = idType,
                OwnerId = idOwner,
                X = usPosX,
                Y = usPosY,
                OwnerType = dwOwnerType,
                Data = dwData
            });
            NpcServer.Instance.Send(NpcServer.NpcClient,msg.Encode());
            return true;
        }

        private static async Task<bool> ExecuteActionEventCreatenewNpcAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 9)
            {
                return false;
            }

            string szName = pszParam[0];
            ushort nType = ushort.Parse(pszParam[1]);
            ushort nSort = ushort.Parse(pszParam[2]);
            ushort nLookface = ushort.Parse(pszParam[3]);
            uint nOwnerType = uint.Parse(pszParam[4]);
            uint nOwner = uint.Parse(pszParam[5]);
            uint idMap = uint.Parse(pszParam[6]);
            ushort nPosX = ushort.Parse(pszParam[7]);
            ushort nPosY = ushort.Parse(pszParam[8]);
            uint nLife = 0;
            uint idBase = 0;
            uint idLink = 0;
            uint setTask0 = 0;
            uint setTask1 = 0;
            uint setTask2 = 0;
            uint setTask3 = 0;
            uint setTask4 = 0;
            uint setTask5 = 0;
            uint setTask6 = 0;
            uint setTask7 = 0;
            int setData0 = 0;
            int setData1 = 0;
            int setData2 = 0;
            int setData3 = 0;
            string szData = "";
            if (pszParam.Length > 9)
            {
                nLife = uint.Parse(pszParam[9]);
                if (pszParam.Length > 10)
                {
                    idBase = uint.Parse(pszParam[10]);
                }

                if (pszParam.Length > 11)
                {
                    idLink = uint.Parse(pszParam[11]);
                }

                if (pszParam.Length > 12)
                {
                    setTask0 = uint.Parse(pszParam[12]);
                }

                if (pszParam.Length > 13)
                {
                    setTask1 = uint.Parse(pszParam[13]);
                }

                if (pszParam.Length > 14)
                {
                    setTask2 = uint.Parse(pszParam[14]);
                }

                if (pszParam.Length > 15)
                {
                    setTask3 = uint.Parse(pszParam[15]);
                }

                if (pszParam.Length > 16)
                {
                    setTask4 = uint.Parse(pszParam[16]);
                }

                if (pszParam.Length > 17)
                {
                    setTask5 = uint.Parse(pszParam[17]);
                }

                if (pszParam.Length > 18)
                {
                    setTask6 = uint.Parse(pszParam[18]);
                }

                if (pszParam.Length > 19)
                {
                    setTask7 = uint.Parse(pszParam[19]);
                }

                if (pszParam.Length > 20)
                {
                    setData0 = int.Parse(pszParam[20]);
                }

                if (pszParam.Length > 21)
                {
                    setData1 = int.Parse(pszParam[21]);
                }

                if (pszParam.Length > 22)
                {
                    setData2 = int.Parse(pszParam[22]);
                }

                if (pszParam.Length > 23)
                {
                    setData3 = int.Parse(pszParam[23]);
                }

                if (pszParam.Length > 24)
                {
                    szData = pszParam[24];
                }
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning("ExecuteActionEventCreatenewNpc invalid {0} map identity for action {1}", idMap, action.Id);
                return false;
            }

            var npc = new DbDynanpc
            {
                Name = szName,
                Base = idBase,
                Cellx = nPosX,
                Celly = nPosY,
                Data0 = setData0,
                Data1 = setData1,
                Data2 = setData2,
                Data3 = setData3,
                Datastr = szData,
                Defence = 0,
                Life = nLife,
                Maxlife = nLife,
                Linkid = idLink,
                Task0 = setTask0,
                Task1 = setTask1,
                Task2 = setTask2,
                Task3 = setTask3,
                Task4 = setTask4,
                Task5 = setTask5,
                Task6 = setTask6,
                Task7 = setTask7,
                Ownerid = nOwner,
                OwnerType = nOwnerType,
                Lookface = nLookface,
                Type = nType,
                Mapid = idMap,
                Sort = nSort
            };

            if (!await ServerDbContext.CreateAsync(npc))
            {
                logger.Warning($"ExecuteActionEventCreatenewNpc could not save dynamic npc");
                return false;
            }

            DynamicNpc dynaNpc = new(npc);
            if (!await dynaNpc.InitializeAsync())
            {
                return false;
            }

            Task gameActionCreateNpcTask() => dynaNpc.EnterMapAsync();
            if (user != null && map.Partition == user.Map.Partition) // if there is an actor, then the action is already queued! (EXPECTED)
            {
                await gameActionCreateNpcTask();
            }
            else
            {
                WorldProcessor.Instance.Queue(map.Partition, gameActionCreateNpcTask);
            }
            return true;
        }

        private static async Task<bool> ExecuteActionEventCountmonsterAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 5)
                return false;

            uint idMap = uint.Parse(pszParam[0]);
            string szField = pszParam[1];
            string szData = pszParam[2];
            string szOpt = pszParam[3];
            int nNum = int.Parse(pszParam[4]);
            var nCount = 0;

            switch (szField.ToLowerInvariant())
            {
                case "name":
                    nCount += RoleManager
                              .QueryRoles(x => x is Monster mob && mob.MapIdentity == idMap && mob.Name.Equals(szData) && mob.IsAlive)
                              .Count;
                    break;
                case "gen_id":
                    nCount += RoleManager
                              .QueryRoles(x => x is Monster mob && mob.GeneratorId == uint.Parse(szData) && mob.IsAlive)
                              .Count;
                    break;
            }

            switch (szOpt)
            {
                case "==":
                    return nCount == nNum;
                case "<":
                    return nCount < nNum;
                case ">":
                    return nCount > nNum;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionEventDeletemonsterAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 2)
                return false;

            uint idMap = uint.Parse(pszParam[0]);
            uint idType = uint.Parse(pszParam[1]);
            var nData = 0;
            var szName = "";

            if (pszParam.Length >= 3)
                nData = int.Parse(pszParam[2]);
            if (pszParam.Length >= 4)
                szName = pszParam[3];

            var ret = false;


            if (!string.IsNullOrEmpty(szName))
            {
                foreach (Role monster in RoleManager.QueryRoles(
                             x => x is Monster && x.MapIdentity == idMap && x.Name.Equals(szName)))
                {
                    await monster.LeaveMapAsync();
                    ret = true;
                }
            }

            if (idType != 0)
            {
                foreach (Role monster in RoleManager.QueryRoles(
                             x => x is Monster mob && x.MapIdentity == idMap && mob.Type == idType))
                {
                    await monster.LeaveMapAsync();
                    ret = true;
                }
            }

            return ret;
        }

        private static async Task<bool> ExecuteActionEventBbsAsync(DbAction action, string param, Character user, Role role,
            Item item, string[] inputs)
        {
            await RoleManager.BroadcastWorldMsgAsync(param, TalkChannel.System);
            return true;
        }

        private static async Task<bool> ExecuteActionEventEraseAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            uint npcType = uint.Parse(pszParam[2]);
            foreach (var dynaNpc in RoleManager.QueryRoleByMap<DynamicNpc>(uint.Parse(pszParam[0])))
            {
                if (dynaNpc.Type == npcType)
                {
                    await dynaNpc.DelNpcAsync();
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionEventTeleportAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 4)
            {
                return false;
            }

            if (!uint.TryParse(pszParam[0], out var idSource) || !uint.TryParse(pszParam[1], out var idTarget) ||
                !ushort.TryParse(pszParam[2], out var usMapX) || !ushort.TryParse(pszParam[3], out var usMapY))
            {
                return false;
            }

            GameMap sourceMap = MapManager.GetMap(idSource);
            GameMap targetMap = MapManager.GetMap(idTarget);

            if (sourceMap == null || targetMap == null)
            {
                return false;
            }

            if (sourceMap.IsTeleportDisable())
            {
                return false;
            }

            if (!sourceMap[usMapX, usMapY].IsAccessible())
            {
                return false;
            }

            foreach (var player in RoleManager.QueryRoleByType<Character>()
                .Where(x => x.MapIdentity == sourceMap.Identity))
            {
                await player.FlyMapAsync(idTarget, usMapX, usMapY);
            }

            return true;
        }

        private static async Task<bool> ExecuteActionEventMassactionAsync(DbAction action, string param, Character user,
            Role role, Item item, string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            if (!uint.TryParse(pszParam[0], out var idMap) || !uint.TryParse(pszParam[1], out var idAction)
                                                           || !int.TryParse(pszParam[2], out var nAmount))
            {
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            if (nAmount <= 0)
            {
                nAmount = int.MaxValue;
            }

            foreach (var player in RoleManager.QueryRoleByMap<Character>(idMap))
            {
                if (nAmount-- <= 0)
                {
                    break;
                }

                await ExecuteActionAsync(idAction, player, role, null, inputs);
            }

            return true;
        }

        #endregion
    }
}
