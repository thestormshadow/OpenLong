using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Family.Managers;
using Serilog;

namespace Long.Module.Family
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        private static readonly ILogger logger = Log.ForContext<ServerStartupHandler>();

        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            try
            {
                ModuleManager.FamilyManager = new FamilyManager();
                await ModuleManager.FamilyManager.InitializeAsync();

                var mgr = new FamilyWarManager();
                await mgr.InitializeAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return false;
            }
        }
    }
}
