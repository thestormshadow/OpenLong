using Long.Login.Database.Entities;
using Long.Login.Network.Login;

namespace Long.Login.States
{
    public sealed class User
    {
        private const int LOGIN_EXP_SECONDS = 15;

        public User(LoginClient client, GameAccount gameAccount, Realm realm)
        {
            Client = client;
            AccountID = (uint)gameAccount.Id;
            UserName = gameAccount.UserName;
            RealmID = realm.Id;
            ExpirationTime = DateTime.Now.AddSeconds(LOGIN_EXP_SECONDS);
        }

        public Guid Guid => Client.Guid;
        public uint AccountID { get; }
        public string UserName { get; }
        public Guid RealmID { get; }
        public LoginClient Client { get; }
        public DateTime ExpirationTime { get; }
        public bool HasExpired => DateTime.Now < ExpirationTime;
    }
}
