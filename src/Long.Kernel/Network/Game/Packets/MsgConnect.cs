using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.States;
using Long.Kernel.States.Registration;
using Long.Kernel.States.User;
using Long.Network.Packets.Game;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public class MsgConnect : MsgConnect<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgConnect>();

        public override async Task ProcessAsync(GameClient client)
        {
            TransferAuthArgs auth;
            if (client.Creation != null)
            {
                uint token = (uint)Token;
                if (token != client.Creation.Token)
                {
                    await client.DisconnectWithMessageAsync(MsgTalk.LoginInvalid);
                    logger.Warning("Invalid client creation Token: {0} from {1}", Token, client.IpAddress);
                    return;
                }

                auth = RoleManager.GetLoginRequest(token.ToString());
                if (auth == null)
                {
                    await client.DisconnectWithMessageAsync(MsgTalk.LoginInvalid);
                    logger.Warning("Invalid creation Token: {0} from {1}", Token, client.IpAddress);
                    return;
                }

                RoleManager.RemoveLoginRequest(token.ToString());
                client.Creation = null;
            }
            else
            {
                auth = RoleManager.GetLoginRequest(Token.ToString());
                if (auth == null)
                {
                    await client.DisconnectWithMessageAsync(MsgTalk.LoginInvalid);
                    logger.Warning("Invalid Login Token: {0} from {1}", Token, client.IpAddress);
                    return;
                }
                RoleManager.RemoveLoginRequest(Token.ToString());
            }

            if (RealmManager.RealmIdentity != 0 && RealmManager.RealmIdentity == RealmManager.ServerIdentity)
            {
                logger.Error("User attempted to login on realm! From: {0}", client.IpAddress);
                await client.DisconnectWithMessageAsync(MsgTalk.LoginRealmInvalid);
                return;
            }

            client.AccountIdentity = auth.AccountID;
            client.AuthorityLevel = auth.AuthorityID;
            client.MacAddress = MacAddress;

#if DEBUG
            //if (client.AuthorityLevel < 2)
            //{
            //    await client.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.NonCooperatorAccount);
            //    logger.Warning("{0} non cooperator account.", client.Identity);
            //    return;
            //}
#endif

            // Generate new keys and check for an existing character
            DbUser character = await UserRepository.FindAsync(auth.AccountID);
            if (character == null)
            {
                // Create a new character
                client.Creation = new AwaitingCreation{ AccountId = auth.AccountID, Token = (uint)Token };
                Managers.RegistrationManager.Registration.Add(client.Creation.Token);
                await client.SendAsync(MsgTalk.LoginNewRole);
                return;
            }

            // The character exists, so we will turn the timeout back.
            client.ReceiveTimeOutSeconds = 30; // 30 seconds or DC
                                               // Character already exists
            client.Character = new Character(client, character)
            {
                VipLevel = (uint)auth.VIPLevel
            };
            if (await RoleManager.LoginUserAsync(client))
            {
                await client.SendAsync(MsgTalk.LoginOk);

                await client.SendAsync(new MsgServerInfo());
                await client.SendAsync(new MsgUserInfo(client.Character));
                await client.SendAsync(new MsgData(DateTime.Now));
                await client.SendAsync(new MsgVipFunctionValidNotify() { Flags = (int)client.Character.UserVipFlag });

                try
                {
                    await OnUserLoginAsync(client.Character);

                    await client.Character.WeaponSkill.InitializeAsync();
                    await client.Character.UserPackage.InitializeAsync();

                    client.Character.Statistic = new UserStatistic(client.Character);
                    await client.Character.Statistic.InitializeAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Could not initialize user ({1} {2}) data! {0}", ex.Message, client.Character.Identity, client.Character.Name);
                    await client.DisconnectWithMessageAsync(MsgConnectEx.RejectionCode.DatabaseError);
                    return;
                }

#if DEBUG
                await client.Character.SendAsync($"Server is running in DEBUG mode.", TalkChannel.Talk, Color.White);
#endif
            }
        }
    }
}
