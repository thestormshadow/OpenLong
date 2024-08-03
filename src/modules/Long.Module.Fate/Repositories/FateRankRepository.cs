using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FateRankRepository
    {
        public static async Task<IList<DbFateRank>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FateRanks.ToListAsync();
        }
    }
}
