using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Team
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnUserLogoutAsync(Character user)
        {
            if (user.Team != null)
            {
                if (user.Team.IsLeader(user.Identity))
                {
                    await user.Team.DismissAsync(user, true);
                }
                else
                {
                    await user.Team.LeaveTeamAsync(user);
                }
            }
        }
    }
}
