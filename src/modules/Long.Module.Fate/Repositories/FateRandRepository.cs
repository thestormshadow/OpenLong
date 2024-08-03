using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FateRandRepository
    {
        public static async Task<IList<DbFateRand>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FateRands.ToListAsync();
        }
    }
}
