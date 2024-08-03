using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Family.Repositories
{
    public static class FamilyBattleEffectShareLimitRepository
    {
        public static async Task<List<DbFamilyBattleEffectShareLimit>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FamilyBattleEffectShareLimits.ToListAsync();
        }
    }
}
