using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.AstProf.Repositories
{
    public static class AstProfLevelRepository
    {
        public static async Task<List<DbAstProfLevel>> GetAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.AstProfLevels.Where(x => x.UserIdentity == idUser).ToListAsync();
        }
    }
}
