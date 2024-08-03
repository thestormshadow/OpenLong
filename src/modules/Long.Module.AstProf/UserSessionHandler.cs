using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.AstProf.States;

namespace Long.Module.AstProf
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            user.AstProf = new AssistantProfession(user);
            return user.AstProf.InitializeAsync();
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
