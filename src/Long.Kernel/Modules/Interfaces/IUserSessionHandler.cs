using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface IUserSessionHandler
    {
        Task OnUserLoginAsync(Character user);
        Task OnUserLogoutAsync(Character user);
        Task OnUserLoginCompleteAsync(Character user);
    }
}
