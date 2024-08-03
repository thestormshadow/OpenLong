using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Competion.States;

namespace Long.Module.Competion
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return QuizShowManager.OnUserLoginAsync(user);
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
