using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class SuperFlagRepository
    {
        public static async Task<List<DbSuperFlag>> GetAsync(uint itemId)
        {
            await using ServerDbContext serverDbContext = new();
            return await serverDbContext.SuperFlags.Where(x => x.ItemId == itemId).Take(10).ToListAsync();
        }
    }
}
