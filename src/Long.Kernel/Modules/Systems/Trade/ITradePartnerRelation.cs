using Long.Kernel.Database;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Trade
{
    public interface ITradePartnerRelation
    {
        int Amount { get; }
        Task<bool> InitializeAsync();
        Task CreateRelationAsync(Character target);
        Task DeleteRelationAsync(uint targetId);
        bool IsTradePartner(uint userId);
        Task NotifyAsync();
        bool IsValidTradePartner(uint userId);
        Task SendInfoAsync(uint targetId);
        Task NotifyOfflineAsync();
        Task OnUserDeleteAsync(ServerDbContext ctx);
    }
}