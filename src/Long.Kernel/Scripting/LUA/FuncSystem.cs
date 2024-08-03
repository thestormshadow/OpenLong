using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States;
using Long.Kernel.States.Mails;
using Long.Kernel.States.MessageBoxes;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Helpers;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        [LuaFunction]
        public bool MenuText(string text, int line)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(text))
            {
                return true;
            }

            user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Dialog,
                Text = text,
                Data = (ushort)line
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MenuLink(string text, int align, string function)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Option,
                Text = text,
                OptionIndex = user.PushTaskId(function),
                Data = (ushort)align
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MenuEdit(string text, int length, int password, string function)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Input,
                OptionIndex = user.PushTaskId(function),
                Data = (ushort)length,
                Text = text
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MenuPic(int w, int h, int faceNum, string function)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            user.SendAsync(new MsgTaskDialog
            {
                TaskIdentity = (uint)(w << 16 | h),
                InteractionType = MsgTaskDialog.TaskInteraction.Avatar,
                Data = (ushort)faceNum
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MsgBox(string text, string successFunction, string failFunction)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
                Text = text,
                OptionIndex = user.PushTaskId(successFunction)
            }).GetAwaiter().GetResult();
            user.PushTaskId(failFunction);
            return true;
        }

        [LuaFunction]
        public bool MenuCreate(int userId, string function)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.Finish
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MenuTaskClear(int idUser)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return false;
            }
            user.ClearTaskId();
            return true;
        }

        [LuaFunction]
        public bool PostCmd(int data)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }

            user.SendAsync(new MsgAction
            {
                Identity = user.Identity,
                Command = (uint)data,
                Action = ActionType.ClientCommand,
                ArgumentX = user.X,
                ArgumentY = user.Y
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public string GetAcceptStr()
        {
            if (input.Length < 1)
            {
                return string.Empty;
            }
            return input[0];
        }

        [LuaFunction]
        public bool CheckTime(int type, string param)
        {
            string[] paramSplit = param.Split(' ');

            DateTime actual = DateTime.Now;
            var nCurWeekDay = (int)actual.DayOfWeek;
            int nCurHour = actual.Hour;
            int nCurMinute = actual.Minute;

            switch (type)
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

        [LuaFunction]
        public void BrocastMsg(int talkChannel, string content)
        {
            RoleManager.BroadcastWorldMsgAsync(content, (TalkChannel)talkChannel, Color.White).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool GotoSomeWhere(int idUser, int nPosX, int nPosY, int idNpc, int idMap)
        {
            Character user = GetUser(idUser);
            if (user == null)
            {
                return false;
            }

            var msg = new MsgAction
            {
                Action = ActionType.PathFinding,
                Identity = user.Identity,
                Timestamp = (uint)idNpc,
                Command = (uint)idMap,
                X = (ushort)nPosX,
                Y = (ushort)nPosY
            };
            user.SendAsync(msg).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public void MouseWaitClick(int userId, int mouseType, string function)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return;
            }

            user.InteractingMouseFunction = function;
            user.SendAsync(new MsgAction
            {
                Action = ActionType.MouseSetFace,
                X = user.X,
                Y = user.Y,
                Identity = user.Identity,
                Command = (uint)mouseType
            }).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool MouseJudgeType(int userId, int type, object param)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            bool success = true;
            switch (type)
            {
                case 1:
                    {
                        if (role is not Npc npc || !npc.Name.Equals(param))
                        {
                            success = false;
                        }
                        break;
                    }
                case 2:
                    {
                        if (role is not Monster monster || monster.Type != uint.Parse(param.ToString()))
                        {
                            success = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (role is not Character targetUser || targetUser.Gender != byte.Parse(param.ToString()))
                        {
                            success = false;
                        }
                        break;
                    }

                default:
                    {
                        success = false;
                        break;
                    }
            }

            if (success)
            {
                user.InteractingNpc = role.Identity;
                user.SendAsync(new MsgAction
                {
                    Action = ActionType.MouseResetFace,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                }).GetAwaiter().GetResult();
            }
            else
            {
                user.SendAsync(new MsgAction
                {
                    Action = ActionType.MouseResetClick,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                }).GetAwaiter().GetResult();
            }
            return success;
        }

        [LuaFunction]
        public bool MouseClearStatus(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            user.InteractingMouseAction = 0;
            user.InteractingMouseFunction = string.Empty;

            user.SendAsync(new MsgAction
            {
                Action = ActionType.MouseResetFace,
                X = user.X,
                Y = user.Y,
                Identity = user.Identity,
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool MouseDeleteChosen(int userId)
        {
            if (role != null)
            {
                role.LeaveMapAsync().GetAwaiter().GetResult();
                return true;
            }
            return false;
        }

        [LuaFunction]
        public bool SaveCustomLog(string file, string text)
        {
            string[] splitName = file.Split('/');
            if (splitName[0].Equals("gmlog"))
            {
                ILogger logger = Logger.CreateLogger(splitName[^1]);
                logger.Information(text);
            }
            else
            {
                ILogger logger = Logger.CreateLogger("lua_processor");
                logger.Information(text);
            }
            return true;
        }

        [LuaFunction]
        public void SaveDebugLog(string text, params object[] args)
        {
            ILogger logger = Logger.CreateLogger("lua_processor_dbg");
            logger.Information(text, args);
        }

        [LuaFunction]
        public long GetSynaGlobalData(int globalData, int index)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            switch (index)
            {
                case G_DYNA_GLOBAL_DATA0: return data.Data0;
                case G_DYNA_GLOBAL_DATA1: return data.Data1;
                case G_DYNA_GLOBAL_DATA2: return data.Data2;
                case G_DYNA_GLOBAL_DATA3: return data.Data3;
                case G_DYNA_GLOBAL_DATA4: return data.Data4;
                case G_DYNA_GLOBAL_DATA5: return data.Data5;
                default: return 0;
            }
        }

        [LuaFunction]
        public string GetSynaGlobalDataStr(int globalData, int index)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            switch (index)
            {
                case G_DYNA_GLOBAL_DATASTR0: return data.Datastr0;
                case G_DYNA_GLOBAL_DATASTR1: return data.Datastr1;
                case G_DYNA_GLOBAL_DATASTR2: return data.Datastr2;
                case G_DYNA_GLOBAL_DATASTR3: return data.Datastr3;
                case G_DYNA_GLOBAL_DATASTR4: return data.Datastr4;
                case G_DYNA_GLOBAL_DATASTR5: return data.Datastr5;
                default: return string.Empty;
            }
        }

        [LuaFunction]
        public int GetSynaGlobalTime(int globalData, int index)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            switch (index)
            {
                case G_DYNA_GLOBAL_TIME0: return (int)data.Time0;
                case G_DYNA_GLOBAL_TIME1: return (int)data.Time1;
                case G_DYNA_GLOBAL_TIME2: return (int)data.Time2;
                case G_DYNA_GLOBAL_TIME3: return (int)data.Time3;
                case G_DYNA_GLOBAL_TIME4: return (int)data.Time4;
                case G_DYNA_GLOBAL_TIME5: return (int)data.Time5;
                default: return 0;
            }
        }

        [LuaFunction]
        public bool ResetAllSynaGlobalData(int globalData)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            data.Data0 = data.Data1 = data.Data2 = data.Data3 = data.Data4 = data.Data5 = 0;
            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool ResetAllSynaGlobalDataStr(int globalData)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            data.Datastr0 = data.Datastr1 = data.Datastr2 = data.Datastr3 = data.Datastr4 = data.Datastr5 = "";
            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool ResetAllSynaGlobalTime(int globalData)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            data.Time0 = data.Time1 = data.Time2 = data.Time3 = data.Time4 = data.Time5 = 0;
            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool SetSynaGlobalData(int globalData, int index, long value)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            switch (index)
            {
                case G_DYNA_GLOBAL_DATA0:
                    DynamicGlobalDataManager.ChangeData(data, 0, value);
                    break;
                case G_DYNA_GLOBAL_DATA1:
                    DynamicGlobalDataManager.ChangeData(data, 1, value);
                    break;
                case G_DYNA_GLOBAL_DATA2:
                    DynamicGlobalDataManager.ChangeData(data, 2, value);
                    break;
                case G_DYNA_GLOBAL_DATA3:
                    DynamicGlobalDataManager.ChangeData(data, 3, value);
                    break;
                case G_DYNA_GLOBAL_DATA4:
                    DynamicGlobalDataManager.ChangeData(data, 4, value);
                    break;
                case G_DYNA_GLOBAL_DATA5:
                    DynamicGlobalDataManager.ChangeData(data, 5, value);
                    break;
                default:
                    return false;
            }
            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool SetSynaGlobalDataStr(int globalData, int index, string value)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            switch (index)
            {
                case G_DYNA_GLOBAL_DATASTR0: DynamicGlobalDataManager.ChangeStringData(data, 0, value); break;
                case G_DYNA_GLOBAL_DATASTR1: DynamicGlobalDataManager.ChangeStringData(data, 1, value); break;
                case G_DYNA_GLOBAL_DATASTR2: DynamicGlobalDataManager.ChangeStringData(data, 2, value); break;
                case G_DYNA_GLOBAL_DATASTR3: DynamicGlobalDataManager.ChangeStringData(data, 3, value); break;
                case G_DYNA_GLOBAL_DATASTR4: DynamicGlobalDataManager.ChangeStringData(data, 4, value); break;
                case G_DYNA_GLOBAL_DATASTR5: DynamicGlobalDataManager.ChangeStringData(data, 5, value); break;
                default: return false;
            }

            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool SetSynaGlobalTime(int globalData, int index, uint value)
        {
            DbDynaGlobalData data = DynamicGlobalDataManager.GetAsync((uint)globalData).GetAwaiter().GetResult();
            if (data == null)
                return false;

            switch (index)
            {
                case G_DYNA_GLOBAL_TIME0: DynamicGlobalDataManager.ChangeTime(data, 0, value); break;
                case G_DYNA_GLOBAL_TIME1: DynamicGlobalDataManager.ChangeTime(data, 1, value); break;
                case G_DYNA_GLOBAL_TIME2: DynamicGlobalDataManager.ChangeTime(data, 2, value); break;
                case G_DYNA_GLOBAL_TIME3: DynamicGlobalDataManager.ChangeTime(data, 3, value); break;
                case G_DYNA_GLOBAL_TIME4: DynamicGlobalDataManager.ChangeTime(data, 4, value); break;
                case G_DYNA_GLOBAL_TIME5: DynamicGlobalDataManager.ChangeTime(data, 5, value); break;
                default: return false;
            }

            DynamicGlobalDataManager.SaveAsync(data).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool SetLuaTimer(int interval, string function)
        {
            logger.Warning("SetLuaTimer(int interval, string function) not implemented [{},{}]", interval, function);
            return true;
        }

        [LuaFunction]
        public bool SetLuaEventTimer(int timerId, int interval)
        {
            logger.Warning("SetLuaEventTimer(int timerId, int interval) not implemented [{},{}]", timerId, interval);
            return true;
        }

        [LuaFunction]
        public bool ChkLuaEventTimer(int timerId)
        {
            logger.Warning("ChkLuaEventTimer(int timerId) not implemented [{}]", timerId);
            return true;
        }

        [LuaFunction]
        public bool KillLuaEventTimer(int timerId)
        {
            logger.Warning("KillLuaEventTimer(int timerId) not implemented [{}]", timerId);
            return true;
        }

        [LuaFunction]
        public bool InviteFilter(int inviteId, string param)
        {
            string[] splitParam = GameAction.SplitParam(param);
            if (splitParam.Length % 3 != 0)
            {
                logger.Warning("Invalid param count for InviteFilter({0}): eventId (attr opt value)[]", param);
                return true;
            }

            if (!int.TryParse(splitParam[0], out int idEvent))
            {
                logger.Warning("Invalid idEvent for InviteFilter: {0}", param);
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

        [LuaFunction]   
        public bool DelInvite(int inviteId)
        {
            ScriptManager.ClearInvitationList(inviteId);
            return true;
        }

        [LuaFunction]
        public bool InviteTrans(uint mapId,
            ushort transX0, ushort transY0,
            ushort transX1, ushort transY1,
            ushort transX2, ushort transY2,
            ushort transX3, ushort transY3,
            ushort transX4, ushort transY4,
            ushort transX5, ushort transY5,
            ushort transX6, ushort transY6,
            ushort transX7, ushort transY7,
            int sendMsg,
            int acceptMsg,
            int eventId,
            int seconds
            )
        {
            var px = new ushort[]
            {
                transX0, transX1, transX2, transX3,
                transX4, transX5, transX6, transX7
            };
            var py = new ushort[]
            {
                transY0, transY1, transY2, transY3,
                transY4, transY5, transY6, transY7
            };

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

                    TimedMessageBox timedMessageBox = new(target, seconds)
                    {
                        MessageId = sendMsg,
                        AcceptMsgId = acceptMsg,
                        InviteId = eventId,
                        TargetMapIdentity = mapId,
                        TargetMapX = px,
                        TargetMapY = py
                    };
                    target.MessageBox = timedMessageBox;
                    timedMessageBox.SendAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Failed to send invitation for player [{0}]. Error: {1}", idUser, ex.Message);
                }
            }

            return true;
        }

        [LuaFunction]
        public bool IsAccountServerNormal()
        {
            return RealmManager.IsAccountServerConnected();
        }

        [LuaFunction]
        public bool SendMail(int userId, ulong money, uint emoney, uint actionId, byte emoneyType, int existDays, string sender, string title, string content, int serverId)
        {
            // nUserId,nMoney,nEmoney,nActionId,nEmoneyType,nExistDay,sSender,sTitle,sContent,serverid
            return MailBox.SendAsync((uint)userId, sender, title, content, (uint)(UnixTimestamp.Now + 60 * 60 * 24 * existDays), money, emoney, emoneyType != 0, 0, 0, actionId).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool SendWebPage(int userId, string param)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            user.SendAsync(param, TalkChannel.Website).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool OpenDialog(int userId, int npcId, int dialogId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            BaseNpc npc = GetNpc(npcId);
            if (npcId != 0 && npc == null)
            {
                return false;
            }
            if (npc != null && user.GetDistance(npc) > Screen.BROADCAST_SIZE)
            {
                return false;
            }

            user.SendAsync(new MsgAction
            {
                Action = ActionType.ClientDialog,
                Identity = user.Identity,
                Command = (uint)dialogId,
                ArgumentX = user.X,
                ArgumentY = user.Y
            }).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool SendHandBrickInfo(int type)
        {
            Character user = GetUser(0);
            if (user == null)
            {
                return false;
            }   

            // plunder war bricks
            if (type == 0)
            {
                if (user.Union.Identity == KingdomManager.Kingdom?.Identity)
                {
                    user.SendAsync(new MsgHandBrickInfo
                    {
                        Action = MsgHandBrickInfo.BrickInfoAction.LeagueBricks,
                        Command = 1,
                        GoldBrick = 10000
                    }).GetAwaiter().GetResult();
                }
                else
                {
                    user.SendAsync(new MsgHandBrickInfo
                    {
                        Action = MsgHandBrickInfo.BrickInfoAction.OpponentBricks,
                        Command = 1,
                        GoldBrick = 123,  // bricks
                        LeagueName = "Teste",
                        CountryName = "Teste2"
                    }).GetAwaiter().GetResult();
                }
            }
            else
            {
                user.SendAsync(new MsgHandBrickInfo
                {
                    Action = MsgHandBrickInfo.BrickInfoAction.SubmitDialog,
                    Command = 0,
                    GoldBrick = 1234, // bricks
                }).GetAwaiter().GetResult();
            }
            return true;
        }
    }
}
