using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuPlayerRepository
    {
        public static async Task<DbJiangHuPlayer> GetAsync(uint idUser)
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuPlayers.FirstOrDefaultAsync(x => x.PlayerId == idUser);
        }

        public static async Task<DbJiangHuPlayer> GetAsync(string name)
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuPlayers.FirstOrDefaultAsync(x => x.Name == name);
        }

        public static async Task<List<DbJiangHuPlayer>> GetAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuPlayers.ToListAsync();
        }

        public static async Task<List<DbJiangHuPlayer>> QueryRankAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuPlayers
                .AsNoTracking()
                .OrderByDescending(x => x.TotalPowerValue)
                .Include(x => x.Player)
                .Where(x => x.Player != null
#if !DEBUG
                    && !x.Player.Name.Contains("[GM]") && !x.Player.Name.Contains("[PM]")
#endif
                )
                .Take(100)
                .ToListAsync();
        }
    }
}
