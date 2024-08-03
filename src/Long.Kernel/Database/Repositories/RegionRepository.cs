using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class RegionRepository
    {
        public static List<DbRegion> Get(uint idMap)
        {
            using var ctx = new ServerDbContext();
            return ctx.Regions.Where(x => x.MapIdentity == idMap).ToList();
        }
    }
}
