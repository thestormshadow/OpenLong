using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class AwardConfigRepository
    {
        public static async Task<List<DbAwardConfig>> GetFirstCreditRewardsAsync(int professionSort)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.AwardConfigs.Where(x => x.Data1 == professionSort).ToListAsync();
        }
    }
}
