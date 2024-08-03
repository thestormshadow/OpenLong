using Long.Login.Network.Game;
using Long.Login.Network.Login;
using Long.Login.Threading;
using Long.Shared.Threads;

namespace Long.Login
{
    public class Kernel
    {
        private static readonly ILogger logger = Log.ForContext<Kernel>();

        private static SchedulerFactory schedulerFactory { get; set; }
        private static LoginServerSocket serverSocket;
        private static GameServerSocket gameServerSocket;

        public static async Task<bool> InitializeAsync(ServerSettings serverSettings)
        {
            try
            {
                serverSocket = new LoginServerSocket(serverSettings);
                _ = serverSocket.StartAsync(serverSettings.Login.Port);

                gameServerSocket = new();
                _ = gameServerSocket.StartAsync(serverSettings.Game.Port);

                schedulerFactory = new SchedulerFactory();
                await schedulerFactory.StartAsync();
                await schedulerFactory.ScheduleAsync<BasicThread>("* * * * * ?");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on server initialization! {0}", ex.Message);
                return false;
            }
        }

    }
}
