using Long.Login.Database.Repositories;
using Long.Login.Managers;
using Long.Login.States;
using Long.Network.Packets.Login;
using Long.Network.Security;
using System.Text;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLoginRealmAuth : MsgLoginRealmAuth<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLoginRealmAuth>();
        private static readonly byte[] AES_KEY;

        static MsgLoginRealmAuth()
        {
            string strKey = Encoding.UTF8.GetString(Convert.FromBase64String("OTEzMTFhMjFlMDI4NWQ0NjY3N2FhNzVkNDRjNzI3YWM="));
            AES_KEY = new byte[strKey.Length / 2];
            for (var index = 0; index < AES_KEY.Length; index++)
            {
                string byteValue = strKey.Substring(index * 2, 2);
                AES_KEY[index] = Convert.ToByte(byteValue, 16);
            }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            if (!Guid.TryParse(Data.RealmID, out var realmID))
            {
                logger.Warning("Invalid realmID {0} for realm auth", Data.RealmID);
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.InvalidRealm);
                return;
            }

            var realmData = RealmRepository.GetById(realmID);
            if (realmData == null)
            {
                logger.Warning("Realm {0} not found.", realmID);
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.InvalidRealm);
                return;
            }

            string username = AesCipherHelper.Encrypt(AES_KEY, Data.Username);
            string password = AesCipherHelper.Encrypt(AES_KEY, Data.Password);
            if (!username.Equals(realmData.Username) || !password.Equals(realmData.Password))
            {
                logger.Warning("Realm {0} invalid username or password.", realmData.RealmID);
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.InvalidPassword);
                return;
            }

#if DEBUG
            //if (!realmData.GameIPAddress.StartsWith("192.168.")
            //    && !realmData.GameIPAddress.StartsWith("127."))
            //{
            //    logger.Warning("Realm {0} is not local.", realmID);
            //    await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.InvalidAddress);
            //    return;
            //}
#else
            if (!realmData.GameIPAddress.Equals(client.IpAddress))
            {
                logger.Warning("Realm {0} is not allowed to connect from {1}.", realmID, client.IpAddress);
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.InvalidAddress);
                return;
            }
#endif

            var realm = RealmManager.GetRealm(realmID);
            if (realm != null)
            {
                logger.Warning("Realm {0} [IPAddress: {1}] duplicated login.", realmID, client.IpAddress);
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.AlreadyConnected);
                realm.Client.Disconnect();
                return;
            }

            client.Realm = realm = new Realm(realmData, client);
            if (!RealmManager.AddRealm(realm))
            {
                await RejectConnectionAsync(client, MsgLoginRealmAuthEx<GameClient>.ResponseCode.AlreadyConnected2);
                return;
            }

            await client.SendAsync(new MsgLoginRealmAuthEx(MsgLoginRealmAuthEx<GameClient>.ResponseCode.Success));
        }

        private static Task RejectConnectionAsync(GameClient client, MsgLoginRealmAuthEx.ResponseCode code)
        {
            return client.SendAsync(new MsgLoginRealmAuthEx(code), () =>
            {
                client.Disconnect();
                return Task.CompletedTask;
            });
        }
    }
}
