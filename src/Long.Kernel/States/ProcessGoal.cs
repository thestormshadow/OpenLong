using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using static Long.Kernel.Managers.ProcessGoalManager;
using static Long.Kernel.States.Items.Item;

namespace Long.Kernel.States
{
    public sealed class ProcessGoal
    {
        private static readonly ILogger logger = Log.ForContext<ProcessGoal>();

        private readonly ConcurrentDictionary<uint, DbPlayerProcessGoal> goals = new();
        private readonly ConcurrentDictionary<uint, DbPlayerProcessSchedule> schedules = new();

        private readonly Character user;

        public ProcessGoal(Character user)
        {
            this.user = user;
        }

        public async Task InitializeAsync()
        {
            var gs = StageGoalRepository.GetPlayerGoals(user.Identity);
            foreach (var goal in gs)
            {
                goals.TryAdd(goal.GoalId, goal);
            }

            var gSchedule = StageGoalRepository.GetPlayerSchedules(user.Identity);
            foreach (var schedule in gSchedule)
            {
                schedules.TryAdd(schedule.TaskId, schedule);
            }

            MsgProcessGoalInfo msg = new MsgProcessGoalInfo();
            foreach (var goal in GetGoals())
            {
                var stageComplete = IsStageCompleted((ushort)goal.Id);
                msg.Goals.Add(new MsgProcessGoalInfo.GoalInfo
                {
                    Id = (int)goal.Id,
                    ClaimEnable = stageComplete && !IsGoalRewardClaimed(goal.Id),
                    Unknown5 = (byte)(stageComplete ? 1 : 0)
                });
            }
            await user.SendAsync(msg);
        }

        private bool IsRewardReady(uint stage, int rewardIdx)
        {
            if (goals.TryGetValue(stage, out var goal))
            {
                uint flag = 1u << rewardIdx;
                return (goal.TaskCompleteState & flag) == flag;
            }
            return true;
        }

        private bool IsRewardReady(DbProcessTask task)
        {
            ulong value = GetTaskProgress(task);
            uint stage = task.Id / 100;
            int rewardIdx = (int)((task.Id - 1) % 100);
            return value >= task.Schedule || IsRewardReady(stage, rewardIdx);
        }

        public Task SetRewardReadyAsync(uint goalId, int rewardIndex)
        {
            var goal = goals.GetOrAdd(goalId, new DbPlayerProcessGoal
            {
                GoalId = goalId,
                UserId = user.Identity
            });

            uint flag = 1u << rewardIndex;
            goal.TaskCompleteState |= flag;

            if (goal.Id == 0)
            {
                return ServerDbContext.CreateAsync(goal);
            }
            return ServerDbContext.UpdateAsync(goal);
        }

        private bool IsRewardClaimed(uint stage, int rewardIdx)
        {
            if (goals.TryGetValue(stage, out var goal))
            {
                uint flag = 1u << rewardIdx;
                return (goal.TaskAwardState & flag) == flag;
            }
            return false;
        }

        public Task SetRewardClaimedAsync(uint goalId, int rewardIndex)
        {
            var goal = goals.GetOrAdd(goalId, new DbPlayerProcessGoal
            {
                GoalId = goalId,
                UserId = user.Identity
            });
            uint flag = 1u << rewardIndex;
            goal.TaskAwardState |= flag;
            if (goal.Id == 0)
            {
                return ServerDbContext.CreateAsync(goal);
            }
            return ServerDbContext.UpdateAsync(goal);
        }

        private bool IsGoalRewardClaimed(uint stage)
        {
            if (goals.TryGetValue(stage, out var goal))
            {
                return goal.ProcessAward;
            }
            return false;
        }

        private bool IsStageCompleted(ushort goalId)
        {
            var goal = GetGoal(goalId);
            if (goal == null)
            {
                return false;
            }
            for (int i = 0; i < goal.TaskNum; i++)
            {
                if (!IsRewardClaimed(goalId, i))
                {
                    return false;
                }
            }
            return true;
        }

        public Task SetProgressAsync(GoalType goalType, uint value)
        {
            var task = GetTasks().FirstOrDefault(x => x.Type == (byte)goalType);
            if (task != null)
            {
                return SetProgressAsync(task, value);
            }
            return Task.CompletedTask;
        }

        public async Task SetProgressAsync(DbProcessTask task, uint value)
        {
            GoalType goalType = (GoalType)task.Type;
            if (task.Sort == 0)
            {
                if (!schedules.TryGetValue((uint)goalType, out var result))
                {
                    result = new DbPlayerProcessSchedule
                    {
                        UserId = user.Identity,
                        TaskId = (uint)goalType
                    };
                    schedules.TryAdd((uint)goalType, result);
                }

                if (value > result.Schedule)
                {
                    result.Schedule = Math.Max(value, result.Schedule);

                    if (result.Id == 0)
                    {
                        await ServerDbContext.CreateAsync(result);
                    }
                    else
                    {
                        await ServerDbContext.UpdateAsync(result);
                    }
                }
            }

            await CheckProgressAsync(goalType);
        }

