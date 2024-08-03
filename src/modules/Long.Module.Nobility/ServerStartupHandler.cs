using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Peerage.Managers;
using Serilog;

namespace Long.Module.Peerage
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        private static readonly ILogger logger = Log.ForContext<ServerStartupHandler>();

        public async Task<bool> OnServerStartupAsync()
        {
            logger.Information("Module nobility is now loading");
            ModuleManager.NobilityManager = new NobilityManager();
            await ModuleManager.NobilityManager.InitializeAsync();
            return true;
        }

        public async Task OnServerShutdownAsync()
        {
            logger.Information("Module nobility is shutting down");
            await ModuleManager.NobilityManager.SaveAsync();
        }
    }
}
