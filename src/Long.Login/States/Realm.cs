using Long.Login.Database.Entities;
using Long.Login.Network.Game;

namespace Long.Login.States
{
    public sealed class Realm
    {
        public Realm(
            RealmData realm,
            GameClient client
            )
        {
            Id = realm.RealmID;
            RealmId = realm.RealmIdx;
            Name = realm.Name;
            IpAddress = realm.GameIPAddress;
            Port = (int)realm.GamePort;
            IsProduction = realm.ProductionRealm;
            Client = client;
        }

        public Guid Id { get; }
        public int RealmId { get; }
        public string Name { get; }
        public string IpAddress { get; }
        public int Port { get; }
        public bool IsProduction { get; }
        public GameClient Client { get; }
    }
}
