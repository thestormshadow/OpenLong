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

        public async Task<bool> OnServerStartupAsync()
        {
			CTFManager = new Managers.CaptureTheFlagManager();
			LSPKManager = new Managers.LineSkillPKManager();
			await EventManager.RegisterEventAsync(new CaptureTheFlag());
			await EventManager.RegisterEventAsync(new LineSkillPK());
			return await Task.FromResult(true);
		}
    }
}
