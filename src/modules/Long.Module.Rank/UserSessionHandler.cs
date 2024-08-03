using Long.Kernel.Database;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Rank.Network;

namespace Long.Module.Rank
{
    public sealed class UserSessionHandler : IUserSessionHandler, IUserDeletedHandler
    {
        public Task OnUserDeletedAsync(Character user, ServerDbContext ctx)
        {
            foreach (var userRecord in ctx.DynaRankRecs.Where(x => x.UserId == user.Identity))
            {
                ctx.DynaRankRecs.Remove(userRecord);
            }
            return Task.CompletedTask;
        }

        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnUserLoginCompleteAsync(Character user)
        {
            await MsgRank.SubmitFlowerRankIconAsync(user);
            // TODO add Chi loading rank
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
