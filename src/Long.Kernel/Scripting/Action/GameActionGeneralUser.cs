using Long.Database.Entities;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Kernel.States;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using Long.Kernel.Managers;
using Long.Kernel.States.World;
using System.Drawing;
using static Long.Kernel.States.User.Character;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Status;
using Long.Kernel.States.Npcs;
using static Long.Kernel.Modules.Systems.AstProf.IAstProf;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionGeneralLotteryAsync(DbAction action, string param, Character user,
            Role role, Item item,
            params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (!user.IsLotteryEnable)
            {
                return false;
            }

            if (user.UserPackage.IsPackFull())
            {
                return false;
            }

            user.ResetLottery();

            if (await user.PlayLotteryAsync((int)action.Data))
            {
                await user.Statistic.IncrementDailyValueAsync(22, 0, 1);
                user.LotteryRetries++;
            }
            return true;
        }

        private static async Task<bool> ExecuteActionChgMapSquareAsync(DbAction action, string param, Character user,
            Role role, Item item,
            params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 6);
            if (!uint.TryParse(splitParams[0], out uint idMap))
            {
                logger.Warning("Invalid map id for ExecuteActionChgMapSquareAsync [{0}]", param);
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning("GameAction::ExecuteActionChgMapSquare invalid map {0}", idMap);
                return false;
            }

            ushort x, y, cx, cy;
            if (splitParams.Length >= 5)
            {
                if (!ushort.TryParse(splitParams[1], out x)
                    || !ushort.TryParse(splitParams[2], out y)
                    || !ushort.TryParse(splitParams[3], out cx)
                    || !ushort.TryParse(splitParams[4], out cy))
                {
                    return false;
                }
            }
            else
            {
                x = 0;
                y = 0;
                cx = (ushort)map.Width;
                cy = (ushort)map.Height;
            }

            int saveLocation = 0;
            if (splitParams.Length > 5)
            {
                int.TryParse(splitParams[5], out saveLocation);
            }

            Point point = await map.QueryRandomPositionAsync(x, y, cx, cy);
            if (!point.Equals(default))
            {
                await user.FlyMapAsync(idMap, point.X, point.Y);
                if (saveLocation != 0)
                {
                    await user.SavePositionAsync(idMap, (ushort)point.X, (ushort)point.Y);
                }
                return true;
            }
            return false;
        }

        private static async Task<bool> ExecuteActionOpenShopAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(new MsgAction
            {
                Identity = user.Identity,
                Command = 32,
                Action = ActionType.OpenShop,
                Timestamp = role.Identity
            });
            return true;
        }

        private static async Task<bool> ExecuteActionProgressBarAsync(DbAction action, string param, Character user, Role role,
            Item item,
            params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning("Progress bar action cannot have null user [ActionID: {0}]", action.Id);
                return false;
            }

            string[] splitParams = param.Split(' ');
            if (splitParams.Length < 3)
            {
                logger.Warning("Progress bar action parameters less than 3");
                return false;
            }

            int seconds = int.Parse(splitParams[0]);
            string message = splitParams[1];
            int unknown = int.Parse(splitParams[2]);

            ProgressBar progressBar = new(seconds + 1)
            {
                Command = (uint)unknown,
                IdNext = action.IdNext,
                IdNextFail = action.IdNextfail
            };
            user.AwaitingProgressBar = progressBar;

            await user.SendAsync(new MsgAction
            {
                Action = ActionType.ProgressBar,
                Identity = user.Identity,
                Command = (uint)unknown,
                Direction = 1,
                MapColor = (uint)seconds,
                Strings = new List<string>
                {
                    message.Replace('~', ' ')
                }
            });
            return true;
        }

        private static async Task<bool> ExecuteActionFrozenGrottoEntranceChkDaysAsync(DbAction action, string param, Character user,
                                                                    Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning("Invalid entity for action type {action.Type} [{action.Id}]");
                return false;
            }

            int days = int.Parse(param);
            DbVipMineTime vipMineTime = await VipMineTimeRepository.GetAsync(user.Identity);
            if (vipMineTime == null)
            {
                vipMineTime = new DbVipMineTime
                {
                    UserId = user.Identity,
                    LastEnterTime = (uint)UnixTimestamp.Now
                };
                await ServerDbContext.CreateAsync(vipMineTime);
            }
            else
            {
                int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int lastEntrance = int.Parse(UnixTimestamp.ToDateTime((int)vipMineTime.LastEnterTime).ToString("yyyyMMdd"));
                if (today - lastEntrance < days)
                {
                    return false;
                }
                vipMineTime.LastEnterTime = (uint)UnixTimestamp.Now;
                await ServerDbContext.UpdateAsync(vipMineTime);
            }
            return true;
        }

        private static async Task<bool> ExecuteActionUserCheckHpFullAsync(DbAction action, string param, Character user,
                                                                    Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            return user.Life >= user.MaxLife;
        }

        private static async Task<bool> ExecuteActionUserCheckHpManaFullAsync(DbAction action, string param, Character user,
                                                                    Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            return user.Mana >= user.MaxMana;
        }

        private static async Task<bool> ExecuteActionAttachBuffStatusAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param);
            if (splitParams.Length < 4)
            {
                logger.Warning("ExecuteActionAttachBuffStatusAsync invalid param count: {0}", param);
                return false;
            }

            int statusEffect = int.Parse(splitParams[0]);
            int statusPower = int.Parse(splitParams[1]);
            int originalPower = statusPower;
            int statusDuration = int.Parse(splitParams[2]);

            switch (statusEffect)
            {
                case StatusSet.BUFF_PSTRIKE:
                case StatusSet.BUFF_MSTRIKE:
                case StatusSet.BUFF_IMMUNITY:
                    {
                        statusPower *= 100;
                        break;
                    }

                case StatusSet.BUFF_BREAK:
                case StatusSet.BUFF_COUNTERACTION:
                    {
                        statusPower *= 10;
                        break;
                    }
            }

            await user.AttachStatusAsync(statusEffect + 1, statusPower, statusDuration, 0);

            ClientUpdateType updateType;
            switch (statusEffect)
            {
                case StatusSet.BUFF_PSTRIKE:
                    {
                        updateType = ClientUpdateType.PhysicCritRate;
                        break;
                    }
                case StatusSet.BUFF_MSTRIKE:
                    {
                        updateType = ClientUpdateType.MagicCritRate;
                        break;
                    }
                case StatusSet.BUFF_IMMUNITY:
                    {
                        updateType = ClientUpdateType.AntiCritRate;
                        break;
                    }
                case StatusSet.BUFF_BREAK:
                    {
                        updateType = ClientUpdateType.SmashAttackRate;
                        break;
                    }
                case StatusSet.BUFF_COUNTERACTION:
                    {
                        updateType = ClientUpdateType.FirmDefenseRate;
                        break;
                    }
                case StatusSet.BUFF_MAX_HEALTH:
                    {
                        updateType = ClientUpdateType.AddMaxLife;
                        break;
                    }
                case StatusSet.BUFF_PATTACK:
                    {
                        updateType = ClientUpdateType.AddPhysicAttack;
                        break;
                    }
                case StatusSet.BUFF_MATTACK:
                    {
                        updateType = ClientUpdateType.AddMagicAttack;
                        break;
                    }
                case StatusSet.BUFF_FINAL_PDMGREDUCTION:
                    {
                        updateType = ClientUpdateType.AddPhysicDamageReduce;
                        break;
                    }
                case StatusSet.BUFF_FINAL_MDMGREDUCTION:
                    {
                        updateType = ClientUpdateType.AddMagicDamageReduce;
                        break;
                    }
                case StatusSet.BUFF_FINAL_PDAMAGE:
                    {
                        updateType = ClientUpdateType.AddFinalPhysicDamage;
                        break;
                    }
                case StatusSet.BUFF_FINAL_MDAMAGE:
                    {
                        updateType = ClientUpdateType.AddFinalMagicDamage;
                        break;
                    }
                default:
                    return true;
            }

            await user.SynchroAttributesAsync(updateType, (uint)statusDuration, (uint)statusEffect, 0u, (uint)originalPower);
            return true;
        }

        private static async Task<bool> ExecuteActionDetachBuffStatusesAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            for (int i = StatusSet.BUFF_PSTRIKE + 1; i <= StatusSet.BUFF_FINAL_MDMGREDUCTION + 1; i++)
            {
                await user.DetachStatusAsync(i);
            }
            return true;
        }

        private static async Task<bool> ExecuteActionUserReturnAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (action.Data > user.VarData.Length)
            {
                return false;
            }

            int totalOfflineDays = 0;
            if (user.PreviousLoginTime.HasValue && user.PreviousLoginTime.Value.Year != 1970)
            {
                totalOfflineDays = (int)(DateTime.Now - user.PreviousLoginTime.Value).TotalDays;
            }
            user.VarData[(int)action.Data] = totalOfflineDays;
            return true;
        }

        private static async Task<bool> ExecuteActionMouseWaitClickAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = param.Split('-');
            uint mouseFace = uint.Parse(splitParams[0]);
            user.InteractingMouseAction = action.Data;
            await user.SendAsync(new MsgAction
            {
                Action = ActionType.MouseSetFace,
                X = user.X,
                Y = user.Y,
                Identity = user.Identity,
                Command = mouseFace
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMouseJudgeTypeAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null || role == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(param))
            {
                return false;
            }

            bool success = true;
            switch (action.Data)
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
                        if (role is not Monster monster || monster.Type != uint.Parse(param))
                        {
                            success = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (role is not Character targetUser || targetUser.Gender != byte.Parse(param))
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
                await user.SendAsync(new MsgAction
                {
                    Action = ActionType.MouseResetFace,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                });
            }
            else
            {
                await user.SendAsync(new MsgAction
                {
                    Action = ActionType.MouseResetClick,
                    X = user.X,
                    Y = user.Y,
                    Identity = user.Identity,
                });
            }
            return true;
        }

        private static async Task<bool> ExecuteActionMouseClearStatusAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            user.InteractingMouseAction = 0;
            user.InteractingMouseFunction = string.Empty;

            await user.SendAsync(new MsgAction
            {
                Action = ActionType.MouseResetFace,
                X = user.X,
                Y = user.Y,
                Identity = user.Identity,
            });
            return true;
        }

        private static async Task<bool> ExecuteActionMouseDeleteChosenAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (role == null)
            {
                return false;
            }
            await role.LeaveMapAsync();
            return true;
        }

        private static async Task<bool> ExecuteActionCheckUserAttributeLimitAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            int totalPoints = user.Strength + user.Speed + user.Vitality + user.Spirit + user.AttributePoints;
            return totalPoints < Role.MAX_USER_ATTRIB_POINTS;
        }

        private static async Task<bool> ExecuteActionUserDecLifeAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null || !user.IsAlive)
            {
                return false;
            }

            if (!int.TryParse(param, out var percent))
            {
                return false;
            }

            switch (action.Data)
            {
                case 0:
                    {
                        int reduceLife = (int)(user.MaxLife * percent / 100d);
                        await user.SetAttributesAsync(ClientUpdateType.Hitpoints, (ulong)Math.Max(1, user.Life - reduceLife));
                        return true;
                    }
                case 1:
                    {
                        int reduceLife = (int)(user.Life * percent / 100d);
                        await user.SetAttributesAsync(ClientUpdateType.Hitpoints, (ulong)Math.Max(1, user.Life - reduceLife));
                        return true;
                    }
            }
            return false;
        }

        private static async Task<bool> ExecuteActionAchievementsAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning("ExecuteActionAchievementsAsync> Invalid actor for action [{0}]", action.Id);
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            if (param.Equals("chk"))
            {
                return user.Achievements.HasAchievement((int)action.Data);
            }
            else if (param.Equals("add"))
            {
                if (user.Achievements.HasAchievement((int)action.Data))
                {
                    return false;
                }

                return await user.Achievements.AwardAchievementAsync((int)action.Data);
            }
            return false;
        }

        private static async Task<bool> ExecuteActionAddProcessActivityTaskAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            await ActivityManager.UpdateTaskActivityAsync(user, (ActivityManager.ActivityType)action.Data);
            return true;
        }

        private static async Task<bool> ExecuteActionAddProcessTaskSchedleAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 2);
            int.TryParse(splitParams[0], out var condition);
            long.TryParse(splitParams[1], out var value);

            await user.StageGoal.SetProgressAsync((ProcessGoalManager.GoalType)action.Data, (uint)value);
            return true;
        }

        private static async Task<bool> ExecuteActionSubclassLearnAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.AstProf == null)
            {
                logger.Warning("Action {0} called without an actor", action.Id);
                return false;
            }

            if (!int.TryParse(param, out var intType))
            {
                logger.Warning("Action {0} invalid param", action.Id);
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            AstProfType type = (AstProfType)intType;
            return await user.AstProf.LearnAsync(type, true);
        }

        private static async Task<bool> ExecuteActionSubclassLevelAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.AstProf == null)
            {
                logger.Warning("Action {0} called without an actor", action.Id);
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                logger.Warning("Action {0} param '{1}' needs at least 3 params", action.Id, param);
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            AstProfType type = (AstProfType)int.Parse(splitParams[0]);
            string opt = splitParams[1];
            int level = int.Parse(splitParams[2]);

            if (opt.Equals("<"))
            {
                return user.AstProf.GetLevel(type) < level;
            }
            else if (opt.Equals("=="))
            {
                return user.AstProf.GetLevel(type) == level;
            }

            logger.Warning("Invalid operation for action {0}! Operation: {1}", action.Id, opt);
            return false;
        }

        private static async Task<bool> ExecuteActionSubclassPromotionAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.AstProf == null)
            {
                logger.Warning("Action {0} called without an actor", action.Id);
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                logger.Warning("Action {0} param '{1}' needs at least 3 params", action.Id, param);
                return false;
            }

            if (user.IsOSUser())
            {
                return false;
            }

            AstProfType type = (AstProfType)int.Parse(splitParams[0]);
            string opt = splitParams[1];
            int rank = int.Parse(splitParams[2]);

            if (opt.Equals("<"))
            {
                return user.AstProf.GetPromotion(type) < rank;
            }
            else if (opt.Equals("=="))
            {
                return user.AstProf.GetPromotion(type) == rank;
            }
            else if (opt.Equals("set"))
            {
                return await user.AstProf.PromoteAsync(type, rank);
            }
            logger.Warning("Invalid operation for action {0}! Operation: {1}", action.Id, opt);
            return false;
        }

        #region Jiang Hu

        private static async Task<bool> ExecuteActionJiangHuInscribedAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.JiangHu == null)
            {
                logger.Warning("ExecuteActionJiangHuInscribedAsync[{0}] called with no actor", action.Id);
                return false;
            }

            return user.JiangHu.HasJiangHu;
        }

        private static async Task<bool> ExecuteActionJiangHuLevelAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.JiangHu == null)
            {
                logger.Warning("ExecuteActionJiangHuLevelAsync[{0}] called with no actor", action.Id);
                return false;
            }

            if (!user.JiangHu.HasJiangHu)
            {
                return false;
            }

            return user.JiangHu.Grade >= action.Data;
        }

        private static async Task<bool> ExecuteActionJiangHuExpProtectionAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.JiangHu == null)
            {
                logger.Warning("ExecuteActionJiangHuExpProtectionAsync[{0}] called with no actor", action.Id);
                return false;
            }

            if (!user.JiangHu.HasJiangHu)
            {
                return false;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionJiangHuAttributesAsync(DbAction action, string param, Character user, Role role, Item item, params string[] inputs)
        {
            if (user?.JiangHu == null)
            {
                logger.Warning("ExecuteActionJiangHuAttributesAsync[{0}] called with no actor", action.Id);
                return false;
            }

            if (user.JiangHu == null || !user.JiangHu.HasJiangHu)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                return false;
            }

            string query = splitParams[0];
            string opt = splitParams[1];
            int value = int.Parse(splitParams[2]);

            int compare = 0;
            if (query.Equals("genuineqi"))
            {
                if (opt.Equals("+="))
                {
                    if (value < 0)
                    {
                        return await user.JiangHu.SpendTalentAsync((byte)value);
                    }

                    await user.JiangHu.AwardTalentAsync((byte)value);
                    return true;
                }

                compare = user.JiangHu.Talent;
            }
            else if (query.Equals("freecultivateparam"))
            {
                if (opt.Equals("+="))
                {
                    if (value < 0)
                    {
                        if (user.JiangHu.FreeCaltivateParam < value)
                        {
                            return false;
                        }
                        user.JiangHu.FreeCaltivateParam -= (uint)value;
                        return true;
                    }

                    user.JiangHu.FreeCaltivateParam += (uint)value;
                    return true;
                }

                compare = (int)user.JiangHu.FreeCaltivateParam;
            }

            switch (opt)
            {
                case "==": return compare == value;
                case "<=": return compare <= value;
                case ">=": return compare >= value;
                case "<": return compare < value;
                case ">": return compare > value;
            }

            return false;
        }

        #endregion
    }
}
