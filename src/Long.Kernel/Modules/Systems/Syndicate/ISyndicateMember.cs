using Long.Database.Entities;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.Syndicate
{
    public interface ISyndicateMember
    {
        uint ArsenalDonation { get; set; }
        uint AssistantIdentity { get; set; }
        uint ConquerPointsDonation { get; set; }
        uint ConquerPointsTotalDonation { get; set; }
        int Gender { get; }
        uint GuideDonation { get; set; }
        uint GuideTotalDonation { get; set; }
        bool IsOnline { get; }
        DateTime JoinDate { get; }
        DateTime? LastLogout { get; set; }
        byte Level { get; set; }
        uint LookFace { get; }
        uint MasterIdentity { get; set; }
        uint MateIdentity { get; }
        uint Merit { get; set; }
        NobilityRank NobilityRank { get; }
        uint OrchidDonation { get; set; }
        int PkDonation { get; set; }
        int PkTotalDonation { get; set; }
        DateTime? PositionExpiration { get; set; }
        int Profession { get; set; }
        SyndicateRank Rank { get; set; }
        string RankName { get; }
        uint RedRoseDonation { get; set; }
        int Silvers { get; set; }
        ulong SilversTotal { get; set; }
        uint SyndicateIdentity { get; }
        string SyndicateName { get; }
        int TotalDonation { get; }
        uint TulipDonation { get; set; }
        int UsableDonation { get; }
        Character User { get; }
        uint UserIdentity { get; }
        string UserName { get; }
        uint WhiteRoseDonation { get; set; }

        void ChangeName(string newName);
        Task<bool> CreateAsync(Character user, ISyndicate syn, SyndicateRank rank);
        Task<bool> CreateAsync(DbSyndicateAttr attr, ISyndicate syn);
        Task<bool> DeleteAsync();
        Task<bool> SaveAsync();

        public enum SyndicateRank : ushort
        {
            GuildLeader = 1000,

            DeputyLeader = 990,
            HonoraryDeputyLeader = 980,
            LeaderSpouse = 920,

            Manager = 890,
            HonoraryManager = 880,

            TulipSupervisor = 859,
            OrchidSupervisor = 858,
            CpSupervisor = 857,
            ArsenalSupervisor = 856,
            SilverSupervisor = 855,
            GuideSupervisor = 854,
            PkSupervisor = 853,
            RoseSupervisor = 852,
            LilySupervisor = 851,
            Supervisor = 850,
            HonorarySupervisor = 840,

            Steward = 690,
            HonorarySteward = 680,
            DeputySteward = 650,
            DeputyLeaderSpouse = 620,
            DeputyLeaderAide = 611,
            LeaderSpouseAide = 610,
            Aide = 602,

            TulipAgent = 599,
            OrchidAgent = 598,
            CpAgent = 597,
            ArsenalAgent = 596,
            SilverAgent = 595,
            GuideAgent = 594,
            PkAgent = 593,
            RoseAgent = 592,
            LilyAgent = 591,
            Agent = 590,
            SupervisorSpouse = 521,
            ManagerSpouse = 520,
            SupervisorAide = 511,
            ManagerAide = 510,

            TulipFollower = 499,
            OrchidFollower = 498,
            CpFollower = 497,
            ArsenalFollower = 496,
            SilverFollower = 495,
            GuideFollower = 494,
            PkFollower = 493,
            RoseFollower = 492,
            LilyFollower = 491,
            Follower = 490,
            StewardSpouse = 420,

            SeniorMember = 210,
            Member = 200,

            None = 0
        }
    }
}
