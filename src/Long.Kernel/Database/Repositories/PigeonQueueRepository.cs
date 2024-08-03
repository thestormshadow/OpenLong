using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
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
