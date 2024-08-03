namespace Long.Kernel.Modules.Interfaces
{
    public interface IServerStartupHandler
    {
        Task<bool> OnServerStartupAsync();
        Task OnServerShutdownAsync();
    }
}
