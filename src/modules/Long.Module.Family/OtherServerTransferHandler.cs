using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Family
{
    public sealed class OtherServerTransferHandler : IOtherServerTransferHandler
    {
        public Task OnEnterOSAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnLeaveOSAsync(Character user)
        {
            if (user.Family != null)
            {
                await user.Family.SendFamilyAsync(user);
                await user.Family.SendRelationsAsync(user);
            }
        }

        public Task TransferOSDataAsync(Character user, ulong sessionId, uint serverId)
        {
            return Task.CompletedTask;
        }
    }
}
