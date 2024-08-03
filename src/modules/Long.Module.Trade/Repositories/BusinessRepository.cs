using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Trade.Repositories
{
    public class BusinessRepository
    {
        public static async Task<IList<DbBusiness>> GetByUserIdAsync(uint userId)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Businesses.Where(x => x.UserId == userId || x.BusinessId == userId).ToListAsync();
        }

        public static async Task CreateAsync(DbBusiness business)
        {
            await using var ctx = new ServerDbContext();
            ctx.Businesses.Add(business);
            await ctx.SaveChangesAsync();
        }

        public static async Task DeleteAsync(DbBusiness business)
        {
            if (business.Identity == 0)
            {
                return;
            }
            await using var ctx = new ServerDbContext();
            ctx.Businesses.Remove(business);
            await ctx.SaveChangesAsync();
        }
    }
}
