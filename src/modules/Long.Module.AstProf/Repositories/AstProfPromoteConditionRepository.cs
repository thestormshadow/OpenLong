using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.AstProf.Repositories
{
    public static class AstProfPromoteConditionRepository
    {
        public static async Task<List<DbAstProfPromoteCondition>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.AstProfPromoteConditions.ToListAsync();
        }
    }
}
