using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FateRuleRepository
    {
        public static async Task<IList<DbFateRule>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FateRules.ToListAsync();
        }
    }
}
