using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public class ItemCrossRepository
    {
        public static List<DbItemCross> GetCross()
        {
            using var ctx = new ServerDbContext();
            return ctx.ItemsCross.ToList();
        }

        public static List<DbItemOSLimit> GetOSLimit()
        {
            using var ctx = new ServerDbContext();
            return ctx.ItemsOSLimit.ToList();
        }
    }
}
