using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class ActionRepository
    {
        public static async Task<List<DbAction>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return db.Actions.FromSqlRaw("SELECT * FROM cq_action UNION ALL SELECT * FROM cq_newaction ORDER BY id ASC").ToList();
        }

        public static async Task<DbAction> GetAsync(uint idAction)
        {
            await using var db = new ServerDbContext();
            return db.Actions.FromSqlRaw($"SELECT * FROM cq_action UNION ALL SELECT * FROM cq_newaction WHERE id = {idAction} ORDER BY id ASC").FirstOrDefault();
        }
    }
}
