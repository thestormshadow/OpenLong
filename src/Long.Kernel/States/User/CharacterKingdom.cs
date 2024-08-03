using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Realm;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        #region League Points

        private DbGoldenLeagueData goldenLeagueData;

        public uint LeaguePoints
        {
            get => goldenLeagueData?.CurrentPoint ?? 0;
            set => goldenLeagueData.CurrentPoint = value;
        }

        public uint LeaguePointsHistory
        {
            get => goldenLeagueData?.TotalPoint ?? 0;
            set => goldenLeagueData.TotalPoint = value;
        }

        public async Task LoadGoldenLeagueDataAsync()
        {
            goldenLeagueData = await GoldenLeagueRepository.GetAsync(Identity) ?? new DbGoldenLeagueData
            {
                UserId = Identity
            };
            await SendGoldenLeaguePointsAsync();
        }

        public async Task<bool> ChangeGoldenLeaguePointsAsync(long amount, bool notify = false)
        {
            if (amount > 0)
            {
                await AwardGoldenLeaguePointsAsync(amount);
                return true;
            }

            if (amount < 0)
            {
                return await SpendMoneyAsync(amount * -1, notify);
            }

            return false;
        }

        public async Task AwardGoldenLeaguePointsAsync(long amount)
        {
            LeaguePoints = (uint)Math.Max(0, Math.Min(LeaguePoints + (ulong)amount, 10_000_000));
            await SaveGoldenLeagueDataAsync();
            await SendGoldenLeaguePointsAsync();
        }

        public async Task<bool> SpendGoldenLeaguePointsAsync(long amount)
        {
            if ((ulong)amount > LeaguePoints)
            {
                return false;
            }

            LeaguePoints = (uint)Math.Max(0, Math.Min(LeaguePoints - (ulong)amount, 10_000_000));
            await SaveGoldenLeagueDataAsync();
            await SendGoldenLeaguePointsAsync();
            return true;
        }

        public Task SendGoldenLeaguePointsAsync()
        {
            return SendAsync(new MsgGLRankingList(LeaguePoints, 0));
        }

        private Task SaveGoldenLeagueDataAsync()
        {
            if (goldenLeagueData == null)
            {
                return Task.CompletedTask;
            }

            if (goldenLeagueData.Id != 0)
            {
                return ServerDbContext.UpdateAsync(goldenLeagueData);
            }

            return ServerDbContext.CreateAsync(goldenLeagueData);
        }

        #endregion

        #region Union

        private uint osUnionId;
        private KingdomOfficial.OfficialPositionFlag osOfficialPositionFlag;
        private string osUnionName;
        private bool osIsKingdom;

        public Union Union { get; set; }
        public UnionMember UnionMember => Union?.QueryMember(Identity);

        public uint UnionIdentity => IsOSUser() ? osUnionId : Union?.Identity ?? 0;
        public KingdomOfficial.OfficialPositionFlag UnionOfficialFlag => IsOSUser() ? osOfficialPositionFlag : UnionMember?.OfficialTypeFlag ?? 0;
        public string UnionName => IsOSUser() ? osUnionName : Union?.Name ?? StrNone;
        public bool IsKingdomMember => IsOSUser() ? osIsKingdom : Union?.IsKingdom ?? false;

        public uint LeagueContribution
        {
            get => user.LeagueContribution;
            set
            {
                user.LeagueContribution = value;
                LeagueContributionLevel = KingdomManager.GetLeagueContributeLevel(value);
            }
        }

        public uint LeagueContributionLevel { get; set; }

        public void SetOSUnionData(uint unionId, KingdomOfficial.OfficialPositionFlag officialFlag, string unionName, bool isKingdom)
        {
            osUnionId = unionId;
            osUnionName = unionName;
            osOfficialPositionFlag = officialFlag;
            osIsKingdom = isKingdom;
        }

        public async Task LoadUnionAsync()
        {
            UnionMember unionMembership = KingdomManager.GetUnionMember(Identity);
            if (unionMembership != null)
            {
                Union = KingdomManager.GetUnion(unionMembership.UnionId);
                await SendUnionAsync();
                LeagueContributionLevel = KingdomManager.GetLeagueContributeLevel(LeagueContribution);
                await Screen.SynchroScreenAsync();
            }
        }

        public async Task SendUnionAsync()
        {
            if (Union == null)
            {
                return;
            }

            await LoadGoldenLeagueDataAsync();

            await BroadcastRoomMsgAsync(
                new MsgUserAttrib(Identity, ClientUpdateType.UnionOfficialPosition, (uint)UnionMember.OfficialTypeFlag),
                true);
            await SendAsync(new MsgLeagueOpt
            {
                Action = MsgLeagueOpt.LeagueOpt.MyUnion,
                Identity = Union.Identity,
                Param = Union.Identity == KingdomManager.Kingdom?.Identity ? 1u : 0
            });
            await SendAsync(new MsgOverheadLeagueInfo
            {
                Data = new MsgOverheadLeagueInfo.MsgUnionRank
                {
                    Target = Identity,
                    Name = Union.Name,
                    IsCountryLeagueMember = (uint)(KingdomManager.Kingdom?.Identity == Union.Identity ? 1 : 0),
                    IdLeague = Union.Identity,
                    IsLeagueLeader = (byte)(Union.IsLeader(Identity) ? 1 : 0),
                    IdServer = OriginServer.ServerId
                }
            });
        }

        #endregion

        #region Kingdom War

        public uint GoldBricks { get; set; }

        public Task AddGoldBricksAsync(uint amount)
        {
            GoldBricks += amount;
            return SynchroAttributesAsync(ClientUpdateType.UnionContributionValue, GoldBricks);
        }

        public Task ResetGoldBricksAsync()
        {
            GoldBricks = 0;
            return SynchroAttributesAsync(ClientUpdateType.UnionContributionValue, GoldBricks);
        }

        #endregion
    }
}