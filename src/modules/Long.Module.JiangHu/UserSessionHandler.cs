using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.JiangHu.States;

namespace Long.Module.JiangHu
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            user.JiangHu = new OwnKongFu(user);
            return user.JiangHu.InitializeAsync();
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return user.JiangHu.LogoutAsync();
        }
    }
}
