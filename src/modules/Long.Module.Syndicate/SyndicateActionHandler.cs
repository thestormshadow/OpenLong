using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.User;

namespace Long.Module.Syndicate
{
    public sealed class SyndicateActionHandler : ISyndicateActionHandler
    {
        public Task OnSyndicateExitAsync(uint userId, ISyndicate syndicate)
        {
            return Task.CompletedTask;
        }

        public async Task OnSyndicateJoinAsync(Character user, ISyndicate syndicate)
        {
            await user.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Thisisyourhome);
        }
    }
}
