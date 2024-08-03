using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuAttribRandRepository
    {
        public static async Task<List<DbJiangHuAttribRand>> GetAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuAttribRands.ToListAsync();
        }
    }
}
