using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class CoatStorageRepository
    {
        public static List<DbCoatStorageAttr> GetCoatAttributes()
        {
            using var ctx = new ServerDbContext();
            return ctx.CoatStorageAttrs.ToList();
        }

        public static List<DbCoatStorageType> GetCoatTypes()
        {
            using var ctx = new ServerDbContext();
            return ctx.CoatStorageTypes.ToList();
        }
    }
}
