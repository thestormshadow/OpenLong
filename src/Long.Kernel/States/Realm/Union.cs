using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Shared.Helpers;
using System.Collections.Concurrent;
using System.Drawing;

namespace Long.Kernel.States.Realm
{
    public sealed class Union
    {
        private static readonly ILogger logger = Log.ForContext<Union>();
        private static readonly ILogger unionLogger = Logger.CreateLogger("union");

        private readonly ConcurrentDictionary<uint, UnionMember> members = new();

        private Union() { }

        private DbLeague league;
        private ISyndicate syndicate;

        /// <remarks>Loadnig system.</remarks>
        public static async Task<Union> CreateAsync(DbLeague league)
        {
            Union union = new Union
            {
                league = league
            };
            union.syndicate = SyndicateManager.GetSyndicate(league.LeaderSyn);
            if (union.syndicate == null)
            {
                return null;
            }

            var officials = await UnionRepository.GetLeagueOfficialsAsync(league.Id);

            var members = await UnionRepository.GetLeagueMembersAsync(union.Identity);
            foreach (var dbMember in members)
            {
                DbUser user = await UserRepository.FindByIdentityAsync(dbMember.UserId);
                if (user == null)
                {
                    logger.Warning("User {0} not found for League {1} {2}", dbMember.UserId, union.Identity, union.Name);
                    continue;
                }

                UnionMember member = new UnionMember(dbMember, user, officials.Where(x => x.PlayerId == user.Identity));
                union.members.TryAdd(user.Identity, member);
            }
            return union;
        }

        /// <remarks>Guild leader asking to create a Union.</remarks>>
        public static async Task<Union> CreateAsync(string name, Character user)
        {
            if (!RoleManager.IsValidName(name))
            {
                return null;
            }

            if (user.Syndicate == null)
            {
                return null;
            }

            if (user.Syndicate.LeagueId != 0)
            {
                return null;
            }

            if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
            {
                return null;
            }

            if (user.Syndicate.Level < 9)
            {
                return null;
            }

            // TODO check CTF rank
            return await CreateAsync(name, user.Syndicate);
        }

        /// <remarks>Called when Guild War ends to assign a Union from GW Winner.</remarks>
        public static async Task<Union> CreateAsync(string name, ISyndicate syndicate)
        {
            await using var ctx = new ServerDbContext();
            await ctx.Database.BeginTransactionAsync();

            try
            {
                Union union = new Union
                {
                    league = new DbLeague
                    {
                        Name = name,
                        LeaderSyn = syndicate.Identity,
                        Announcement = "This is a new Union.",
                        Declaration = string.Empty,
                        Title = string.Empty
                    },
                    syndicate = syndicate
                };

                ctx.Leagues.Add(union.league);
                await ctx.SaveChangesAsync();

                DbLeagueMember dbLeagueMember = new DbLeagueMember
                {
                    LeagueId = union.league.Id,
                    UserId = syndicate.Leader.UserIdentity
                };
                DbOfficialPosition officialPosition = new DbOfficialPosition
                {
                    LeagueId = union.league.Id,
                    PlayerId = syndicate.Leader.UserIdentity,
                    OfficialType = (ushort)KingdomOfficial.OfficialPosition.Emperor
                };

                KingdomOfficial official = new KingdomOfficial(officialPosition);

                UnionMember unionMember = new UnionMember(dbLeagueMember, official)
                {
                    Name = syndicate.Leader.UserName,
                    Level = syndicate.Leader.Level
                };
                union.members.TryAdd(unionMember.Identity, unionMember);

                ctx.LeagueMembers.Add(dbLeagueMember);
                ctx.OfficialPositions.Add(officialPosition);
                await ctx.SaveChangesAsync();

                if (!KingdomManager.AddUnion(union))
                {
                    throw new Exception("Could not add union to KingdomManager.");
                }

                var synMembers = syndicate.GetMembers().Where(x => x.Rank != ISyndicateMember.SyndicateRank.GuildLeader);
                foreach (var synMember in synMembers)
                {
                    DbLeagueMember dlm = new DbLeagueMember
                    {
                        LeagueId = union.league.Id,
                        UserId = synMember.UserIdentity
                    };

                    UnionMember um = new UnionMember(dlm)
                    {
                        Name = synMember.UserName,
                        Level = synMember.Level
                    };
                    union.members.TryAdd(um.Identity, um);
                    ctx.LeagueMembers.Add(dlm);
                }

                await ctx.SaveChangesAsync();

                await ctx.Database.CommitTransactionAsync();

                syndicate.LeagueId = union.Identity;
                await syndicate.SaveAsync();

                return union;
            }
            catch (Exception ex)
            {
                await ctx.Database.RollbackTransactionAsync();
                logger.Error(ex, "Could not create new Union.");
                return null;
            }
        }

        public uint Identity => league.Id;
        public string Name => league.Name;
        public UnionMember Leader => members.TryGetValue(syndicate.Leader.UserIdentity, out var leader) ? leader : null;

