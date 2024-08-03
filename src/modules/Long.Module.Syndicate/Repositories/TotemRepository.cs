using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Syndicate.Repositories
{
    public static class TotemRepository
    {
        public static async Task<List<DbTotemAdd>> GetAsync(uint idSyndicate)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TotemAdds.Where(x => x.OwnerIdentity == idSyndicate).ToListAsync();
        }
    }
}
