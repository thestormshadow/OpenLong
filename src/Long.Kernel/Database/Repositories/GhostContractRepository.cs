using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public class GhostContractRepository
    {
        public static async Task CreateAsync(DbGhostContract ghostContract)
        {
            await using var ctx = new ServerDbContext();
            ctx.GhostContracts.Add(ghostContract);
            await ctx.SaveChangesAsync();
        }

        public static async Task<DbGhostContract> GetAsync(uint itemId)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.GhostContracts.FirstOrDefaultAsync(x => x.ItemId == itemId);
        }

        public static async Task DeleteAsync(DbGhostContract ghostContract)
        {
            await using var ctx = new ServerDbContext();
            ctx.GhostContracts.Remove(ghostContract);
            await ctx.SaveChangesAsync();
        }
    }
}
