using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Guide.Repositories
{
    public static class TutorTypeRepository
    {
        public static async Task<List<DbTutorType>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.TutorTypes.ToListAsync();
        }
    }
}
