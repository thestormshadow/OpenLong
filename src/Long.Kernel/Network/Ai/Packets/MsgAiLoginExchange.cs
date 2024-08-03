using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Processors;
using Long.Kernel.Settings;
using Long.Kernel.States;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiLoginExchange : MsgAiLoginExchange<AiClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAiLoginExchange>();

        public override async Task ProcessAsync(AiClient client)
        {
			GameServerSettings serverSettings = new GameServerSettings("Game");
			logger.Information($"Received auth data from {client.GUID}");
            if (client.Stage != AiClient.ConnectionStage.AwaitingAuth)
            {
                logger.Warning($"This Npc Server is already signed in!");
                await client.SendAsync(new MsgAiLoginExchangeEx()
                {
                    Data = new MsgAiLoginExchangeExContract()
					{
						Result = AiLoginResult.AlreadySignedIn
					}
                });
                return;
            }

            if (!Data.UserName.Contains(serverSettings.Ai.Username) ||
                !Data.Password.Contains(serverSettings.Ai.Password))
            {
                logger.Error($"Invalid username or password!!!");
                await client.SendAsync(new MsgAiLoginExchangeEx
                {
					Data = new MsgAiLoginExchangeExContract()
					{
						Result = AiLoginResult.AlreadySignedIn
					}
				});
                return;
            }

            if (NpcServer.NpcClient != null && !NpcServer.NpcClient.GUID.Equals(client.GUID))
            {
                logger.Warning($"NPC Server is already connected...");
                await client.SendAsync(new MsgAiLoginExchangeEx
                {
					Data = new MsgAiLoginExchangeExContract()
					{
						Result = AiLoginResult.AlreadyBound
					}
				});
                return;
            }

            client.Stage = AiClient.ConnectionStage.Authenticated;
            logger.Warning($"NPC Server login OK...");

			WorldProcessor.Instance.Queue(0, () =>
            {
                foreach (var mob in RoleManager.QueryRoles(x => x is Monster monster && monster.GeneratorId != 0))
                {
                    mob.QueueAction(mob.LeaveMapAsync);
                }
                return Task.CompletedTask;
            });

            await client.SendAsync(new MsgAiLoginExchangeEx
            {
				Data = new MsgAiLoginExchangeExContract()
				{
					Result = AiLoginResult.Success
				}
			});

            var players = RoleManager.QueryUserSet();
            foreach (var player in players)
            {
				await NpcServer.NpcClient.SendAsync(new MsgAiPlayerLogin(player));
            }
        }
    }
}
