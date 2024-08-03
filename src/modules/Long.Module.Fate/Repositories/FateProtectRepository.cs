using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FateProtectRepository
    {
        public static async Task<IList<DbFateProtect>> GetAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.FateProtects.Where(x => x.PlayerId == idUser).ToListAsync();
        }
    }
}
