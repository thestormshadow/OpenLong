using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.User;
using Long.Shared;
using Long.Shared.Helpers;
using Serilog;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;

namespace Long.Module.Syndicate.States
{
    public sealed class SyndicateMember : ISyndicateMember
    {
        private static readonly ILogger logger = Logger.CreateLogger("syndicate_member");

        private DbSyndicateAttr memberAttributes;

        public uint UserIdentity => memberAttributes.UserIdentity;
        public uint LookFace { get; private set; }
        public int Gender => (int)(LookFace % 10000 / 1000);
        public string UserName { get; private set; }
        public uint MateIdentity { get; private set; }
        public NobilityRank NobilityRank => ModuleManager.NobilityManager.GetRanking(UserIdentity);

        public uint SyndicateIdentity => memberAttributes.SynId;

        public int Silvers
        {
            get => (int)memberAttributes.Proffer;
            set => memberAttributes.Proffer = value;
        }

        public ulong SilversTotal
        {
            get => memberAttributes.ProfferTotal;
            set => memberAttributes.ProfferTotal = value;
        }

        public string SyndicateName { get; private set; }

        public byte Level { get; set; }
        public int Profession { get; set; }

        public SyndicateRank Rank
        {
            get => (SyndicateRank)memberAttributes.Rank;
            set => memberAttributes.Rank = (ushort)value;
        }

        public string RankName
        {
            get
            {
                switch (Rank)
                {
                    case SyndicateRank.GuildLeader:
                        return "Guild Leader";
                    case SyndicateRank.LeaderSpouse:
                        return "Leader Spouse";
                    case SyndicateRank.LeaderSpouseAide:
                        return "Leader Spouse Aide";
                    case SyndicateRank.DeputyLeader:
                        return "Deputy Leader";
                    case SyndicateRank.DeputyLeaderAide:
                        return "Deputy Leader Aide";
                    case SyndicateRank.DeputyLeaderSpouse:
                        return "Deputy Leader Spouse";
                    case SyndicateRank.HonoraryDeputyLeader:
                        return "Honorary Deputy Leader";
                    case SyndicateRank.Manager:
                        return "Manager";
                    case SyndicateRank.ManagerAide:
                        return "Manager Aide";
                    case SyndicateRank.ManagerSpouse:
                        return "Manager Spouse";
                    case SyndicateRank.HonoraryManager:
                        return "Honorary Manager";
                    case SyndicateRank.Supervisor:
                        return "Supervisor";
                    case SyndicateRank.SupervisorAide:
                        return "Supervisor Aide";
                    case SyndicateRank.SupervisorSpouse:
                        return "Supervisor Spouse";
                    case SyndicateRank.TulipSupervisor:
                        return "Tulip Supervisor";
                    case SyndicateRank.ArsenalSupervisor:
                        return "Arsenal Supervisor";
                    case SyndicateRank.CpSupervisor:
                        return "CP Supervisor";
                    case SyndicateRank.GuideSupervisor:
                        return "Guide Supervisor";
                    case SyndicateRank.LilySupervisor:
                        return "Lily Supervisor";
                    case SyndicateRank.OrchidSupervisor:
                        return "Orchid Supervisor";
                    case SyndicateRank.SilverSupervisor:
                        return "Silver Supervisor";
                    case SyndicateRank.RoseSupervisor:
                        return "Rose Supervisor";
                    case SyndicateRank.PkSupervisor:
                        return "PK Supervisor";
                    case SyndicateRank.HonorarySupervisor:
                        return "Honorary Supervisor";
                    case SyndicateRank.Steward:
                        return "Steward";
                    case SyndicateRank.StewardSpouse:
                        return "Steward Spouse";
                    case SyndicateRank.DeputySteward:
                        return "Deputy Steward";
                    case SyndicateRank.HonorarySteward:
                        return "Honorary Steward";
                    case SyndicateRank.Aide:
                        return "Aide";
                    case SyndicateRank.TulipAgent:
                        return "Tulip Agent";
                    case SyndicateRank.OrchidAgent:
                        return "Orchid Agent";
                    case SyndicateRank.CpAgent:
                        return "CP Agent";
                    case SyndicateRank.ArsenalAgent:
                        return "Arsenal Agent";
                    case SyndicateRank.SilverAgent:
                        return "Silver Agent";
                    case SyndicateRank.GuideAgent:
                        return "Guide Agent";
                    case SyndicateRank.PkAgent:
                        return "PK Agent";
                    case SyndicateRank.RoseAgent:
                        return "Rose Agent";
                    case SyndicateRank.LilyAgent:
                        return "Lily Agent";
                    case SyndicateRank.Agent:
                        return "Agent Follower";
                    case SyndicateRank.TulipFollower:
                        return "Tulip Follower";
                    case SyndicateRank.OrchidFollower:
                        return "Orchid Follower";
                    case SyndicateRank.CpFollower:
                        return "CP Follower";
                    case SyndicateRank.ArsenalFollower:
                        return "Arsenal Follower";
                    case SyndicateRank.SilverFollower:
                        return "Silver Follower";
                    case SyndicateRank.GuideFollower:
                        return "Guide Follower";
                    case SyndicateRank.PkFollower:
                        return "PK Follower";
                    case SyndicateRank.RoseFollower:
                        return "Rose Follower";
                    case SyndicateRank.LilyFollower:
                        return "Lily Follower";
                    case SyndicateRank.Follower:
                        return "Follower";
                    case SyndicateRank.SeniorMember:
                        return "Member";
                    case SyndicateRank.Member:
                        return "Member";
                    default:
                        return "Error";
                }
            }
        }

