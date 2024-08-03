using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Fate
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            user.Fate = new States.Fate(user);
            return user.Fate.InitializeAsync();
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return user.Fate.SaveAsync();
        }
    }
}
