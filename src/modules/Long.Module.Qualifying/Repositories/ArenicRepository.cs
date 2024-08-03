using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Qualifying.Repositories
{
    public static class QualifierRepository
    {
        public static async Task<List<DbArenic>> GetAsync(DateTime date, int type)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Arenics
                            .Where(x => x.Date == uint.Parse(date.Date.ToString("yyyyMMdd")) && x.Type == type)
                            .ToListAsync();
        }
    }
}
