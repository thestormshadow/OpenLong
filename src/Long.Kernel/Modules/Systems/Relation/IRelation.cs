using Long.Kernel.Modules.Interfaces;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Modules.Systems.Relation
{
    public interface IRelation : IInitializeSystem
    {
        int FriendAmount { get; }
        int MaximumFriendAmount { get; }
        bool IsFriend(uint idTarget);
        Task<bool> AddFriendAsync(uint idTarget);
        Task SendFriendInfoAsync(uint idTarget);
        Task<bool> DeleteFriendAsync(uint idTarget);
        Task SendToFriendsAsync(IPacket msg);
        Task SendToFriendsAsync(string message, Color? color);

        bool IsEnemy(uint idTarget);
        Task AddEnemyAsync(uint idTarget);
        Task SendEnemyInfoAsync(uint idTarget);
        Task DeleteEnemyAsync(uint idTarget);

        Task SendAllFriendAsync();
        Task SendAllEnemyAsync();
        Task DoOnlineNotificationAsync();
        Task DoOfflineNotificationAsync();
    }
}
