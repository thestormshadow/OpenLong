using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Pigeon.Managers;

namespace Long.Module.Pigeon
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return PigeonManager.SendToUserAsync(user);
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
