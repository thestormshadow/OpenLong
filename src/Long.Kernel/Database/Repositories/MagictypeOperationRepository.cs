using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class MagictypeOperationRepository
    {
        public static async Task<List<DbMagictypeOp>> GetAsync()
        {
            await using var db = new ServerDbContext();
            return await db.MagictypeOperations.ToListAsync();
        }
    }
}
