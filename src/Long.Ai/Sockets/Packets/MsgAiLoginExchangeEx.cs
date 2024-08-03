using Long.Ai.Managers;
using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiLoginExchangeEx : MsgAiLoginExchangeEx<GameServer>
	{
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiLoginExchangeEx>();

		public override async Task ProcessAsync(GameServer client)
        {
            switch (Data.Result)
            {
                case AiLoginResult.Success:
                    {
                        logger.Information("Accepted on the game server!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Accepted on the game server!");
						Console.ResetColor();
						client.Stage = GameServer.ConnectionStage.Ready;

                        GeneratorManager.SynchroGenerators();
                        return;
                    }

                case AiLoginResult.AlreadySignedIn:
                    logger.Error("Could not connect to the game server! Already signed in!");
                    break;
                case AiLoginResult.InvalidPassword:
                    logger.Error("Could not connect to the game server! Invalid username or password!");
                    break;
                case AiLoginResult.InvalidAddress:
                    logger.Error("Could not connect to the game server! Address not authorized!");
                    break;
                case AiLoginResult.AlreadyBound:
                    logger.Error("Could not connect to the game server! Server is already bound!");
                    break;
                case AiLoginResult.UnknownError:
                    logger.Error("Could not connect to the game server! Unknown error!");
                    break;
            }

            if (client.Socket.Connected)
            {
                client.Disconnect();
            }
        }
    }
}
