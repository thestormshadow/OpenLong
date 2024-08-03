using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Guide.Repositories
{
    public static class TutorBattleLimitTypeRepository
    {
        public static async Task<List<DbTutorBattleLimitType>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TutorBattleLimitTypes.ToListAsync();
        }
    }
}