        public uint SyndicateIdentity => syndicate.Identity;
        public ISyndicate Syndicate => syndicate;

        public uint CountryDate
        {
            get => league.CountryDate;
            set => league.CountryDate = value;
        }

        public uint Brick => league.Brick;

        public string Announcement
        {
            get => league.Announcement;
            set => league.Announcement = value;
        }

        public string Declaration
        {
            get => league.Declaration;
            set => league.Declaration = value;
        }

        public string Title
        {
            get => league.Title;
            set => league.Title = value;
        }

        public ulong Money => league.Money;

        public bool IsKingdom => KingdomManager.Kingdom?.Identity == Identity;

        #region Pledge

        public async Task<bool> PledgeAsync(Character user)
        {
            if (user.IsOSUser())
            {
                return false;
            }

            if (user.Syndicate != null) // syndicate must be allegiance!
            {
                return false;
            }

            var dbMember = new DbLeagueMember
            {
                LeagueId = Identity,
                UserId = user.Identity
            };
            UnionMember member = new UnionMember(dbMember, user);

            if (!await ServerDbContext.CreateAsync(dbMember))
            {
                logger.Error("Error when saving UnionMember db instance");
                return false;
            }

            if (members.TryAdd(user.Identity, member))
            {
                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrLeagueJoinSuccess, user.Name, Name), TalkChannel.Talk, Color.White);
                await user.LoadUnionAsync();
                await user.Screen.SynchroScreenAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> PledgeSyndicateAsync(ISyndicate syndicate)
        {
            if (syndicate.LeagueId != 0)
            {
                return false;
            }

            await using var ctx = new ServerDbContext();
            await ctx.Database.BeginTransactionAsync();
            try
            {
                var synMembers = syndicate.GetMembers();
                foreach (var synMember in synMembers)
                {
                    DbLeagueMember dlm = new DbLeagueMember
                    {
                        LeagueId = league.Id,
                        UserId = synMember.UserIdentity
                    };

                    UnionMember um = new UnionMember(dlm)
                    {
                        Name = synMember.UserName,
                        Level = synMember.Level
                    };
                    members.TryAdd(um.Identity, um);
                    ctx.LeagueMembers.Add(dlm);
                }

                await ctx.SaveChangesAsync();
                await ctx.Database.CommitTransactionAsync();

                syndicate.LeagueId = Identity;
                await syndicate.SaveAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return false;
            }

            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynJoinLeague, syndicate.Name, Name), TalkChannel.Talk, Color.White);

            foreach (var member in syndicate.GetMembers().Where(x => x.IsOnline))
            {
                var memberUser = member.User;
                await memberUser.LoadUnionAsync();
                await memberUser.Screen.SynchroScreenAsync();
            }

