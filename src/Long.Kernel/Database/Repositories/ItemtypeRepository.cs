using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class ItemtypeRepository
    {
        public static List<DbItemtype> Get()
        {
            using var db = new ServerDbContext();
            return db.Itemtypes.ToList();
        }
    }
}
