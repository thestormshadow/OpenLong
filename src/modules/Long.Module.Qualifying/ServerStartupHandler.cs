using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Qualifying.States.UserQualifier;

namespace Long.Module.Qualifying
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnServerStartupAsync()
        {
			return EventManager.RegisterEventAsync(new ArenaQualifier());
		}
    }
}
