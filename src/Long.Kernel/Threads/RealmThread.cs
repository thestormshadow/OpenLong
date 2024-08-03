using Long.Kernel.Managers;
using Long.Kernel.Settings;
using Quartz;

namespace Long.Kernel.Threads
{
    [DisallowConcurrentExecution]
    public sealed class RealmThread : IJob
    {
        private const int REALM_PING_SECS = 30;
        private const int REALM_RECONNECT_SECS = 15;

        private static readonly ILogger logger = Log.ForContext<RealmThread>();

        private static readonly TimeOut realmPingTm = new(REALM_PING_SECS);
        private static readonly TimeOut realmConnectTm = new(REALM_RECONNECT_SECS);

        static RealmThread()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (!RealmConnectionManager.NoRealmServer)
            {
                if (!GameServerSettings.IsRealm)
                {
                    if (RealmConnectionManager.RealmSession == null && realmConnectTm.ToNextTime())
                    {
                        await RealmConnectionManager.ConnectToRealmAsync();
                    }
                }
            }

            if (realmPingTm.ToNextTime())
            {
                await RealmConnectionManager.PingServersAsync();
            }

            await RealmManager.OnTimerAsync();
        }

        public static void ResetRealmPingTimer()
        {
            realmPingTm.Startup(REALM_PING_SECS);
        }
    }
}