            return true;
        }

        #endregion

        #region Quit

        public async Task<bool> QuitAsync(Character user)
        {
            if (user.IsOSUser())
            {
                return false;
            }

            if (user.Syndicate != null) // Cannot leave if in syndicate!
            {
                return false;
            }

            if (!members.TryRemove(user.Identity, out var member))
            {
                return false;
            }
            await member.DeleteAsync();

            user.Union = null;
            await user.SendAsync(new MsgLeagueOpt
            {
                Action = MsgLeagueOpt.LeagueOpt.MyUnion,
                Strings = new() { string.Empty }
            });
            await user.SendAsync(new MsgOverheadLeagueInfo
            {
                Data = new MsgOverheadLeagueInfo.MsgUnionRank
                {
                    Target = user.Identity,
                    Name = string.Empty,
                    IdServer = user.OriginServer.ServerId
                }
            });
            return true;
        }

        public async Task<bool> QuitSyndicateAsync(uint syndicateId)
        {
            ISyndicate syndicate = SyndicateManager.GetSyndicate(syndicateId);
            if (syndicate == null)
            {
                return false;
            }

            var syndicates = KingdomManager.GetUnionSyndicates(Identity);
            if (syndicates.All(x => x.Identity != syndicateId))
            {
                return false;
            }

            if (syndicate.LeagueId != Identity)
            {
                return false;
            }

            using var ctx = new ServerDbContext();
            await ctx.Database.BeginTransactionAsync();
            try
            {
                var removeMembers = new List<uint>();
                foreach (var synMember in syndicate.GetMembers())
                {
                    var member = QueryMember(synMember.UserIdentity);
                    if (member == null) continue;


                    removeMembers.Add(member.Identity);
                    await member.DeleteAsync(ctx);
                }
                await ctx.Database.CommitTransactionAsync();

                foreach (var memberId in removeMembers)
                {
                    members.TryRemove(memberId, out var member);

                    if (member != null && member.IsOnline)
                    {
                        var memberUser = member.User;
                        memberUser.Union = null;
                        await memberUser.SendAsync(new MsgLeagueOpt
                        {
                            Action = MsgLeagueOpt.LeagueOpt.MyUnion,
                            Strings = new() { string.Empty }
                        });
                        await memberUser.SendAsync(new MsgOverheadLeagueInfo
                        {
                            Data = new MsgOverheadLeagueInfo.MsgUnionRank
                            {
                                Target = memberUser.Identity,
                                Name = string.Empty,
                                IdServer = memberUser.OriginServer.ServerId
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await ctx.Database.RollbackTransactionAsync();
                logger.Error(ex, ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        #region Member

        public bool IsLeader(uint userId)
        {
            return Leader?.Identity == userId;
        }

        public UnionMember QueryMember(uint id)
        {
            return members.TryGetValue(id, out var member) ? member : null;
        }

        public List<UnionMember> GetOtherMembers()
        {
            return members.Values.Where(x => SyndicateManager?.FindByUser(x.Identity) == null).ToList();
        }

        #endregion

        #region Abdicate

        #endregion

        #region Officials

        public UnionMember FindOfficialByPosition(KingdomOfficial.OfficialPosition position)
        {
            return members.Values.FirstOrDefault(x => x.OfficialTypeFlag.HasFlag(KingdomOfficial.GetOfficialTypeFlag(position)));
        }

        public List<UnionMember> GetOfficials()
        {
            List<UnionMember> officials = new List<UnionMember>
            {
                FindOfficialByPosition(KingdomOfficial.OfficialPosition.Emperor)
            };
            foreach (var officialType in KingdomManager.GetOfficialsTypeByType(2))
            {
                var member = FindOfficialByPosition((KingdomOfficial.OfficialPosition)officialType.Id);
                if (member != null)
                {
                    officials.Add(member);
                }
            }
            return officials;
        }

        public List<UnionMember> GetCoreOfficials()
        {
            List<UnionMember> officials = new ();
            foreach (var officialType in KingdomManager.GetOfficialsTypeByType(2))
            {
                var member = FindOfficialByPosition((KingdomOfficial.OfficialPosition)officialType.Id);
                if (member != null)
                {
                    officials.Add(member);
                }
            }
            return officials;
        }

        public int GetOfficialCount(KingdomOfficial.OfficialPosition officialType)
        {
            return members.Values.Count(x => x.OfficialTypeFlag.HasFlag(KingdomOfficial.GetOfficialTypeFlag(officialType)));
        }

        public async Task<bool> SetOfficialAsync(KingdomOfficial.OfficialPosition officialPosition, Character targetUser)
        {
            var unionMember = QueryMember(targetUser.Identity);
            if (unionMember == null)
            {
                return false;
            }

            return await unionMember.SetOfficialAsync(officialPosition);
        }

        #endregion

        #region Allegiance

        public bool IsAllegiance(uint guildId)
        {
            ISyndicate syndicate = SyndicateManager?.GetSyndicate(guildId);
            if (syndicate == null)
            {
                return false;
            }
            return syndicate.LeagueId == Identity;
        }

        #endregion

        #region Broadcast

        public Task BroadcastAsync(IPacket msg, uint skipUser = 0) => BroadcastAsync(msg.Encode(), skipUser);

        public async Task BroadcastAsync(byte[] msg, uint skipUser = 0)
        {
            foreach (var member in members.Values.Select(x => x.User).Where(x => x != null && x.Identity != skipUser))
            {
                await member.SendAsync(msg);
            }
        }

        public Task BroadcastAsync(string message, TalkChannel channel = TalkChannel.Talk, Color? color = null)
        {
            return BroadcastAsync(message, 0, channel, color);
        }

        public async Task BroadcastAsync(string message, uint skipUser, TalkChannel channel = TalkChannel.Talk, Color? color = null)
        {
            MsgTalk msg = new MsgTalk(channel, color ?? Color.White, message);
            foreach (var user in members.Values
                .Select(x => x.User)
                .Where(x => x != null && x.Identity != skipUser))
            {
                await user.SendAsync(msg);
            }
        }

        #endregion

        public Task SendInfoAsync(Character user)
        {
            return user.SendAsync(new MsgLeagueInfo
            {
                Treasury = 123,
                GoldBricks = Brick,
                Stipend = 0,
                ServerId = RealmManager.ServerIdentity,
                RealmId = RealmManager.RealmIdentity,
                Name = Name,
                Bulletin = Announcement,
                Title = Title,
                PlunderServer = string.Empty,
                InvadingUnion = string.Empty,
                LeaderName = Leader?.Name ?? StrNone
            });
        }

        public async Task BroadcastInfoAsync()
        {
            foreach (var member in members.Values) 
            { 
                Character user = RoleManager.GetUser(member.Identity);
                if (user != null)
                {
                    await user.SendUnionAsync();
                    await SendInfoAsync(user);
                }
            }
        }

        public Task<bool> SaveAsync() => ServerDbContext.UpdateAsync(league);
        public Task<bool> DeleteAsync() => ServerDbContext.DeleteAsync(league);
    }
}
