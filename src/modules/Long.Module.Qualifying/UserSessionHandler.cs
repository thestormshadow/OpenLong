using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Qualifying.Network;
using static Microsoft.VisualStudio.Threading.AsyncReaderWriterLock;

namespace Long.Module.Qualifying
{
    public sealed class UserSessionHandler : IUserSessionHandler
    {
        public Task OnUserLoginAsync(Character user)
        {
            return Task.CompletedTask;
        }

        public async Task OnUserLoginCompleteAsync(Character user)
        {
			await new MsgTeamArenaHeroData().ProcessAsync(user.Client);
		}

        public async Task OnUserLogoutAsync(Character user)
        {
            if (user.Booth != null)
            {
                await user.Booth.LeaveMapAsync();
                user.Booth = null;
            }
        }
    }
}
