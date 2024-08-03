using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.AstProf.Repositories
{
    public static class AstProfInaugurationConditionRepository
    {
        public static async Task<List<DbAstProfInaugurationCondition>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.AstProfInaugurationConditions.ToListAsync();
        }
    }
}
