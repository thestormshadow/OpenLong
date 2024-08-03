using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Totem.Managers;

namespace Long.Module.Totem
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnServerStartupAsync()
        {
            ModuleManager.TotemManager = new TotemManager();
            return Task.FromResult(true);
        }
    }
}
