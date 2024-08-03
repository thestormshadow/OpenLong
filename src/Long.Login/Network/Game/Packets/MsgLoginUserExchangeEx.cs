using Long.Login.Managers;
using Long.Login.Network.Login.Packets;
using Long.Login.States;
using Long.Network.Packets.Login;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLoginUserExchangeEx : MsgLoginUserExchangeEx<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLoginUserExchangeEx>();

        public override async Task ProcessAsync(GameClient client)
        {
            var request = UserManager.GetUser(Data.RequestGuid);
            if (request == null)
            {
                logger.Warning("Login request {0} not found! Expired or not existent.", Data.Request);
                return;
            }

            if (Data.Response != SUCCESS)
            {
                logger.Warning("Login request {0} for account {1} response code {2}", Data.Request, request.UserName, Data.Response);
                await request.Client.DisconnectWithRejectionCodeAsync(Long.Network.Packets.Game.MsgConnectEx<Login.LoginClient>.RejectionCode.AccountActivationFailed);
                return;
            }

            Realm realm = RealmManager.GetRealm(request.RealmID);
            if (realm == null)
            {
                logger.Warning("Realm {1} disconnected between authentication for user {0}", request.UserName, request.RealmID);
                await request.Client.DisconnectWithRejectionCodeAsync(Long.Network.Packets.Game.MsgConnectEx<Login.LoginClient>.RejectionCode.ServerDown);
                return;
            }

            LoginStatisticManager.IncreaseSuccessLogin();
            await request.Client.SendAsync(new MsgConnectEx(realm.IpAddress, (uint)realm.Port, Data.Token));
            logger.Information("User {0} has logged in on realm {1}", request.UserName, realm.Name);
        }
    }
}
