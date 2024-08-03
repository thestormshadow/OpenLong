using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Syndicate.Network;
using Long.Module.Syndicate.Repositories;
using Long.Network.Packets;
using Long.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Drawing;
using System.Globalization;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicate;
using static Long.Kernel.Network.Game.Packets.MsgName;
using static Long.Kernel.StrRes;
using static Long.Kernel.Service.RandomService;
using static Long.Module.Syndicate.Network.MsgFactionRankInfo;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;
using Long.Kernel.Modules.Systems.Totem;

namespace Long.Module.Syndicate.States
{
    public sealed class Syndicate : ISyndicate
    {
        #region Private Getters

        private int MaxDeputyLeader => Level < 4 ? 2 : Level < 7 ? 3 : 4;

        private int MaxHonoraryDeputyLeader => Level < 6 ? 1 : 2;

        private int MaxHonoraryManager => Level < 5 ? 1 : Level < 7 ? 2 : Level < 9 ? 4 : 6;

        private int MaxHonorarySupervisor => Level < 5 ? 1 : Level < 7 ? 2 : Level < 9 ? 4 : 6;

        private int MaxHonorarySteward => Level < 3 ? 1 : Level < 5 ? 2 : Level < 7 ? 4 : Level < 9 ? 6 : 8;

        private int MaxAide => Level < 4 ? 2 : Level < 6 ? 4 : 6;

        private int MaxManager
        {
            get
            {
                switch (Level)
                {
                    case 1:
                    case 2: return 1;
                    case 3:
                    case 4: return 2;
                    case 5:
                    case 6: return 4;
                    case 7:
                    case 8: return 6;
                    case 9: return 8;
                    default: return 0;
                }
            }
        }

        private int MaxSupervisor => Level < 4 ? 0 : Level < 8 ? 1 : 2;

        private int MaxSteward
        {
            get
            {
                switch (Level)
                {
                    case 0:
                    case 1: return 0;
                    case 2: return 1;
                    case 3: return 2;
                    case 4: return 3;
                    case 5: return 4;
                    case 6: return 5;
                    case 7: return 6;
                    case 8:
                    case 9: return 8;
                    default: return 0;
                }
            }
        }

        #endregion

        public const uint HONORARY_DEPUTY_LEADER_PRICE = 6500,
                          HONORARY_MANAGER_PRICE = 3200,
                          HONORARY_SUPERVISOR_PRICE = 2500,
                          HONORARY_STEWARD_PRICE = 1000;

        private const int MEMBER_MIN_LEVEL = 15;
        private const int MAX_MEMBER_SIZE = 800;
        private const int DISBAND_MONEY = 100000;
        public const int SYNDICATE_ACTION_COST = 50000;
        private const int EXIT_MONEY = 20000;
        private const string DEFAULT_ANNOUNCEMENT_S = "This is a new guild.";
        
        private DbSyndicate syndicate;
        private ISyndicateMember leader;
        private string name;
        private readonly ConcurrentDictionary<uint, ISyndicateMember> members = new();
        private readonly ConcurrentDictionary<ushort, ISyndicate> allies = new();
        private readonly ConcurrentDictionary<ushort, ISyndicate> enemies = new();

        public ushort Identity => (ushort)syndicate.Identity;
        public string Name => name;
        public int MemberCount => members.Count;

        public long Money
        {
            get => syndicate.Money;
            set => syndicate.Money = value;
        }

        public byte Level 
        {
            get => syndicate.SynRank;
            private set => syndicate.SynRank = value;
        }

        public uint ConquerPoints
        {
            get => syndicate.Emoney;
            set => syndicate.Emoney = value;
        }

        public string Announce
        {
            get => syndicate.Announce;
            set => syndicate.Announce = value;
        }

