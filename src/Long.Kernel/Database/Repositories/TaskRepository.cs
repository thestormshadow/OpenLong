using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class TaskRepository
    {
        public static async Task<List<DbTask>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return db.Tasks.ToList();
        }
    }
}
