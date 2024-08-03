using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class StageGoalRepository
    {
        public static List<DbProcessGoal> GetGoals()
        {
            using var ctx = new ServerDbContext();
            return ctx.ProcessGoals.ToList();
        }

        public static List<DbProcessTask> GetTasks()
        {
            using var ctx = new ServerDbContext();
            return ctx.ProcessTasks.ToList();
        }

        public static List<DbPlayerProcessGoal> GetPlayerGoals(uint idUser)
        {
            using var ctx = new ServerDbContext();
            return ctx.PlayerProcessGoals.Where(x => x.UserId == idUser).ToList();
        }

        public static List<DbPlayerProcessSchedule> GetPlayerSchedules(uint idUser)
        {
            using var ctx = new ServerDbContext();
            return ctx.PlayerProcessSchedules.Where(x => x.UserId == idUser).ToList();
        }

        public static async Task SaveAsync(DbPlayerProcessGoal processGoal)
        {
            await using var ctx = new ServerDbContext();
            if (processGoal.Id == 0)
            {
                ctx.PlayerProcessGoals.Add(processGoal);
            }
            else
            {
                ctx.PlayerProcessGoals.Update(processGoal);
            }
            await ctx.SaveChangesAsync();
        }

        public static async Task SaveAsync(DbPlayerProcessSchedule processSchedule)
        {
            await using var ctx = new ServerDbContext();
            if (processSchedule.Id == 0)
            {
                ctx.PlayerProcessSchedules.Add(processSchedule);
            }
            else
            {
                ctx.PlayerProcessSchedules.Update(processSchedule);
            }
            await ctx.SaveChangesAsync();
        }
    }
}
