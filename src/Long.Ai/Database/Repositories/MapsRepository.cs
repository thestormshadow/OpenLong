using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Ai.Database.Repositories
{
    public static class MapsRepository
    {
        public static async Task<DbMap> GetAsync(uint idMap)
        {
            await using var db = new ServerDbContext();
            return await db.Maps.FirstOrDefaultAsync(x => x.Identity == idMap);
        }

        public static async Task<List<DbMap>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return await db.Maps.Where(x => x.ServerIndex == -1)
                     .ToListAsync();
        }

        public static async Task<List<DbDynamap>> GetDynaAsync()
        {
            await using var db = new ServerDbContext();
            return await db.DynaMaps.Where(x => x.Identity > 1000000 &&
                                          x.ServerIndex <= 0)
                     .ToListAsync();
        }
    }
}
