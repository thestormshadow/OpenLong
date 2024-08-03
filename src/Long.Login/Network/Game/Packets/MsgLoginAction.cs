using Long.Network.Packets.Login;

namespace Long.Login.Network.Game.Packets
{
    public sealed class MsgLoginAction : MsgLoginAction<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgLoginAction>();

        public override async Task ProcessAsync(GameClient client)
        {
            if (client.Realm != null && client.Realm.RealmId != Data.ServerID)
            {
                logger.Warning("Server {0} {1} submit invalid server ID {2}", client.Realm.RealmId, client.Realm.Name, Data.ServerID);
                client.Disconnect();
                return;
            }

            switch ((LoginActionEnum)Data.Action)
            {
                case LoginActionEnum.RequestMasterRealm:
                    {
                        //if (client.Realm.MasterRealmID == 0)
                        //{
                        //    // this do not have a master
                        //    await client.SendAsync(new MsgLoginAction
                        //    {
                        //        Data = new LoginActionPB
                        //        {
                        //            ServerID = (uint)client.Realm.RealmId,
                        //            ServerUUID = client.Realm.Id.ToString(),
                        //            Action = (uint)LoginActionEnum.RequestMasterRealm,
                        //            Command = 2,
                        //        }
                        //    });
                        //    return;
                        //}

                        //if (await client.Realm.SubmitMasterRealmInfoAsync())
                        //{
                        //    //await client.SendAsync(new MsgLoginAction
                        //    //{
                        //    //    Data = new LoginActionPB
                        //    //    {
                        //    //        ServerID = (uint)client.Realm.RealmId,
                        //    //        ServerUUID = client.Realm.Id.ToString(),
                        //    //        Action = (uint)LoginActionEnum.RequestMasterRealm,
                        //    //        Param = (uint)client.Realm.RealmId,
                        //    //        Data = (uint)client.Realm.MasterRealmID
                        //    //    }
                        //    //});
                        //    return;
                        //}

                        //// we have a master but probably he is not connected!
                        //await client.SendAsync(new MsgLoginAction
                        //{
                        //    Data = new LoginActionPB
                        //    {
                        //        ServerID = (uint)client.Realm.RealmId,
                        //        ServerUUID = client.Realm.Id.ToString(),
                        //        Action = (uint)LoginActionEnum.RequestMasterRealm,
                        //        Command = 1,
                        //    }
                        //});
                        return;
                    }

                case LoginActionEnum.RequestCredentials:
                    {
                        //var callerServer = client.Realm;
                        //var targetServer = RealmManager.GetRealm(Data.Data);

                        //if (targetServer == null)
                        //{
                        //    logger.Warning("Server {0} tried asked for credentials to connect on not connected ServerID {1}.", callerServer.Name, Data.Data);
                        //    await client.SendAsync(this);
                        //    return;
                        //}


                        //logger.Information("Server {0} has been authorized and redirected to connect to server {1}.");
                        return;
                    }

                case LoginActionEnum.RequestRealmData:
                    {
                        // moved to realm auth
                        return;
                    }

                case LoginActionEnum.Ping:
                    {
                        client.LastPing = (uint)Environment.TickCount;
                        await client.SendAsync(this);
                        return;
                    }
            }
        }
    }
}
