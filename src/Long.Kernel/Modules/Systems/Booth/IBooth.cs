using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Booth
{
    public interface IBooth
    {
        uint Identity { get; }
        ushort Type { get; }
        string HawkMessage { get; set; }

        Task<bool> InitializeAsync();
        Task QueryItemsAsync(Character requester);
        bool AddItem(Item item, uint value, MsgItem.Moneytype type);
        Task<bool> SellBoothItemAsync(uint idItem, Character target);
        bool HasItem(uint idItem);
        bool RemoveItem(uint idItem);
        bool ValidateItem(uint id);
        Task EnterMapAsync();
        Task LeaveMapAsync();
        Task SendSpawnToAsync(Character player);
    }
}
