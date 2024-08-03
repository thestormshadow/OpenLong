using Long.Kernel.Managers;
using Long.Kernel.Network.Cross.Server;
using Long.Kernel.Processors;
using Long.Kernel.Settings;
using Long.Kernel.Threads;
using Long.Realm.Threading;
using Long.Shared.Threads;
using Long.World;
using Serilog;

namespace Long.Realm
{
    internal class Kernel
    {
        private static readonly ILogger logger = Log.ForContext<Kernel>();
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static SchedulerFactory schedulerFactory { get; set; }
        private static UserThread userThread { get; set; }
        private static EventThread eventThread { get; set; }
        private static CrossServerListener RealmSocket { get; set; }

        public static async Task<bool> InitializeAsync(GameServerSettings serverSettings)
        {
            try
            {
                WorldProcessor.Create(cancellationTokenSource.Token);

                await MapDataManager.LoadDataAsync();
                await MapManager.InitializeAsync();
                await ItemManager.InitializeAsync();
                await MagicManager.InitializeAsync();
                await ExperienceManager.InitializeAsync();
                await RoleManager.InitializeAsync();
                await SupermanManager.InitializeAsync();
                await DynamicGlobalDataManager.InitializeAsync();
                await ScriptManager.InitializeAsync();
                await NpcManager.InitializeAsync();
                await LotteryManager.InitializeAsync();
                await AchievementManager.InitializeAsync();
                await ActivityManager.InitializeAsync();
                await ProcessGoalManager.InitializeAsync();
                await SlotMachineManager.InitializeAsync();

                ModuleManager.Initialize();
                await ModuleManager.OnServerInitializeModulesAsync();

                await RealmManager.InitializeAsync();
                await KingdomManager.InitializeAsync();

                await ServerStatisticManager.InitializeAsync();

                LuaScriptManager.Run("Event_Server_Start()");

                BasicThread.SetStartTime();

                schedulerFactory = new SchedulerFactory();
                await schedulerFactory.StartAsync();
                await schedulerFactory.ScheduleAsync<BasicThread>("* * * * * ?");
                await schedulerFactory.ScheduleAsync<EventThread>("* * * * * ?");
				await schedulerFactory.ScheduleAsync<UserThread>("* * * * * ?");

				RealmSocket = new CrossServerListener();
                _ = RealmSocket.StartAsync(serverSettings.Game.Port, serverSettings.Game.IPAddress);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error on server initialization! {0}", ex.Message);
                return false;
            }
        }

        public static async Task CloseAsync()
        {
            await cancellationTokenSource.CancelAsync();
            for (int i = 5; i >= 0; i--)
            {
                logger.Information("Closing in {0} seconds...", i);
                await Task.Delay(1000);
            }
            await schedulerFactory.StopAsync();
        }
    }
}
