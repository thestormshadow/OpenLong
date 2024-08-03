using Long.Ai.Managers;
using Long.Ai.Sockets;
using Long.Ai.Sockets.Packets;
using Quartz;
using Serilog;

namespace Long.Ai.Threading
{
    [DisallowConcurrentExecution]
    public class BasicThread : IJob
    {
        public const int LOGIN_DELAY_SECS = 1000;

		private static readonly Serilog.ILogger logger = Log.ForContext<BasicThread>();

		private const string CONSOLE_TITLE = "Conquer Online AI Server - {0} - {1} - Roles: {2}";
        private static long lastUpdateTick = 0;

        private static readonly TimeOutMS gameServerReconnectTimer = new();
        private static readonly TimeOutMS pingTimeoutTimer = new();

        static BasicThread()
        {
            gameServerReconnectTimer.Startup(LOGIN_DELAY_SECS);
            pingTimeoutTimer.Startup(15000);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.Title = string.Format(CONSOLE_TITLE,
                Kernel.NetworkMonitor.GetCurrentInfo((int)(Environment.TickCount - lastUpdateTick)),
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                RoleManager.RolesCount);

            lastUpdateTick = Environment.TickCount;

            bool isGameServerConnected = !(GameServerHandler.Instance?.GameServer == null || GameServerHandler.Instance.GameServer.Socket.Connected != true);
            if (!isGameServerConnected && gameServerReconnectTimer.ToNextTime(LOGIN_DELAY_SECS))
            {
                logger.Information($"Attempting connection with the game server on [{ServerConfiguration.Configuration.Ai.IPAddress}:{ServerConfiguration.Configuration.Ai.Port}]...");

                GameServerHandler gameServerHandler = new();
                if (await gameServerHandler.ConnectToAsync(ServerConfiguration.Configuration.Ai.IPAddress, ServerConfiguration.Configuration.Ai.Port))
                {
                    logger.Information("Connected to the game server!");
                }
            }

            if (isGameServerConnected)
            {
                if (GameServerHandler.Instance.GameServer.Stage == GameServer.ConnectionStage.Awaiting && gameServerReconnectTimer.IsTimeOut())
                {
                    GameServerHandler.Instance.GameServer.Stage = GameServer.ConnectionStage.Exchanging;
                    var msg = new MsgAiHandshake(GameServerHandler.Instance.GameServer.DiffieHellman.PublicKey, GameServerHandler.Instance.GameServer.DiffieHellman.Modulus, null, null);
                    await GameServerHandler.Instance.GameServer.SendAsync(msg);
                }
                else if (pingTimeoutTimer.ToNextTime())
                {
                    await GameServerHandler.Instance.GameServer.SendAsync(new MsgAiPing
                    {
                        Data = new Network.Packets.Ai.MsgAiPingContract
                        {
							Timestamp = Environment.TickCount,
							TimestampMs = Environment.TickCount64
						}
                        
                    });
                }
            }
        }

        public static void ResetReconnectTimer()
        {
            gameServerReconnectTimer.Startup(LOGIN_DELAY_SECS);
            pingTimeoutTimer.Startup(15000);
        }
    }
}
