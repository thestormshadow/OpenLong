using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;

namespace Long.Module.Syndicate
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnUserLoginCompleteAsync(Character user)
        {
            user.Syndicate = ModuleManager.SyndicateManager.FindByUser(user.Identity);
            await States.Syndicate.SendSyndicateAsync(user);
            if (user.Syndicate != null)
            {
                await user.Syndicate.SendRelationAsync(user);
            }
        }

        public Task OnUserLogoutAsync(Character user)
        {
            return Task.CompletedTask;
        }
    }
}
