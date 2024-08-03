using Long.Kernel.Managers;
using Long.Kernel.Network.Cross.Server;
using Long.Kernel.Network.Login;
using Long.Kernel.Settings;
using Long.Shared;
using Quartz;

namespace Long.Realm.Threading
{
    [DisallowConcurrentExecution]
    public sealed class BasicThread : IJob
    {
        private const int TITLE_STC_MS_UPDATE = 1000;
        private static TimeOut accountConnectTimeout = new TimeOut(3);
        private static long lastTick = Environment.TickCount64;
        private static DateTime startTime;

        public async Task Execute(IJobExecutionContext context)
        {
            double download = 0.00;
            double upload = 0.00;
            int packetsSent = 0;
            int packetsRecv = 0;

            var serverSettings = GameServerSettings.Instance;

            if (LoginClientSocket.Instance != null)
            {
                var info = LoginClientSocket.Instance.NetworkMonitor.GetCurrentInfo(TITLE_STC_MS_UPDATE);
                download += info.Download;
                upload += info.Upload;
                packetsSent += info.SentPackets;
                packetsRecv += info.RecvPackets;
            }

            if (CrossServerListener.Instance != null)
            {
                var info = CrossServerListener.Instance.NetworkMonitor.GetCurrentInfo(TITLE_STC_MS_UPDATE);
                download += info.Download;
                upload += info.Upload;
                packetsSent += info.SentPackets;
                packetsRecv += info.RecvPackets;
            }

            Console.Title = $"{serverSettings.Game.Name} " +
                $"- {DateTime.Now:yyyy-MM-dd HH:mm:ss} " +
                $"- Start: {startTime:yyyy-MM-dd HH:mm:ss} " +
                $"- Online: {RoleManager.OnlinePlayers}(max: {RoleManager.MaxOnlinePlayers}, limit: {serverSettings.Game.MaxOnlinePlayers}) " +
                $"- Network(↑{upload:F2} kbps [{packetsSent:0000}], ↓{download:F2} kbps [{packetsRecv:0000}])";

            if (LoginClientSocket.Instance == null && accountConnectTimeout.ToNextTime())
            {
                LoginClientSocket loginClientSocket = new LoginClientSocket();
                await loginClientSocket.ConnectToAsync(serverSettings.Login.IPAddress, serverSettings.Login.Port);
            }

            lastTick = Environment.TickCount64;
        }

        public static void SetStartTime()
        {
            if (startTime != default)
            {
                return;
            }
            startTime = DateTime.Now;
        }
    }
}
