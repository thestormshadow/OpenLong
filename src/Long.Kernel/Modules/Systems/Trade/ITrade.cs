using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Trade
{
    public interface ITrade
    {
        bool ContainsItem(uint idItem);
        Task<bool> AddItemAsync(uint idItem, Character sender);
        Task<bool> AddMoneyAsync(uint amount, Character sender);
        Task<bool> AddEmoneyAsync(uint amount, Character sender);
        Task AcceptAsync(uint acceptId, bool skipSuspiciousCheck);
        Task SendCloseAsync();
    }
}