        private ulong GetTaskProgress(DbProcessTask task)
        {
            GoalType goalType = (GoalType)task.Type;
            if (task.Sort == 0)
            {
                if (schedules.TryGetValue((uint)goalType, out var result))
                {
                    return result.Schedule;
                }
                return 0;
            }

            ulong schedule = task.Schedule;
            switch (goalType)
            {
                case GoalType.LevelUp: return user.Level;
                case GoalType.Metempsychosis: return user.Metempsychosis;
                case GoalType.EquipmentQuality:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                switch (pos)
                                {
                                    case ItemPosition.Mount:
                                    case ItemPosition.Gourd:
                                    case ItemPosition.Garment:
                                    case ItemPosition.RightHandAccessory:
                                    case ItemPosition.LeftHandAccessory:
                                    case ItemPosition.MountArmor:
                                    case (ItemPosition)13:
                                    case (ItemPosition)14:
                                        continue;
                                    default:
                                        break;
                                }
                            }

                            if (item == null)
                            {
                                break;
                            }

                            if (!item.IsEquipment())
                            {
                                continue;
                            }

                            if (item.GetQuality() % 10 < (int)schedule)
                            {
                                continue;
                            }
                            count++;
                        }
                        return count;
                    }
                case GoalType.ProfessionPromotion: return user.ProfessionLevel;
                case GoalType.CreateJoinSyndicate: return user.SyndicateIdentity;
                case GoalType.AddFriends: return (uint)(user.Relation?.FriendAmount ?? 0);                
                case GoalType.SuperTalismans:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            if (pos != ItemPosition.AttackTalisman && pos != ItemPosition.DefenceTalisman && pos != ItemPosition.Crop)
                            {
                                continue;
                            }
                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                continue;
                            }
                            if (item.GetQuality() == 9)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                case GoalType.TotalComposingLevel:
                    {
                        uint composingLevel = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item != null)
                            {
                                composingLevel += item.Plus;
                            }
                        }
                        return composingLevel;
                    }
                case GoalType.EquipmentPlus3:
                    {
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item != null && item.Plus >= (byte)schedule)
                            {
                                return schedule;
                            }
                        }
                        return 0;
                    }
                case GoalType.JoinSubClass:
                    {
                        return (ulong)(user.AstProf?.Count ?? 0);
                    }                
                case GoalType.TotalEmbedGems:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                continue;
                            }

                            if (item.SocketOne != SocketGem.NoSocket && item.SocketOne != SocketGem.EmptySocket)
                            {
                                count++;
                            }
                            if (item.SocketTwo != SocketGem.NoSocket && item.SocketTwo != SocketGem.EmptySocket)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                case GoalType.TotalEmbedSuperGems:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                continue;
                            }

                            if (((byte)item.SocketOne) % 10 == 3)
                            {
                                count++;
                            }
                            if (((byte)item.SocketTwo) % 10 == 3)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                case GoalType.DragonSoulLevel:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                continue;
                            }

                            if (task.Condition == 2)
                            {
                                if (item.ItemStatus?.CurrentArtifact?.ItemStatus?.Level >= schedule
                                    && item.ItemStatus.CurrentArtifact.IsPermanent)
                                {
                                    count++;
                                }
                            }
                            else if (task.Condition == 3)
                            {
                                if (item.ItemStatus?.CurrentArtifact?.ItemStatus?.Level >= schedule)
                                {
                                    count++;
                                }
                            }
                        }
                        return count;
                    }
                case GoalType.ChiStudyTotalPoints:
                    {
                        return user.ChiPoints; // ?
                    }
                case GoalType.JiangHuScore:
                    {
                        return user.JiangHu?.InnerPower ?? 0;
                    }
                case GoalType.HouseLevel:
                    {
                        return user.HomeIdentity;
                    }
                case GoalType.Marriage:
                    {
                        return user.MateIdentity;
                    }
                case GoalType.NobilityDonation:
                    {
                        return user.Nobility?.Donation ?? 0;
                    }
                
                case GoalType.Tutor:
                    {
                        return (uint)(user.Guide?.Tutor != null ? 1 : 0);
                    }
                
                case GoalType.BattlePower:
                    {
                        return (uint)user.BattlePower;
                    }
                case GoalType.EquipSteed:
                    {
                        return user.Mount != null ? 1u : 0u;
                    }
                case GoalType.RefineryLevel:
                    {
                        uint count = 0;
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            switch (pos)
                            {
                                case ItemPosition.Gourd:
                                case ItemPosition.Garment:
                                case ItemPosition.RightHandAccessory:
                                case ItemPosition.LeftHandAccessory:
                                case ItemPosition.MountArmor:
                                    continue;
                            }

                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                continue;
                            }

                            if (task.Condition == 2)
                            {
                                if (item.ItemStatus?.CurrentRefinery?.ItemStatus?.Level >= schedule
                                    && item.ItemStatus.CurrentRefinery.IsPermanent)
                                {
                                    count++;
                                }
                            }
                            else if (task.Condition == 3)
                            {
                                if (item.ItemStatus?.CurrentRefinery?.ItemStatus?.Level >= schedule)
                                {
                                    count++;
                                }
                            }
                        }
                        return count;
                    }
                default:
                    {
                        logger.Warning("Unhandled ProcessGoal Type {0} for user {1} {2}", goalType, user.Identity, user.Name);
                        break;
                    }
            }

            return 0;
        }

        private async Task CheckProgressAsync(GoalType goalType)
        {
            var tasks = GetTasks();
            foreach (var task in tasks.Where(x => x.Type == (byte)goalType))
            {
                var value = GetTaskProgress(task);
                if (value >= task.Schedule)
                {
                    await SetRewardReadyAsync(task.Id / 100, (int)(task.Id % 100 - 1));
                }
            }
        }

        public async Task<bool> ClaimTaskRewardAsync(ushort taskId)
        {
            var task = GetTask(taskId);
            if (task == null)
            {
                return false;
            }

            uint goalId = task.Id / 100;
            int taskIndex = (int)((task.Id % 100) - 1);
            bool completed = IsRewardReady(task);
            if (!completed)
            {
                return false;
            }

            bool claimed = IsRewardClaimed(goalId, taskIndex);
            if (claimed)
            {
                return false;
            }

            if (!user.UserPackage.IsPackSpare(3))
            {
                await user.SendAsync(string.Format(StrNotEnoughSpaceN, 3));
                return false;
            }

            for (int i = 0; i < task.Number; i++)
            {
                await user.UserPackage.AwardItemAsync(task.ItemType, ItemPosition.Inventory, task.Monopoly != 0);
            }

            await SetRewardClaimedAsync(goalId, taskIndex);
            return true;
        }

        public async Task<bool> ClaimGoalRewardAsync(ushort goalId)
        {
            DbProcessGoal goal = GetGoal(goalId);
            if (goal == null)
            {
                return false;
            }

            if (!IsStageCompleted(goalId))
            {
                return false;
            }

            if (IsGoalRewardClaimed(goalId))
            {
                return false;
            }

            int rewardCount = Math.Min(3, goal.Number1 + goal.Number2 + goal.Number3);
            if (!user.UserPackage.IsPackSpare(rewardCount))
            {
                await user.SendAsync(string.Format(StrNotEnoughSpaceN, rewardCount));
                return false;
            }

            if (goal.Number1 > 0 && goal.ItemType1 != 0)
            {
                for (int i = 0; i < goal.Number1; i++)
                {
                    await user.UserPackage.AwardItemAsync(goal.ItemType1, ItemPosition.Inventory, goal.Monopoly1 != 0, true);
                }
            }

            if (goal.Number2 > 0 && goal.ItemType2 != 0)
            {
                for (int i = 0; i < goal.Number2; i++)
                {
                    await user.UserPackage.AwardItemAsync(goal.ItemType2, ItemPosition.Inventory, goal.Monopoly2 != 0, true);
                }
            }

            if (goal.Number3 > 0 && goal.ItemType3 != 0)
            {
                for (int i = 0; i < goal.Number3; i++)
                {
                    await user.UserPackage.AwardItemAsync(goal.ItemType3, ItemPosition.Inventory, goal.Monopoly3 != 0, true);
                }
            }
            return true;
        }

        public Task SendAsync(ushort id)
        {
            MsgProcessGoalTask msg = new MsgProcessGoalTask
            {
                Param = id,
                Completed = IsStageCompleted(id)
            };
            var goal = GetTasks(id);
            foreach (var task in goal)
            {
                int taskIndex = (int)((task.Id - 1) % 100);
                bool completion = IsRewardReady(task);
                bool claimed = IsRewardClaimed(id, taskIndex);
                if (completion || claimed)
                {
                    msg.Goals.Add(new MsgProcessGoalTask.GoalTaskStruct
                    {
                        Id = (int)task.Id,
                        Unknown = (int)GetTaskProgress(task),
                        Claimed = claimed
                    });
                }
            }
            return user.SendAsync(msg);
        }
    }
}
