using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.States.User;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        [LuaFunction]
        public bool SetUserStatisticDaily(int userId, uint eventId, uint eventType, uint data, int save)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.Statistic.AddOrUpdateDailyAsync(eventType, eventId, data).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool DeleteUserStatisticDaily(int userId, uint eventId, uint eventType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.Statistic.DeleteDailyAsync(eventId, eventType).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool IsexitUserStatisticDaily(uint eventId, uint eventType, int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.Statistic.GetDailyStc(eventId, eventType) != null;
        }

        [LuaFunction]
        public long GetUserStatisticDaily(int userId, uint eventId, uint eventType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            var stc = user.Statistic.GetDailyStc(eventId, eventType);
            return stc?.Data ?? 0;
        }

        [LuaFunction]
        public bool IsExistTaskDetail(int userId, uint taskId)
        {
            Character user = GetUser(userId);
            if (user?.TaskDetail == null)
            {
                return false;
            }
            return user.TaskDetail.QueryTaskData(taskId) != null;
        }

        [LuaFunction]
        public bool DeleteTaskDetail(int userId, uint taskId)
        {
            Character user = GetUser(userId);
            if (user?.TaskDetail == null)
            {
                return false;
            }
            return user.TaskDetail.DeleteTaskAsync(taskId).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool AddTaskDetail(int userId, uint taskId, int nLimitTime)
        {
            Character user = GetUser(userId);
            if (user?.TaskDetail == null)
            {
                return false;
            }

            return user.TaskDetail.CreateNewAsync(taskId).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool SetTaskDetailData(int userId, uint taskId, int index, int data)
        {
            Character user = GetUser(userId);
            if (user?.TaskDetail == null)
            {
                return false;
            }

            DbTaskDetail taskDetail = user.TaskDetail.QueryTaskData(taskId);
            if (taskDetail == null)
            {
                return false;
            }

            switch (index)
            {
                case G_TASKDETAIL_DATA1: return user.TaskDetail.SetDataAsync(taskId, "data1", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA2: return user.TaskDetail.SetDataAsync(taskId, "data2", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA3: return user.TaskDetail.SetDataAsync(taskId, "data3", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA4: return user.TaskDetail.SetDataAsync(taskId, "data4", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA5: return user.TaskDetail.SetDataAsync(taskId, "data5", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA6: return user.TaskDetail.SetDataAsync(taskId, "data6", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_DATA7: return user.TaskDetail.SetDataAsync(taskId, "data7", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_TASK_OVERTIME: return user.TaskDetail.SetDataAsync(taskId, "task_overtime", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_NOTIFY_FLAG: return user.TaskDetail.SetDataAsync(taskId, "notify_flag", data).GetAwaiter().GetResult();
                case G_TASKDETAIL_COMPLETE_FLAG: return user.TaskDetail.SetCompleteAsync(taskId, data).GetAwaiter().GetResult();
                case G_MAX_ACCUMULATE_TIMES: return user.TaskDetail.SetDataAsync(taskId, "max_accumulate_times", data).GetAwaiter().GetResult();
                default:
                    {
                        logger.Warning("SetTaskDetailData[index:{0}] unhandled", index);
                        return false;
                    }
            }
        }

        [LuaFunction]
        public long GetTaskDetailData(int userId, uint taskId, int index)
        {
            Character user = GetUser(userId);
            if (user?.TaskDetail == null)
            {
                return 0;
            }

            DbTaskDetail taskDetail = user.TaskDetail.QueryTaskData(taskId);
            if (taskDetail == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_TASKDETAIL_DATA1: return taskDetail.Data1;
                case G_TASKDETAIL_DATA2: return taskDetail.Data2;
                case G_TASKDETAIL_DATA3: return taskDetail.Data3;
                case G_TASKDETAIL_DATA4: return taskDetail.Data4;
                case G_TASKDETAIL_DATA5: return taskDetail.Data5;
                case G_TASKDETAIL_DATA6: return taskDetail.Data6;
                case G_TASKDETAIL_DATA7: return taskDetail.Data7;
                case G_TASKDETAIL_TASK_OVERTIME: return taskDetail.TaskOvertime;
                case G_TASKDETAIL_NOTIFY_FLAG: return taskDetail.NotifyFlag;
                case G_TASKDETAIL_COMPLETE_FLAG: return taskDetail.CompleteFlag;
                case G_MAX_ACCUMULATE_TIMES: return taskDetail.MaxAccumulateTimes;
                default: return 0;
            }
        }

        [LuaFunction]
        public long GetUserStatistic(int userId, uint eventId, uint eventType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            var stc = user.Statistic.GetStc(eventId, eventType);
            return stc?.Data ?? 0;
        }

        [LuaFunction]
        public bool SetUserStatistic(int userId, uint eventType, uint eventId, long data, int save)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.Statistic.AddOrUpdateAsync(eventType, eventId, (uint)data, save != 0).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public int GetUserStcTimestamp(int userId, uint eventType, uint eventId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }

            var stc = user.Statistic.GetStc(eventType, eventId);
            if (stc?.Timestamp != null)
            {
                return (int)new DateTimeOffset(DateTimeOffset.FromUnixTimeSeconds(stc.Timestamp).DateTime).ToUnixTimeSeconds();
            }
            return 0;
        }

        [LuaFunction]
        public bool SetUserStcTimestamp(int userId, uint eventId, uint eventType, int data)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            var stc = user.Statistic.GetStc(eventId, eventType);
            if (stc != null)
            {
                DateTime dateTime = DateTime.Now;
                if (data != 0)
                {
                    dateTime = dateTime.AddSeconds(data);
                }
                return user.Statistic.SetTimestampAsync(eventId, eventType, dateTime).GetAwaiter().GetResult();
            }
            return false;
        }
    }
}
