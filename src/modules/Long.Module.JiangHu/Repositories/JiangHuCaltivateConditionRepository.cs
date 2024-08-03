using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuCaltivateConditionRepository
    {
        public static async Task<List<DbJiangHuCaltivateCondition>> GetAsync()
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuCaltivateConditions.ToListAsync();
        }
    }
}
