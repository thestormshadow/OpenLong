using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Processors;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Managers;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgName;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private GameMap GetGameMap(int mapId)
        {
            if (mapId <= 0)
            {
                return user?.Map ?? role?.Map;
            }
            else
            {
                return MapManager.GetMap((uint)mapId);
            }
        }

        [LuaFunction]
        public long GetMapInt(int mapId, int index)
        {
            GameMap gameMap = GetGameMap(mapId);
            if (gameMap == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_MAP_ID: return gameMap.Identity;
                case G_MAP_Type: return (long)gameMap.Type;
                case G_MAP_OwnerID: return gameMap.OwnerIdentity;
                case G_MAP_RebornMap:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return rebornMapId;
                    }
                case G_MAP_PosX:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return rebornMapPos.X;
                    }
                case G_MAP_PosY:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return rebornMapPos.Y;
                    }
                case G_MAP_SYNID: return gameMap.OwnerIdentity;
                case G_MAP_RES_LEV: return gameMap.ResLev;
                case G_MAP_DOC: return gameMap.MapDoc;
                case G_MAP_STATUS: return (long)gameMap.Flag;
                case G_MAP_PORTAL_X: return gameMap.PortalX;
                case G_MAP_PORTAL_Y: return gameMap.PortalY;
                default: return 0;
            }
        }

        [LuaFunction]
        public ulong GetMapIntEx(int mapId, int index)
        {
            GameMap gameMap = GetGameMap(mapId);
            if (gameMap == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_MAP_ID: return gameMap.Identity;
                case G_MAP_Type: return (ulong)gameMap.Type;
                case G_MAP_OwnerID: return gameMap.OwnerIdentity;
                case G_MAP_RebornMap:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return rebornMapId;
                    }
                case G_MAP_PosX:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return (ulong)rebornMapPos.X;
                    }
                case G_MAP_PosY:
                    {
                        uint rebornMapId = 0;
                        Point rebornMapPos = new Point();
                        gameMap.GetRebornMap(ref rebornMapId, ref rebornMapPos);
                        return (ulong)rebornMapPos.Y;
                    }
                case G_MAP_SYNID: return gameMap.OwnerIdentity;
                case G_MAP_RES_LEV: return gameMap.ResLev;
                case G_MAP_DOC: return gameMap.MapDoc;
                case G_MAP_STATUS: return gameMap.Flag;
                case G_MAP_PORTAL_X: return gameMap.PortalX;
                case G_MAP_PORTAL_Y: return gameMap.PortalY;
                default: return 0;
            }
        }

        [LuaFunction]
        public string GetMapStr(int mapId, int index)
        {
            GameMap gameMap = GetGameMap(mapId);
            if (gameMap == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_MAP_Name: return gameMap.Name;
                default: return StrNone;
            }
        }

        [LuaFunction]
        public bool MapFireWorks(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            user.BroadcastRoomMsgAsync(new MsgName
            {
                Identity = user.Identity,
                Action = StringAction.Fireworks
            }, true).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public int CountMapUser(int userId, int userAlive)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }
            if (userAlive != 0)
            {
                return user.Map.QueryRoles(x => x.IsPlayer() && x.IsAlive).Count;
            }
            return user.Map.QueryRoles(x => x.IsPlayer()).Count;
        }

        [LuaFunction]
        public bool BroadcastMapMsg(int mapId, string message)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }
            gameMap.BroadcastMsgAsync(message).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool DropMapItem(int mapId, int x, int y, int itemType)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }

            var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
            if (mapItem.Create(gameMap, new Point(x, y), (uint)itemType, 0, 0, 0, 0))
            {
                if (user != null && user.Map.Partition == gameMap.Partition)
                {
                    mapItem.EnterMapAsync().GetAwaiter().GetResult();
                }
                else
                {
                    WorldProcessor.Instance.Queue(gameMap.Partition, mapItem.EnterMapAsync);
                }
            }
            else
            {
                IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                return false;
            }
            return true;
        }

        [LuaFunction]
        public int CountMapMonster(int mapId, int monsterId, int startX, int startY, int cx, int cy)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return 0;
            }

            return gameMap.QueryRoles(x => x is Monster monster && (monsterId != 0 && monster.Type == monsterId || monsterId == 0) && x.X >= startX &&
                                                                             x.X < startX + cx
                                                                             && x.Y >= startY &&
                                                                             x.Y < startY + cy).Count();
        }

        [LuaFunction]
        public bool DropMultiItems(int mapId, int itemId, int initX, int initY, int endX, int endY, int count, int existTime)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }

            DbItemtype itemtype = GetItemType(itemId);
            if (itemtype == null)
            {
                logger.Warning("DropMultiItems Invalid itemtype {0}", itemId);
                return false;
            }

            for (var i = 0; i < count; i++)
            {
                var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
                var positionRetry = 0;
                var posSuccess = true;

                int targetX = initX + NextAsync(endX).GetAwaiter().GetResult();
                int targetY = initY + NextAsync(endY).GetAwaiter().GetResult();

                var pos = new Point(targetX, targetY);
                while (!gameMap.FindDropItemCell(9, ref pos))
                {
                    if (positionRetry++ >= 5)
                    {
                        posSuccess = false;
                        break;
                    }

                    targetX = initX + NextAsync(endX).GetAwaiter().GetResult();
                    targetY = initY + NextAsync(endY).GetAwaiter().GetResult();

                    pos = new Point(targetX, targetY);
                }

                if (!posSuccess)
                {
                    IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                    continue;
                }

                if (!mapItem.Create(gameMap, pos, itemtype.Type, 0, 0, 0, 0))
                {
                    IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                    continue;
                }

                mapItem.SetAliveTimeout(existTime);
                if (user?.Map != null && user.Map.Partition == gameMap.Partition)
                {
                    mapItem.EnterMapAsync().GetAwaiter().GetResult();
                }
                else
                {
                    WorldProcessor.Instance.Queue(gameMap.Partition, mapItem.EnterMapAsync);
                }
            }
            return true;
        }

        [LuaFunction]
        public bool MapEffect(int mapId, int x, int y, string effectName)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }

            gameMap.BroadcastRoomMsgAsync(x, y, new MsgName
            {
                Identity = 0,
                Action = StringAction.MapEffect,
                X = (ushort)x,
                Y = (ushort)y,
                Strings = new List<string>
                {
                    effectName
                }
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MapUserChgMap(int mapId, int targetMapId, int x, int y)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }

            GameMap targetGameMap = MapManager.GetMap((uint)targetMapId);
            if (targetGameMap == null)
            {
                return false;
            }

            if (gameMap.IsTeleportDisable())
            {
                return false;
            }

            if (!targetGameMap.IsStandEnable(x, y) || !targetGameMap.IsValidPoint(x, y))
            {
                return false;
            }

            foreach (var player in gameMap.QueryPlayers())
            {
                player.FlyMapAsync((uint)targetMapId, x, y).GetAwaiter().GetResult();
            }
            return true;
        }

        [LuaFunction]
        public bool MapUserExeFunc(int mapId, int data, string function)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }

            if (data <= 0)
            {
                data = int.MaxValue;
            }

            foreach (var player in gameMap.QueryPlayers().Take(data))
            {
                player.QueueAction(() =>
                {
                    LuaScriptManager.Run(player, role, item, Array.Empty<string>(), function);
                    return Task.CompletedTask;
                });
            }
            return true;
        }

        [LuaFunction]
        public bool MapChangeLight(int mapId, int rgb)
        {
            GameMap gameMap = MapManager.GetMap((uint)mapId);
            if (gameMap == null)
            {
                return false;
            }
            gameMap.BroadcastMsgAsync(new MsgAction
            {
                Identity = 1,
                Command = (uint)rgb,
                Argument = 0,
                Action = ActionType.MapArgb
            }).GetAwaiter().GetResult();
            return true;
        }
    }
}
