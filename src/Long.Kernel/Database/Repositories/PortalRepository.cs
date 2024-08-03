using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class PortalRepository
    {
        public static DbPortal Get(uint idMap, uint idx)
        {
            using var context = new ServerDbContext();
            return context.Portals.FirstOrDefault(x => x.MapId == idMap && x.PortalIndex == idx);
        }
    }
}
