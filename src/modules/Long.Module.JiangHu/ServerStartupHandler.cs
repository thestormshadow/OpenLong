using Long.Kernel.Modules.Interfaces;

namespace Long.Module.JiangHu
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            JiangHuManager = new Managers.JiangHuManager();
            await JiangHuManager.InitializeAsync();
            return true;
        }
    }
}
