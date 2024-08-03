using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class MagicRepository
    {
        public static async Task<List<DbMagic>> GetAsync(uint idUser)
        {
            await using var db = new ServerDbContext();
            return db.Magic.Where(x => x.OwnerId == idUser).ToList();
        }
    }
}
