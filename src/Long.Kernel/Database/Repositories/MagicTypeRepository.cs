using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class MagicTypeRepository
    {
        public static async Task<List<DbMagictype>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return await db.Magictypes.ToListAsync();
        }
    }
}
