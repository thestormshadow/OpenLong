using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Events
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
			//return QuizShowManager.OnUserLoginAsync(user);
			return Task.CompletedTask;
		}

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
