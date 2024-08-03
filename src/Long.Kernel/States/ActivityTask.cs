using Long.Database.Entities;
using Long.Kernel.Database;
using static Long.Kernel.Managers.ActivityManager;

namespace Long.Kernel.States
{
    public sealed class ActivityTask
    {
        private readonly DbActivityUserTask userTask;

        public ActivityTask(DbActivityUserTask userTask)
        {
            this.userTask = userTask;
            Type = GetTaskTypeById(userTask.ActivityId);
        }

        public ActivityType Type { get; init; }

        public uint UserId => userTask.UserId;

        public uint ActivityId => userTask.ActivityId;

        public byte CompleteFlag
        {
            get => userTask.CompleteFlag;
            set => userTask.CompleteFlag = value;
        }

        public byte Schedule
        {
            get => userTask.Schedule;
            set => userTask.Schedule = value;
        }

        public Task SaveAsync()
        {
            return ServerDbContext.UpdateAsync(userTask);
        }

        public static implicit operator DbActivityUserTask(ActivityTask task)
        {
            return task.userTask;
        }
    }
}
