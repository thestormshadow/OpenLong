using Long.Login.Database.Entities;

namespace Long.Login.Database.Repositories
{
    public class RealmRepository
    {
        private static readonly ILogger logger = Log.ForContext<RealmRepository>();

        public static RealmData GetById(Guid realmId)
        {
            try
            {
                using var context = new ServerDbContext();
                return context.RealmDatas.FirstOrDefault(x => x.RealmID == realmId);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on getting realm data {0}", ex.Message);
                return null;
            }
        }

        public static List<RealmData> GetByRealm(uint masterId)
        {
            using var context = new ServerDbContext();
            return context.RealmDatas.Where(x => x.MasterRealmID == masterId || x.RealmIdx == masterId).ToList();
        }
    }
}