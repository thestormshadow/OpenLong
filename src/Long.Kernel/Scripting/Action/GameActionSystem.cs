using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.States.User.Character;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;
using Long.Kernel.States.MessageBoxes;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        #region Action 100-199

        private static async Task<bool> ExecuteActionMenuTextAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning("Action[{0}] type 101 non character", action.Id);
                return false;
            }

            int messages = (int)Math.Ceiling(param.Length / (double)byte.MaxValue);
            for (int i = 0; i < messages; i++)
            {
                await user.SendAsync(new MsgTaskDialog
                {
                    InteractionType = MsgTaskDialog.TaskInteraction.Dialog,
                    Text = param.Substring(i * byte.MaxValue, Math.Min(byte.MaxValue, param.Length - byte.MaxValue * i)),
                    Data = (ushort)action.Data
                });
            }
            return true;
        }

        private static async Task<bool> ExecuteActionMenuLinkAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning("Action[{0}] type 102 not user", action.Id);
                return false;
            }

            uint task = 0;
            int align = 0;
            string[] parsed = param.Split(' ');
            if (parsed.Length > 1)
            {
                uint.TryParse(parsed[1], out task);
            }

            if (parsed.Length > 2)
            {
                int.TryParse(parsed[2], out align);
            }

            await user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Option,
                Text = parsed[0],
                OptionIndex = user.PushTaskId(task.ToString()),
                Data = (ushort)align
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMenuEditAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] paramStrings = SplitParam(param, 3);
            if (paramStrings.Length < 3)
            {
                logger.Warning("Invalid input param length for {0}, param: {1}", action.Id, param);
                return false;
            }

            await user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Input,
                OptionIndex = user.PushTaskId(paramStrings[1]),
                Data = ushort.Parse(paramStrings[0]),
                Text = paramStrings[2]
            });

            return true;
        }

        private static async Task<bool> ExecuteActionMenuPicAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);

            ushort x = ushort.Parse(splitParam[0]);
            ushort y = ushort.Parse(splitParam[1]);

            await user.SendAsync(new MsgTaskDialog
            {
                TaskIdentity = (uint)((x << 16) | y),
                InteractionType = MsgTaskDialog.TaskInteraction.Avatar,
                Data = ushort.Parse(splitParam[2])
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMenuMessageAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
                Text = param,
                OptionIndex = user.PushTaskId(action.Data.ToString())
            });

            return true;
        }

        private static async Task<bool> ExecuteActionMenuCreateAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Finish
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMenuRandAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            string[] paramSplit = SplitParam(param, 2);

            int x = int.Parse(paramSplit[0]);
            int y = int.Parse(paramSplit[1]);
            double chance = 0.01;
            if (x > y)
            {
                chance = 99;
            }
            else
            {
                chance = x / (double)y;
                chance *= 100;
            }

            return await ChanceCalcAsync(chance);
        }

        private static async Task<bool> ExecuteActionMenuRandActionAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] paramSplit = SplitParam(param);
            if (paramSplit.Length == 0)
            {
                return false;
            }

            uint taskId = uint.Parse(paramSplit[await NextAsync(0, paramSplit.Length) % paramSplit.Length]);
            await ExecuteActionAsync(taskId, user, role, item, inputs);
            return true;
        }

        private static async Task<bool> ExecuteActionMenuChkTimeAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] paramSplit = SplitParam(param);

            DateTime actual = DateTime.Now;
            var nCurWeekDay = (int)actual.DayOfWeek;
            int nCurHour = actual.Hour;
            int nCurMinute = actual.Minute;

            switch (action.Data)
            {
                #region Complete date (yyyy-mm-dd hh:mm yyyy-mm-dd hh:mm)

                case 0:
                    {
                        if (paramSplit.Length < 4)
                        {
                            return false;
                        }

                        string[] time0 = paramSplit[1].Split(':');
                        string[] date0 = paramSplit[0].Split('-');
                        string[] time1 = paramSplit[3].Split(':');
                        string[] date1 = paramSplit[2].Split('-');

                        var dTime0 = new DateTime(int.Parse(date0[0]), int.Parse(date0[1]), int.Parse(date0[2]),
                            int.Parse(time0[0]), int.Parse(time0[1]), 0);
                        var dTime1 = new DateTime(int.Parse(date1[0]), int.Parse(date1[1]), int.Parse(date1[2]),
                            int.Parse(time1[0]), int.Parse(time1[1]), 59);

                        return dTime0 <= actual && dTime1 >= actual;
                    }

                #endregion

                #region On Year date (mm-dd hh:mm mm-dd hh:mm)

                case 1:
                    {
                        if (paramSplit.Length < 4)
                        {
                            return false;
                        }

                        string[] time0 = paramSplit[1].Split(':');
                        string[] date0 = paramSplit[0].Split('-');
                        string[] time1 = paramSplit[3].Split(':');
                        string[] date1 = paramSplit[2].Split('-');

                        var dTime0 = new DateTime(DateTime.Now.Year, int.Parse(date0[1]), int.Parse(date0[2]),
                            int.Parse(time0[0]), int.Parse(time0[1]), 0);
                        var dTime1 = new DateTime(DateTime.Now.Year, int.Parse(date1[1]), int.Parse(date1[2]),
                            int.Parse(time1[0]), int.Parse(time1[1]), 59);

                        return dTime0 <= actual && dTime1 >= actual;
                    }

                #endregion

                #region Day of the month (dd hh:mm dd hh:mm)

                case 2:
                    {
                        if (paramSplit.Length < 4)
                        {
                            return false;
                        }

                        string[] time0 = paramSplit[1].Split(':');
                        string date0 = paramSplit[0];
                        string[] time1 = paramSplit[3].Split(':');
                        string date1 = paramSplit[2];

                        var dTime0 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(date0),
                            int.Parse(time0[0]), int.Parse(time0[1]), 0);
                        var dTime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(date1),
                            int.Parse(time1[0]), int.Parse(time1[1]), 59);

                        return dTime0 <= actual && dTime1 >= actual;
                    }

                #endregion

                #region Day of the week (dw hh:mm dw hh:mm)

                case 3:
                    {
                        if (paramSplit.Length < 4)
                        {
                            return false;
                        }

                        string[] time0 = paramSplit[1].Split(':');
                        string[] time1 = paramSplit[3].Split(':');

                        int nDay0 = int.Parse(paramSplit[0]);
                        int nDay1 = int.Parse(paramSplit[2]);
                        int nHour0 = int.Parse(time0[0]);
                        int nHour1 = int.Parse(time1[0]);
                        int nMinute0 = int.Parse(time0[1]);
                        int nMinute1 = int.Parse(time1[1]);

                        int timeNow = nCurWeekDay * 24 * 60 + nCurHour * 60 + nCurMinute;
                        int from = nDay0 * 24 * 60 + nHour0 * 60 + nMinute0;
                        int to = nDay1 * 24 * 60 + nHour1 * 60 + nMinute1;

                        return timeNow >= from && timeNow <= to;
                    }

                #endregion

                #region Hour check (hh:mm hh:mm)

                case 4:
                    {
                        if (paramSplit.Length < 2)
                        {
                            return false;
                        }

                        string[] time0 = paramSplit[0].Split(':');
                        string[] time1 = paramSplit[1].Split(':');

                        int nHour0 = int.Parse(time0[0]);
                        int nHour1 = int.Parse(time1[0]);
                        int nMinute0 = int.Parse(time0[1]);
                        int nMinute1 = int.Parse(time1[1]);

                        int timeNow = nCurHour * 60 + nCurMinute;
                        int from = nHour0 * 60 + nMinute0;
                        int to = nHour1 * 60 + nMinute1;

                        return timeNow >= from && timeNow <= to;
                    }

                #endregion

                #region Minute check (mm mm)

                case 5:
                    {
                        if (paramSplit.Length < 2)
                        {
                            return false;
                        }

                        return nCurMinute >= int.Parse(paramSplit[0]) && nCurMinute <= int.Parse(paramSplit[1]);
                    }

                    #endregion
            }

            return false;
        }

        private static async Task<bool> ExecuteActionPostcmdAsync(DbAction action, string param, Character user, Role role,
            Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(new MsgAction
            {
                Identity = user.Identity,
                Command = action.Data,
                Action = ActionType.ClientCommand,
                ArgumentX = user.X,
                ArgumentY = user.Y
            });
            return true;
        }

        private static async Task<bool> ExecuteActionBrocastmsgAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            await RoleManager.BroadcastWorldMsgAsync(param, (TalkChannel)action.Data, Color.White);
            return true;
        }

        private static async Task<bool> ExecuteActionSysExecActionAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                return false;
            }

            if (!int.TryParse(splitParams[0], out int secSpan)
                || !uint.TryParse(splitParams[1], out uint idAction))
            {
                return false;
            }

            ScriptManager.QueueAction(new QueuedAction(secSpan, idAction, 0));
            return true;
        }

        private static async Task<bool> ExecuteActionExecutequeryAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            try
            {
                if (param.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase) ||
                    param.Trim().StartsWith("UPDATE", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!param.Contains("WHERE") || !param.Contains("LIMIT"))
                    {
                        logger.Error($"ExecuteActionExecutequery {action.Id} doesn't have WHERE or LIMIT clause [{param}]");
                        return false;
                    }
                }

                await using var ctx = new ServerDbContext();
                await ctx.Database.ExecuteSqlRawAsync(param);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Could not execute query action {0}", ex.Message);
                return false;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionSysInviteFilterAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (action.Data == 0) // create invitation list
            {
                string[] splitParam = SplitParam(param);
                if (splitParam.Length < 4 || splitParam.Length % 3 != 1)
                {
                    logger.Warning("Invalid param count for ExecuteActionSysInviteFilterAsync({0}): eventId (attr opt value)[]", param);
                    return true;
                }

                if (!int.TryParse(splitParam[0], out int idEvent))
                {
                    logger.Warning("Invalid idEvent for ExecuteActionSysInviteFilterAsync: {0}", param);
                    return true;
                }

                var players = RoleManager.QueryUserSet().AsEnumerable();
                for (int i = 1; i < splitParam.Length; i += 3)
                {
                    string attr = splitParam[i];
                    string opt = splitParam[i + 1];
                    string valueString = splitParam[i + 2];
                    int.TryParse(valueString, out var value);

                    if (attr.Equals("level"))
                    {
                        if (opt.Equals("=="))
                        {
                            players = players.Where(x => x.Level == value);
                        }
                        else if (opt.Equals("<="))
                        {
                            players = players.Where(x => x.Level <= value);
                        }
                        else if (opt.Equals(">="))
                        {
                            players = players.Where(x => x.Level >= value);
                        }
                    }
                    else if (attr.Equals("profession"))
                    {
                        if (opt.Equals("=="))
                        {
                            players = players.Where(x => x.Profession == value);
                        }
                        else if (opt.Equals("<="))
                        {
                            players = players.Where(x => x.Profession <= value);
                        }
                        else if (opt.Equals(">="))
                        {
                            players = players.Where(x => x.Profession >= value);
                        }
                    }
                    else if (attr.Equals("rankshow"))
                    {
                        if (opt.Equals("=="))
                        {
                            players = players.Where(x => (int)x.SyndicateRank == value);
                        }
                        else if (opt.Equals("<="))
                        {
                            players = players.Where(x => (int)x.SyndicateRank <= value);
                        }
                        else if (opt.Equals(">="))
                        {
                            players = players.Where(x => (int)x.SyndicateRank >= value);
                        }
                    }
                    else if (attr.Equals("metempsychosis"))
                    {
                        if (opt.Equals("=="))
                        {
                            players = players.Where(x => x.Metempsychosis == value);
                        }
                        else if (opt.Equals("<="))
                        {
                            players = players.Where(x => x.Metempsychosis <= value);
                        }
                        else if (opt.Equals(">="))
                        {
                            players = players.Where(x => x.Metempsychosis >= value);
                        }
                    }
                    else if (attr.Equals("battlelev"))
                    {
                        if (opt.Equals("=="))
                        {
                            players = players.Where(x => x.BattlePower == value);
                        }
                        else if (opt.Equals("<="))
                        {
                            players = players.Where(x => x.BattlePower <= value);
                        }
                        else if (opt.Equals(">="))
                        {
                            players = players.Where(x => x.BattlePower >= value);
                        }
                    }
                }

                foreach (var player in players)
                {
                    ScriptManager.AddToInvitationList(idEvent, player.Identity);
                }
                return true;
            }
            else if (action.Data == 1) // clear invitation list
            {
                if (int.TryParse(param, out var value))
                {
                    ScriptManager.ClearInvitationList(value);
                }
                return true;
            }
            return false;
        }

        private static async Task<bool> ExecuteActionInviteTransAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            string[] split = param.Trim().Split(' ');
            if (split.Length != 21)
            {
                logger.Warning("ActionInviteTrans must have 21 parameters. mapid 8pos msg acceptmsg type seconds");
                return false;
            }

            uint idMap = uint.Parse(split[0]);
            ushort[] px = new ushort[8];
            ushort[] py = new ushort[8];
            for (int i = 0; i < 8; i++)
            {
                px[i] = ushort.Parse(split[i * 2 + 1]);
                py[i] = ushort.Parse(split[i * 2 + 2]);
            }
            int sendMsg = int.Parse(split[17]);
            int acceptMsg = int.Parse(split[18]);
            int eventId = int.Parse(split[19]);
            int seconds = int.Parse(split[20]);

            var invitatedPlayersIds = ScriptManager.QueryInvitationList(eventId);
            foreach (var idUser in invitatedPlayersIds)
            {
                try
                {
                    var target = RoleManager.GetUser(idUser);
                    if (target?.Map == null)
                    {
                        continue;
                    }

                    if (target.Map.IsPrisionMap())
                    {
                        continue;
                    }

                    if (target.Map.IsTeleportDisable())
                    {
                        continue;
                    }

                    if (target.Map.IsArenicMapInGeneral())
                    {
                        continue;
                    }

                    TimedMessageBox timedMessageBox = new(target, seconds);
                    timedMessageBox.MessageId = sendMsg;
                    timedMessageBox.AcceptMsgId = acceptMsg;
                    timedMessageBox.InviteId = eventId;
                    timedMessageBox.TargetMapIdentity = idMap;
                    timedMessageBox.TargetMapX = px;
                    timedMessageBox.TargetMapY = py;
                    target.MessageBox = timedMessageBox;
                    await timedMessageBox.SendAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Failed to send invitation for player [{0}]. Error: {1}", idUser, ex.Message);
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionSysPathFindingAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] @params = param.Split(' ');
            ushort x = ushort.Parse(@params[0]);
            ushort y = ushort.Parse(@params[1]);
            uint npc = uint.Parse(@params[2]);
            uint idMap = user.MapIdentity;

            if (@params.Length > 3)
            {
                idMap = uint.Parse(@params[3]);
            }

            var msg = new MsgAction();
            msg.Action = ActionType.PathFinding;
            msg.Identity = user.Identity;
            msg.Timestamp = npc;
            msg.Command = idMap;
            msg.X = x;
            msg.Y = y;
            await user.SendAsync(msg);
            return true;
        }

        private static async Task<bool> ExecuteActionVipFunctionCheckAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }
            return user.UserVipFlag.HasFlag((VipFlags)action.Data);
        }

        private static async Task<bool> ExecuteActionDynaGlobalDataAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            string[] splitParams = SplitParam(param, 4);
            if (splitParams.Length < 3)
            {
                logger.Warning("Invalid number of params for action {action}", action.Id);
                return false;
            }

            uint dataId = uint.Parse(splitParams[0]);
            string type = splitParams[1];
            string opt = splitParams[2];
            string strValue = string.Empty;// = splitParams[3];
            if (splitParams.Length > 3)
            {
                strValue = splitParams[3];
            }
            long.TryParse(strValue, out var value);
            long cmpValue = 0;
            string cmpStrValue = string.Empty;

            DbDynaGlobalData data = await DynamicGlobalDataManager.GetAsync(action.Data);
            if (type.Equals("data"))
            {
                cmpValue = DynamicGlobalDataManager.GetData(data, (int)dataId);
                if (opt.Equals("set"))
                {
                    DynamicGlobalDataManager.ChangeData(data, (int)dataId, value);
                    return await DynamicGlobalDataManager.SaveAsync(data);
                }
                else if (opt.Equals("+="))
                {
                    DynamicGlobalDataManager.ChangeData(data, (int)dataId, cmpValue + value);
                    return await DynamicGlobalDataManager.SaveAsync(data);
                }
                else if (opt.Equals("resetall"))
                {
                    DynamicGlobalDataManager.ChangeData(data, 0, 0);
                    DynamicGlobalDataManager.ChangeData(data, 1, 0);
                    DynamicGlobalDataManager.ChangeData(data, 2, 0);
                    DynamicGlobalDataManager.ChangeData(data, 3, 0);
                    DynamicGlobalDataManager.ChangeData(data, 4, 0);
                    DynamicGlobalDataManager.ChangeData(data, 5, 0);
                    return await DynamicGlobalDataManager.SaveAsync(data);
                }
            }
            else if (type.Equals("datastr"))
            {
                cmpStrValue = DynamicGlobalDataManager.GetStringData(data, (int)dataId);
                if (opt.Equals("set"))
                {
                    DynamicGlobalDataManager.ChangeStringData(data, (int)dataId, strValue);
                    return await DynamicGlobalDataManager.SaveAsync(data);
                }
            }
            else if (type.Equals("time"))
            {
                cmpValue = DynamicGlobalDataManager.GetData(data, (int)dataId);
                if (opt.Equals("set"))
                {
                    DynamicGlobalDataManager.ChangeTime(data, (int)dataId, (uint)value);
                    return await DynamicGlobalDataManager.SaveAsync(data);
                }
            }
            else
            {
                logger.Warning("ExecuteActionDynaGlobalDataAsync: Invalid cmp type for type 150 [{0}] {1}", type, action.Id);
                return false;
            }

            switch (opt)
            {
                case "==":
                    {
                        if (type.Equals("datastr"))
                        {
                            return cmpStrValue.Equals(strValue);
                        }
                        else
                        {
                            return value == cmpValue;
                        }
                    }
                case "!=": return cmpValue != value;
                case ">=": return cmpValue >= value;
                case "<=": return cmpValue <= value;
                case ">": return cmpValue > value;
                case "<": return cmpValue < value;
            }

            return true;
        }

        #endregion
    }
}
