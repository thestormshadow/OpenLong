using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.States;
using Long.Kernel.States.Events;
using Long.Kernel.States.Events.Interfaces;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;
using Long.Module.Qualifying.Repositories;
using Long.Module.Qualifying.States.TeamQualifier;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Shared.Managers;
using Serilog;
using System.Collections.Concurrent;
using static Long.Module.Qualifying.Network.MsgQualifierWitness;

namespace Long.Module.Qualifying.Network.States
{
    public sealed class TeamArenaQualifier : GameEvent, ITeamEvent, IWitnessEvent, ITeamQualifier
	{
        private static readonly ILogger logger = Log.ForContext<TeamArenaQualifier>();

        public const int MIN_LEVEL = 70;
        public const int PRICE_PER_1500_POINTS = 1_500_000;
        public const uint BASE_MAP_ID_U = 940000;
        public const uint TRIUMPH_HONOR_REWARD = 5000;
        public const int MATCH_TIME_SECONDS = 300;

        public static IdentityManager MapIdentityGenerator = new(940000, 949999);

        private Dictionary<uint, QualifierInformation> pastSeasonArenics = new();
        private ConcurrentDictionary<uint, QualifierInformation> arenics = new();
        private readonly ConcurrentDictionary<uint, TeamArenaQualifierCompany> awaitingQueue = new();
        private readonly ConcurrentDictionary<uint, TeamArenaQualifierMatch> matches = new();
        private readonly ConcurrentDictionary<uint, DbArenicHonor> rewards = new();

        private static readonly uint[] StartPoints =
        {
            1500, // over 70
            2200, // over 90
            2700, // over 100
            3200, // over 110
            4000  // over 120
        };

        public TeamArenaQualifier() 
            : base("Team Arena Qualifier", 1000)
        {
        }

        #region Override

        public override EventType Identity => EventType.TeamArenaQualifier;

        public override async Task<bool> CreateAsync()
        {
            Map = MapManager.GetMap(BASE_MAP_ID_U);

            if (Map == null)
            {
                logger.Warning($"Base Map {BASE_MAP_ID_U} not found Team Arena Qualifier");
                return false;
            }

            var dbArenics = await QualifierRepository.GetAsync(DateTime.Now.AddDays(-1), 1);
            foreach (var dbArenic in dbArenics
                .Where(x => x.DayWins > 0 || x.DayLoses > 0)
                .OrderByDescending(x => x.AthletePoint)
                .ThenByDescending(x => x.DayWins)
                .ThenBy(x => x.DayLoses))
            {
				QualifierInformation arenicInformation = new QualifierInformation(dbArenic);
                if (!await arenicInformation.InitializeAsync())
                {
                    continue;
                }

                pastSeasonArenics.TryAdd(arenicInformation.UserId, arenicInformation);

                if (pastSeasonArenics.Count >= 10)
                {
                    break;
                }
            }

            dbArenics = await  QualifierRepository.GetAsync(DateTime.Now, 1);
            foreach (var dbArenic in dbArenics)
            {
				QualifierInformation arenicInformation = new QualifierInformation(dbArenic);
                if (!await arenicInformation.InitializeAsync())
                {
                    continue;
                }

                arenics.TryAdd(arenicInformation.UserId, arenicInformation);
            }

            foreach (DbArenicHonor honor in await QualifierHonorRepository.GetAsync(0))
            {
                rewards.TryAdd(honor.Id, honor);
            }

            return true;
        }

        public override bool IsAllowedToJoin(Role sender)
        {
            if (sender is not Character user)
            {
                return false;
            }

            if (user.Level < MIN_LEVEL)
            {
                return false;
            }

            if (user.Team == null)
            {
                return false;
            }

            if (user.Map.IsPrisionMap()
                || user.Map.IsTeleportDisable()
                || user.Map.IsArenicMapInGeneral())
            {
                return false;
            }

            if (user.TeamQualifierPoints == 0)
            {
                return false;
            }

            if (user.IsInQualifierEvent())
            {
                return false;
            }

            return true;
        }

        public override Task OnEnterAsync(Character sender)
        {
            return SendArenaInformationAsync(sender);
        }

