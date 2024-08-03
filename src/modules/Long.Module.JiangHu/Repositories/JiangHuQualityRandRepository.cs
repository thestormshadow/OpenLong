using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuQualityRandRepository
    {
        public static async Task<List<DbJiangHuQualityRand>> GetAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuQualityRands.ToListAsync();
        }
    }
}
