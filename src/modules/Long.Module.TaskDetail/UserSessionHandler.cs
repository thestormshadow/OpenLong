using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.TaskDetail
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            user.TaskDetail = new States.TaskDetail(user);
            return user.TaskDetail.InitializeAsync();
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
