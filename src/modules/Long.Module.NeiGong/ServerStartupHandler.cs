using Long.Kernel.Modules.Interfaces;

namespace Long.Module.NeiGong
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            NeiGongManager = new Managers.NeiGongManager();
            await NeiGongManager.InitializeAsync();
            return true;
        }
    }
}
