using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class DynamicGlobalDataRepository
    {
        public static async Task<List<DbDynaGlobalData>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DynaGlobalDatas.ToListAsync();
        }

        public static async Task<List<DbDynaGlobalData>> GetAsync(params uint[] ids)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DynaGlobalDatas
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}
