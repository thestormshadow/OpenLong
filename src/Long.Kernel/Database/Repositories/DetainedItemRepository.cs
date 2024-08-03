using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class DetainedItemRepository
    {
        public static async Task<List<DbDetainedItem>> GetFromHunterAsync(uint hunter)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DetainedItems.Where(x => x.HunterIdentity == hunter).ToListAsync();
        }

        public static async Task<List<DbDetainedItem>> GetFromDischargerAsync(uint target)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DetainedItems.Where(x => x.TargetIdentity == target).ToListAsync();
        }

        public static async Task<DbDetainedItem> GetByIdAsync(uint id)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DetainedItems.FirstOrDefaultAsync(x => x.Identity == id);
        }

        public static async Task<List<DbDetainedItem>> GetByItemAsync(uint idItem)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.DetainedItems.Where(x => x.ItemIdentity == idItem).ToListAsync();
        }
    }
}
