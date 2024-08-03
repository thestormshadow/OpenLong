using Long.Kernel.Managers;
using Long.Network.Packets.Login;

namespace Long.Kernel.Network.Login.Packets
{
    public sealed class MsgLoginAction : MsgLoginAction<LoginServer>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLoginAction>();

        public override async Task ProcessAsync(LoginServer client)
        {
            if (Data.ServerID != RealmManager.ServerIdentity)
            {
                return;
            }

            switch ((LoginActionEnum)Data.Action)
            {
                case LoginActionEnum.RequestMasterRealm:
                    {
                        //if (Data.Command == 1)
                        //{
                        //    logger.Warning("Realm is not connected to the account server! Retry in a few moments or verify servers connectivity.");
                        //    return;
                        //}

                        //if (Data.Command == 2)
                        //{
                        //    logger.Information("This server do not have or require a master server.");
                        //    RealmConnectionManager.NoRealmServer = true;
                        //    return;
                        //}

                        //RealmManager.RealmIdentity = Data.Param;

                        //if (Data.Strings.Count != 4)
                        //{
                        //    logger.Warning("Server received invalid authentication data. Strings length: {0}", Data.Strings.Count);
                        //    return;
                        //}

                        //RealmConnectionManager.ServerRelationInfo.TryRemove(Data.Param, out var _);
                        //RealmConnectionManager.ServerRelationInfo.TryAdd(Data.Param, new RealmConnectionManager.ServerRelationData
                        //{
                        //    ServerID = Data.Param,
                        //    IPAddress = Data.Strings[0],
                        //    Port = int.Parse(Data.Strings[1]),
                        //    Username = Data.Strings[2],
                        //    Password = Data.Strings[3]
                        //});

                        //await RealmConnectionManager.ConnectToRealmAsync();
                        return;
                    }

                case LoginActionEnum.Ping:
                    {
                        client.LastPing = (uint)Environment.TickCount;
                        return;
                    }
            }
        }
    }
}
