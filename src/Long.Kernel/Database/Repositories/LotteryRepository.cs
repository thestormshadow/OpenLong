using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class LotteryRepository
    {
        public static async Task<List<DbLottery>> GetAsync()
        {
            await using ServerDbContext ctx = new();
            return await ctx.Lottery.ToListAsync();
        }
    }
}
