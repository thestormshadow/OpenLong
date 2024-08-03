using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Ai.Database.Repositories
{
    public static class RegionRepository
    {
        public static async Task<List<DbRegion>> GetAsync(uint idMap)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Regions.Where(x => x.MapIdentity == idMap).ToListAsync();
        }
    }
}
