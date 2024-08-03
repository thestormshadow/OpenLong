using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class PkStatisticRepository
    {
        public static async Task<List<DbPkStatistic>> GetAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.PkStatistics
				.Where(x => x.KillerId == idUser)
                .ToListAsync();
        }
    }
}
