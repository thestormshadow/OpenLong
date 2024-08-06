using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class RebirthRepository
    {
        public static async Task<List<DbRebirth>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return await db.Rebirths.ToListAsync();
        }
    }
}
