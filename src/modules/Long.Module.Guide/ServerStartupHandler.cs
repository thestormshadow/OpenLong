using Long.Kernel.Modules.Interfaces;
using Long.Module.Guide.Managers;

namespace Long.Module.Guide
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            await GuideManager.InitializeAsync();
            return true;
        }
    }
}
