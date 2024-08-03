using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Family.Repositories
{
    public static class FamilyRepository
    {
        public static async Task<List<DbFamily>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Families.ToListAsync();
        }
    }
}
