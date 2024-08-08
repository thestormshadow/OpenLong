using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Events.Repositories
{
    public static class CompeteRankRepository
    {
        public static async Task<List<DbSynCompeteRank>> GetSynCompeteRankAsync()
        {
            await using ServerDbContext serverDbContext = new();
            return await serverDbContext.SynCompeteRanks.OrderBy(x => x.Rank).ToListAsync();
        }
    }
}