        public DateTime JoinDate => UnixTimestamp.ToDateTime(memberAttributes.JoinDate);

        public Character User => RoleManager.GetUser(UserIdentity);
        public bool IsOnline => User != null;

        public uint ConquerPointsDonation
        {
            get => memberAttributes.Emoney;
            set => memberAttributes.Emoney = value;
        }

        public uint ConquerPointsTotalDonation
        {
            get => memberAttributes.EmoneyTotal;
            set => memberAttributes.EmoneyTotal = value;
        }

        public uint GuideDonation
        {
            get => memberAttributes.Guide;
            set => memberAttributes.Guide = value;
        }

        public uint GuideTotalDonation
        {
            get => memberAttributes.GuideTotal;
            set => memberAttributes.GuideTotal = value;
        }

        public int PkDonation
        {
            get => memberAttributes.Pk;
            set => memberAttributes.Pk = value;
        }

        public int PkTotalDonation
        {
            get => memberAttributes.PkTotal;
            set => memberAttributes.PkTotal = value;
        }

        public uint ArsenalDonation
        {
            get => memberAttributes.Arsenal;
            set => memberAttributes.Arsenal = value;
        }

        public uint RedRoseDonation
        {
            get => memberAttributes.Flower;
            set => memberAttributes.Flower = value;
        }

        public uint WhiteRoseDonation
        {
            get => memberAttributes.WhiteFlower;
            set => memberAttributes.WhiteFlower = value;
        }

        public uint OrchidDonation
        {
            get => memberAttributes.Orchid;
            set => memberAttributes.Orchid = value;
        }

        public uint TulipDonation
        {
            get => memberAttributes.Tulip;
            set => memberAttributes.Tulip = value;
        }

        public uint Merit
        {
            get => memberAttributes.Merit;
            set => memberAttributes.Merit = value;
        }

        public DateTime? LastLogout
        {
            get => UnixTimestamp.ToNullableDateTime((int)memberAttributes.LastLogout);
            set => memberAttributes.LastLogout = (uint)UnixTimestamp.FromDateTime(value);
        }

        public DateTime? PositionExpiration
        {
            get => UnixTimestamp.ToNullableDateTime((int)memberAttributes.Expiration);
            set => memberAttributes.Expiration = (uint)UnixTimestamp.FromDateTime(value);
        }

        public uint MasterIdentity
        {
            get => memberAttributes.MasterId;
            set => memberAttributes.MasterId = value;
        }

        public uint AssistantIdentity
        {
            get => memberAttributes.AssistantIdentity;
            set => memberAttributes.AssistantIdentity = value;
        }

        public int TotalDonation => (int)(Silvers / 10000 + ConquerPointsDonation * 20 + GuideDonation + PkDonation +
                                           ArsenalDonation + RedRoseDonation + WhiteRoseDonation + OrchidDonation +
                                           TulipDonation);

        public int UsableDonation => (int)(Silvers / 10000 + ConquerPointsDonation * 20 + GuideDonation + PkDonation +
                                            ArsenalDonation + RedRoseDonation + WhiteRoseDonation + OrchidDonation +
                                            TulipDonation);

        public async Task<bool> CreateAsync(DbSyndicateAttr attr, ISyndicate syn)
        {
            if (attr == null || syn == null || memberAttributes != null)
            {
                return false;
            }

            memberAttributes = attr;

            DbUser user = await UserRepository.FindByIdentityAsync(attr.UserIdentity);
            if (user == null)
            {
                return false;
            }

            UserName = user.Name;
            LookFace = user.Mesh;
            MateIdentity = user.Mate;
            SyndicateName = syn.Name;
            Level = user.Level;
            Profession = user.Profession;
            return true;
        }

        public async Task<bool> CreateAsync(Character user, ISyndicate syn, SyndicateRank rank)
        {
            if (user == null || syn == null || memberAttributes != null)
            {
                return false;
            }

            memberAttributes = new DbSyndicateAttr
            {
                UserIdentity = user.Identity,
                SynId = syn.Identity,
                Arsenal = 0,
                Emoney = 0,
                EmoneyTotal = 0,
                Merit = 0,
                Guide = 0,
                GuideTotal = 0,
                JoinDate = (uint)UnixTimestamp.Now,
                Pk = 0,
                PkTotal = 0,
                Proffer = 0,
                ProfferTotal = 0,
                Rank = (ushort)rank
            };

            if (!await ServerDbContext.CreateAsync(memberAttributes))
            {
                return false;
            }

            UserName = user.Name;
            LookFace = user.Mesh;
            SyndicateName = syn.Name;
            Level = user.Level;
            MateIdentity = user.MateIdentity;
            Profession = user.Profession;

            logger.Information($"User [{user.Identity}, {user.Name}] has joined [{syn.Identity}, {syn.Name}]");
            return true;
        }

        public void ChangeName(string newName)
        {
            UserName = newName;
        }

        public Task<bool> DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(memberAttributes);
        }

        public Task<bool> SaveAsync()
        {
            return ServerDbContext.UpdateAsync(memberAttributes);
        }
    }
}
