using Long.Kernel.Database;
using Long.Kernel.Modules;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Modules.Managers;
using Long.Kernel.Modules.Systems.AstProf;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Modules.Systems.JiangHu;
using Long.Kernel.Modules.Systems.NeiGong;
using Long.Kernel.Modules.Systems.Peerage;
using Long.Kernel.Modules.Systems.Rank;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.Modules.Systems.Totem;
using Long.Kernel.Network.Game;
using Long.Kernel.Settings;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using System.Reflection;

namespace Long.Kernel.Managers
{
    public class ModuleManager
    {
        public enum ModuleEnum
        {
            AstProf,
            Booth,
            Competion,
            Family,
            Fate,
            Flower,
            Guide,
            JiangHu,
            NeiGong,
            Peerage,
            Pigeon,
            Rank,
            Relation,
            Syndicate,
            TaskDetail,
            Team,
            Totem,
            Trade,
            Qualifying
        }

        private static readonly ILogger logger = Log.ForContext<ModuleManager>();

        private static readonly List<ModuleEnum> modules = new List<ModuleEnum>();
        private static readonly List<INetworkMessageHandler> gameNetworkMessageHandlers = new();
        private static readonly List<IServerStartupHandler> serverStartupHandlers = new();
        private static readonly List<IUserSessionHandler> userSessionHandlers = new();
        private static readonly List<IDailyResetHandler> dayResetHandlers = new();
        private static readonly List<IUserDeletedHandler> userDeletedHandlers = new();
        private static readonly List<IEventTimer> eventTimerHandlers = new();
        private static readonly List<ITeamActionManager> teamActionManagerHandlers = new();
        private static readonly List<ISyndicateActionHandler> syndicateActionHandlers = new();
        private static readonly List<IFamilyActionHandler> familyActionHandler = new();
        private static readonly List<IChangeMapHandler> changeMapHandlers = new();
        private static readonly List<IOtherServerTransferHandler> otherServerTransferHandlers = new();

        public static IRegistrationManager RegistrationManager { get; set; }
        public static IFamilyManager FamilyManager { get; set; }
        public static INobilityManager NobilityManager { get; set; }
        public static ISyndicateManager SyndicateManager { get; set; }
        public static ITotemManager TotemManager { get; set; }
        public static IAstProfManager AstProfManager { get; set; }
        public static IDynamicRankManager DynamicRankManager { get; set; }
        public static IFateManager FateManager { get; set; }
        public static IJiangHuManager JiangHuManager { get; set; }
        public static INeiGongManager NeiGongManager { get; set; }

        public static bool IsModuleLoaded(ModuleEnum module)
        {
            return modules.Contains(module);
        }

        public static void Initialize()
        {
            GameServerSettings serverSettings = GameServerSettings.Instance;
            foreach (var module in serverSettings.Modules)
            {
                string moduleName = $"Long.Module.{module}.dll";
                Assembly assembly = Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, moduleName));
                Type[] types = assembly.GetExportedTypes();

                if (!Enum.TryParse<ModuleEnum>(module, out var moduleEnum))
                {
                    logger.Error("Invalid module name {0}. Check your modules or remove the damaged name.", module);
                    throw new Exception($"Invalid module name {module}. Check your modules or remove the damaged name.");
                }

                if (modules.Contains(moduleEnum))
                {
                    logger.Warning("Duplicated module {0}. Skipping", module);
                    continue;
                }

                logger.Information("Loading module {0}...", moduleName);

