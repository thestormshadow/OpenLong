using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Fate.Managers;

namespace Long.Module.Fate
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return ModuleManager.FateManager.SaveRankAsync();
        }

        public async Task<bool> OnServerStartupAsync()
        {
            ModuleManager.FateManager = new FateManager();
            await ModuleManager.FateManager.InitializeAsync();
            return true;
        }
    }
}
