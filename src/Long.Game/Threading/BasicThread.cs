using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Login;
using Long.Kernel.Network.Login.Packets;
using Long.Kernel.Settings;
using Long.Network.Packets.Login;
using Long.Shared;
using Quartz;

namespace Long.Game.Threading
{
    [DisallowConcurrentExecution]
    public sealed class BasicThread : IJob
    {
        private const int TITLE_STC_MS_UPDATE = 1000;

        private static DateTime startTime;
        private static long lastTick = Environment.TickCount64;
        private static TimeOut accountConnectTimeout = new TimeOut(3);
        private static TimeOut accountPingTm;

        static BasicThread()
        {
            accountPingTm = new TimeOut();
            accountPingTm.Startup(30);
        }

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

            if (GameServerSocket.Instance != null)
            {
                var info = GameServerSocket.Instance.NetworkMonitor.GetCurrentInfo(TITLE_STC_MS_UPDATE);
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
            else if (LoginClientSocket.Instance != null)
            {
                if (accountPingTm.ToNextTime())
                {
                    await LoginClientSocket.Instance.Client.SendAsync(new MsgLoginAction
                    {
                        Data = new MsgLoginAction<LoginServer>.LoginActionPB
                        {
                            Action = (uint)MsgLoginAction.LoginActionEnum.Ping,
                            ServerID = RealmManager.ServerIdentity,
                            ServerUUID = GameServerSettings.Instance.Game.Id.ToString()
                        }
                    });
                }
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
