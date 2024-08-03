using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.AstProf
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
            if (user.AstProf == null)
            {
                return Task.CompletedTask;
            }
            return user.AstProf.TransferOSDataAsync(sessionId, serverId);
        }
    }
}
