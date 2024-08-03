using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.States.User;

namespace Long.Module.Fate
{
    public sealed class OtherServerTransferHandler : IOtherServerTransferHandler
    {
        public async Task OnEnterOSAsync(Character user)
        {
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Dragon);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Phoenix);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Tiger);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Turtle);
        }

        public async Task OnLeaveOSAsync(Character user)
        {
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Dragon);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Phoenix);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Tiger);
            await DynamicRankManager.SubmitUserFateRankAsync(user, IFate.FateType.Turtle);
        }

        public Task TransferOSDataAsync(Character user, ulong sessionId, uint serverId)
        {
            if (user.Fate == null)
            {
                return Task.CompletedTask;
            }
            return user.Fate.TransferOSDataAsync(sessionId, serverId);
        }
    }
}
