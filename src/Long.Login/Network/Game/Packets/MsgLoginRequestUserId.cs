using Long.Login.Database.Entities;
using Long.Login.Database.Repositories;
using Long.Network.Packets.Login;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLoginRequestUserId : MsgLoginRequestUserId<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLoginRequestUserId>();

        public override async Task ProcessAsync(GameClient client)
        {
            // TODO check if user is banned?
            var account = AccountRepository.GetByID((int)Data.AccountID);
            if (account == null)
            {
                logger.Warning("Could not find account ID {0} to create new account on Realm {1}", Data.AccountID, client.Realm.Name);
                await client.SendAsync(this);
                return;
            }

            RealmUser realmUser = new RealmUser
            {
                AccountId = (int)Data.AccountID,
                RealmId = client.Realm.RealmId,
                CreationDate = DateTime.Now,
            };
            
            if (!RealmUserRepository.Create(realmUser))
            {
                logger.Warning("Database error when creating new account on Realm {0}", client.Realm.Name);
                await client.SendAsync(this);
                return;
            }

            Data = new LoginRequestUserIdPB
            {
                UserID = realmUser.PlayerId,
                AccountID = account.Id,
                RequestID = Data.RequestID
            };
            await client.SendAsync(this);
            logger.Information("Created user {0} for account {1} on {2}", realmUser.PlayerId, account.Id, client.Realm.Name);
        }
    }
}
