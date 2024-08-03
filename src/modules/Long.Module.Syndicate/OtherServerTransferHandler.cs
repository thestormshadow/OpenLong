using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Syndicate
{
    public sealed class OtherServerTransferHandler : IOtherServerTransferHandler
    {
        public Task OnEnterOSAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnLeaveOSAsync(Character user)
        {
            if (user.Syndicate != null)
            {
                return user.Syndicate.SendSyndicateToUserAsync(user);
            }
            return Task.CompletedTask;
        }

        public Task TransferOSDataAsync(Character user, ulong sessionId, uint serverId)
        {
            return Task.CompletedTask;
        }
    }
}
