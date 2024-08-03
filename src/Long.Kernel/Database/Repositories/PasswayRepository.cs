using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class PasswayRepository
    {
        public static List<DbPassway> Get(uint idMap)
        {
            using var context = new ServerDbContext();
            return context.Passways.Where(x => x.MapId == idMap).ToList();
        }
    }
}
