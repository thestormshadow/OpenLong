using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Booth
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
            if (user.Booth != null)
            {
                await user.Booth.LeaveMapAsync();
                user.Booth = null;
            }
        }
    }
}
