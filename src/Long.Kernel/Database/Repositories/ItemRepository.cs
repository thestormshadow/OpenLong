using Long.Database.Entities;
using Long.Database.Entities.Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class ItemRepository
    {
        public static List<DbItem> Get(uint idUser)
        {
            using var db = new ServerDbContext();
            return db.Items.Where(x => x.PlayerId == idUser).ToList();
        }

        public static async Task<DbItem> GetByIdAsync(uint idItem)
        {
            await using var db = new ServerDbContext();
            return await db.Items.FirstOrDefaultAsync(x => x.Id == idItem);
        }

        public static async Task<List<DbItem>> GetBySyndicateAsync(uint idSyndicate)
        {
            await using var db = new ServerDbContext();
            return db.Items.Where(x => x.Syndicate == idSyndicate).ToList();
        }

        public static async Task<List<DbItemLimit>> GetLimitsAsync()
        {
            await using var db = new ServerDbContext();
            return await db.ItemsLimit.ToListAsync();
        }
    }
}