        public override Task OnExitAsync(Character sender)
        {
            return SendArenaInformationAsync(sender);
        }

        public override async Task<bool> OnReviveAsync(Character sender, bool selfRevive)
        {
            if (!selfRevive)
            {
                return false;
            }

            TeamArenaQualifierMatch match = FindMatch(sender.Identity);
            if (match is not { IsRunning: true })
            {
                return false;
            }

            await match.DefeatAsync(sender);
            await sender.SendAsync(new MsgQualifierWitness
            {
                Action = WitnessAction.Leave
            });
            return false;
        }

        public override Task OnBeAttackAsync(Role attacker, Role target, int damage = 0, Magic magic = null)
        {
            if (attacker is not Character || target is not Character)
            {
                return Task.CompletedTask;
            }

            if (attacker.MapIdentity - attacker.MapIdentity % BASE_MAP_ID_U != BASE_MAP_ID_U || attacker.MapIdentity != target.MapIdentity)
            {
                return Task.CompletedTask; // ??? should remove?
            }

            TeamArenaQualifierMatch match = FindMatchByMap(attacker.MapIdentity);
            if (match == null)
            {
                return Task.CompletedTask; // ??? Should remove???
            }

            if (match.Team1.IsMember(attacker.Identity))
            {
                match.Score1 += damage;
            }
            else if (match.Team2.IsMember(attacker.Identity))
            {
                match.Score2 += damage;
            }
            else
            {
                return Task.CompletedTask;
            }

            return match.SendBoardAsync();
        }

        public override bool IsAttackEnable(Role sender, Magic magic = null)
        {
            TeamArenaQualifierMatch match = FindMatch(sender.Identity);
            if (match == null)
            {
                return false;
            }

            return match.IsAttackEnable;
        }

        public override async Task OnBeKillAsync(Role attacker, Role target, Magic magic = null)
        {
            TeamArenaQualifierMatch match = FindMatch(attacker.Identity) ?? FindMatch(target.Identity);
            if (match == null)
            {
                logger.Fatal("Target event kill not in event!!!! {} -> {}", attacker.Identity, target.Identity);
                return; // ????
            }

            if (match.GetAliveUsers1() == 0)
            {
                await match.FinishAsync(match.TeamId2, match.TeamId1);
            }
            else if (match.GetAliveUsers2() == 0)
            {
                await match.FinishAsync(match.TeamId1, match.TeamId2);
            }
        }

        /// <inheritdoc />
        public override async Task OnTimerAsync()
        {
            foreach (TeamArenaQualifierCompany company in awaitingQueue.Values)
            {
                if (company.Team == null || company.Team.MemberCount == 0)
                {
                    await UnsubscribeAsync(company.Identity);
                    continue;
                }

                TeamArenaQualifierCompany targetCompany = await FindTargetAsync(company);
                if (targetCompany == null)
                {
                    continue;
                }

                TeamArenaQualifierMatch match = new();
                if (!await match.CreateAsync(company.Team.Leader, company, targetCompany.Team.Leader, targetCompany) 
                    || !matches.TryAdd(match.MapIdentity, match))
                {
                    await UnsubscribeAsync(company.Identity);
                    await UnsubscribeAsync(targetCompany.Identity);
                    continue;
                }

                awaitingQueue.TryRemove(company.Identity, out _);
                awaitingQueue.TryRemove(targetCompany.Identity, out _);

                match.Team1.QualifierStatus = ArenaStatus.WaitingInactive;
                foreach (Character member in match.Team1.Members)
                {
                    await SendArenaInformationAsync(member);
                }

                match.Team2.QualifierStatus = ArenaStatus.WaitingInactive;
                foreach (Character member in match.Team2.Members)
                {
                    await SendArenaInformationAsync(member);
                }
            }

            foreach (TeamArenaQualifierMatch match in matches.Values)
            {
                if (match.Status == TeamArenaQualifierMatch.MatchStatus.ReadyToDispose)
                {
                    matches.TryRemove(match.MapIdentity, out _);
                    continue;
                }

                await match.OnTimerAsync();
            }
        }

