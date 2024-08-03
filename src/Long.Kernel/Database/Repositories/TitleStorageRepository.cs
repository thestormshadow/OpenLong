using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public class TitleStorageRepository
    {
        public static async Task<List<DbTitleRule>> GetTitleRulesAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TitleRules.ToListAsync();
        }

        public static async Task<List<DbTitleType>> GetTitleTypesAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TitleTypes.ToListAsync();
        }
    }
}
