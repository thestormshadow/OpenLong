using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class ItemAdditionRepository
    {
        public static List<DbItemAddition> Get()
        {
            using var db = new ServerDbContext();
            return db.ItemAdditions.ToList();
        }
    }
}