        public override async Task OnDailyResetAsync()
        {
            foreach (var arenic in arenics.Values)
            {
                if (GetReward(arenic.UserId, out var qualifierReward))
                {
                    Character onlineUser = RoleManager.GetUser(arenic.UserId);
                    if (onlineUser != null)
                    {
                        onlineUser.HonorPoints += qualifierReward.DayPrize;
                        onlineUser.HistoryHonorPoints += qualifierReward.DayPrize;

                        onlineUser.TeamQualifierPoints = GetInitialPoints(onlineUser.Level);
                        onlineUser.TeamQualifierDayWins = 0;
                        onlineUser.TeamQualifierDayLoses = 0;
                    }
                    else
                    {
                        await ServerDbContext.ScalarAsync($"UPDATE cq_user " +
                            $"SET athlete_cur_honor_point=athlete_cur_honor_point+{qualifierReward.DayPrize}, " +
                            $"athlete_hisorty_honor_point=athlete_hisorty_honor_point+{qualifierReward.DayPrize}, " +
                            $"team_athlete_point={GetInitialPoints(arenic.Level)}, " +
                            $"team_athlete_season_win=0, team_athlete_season_lost=0 " +
                            $"WHERE id={arenic.UserId} LIMIT 1;");
                    }
                }
            }

            arenics.Clear();
        }

        #endregion

        #region Inscribe

        public async Task<bool> InscribeAsync(Character user)
        {
            if (user.Team == null)
            {
                return false;
            }

            if (HasTeamJoined(user.Team.TeamId))
            {
                return false; // already joined ????
            }

            if (!IsAllowedToJoin(user))
            {
                return false;
            }

            if (EnterQueue(user) != null)
            {
                await user.SignInEventAsync(this);
            }

            return true;
        }

        public async Task<bool> UnsubscribeAsync(uint idUser)
        {
            Character user = RoleManager.GetUser(idUser);

            LeaveQueue(idUser);
            if (user != null)
            {
                if (user.Team != null)
                {
                    user.Team.QualifierStatus = ArenaStatus.NotSignedUp;
                }
            }

            return true;
        }

        #endregion

        #region ITeamEvent

        public bool AllowJoinTeam(Character leader, Character target)
        {
            return true;
        }

        public Task<bool> OnJoinTeamAsync(Character leader, Character target)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> OnLeaveTeamAsync(Character sender)
        {
            TeamArenaQualifierMatch match = FindMatch(sender.Identity);
            if (match == null)
            {
                return true;
            }

            if (match.IsRunning)
            {
                await match.DefeatAsync(sender);
                await sender.SendAsync(new MsgQualifierWitness
                {
                    Action = WitnessAction.Leave
                });

                await sender.FlyMapAsync(sender.RecordMapIdentity, sender.RecordMapX, sender.RecordMapY);
            }

            match.Company1.Participants.TryRemove(sender.Identity, out _);
            match.Company2.Participants.TryRemove(sender.Identity, out _);
            await SendArenaInformationAsync(sender);
            return true;
        }

        #endregion

        #region Team Management

        public int TeamsOnQueue => awaitingQueue.Count;

        private TeamArenaQualifierCompany CreateCompany(Character leader)
        {
            var company = new TeamArenaQualifierCompany(leader.Team)
            {
                PreviousPkModes = new Dictionary<uint, Character.PkModeType>(),
                Participants = new ConcurrentDictionary<uint, Character>()
            };

            return company;
        }

        public TeamArenaQualifierCompany FindInQueue(uint idUser)
        {
            foreach (var awaiting in awaitingQueue.Values)
            {
                foreach (var member in awaiting.Team.Members)
                {
                    if (member.Identity == idUser)
                        return awaiting;
                }
            }
            return null;
        }

        public TeamArenaQualifierCompany EnterQueue(Character user)
        {
            if (FindInQueue(user.Identity) != null)
            {
                return null;
            }

            var company = CreateCompany(user);
            if (company != null && awaitingQueue.TryAdd(company.Identity, company))
            {
                user.Team.QualifierStatus = ArenaStatus.WaitingForOpponent;
                foreach (var participant in user.Team.Members)
                {
                    if (IsAllowedToJoin(participant))
                    {
                        _ = SendArenaInformationAsync(participant).ConfigureAwait(false);
                    }
                }
                return company;
            }
            return null;
        }

