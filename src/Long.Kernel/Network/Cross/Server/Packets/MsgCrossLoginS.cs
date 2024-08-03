using Long.Kernel.Managers;
using Long.Kernel.Settings;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossLoginS : MsgCrossLogin<CrossServerActor>
    {
        private static readonly ILogger logger = Log.ForContext<MsgCrossLoginS>();

        public override async Task ProcessAsync(CrossServerActor client)
        {
            var settings = GameServerSettings.Instance.Realm;

            var server = settings.Servers.FirstOrDefault(x => x.ServerID == Data.ServerIDX);
            if (server == null)
            {
                logger.Error("Unexistent server {0} {1} {2} tried to login from {3}.", Data.ServerID, Data.ServerIDX, Data.ServerName, client.IpAddress);
                client.Disconnect();
                return;
            }

            if (GameServerSettings.IsRealm)
            {
                if (!settings.Username.Equals(Data.Username)
                    || !settings.Password.Equals(Data.Password))
                {
                    logger.Error("Invalid username or password from {0} on {1}.", server.ServerName, client.IpAddress);
                    client.Disconnect();
                    return;
                }

                if (!server.IPAddress.Equals(client.IpAddress))
                {
                    logger.Error("Invalid connection IP Address from {0} on {1}. Expected: {2}", server.ServerName, client.IpAddress, server.IPAddress);
                    client.Disconnect();
                    return;
                }
            }
            else
            {
                if (!server.Username.Equals(Data.Username)
                    || !server.Password.Equals(Data.Password))
                {
                    logger.Error("Invalid username or password from {0} on {1}.", server.ServerName, client.IpAddress);
                    client.Disconnect();
                    return;
                }

                if (!server.IPAddress.Equals(client.IpAddress))
                {
                    logger.Error("Invalid connection IP Address from {0} on {1}. Expected: {2}", server.ServerName, client.IpAddress, server.IPAddress);
                    client.Disconnect();
                    return;
                }
            }

            client.ServerId = server.ServerID;
            await client.SendAsync(new MsgCrossLoginExS
            {
                Data = new MsgCrossLoginEx<CrossServerActor>.CrossLoginExPB
                {
                    Response = MsgCrossLoginExS.SUCCESS_RESPONSE,
                    ServerID = RealmManager.ServerIdentity,
                }
            });
            logger.Information("Server {0} has been authorized to join the cross network.", server.ServerName);

            RealmConnectionManager.AddConnection(server.ServerID, client);
        }
    }
}
