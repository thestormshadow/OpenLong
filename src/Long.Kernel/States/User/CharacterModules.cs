using Long.Kernel.Modules.Systems.AstProf;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Modules.Systems.Flower;
using Long.Kernel.Modules.Systems.Guide;
using Long.Kernel.Modules.Systems.JiangHu;
using Long.Kernel.Modules.Systems.NeiGong;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Status;
using static Long.Kernel.Modules.Systems.AstProf.IAstProf;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        #region Nobility

        private NobilityRank osNobilityRank;

        public ulong Donation
        {
            get => user.Donation;
            set => user.Donation = value;
        }

        public INobility Nobility { get; set; }

        public NobilityRank NobilityRank
        {
            get => IsOSUser() ? osNobilityRank : Nobility?.Rank ?? 0;
            set 
            {
                if (IsOSUser())
                {
                    osNobilityRank = value;
                }
            }
        }

        #endregion

        #region Flower

        public IFlower Flower { get; set; }

        public uint FlowerRed
        {
            get => user.FlowerRed;
            set => user.FlowerRed = value;
        }

        public uint FlowerWhite
        {
            get => user.FlowerWhite;
            set => user.FlowerWhite = value;
        }

        public uint FlowerOrchid
        {
            get => user.FlowerOrchid;
            set => user.FlowerOrchid = value;
        }

        public uint FlowerTulip
        {
            get => user.FlowerTulip;
            set => user.FlowerTulip = value;
        }

        public uint SendFlowerTime
        {
            get => user.SendFlowerDate;
            set => user.SendFlowerDate = value;
        }

        private const int V = 0;

        #endregion

        #region Team

        private readonly TimeOut teamLeaderPosTimer = new(3);

        public ITeam Team { get; set; }

        public uint VirtuePoints
        {
            get => user.Virtue;
            set => user.Virtue = value;
        }

        public async Task BroadcastTeamLifeAsync(bool changeMaxLife = false)
        {
            if (Team != null)
            {
                await Team.BroadcastMemberLifeAsync(this, changeMaxLife);
            }
        }

        public async Task OnLeaveTeamAsync()
        {
            for (int i = StatusSet.TYRANT_AURA; i <= StatusSet.EARTH_AURA; i++)
            {
                IStatus aura = QueryStatus(i);
                if (aura == null || aura.IsUserCast)
                {
                    continue;
                }

                await DetachStatusAsync(i);
            }
        }

        #endregion

        #region Syndicate

        private uint osSyndicateId;
        private ISyndicateMember.SyndicateRank osSyndicateRank;
        private string osSyndicateName;
        public int osTotemBattleEffect;

        public ISyndicate Syndicate { get; set; }
        public ISyndicateMember SyndicateMember => Syndicate?.QueryMember(Identity);

        public ISyndicateMember.SyndicateRank SyndicateRank => IsOSUser() ? osSyndicateRank : SyndicateMember?.Rank ?? ISyndicateMember.SyndicateRank.None;
        public uint SyndicateIdentity => IsOSUser() ? osSyndicateId : Syndicate?.Identity ?? 0;
        public string SyndicateName => IsOSUser() ? osSyndicateName : Syndicate?.Name ?? StrNone;
        public int TotemBattlePower => IsOSUser() ? osTotemBattleEffect : Syndicate?.GetSharedBattlePower(SyndicateRank) ?? 0;

        public void SetOSSyndicateData(uint syndicateId, ISyndicateMember.SyndicateRank rank, string name, int battleEffect)
        {
            osSyndicateId = syndicateId;
            osSyndicateName = name;
            osTotemBattleEffect = battleEffect;
            osSyndicateRank = rank;
        }

        #endregion

        #region Family

        private uint osFamilyId;
        private IFamily.FamilyRank osFamilyRank;
        private string osFamilyName;

        public IFamily Family { get; set; }
        public IFamilyMember FamilyMember => Family?.GetMember(Identity);
        public IFamily.FamilyRank FamilyRank => FamilyMember?.Rank ?? IFamily.FamilyRank.None;

        public uint FamilyIdentity => IsOSUser() ? osFamilyId : Family?.Identity ?? 0u;
        public string FamilyName => IsOSUser() ? osFamilyName : Family?.Name ?? StrNone;
        public int FamilyBattlePower => IsOSUser() ? 0 : Team?.FamilyBattlePower(this, out _) ?? 0;

        public void SetOSFamilyData(uint familyId, IFamily.FamilyRank familyRank, string familyName)
        {
            osFamilyId = familyId;
            osFamilyRank = familyRank;
            osFamilyName = familyName;
        }

        public Task SynchroFamilyBattlePowerAsync()
        {
            if (Team == null || Family == null)
            {
                return Task.CompletedTask;
            }

            int bp = Team.FamilyBattlePower(this, out uint provider);
            MsgUserAttrib msg = new(Identity, ClientUpdateType.FamilySharedBattlePower, provider);
            msg.Append(ClientUpdateType.FamilySharedBattlePower, (ulong)bp);
            return SendAsync(msg);
        }

        #endregion

        #region Guide

        public IGuide Guide { get; set; }
		public ITutor Tutor { get; set; }

		#endregion

		#region AstProf (Sub-classes)

		public ulong AstProfRanks
        {
            get => user.AstProfRank;
            set => user.AstProfRank = value;
        }

        public AstProfType AstProfType
        {
            get => (AstProfType)user.AstProfCurrent;
            set => user.AstProfCurrent = (byte)value;
        }

        public IAstProf AstProf { get; set; }

        public async Task<bool> ChangeCultivationAsync(int amount)
        {
            if (amount > 0)
            {
                await AwardCultivationAsync(amount);
                return true;
            }

            if (amount < 0)
            {
                return await SpendCultivationAsync(amount * -1);
            }

            return false;
        }

        public async Task AwardCultivationAsync(int amount)
        {
            StudyPoints = (uint)(StudyPoints + amount);
            await SaveAsync();

            if (AstProf != null)
            {
                await AstProf.UpdateStudyAsync((uint)amount);
            }
        }

        public async Task<bool> SpendCultivationAsync(int amount)
        {
            if (amount > StudyPoints)
            {
                return false;
            }

            StudyPoints = (uint)(StudyPoints - amount);
            await SaveAsync();
            if (AstProf != null)
            {
                await AstProf.UpdateStudyAsync();
            }

            return true;
        }

        #endregion

        #region Fate

        public IFate Fate { get; set; }

        #endregion

        #region Jiang Hu

        public IJiangHu JiangHu { get; set; }

        public JiangPkMode JiangPkImmunity { get; set; }

        #endregion

        #region Inner Power

        public INeiGong InnerStrength { get; set; }

        public uint CultureValue
        {
            get => user.CultureValue;
            set => user.CultureValue = value;
        }

        public async Task<bool> ChangeCultureAsync(int amount)
        {
            if (amount > 0)
            {
                await AwardCultureAsync(amount);
                return true;
            }

            if (amount < 0)
            {
                return await SpendCultureAsync(amount * -1);
            }

            return false;
        }

        public async Task AwardCultureAsync(int amount)
        {
            CultureValue = (uint)(CultureValue + amount);
            await SaveAsync();
            await SendAsync(new MsgUserAttrib(Identity, ClientUpdateType.NeiGongValue, CultureValue));
        }

        public async Task<bool> SpendCultureAsync(int amount)
        {
            if (amount > CultureValue)
            {
                return false;
            }

            CultureValue = (uint)(CultureValue - amount);
            await SaveAsync();
            await SendAsync(new MsgUserAttrib(Identity, ClientUpdateType.NeiGongValue, CultureValue));
            return true;
        }

        #endregion
    }
}