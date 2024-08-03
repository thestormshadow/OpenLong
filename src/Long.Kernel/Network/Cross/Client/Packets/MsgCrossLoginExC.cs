using Long.Kernel.Managers;
using Long.Kernel.Settings;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossLoginExC : MsgCrossLoginEx<CrossClientActor>
    {
        private static readonly ILogger logger = Log.ForContext<MsgCrossLoginExC>();
        public override Task ProcessAsync(CrossClientActor client)
        {
            var targetServer = GameServerSettings.Instance.Realm.Servers.FirstOrDefault(x => x.ServerID == Data.ServerID); // MUST not return null
            if (targetServer == null)
            {
                logger.Fatal("Logged to not setup server {0}?", Data.ServerID);
                client.Disconnect();
                return Task.CompletedTask;
            }

            if (Data.Response == SUCCESS_RESPONSE)
            {
                logger.Information("Connected to {0}.", targetServer.ServerName);
                if (Data.ServerID != RealmManager.RealmIdentity)
                {
                    RealmConnectionManager.AddConnection(Data.ServerID, client);
                }
                return Task.CompletedTask;
            }

            logger.Error("Login error! {0}", Data.Response);
            if (client.Socket.Connected)
            {
                client.Disconnect();
            }
            return Task.CompletedTask;
        }
    }
}
