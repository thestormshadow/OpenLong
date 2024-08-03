using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Module.TaskDetail.Repositories
{
    public static class TaskDetailRepository
    {
        public static async Task<List<DbTaskDetail>> GetAsync(uint idUser)
        {
            await using var db = new ServerDbContext();
            return db.TaskDetails.Where(x => x.UserIdentity == idUser).ToList();
        }
    }
}