        public TeamArenaQualifierCompany LeaveQueue(uint idUser)
        {
            var queue = FindInQueue(idUser);
            if (queue != null && awaitingQueue.TryRemove(queue.Identity, out TeamArenaQualifierCompany result))
            {
                queue.Team.QualifierStatus = ArenaStatus.NotSignedUp;
                foreach (var participant in result.Participants.Values)
                {
                    _ = SendArenaInformationAsync(participant).ConfigureAwait(false);
                }
                return result;
            }
            return null;
        }

        public override async Task OnLogoutAsync(Character user)
        {
            var match = FindMatch(user.Identity);
            if (match != null)
            {
                if (match.Company1.Participants.TryRemove(user.Identity, out _)
                    || match.Company2.Participants.TryRemove(user.Identity, out _))
                {
                    await match.DefeatAsync(user);
                }
            }

            var queue = FindInQueue(user.Identity);
            if (queue != null && queue.Team.MemberCount == 0)
            {
                awaitingQueue.TryRemove(queue.Identity, out _);
            }
        }

        public ArenaStatus QueryUserTeamQualifierStatus(Character user)
        {
            var queue = FindInQueue(user.Identity);
            if (queue != null)
            {
                if (user.Team.IsLeader(user.Identity))
                {
                    return user.TeamQualifierStatus;
                }
                return queue.Team.Leader.TeamQualifierStatus;
            }

            var match = FindMatch(user.Identity);
            if (match != null && user.Team != null)
            {
                if (user.Team.IsLeader(user.Identity))
                {
                    return user.TeamQualifierStatus;
                }
                return user.Team.Leader.TeamQualifierStatus;
            }
            return ArenaStatus.NotSignedUp;
        }

        public bool HasTeamJoined(uint idUser)
        {
            return FindInQueue(idUser) != null || FindMatch(idUser) != null;
        }

        public bool IsInsideMatch(uint idUser)
        {
            return FindMatch(idUser) != null;
        }

        #endregion

        #region Match Management

        public TeamArenaQualifierMatch FindMatch(uint idUser)
        {
            foreach (var match in matches.Values)
            {
                foreach (var member in match.Company1.Participants.Values)
                {
                    if (member.Identity == idUser)
                        return match;
                }

                foreach (var member in match.Company2.Participants.Values)
                {
                    if (member.Identity == idUser)
                        return match;
                }
            }
            return null;
        }

        public TeamArenaQualifierMatch FindMatchByMap(uint idMap)
        {
            return matches.TryGetValue(idMap, out TeamArenaQualifierMatch match) ? match : null;
        }

        public int MatchCount => matches.Count;

        public List<TeamArenaQualifierMatch> QueryMatches(int from, int limit)
        {
            return matches.Values
                .Where(x => x.IsRunning)
                .Skip(from)
                .Take(limit)
                .ToList();
        }

        private async Task UpdateArenicAsync(TeamArenaQualifierCompany match)
        {
            foreach (var player in match.Participants.Values)
            {
                if (!arenics.TryGetValue(player.Identity, out var arenic)
                    || arenic.Date.Date != DateTime.Now.Date)
                {
                    arenics.TryRemove(player.Identity, out _);
                    arenics.TryAdd(player.Identity, arenic = new QualifierInformation(player, 1));
                }

                await player.UpdateTaskActivityAsync(ActivityManager.ActivityType.TeamQualifier);

                arenic.AthletePoint = player.TeamQualifierPoints;
                arenic.DayWins = player.TeamQualifierDayWins;
                arenic.DayLoses = player.TeamQualifierDayLoses;
                arenic.CurrentHonor = player.HonorPoints;
                arenic.HistoryHonor = player.HistoryHonorPoints;
                await arenic.SaveAsync();
            }
        }

        public async Task FinishMatchAsync(TeamArenaQualifierMatch match)
        {
            await UpdateArenicAsync(match.Company1);
            await UpdateArenicAsync(match.Company2);
        }

