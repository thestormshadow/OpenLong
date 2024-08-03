using Long.Kernel.Managers;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletLoginEx : MsgPigletLoginEx<PigletActor>
    {
        private static readonly ILogger logger = Log.ForContext<MsgPigletLoginEx>();

        public override async Task ProcessAsync(PigletActor client)
        {
            PigletClient.ConnectionStage = PigletClient.ConnectionState.Connected;
            PigletClient.Instance.Actor = client;
            logger.Information($"GM Server connected!!!");

            await client.SendAsync(new MsgPigletRealmStatus
            {
                Data = new MsgPigletRealmStatus<PigletActor>.RealmStatusData
                {
                    Status = MaintenanceManager.ServerUp
                }
            });

            MsgPigletUserLogin userLogin = new MsgPigletUserLogin()
            {
                Data = new MsgPigletUserLogin<PigletActor>.UserLoginData()
                {
                    Users = new List<MsgPigletUserLogin<PigletActor>.UserData>(),
                    ServerSync = true
                },
            };
            foreach (var user in RoleManager.QueryUserSet())
            {
                if (userLogin.Data.Users.Count >= 50)
                {
                    await client.SendAsync(userLogin);
                    userLogin.Data.Users.Clear();
                }

                userLogin.Data.Users.Add(new MsgPigletUserLogin<PigletActor>.UserData
                {
                    AccountId = user.Client.AccountIdentity,
                    IsLogin = true,
                    UserId = user.Identity
                });
            }            
            if (userLogin.Data.Users.Count > 0)
            {
                await client.SendAsync(userLogin);
            }
        }
    }
}