                foreach (Type type in types)
                {
                    if (typeof(INetworkMessageHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as INetworkMessageHandler;
                        if (handler != null)
                        {
                            gameNetworkMessageHandlers.Add(handler);
                            logger.Debug("Module {0} registered INetworkMessageHandler", module);
                        }
                    }

                    if (typeof(IServerStartupHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IServerStartupHandler;
                        if (handler != null)
                        {
                            serverStartupHandlers.Add(handler);
                            logger.Debug("Module {0} registered IServerStartupHandler", module);
                        }
                    }

                    if (typeof(IUserSessionHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IUserSessionHandler;
                        if (handler != null)
                        {
                            userSessionHandlers.Add(handler);
                            logger.Debug("Module {0} registered IUserSessionHandler", module);
                        }
                    }

                    if (typeof(IDailyResetHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IDailyResetHandler;
                        if (handler != null)
                        {
                            dayResetHandlers.Add(handler);
                            logger.Debug("Module {0} registered IDailyResetHandler", module);
                        }
                    }

                    if (typeof(IUserDeletedHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IUserDeletedHandler;
                        if (handler != null)
                        {
                            userDeletedHandlers.Add(handler);
                            logger.Debug("Module {0} registered IUserDeletedHandler", module);
                        }
                    }

                    if (typeof(IEventTimer).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IEventTimer;
                        if (handler != null)
                        {
                            eventTimerHandlers.Add(handler);
                            logger.Debug("Module {0} registered IEventTimer", module);
                        }
                    }

                    if (typeof(ITeamActionManager).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as ITeamActionManager;
                        if (handler != null)
                        {
                            teamActionManagerHandlers.Add(handler);
                            logger.Debug("Module {0} registered ITeamActionManager", module);
                        }
                    }

                    if (typeof(ISyndicateActionHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as ISyndicateActionHandler;
                        if (handler != null)
                        {
                            syndicateActionHandlers.Add(handler);
                            logger.Debug("Module {0} registered ISyndicateActionHandler", module);
                        }
                    }

                    if (typeof(IFamilyActionHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IFamilyActionHandler;
                        if (handler != null)
                        {
                            familyActionHandler.Add(handler);
                            logger.Debug("Module {0} registered IFamilyActionHandler", module);
                        }
                    }

                    // IChangeMapHandler
                    if (typeof(IChangeMapHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IChangeMapHandler;
                        if (handler != null)
                        {
                            changeMapHandlers.Add(handler);
                            logger.Debug("Module {0} registered IChangeMapHandler", module);
                        }
                    }

                    if (typeof(IOtherServerTransferHandler).IsAssignableFrom(type))
                    {
                        var handler = Activator.CreateInstance(type) as IOtherServerTransferHandler;
                        if (handler != null)
                        {
                            otherServerTransferHandlers.Add(handler);
                            logger.Debug("Module {0} registered IOtherServerTransfer", module);
                        }
                    }

                    modules.Add(moduleEnum);
                }
            }
        }

        public static async Task OnServerInitializeModulesAsync()
        {
            RegistrationManager = new RegistrationManager();

            foreach (var module in serverStartupHandlers)
            {
                await module.OnServerStartupAsync(); // module with error? crash application
            }
        }

        public static async Task OnServerShutdownModulesAsync()
        {
            foreach (var module in serverStartupHandlers)
            {
                try
                {
                    await module.OnServerShutdownAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnUserLoginAsync(Character user)
        {
            foreach (var module in userSessionHandlers)
            {
                try
                {
                    await module.OnUserLoginAsync(user);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnUserLoginCompleteAsync(Character user)
        {
            foreach (var module in userSessionHandlers)
            {
                try
                {
                    await module.OnUserLoginCompleteAsync(user);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnUserLogoutAsync(Character user)
        {
            foreach (var module in userSessionHandlers)
            {
                try
                {
                    await module.OnUserLogoutAsync(user);
                }
                catch (Exception ex)
                {
                    
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnUserDeletedAsync(Character user, ServerDbContext ctx)
        {
            foreach (var module in userDeletedHandlers)
            {
                try
                {
                    await module.OnUserDeletedAsync(user, ctx);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnTeamCreateAsync(Character user, ITeam team)
        {
            await user.StageGoal.SetProgressAsync(ProcessGoalManager.GoalType.MakeJoinTeam, 1);

            foreach (var module in teamActionManagerHandlers)
            {
                try
                {
                    await module.OnTeamCreateAsync(user, team);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnTeamDismissAsync(ITeam team)
        {
            foreach (var module in teamActionManagerHandlers)
            {
                try
                {
                    await module.OnTeamDismissAsync(team);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnTeamJoinAsync(Character user, ITeam team)
        {
            await user.StageGoal.SetProgressAsync(ProcessGoalManager.GoalType.MakeJoinTeam, 1);

            foreach (var module in teamActionManagerHandlers)
            {
                try
                {
                    await module.OnTeamJoinAsync(user, team);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnTeamExitAsync(Character user, ITeam team)
        {
            foreach (var module in teamActionManagerHandlers)
            {
                try
                {
                    await module.OnTeamExitAsync(user, team);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnFamilyJoinAsync(Character user, IFamily family)
        {
            foreach (var module in familyActionHandler)
            {
                try
                {
                    await module.OnFamilyJoinAsync(user, family);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnFamilyExitAsync(uint userId, IFamily family)
        {
            foreach (var module in familyActionHandler)
            {
                try
                {
                    await module.OnFamilyExitAsync(userId, family);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnSyndicateJoinAsync(Character user, ISyndicate syndicate)
        {
            foreach (var module in syndicateActionHandlers)
            {
                try
                {
                    await module.OnSyndicateJoinAsync(user, syndicate);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnSyndicateExitAsync(uint userId, ISyndicate syndicate)
        {
            foreach (var module in syndicateActionHandlers)
            {
                try
                {
                    await module.OnSyndicateExitAsync(userId, syndicate);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnEnterMapAsync(Character user, GameMap gameMap)
        {
            foreach (var module in changeMapHandlers)
            {
                try
                {
                    await module.OnEnterMapAsync(user, gameMap);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnLeaveMapAsync(Character user, GameMap gameMap)
        {
            foreach (var module in changeMapHandlers)
            {
                try
                {
                    await module.OnLeaveMapAsync(user, gameMap);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        /// <summary>
        /// Runs once every second.
        /// </summary>
        public static async Task OnEventTimerAsync()
        {
            foreach (var module in eventTimerHandlers)
            {
                try
                {
                    await module.OnEventTimerAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnDailyResetAsync()
        {
            foreach (var module in dayResetHandlers)
            {
                try
                {
                    await module.OnDailyResetAsync();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnTransferOSDataAsync(Character user, ulong sessionId, uint serverId)
        {
            foreach (var module in otherServerTransferHandlers)
            {
                try
                {
                    await module.TransferOSDataAsync(user, sessionId, serverId);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnEnterOSAsync(Character user)
        {
            foreach (var module in otherServerTransferHandlers)
            {
                try
                {
                    await module.OnEnterOSAsync(user);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task OnLeaveOSAsync(Character user)
        {
            foreach (var module in otherServerTransferHandlers)
            {
                try
                {
                    await module.OnLeaveOSAsync(user);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }
        }

        public static async Task<bool> OnNetworkMessageReceivedAsync(GameClient actor, PacketType type, byte[] message)
        {
            bool handled = false;
            foreach (var module in gameNetworkMessageHandlers)
            {
                if (await module.OnReceiveAsync(actor, type, message))
                {
                    handled = true;
                }
            }
            return handled;
        }
    }
}
