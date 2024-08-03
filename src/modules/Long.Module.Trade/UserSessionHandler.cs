using Long.Kernel.Database;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.States.User;
using Long.Module.Trade.States;

namespace Long.Module.Trade
{
    public sealed class UserSessionHandler : IUserSessionHandler, IUserDeletedHandler
    {
        public Task OnUserDeletedAsync(Character user, ServerDbContext ctx)
        {
            return user.TradePartnerRelation.OnUserDeleteAsync(ctx);
        }

        public async Task OnUserLoginAsync(Character user)
        {
            user.TradePartnerRelation = new TradePartnerRelation(user);
            await user.TradePartnerRelation.InitializeAsync();
        }

        public async Task OnUserLoginCompleteAsync(Character user)
        {
            await user.TradePartnerRelation.NotifyAsync();
        }

        public async Task OnUserLogoutAsync(Character user)
        {
            if (user.Trade != null)
            {
                await user.Trade.SendCloseAsync();
            }
            await user.TradePartnerRelation.NotifyOfflineAsync();
        }
    }
}
