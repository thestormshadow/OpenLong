using Long.Game.Threading;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Cross.Server;
using Long.Kernel.Network.Game;
using Long.Kernel.Processors;
using Long.Kernel.Settings;
using Long.Kernel.States;
using Long.Kernel.Threads;
using Long.Shared.Threads;
using Long.World;
using Serilog;
using System.Net.Sockets;

namespace Long.Game
{
    public class Kernel
    {
        private static readonly ILogger logger = Log.ForContext<Kernel>();

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private static SchedulerFactory schedulerFactory { get; set; }
        private static GameServerSocket gameServerSocket { get; set; }
        private static CrossServerListener crossServerListener { get; set; }
        private static NpcServer npcServer { get; set; }

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
				await EventManager.InitializeAsync().ConfigureAwait(true);
				ModuleManager.Initialize();
				await ModuleManager.OnServerInitializeModulesAsync();
				await NpcManager.InitializeAsync();
                await LotteryManager.InitializeAsync();
                await AchievementManager.InitializeAsync();
                await ActivityManager.InitializeAsync();
                await ProcessGoalManager.InitializeAsync();
                await SlotMachineManager.InitializeAsync();
                await DailySignIn.StaticInitializeAsync();
				await BattleSystemManager.InitializeAsync().ConfigureAwait(true);
				await CoatStorageManager.InitializeAsync();
                await TitleStorageManager.InitializeAsync();
				await MineManager.InitializeAsync().ConfigureAwait(true);

				await RealmManager.InitializeAsync();
                await KingdomManager.InitializeAsync();

                await ServerStatisticManager.InitializeAsync();

                gameServerSocket = new GameServerSocket(serverSettings);
                _ = gameServerSocket.StartAsync(serverSettings.Game.Port, serverSettings.Game.IPAddress);

                crossServerListener = new CrossServerListener();
                _ = crossServerListener.StartAsync(serverSettings.Cross.ListenPort, serverSettings.Cross.IPAddress);

				npcServer = new NpcServer();
				_ = npcServer.StartAsync(serverSettings.Ai.Port, serverSettings.Ai.IPAddress);

				LuaScriptManager.Run("Event_Server_Start()");

                BasicThread.SetStartTime();

				schedulerFactory = new SchedulerFactory();
                await schedulerFactory.StartAsync();
                await schedulerFactory.ScheduleAsync<BasicThread>("* * * * * ?");
                await schedulerFactory.ScheduleAsync<EventThread>("* * * * * ?");
                await schedulerFactory.ScheduleAsync<RealmThread>("* * * * * ?");
				await schedulerFactory.ScheduleAsync<UserThread>("* * * * * ?");
				await schedulerFactory.ScheduleAsync<RoleThread>("* * * * * ?");

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

		public static void SetMaintenance()
		{
			try
			{
				npcServer.Close();
			}
			catch (Exception ex)
			{
				logger.Warning(ex, "Error when closing NPC Server socket!!! {}", ex.Message);
			}
		}
	}
}
