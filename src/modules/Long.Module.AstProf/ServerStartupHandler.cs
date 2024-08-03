using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.AstProf.States;

namespace Long.Module.AstProf
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<bool> OnServerStartupAsync()
        {
            ModuleManager.AstProfManager = new AssistantProfessionManager();
            await ModuleManager.AstProfManager.InitializeAsync();
            return true;
        }
    }
}
