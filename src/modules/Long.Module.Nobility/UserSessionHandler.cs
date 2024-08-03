using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Peerage.States;

namespace Long.Module.Peerage
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            user.Nobility = new Nobility(user);
            return user.Nobility.InitializeAsync();
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
