using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Kernel.States;
using Long.Shared.Managers;
using Long.World.Enums;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgName;
using Long.Kernel.Processors;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionMapMovenpcAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 3)
            {
                return false;
            }

            uint idMap = uint.Parse(splitParam[0]);
            ushort nPosX = ushort.Parse(splitParam[1]), nPosY = ushort.Parse(splitParam[2]);

            if (idMap <= 0 || nPosX <= 0 || nPosY <= 0)
            {
                return false;
            }

            var npc = RoleManager.GetRole<BaseNpc>(action.Data);
            if (npc == null)
            {
                return false;
            }

            return await npc.ChangePosAsync(idMap, nPosX, nPosY);
        }

        private static async Task<bool> ExecuteActionMapMapuserAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 3)
            {
                return false;
            }

            var amount = 0;

            if (splitParam[0].Equals("map_user", StringComparison.InvariantCultureIgnoreCase))
            {
                amount += MapManager.GetMap(action.Data)?.PlayerCount ?? 0;
            }
            else if (splitParam[0].Equals("alive_user", StringComparison.InvariantCultureIgnoreCase))
            {
                amount += RoleManager.QueryRoleByMap<Character>(action.Data).Count(x => x.IsAlive);
            }
            else
            {
                logger.Warning($"ExecuteActionMapMapuser invalid cmd {splitParam[0]} for action {action.Id}, {param}");
                return false;
            }

            switch (splitParam[1])
            {
                case "==":
                    return amount == int.Parse(splitParam[2]);
                case "<=":
                    return amount <= int.Parse(splitParam[2]);
                case ">=":
                    return amount >= int.Parse(splitParam[2]);
            }

            return false;
        }

        private static async Task<bool> ExecuteActionMapBrocastmsgAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            GameMap map = MapManager.GetMap(action.Data);
            if (map == null)
            {
                return false;
            }

            await map.BroadcastMsgAsync(param);
            return true;
        }

        private static async Task<bool> ExecuteActionMapDropitemAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 4)
            {
                return false;
            }

            uint idMap = uint.Parse(splitParam[1]);
            uint idItemtype = uint.Parse(splitParam[0]);
            ushort x = ushort.Parse(splitParam[2]);
            ushort y = ushort.Parse(splitParam[3]);

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
            if (mapItem.Create(map, new Point(x, y), idItemtype, 0, 0, 0, 0))
            {
                if (user != null && user.Map.Partition == map.Partition)
                {
                    await mapItem.EnterMapAsync();
                }
                else
                {
                    WorldProcessor.Instance.Queue(map.Partition, () => mapItem.EnterMapAsync());
                }
            }
            else
            {
                IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                return false;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionMapSetstatusAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 3)
            {
                return false;
            }

            uint idMap = uint.Parse(splitParam[0]);
            byte dwStatus = byte.Parse(splitParam[1]);
            bool flag = splitParam[2] != "0";

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            await map.SetStatusAsync(dwStatus, flag);
            return true;
        }

        private static async Task<bool> ExecuteActionMapAttribAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 3)
            {
                return false;
            }

            string szField = splitParam[0];
            string szOpt = splitParam[1];
            var x = 0;
            int data = int.Parse(splitParam[2]);
            uint idMap = 0;

            if (splitParam.Length >= 4)
            {
                idMap = uint.Parse(splitParam[3]);
            }

            GameMap map;
            if (idMap == 0)
            {
                if (user == null)
                {
                    return false;
                }

                map = MapManager.GetMap(user.MapIdentity);
            }
            else
            {
                map = MapManager.GetMap(idMap);
            }

            if (map == null)
            {
                return false;
            }

            if (szField.Equals("status", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (szOpt.ToLowerInvariant())
                {
                    case "test":
                        return map.IsWarTime();
                    case "set":
                        await map.SetStatusAsync((ulong)data, true);
                        return true;
                    case "reset":
                        await map.SetStatusAsync((ulong)data, false);
                        return true;
                }
            }
            else if (szField.Equals("type", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (szOpt.ToLowerInvariant())
                {
                    case "test":
                        return map.Type.HasFlag((MapTypeFlag)data);
                }
            }
            else if (szField.Equals("mapdoc", StringComparison.InvariantCultureIgnoreCase))
            {
                if (szOpt.Equals("="))
                {
                    map.MapDoc = (uint)data;
                    await map.SaveAsync();
                    return true;
                }

                x = (int)map.MapDoc;
            }
            else if (szField.Equals("portal0_x", StringComparison.InvariantCultureIgnoreCase))
            {
                if (szOpt.Equals("="))
                {
                    map.PortalX = (ushort)data;
                    await map.SaveAsync();
                    return true;
                }

                x = map.PortalX;
            }
            else if (szField.Equals("portal0_y", StringComparison.InvariantCultureIgnoreCase))
            {
                if (szOpt.Equals("="))
                {
                    map.PortalY = (ushort)data;
                    await map.SaveAsync();
                    return true;
                }

                x = map.PortalY;
            }
            else if (szField.Equals("res_lev", StringComparison.InvariantCultureIgnoreCase))
            {
                if (szOpt.Equals("="))
                {
                    map.ResLev = (byte)data;
                    await map.SaveAsync();
                    return true;
                }

                x = map.ResLev;
            }
            else
            {
                logger.Warning($"ExecuteActionMapAttrib invalid field {szField} for action {action.Id}, {param}");
                return false;
            }

            switch (szOpt)
            {
                case "==": return x == data;
                case ">=": return x >= data;
                case "<=": return x <= data;
                case "<": return x < data;
                case ">": return x > data;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionMapRegionMonsterAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 8)
            {
                logger.Warning($"ERROR: Invalid param amount on actionid: [{action.Id}]");
                return false;
            }

            string szOpt = splitParam[6];
            uint idMap = uint.Parse(splitParam[0]);
            uint idType = uint.Parse(splitParam[5]);
            ushort nRegionX = ushort.Parse(splitParam[1]),
                   nRegionY = ushort.Parse(splitParam[2]),
                   nRegionCX = ushort.Parse(splitParam[3]),
                   nRegionCY = ushort.Parse(splitParam[4]);
            int nData = int.Parse(splitParam[7]);

            GameMap map;
            if (idMap == 0)
            {
                if (user == null)
                {
                    return false;
                }

                idMap = user.MapIdentity;
                map = user.Map;
            }
            else
            {
                map = MapManager.GetMap(idMap);
            }

            if (map == null)
            {
                return false;
            }

            int count = map.QueryRoles(x => x is Monster monster && (idType != 0 && monster.Type == idType || idType == 0) && x.X >= nRegionX &&
                                                                             x.X < nRegionX + nRegionCX
                                                                             && x.Y >= nRegionY &&
                                                                             x.Y < nRegionY + nRegionCY).Count();

            switch (szOpt)
            {
                case "==": return count == nData;
                case "<=": return count <= nData;
                case ">=": return count >= nData;
                case "<": return count < nData;
                case ">": return count > nData;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionMapRandDropItemAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            // Example: 728006 3030 186 187 304 307 250 3600
            //          ItemID MAP  X   Y   CX  CY  AMOUNT DURATION
            string[] splitParam = SplitParam(param, 8);
            if (splitParam.Length != 8)
            {
                logger.Warning($"ExecuteActionMapRandDropItem: ItemID MAP  X   Y   CX  CY  AMOUNT DURATION :: {param} ({action.Id})");
                return false;
            }

            uint idItemtype = uint.Parse(splitParam[0]); // the item to be dropped
            uint idMap = uint.Parse(splitParam[1]);      // the map
            ushort initX = ushort.Parse(splitParam[2]);  // start coordinates
            ushort initY = ushort.Parse(splitParam[3]);  // start coordinates 
            ushort endX = ushort.Parse(splitParam[4]);   // end coordinates
            ushort endY = ushort.Parse(splitParam[5]);   // end coordinates
            int amount = int.Parse(splitParam[6]);       // amount of items to be dropped
            int duration = int.Parse(splitParam[7]);     // duration of the item in the floor

            DbItemtype itemtype = ItemManager.GetItemtype(idItemtype);
            if (itemtype == null)
            {
                logger.Warning($"Invalid itemtype {idItemtype}, {param}, {action.Id}");
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning($"Invalid map {idMap}, {param}, {action.Id}");
                return false;
            }

            for (var i = 0; i < amount; i++)
            {
                var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
                var positionRetry = 0;
                var posSuccess = true;

                int targetX = initX + await NextAsync(endX);
                int targetY = initY + await NextAsync(endY);

                var pos = new Point(targetX, targetY);
                while (!map.FindDropItemCell(9, ref pos))
                {
                    if (positionRetry++ >= 5)
                    {
                        posSuccess = false;
                        break;
                    }

                    targetX = initX + await NextAsync(endX);
                    targetY = initY + await NextAsync(endY);

                    pos = new Point(targetX, targetY);
                }

                if (!posSuccess)
                {
                    IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                    continue;
                }

                if (!mapItem.Create(map, pos, idItemtype, 0, 0, 0, 0, MapItem.DropMode.Common))
                {
                    IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                    continue;
                }

                mapItem.SetAliveTimeout(duration);
                if (user?.Map != null && user.Map.Partition == map.Partition)
                {
                    await mapItem.EnterMapAsync();
                }
                else
                {
                    WorldProcessor.Instance.Queue(map.Partition, () => mapItem.EnterMapAsync());
                }
            }
            return true;
        }

        private static async Task<bool> ExecuteActionMapChangeweatherAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 5)
            {
                return false;
            }

            int nType = int.Parse(pszParam[0]), nIntensity = int.Parse(pszParam[1]), nDir = int.Parse(pszParam[2]);
            uint dwColor = uint.Parse(pszParam[3]), dwKeepSecs = uint.Parse(pszParam[4]);

            GameMap map;
            if (action.Data == 0)
            {
                if (user == null)
                {
                    return false;
                }

                map = user.Map;
            }
            else
            {
                map = MapManager.GetMap(action.Data);
            }

            if (map == null)
            {
                return false;
            }

            await map.Weather.SetNewWeatherAsync((Weather.WeatherType)nType, nIntensity, nDir, (int)dwColor,
                                                 (int)dwKeepSecs, 0);
            await map.Weather.SendWeatherAsync();
            return true;
        }

        private static async Task<bool> ExecuteActionMapChangelightAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 2)
            {
                return false;
            }

            uint idMap = uint.Parse(splitParam[0]), dwRgb = uint.Parse(splitParam[1]);

            GameMap map;
            if (action.Data == 0)
            {
                if (user == null)
                {
                    return false;
                }

                map = user.Map;
            }
            else
            {
                map = MapManager.GetMap(idMap);
            }

            if (map == null)
            {
                return false;
            }

            map.Light = dwRgb;
            await map.BroadcastMsgAsync(new MsgAction
            {
                Identity = 1,
                Command = dwRgb,
                Argument = 0,
                Action = ActionType.MapArgb
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMapMapeffectAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);

            if (splitParam.Length < 4)
            {
                return false;
            }

            uint idMap = uint.Parse(splitParam[0]);
            ushort posX = ushort.Parse(splitParam[1]), posY = ushort.Parse(splitParam[2]);
            string szEffect = splitParam[3];

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            await map.BroadcastRoomMsgAsync(posX, posY, new MsgName
            {
                Identity = 0,
                Action = StringAction.MapEffect,
                X = posX,
                Y = posY,
                Strings = new List<string>
                {
                    szEffect
                }
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMapFireworksAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user != null)
            {
                await user.BroadcastRoomMsgAsync(new MsgName
                {
                    Identity = user.Identity,
                    Action = StringAction.Fireworks
                }, true);
            }

            return true;
        }

        private static async Task<bool> ExecuteActionMapAbleExpAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            return ((user?.Map ?? role?.Map)?.IsNoExpMap() == true);
        }
    }
}