        public async Task<TeamArenaQualifierCompany> FindTargetAsync(TeamArenaQualifierCompany request)
        {
            var possibleTargets = new List<TeamArenaQualifierCompany>();
            foreach (TeamArenaQualifierCompany target in awaitingQueue.Values
                                                            .Where(x => x.Identity != request.Identity
                                                                        && IsMatchEnable(request.Grade, x.Grade)))
            {
                possibleTargets.Add(target);
            }

            if (possibleTargets.Count == 0)
            {
                return null;
            }

            return possibleTargets[await Long.Kernel.Service.RandomService.NextAsync(possibleTargets.Count) % possibleTargets.Count];
        }

        #endregion

        #region Rankings

        public int GetPlayerRanking(uint idUser)
        {
            var rank = 0;
            foreach (var arenic in arenics.Values
                .Where(x => x.DayWins > 0 || x.DayLoses > 0)
                .OrderByDescending(x => x.AthletePoint)
                .ThenByDescending(x => x.DayWins)
                .ThenBy(x => x.DayLoses))
            {
                if (idUser == arenic.UserId)
                {
                    return rank + 1;
                }

                rank++;
            }

            return 0;
        }

        public bool GetReward(uint idUser, out DbArenicHonor value)
        {
            if (rewards.TryGetValue((uint)GetPlayerRanking(idUser), out DbArenicHonor result))
            {
                value = result;
                return true;
            }

            value = default;
            return false;
        }

        public List<QualifierInformation> GetSeasonRank()
        {
            return pastSeasonArenics.Values
                .OrderByDescending(x => x.AthletePoint)
                .ThenByDescending(x => x.DayWins)
                .ThenBy(x => x.DayLoses)
                .ToList();
        }

        public List<QualifierInformation> GetRanking(int page)
        {
            const int ipp = 10;
            return arenics.Values
                .Where(x => x.DayWins > 0 || x.DayLoses > 0)
                .OrderByDescending(x => x.AthletePoint)
                .ThenByDescending(x => x.DayWins)
                .ThenBy(x => x.DayLoses)
                .Skip(ipp * page)
                .Take(ipp)
                .ToList();
        }

        public int RankCount()
        {
            return arenics.Values.Where(x => x.DayWins > 0 || x.DayLoses > 0).Count();
        }

        #endregion

        #region Witness

        public bool IsWitness(Character user)
        {
            var match = FindMatch(user.Identity);
            if (match != null)
            {
                return false;
            }
            match = FindMatchByMap(user.MapIdentity);
            if (match != null)
            {
                return true;
            }
            return false;

        }

        public Task WatchAsync(Character user, uint target)
        {
            throw new NotImplementedException();
        }

        public Task WitnessExitAsync(Character user)
        {
            throw new NotImplementedException();
        }

        public Task WitnessVoteAsync(Character user, uint target)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Default Data

        public static bool IsMatchEnable(int userGrade, int targetGrade)
        {
            int nDelta = userGrade - targetGrade;
            if (nDelta < 0)
            {
                nDelta *= -1;
            }

            return nDelta < 2;
        }

        public static uint GetInitialPoints(byte level)
        {
            if (level < MIN_LEVEL)
            {
                return 0;
            }

            if (level < 90)
            {
                return StartPoints[0];
            }

            if (level < 100)
            {
                return StartPoints[1];
            }

            if (level < 110)
            {
                return StartPoints[2];
            }

            if (level < 120)
            {
                return StartPoints[3];
            }

            return StartPoints[4];
        }

        #endregion

        #region Socket

        public static Task SendArenaInformationAsync(Character target)
        {
            return target.SendAsync(new MsgTeamArenaHeroData
            {
                Rank = target.TeamQualifierRank,
                CurrentHonor = (int)target.HonorPoints,
                HistoryHonor = (int)target.HistoryHonorPoints,
                Points = (int)target.TeamQualifierPoints,
                TodayVitory = (int)target.TeamQualifierDayWins,
                TodayDefeat = (int)target.TeamQualifierDayLoses,
                TotalVictory = (int)target.TeamQualifierHistoryWins,
                TotalDefeat = (int)target.TeamQualifierHistoryLoses,
                Status = (int)target.TeamQualifierStatus
            });
        }

        #endregion
    }
}
