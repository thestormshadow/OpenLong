using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.TaskDetail;
using Long.Kernel.States.User;
using Long.Module.TaskDetail.Network;
using Long.Module.TaskDetail.Repositories;
using Long.Shared;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Long.Module.TaskDetail.States
{
    public sealed class TaskDetail : ITaskDetail
    {
        private readonly ConcurrentDictionary<uint, DbTaskDetail> taskDetails = new();
        private readonly Character user;

        public TaskDetail(Character user)
        {
            this.user = user;
        }

        public async Task InitializeAsync()
        {
            if (user == null)
            {
                return;
            }

            foreach (var dbDetail in await TaskDetailRepository.GetAsync(user.Identity))
            {
                if (!taskDetails.ContainsKey(dbDetail.TaskIdentity))
                {
                    taskDetails.TryAdd(dbDetail.TaskIdentity, dbDetail);
                }
            }

            return;
        }

        public async Task<bool> CreateNewAsync(uint idTask)
        {
            if (QueryTaskData(idTask) != null)
            {
                return false;
            }

            DbTaskDetail detail = new()
            {
                UserIdentity = user.Identity,
                TaskIdentity = idTask
            };

            if (await SaveAsync(detail))
            {
                await user.SendAsync(new MsgTaskStatus
                {
                    Mode = MsgTaskStatus.TaskStatusMode.Add,
                    Tasks = new List<MsgTaskStatus.TaskItemStruct>
                    {
                        new MsgTaskStatus.TaskItemStruct
                        {
                            Identity = (int)detail.TaskIdentity,
                            Status = MsgTaskStatus.TaskItemStatus.Available,
                            Time = 0
                        }
                    }
                });
                return taskDetails.TryAdd(detail.TaskIdentity, detail);
            }
            return false;
        }

        public DbTaskDetail QueryTaskData(uint idTask)
        {
            return taskDetails.TryGetValue(idTask, out var result) ? result : null;
        }

        public async Task<bool> SetCompleteAsync(uint idTask, int value, bool save = true)
        {
            if (!taskDetails.TryGetValue(idTask, out var detail))
            {
                await CreateNewAsync(idTask);
                if (!taskDetails.TryGetValue(idTask, out detail))
                {
                    return false;
                }
            }

            detail.CompleteFlag = (ushort)value;
            detail.TaskOvertime = (uint)UnixTimestamp.Now;

            if (value != 0)
            {
                await user.SendAsync(new MsgTaskStatus
                {
                    Mode = MsgTaskStatus.TaskStatusMode.Finish,
                    Tasks = new List<MsgTaskStatus.TaskItemStruct>
                    {
                        new MsgTaskStatus.TaskItemStruct
                        {
                            Identity = (int)detail.TaskIdentity,
                            Status = MsgTaskStatus.TaskItemStatus.Done,
                            Time = (int)detail.MaxAccumulateTimes
                        }
                    }
                });
            }
            else
            {
                await user.SendAsync(new MsgTaskStatus
                {
                    Mode = MsgTaskStatus.TaskStatusMode.Update,
                    Tasks = new List<MsgTaskStatus.TaskItemStruct>
                    {
                        new MsgTaskStatus.TaskItemStruct
                        {
                            Identity = (int)detail.TaskIdentity,
                            Status = MsgTaskStatus.TaskItemStatus.Accepted,
                            Time = (int)detail.MaxAccumulateTimes
                        }
                    }
                });
            }

            if (save)
            {
                await SaveAsync(detail);
            }
            return true;
        }

        public int GetData(uint idTask, string name)
        {
            if (!taskDetails.TryGetValue(idTask, out var detail))
            {
                return 0;
            }

            switch (name.ToLowerInvariant())
            {
                case "data1": return detail.Data1;
                case "data2": return detail.Data2;
                case "data3": return detail.Data3;
                case "data4": return detail.Data4;
                case "data5": return detail.Data5;
                case "data6": return detail.Data6;
                case "data7": return detail.Data7;
                default:
                    return 0;
            }
        }

        public async Task<bool> AddDataAsync(uint idTask, string name, int data)
        {
            if (!taskDetails.TryGetValue(idTask, out var detail))
            {
                return false;
            }

            switch (name.ToLowerInvariant())
            {
                case "data1": detail.Data1 += data; break;
                case "data2": detail.Data2 += data; break;
                case "data3": detail.Data3 += data; break;
                case "data4": detail.Data4 += data; break;
                case "data5": detail.Data5 += data; break;
                case "data6": detail.Data6 += data; break;
                case "data7": detail.Data7 += data; break;
                default:
                    return false;
            }

            await user.SendAsync(new MsgTaskDetailInfo
            {
                TaskIdentity = idTask,
                Data0 = detail.Data1,
                Data1 = detail.Data2,
                Data2 = detail.Data3,
                Data3 = detail.Data4,
                Data4 = detail.Data5,
                Data5 = detail.Data6,
                Data6 = detail.Data7,
            });
            return await SaveAsync(detail);
        }

        public async Task<bool> SetDataAsync(uint idTask, string name, int data)
        {
            if (!taskDetails.TryGetValue(idTask, out var detail))
            {
                return false;
            }

            switch (name.ToLowerInvariant())
            {
                case "data1": detail.Data1 = data; break;
                case "data2": detail.Data2 = data; break;
                case "data3": detail.Data3 = data; break;
                case "data4": detail.Data4 = data; break;
                case "data5": detail.Data5 = data; break;
                case "data6": detail.Data6 = data; break;
                case "data7": detail.Data7 = data; break;
                case "task_overtime": detail.TaskOvertime = (uint)data; break;
                case "notify_flag": detail.NotifyFlag = (byte)data; break;
                case "max_accumulate_times": detail.MaxAccumulateTimes = (uint)data; break;
                default:
                    return false;
            }

            await user.SendAsync(new MsgTaskDetailInfo
            {
                TaskIdentity = idTask,
                Data0 = detail.Data1,
                Data1 = detail.Data2,
                Data2 = detail.Data3,
                Data3 = detail.Data4,
                Data4 = detail.Data5,
                Data5 = detail.Data6,
                Data6 = detail.Data7,
            });
            return await SaveAsync(detail);
        }

        public async Task DailyResetAsync()
        {
            var tasks = new List<DbTaskDetail>();
            foreach (var dailyTask in ITaskDetail.DailyTasks)
            {
                if (taskDetails.TryRemove(dailyTask, out var task) && task.CompleteFlag > 0)
                {
                    tasks.Add(task);

                    await user.SendAsync(new MsgTaskStatus
                    {
                        Mode = MsgTaskStatus.TaskStatusMode.Update,
                        Tasks = new List<MsgTaskStatus.TaskItemStruct>
                        {
                            new MsgTaskStatus.TaskItemStruct
                            {
                                Identity = (int)task.TaskIdentity,
                                Status = MsgTaskStatus.TaskItemStatus.Available,
                                Time = 0
                            }
                        }
                    });

                    await user.SendAsync(new MsgTaskDetailInfo
                    {
                        TaskIdentity = task.TaskIdentity,
                        Data0 = task.Data1,
                        Data1 = task.Data2,
                        Data2 = task.Data3,
                        Data3 = task.Data4,
                        Data4 = task.Data5,
                        Data5 = task.Data6,
                        Data6 = task.Data7,
                    });
                }
            }
            await ServerDbContext.DeleteRangeAsync(tasks);
        }

        public async Task<bool> DeleteTaskAsync(uint idTask)
        {
            if (!taskDetails.TryRemove(idTask, out var detail))
            {
                return false;
            }

            await user.SendAsync(new MsgTaskStatus
            {
                Mode = MsgTaskStatus.TaskStatusMode.Remove,
                Tasks = new List<MsgTaskStatus.TaskItemStruct>
                    {
                        new MsgTaskStatus.TaskItemStruct
                        {
                            Identity = (int)detail.TaskIdentity,
                            Status = MsgTaskStatus.TaskItemStatus.Available
                        }
                    }
            });
            return await DeleteAsync(detail);
        }

        public Task<bool> SaveAsync(DbTaskDetail detail)
        {
            if (detail.Identity == 0)
            {
                return ServerDbContext.CreateAsync(detail);
            }
            return ServerDbContext.UpdateAsync(detail);
        }

        public Task<bool> DeleteAsync(DbTaskDetail detail)
        {
            return ServerDbContext.DeleteAsync(detail);
        }
    }
}
