using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.JiangHu
{
    public sealed class OtherServerTransferHandler : IOtherServerTransferHandler
    {
        public Task OnEnterOSAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnLeaveOSAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task TransferOSDataAsync(Character user, ulong sessionId, uint serverId)
        {
            return user.JiangHu.SubmitOSDataAsync(sessionId, serverId);
        }
    }
}