        public DateTime AnnounceDate
        {
            get => DateTime.ParseExact(syndicate.PublishTime.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            set => syndicate.PublishTime = uint.Parse(value.ToString("yyyyMMdd"));
        }

        public bool Deleted => syndicate.DelFlag != 0;

        public ISyndicateMember Leader => leader;

        #region Position Count

        public int DeputyLeaderCount => members.Values.Count(x => x.Rank == SyndicateRank.DeputyLeader);
        public int HonoraryDeputyLeaderCount => members.Values.Count(x => x.Rank == SyndicateRank.HonoraryDeputyLeader);
        public int HonoraryManagerCount => members.Values.Count(x => x.Rank == SyndicateRank.HonoraryManager);
        public int HonorarySupervisorCount => members.Values.Count(x => x.Rank == SyndicateRank.HonorarySupervisor);
        public int HonoraryStewardCount => members.Values.Count(x => x.Rank == SyndicateRank.HonorarySteward);

        #endregion

        #region Create

        public async Task<bool> CreateAsync(DbSyndicate syn)
        {
            syndicate = syn;
            name = syn.Name;

            if (Deleted)
            {
                return true;
            }

            List<DbSyndicateAttr> tempMembers = await SyndicateAttrRepository.GetAsync(Identity);
            foreach (DbSyndicateAttr dbMember in tempMembers)
            {
                var member = new SyndicateMember();
                if (!await member.CreateAsync(dbMember, this))
                {
                    continue;
                }

                if (members.TryAdd(member.UserIdentity, member))
                {
                    if (member.Rank == SyndicateRank.GuildLeader)
                    {
                        leader = member;
                    }
                }
            }

            if (MemberCount == 0)
            {
                syndicate.DelFlag = 1;
                return true;
            }

            if (leader == null)
            {
                leader = members.Values.OrderByDescending(x => x.TotalDonation).FirstOrDefault();
                leader.Rank = SyndicateRank.GuildLeader;
                await leader.SaveAsync();
            }

            Level = 1;

            if (ModuleManager.TotemManager != null)
            {
                await ModuleManager.TotemManager.InitializeAsync(this);
                SetLevel();
            }

            foreach (var member in members.Values.Where(x => !IsUserSetPosition(x.Rank)))
            {
                if (Leader.MateIdentity == member.UserIdentity
                    && member.Rank != SyndicateRank.LeaderSpouse)
                {
                    member.Rank = SyndicateRank.LeaderSpouse;
                    await member.SaveAsync();
                    continue;
                }

                member.Rank = SyndicateRank.Member;
            }

            uint amount = 0;
            uint maxAmount = MaxPositionAmount(SyndicateRank.Manager);

            #region Manager

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member)
                                               .OrderByDescending(x => x.UsableDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.Manager;
                await member.SaveAsync();
            }

            #endregion

            #region Rose Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.RoseSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.RedRoseDonation > 0)
                                               .OrderByDescending(x => x.RedRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.RoseSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region White Rose Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.LilySupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.WhiteRoseDonation > 0)
                                               .OrderByDescending(x => x.WhiteRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.LilySupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Orchid Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.OrchidSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.OrchidDonation > 0)
                                               .OrderByDescending(x => x.OrchidDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.OrchidSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Tulip Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.TulipSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.TulipDonation > 0)
                                               .OrderByDescending(x => x.TulipDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.TulipSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Pk Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.PkSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.PkDonation > 0)
                                               .OrderByDescending(x => x.PkDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.PkSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Guide Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.GuideSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.GuideDonation > 0)
                                               .OrderByDescending(x => x.GuideDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.GuideSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Silver Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.SilverSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Silvers > 0)
                                               .OrderByDescending(x => x.Silvers))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.SilverSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region CPs Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.CpSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ConquerPointsDonation > 0)
                                               .OrderByDescending(x => x.ConquerPointsDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.CpSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Arsenal Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.ArsenalSupervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ArsenalDonation > 0)
                                               .OrderByDescending(x => x.ArsenalDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.ArsenalSupervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Supervisor

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.Supervisor);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.TotalDonation > 0)
                                               .OrderByDescending(x => x.UsableDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.Supervisor;
                await member.SaveAsync();
            }

            #endregion

            #region Steward

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.Steward);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.UsableDonation > 0)
                                               .OrderByDescending(x => x.UsableDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.Steward;
                await member.SaveAsync();
            }

            #endregion

            #region Deputy Steward

            foreach (var member in this.members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member &&
                                                           x.UsableDonation >= 170000))
            {
                member.Rank = SyndicateRank.DeputySteward;
                await member.SaveAsync();
            }

            #endregion

            #region Rose Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.RoseAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.RedRoseDonation > 0)
                                               .OrderByDescending(x => x.RedRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.RoseAgent;
                await member.SaveAsync();
            }

            #endregion

            #region White Rose Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.LilyAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.WhiteRoseDonation > 0)
                                               .OrderByDescending(x => x.WhiteRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.LilyAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Orchid Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.OrchidAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.OrchidDonation > 0)
                                               .OrderByDescending(x => x.OrchidDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.OrchidAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Tulip Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.TulipAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.TulipDonation > 0)
                                               .OrderByDescending(x => x.TulipDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.TulipAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Pk Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.PkAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.PkDonation > 0)
                                               .OrderByDescending(x => x.PkDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.PkAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Guide Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.GuideAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.GuideDonation > 0)
                                               .OrderByDescending(x => x.GuideDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.GuideAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Silver Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.SilverAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Silvers > 0)
                                               .OrderByDescending(x => x.Silvers))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.SilverAgent;
                await member.SaveAsync();
            }

            #endregion

            #region CPs Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.CpAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ConquerPointsDonation > 0)
                                               .OrderByDescending(x => x.ConquerPointsDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.CpAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Arsenal Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.ArsenalAgent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ArsenalDonation > 0)
                                               .OrderByDescending(x => x.ArsenalDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.ArsenalAgent;
                await member.SaveAsync();
            }

            #endregion

            #region Agent

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.Agent);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.UsableDonation > 0)
                                               .OrderByDescending(x => x.UsableDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.Agent;
                await member.SaveAsync();
            }

            #endregion

            #region Rose Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.RoseFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.RedRoseDonation > 0)
                                               .OrderByDescending(x => x.RedRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.RoseFollower;
                await member.SaveAsync();
            }

            #endregion

            #region White Rose Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.LilyFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.WhiteRoseDonation > 0)
                                               .OrderByDescending(x => x.WhiteRoseDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.LilyFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Orchid Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.OrchidFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.OrchidDonation > 0)
                                               .OrderByDescending(x => x.OrchidDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.OrchidFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Tulip Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.TulipFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Gender == 2 && x.TulipDonation > 0)
                                               .OrderByDescending(x => x.TulipDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.TulipFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Pk Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.PkFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.PkDonation > 0)
                                               .OrderByDescending(x => x.PkDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.PkFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Guide Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.GuideFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.GuideDonation > 0)
                                               .OrderByDescending(x => x.GuideDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.GuideFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Silver Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.SilverFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.Silvers > 0)
                                               .OrderByDescending(x => x.Silvers))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.SilverFollower;
                await member.SaveAsync();
            }

            #endregion

            #region CPs Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.CpFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ConquerPointsDonation > 0)
                                               .OrderByDescending(x => x.ConquerPointsDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.CpFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Arsenal Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.ArsenalFollower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.ArsenalDonation > 0)
                                               .OrderByDescending(x => x.ArsenalDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.ArsenalFollower;
                await member.SaveAsync();
            }

            #endregion

            #region Follower

            amount = 0;
            maxAmount = MaxPositionAmount(SyndicateRank.Follower);

            foreach (var member in members
                                               .Values
                                               .Where(x => x.Rank == SyndicateRank.Member && x.UsableDonation > 0)
                                               .OrderByDescending(x => x.UsableDonation))
            {
                if (amount++ >= maxAmount)
                {
                    break;
                }

                member.Rank = SyndicateRank.Follower;
                await member.SaveAsync();
            }

            #endregion

            await SaveAsync();
            return true;
        }

        public async Task<bool> CreateAsync(string name, int investment, Character leader)
        {
            if (ModuleManager.SyndicateManager.GetSyndicate(name) != null)
            {
                await leader.SendAsync(StrSynNameInUse);
                return false;
            }

            syndicate = new DbSyndicate
            {
                Announce = DEFAULT_ANNOUNCEMENT_S,
                PublishTime = uint.Parse(DateTime.Now.ToString("yyyyMMdd")),
                Emoney = 0,
                LeaderId = leader.Identity,
                LeaderTitle = SyndicateRank.GuildLeader.ToString(),
                Money = investment / 2,
                Name = name,
                ConditionProf = 0,
                ConditionMetem = 0,
                ConditionLevel = 1
            };

            if (!await SaveAsync())
            {
                return false;
            }

            Level = 1;

            this.name = name;
            this.leader = new SyndicateMember();
            if (!await this.leader.CreateAsync(leader, this, SyndicateRank.GuildLeader))
            {
                await DeleteAsync();
                return false;
            }

            this.leader.Silvers = investment / 2;
            await this.leader.SaveAsync();

            members.TryAdd(this.leader.UserIdentity, this.leader);

            if (ModuleManager.TotemManager != null)
            {
                await ModuleManager.TotemManager.CreateAsync(this);
            }

            await leader.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Imtheking);
            return true;
        }

        public void LoadRelation()
        {
            AppendAlly(syndicate.Ally0);
            AppendAlly(syndicate.Ally1);
            AppendAlly(syndicate.Ally2);
            AppendAlly(syndicate.Ally3);
            AppendAlly(syndicate.Ally4);
            AppendAlly(syndicate.Ally5);
            AppendAlly(syndicate.Ally6);
            AppendAlly(syndicate.Ally7);
            AppendAlly(syndicate.Ally8);
            AppendAlly(syndicate.Ally9);
            AppendAlly(syndicate.Ally10);
            AppendAlly(syndicate.Ally11);
            AppendAlly(syndicate.Ally12);
            AppendAlly(syndicate.Ally13);
            AppendAlly(syndicate.Ally14);

            AppendEnemy(syndicate.Enemy0);
            AppendEnemy(syndicate.Enemy1);
            AppendEnemy(syndicate.Enemy2);
            AppendEnemy(syndicate.Enemy3);
            AppendEnemy(syndicate.Enemy4);
            AppendEnemy(syndicate.Enemy5);
            AppendEnemy(syndicate.Enemy6);
            AppendEnemy(syndicate.Enemy7);
            AppendEnemy(syndicate.Enemy8);
            AppendEnemy(syndicate.Enemy9);
            AppendEnemy(syndicate.Enemy10);
            AppendEnemy(syndicate.Enemy11);
            AppendEnemy(syndicate.Enemy12);
            AppendEnemy(syndicate.Enemy13);
            AppendEnemy(syndicate.Enemy14);
        }

        private void AppendAlly(uint idAlly)
        {
            if (idAlly == 0)
            {
                return;
            }

            ISyndicate syndicate = ModuleManager.SyndicateManager.GetSyndicate((int)idAlly);
            if (syndicate == null || syndicate.Deleted)
            {
                // invalid ally
                UnSetAlly(idAlly);
                return;
            }

            allies.TryAdd(syndicate.Identity, syndicate);
        }

        private void AppendEnemy(uint idEnemy)
        {
            if (idEnemy == 0)
            {
                return;
            }

            ISyndicate syndicate = ModuleManager.SyndicateManager.GetSyndicate((int)idEnemy);
            if (syndicate == null || syndicate.Deleted)
            {
                UnSetEnemy(idEnemy);
                return;
            }

            enemies.TryAdd(syndicate.Identity, syndicate);
        }

        #endregion

        #region Change Name

        public Task<bool> ChangeNameAsync(string name)
        {
            syndicate.Name = name;
            return SaveAsync();
        }

        #endregion

        #region Disband

        public async Task<bool> DisbandAsync(Character user)
        {
            if (user.Identity != Leader.UserIdentity)
            {
                return false;
            }

            if (MemberCount > 1)
            {
                return false;
            }

            if (Money < DISBAND_MONEY)
            {
                return false;
            }

            if (LeagueId != 0)
            {
                await user.SendAsync(StrSynInLeague);
                return false;
            }

            user.Syndicate = null;

            if (members.TryRemove(user.Identity, out var member))
            {
                await member.DeleteAsync();
            }

            // additional clean up
            await ServerDbContext.ScalarAsync($"DELETE FROM `cq_synattr` WHERE `syn_id`={Identity}");

            foreach (Syndicate ally in allies.Values)
            {
                await RemoveAllyAsync(ally.Identity);
            }

            foreach (Syndicate enemy in enemies.Values)
            {
                await PeaceAsync(user, enemy);
            }

            await user.SendAsync(new MsgSyndicate
            {
                Identity = Identity,
                Mode = MsgSyndicate.SyndicateRequest.Disband
            });

            await user.Screen.SynchroScreenAsync();
            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynDestroy, Name), Kernel.Network.Game.Packets.TalkChannel.Talk, Color.White);
            return await SoftDeleteAsync();
        }

        #endregion

        #region Member Management

        public List<ISyndicateMember> GetMembers()
        {
            return members.Values.ToList();
        }

        public async Task<bool> AppendMemberAsync(Character target, Character caller, JoinMode mode)
        {
            if ((mode == JoinMode.Invite || mode == JoinMode.Request) && caller == null)
            {
                return false;
            }

            if (target.SyndicateIdentity != 0)
            {
                return false;
            }

            if (target.Level < MEMBER_MIN_LEVEL)
            {
                return false;
            }

            if (MemberCount >= MAX_MEMBER_SIZE)
            {
                return false;
            }

            if (mode != JoinMode.Recruitment)
            {
                if (target.Level < LevelRequirement)
                {
                    return false;
                }

                if (target.Metempsychosis < MetempsychosisRequirement)
                {
                    return false;
                }

                switch (target.ProfessionSort)
                {
                    case 10:
                        {
                            if (!AllowTrojan)
                            {
                                return false;
                            }

                            break;
                        }
                    case 20:
                        {
                            if (!AllowWarrior)
                            {
                                return false;
                            }

                            break;
                        }
                    case 40:
                        {
                            if (!AllowArcher)
                            {
                                return false;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (!AllowNinja)
                            {
                                return false;
                            }
                            break;
                        }
                    case 60:
                        {
                            if (!AllowMonk)
                            {
                                return false;
                            }
                            break;
                        }
                    case 70:
                        {
                            if (!AllowPirate)
                            {
                                return false;
                            }
                            break;
                        }
                    case 100:
                        {
                            if (!AllowTaoist)
                            {
                                return false;
                            }
                            break;
                        }
                }
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await (caller ?? target).SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            if (target.Union != null 
                && target.Union.Identity != LeagueId)
            {
                return false;
            }

            var newMember = new SyndicateMember();
            if (!await newMember.CreateAsync(target, this, SyndicateRank.Member))
            {
                return false;
            }

            if (!members.TryAdd(newMember.UserIdentity, newMember))
            {
                await newMember.DeleteAsync();
                return false;
            }

            target.Syndicate = this;

            await SendSyndicateAsync(target);
            await SendAsync(target);
            await SendRelationAsync(target);
            await target.Screen.SynchroScreenAsync();

            await ModuleManager.OnSyndicateJoinAsync(target, this);

            await SaveAsync();

            switch (mode)
            {
                case JoinMode.Invite:
                    await SendAsync(string.Format(StrSynInviteGuild, caller.SyndicateMember.RankName,
                                                  caller.Name, target.Name));
                    break;
                case JoinMode.Request:
                    await SendAsync(string.Format(StrSynJoinGuild, caller.SyndicateMember.RankName,
                                                  caller.Name, target.Name));
                    break;
                case JoinMode.Recruitment:
                    await SendAsync(string.Format(StrSynRecruitmentJoin, target.Name));
                    break;
            }

            if (members.TryGetValue(target.MateIdentity, out var mate))
            {
                SyndicateRank matePos = GetSpousePosition(mate.Rank);
                if (matePos != SyndicateRank.None && matePos > mate.Rank
                    && IsSystemDefinedPosition(mate.Rank))
                {
                    await SetSpouseAsync(target.SyndicateMember, matePos);
                }
            }
            return true;
        }

        public async Task<bool> QuitSyndicateAsync(Character target)
        {
            if (target.SyndicateRank == SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (!members.TryGetValue(target.Identity, out var member))
            {
                return false;
            }

            if (member.Silvers < EXIT_MONEY)
            {
                await target.SendAsync(string.Format(StrSynExitNotEnoughMoney, EXIT_MONEY));
                return false;
            }

            members.TryRemove(target.Identity, out _);

            target.Syndicate = null;

            await target.SendAsync(new MsgSyndicate
            {
                Identity = Identity,
                Mode = MsgSyndicate.SyndicateRequest.Disband
            });

            await SaveAsync();

            await target.Screen.SynchroScreenAsync();

            await ModuleManager.OnSyndicateExitAsync(target.Identity, this);

            await ServerDbContext.CreateAsync(new DbSyndicateMemberHistory
            {
                UserIdentity = member.UserIdentity,
                JoinDate = member.JoinDate,
                LeaveDate = DateTime.Now,
                SyndicateIdentity = Identity,
                Rank = (ushort)member.Rank,
                Silver = member.Silvers,
                ConquerPoints = 0,
                Guide = 0,
                PkPoints = 0
            });

            await member.DeleteAsync();
            await SendAsync(string.Format(StrSynMemberExit, target.Name));

            if (members.TryGetValue(member.MateIdentity, out var mate))
            {
                if (IsSpousePosition(mate.Rank))
                {
                    await SetSpouseAsync(mate, SyndicateRank.Member);
                }
            }

            return true;
        }

        public async Task<bool> KickOutMemberAsync(Character sender, string name)
        {
            if (sender.SyndicateRank < SyndicateRank.DeputyLeader)
            {
                return false;
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await sender.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            ISyndicateMember member = QueryMember(name);
            if (member == null)
            {
                return false;
            }

            if (member.Rank == SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (!members.TryRemove(member.UserIdentity, out _))
            {
                return false;
            }

            Character target = member.User;
            if (target != null)
            {
                target.Syndicate = null;
                await target.SendAsync(new MsgSyndicate
                {
                    Identity = Identity,
                    Mode = MsgSyndicate.SyndicateRequest.Disband
                });
                await target.Screen.SynchroScreenAsync();
                await target.SendAsync(string.Format(StrSynYouBeenKicked, sender.Name));
            }

            await ModuleManager.OnSyndicateExitAsync(member.UserIdentity, this);

            await ServerDbContext.CreateAsync(new DbSyndicateMemberHistory
            {
                UserIdentity = member.UserIdentity,
                JoinDate = member.JoinDate,
                LeaveDate = DateTime.Now,
                SyndicateIdentity = Identity,
                Rank = (ushort)member.Rank,
                Silver = member.Silvers,
                ConquerPoints = 0,
                Guide = 0,
                PkPoints = 0
            });

            await SaveAsync();
            await member.DeleteAsync();
            await SendAsync(string.Format(StrSynMemberKickout, sender.SyndicateMember.RankName, sender.Name,
                                          member.UserName));

            if (members.TryGetValue(member.MateIdentity, out var mate))
            {
                if (IsSpousePosition(mate.Rank))
                {
                    await SetSpouseAsync(mate, SyndicateRank.Member);
                }
            }

            return true;
        }

        #endregion

        #region Promote and Demote

        public Task<bool> PromoteAsync(Character sender, string target, SyndicateRank position)
        {
            Character user = RoleManager.GetUser(target);
            if (user == null || user.SyndicateIdentity != sender.SyndicateIdentity)
            {
                return Task.FromResult(false);
            }

            return PromoteAsync(sender, user, position);
        }

        public Task<bool> PromoteAsync(Character sender, uint target, SyndicateRank position)
        {
            Character user = RoleManager.GetUser(target);
            if (user == null || user.SyndicateIdentity != sender.SyndicateIdentity)
            {
                return Task.FromResult(false);
            }

            return PromoteAsync(sender, user, position);
        }

        public async Task<bool> PromoteAsync(Character sender, Character target, SyndicateRank position)
        {
            if (target.SyndicateRank == SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (target.SyndicateIdentity != Identity)
            {
                return false;
            }

            uint cost = 0;
            switch (position)
            {
                case SyndicateRank.GuildLeader:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        break;
                    }

                case SyndicateRank.DeputyLeader:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        if (DeputyLeaderCount >= MaxDeputyLeader)
                        {
                            return false;
                        }

                        break;
                    }

                case SyndicateRank.HonoraryDeputyLeader:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        if (HonoraryDeputyLeaderCount >= MaxHonoraryDeputyLeader)
                        {
                            return false;
                        }

                        cost = HONORARY_DEPUTY_LEADER_PRICE;
                        break;
                    }

                case SyndicateRank.HonoraryManager:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        if (HonoraryManagerCount >= MaxHonoraryManager)
                        {
                            return false;
                        }

                        cost = HONORARY_MANAGER_PRICE;
                        break;
                    }

                case SyndicateRank.HonorarySupervisor:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        if (HonorarySupervisorCount >= MaxHonorarySupervisor)
                        {
                            return false;
                        }

                        cost = HONORARY_SUPERVISOR_PRICE;
                        break;
                    }

                case SyndicateRank.HonorarySteward:
                    {
                        if (sender.SyndicateRank != SyndicateRank.GuildLeader)
                        {
                            return false;
                        }

                        if (HonoraryStewardCount >= MaxHonorarySteward)
                        {
                            return false;
                        }

                        cost = HONORARY_STEWARD_PRICE;
                        break;
                    }

                default:
                    {
                        if (!IsMasterPosition(sender.SyndicateRank))
                        {
                            return false;
                        }

                        SyndicateRank assistant = GetAssistantPosition(sender.SyndicateRank);
                        if (assistant != position)
                        {
                            return false;
                        }

                        if (sender.SyndicateMember.AssistantIdentity != 0)
                        {
                            return false;
                        }

                        if (target.SyndicateMember.MasterIdentity != 0)
                        {
                            return false;
                        }

                        if (IsUserSetPosition(target.SyndicateRank))
                        {
                            return false;
                        }

                        break;
                    }
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await sender.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            if (cost > 0)
            {
                if (ConquerPoints < cost)
                {
                    return false;
                }

                ConquerPoints -= cost;
                await SaveAsync();
            }

            if (position == SyndicateRank.GuildLeader) // abdicate
            {
                sender.SyndicateMember.Rank = SyndicateRank.Member;
                await SendSyndicateAsync(sender);
                await sender.Screen.SynchroScreenAsync();
                await sender.SyndicateMember.SaveAsync();

                await target.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Imtheking);

                await SendAsync(string.Format(StrSynAbdicate, sender.Name, target.Name));
            }
            else
            {
                await SendAsync(string.Format(StrSynPromoted, sender.SyndicateMember.RankName, sender.Name,
                                              target.Name,
                                              position));
            }

            target.SyndicateMember.Rank = position;
            await SendSyndicateAsync(target);
            await target.Screen.SynchroScreenAsync();
            await target.SyndicateMember.SaveAsync();

            if (cost > 0)
            {
                target.SyndicateMember.PositionExpiration = DateTime.Now.AddDays(30);
            }

            if (members.TryGetValue(target.MateIdentity, out var mate))
            {
                SyndicateRank matePos = GetSpousePosition(mate.Rank);
                if (matePos != SyndicateRank.None && matePos > mate.Rank &&
                    IsSystemDefinedPosition(mate.Rank))
                {
                    await SetSpouseAsync(target.SyndicateMember, matePos);
                }
            }

            return true;
        }

        public Task<bool> DemoteAsync(Character sender, string name)
        {
            ISyndicateMember member = QueryMember(name);
            if (member == null)
            {
                return Task.FromResult(false);
            }

            return DemoteAsync(sender, member);
        }

        public Task<bool> DemoteAsync(Character sender, uint target)
        {
            ISyndicateMember member = QueryMember(target);
            if (member == null)
            {
                return Task.FromResult(false);
            }

            return DemoteAsync(sender, member);
        }

        public async Task<bool> DemoteAsync(Character sender, ISyndicateMember member)
        {
            if (sender.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (member.Rank == SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (IsSpousePosition(member.Rank))
            {
                return false;
            }

            if (sender.SyndicateRank != SyndicateRank.GuildLeader)
            {
                if (IsAssistantPosition(member.Rank) && sender.Identity != member.MasterIdentity)
                {
                    return false;
                }

                if (IsSystemDefinedPosition(member.Rank))
                {
                    return false;
                }
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await sender.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            member.Rank = SyndicateRank.Member;

            if (member.PositionExpiration != null)
            {
                member.PositionExpiration = null;
            }

            if (member.User != null)
            {
                await SendSyndicateAsync(member.User);
                await member.User.Screen.SynchroScreenAsync();
            }

            await member.SaveAsync();

            if (members.TryGetValue(member.MateIdentity, out var mate))
            {
                if (IsSpousePosition(mate.Rank))
                {
                    await SetSpouseAsync(mate, SyndicateRank.Member);
                }
            }

            return true;
        }

        /// <remarks>USER MUST BE ONLINE!</remarks>
        public static async Task SetSpouseAsync(ISyndicateMember member, SyndicateRank rank)
        {
            member.Rank = rank;
            if (member.User != null)
            {
                await SendSyndicateAsync(member.User);
                await member.User.Screen.SynchroScreenAsync();
            }

            await member.SaveAsync();
        }

        public Task SendPromotionListAsync(Character target)
        {
            var msg = new MsgSyndicate
            {
                Mode = MsgSyndicate.SyndicateRequest.PromotionList
            };

            if (target.SyndicateRank == SyndicateRank.GuildLeader)
            {
                msg.Strings.Add(
                    $"{(int)SyndicateRank.GuildLeader} 1 1 {GetSharedBattlePower(SyndicateRank.GuildLeader)} 0");
                msg.Strings.Add(
                    $"{(int)SyndicateRank.DeputyLeader} {DeputyLeaderCount} {MaxDeputyLeader} {GetSharedBattlePower(SyndicateRank.DeputyLeader)} 0");
                msg.Strings.Add(
                    $"{(int)SyndicateRank.HonoraryDeputyLeader} {HonoraryDeputyLeaderCount} {MaxHonoraryDeputyLeader} {GetSharedBattlePower(SyndicateRank.HonoraryDeputyLeader)} {HONORARY_DEPUTY_LEADER_PRICE}");
                msg.Strings.Add(
                    $"{(int)SyndicateRank.HonoraryManager} {HonoraryManagerCount} {MaxHonoraryManager} {GetSharedBattlePower(SyndicateRank.HonoraryManager)} {HONORARY_MANAGER_PRICE}");
                msg.Strings.Add(
                    $"{(int)SyndicateRank.HonorarySupervisor} {HonorarySupervisorCount} {MaxHonorarySupervisor} {GetSharedBattlePower(SyndicateRank.HonorarySupervisor)} {HONORARY_SUPERVISOR_PRICE}");
                msg.Strings.Add(
                    $"{(int)SyndicateRank.HonorarySteward} {HonoraryStewardCount} {MaxHonorarySteward} {GetSharedBattlePower(SyndicateRank.HonorarySteward)} {HONORARY_STEWARD_PRICE}");
            }

            if (IsMasterPosition(target.SyndicateRank))
            {
                SyndicateRank assistantRank = GetAssistantPosition(target.SyndicateRank);
                int assistantCount = target.SyndicateMember.AssistantIdentity != 0 ? 1 : 0;
                msg.Strings.Add($"{(int)assistantRank} {assistantCount} 1 {GetSharedBattlePower(assistantRank)} 0");
            }

            return target.SendAsync(msg);
        }

        public uint MaxPositionAmount(SyndicateRank pos)
        {
            switch (Level)
            {
                #region Level 1

                case 1:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 1;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                            case SyndicateRank.Steward:
                                return 0;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 2

                case 2:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 1;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 0;
                            case SyndicateRank.Steward:
                                return 1;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 3

                case 3:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 2;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 0;
                            case SyndicateRank.Steward:
                                return 2;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 4

                case 4:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 2;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 1;
                            case SyndicateRank.Steward:
                                return 3;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 5

                case 5:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 4;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 1;
                            case SyndicateRank.Steward:
                                return 4;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 6

                case 6:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 4;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 1;
                            case SyndicateRank.Steward:
                                return 5;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 7

                case 7:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 6;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 1;
                            case SyndicateRank.Steward:
                                return 6;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 8

                case 8:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 6;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 2;
                            case SyndicateRank.Steward:
                                return 7;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                #region Level 9

                case 9:
                    {
                        switch (pos)
                        {
                            case SyndicateRank.Manager:
                            case SyndicateRank.ManagerAide:
                                return 8;
                            case SyndicateRank.Supervisor:
                            case SyndicateRank.ArsenalSupervisor:
                            case SyndicateRank.CpSupervisor:
                            case SyndicateRank.GuideSupervisor:
                            case SyndicateRank.LilySupervisor:
                            case SyndicateRank.OrchidSupervisor:
                            case SyndicateRank.PkSupervisor:
                            case SyndicateRank.SilverSupervisor:
                            case SyndicateRank.TulipSupervisor:
                                return 2;
                            case SyndicateRank.Steward:
                                return 8;
                            case SyndicateRank.Agent:
                            case SyndicateRank.ArsenalAgent:
                            case SyndicateRank.CpAgent:
                            case SyndicateRank.GuideAgent:
                            case SyndicateRank.LilyAgent:
                            case SyndicateRank.OrchidAgent:
                            case SyndicateRank.PkAgent:
                            case SyndicateRank.SilverAgent:
                            case SyndicateRank.TulipAgent:
                            case SyndicateRank.Follower:
                            case SyndicateRank.ArsenalFollower:
                            case SyndicateRank.CpFollower:
                            case SyndicateRank.GuideFollower:
                            case SyndicateRank.LilyFollower:
                            case SyndicateRank.OrchidFollower:
                            case SyndicateRank.PkFollower:
                            case SyndicateRank.SilverFollower:
                            case SyndicateRank.TulipFollower:
                                return 1;
                            default:
                                return 0;
                        }
                    }

                #endregion

                default:
                    return 0;
            }
        }

        public static bool IsSystemDefinedPosition(SyndicateRank pos)
        {
            if (pos == SyndicateRank.Manager)
            {
                return true;
            }

            if (pos == SyndicateRank.Steward)
            {
                return true;
            }

            if (pos == SyndicateRank.DeputySteward)
            {
                return true;
            }

            if (pos >= SyndicateRank.Supervisor && pos <= SyndicateRank.TulipSupervisor)
            {
                return true;
            }

            if (pos >= SyndicateRank.Agent && pos <= SyndicateRank.TulipAgent)
            {
                return true;
            }

            if (pos >= SyndicateRank.Follower && pos <= SyndicateRank.TulipFollower)
            {
                return true;
            }

            return IsSpousePosition(pos);
        }

        public static bool IsUserSetPosition(SyndicateRank pos)
        {
            switch (pos)
            {
                case SyndicateRank.GuildLeader:
                case SyndicateRank.LeaderSpouse:
                case SyndicateRank.LeaderSpouseAide:
                case SyndicateRank.DeputyLeader:
                case SyndicateRank.HonoraryDeputyLeader:
                case SyndicateRank.HonoraryManager:
                case SyndicateRank.HonorarySteward:
                case SyndicateRank.HonorarySupervisor:
                case SyndicateRank.DeputyLeaderAide:
                case SyndicateRank.ManagerAide:
                case SyndicateRank.SupervisorAide:
                case SyndicateRank.Aide:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsHonoraryPosition(SyndicateRank pos)
        {
            switch (pos)
            {
                case SyndicateRank.HonoraryDeputyLeader:
                case SyndicateRank.HonoraryManager:
                case SyndicateRank.HonorarySteward:
                case SyndicateRank.HonorarySupervisor:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsSpousePosition(SyndicateRank pos)
        {
            switch (pos)
            {
                case SyndicateRank.DeputyLeaderSpouse:
                case SyndicateRank.LeaderSpouse:
                case SyndicateRank.ManagerSpouse:
                case SyndicateRank.StewardSpouse:
                case SyndicateRank.SupervisorSpouse:
                    return true;
                default:
                    return false;
            }
        }

        public static SyndicateRank GetSpousePosition(SyndicateRank rank)
        {
            switch (rank)
            {
                case SyndicateRank.GuildLeader: return SyndicateRank.LeaderSpouse;
                case SyndicateRank.DeputyLeader:
                    return SyndicateRank.DeputyLeaderSpouse;
                case SyndicateRank.Manager: return SyndicateRank.ManagerSpouse;
                case SyndicateRank.Steward: return SyndicateRank.StewardSpouse;
                case SyndicateRank.Supervisor: return SyndicateRank.SupervisorSpouse;
                default: return SyndicateRank.None;
            }
        }

        public static SyndicateRank GetAssistantPosition(SyndicateRank rank)
        {
            if (!IsMasterPosition(rank))
            {
                return SyndicateRank.None;
            }

            switch (rank)
            {
                case SyndicateRank.GuildLeader:
                    return SyndicateRank.Aide;
                case SyndicateRank.LeaderSpouse:
                    return SyndicateRank.LeaderSpouseAide;
                case SyndicateRank.DeputyLeader:
                case SyndicateRank.HonoraryDeputyLeader:
                    return SyndicateRank.DeputyLeaderAide;
                case SyndicateRank.Manager:
                case SyndicateRank.HonoraryManager:
                    return SyndicateRank.ManagerAide;
                case SyndicateRank.Supervisor:
                case SyndicateRank.HonorarySupervisor:
                case SyndicateRank.ArsenalSupervisor:
                case SyndicateRank.CpSupervisor:
                case SyndicateRank.GuideSupervisor:
                case SyndicateRank.LilySupervisor:
                case SyndicateRank.OrchidSupervisor:
                case SyndicateRank.PkSupervisor:
                case SyndicateRank.SilverSupervisor:
                case SyndicateRank.TulipSupervisor:
                    return SyndicateRank.SupervisorAide;
            }

            return SyndicateRank.None;
        }

        #endregion

        #region Positions

        public static bool IsMasterPosition(SyndicateRank rank)
        {
            switch (rank)
            {
                case SyndicateRank.GuildLeader:
                case SyndicateRank.LeaderSpouse:
                case SyndicateRank.DeputyLeader:
                case SyndicateRank.HonoraryDeputyLeader:
                case SyndicateRank.Manager:
                case SyndicateRank.HonoraryManager:
                case SyndicateRank.Supervisor:
                case SyndicateRank.HonorarySupervisor:
                case SyndicateRank.ArsenalSupervisor:
                case SyndicateRank.CpSupervisor:
                case SyndicateRank.GuideSupervisor:
                case SyndicateRank.LilySupervisor:
                case SyndicateRank.OrchidSupervisor:
                case SyndicateRank.PkSupervisor:
                case SyndicateRank.SilverSupervisor:
                case SyndicateRank.TulipSupervisor:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsAssistantPosition(SyndicateRank rank)
        {
            switch (rank)
            {
                case SyndicateRank.Aide:
                case SyndicateRank.LeaderSpouseAide:
                case SyndicateRank.DeputyLeaderAide:
                case SyndicateRank.ManagerAide:
                case SyndicateRank.SupervisorAide:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #region Alliance

        public int AlliesCount => allies.Count;

        public async Task<bool> CreateAllianceAsync(Character user, ISyndicate target)
        {
            if (user.SyndicateIdentity != Identity)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                await user.SendAsync(StrSynYouNoLeader);
                return false;
            }

            if (IsAlly(target.Identity) || IsEnemy(target.Identity))
            {
                return false;
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await user.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            if (AlliesCount >= MaxAllies())
            {
                return false;
            }

            await AddAllyAsync(target);
            await target.AddAllyAsync(this);

            await SendAsync(string.Format(StrSynAllyAdd, user.Name, target.Name));
            await target.SendAsync(string.Format(StrSynAllyAdd, target.Leader.UserName, Name));

            return true;
        }

        public async Task<bool> DisbandAllianceAsync(Character user, ISyndicate target)
        {
            if (user.SyndicateIdentity != Identity)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                await user.SendAsync(StrSynYouNoLeader);
                return false;
            }

            if (!IsAlly(target.Identity))
            {
                return false;
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await user.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            await RemoveAllyAsync(target.Identity);
            await target.RemoveAllyAsync(Identity);

            await SendAsync(string.Format(StrSynAllyRemove, user.Name, target.Name));
            await target.SendAsync(string.Format(StrSynAllyRemoved, user.Name, target.Name));
            return true;
        }

        public async Task AddAllyAsync(ISyndicate target)
        {
            allies.TryAdd(target.Identity, target);
            SetAlly(target.Identity);
            await SendAsync(new MsgSyndicate
            {
                Identity = target.Identity,
                Mode = MsgSyndicate.SyndicateRequest.Ally
            });
            await SendAsync(new MsgName
            {
                Identity = target.Identity,
                Action = StringAction.SetAlly,
                Strings = new List<string>
                {
                    $"{target.Name} {target.Leader.UserName} {target.Level} {target.MemberCount}"
                }
            });
        }

        public async Task RemoveAllyAsync(uint idAlly)
        {
            allies.TryRemove((ushort)idAlly, out _);
            UnSetAlly(idAlly);
            await SendAsync(new MsgSyndicate
            {
                Identity = idAlly,
                Mode = MsgSyndicate.SyndicateRequest.Unally
            });
        }

        private bool SetAlly(uint idAlly)
        {
            if (syndicate.Ally0 == 0)
            {
                syndicate.Ally0 = idAlly;
                return true;
            }
            if (syndicate.Ally1 == 0)
            {
                syndicate.Ally1 = idAlly;
                return true;
            }
            if (syndicate.Ally2 == 0)
            {
                syndicate.Ally2 = idAlly;
                return true;
            }
            if (syndicate.Ally3 == 0)
            {
                syndicate.Ally3 = idAlly;
                return true;
            }
            if (syndicate.Ally4 == 0)
            {
                syndicate.Ally4 = idAlly;
                return true;
            }
            if (syndicate.Ally5 == 0)
            {
                syndicate.Ally5 = idAlly;
                return true;
            }
            if (syndicate.Ally6 == 0)
            {
                syndicate.Ally6 = idAlly;
                return true;
            }
            if (syndicate.Ally7 == 0)
            {
                syndicate.Ally7 = idAlly;
                return true;
            }
            if (syndicate.Ally8 == 0)
            {
                syndicate.Ally8 = idAlly;
                return true;
            }
            if (syndicate.Ally9 == 0)
            {
                syndicate.Ally9 = idAlly;
                return true;
            }
            if (syndicate.Ally10 == 0)
            {
                syndicate.Ally10 = idAlly;
                return true;
            }
            if (syndicate.Ally11 == 0)
            {
                syndicate.Ally11 = idAlly;
                return true;
            }
            if (syndicate.Ally12 == 0)
            {
                syndicate.Ally12 = idAlly;
                return true;
            }
            if (syndicate.Ally13 == 0)
            {
                syndicate.Ally13 = idAlly;
                return true;
            }
            if (syndicate.Ally14 == 0)
            {
                syndicate.Ally14 = idAlly;
                return true;
            }
            return false;
        }

        private bool UnSetAlly(uint idAlly)
        {
            if (syndicate.Ally0 == idAlly)
            {
                syndicate.Ally0 = 0;
                return true;
            }
            if (syndicate.Ally1 == idAlly)
            {
                syndicate.Ally1 = 0;
                return true;
            }
            if (syndicate.Ally2 == idAlly)
            {
                syndicate.Ally2 = 0;
                return true;
            }
            if (syndicate.Ally3 == idAlly)
            {
                syndicate.Ally3 = 0;
                return true;
            }
            if (syndicate.Ally4 == idAlly)
            {
                syndicate.Ally4 = 0;
                return true;
            }
            if (syndicate.Ally5 == idAlly)
            {
                syndicate.Ally5 = 0;
                return true;
            }
            if (syndicate.Ally6 == idAlly)
            {
                syndicate.Ally6 = 0;
                return true;
            }
            if (syndicate.Ally7 == idAlly)
            {
                syndicate.Ally7 = 0;
                return true;
            }
            if (syndicate.Ally8 == idAlly)
            {
                syndicate.Ally8 = 0;
                return true;
            }
            if (syndicate.Ally9 == idAlly)
            {
                syndicate.Ally9 = 0;
                return true;
            }
            if (syndicate.Ally10 == idAlly)
            {
                syndicate.Ally10 = 0;
                return true;
            }
            if (syndicate.Ally11 == idAlly)
            {
                syndicate.Ally11 = 0;
                return true;
            }
            if (syndicate.Ally12 == idAlly)
            {
                syndicate.Ally12 = 0;
                return true;
            }
            if (syndicate.Ally13 == idAlly)
            {
                syndicate.Ally13 = 0;
                return true;
            }
            if (syndicate.Ally14 == idAlly)
            {
                syndicate.Ally14 = 0;
                return true;
            }
            return false;
        }

        public bool IsAlly(uint id)
        {
            return allies.ContainsKey((ushort)id);
        }

        public bool UserIsAlly(uint idUser)
        {
            foreach (var ally in allies.Values)
            {
                if (ally.QueryMember(idUser) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public ushort MaxAllies()
        {
            switch (Level)
            {
                case 1: return 5;
                case 2: return 7;
                case 3: return 9;
                case 4: return 12;
                default: return 15;
            }
        }

        #endregion

        #region Enemies

        public int EnemyCount => enemies.Count;

        public async Task<bool> AntagonizeAsync(Character user, ISyndicate target)
        {
            if (user.SyndicateIdentity != Identity)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                await user.SendAsync(StrSynYouNoLeader);
                return false;
            }

            if (IsAlly(target.Identity) || IsEnemy(target.Identity))
            {
                return false;
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await user.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            if (EnemyCount >= MaxEnemies())
            {
                // ? message
                return false;
            }

            if (enemies.TryAdd(target.Identity, target))
            {
                SetEnemy(target.Identity);
            }

            await SendAsync(new MsgSyndicate
            {
                Identity = target.Identity,
                Mode = MsgSyndicate.SyndicateRequest.Enemy
            });
            await SendAsync(new MsgName
            {
                Identity = target.Identity,
                Action = StringAction.SetEnemy,
                Strings = new List<string>
                {
                    $"{target.Name} {target.Leader.UserName} {target.Level} {target.MemberCount}"
                }
            });

            await SendAsync(string.Format(StrSynAddEnemy, user.Name, target.Name));
            await target.SendAsync(string.Format(StrSynAddedEnemy, user.Name, Name));
            return true;
        }

        public async Task<bool> PeaceAsync(Character user, ISyndicate target)
        {
            if (user.SyndicateIdentity != Identity)
            {
                return false;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                await user.SendAsync(StrSynYouNoLeader);
                return false;
            }

            if (!IsEnemy(target.Identity))
            {
                return false;
            }

            if (Money < SYNDICATE_ACTION_COST)
            {
                await user.SendAsync(string.Format(StrSynNoMoney, SYNDICATE_ACTION_COST));
                return false;
            }

            enemies.TryRemove(target.Identity, out _);
            UnSetEnemy(target.Identity);

            await SendAsync(new MsgSyndicate
            {
                Identity = target.Identity,
                Mode = MsgSyndicate.SyndicateRequest.Unenemy
            });

            await SendAsync(string.Format(StrSynRemoveEnemy, user.Name, target.Name));
            await target.SendAsync(string.Format(StrSynRemovedEnemy, user.Name, Name));

            return true;
        }

        private bool SetEnemy(uint idEnemy)
        {
            if (syndicate.Enemy0 == 0)
            {
                syndicate.Enemy0 = idEnemy;
                return true;
            }
            if (syndicate.Enemy1 == 0)
            {
                syndicate.Enemy1 = idEnemy;
                return true;
            }
            if (syndicate.Enemy2 == 0)
            {
                syndicate.Enemy2 = idEnemy;
                return true;
            }
            if (syndicate.Enemy3 == 0)
            {
                syndicate.Enemy3 = idEnemy;
                return true;
            }
            if (syndicate.Enemy4 == 0)
            {
                syndicate.Enemy4 = idEnemy;
                return true;
            }
            if (syndicate.Enemy5 == 0)
            {
                syndicate.Enemy5 = idEnemy;
                return true;
            }
            if (syndicate.Enemy6 == 0)
            {
                syndicate.Enemy6 = idEnemy;
                return true;
            }
            if (syndicate.Enemy7 == 0)
            {
                syndicate.Enemy7 = idEnemy;
                return true;
            }
            if (syndicate.Enemy8 == 0)
            {
                syndicate.Enemy8 = idEnemy;
                return true;
            }
            if (syndicate.Enemy9 == 0)
            {
                syndicate.Enemy9 = idEnemy;
                return true;
            }
            if (syndicate.Enemy10 == 0)
            {
                syndicate.Enemy10 = idEnemy;
                return true;
            }
            if (syndicate.Enemy11 == 0)
            {
                syndicate.Enemy11 = idEnemy;
                return true;
            }
            if (syndicate.Enemy12 == 0)
            {
                syndicate.Enemy12 = idEnemy;
                return true;
            }
            if (syndicate.Enemy13 == 0)
            {
                syndicate.Enemy13 = idEnemy;
                return true;
            }
            if (syndicate.Enemy14 == 0)
            {
                syndicate.Enemy14 = idEnemy;
                return true;
            }
            return false;
        }

        private bool UnSetEnemy(uint idEnemy)
        {
            if (syndicate.Enemy0 == idEnemy)
            {
                syndicate.Enemy0 = 0;
                return true;
            }
            if (syndicate.Enemy1 == idEnemy)
            {
                syndicate.Enemy1 = 0;
                return true;
            }
            if (syndicate.Enemy2 == idEnemy)
            {
                syndicate.Enemy2 = 0;
                return true;
            }
            if (syndicate.Enemy3 == idEnemy)
            {
                syndicate.Enemy3 = 0;
                return true;
            }
            if (syndicate.Enemy4 == idEnemy)
            {
                syndicate.Enemy4 = 0;
                return true;
            }
            if (syndicate.Enemy5 == idEnemy)
            {
                syndicate.Enemy5 = 0;
                return true;
            }
            if (syndicate.Enemy6 == idEnemy)
            {
                syndicate.Enemy6 = 0;
                return true;
            }
            if (syndicate.Enemy7 == idEnemy)
            {
                syndicate.Enemy7 = 0;
                return true;
            }
            if (syndicate.Enemy8 == idEnemy)
            {
                syndicate.Enemy8 = 0;
                return true;
            }
            if (syndicate.Enemy9 == idEnemy)
            {
                syndicate.Enemy9 = 0;
                return true;
            }
            if (syndicate.Enemy10 == idEnemy)
            {
                syndicate.Enemy10 = 0;
                return true;
            }
            if (syndicate.Enemy11 == idEnemy)
            {
                syndicate.Enemy11 = 0;
                return true;
            }
            if (syndicate.Enemy12 == idEnemy)
            {
                syndicate.Enemy12 = 0;
                return true;
            }
            if (syndicate.Enemy13 == idEnemy)
            {
                syndicate.Enemy13 = 0;
                return true;
            }
            if (syndicate.Enemy14 == idEnemy)
            {
                syndicate.Enemy14 = 0;
                return true;
            }
            return false;
        }

        public bool IsEnemy(uint id)
        {
            return enemies.ContainsKey((ushort)id);
        }

        public ushort MaxEnemies()
        {
            switch (Level)
            {
                case 1: return 5;
                case 2: return 7;
                case 3: return 9;
                case 4: return 12;
                default: return 15;
            }
        }

        #endregion

        #region Requirements

        public bool AllowTrojan => !ProfessionRequirement.HasFlag(ProfessionPermission.Trojan);
        public bool AllowWarrior => !ProfessionRequirement.HasFlag(ProfessionPermission.Warrior);
        public bool AllowArcher => !ProfessionRequirement.HasFlag(ProfessionPermission.Archer);
        public bool AllowTaoist => !ProfessionRequirement.HasFlag(ProfessionPermission.Taoist);
        public bool AllowNinja => !ProfessionRequirement.HasFlag(ProfessionPermission.Ninja);
        public bool AllowMonk => !ProfessionRequirement.HasFlag(ProfessionPermission.Monk);
        public bool AllowPirate => !ProfessionRequirement.HasFlag(ProfessionPermission.Pirate);
        public bool AllowDragonWarrior => !ProfessionRequirement.HasFlag(ProfessionPermission.DragonWarrior);

        public ProfessionPermission ProfessionRequirement
        {
            get => (ProfessionPermission)syndicate.ConditionProf;
            set => syndicate.ConditionProf = (uint)value;
        }

        public byte LevelRequirement
        {
            get => syndicate.ConditionLevel;
            set => syndicate.ConditionLevel = value;
        }

        public byte MetempsychosisRequirement
        {
            get => syndicate.ConditionMetem;
            set => syndicate.ConditionMetem = value;
        }

        #endregion

        #region Totem

        public ITotem Totem { get; set; }

        public ITotem.TotemPoleFlag TotemPole
        {
            get => (ITotem.TotemPoleFlag)syndicate.TotemPole;
            set => syndicate.TotemPole = (uint)value;
        }

        public int GetSharedBattlePower(SyndicateRank rank)
        {
            if (Totem == null)
            {
                return 0;
            }

            int totalBp = Totem.SharedBattlePower;
            switch (rank)
            {
                case SyndicateRank.GuildLeader:
                    return totalBp;
                case SyndicateRank.LeaderSpouse:
                case SyndicateRank.DeputyLeader:
                case SyndicateRank.HonoraryDeputyLeader:
                    return (int)(totalBp * 0.9f);
                case SyndicateRank.Manager:
                case SyndicateRank.HonoraryManager:
                    return (int)(totalBp * 0.8f);
                case SyndicateRank.Supervisor:
                case SyndicateRank.ArsenalSupervisor:
                case SyndicateRank.CpSupervisor:
                case SyndicateRank.GuideSupervisor:
                case SyndicateRank.HonorarySupervisor:
                case SyndicateRank.LilySupervisor:
                case SyndicateRank.OrchidSupervisor:
                case SyndicateRank.PkSupervisor:
                case SyndicateRank.RoseSupervisor:
                case SyndicateRank.SilverSupervisor:
                case SyndicateRank.TulipSupervisor:
                    return (int)(totalBp * 0.7f);
                case SyndicateRank.Steward:
                case SyndicateRank.DeputyLeaderSpouse:
                case SyndicateRank.LeaderSpouseAide:
                case SyndicateRank.DeputyLeaderAide:
                    return (int)(totalBp * 0.5f);
                case SyndicateRank.DeputySteward:
                    return (int)(totalBp * 0.4f);
                case SyndicateRank.Agent:
                case SyndicateRank.SupervisorSpouse:
                case SyndicateRank.ManagerSpouse:
                case SyndicateRank.SupervisorAide:
                case SyndicateRank.ManagerAide:
                case SyndicateRank.ArsenalAgent:
                case SyndicateRank.CpAgent:
                case SyndicateRank.GuideAgent:
                case SyndicateRank.LilyAgent:
                case SyndicateRank.OrchidAgent:
                case SyndicateRank.PkAgent:
                case SyndicateRank.RoseAgent:
                case SyndicateRank.SilverAgent:
                case SyndicateRank.TulipAgent:
                    return (int)(totalBp * 0.3f);
                case SyndicateRank.StewardSpouse:
                case SyndicateRank.Follower:
                case SyndicateRank.ArsenalFollower:
                case SyndicateRank.CpFollower:
                case SyndicateRank.GuideFollower:
                case SyndicateRank.LilyFollower:
                case SyndicateRank.OrchidFollower:
                case SyndicateRank.PkFollower:
                case SyndicateRank.RoseFollower:
                case SyndicateRank.SilverFollower:
                case SyndicateRank.TulipFollower:
                    return (int)(totalBp * 0.2f);
                case SyndicateRank.SeniorMember:
                    return (int)(totalBp * 0.15f);
                case SyndicateRank.Member:
                    return (int)(totalBp * 0.1f);
                default:
                    return 0;
            }
        }

        public async Task SynchronizeBattlePowerAsync()
        {
            foreach (ISyndicateMember member in members.Values.Where(x => x.IsOnline))
            {
                await member.User.SendAsync(new MsgUserAttrib(member.UserIdentity, ClientUpdateType.TotemPoleBattlePower, (ulong)GetSharedBattlePower(member.Rank)));
            }
        }

        public void SetLevel()
        {
            Level = (byte)(Totem?.Level ?? 1);
        }

        #endregion

        #region Query

        public ISyndicateMember QueryMember(uint id)
        {
            return members.TryGetValue(id, out var member) ? member : null;
        }

        public ISyndicateMember QueryMember(string member)
        {
            return members.Values.FirstOrDefault(x => x.UserName == member);
        }

        public async Task<ISyndicateMember> QueryRandomMemberAsync()
        {
            if (this.members.Count == 0)
            {
                return null;
            }
            List<ISyndicateMember> members = this.members.Values.ToList();
            return members[await NextAsync(members.Count) % members.Count];
        }

        public List<ISyndicateMember> QueryRank(int type)
        {
            switch ((RankRequestType)type)
            {
                case RankRequestType.Silvers:
                    return members.Values.Where(x => x.Silvers > 0).OrderByDescending(x => x.Silvers).ToList();
                case RankRequestType.ConquerPoints:
                    return members.Values.Where(x => x.ConquerPointsDonation > 0)
                                       .OrderByDescending(x => x.ConquerPointsDonation).ToList();
                case RankRequestType.Guide:
                    return members.Values.Where(x => x.GuideDonation > 0).OrderByDescending(x => x.GuideDonation)
                                       .ToList();
                case RankRequestType.PK:
                    return members.Values.Where(x => x.PkDonation > 0).OrderByDescending(x => x.PkDonation)
                                       .ToList();
                case RankRequestType.Arsenal:
                    return members.Values.Where(x => x.ArsenalDonation > 0)
                                       .OrderByDescending(x => x.ArsenalDonation).ToList();
                case RankRequestType.RedRose:
                    return members.Values.Where(x => x.RedRoseDonation > 0)
                                       .OrderByDescending(x => x.RedRoseDonation).ToList();
                case RankRequestType.WhiteRose:
                    return members.Values.Where(x => x.WhiteRoseDonation > 0)
                                       .OrderByDescending(x => x.WhiteRoseDonation).ToList();
                case RankRequestType.Orchid:
                    return members.Values.Where(x => x.OrchidDonation > 0).OrderByDescending(x => x.OrchidDonation)
                                       .ToList();
                case RankRequestType.Tulip:
                    return members.Values.Where(x => x.TulipDonation > 0).OrderByDescending(x => x.TulipDonation)
                                       .ToList();
                case RankRequestType.Total:
                    return members.Values.Where(x => x.TotalDonation > 0).OrderByDescending(x => x.TotalDonation)
                                       .ToList();
                case RankRequestType.Usable:
                    return members.Values.Where(x => x.TotalDonation > 0).OrderByDescending(x => x.UsableDonation)
                                       .ToList();
            }

            return new List<ISyndicateMember>();
        }

        #endregion

        #region Union

        public uint LeagueId
        {
            get => syndicate.LeagueId;
            set => syndicate.LeagueId = value;
        }

        #endregion

        #region Socket

        public static async Task SendSyndicateAsync(Character user)
        {
            if (user.Syndicate is Syndicate syndicate 
                && syndicate.members.TryGetValue(user.Identity, out var syndicateMember))
            {
                await user.SendAsync(new MsgSyndicateAttributeInfo
                {
                    Identity = syndicate.Identity,
                    Rank = (int)syndicateMember.Rank,
                    MemberAmount = syndicate.MemberCount,
                    Funds = syndicate.Money,
                    PlayerDonation = syndicateMember.Silvers,
                    LeaderName = syndicate.Leader?.UserName ?? StrNone,
                    ConditionLevel = syndicate.LevelRequirement,
                    ConditionMetempsychosis = syndicate.MetempsychosisRequirement,
                    ConditionProfession = (int)syndicate.ProfessionRequirement,
                    ConquerPointsFunds = syndicate.ConquerPoints,
                    PositionExpiration = uint.Parse(syndicateMember.PositionExpiration?.ToString("yyyyMMdd") ?? "0"),
                    EnrollmentDate = uint.Parse(syndicateMember.JoinDate.ToString("yyyyMMdd")),
                    Level = syndicate.Level
                });
                await user.SendAsync(new MsgSyndicate
                {
                    Mode = MsgSyndicate.SyndicateRequest.Bulletin,
                    Strings = new List<string> { syndicate.Announce },
                    Identity = uint.Parse(syndicate.AnnounceDate.ToString("yyyyMMdd"))
                });
                await syndicate.SendAsync(user);
                await user.SynchroAttributesAsync(ClientUpdateType.TotemPoleBattlePower, (ulong)syndicate.GetSharedBattlePower(user.SyndicateRank));
            }
            else
            {
                await user.SendAsync(new MsgSyndicateAttributeInfo
                {
                    Rank = (int)SyndicateRank.None
                });
            }
        }

        public Task SendSyndicateToUserAsync(Character user)
        {
            return SendSyndicateAsync(user);
        }

        public async Task SendMembersAsync(int page, Character target)
        {
            const int MAX_PER_PAGE_I = 12;
            int startAt = page; // just in case I need to change the value in runtime
            var current = 0;

            var msg = new MsgSynMemberList
            {
                Index = page
            };

            foreach (ISyndicateMember member in members.Values
                                                           .OrderByDescending(x => x.IsOnline)
                                                           .ThenByDescending(x => x.Rank)
                                                           .ThenByDescending(x => x.Level))
            {
                if (current - startAt >= MAX_PER_PAGE_I)
                {
                    break;
                }

                if (current++ < startAt)
                {
                    continue;
                }

                int lastLoginSeconds = 0;
                if (!member.IsOnline && member.LastLogout != null)
                {
                    lastLoginSeconds = UnixTimestamp.Now - UnixTimestamp.FromDateTime(member.LastLogout.Value);
                }

                msg.Members.Add(new MsgSynMemberList.MemberStruct
                {
                    Identity = member.UserIdentity,
                    Name = member.UserName,
                    Rank = (int)member.Rank,
                    Nobility = (int)member.NobilityRank,
                    PositionExpire = uint.Parse(member.PositionExpiration?.ToString("yyyyMMdd") ?? "0"),
                    IsOnline = member.IsOnline,
                    LookFace = member.LookFace,
                    TotalDonation = member.TotalDonation,
                    Level = member.Level,
                    Profession = member.Profession,
                    LastLoginSeconds = lastLoginSeconds
                });
            }

            await target.SendAsync(msg);
        }

        private static readonly SyndicateRank[] ShowMinContriRank =
        {
            SyndicateRank.Manager,
            SyndicateRank.Supervisor,
            SyndicateRank.SilverSupervisor,
            SyndicateRank.CpSupervisor,
            SyndicateRank.PkSupervisor,
            SyndicateRank.GuideSupervisor,
            SyndicateRank.ArsenalSupervisor,
            SyndicateRank.RoseSupervisor,
            SyndicateRank.LilySupervisor,
            SyndicateRank.OrchidSupervisor,
            SyndicateRank.TulipSupervisor,
            SyndicateRank.Steward,
            SyndicateRank.DeputySteward,
            SyndicateRank.TulipAgent,
            SyndicateRank.OrchidAgent,
            SyndicateRank.CpAgent,
            SyndicateRank.ArsenalAgent,
            SyndicateRank.SilverAgent,
            SyndicateRank.GuideAgent,
            SyndicateRank.PkAgent,
            SyndicateRank.RoseAgent,
            SyndicateRank.LilyAgent,
            SyndicateRank.Agent,
            SyndicateRank.TulipFollower,
            SyndicateRank.OrchidFollower,
            SyndicateRank.CpFollower,
            SyndicateRank.ArsenalFollower,
            SyndicateRank.SilverFollower,
            SyndicateRank.GuideFollower,
            SyndicateRank.PkFollower,
            SyndicateRank.RoseFollower,
            SyndicateRank.LilyFollower,
            SyndicateRank.Follower,
            SyndicateRank.SeniorMember
        };

        public Task SendMinContributionAsync(Character target)
        {
            if (target.SyndicateIdentity != Identity)
            {
                return Task.CompletedTask;
            }

            var msg = new MsgDutyMinContri();
            foreach (SyndicateRank pos in ShowMinContriRank)
            {
                uint min = 0;
                int current = members.Values.Count(x => x.Rank == pos);
                var maxPerPos = (int)MaxPositionAmount(pos);

                switch (pos)
                {
                    case SyndicateRank.Manager:
                    case SyndicateRank.Steward:
                    case SyndicateRank.Supervisor:
                    case SyndicateRank.Agent:
                    case SyndicateRank.Follower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = (uint)members.Values.Where(x => x.Rank == pos).Select(x => x.UsableDonation)
                                                         .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = (uint)members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                         .Select(x => x.UsableDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.RoseSupervisor:
                    case SyndicateRank.RoseAgent:
                    case SyndicateRank.RoseFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.RedRoseDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.RedRoseDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.LilySupervisor:
                    case SyndicateRank.LilyAgent:
                    case SyndicateRank.LilyFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.WhiteRoseDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.WhiteRoseDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.OrchidAgent:
                    case SyndicateRank.OrchidFollower:
                    case SyndicateRank.OrchidSupervisor:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.OrchidDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.OrchidDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.TulipSupervisor:
                    case SyndicateRank.TulipAgent:
                    case SyndicateRank.TulipFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.TulipDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.TulipDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.PkSupervisor:
                    case SyndicateRank.PkAgent:
                    case SyndicateRank.PkFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = (uint)members.Values.Where(x => x.Rank == pos).Select(x => x.PkDonation)
                                                         .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = (uint)members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                         .Select(x => x.PkDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.GuideSupervisor:
                    case SyndicateRank.GuideAgent:
                    case SyndicateRank.GuideFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.GuideDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.GuideDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.SilverSupervisor:
                    case SyndicateRank.SilverFollower:
                    case SyndicateRank.SilverAgent:
                        {
                            if (current >= maxPerPos)
                            {
                                min = (uint)members.Values.Where(x => x.Rank == pos).Select(x => x.Silvers)
                                                         .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = (uint)members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                         .Select(x => x.Silvers).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.CpSupervisor:
                    case SyndicateRank.CpAgent:
                    case SyndicateRank.CpFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.ConquerPointsDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.ConquerPointsDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.ArsenalSupervisor:
                    case SyndicateRank.ArsenalAgent:
                    case SyndicateRank.ArsenalFollower:
                        {
                            if (current >= maxPerPos)
                            {
                                min = members.Values.Where(x => x.Rank == pos).Select(x => x.ArsenalDonation)
                                                  .DefaultIfEmpty().Min(x => x);
                            }
                            else
                            {
                                min = members.Values.Where(x => x.Rank == SyndicateRank.Member)
                                                  .Select(x => x.ArsenalDonation).DefaultIfEmpty().Max(x => x);
                            }

                            break;
                        }

                    case SyndicateRank.DeputySteward:
                        {
                            min = 175000;
                            break;
                        }

                    case SyndicateRank.SeniorMember:
                        {
                            min = 25000;
                            break;
                        }
                }

                msg.Members.Add(new MsgDutyMinContri.MinContriStruct
                {
                    Position = (int)pos,
                    Donation = min
                });
            }

            return target.SendAsync(msg);
        }

        public async Task SendRelationAsync(Character target)
        {
            foreach (Syndicate ally in allies.Values)
            {
                await target.SendAsync(new MsgName
                {
                    Identity = ally.Identity,
                    Action = StringAction.SetAlly,
                    Strings = new List<string>
                    {
                        $"{ally.Name} {ally.Leader?.UserName ?? StrNone} {ally.Level} {ally.MemberCount}"
                    }
                });
            }

            foreach (Syndicate enemy in enemies.Values)
            {
                await target.SendAsync(new MsgName
                {
                    Identity = enemy.Identity,
                    Action = StringAction.SetEnemy,
                    Strings = new List<string>
                    {
                        $"{enemy.Name} {enemy.Leader?.UserName ?? StrNone} {enemy.Level} {enemy.MemberCount}"
                    }
                });
            }
        }

        public async Task BroadcastNameAsync()
        {
            await RoleManager.BroadcastWorldMsgAsync(new MsgName
            {
                Action = StringAction.Guild,
                Identity = Identity,
                Strings = new List<string>
                {
                    Name
                }
            });
        }

        public async Task SendAsync(Character user)
        {
            await user.SendAsync(new MsgName
            {
                Action = StringAction.Guild,
                Identity = Identity,
                Strings = new List<string>
                {
                    Name
                }
            });
        }

        public async Task SendAsync(string message, uint idIgnore = 0u, Color? color = null)
        {
            await SendAsync(new MsgTalk(TalkChannel.Guild, color ?? Color.White, message), idIgnore);
        }

        public Task SendAsync(IPacket msg, uint exclude = 0u)
        {
            return SendAsync(msg.Encode());
        }

        public async Task SendAsync(byte[] msg, uint exclude = 0u)
        {
            foreach (SyndicateMember player in members.Values)
            {
                if (exclude == player.UserIdentity || !player.IsOnline)
                {
                    continue;
                }

                await player.User.SendAsync(msg);
            }
        }

        public async Task BroadcastToAlliesAsync(IPacket msg)
        {
            byte[] encoded = msg.Encode();
            foreach (var ally in allies.Values)
            {
                await ally.SendAsync(encoded);
            }
        }

        #endregion

        #region Database

        public async Task<bool> SaveAsync()
        {
            return await ServerDbContext.UpdateAsync(syndicate);
        }

        public async Task<bool> SoftDeleteAsync()
        {
            syndicate.DelFlag = 1;
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync()
        {
            return await ServerDbContext.DeleteAsync(syndicate);
        }

        #endregion
    }
}
