using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FatePlayerRepository
    {
        public static async Task<IList<DbFatePlayer>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FatePlayers.ToListAsync();
        }

        public static async Task<DbFatePlayer> GetAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FatePlayers.FirstOrDefaultAsync(x => x.PlayerId == idUser);
        }
    }
}
