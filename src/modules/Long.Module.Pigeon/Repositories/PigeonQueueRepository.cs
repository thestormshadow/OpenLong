using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Pigeon.Repositories
{
    public static class PigeonQueueRepository
    {
        public static async Task<List<DbPigeonQueue>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.PigeonQueues.ToListAsync();
        }
    }
}
