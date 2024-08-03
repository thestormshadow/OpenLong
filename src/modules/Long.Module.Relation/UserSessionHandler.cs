using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Relation.States;

namespace Long.Module.Relation
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            user.Relation = new Relationship(user);
            return user.Relation.InitializeAsync();
        }

        public Task OnUserLoginCompleteAsync(Character user)
        {
            return user.Relation.DoOnlineNotificationAsync();
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return user.Relation.DoOfflineNotificationAsync();
        }
    }
}
