using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Guide.Repositories
{
    public static class TutorAccessRepository
    {
        public static async Task<DbTutorAccess> GetAsync(uint idGuide)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TutorAccess.FirstOrDefaultAsync(x => x.GuideIdentity == idGuide);
        }
    }
}
