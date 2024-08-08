using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Events.States;
using static Long.Kernel.Managers.ModuleManager;

namespace Long.Module.Events
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnServerStartupAsync()
        {
			CTFManager = new Managers.CaptureTheFlagManager();
			return EventManager.RegisterEventAsync(new CaptureTheFlag());
		}
    }
}
