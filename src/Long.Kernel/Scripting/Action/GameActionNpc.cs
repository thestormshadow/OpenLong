using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States;
using Long.Kernel.States.World;
using System.Drawing;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Modules.Systems.Syndicate;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        #region Npc 200-299

        private static async Task<bool> ExecuteActionNpcAttrAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            string[] splitParams = SplitParam(param);
            if (splitParams.Length < 3)
            {
                logger.Warning("ExecuteActionNpcAttr invalid param num {0}, {1}", param, action.Id);
                return false;
            }

            string ope = splitParams[0].ToLower();
            string opt = splitParams[1].ToLower();
            bool isInt = int.TryParse(splitParams[2], out int data);
            string strData = splitParams[2];

            if (splitParams.Length < 4 || !uint.TryParse(splitParams[3], out uint idNpc))
            {
                idNpc = role?.Identity ?? user?.InteractingNpc ?? 0;
            }

            var npc = RoleManager.GetRole<BaseNpc>(idNpc);
            if (npc == null)
            {
                logger.Warning("ExecuteActionNpcAttr invalid NPC id {0} for action {1}", idNpc, action.Id);
                return false;
            }

            var cmp = 0;
            var strCmp = "";
            if (ope.Equals("life", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    return await npc.SetAttributesAsync(ClientUpdateType.Hitpoints, (ulong)data);
                }

                if (opt == "+=")
                {
                    return await npc.AddAttributesAsync(ClientUpdateType.Hitpoints, data);
                }

                cmp = (int)npc.Life;
            }
            else if (ope.Equals("lookface", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    return await npc.SetAttributesAsync(ClientUpdateType.Mesh, (ulong)data);
                }

                cmp = (int)npc.Mesh;
            }
            else if (ope.Equals("ownerid", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    if (npc is not DynamicNpc dyna)
                    {
                        return false;
                    }

                    return await dyna.SetOwnerAsync((uint)data);
                }

                cmp = (int)npc.OwnerIdentity;
            }
            else if (ope.Equals("ownertype", StringComparison.InvariantCultureIgnoreCase))
            {
                cmp = (int)npc.OwnerType;
            }
            else if (ope.Equals("maxlife", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    return await npc.SetAttributesAsync(ClientUpdateType.TeamMemberMaxHP, (ulong)data);
                }

                cmp = (int)npc.MaxLife;
            }
            else if (ope.StartsWith("data", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    npc.SetData(ope, data);
                    return await npc.SaveAsync();
                }

                if (opt == "+=")
                {
                    npc.SetData(ope, npc.GetData(ope) + data);
                    return await npc.SaveAsync();
                }

                cmp = npc.GetData(ope);
                isInt = true;
            }
            else if (ope.Equals("datastr", StringComparison.InvariantCultureIgnoreCase))
            {
                if (opt == "=")
                {
                    npc.DataStr = strData;
                    return await npc.SaveAsync();
                }

                if (opt == "+=")
                {
                    npc.DataStr += strData;
                    return await npc.SaveAsync();
                }

                strCmp = npc.DataStr;
            }

            switch (opt)
            {
                case "==": return isInt && cmp == data || strCmp == strData;
                case ">=": return isInt && cmp >= data;
                case "<=": return isInt && cmp <= data;
                case ">": return isInt && cmp > data;
                case "<": return isInt && cmp < data;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionNpcEraseAsync(DbAction action, string param, Character user,
                                                                   Role role,
                                                                   Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            BaseNpc npc = RoleManager.GetRole<BaseNpc>(user.InteractingNpc) ?? role as BaseNpc;
            if (npc == null)
            {
                return false;
            }

            if (action.Data == 0)
            {
                await npc.DelNpcAsync();
                user.InteractingNpc = 0;
                return true;
            }

            foreach (DynamicNpc del in RoleManager.QueryRoleByType<DynamicNpc>().Where(x => x.Type == action.Data))
            {
                await del.DelNpcAsync();
            }

            return true;
        }

        private static async Task<bool> ExecuteActionNpcResetsynownerAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (!(role is DynamicNpc npc))
            {
                return false;
            }

            if ((npc.IsSynNpc() && !npc.Map.IsSynMap())
                || (npc.IsCtfFlag() && !npc.Map.IsSynMap()))
            {
                return false;
            }

            DynamicNpc.Score score = npc.GetTopScore();
            if (score != null)
            {
                ISyndicate syn = SyndicateManager.GetSyndicate((int)score.Identity);
                if (npc.IsSynFlag() && syn != null)
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrWarWon, syn.Name),
                                                        TalkChannel.Center);
                    npc.Map.OwnerIdentity = syn.Identity;
                }
                else if (npc.IsCtfFlag())
                {
                    if (user?.IsPm() == true)
                    {
                        await user.SendAsync("CTF Flag is not handled");
                    }

                    return true;
                }

                if (syn != null)
                {
                    await npc.SetOwnerAsync(syn.Identity, true);
                }

                npc.ClearScores();
                await npc.Map.SaveAsync();
                await npc.SaveAsync();
            }

            foreach (Character player in npc.Map.QueryRoles(x => x is Character).Cast<Character>())
            {
                //player.BattleSystem.ResetBattle();
                //await player.MagicData.AbortMagicAsync(true);
            }

            if (npc.IsSynFlag())
            {
                foreach (BaseNpc resetNpc in RoleManager.QueryRoleByMap<BaseNpc>(npc.MapIdentity))
                {
                    if (resetNpc.IsSynFlag())
                    {
                        continue;
                    }

                    resetNpc.OwnerIdentity = npc.OwnerIdentity;
                    await resetNpc.SaveAsync();
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionNpcFindNextTableAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 4)
            {
                return false;
            }

            uint idNpc = uint.Parse(splitParam[0]);
            uint idMap = uint.Parse(splitParam[1]);
            ushort usMapX = ushort.Parse(splitParam[2]);
            ushort usMapY = ushort.Parse(splitParam[3]);

            var npc = RoleManager.GetRole<BaseNpc>(idNpc);
            if (npc == null)
            {
                return false;
            }

            npc.SetData("data0", (int)idMap);
            npc.SetData("data1", usMapX);
            npc.SetData("data2", usMapY);
            await npc.SaveAsync();
            return true;
        }

        private static async Task<bool> ExecuteActionNpcFamilyCreateAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user == null || user.Family != null)
            {
                return false;
            }

            if (user.Level < 50 || user.Silvers < 500000)
            {
                return false;
            }

            return await FamilyManager.CreateFamilyAsync(user, inputs[0], 500000);
        }

        private static async Task<bool> ExecuteActionNpcFamilyDestroyAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user?.Family == null)
            {
                return false;
            }

            return await FamilyManager.DisbandFamilyAsync(user, user.Family);
        }

        private static async Task<bool> ExecuteActionNpcFamilyChangeNameAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user?.Family == null)
            {
                return false;
            }

            if (user.FamilyRank != IFamily.FamilyRank.ClanLeader)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 2);
            uint nextIdAction = uint.Parse(splitParams[0]);

            if (await FamilyManager.ChangeFamilyNameAsync(user.Family, inputs[0]))
            {
                return await ExecuteActionAsync(nextIdAction, user, role, item, inputs[0]);
            }
            return false;
        }

        private static async Task<bool> ExecuteActionNpcChangePosAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            string[] p = param.Split(' ');
            if (p.Length != 4 && p.Length != 6) 
            {
                logger.Warning("ExecuteActionNpcChangePosAsync must have 4 or 6 params: mapid npctype x y [cx] [cy]");
                return false;
            }

            if (!uint.TryParse(p[0], out var mapId))
            {
                logger.Warning("ExecuteActionNpcChangePosAsync could not parse mapid for action {0}", action.Id);
                return false;
            }

            if (!int.TryParse(p[1], out var npcType))
            {
                logger.Warning("ExecuteActionNpcChangePosAsync could not parse npcType for action {0}", action.Id);
                return false;
            }

            if (!ushort.TryParse(p[2], out var x)
                || !ushort.TryParse(p[3], out var y))
            {
                logger.Warning("ExecuteActionNpcChangePosAsync could not parse fromPos for action {0}", action.Id);
                return false;
            }

            ushort cx = 0,
                cy = 0;
            if (p.Length == 6)
            {
                if (!ushort.TryParse(p[4], out cx)
                    || !ushort.TryParse(p[5], out cy))
                {
                    logger.Warning("ExecuteActionNpcChangePosAsync could not parse toPos for action {0}", action.Id);
                    return false;
                }
            }

            GameMap gameMap = MapManager.GetMap(mapId);
            if (gameMap == null)
            {
                logger.Warning("Could not find map id {0} for action {1}", mapId, action.Id);
                return false;
            }

            foreach (var npc in gameMap.QueryRoles(x => x is BaseNpc).Cast<BaseNpc>())
            {
                ushort newX = x;
                ushort newY = y;
                if (cx != 0 && cy != 0)
                {
                    Point pos = await gameMap.QueryRandomPositionAsync(x, y, cx, cy);
                    if (pos != default(Point))
                    {
                        newX = (ushort)pos.X;
                        newY = (ushort)pos.Y;
                    }
                }

                gameMap.QueueAction(() => npc.ChangePosAsync(mapId, newX, newY));
            }

            return true;
        }

        #endregion
    }
}
