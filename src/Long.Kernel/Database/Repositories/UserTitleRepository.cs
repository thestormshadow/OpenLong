using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public class UserTitleRepository
    {
        public static async Task<List<DbUserTitle>> GetByUserAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.UserTitles.Where(x => x.PlayerId == idUser).ToListAsync();
        }
    }
}
