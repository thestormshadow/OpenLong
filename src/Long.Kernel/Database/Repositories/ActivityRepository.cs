using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class ActivityRepository
    {
        public static IList<DbActivityRewardType> GetRewards()
        {
            using var ctx = new ServerDbContext();
            return ctx.ActivityRewardTypes.ToList();
        }

        public static IList<DbActivityTaskType> GetTasks()
        {
            using var ctx = new ServerDbContext();
            return ctx.ActivityTaskTypes.ToList();
        }

        public static IList<DbActivityUserTask> GetUserTasks()
        {
            using var ctx = new ServerDbContext();
            return ctx.ActivityUserTasks.ToList();
        }

        public static IList<DbActivityUserTask> GetUserTasks(uint idUser)
        {
            using var ctx = new ServerDbContext();
            return ctx.ActivityUserTasks.Where(x => x.UserId == idUser).ToList();
        }
    }
}
