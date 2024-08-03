using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Syndicate.Managers;

namespace Long.Module.Syndicate
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnServerStartupAsync()
        {
            ModuleManager.SyndicateManager = new SyndicateManager();
            return ModuleManager.SyndicateManager.InitializeAsync();
        }
    }
}
