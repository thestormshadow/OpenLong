using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Realm;
using Long.Kernel.States.User;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class KingdomManager
    {
        private static readonly ILogger logger = Log.ForContext<KingdomManager>();

        protected KingdomManager() { }

        private static readonly ConcurrentDictionary<uint, Union> Unions = new();

        public static Union Kingdom { get; private set; }
        private static List<DbLeagueContribute> leagueContributes = new();
        private static Dictionary<uint, DbTokenType> TokenTypes = new();
        private static Dictionary<uint, DbOfficialType> OfficialTypes = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Kingdom is being initialized!");

            var leagues = await UnionRepository.GetLeaguesAsync();
            foreach (var dbLeague in leagues)
            {
                var league = await Union.CreateAsync(dbLeague);
                if (league == null)
                {
                    logger.Error("Could not load Union {0} {1}", dbLeague.Id, dbLeague.Name);
                    continue;
                }
                AddUnion(league);
            }

            logger.Information("Kingdom is initialized with {0} Unions", Unions.Count);

            foreach (var token in await UnionRepository.GetTokenTypesAsync())
            {
                TokenTypes.TryAdd(token.Id, token);
            }

            foreach (var ot in await UnionRepository.GetOfficialTypesAsync())
            {
                OfficialTypes.TryAdd(ot.Id, ot);
            }

            leagueContributes.AddRange(await UnionRepository.GetContributeListAsync());

            Kingdom = Unions.Values.FirstOrDefault(x => x.CountryDate != 0);
        }

        public static bool AddUnion(Union union)
        {
            return Unions.TryAdd(union.Identity, union);
        }

        public static Union GetUnion(uint identity)
        {
            return Unions.TryGetValue(identity, out var union) ? union : null;
        }

        public static Union GetUnion(string name)
        {
            return Unions.Values.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public static void RemoveUnion(uint identity) 
        {
            Unions.TryRemove(identity, out _);
        }

        public static List<DbOfficialType> GetOfficialsTypeByType(byte type)
        {
            return OfficialTypes.Values.Where(x => x.Id / 1000 == type).ToList();
        }

        public static List<DbOfficialType> GetOfficialTypesBySubtype(ushort subtype)
        {
            return OfficialTypes.Values.Where(x => x.Id / 10 == subtype).ToList();
        }

        public static DbTokenType GetTokenType(uint id)
        {
            return TokenTypes.TryGetValue(id, out var token) ? token : null;
        }

        public static uint GetLeagueContributeLevel(uint contribution)
        {
            foreach (var c in leagueContributes.OrderByDescending(x => x.NeedContribute))
            {
                if (contribution >= c.NeedContribute)
                {
                    return c.Level;
                }
            }
            return 0;
        }

        public static async Task<bool> CreateNewUnionAsync(Character user)
        {
            if (user?.Syndicate == null || user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (user.Syndicate.LeagueId != 0)
            {
                return false;
            }

            Union union = await Union.CreateAsync(user.SyndicateName, user.Syndicate);
            if (union == null)
            {
                return false;
            }

            foreach (var member in user.Syndicate.GetMembers().Select(x => x.User))
            {
                await member.LoadUnionAsync();
                await member.Screen.SynchroScreenAsync();
            }
            return true;
        }

        public static async Task<bool> CreateNewUnionAsync(Character user, string name)
        {
            if (user?.Syndicate == null || user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
            {
                return false;
            }

            if (user.Syndicate.LeagueId != 0)
            {
                return false;
            }

            Union union = await Union.CreateAsync(name, user);
            if (union == null)
            {
                return false;
            }

            foreach (var member in user.Syndicate.GetMembers().Select(x => x.User))
            {
                await member.LoadUnionAsync();
                await member.Screen.SynchroScreenAsync();
            }
            return true;
        }

        public static UnionMember GetUnionMember(uint identity)
        {
            foreach (var union in Unions.Values)
            {
                var member = union.QueryMember(identity);
                if (member != null)
                {
                    return member;
                }
            }
            return null;
        }

        public static DbOfficialType GetOfficialType(uint identity)
        {
            return OfficialTypes.TryGetValue(identity, out var value) ? value : null;
        }

        public static Task SubmitAllegianceListAsync(Character user, int page)
        {
            const int ipp = 3;
            MsgLeagueAllegianceList msg = new MsgLeagueAllegianceList
            {
                Page = (ushort)page,
                PageCount = (ushort)(Unions.Count / ipp + 1),
                Param = 1
            };
            foreach (var union in Unions.Values
                .Skip(ipp * page)
                .Take(ipp))
            {
                msg.Data.Add(new MsgLeagueAllegianceList.UnionData
                {
                    Identity = union.Identity,
                    Name = union.Name,
                    LeaderName = union.Leader?.Name ?? StrNone,
                    Bricks = union.Brick,
                    Funds = union.Money,
                    RecruitDeclaration = union.Declaration
                });
            }
            return user.SendAsync(msg);
        }

        public static Task SubmitTokenTypesAsync(Character user)
        {
            if (user.Union == null)
            {
                return Task.CompletedTask;
            }

            var tokens = TokenTypes.Values.Select(x => new MsgLeagueOrderStatus.Token
            {
                Type = x.Id
            }).ToArray();

            MsgLeagueOrderStatus msg = new MsgLeagueOrderStatus
            {
                Data = new MsgLeagueOrderStatus.GroupToken
                {
                    Count = (uint)tokens.Length,
                    Tokens = tokens
                }
            };
            return user.SendAsync(msg);
        }

        public static List<ISyndicate> GetUnionSyndicates(uint leagueId)
        {
            if (SyndicateManager == null)
            {
                return new();
            }
            return SyndicateManager.GetByLeague(leagueId);
        }

        public static Task SubmitSyndicateListAsync(Character user, Union union, int page)
        {
            const int ipp = 15;
            var syndicates = GetUnionSyndicates(union.Identity);
            MsgLeagueSynList msg = new MsgLeagueSynList
            {
                Param = 1,
                Count = syndicates.Count
            };
            foreach (var syndicate in syndicates
                .Skip(page * ipp)
                .Take(ipp))
            {
                msg.Data.Add(new MsgLeagueSynList.SyndicateData
                {
                    Identity = syndicate.Identity,
                    Name = syndicate.Name,
                    Silvers = syndicate.Money,
                    Level = syndicate.Level,
                    Members = (ushort)syndicate.MemberCount,
                    LeaderName = syndicate.Leader?.UserName ?? StrNone
                });
            }
            msg.From = (ushort)msg.Data.Count;
            return user.SendAsync(msg);
        }

        public static Task SubmitTop3UnionsAsync(Character user, int pageIndex)
        {
            const int ipp = 3;
            MsgLeagueRank msg = new MsgLeagueRank
            {
                Action = MsgLeagueRank.RankMode.Top3Union,
                PageCount = 3,
                Param = 1
            };
            foreach (var union in Unions.Values
                .OrderByDescending(x => x.Brick)
                .Skip(pageIndex * ipp)
                .Take(ipp))
            {
                msg.Data.Add(new MsgLeagueRank.UnionData
                {
                    Bricks = union.Brick,
                    LeaderName = union.Leader?.Name ?? StrNone,
                    Name = union.Name,
                    ServerId = RealmManager.ServerIdentity
                });
            }
            return user.SendAsync(msg);
        }

        public static Task SubmitGlobalUnionRankAsync(Character user, int pageIndex)
        {
            const int ipp = 10;
            var displayUnions = Unions.Values; // TODO cross server union display
            MsgLeagueRank msg = new MsgLeagueRank
            {
                Action = MsgLeagueRank.RankMode.General,
                Page = (ushort)pageIndex,
                Param = 1,
                Count = displayUnions.Count
            };
            foreach (var union in displayUnions
                .Skip(pageIndex * ipp)
                .Take(ipp))
            {
                msg.Data.Add(new MsgLeagueRank.UnionData
                {
                    Bricks = union.Brick,
                    LeaderName = union.Leader?.Name ?? StrNone,
                    Name= union.Name,
                    ServerId = RealmManager.ServerIdentity
                });
            }
            msg.PageCount = msg.Data.Count;
            return user.SendAsync(msg);
        }

        public static async Task SetKingdomAsync(Union union)
        {
            // displace old kingdom
            if (Kingdom != null)
            {
                Kingdom.CountryDate = 0;
                await Kingdom.SaveAsync();
            }

            Kingdom = union;
            Kingdom.CountryDate = uint.Parse(DateTime.Now.ToString("yyMMdd"));

            await Kingdom.SaveAsync();
            await Kingdom.BroadcastInfoAsync();            
        }

        public static async Task OnTimerAsync()
        {
            DateTime now = DateTime.Now;
            if (now.Hour == 22 
                && now.Minute == 0 
                && now.Second == 0)
            { 
                var top1Union = Unions.Values.OrderByDescending(x => x.Brick).FirstOrDefault();
                if (top1Union != null
                    && top1Union.Identity != Kingdom?.Identity)
                {
                    // switch kingdoms
                    await SetKingdomAsync(top1Union);
                }
            }
        }
    }
}
