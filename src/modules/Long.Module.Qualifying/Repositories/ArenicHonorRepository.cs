using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Qualifying.Repositories
{
    public static class QualifierHonorRepository
    {
        public static async Task<List<DbArenicHonor>> GetAsync(byte type)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.ArenicHonors.Where(x => x.Type == type).ToListAsync();
        }
    }
}
