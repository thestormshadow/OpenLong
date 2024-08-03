using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class ActivityManager
    {
        public const uint StcEventId = 2822;

        private static readonly ILogger logger = Log.ForContext<ActivityManager>();

        private static ConcurrentDictionary<uint, List<ActivityTask>> userTasks = new();
        private static ConcurrentDictionary<uint, DbActivityRewardType> rewardTypes = new();
        private static ConcurrentDictionary<uint, DbActivityTaskType> taskTypes = new();

        public static async Task InitializeAsync()
        {
            logger.Information($"Activity manager initializing");

            foreach (var rewardType in ActivityRepository.GetRewards())
            {
                rewardTypes.TryAdd(rewardType.Id, rewardType);
            }

            foreach (var task in ActivityRepository.GetTasks())
            {
                taskTypes.TryAdd(task.Id, task);
            }
        }

        public static async Task<bool> CheckForActivityTaskUpdatesAsync(Character user)
        {
            bool result = false;
            var userTasks = GetUserTaskList(user);
            foreach (var availableTask in GetDisponibleTaskByUser(user))
            {
                if (userTasks.All(x => (byte)x.Type != availableTask.Type))
                {
                    var dbTask = ActivityRepository.GetUserTasks(user.Identity).FirstOrDefault(x => x.ActivityId == availableTask.Id);
                    dbTask ??= new DbActivityUserTask
                    {
                        ActivityId = availableTask.Id,
                        UserId = user.Identity
                    };
                    var task = new ActivityTask(dbTask);
                    userTasks.Add(task);
                    await task.SaveAsync();
                    result = true;
                }
            }
            return result;
        }

        public static List<ActivityTask> GetUserTaskList(Character user)
        {
            if (userTasks.TryGetValue(user.Identity, out var taskList))
            {
                return taskList;
            }

            taskList = new List<ActivityTask>();
            foreach (var task in ActivityRepository.GetUserTasks(user.Identity))
            {
                taskList.Add(new ActivityTask(task));
            }

            foreach (var userTask in GetDisponibleTaskByUser(user))
            {
                if (taskList.Any(x => x.ActivityId == userTask.Id))
                {
                    continue;
                }

                var task = new ActivityTask(new DbActivityUserTask
                {
                    ActivityId = userTask.Id,
                    UserId = user.Identity
                });
                taskList.Add(task);
            }
            userTasks.TryAdd(user.Identity, taskList);
            return taskList;
        }

        public static List<DbActivityTaskType> GetDisponibleTaskByUser(Character user)
        {
            List<DbActivityTaskType> taskTypes = new List<DbActivityTaskType>();
            foreach (var task in ActivityManager.taskTypes.Values.OrderByDescending(x => x.Id))
            {
                int openRebirth = (int)(task.OpenLev / 1000);
                int openLevel = (int)(task.OpenLev % 1000);
                int closeRebirth = (int)(task.CloseLev / 1000);
                int closeLevel = (int)(task.CloseLev % 1000);

                if (user.Metempsychosis < openRebirth || user.Metempsychosis > closeRebirth)
                {
                    continue;
                }

                if (user.Level < openLevel || user.Level > closeLevel)
                {
                    continue;
                }

                taskTypes.Add(task);
            }
            return taskTypes.DistinctBy(x => x.Type).ToList();
        }

        public static DbActivityTaskType GetTaskById(uint id)
        {
            return taskTypes.TryGetValue(id, out var result) ? result : null;
        }

        public static DbActivityTaskType GetTaskByType(Character user, ActivityType activityType)
        {
            return GetDisponibleTaskByUser(user).FirstOrDefault(x => x.Type == (int)activityType);
        }

        public static ActivityType GetTaskTypeById(uint idTask)
        {
            return ((ActivityType?)taskTypes.Values.FirstOrDefault(x => x.Id == idTask)?.Type) ?? ActivityType.None;
        }

        public static async Task UpdateTaskActivityAsync(Character user, ActivityType activityType)
        {
            ActivityTask activityTask = null;
            foreach (var activity in GetUserTaskList(user))
            {
                var act = GetTaskTypeById(activity.ActivityId);
                if (act == activityType)
                {
                    activityTask = activity;
                    break;
                }
            }

            if (activityTask == null)
            {
                return;
            }

            if (activityTask.CompleteFlag != 0)
            {
                return;
            }

            byte newAmount = (byte)(activityTask.Schedule + 1);
            var task = GetTaskById(activityTask.ActivityId);
            if (newAmount > task.MaxNum)
            {
                return;
            }

            activityTask.Schedule = newAmount;
            if (activityTask.Schedule >= task.MaxNum)
            {
                activityTask.CompleteFlag = 1;
            }

            await activityTask.SaveAsync();

            await user.SendAsync(new MsgActivityTask
            {
                Mode = MsgActivityTask.Action.UpdateActivityTask,
                Activities = new List<MsgActivityTask.Activity>
                {
                    new MsgActivityTask.Activity
                    {
                        Id = activityTask.ActivityId,
                        Completed = activityTask.CompleteFlag,
                        Progress = activityTask.Schedule
                    }
                }
            });

            await user.SubmitDailyActivityScoreAsync();
        }

        public static async Task<bool> ClaimRewardAsync(Character user, uint rewardId)
        {
            if (rewardId == 0) { return false; }

            DbActivityRewardType rewardType = null;
            foreach (var task in rewardTypes.Values
                .Where(x => x.RewardGrade == rewardId)
                .OrderByDescending(x => x.Metempsychosis))
            {
                if (task.Metempsychosis <= user.Metempsychosis)
                {
                    rewardType = task;
                    break;
                }
            }

            if (rewardType == null)
            {
                return false;
            }

            if (user.Statistic.HasDailyEvent(StcEventId, rewardId))
            {
                var evt = user.Statistic.GetDailyStc(StcEventId, rewardId);
                if (evt != null)
                {
                    return false;
                }
            }

            if (user.ActivityPoints < rewardType.ActivityReq)
            {
                return false;
            }

            int rewardCount = (int)Math.Min(3, rewardType.Reward1Num + rewardType.Reward2Num + rewardType.Reward3Num);
            if (!user.UserPackage.IsPackSpare(rewardCount))
            {
                await user.SendAsync(string.Format(StrNotEnoughSpaceN, rewardCount));
                return false;
            }

            if (rewardType.Reward1Num > 0 && rewardType.Reward1 != 0)
            {
                for (int i = 0; i < rewardType.Reward1Num; i++)
                {
                    await user.UserPackage.AwardItemAsync(rewardType.Reward1, Item.ItemPosition.Inventory, rewardType.Reward1Mono != 0, true);
                }
            }

            if (rewardType.Reward2Num > 0 && rewardType.Reward2 != 0)
            {
                for (int i = 0; i < rewardType.Reward2Num; i++)
                {
                    await user.UserPackage.AwardItemAsync(rewardType.Reward2, Item.ItemPosition.Inventory, rewardType.Reward2Mono != 0, true);
                }
            }

            if (rewardType.Reward3Num > 0 && rewardType.Reward3 != 0)
            {
                for (int i = 0; i < rewardType.Reward3Num; i++)
                {
                    await user.UserPackage.AwardItemAsync(rewardType.Reward3, Item.ItemPosition.Inventory, rewardType.Reward3Mono != 0, true);
                }
            }

            await user.Statistic.AddOrUpdateDailyAsync(StcEventId, rewardId, 1);
            return true;
        }

        public static async Task OnDailyResetAsync()
        {
            await using var ctx = new ServerDbContext();
            foreach (var usr in userTasks)
            {
                foreach (var task in usr.Value)
                {
                    ctx.ActivityUserTasks.Remove(task);
                }
            }
            await ctx.SaveChangesAsync();
            userTasks.Clear();
        }

        public enum ActivityType
        {
            None,
            /// <summary>
            /// Login to get Active Points.
            /// </summary>
            LoginTheGame,
            /// <summary>
            /// Keep online for 0.5 hour.
            /// </summary>
            HalfHourOnline,
            /// <summary>
            /// Become a VIP player to get Active Points.
            /// </summary>
            VipActiveness,
            /// <summary>
            /// Take Daily Quests at Daily Quest Envoy in Market.
            /// </summary>
            DailyQuest,
            /// <summary>
            /// Compete in the Qualifier.
            /// </summary>
            Qualifier,
            /// <summary>
            /// Complete in the Team Qualifier.
            /// </summary>
            TeamQualifier,
            /// <summary>
            /// Join the Champion`s Arena.
            /// </summary>
            ChampionsArena = 7,
            /// <summary>
            /// Study Chi once on the Chi window.
            /// </summary>
            ChiStudy = 8,
            /// <summary>
            /// Training once on the Chi window.
            /// </summary>
            JiangHu = 9,
            /// <summary>
            /// Sign up for Treasure in the Blue with Squidward Octopus (TwinCity 290,208).
            /// </summary>
            TreasureInBlue = 10,
            /// <summary>
            /// Send flowers/gift to get Active Points.
            /// </summary>
            FlowerGifts = 11,
            /// <summary>
            /// Enlighten low-level players.
            /// </summary>
            Enlightenment,
            /// <summary>
            /// Talk to Lady Luck to play Lottery.
            /// </summary>
            Lottery = 14,
            /// <summary>
            /// Join the Horse Racing everyday.
            /// </summary>
            HorseRacing = 15,
        }
    }
}
