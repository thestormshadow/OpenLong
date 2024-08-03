using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Rank.Managers;

namespace Long.Module.Rank
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            ModuleManager.DynamicRankManager = new DynamicRankManager();
            return true;
        }
    }
}
