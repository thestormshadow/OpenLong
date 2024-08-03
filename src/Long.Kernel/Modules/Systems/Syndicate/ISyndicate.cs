using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Totem;
using Long.Kernel.States.User;
using Long.Network.Packets;
using System.Drawing;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;

namespace Long.Kernel.Modules.Systems.Syndicate
{
    public interface ISyndicate
    {
        const uint HONORARY_DEPUTY_LEADER_PRICE = 6500,
                          HONORARY_MANAGER_PRICE = 3200,
                          HONORARY_SUPERVISOR_PRICE = 2500,
                          HONORARY_STEWARD_PRICE = 1000;

        const int MEMBER_MIN_LEVEL = 15;
        const int MAX_MEMBER_SIZE = 800;
        const int DISBAND_MONEY = 100000;
        const int SYNDICATE_ACTION_COST = 50000;
        const int EXIT_MONEY = 20000;
        const string DEFAULT_ANNOUNCEMENT_S = "This is a new guild.";
        const int MAX_ALLY = 5;
        const int MAX_ENEMY = 5;

        ushort Identity { get; }
        string Name { get; }
        int MemberCount { get; }

        long Money { get; set; }
        byte Level { get; }
        uint ConquerPoints { get; set; }
        string Announce { get; set; }
        DateTime AnnounceDate { get; set; }
        bool Deleted { get; }
        ISyndicateMember Leader { get; }

        int DeputyLeaderCount { get; }
        int HonoraryDeputyLeaderCount { get; }
        int HonoraryManagerCount { get; }
        int HonorarySupervisorCount { get; }
        int HonoraryStewardCount { get; }

        Task<bool> CreateAsync(DbSyndicate syn);
        Task<bool> CreateAsync(string name, int investment, Character leader);
        void LoadRelation();

        Task<bool> DisbandAsync(Character user);
        Task<bool> AppendMemberAsync(Character target, Character caller, JoinMode mode);
        Task<bool> QuitSyndicateAsync(Character target);
        Task<bool> KickOutMemberAsync(Character sender, string name);
        List<ISyndicateMember> GetMembers();

        Task<bool> PromoteAsync(Character sender, string target, SyndicateRank position);
        Task<bool> PromoteAsync(Character sender, uint target, SyndicateRank position);
        Task<bool> PromoteAsync(Character sender, Character target, SyndicateRank position);
        Task<bool> DemoteAsync(Character sender, string name);
        Task<bool> DemoteAsync(Character sender, uint target);
        Task<bool> DemoteAsync(Character sender, ISyndicateMember member);

        Task SendPromotionListAsync(Character target);
        uint MaxPositionAmount(SyndicateRank pos);

        int AlliesCount { get; }
        Task<bool> CreateAllianceAsync(Character user, ISyndicate target);
        Task<bool> DisbandAllianceAsync(Character user, ISyndicate target);
        Task AddAllyAsync(ISyndicate target);
        Task RemoveAllyAsync(uint idAlly);
        bool IsAlly(uint id);
        bool UserIsAlly(uint idUser);
        ushort MaxAllies();

        int EnemyCount { get; }
        Task<bool> AntagonizeAsync(Character user, ISyndicate target);
        Task<bool> PeaceAsync(Character user, ISyndicate target);
        bool IsEnemy(uint id);
        ushort MaxEnemies();

        bool AllowTrojan { get; }
        bool AllowWarrior { get; }
        bool AllowArcher { get; }
        bool AllowTaoist { get; }
        bool AllowNinja { get; }
        bool AllowMonk { get; }
        bool AllowPirate { get; }
        bool AllowDragonWarrior { get; }
        ProfessionPermission ProfessionRequirement { get; set; }
        byte LevelRequirement { get; set; }
        byte MetempsychosisRequirement { get; set; }

        ISyndicateMember QueryMember(uint id);
        ISyndicateMember QueryMember(string member);
        Task<ISyndicateMember> QueryRandomMemberAsync();
        List<ISyndicateMember> QueryRank(int type);

        ITotem Totem { get; set; }
        ITotem.TotemPoleFlag TotemPole { get; set; }
        int GetSharedBattlePower(SyndicateRank rank);

        Task SendMembersAsync(int page, Character target);
        Task SendMinContributionAsync(Character target);
        Task SendRelationAsync(Character target);
        Task BroadcastNameAsync();
        Task SendAsync(Character user);
        Task SendAsync(string message, uint idIgnore = 0u, Color? color = null);
        Task SendAsync(IPacket msg, uint exclude = 0u);
        Task SendAsync(byte[] msg, uint exclude = 0u);
        Task BroadcastToAlliesAsync(IPacket msg);

        Task<bool> SaveAsync();
        Task<bool> SoftDeleteAsync();
        Task<bool> DeleteAsync();
        Task SynchronizeBattlePowerAsync();
        Task SendSyndicateToUserAsync(Character user);
        Task<bool> ChangeNameAsync(string name);

        uint LeagueId { get; set; }

        public enum JoinMode
        {
            Invite,
            Request,
            Recruitment
        }

        [Flags]
        public enum ProfessionPermission
        {
            Trojan = 1,
            Warrior = 2,
            Taoist = 4,
            Archer = 8,
            Ninja = 16,
            Monk = 32,
            Pirate = 64,
            DragonWarrior = 128,

            All = Trojan | Warrior | Taoist | Archer | Ninja | Monk | Pirate | DragonWarrior
        }
    }
}
