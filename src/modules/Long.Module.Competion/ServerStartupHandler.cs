using Long.Kernel.Modules.Interfaces;
using Long.Module.Competion.States;

namespace Long.Module.Competion
{
    public sealed class ServerStartupHandler : IServerStartupHandler
    {
        public Task OnServerShutdownAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> OnServerStartupAsync()
        {
            return QuizShowManager.OnServerInitializeAsync();
        }
    }
}
