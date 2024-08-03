using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuPowerEffectRepository
    {
        public static async Task<List<DbJiangHuPowerEffect>> GetAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuPowerEffects.ToListAsync();
        }
    }
}
