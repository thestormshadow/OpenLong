using Long.Database.Entities;

namespace Long.Kernel.Database.Repositories
{
    public static class ClientConfigRepository
    {
        public static List<DbClientConfig> GetRealm(string crossRealm)
        {
            using var ctx = new ServerDbContext();
            return ctx.ClientConfigs.Where(x => x.CrossServer.Equals(crossRealm)).ToList();
        }

        public static DbClientConfig Get(uint serverID)
        {
            using var ctx = new ServerDbContext();
            return ctx.ClientConfigs.Where(x => x.Id == serverID).Take(1).FirstOrDefault();
        }
    }
}
