using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Qualifying.Network.States;
using Long.Module.Qualifying.States;

namespace Long.Module.Qualifying
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
			await EventManager.RegisterEventAsync(new TeamArenaQualifier());
			return await EventManager.RegisterEventAsync(new ArenaQualifier());
		}
    }
}
