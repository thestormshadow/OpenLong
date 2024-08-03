using Long.Login.States;
using System.Collections.Concurrent;

namespace Long.Login.Managers
{
    public class RealmManager
    {
        private static readonly ILogger logger = Log.ForContext<RealmManager>();
        private static ConcurrentDictionary<Guid, Realm> realms = new();

        public static bool AddRealm(Realm realm)
        {
            if (realms.ContainsKey(realm.Id))
            {
                return false;
            }
            if (realms.TryAdd(realm.Id, realm))
            {
                logger.Information("Realm {0} connected.", realm.Name);
                return true;
            }
            logger.Warning("For some reason the realm {0} is already in the dictionary.", realm.Name);
            return false;
        }

        public static Realm GetRealm(Guid id)
        {
            return realms.TryGetValue(id, out var realm) ? realm : null;
        }

        public static Realm GetRealm(uint id)
        {
            return realms.Values.FirstOrDefault(x => x.RealmId == id);
        }

        public static Realm GetRealm(string name)
        {
            return realms.Values.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCulture));
        }

        public static bool ContainsRealm(Guid realmId)
        {
            return realms.ContainsKey(realmId);
        }

        public static bool IsConnected(Guid realmId)
        {
            if (realms.TryGetValue(realmId, out Realm realm))
            {
                return realm.Client.Socket.Connected;
            }
            return false;
        }

        public static bool RemoveRealm(Guid realmId)
        {
            return realms.TryRemove(realmId, out _);
        }
    }
}
