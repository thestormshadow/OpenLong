using Long.Kernel.States.User;
using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Modules.Systems.Family
{
    public interface IFamily
    {
        int AllyCount { get; }
        string Announcement { get; set; }
        byte BattlePowerTower { get; set; }
        uint Challenge { get; set; }
        DateTime CreationDate { get; }
        int EnemyCount { get; }
        uint Identity { get; }
        bool IsDeleted { get; }
        IFamilyMember Leader { get; }
        uint LeaderIdentity { get; }
        int MembersCount { get; }
        ulong Money { get; set; }
        string Name { get; }
        uint Occupy { get; set; }
        int PureMembersCount { get; }
        byte Rank { get; set; }
        uint Reputation { get; set; }
        int SharedBattlePowerFactor { get; }

        Task<bool> AbdicateAsync(Character caller, string targetName);
        Task<bool> AppendMemberAsync(Character caller, Character target, FamilyRank rank = FamilyRank.Member);
        Task<bool> ChangeNameAsync(string name);
        IFamilyMember GetMember(string name);
        IFamilyMember GetMember(uint idMember);
        bool IsAlly(uint idAlly);
        bool IsEnemy(uint idEnemy);
        Task<bool> KickOutAsync(Character caller, uint idTarget);
        Task<bool> LeaveAsync(Character user);
        void LoadRelations();
        Task<bool> SaveAsync();
        Task SendAsync(IPacket msg, uint exclude = 0);
        Task SendAsync(string message, uint idIgnore = 0, Color? color = null);
        Task SendMembersAsync(int idx, Character target);
        Task SendRelationsAsync();
        Task SendRelationsAsync(Character target);
        void SetAlly(IFamily ally);
        void SetEnemy(IFamily enemy);
        Task<bool> SoftDeleteAsync();
        void UnsetAlly(uint idAlly);
        void UnsetEnemy(uint idEnemy);

        Task SendFamilyAsync(Character user);
        Task SendFamilyOccupyAsync(Character user);

        public enum FamilyRank : ushort
        {
            ClanLeader = 100,
            Spouse = 11,
            Member = 10,
            None = 0
        }
    }
}
