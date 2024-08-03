using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface IOtherServerTransferHandler
    {
        Task TransferOSDataAsync(Character user, ulong sessionId, uint serverId);
        Task OnEnterOSAsync(Character user);
        Task OnLeaveOSAsync(Character user);
    }
}
