using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class SlotMachineRepository
    {
        public static async Task<List<DbSlotWinningRule>> GetAsync(byte type)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.SlotWinningRules.Where(x => x.Type == type).ToListAsync();
        }
    }
}
