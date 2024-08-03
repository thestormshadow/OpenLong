using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.NeiGong.States;

namespace Long.Module.NeiGong
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            user.InnerStrength = new InnerStrength(user);
            return user.InnerStrength.InitializeAsync();
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
