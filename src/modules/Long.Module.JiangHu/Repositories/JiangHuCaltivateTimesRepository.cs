using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.JiangHu.Repositories
{
    public static class JiangHuCaltivateTimesRepository
    {
        public static async Task<DbJiangHuCaltivateTimes> GetAsync(uint idUser)
        {
            await using var serverDbContext = new ServerDbContext();
            return await serverDbContext.JiangHuCaltivateTimes.FirstOrDefaultAsync(x => x.PlayerId == idUser);
        }
    }
}
