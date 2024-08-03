using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Fate.Repositories
{
    public static class FateInitAttribRepository
    {
        public static async Task<IList<DbInitFateAttrib>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.InitFateAttribs.ToListAsync();
        }
    }
}
