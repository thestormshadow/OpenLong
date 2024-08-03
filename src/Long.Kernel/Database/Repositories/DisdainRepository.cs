using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class DisdainRepository
    {
        public static async Task<List<DbDisdain>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Disdains.ToListAsync();
        }
    }
}
