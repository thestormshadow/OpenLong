using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.States.Realm
{
    public sealed class UnionMember
    {
        private readonly DbLeagueMember leagueMember;
        private readonly ConcurrentDictionary<KingdomOfficial.OfficialPosition, KingdomOfficial> officialPosition = new();

        public UnionMember(DbLeagueMember leagueMember, DbUser user, IEnumerable<DbOfficialPosition> officialPositions)
        {
            this.leagueMember = leagueMember;
            foreach (var of in officialPositions)
            {
                var official = new KingdomOfficial(of);
                officialPosition.TryAdd(official.OfficialType, official);
            }
            Name = user.Name;
            Level = user.Level;
            BattlePower = 0;
            Mesh = user.Mesh;
            Profession = user.Profession;
        }

        public UnionMember(DbLeagueMember leagueMember, Character user)
        {
            this.leagueMember = leagueMember;
            Name = user.Name;
            Level = user.Level;
            BattlePower = (uint)user.BattlePower;
            Mesh = user.Mesh;
            Profession = user.Profession;
        }

        public UnionMember(DbLeagueMember leagueMember, KingdomOfficial officialPosition = null)
        {
            this.leagueMember = leagueMember;
            if (officialPosition != null)
            {
                this.officialPosition.TryAdd(officialPosition.OfficialType, officialPosition);
            }
        }

        public uint Identity => this.leagueMember.UserId;
        public string Name { get; set; }
        public byte Level { get; set; }
        public uint BattlePower { get; set; }
        public uint Mesh { get; set; }
        public NobilityRank NobilityRank => NobilityManager?.GetRanking(Identity) ?? NobilityRank.Serf;
        public ushort Profession { get; set; }

        public uint UnionId => leagueMember?.LeagueId ?? 0;

        public bool IsOnline => RoleManager.GetUser(Identity) != null;
        public Character User => RoleManager.GetUser(Identity);

        public bool SalaryFlag
        {
            get => leagueMember.SalaryFlag != 0;
            set => leagueMember.SalaryFlag = (byte)(value ? 1 : 0);
        }

        public KingdomOfficial.OfficialPosition OfficialType => UnionId == KingdomManager.Kingdom?.Identity ?
            officialPosition.Values.Select(x => x.OfficialType).OrderBy(x => x).FirstOrDefault(KingdomOfficial.OfficialPosition.Member) : KingdomOfficial.OfficialPosition.Member;

        public KingdomOfficial.OfficialPositionFlag OfficialTypeFlag
        {
            get
            {
                if (UnionId == KingdomManager.Kingdom?.Identity)
                {
                    KingdomOfficial.OfficialPositionFlag flag = 0;
                    foreach (var o in officialPosition.Values)
                    {
                        flag |= o.OfficialTypeFlag;
                    }
                    return flag;
                }
                return 0;
            }
        }

        public bool IsOfficialType(KingdomOfficial.OfficialPosition position)
        {
            return officialPosition.ContainsKey(position);
        }

        public async Task<bool> SetOfficialAsync(KingdomOfficial.OfficialPosition officialPosition)
        {
            if (this.officialPosition.ContainsKey(officialPosition))
            {
                return false;
            }

            DbOfficialPosition position = new DbOfficialPosition
            {
                LeagueId = leagueMember.LeagueId,
                PlayerId = leagueMember.UserId,
                OfficialType = (ushort)officialPosition,
                OfficialTime = uint.Parse(DateTime.Now.ToString("yyMMdd"))
            };

            if (!await ServerDbContext.CreateAsync(position))
            {
                return false;
            }

            this.officialPosition.TryAdd(officialPosition, new KingdomOfficial(position));

            Character user = User;
            if (user != null)
            {
                await user.SendUnionAsync();
            }
            return true;
        }

        public Task<bool> SaveAsync() => ServerDbContext.UpdateAsync(leagueMember);
        public Task<bool> DeleteAsync() => ServerDbContext.DeleteAsync(leagueMember);
        public Task DeleteAsync(ServerDbContext ctx)
        {
            ctx.LeagueMembers.Remove(leagueMember);
            return Task.CompletedTask;
        }

        public enum ContributionPosition
        {
            None,
            Corporal,
            Decurion,
            Centurion,
            Sergeant,
            StaffSergeant,
            MasterSergeant,
            DeputyGeneral,
            AssistantGenera,
            General,
            ChiefofStaff,
            ChariotsandCava,
            FlyingCavalryGe,
            GeneralinChief,
        }
    }
}
