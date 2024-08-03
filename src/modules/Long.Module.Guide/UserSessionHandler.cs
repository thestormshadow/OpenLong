using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Guide
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            user.Guide = new States.Guide(user);
            return user.Guide.InitializeAsync();
        }

        public Task OnUserLogoutAsync(Character user)
        {
            if (user.Guide != null)            
				return user.Guide.OnLogoutAsync();			
            else
                return Task.CompletedTask;
        }
    }
}
