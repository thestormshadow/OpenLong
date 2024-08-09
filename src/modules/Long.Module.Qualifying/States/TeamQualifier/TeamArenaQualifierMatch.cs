using Long.Database.Entities;
using Long.Kernel;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Processors;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Module.Qualifying.Network;
using Long.Module.Qualifying.Network.States;
using Long.Network.Packets;
using Long.Shared;
using Serilog;
using static Long.Kernel.Service.RandomService;
using static Long.Kernel.States.User.Character;
using static Long.Module.Qualifying.Network.MsgQualifierWitness;

namespace Long.Module.Qualifying.States.TeamQualifier
{
    public sealed class TeamArenaQualifierMatch
    {
        public enum MatchStatus
        {
            Awaiting,
            Running,
            Finished,
            ReadyToDispose
        }

        private readonly Dictionary<uint, bool> accept = new();
        private readonly TimeOut confirmation = new();
        private readonly TimeOut leaveMap = new();
        private static readonly ILogger logger = Log.ForContext<TeamArenaQualifierMatch>();
        private readonly TimeOut matchTime = new();

        private readonly TeamArenaQualifier qualifier;

        private readonly TimeOut startMatch = new();

        private readonly List<uint> wavers = new();

        public TeamArenaQualifierMatch()
        {
            qualifier = EventManager.GetEvent<TeamArenaQualifier>();
        }

        public uint MapIdentity { get; private set; }
        public GameMap Map { get; private set; }
        public MatchStatus Status { get; private set; } = MatchStatus.Awaiting;
        public uint Winner { get; private set; }

        public uint TeamId1 => Team1?.TeamId ?? 0;
        public ITeam Team1 => Company1?.Team;
        public TeamArenaQualifierCompany Company1 { get; private set; }
        public int Score1 { get; set; }
        public int Cheers1 { get; private set; }

        public uint TeamId2 => Team2?.TeamId ?? 0;
        public ITeam Team2 => Company2?.Team;
        public TeamArenaQualifierCompany Company2 { get; private set; }
        public int Score2 { get; set; }
        public int Cheers2 { get; private set; }

        public bool InvitationExpired => confirmation.IsActive() && confirmation.IsTimeOut();
        public bool IsRunning => Status == MatchStatus.Running;

        public bool IsAttackEnable => startMatch.IsActive() && startMatch.IsTimeOut() && Status == MatchStatus.Running;
        public bool TimeOut => matchTime.IsActive() && matchTime.IsTimeOut();

        ~TeamArenaQualifierMatch()
        {
            if (MapIdentity > 0) TeamArenaQualifier.MapIdentityGenerator.ReturnIdentity(MapIdentity);
        }

        public int GetAliveUsers1()
        {
            return Company1.Participants.Values.Count(x => x.IsAlive && x.MapIdentity == MapIdentity);
        }

        public int GetAliveUsers2()
        {
            return Company2.Participants.Values.Count(x => x.IsAlive && x.MapIdentity == MapIdentity);
        }

        public bool UserHasVoted(uint idUser)
        {
            if (!Team1.IsMember(idUser) && !Team2.IsMember(idUser)) return false;

            return accept.ContainsKey(idUser);
        }

        public bool Accept(uint idUser)
        {
            if (!Team1.IsMember(idUser) && !Team2.IsMember(idUser)) return false;

            if (accept.ContainsKey(idUser))
                return false;

            accept.Add(idUser, true);
            return true;
        }

        public bool Deny(uint idUser)
        {
            if (!Team1.IsMember(idUser) && !Team2.IsMember(idUser)) return false;

            if (accept.ContainsKey(idUser))
                return false;

            accept.Add(idUser, false);
            return true;
        }

        public bool AllUsersVoted()
        {
            foreach (Character p in Company1.Participants.Values)
                if (!accept.ContainsKey(p.Identity))
                    return false;
            foreach (Character p in Company2.Participants.Values)
                if (!accept.ContainsKey(p.Identity))
                    return false;
            return true;
        }

        public bool IsAccepted()
        {
            var team1 = false;
            foreach (Character p in Company1.Participants.Values)
            {
                if (!accept.ContainsKey(p.Identity)) continue;

                if (accept[p.Identity])
                {
                    team1 = true;
                    break;
                }
            }

            var team2 = false;
            foreach (Character p in Company2.Participants.Values)
            {
                if (!accept.ContainsKey(p.Identity)) continue;

                if (accept[p.Identity])
                {
                    team2 = true;
                    break;
                }
            }

            return team1 && team2;
        }

        public bool IsDenied(out uint winner, out uint loser)
        {
            var team1 = true;
            foreach (Character p in Company1.Participants.Values)
            {
                if (!accept.ContainsKey(p.Identity)) continue;

                if (accept[p.Identity])
                {
                    team1 = false;
                    break;
                }
            }

            if (team1)
            {
                winner = Company2.Identity;
                loser = Company1.Identity;
                return true;
            }

            var team2 = true;
            foreach (Character p in Company2.Participants.Values)
            {
                if (!accept.ContainsKey(p.Identity)) continue;

                if (accept[p.Identity])
                {
                    team2 = false;
                    break;
                }
            }

            if (team2)
            {
                winner = Company1.Identity;
                loser = Company2.Identity;
                return true;
            }

            winner = 0;
            loser = 0;
            return false;
        }

        private bool ValidateTeam(TeamArenaQualifierCompany company, TeamArenaQualifier qualifier)
        {
            foreach (Character member in company.Team.Members)
            {
                if (!qualifier.IsAllowedToJoin(member))
                    continue;

                company.Participants.TryAdd(member.Identity, member);
            }

            if (company.Participants.IsEmpty) return false;

            return true;
        }

        public async Task<bool> CreateAsync(Character leader1, TeamArenaQualifierCompany company1, Character leader2,
            TeamArenaQualifierCompany company2)
        {
            if (leader1.Team == null || !leader1.Team.IsLeader(leader1.Identity)) return false;

            if (leader2.Team == null || !leader2.Team.IsLeader(leader2.Identity)) return false;

            company1.Participants.Clear();
            company1.PreviousPkModes.Clear();

            company2.Participants.Clear();
            company2.PreviousPkModes.Clear();

            if (!ValidateTeam(company1, qualifier))
            {
                logger.Warning($"Could not validate team! Team {company1.Identity} denied");
                return false;
            }

            if (!ValidateTeam(company2, qualifier))
            {
                logger.Warning($"Could not validate team2! Team {company2.Identity} denied");
                return false;
            }

            Company1 = company1;
            Company2 = company2;

            MapIdentity = (uint)TeamArenaQualifier.MapIdentityGenerator.GetNextIdentity;

            var msg = new MsgTeamArenaInteractive
            {
				Action = MsgQualifierInteractive.InteractionType.Countdown,
				Identity = (uint)ArenaStatus.WaitingInactive
			}.Encode();

            foreach (Character p in Company1.Participants.Values)
            {
                await p.SendAsync(msg);
            }

            foreach (Character p in Company2.Participants.Values)
            {
                await p.SendAsync(msg);
            }

            confirmation.Startup(60);
            return true;
        }

        private async Task<bool> PrepareAsync(TeamArenaQualifierCompany company1)
        {
            foreach (Character member in company1.Participants.Values)
            {
                if (!qualifier.IsAllowedToJoin(member)) return false;

                await member.DetachAllStatusAsync();

                if (!member.IsAlive) await member.RebornAsync(false, true);
                if (!member.Map.IsRecordDisable()) await member.SavePositionAsync(member.MapIdentity, member.X, member.Y);
                company1.PreviousPkModes.TryAdd(member.Identity, member.PkMode);
                await member.SetPkModeAsync(PkModeType.Team);
            }

            return true;
        }

        private async Task MoveToMapAsync(TeamArenaQualifierCompany company, TeamArenaQualifierCompany opponent)
        {
            MsgTeamArenaFightingMemberInfo memberInfo = new()
            {
                TeamId = opponent.Identity,
                Mode = MsgTeamArenaFightingMemberInfo.ShowType.Opponent
            };
            foreach (Character member in opponent.Participants.Values)
                memberInfo.Members.Add(new MsgTeamArenaFightingMemberInfo.TeamMemberInfoStruct
                {
                    UserId = member.Identity,
                    Name = member.Name,
                    Level = member.Level,
                    Mesh = member.Mesh,
                    Profession = member.Profession,
                    Rank = member.TeamQualifierRank,
                    Score = (int)member.TeamQualifierPoints
                });

            foreach (Character sender in company.Participants.Values)
            {
                int x = 32 + await NextAsync(37);
                int y = 32 + await NextAsync(37);

                await sender.FlyMapAsync(MapIdentity, x, y);

                await sender.SendAsync(new MsgTeamArenaInteractive
                {
                    Action = MsgQualifierInteractive.InteractionType.StartTheFight
                });

                await sender.SendAsync(new MsgTeamArenaScore
                {
                    TeamId0 = company.Identity,
                    Rank0 = company.Rank,
                    Name0 = company.Name,
                    Damage0 = 0,

                    TeamId1 = opponent.Identity,
                    Rank1 = opponent.Rank,
                    Name1 = opponent.Name,
                    Damage1 = 0
                });

                await sender.SendAsync(new MsgTeamArenaInteractive
                {
                    Action = MsgQualifierInteractive.InteractionType.Match,
                    Option = MsgQualifierInteractive.QualifierDialogButton.MatchOn
                });

                await sender.SendAsync(memberInfo);

                await sender.SetAttributesAsync(ClientUpdateType.Hitpoints, sender.MaxLife);
                await sender.SetAttributesAsync(ClientUpdateType.Mana, sender.MaxMana);
                await sender.SetAttributesAsync(ClientUpdateType.Stamina, sender.MaxEnergy);
                await sender.ClsXpValAsync();
            }
        }

        public async Task<bool> StartAsync()
        {
            confirmation.Clear();

            DbDynamap dynaMap = new()
            {
                Identity = MapIdentity,
                Name = "TeamArenaQualifier",
                Description = $"{Company1.Name} x {Company2.Name}`s map",
                Type = (uint)qualifier.Map.Type,
                OwnerIdentity = Company1.Identity,
                LinkMap = 1002,
                LinkX = 300,
                LinkY = 278,
                MapDoc = qualifier.Map.MapDoc,
                OwnerType = 1
            };

            Map = new GameMap(dynaMap);

            if (!await Map.InitializeAsync())
            {
                logger.Error("Could not initialize map for arena qualifier!!");
                return false;
            }

            await MapManager.AddMapAsync(Map);

            if (!await PrepareAsync(Company1))
            {
                await FinishAsync(Company1.Identity, Company2.Identity);
                return false;
            }

            if (!await PrepareAsync(Company2))
            {
                await FinishAsync(Company2.Identity, Company1.Identity);
                return false;
            }

            await MoveToMapAsync(Company1, Company2);
            await MoveToMapAsync(Company2, Company1);

            Status = MatchStatus.Running;
            startMatch.Startup(11);
            return true;
        }

        public async Task OnTimerAsync()
        {
            if (confirmation.IsActive())
            {
                if (IsAccepted())
                {
                    await StartAsync();
                    return;
                }
            }
            
            if (confirmation.IsActive() && confirmation.IsTimeOut())
            {
                if (IsAccepted())
                    await StartAsync();
                else
                    await DrawAsync();
                return;
            }

            if (startMatch.IsActive() && !matchTime.IsActive())
            {
                matchTime.Startup(ArenaQualifier.MATCH_TIME_SECONDS);
                return;
            }

            if (Status == MatchStatus.ReadyToDispose) return; // do nothing :]

            if (Status == MatchStatus.Running && TimeOut)
            {
                await FinishAsync();
                return; // finish match now!
            }

            if (leaveMap.IsActive() && leaveMap.IsTimeOut()) QueueAction(() => DestroyAsync());
        }

        public async Task VictoryAsync(TeamArenaQualifierCompany company)
        {
            company.Team.QualifierStatus = ArenaStatus.WaitingInactive;
            foreach (Character participant in company.Participants.Values)
            {
                await VictoryAsync(participant);
            }
        }

        public async Task VictoryAsync(Character user)
        {
            user.TeamQualifierPoints = (uint)(user.TeamQualifierPoints * 1.02d);
            user.TeamQualifierDayWins++;
            user.TeamQualifierHistoryWins++;

            if (user.TeamQualifierDayWins == 9)
            {
                user.HonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                user.HistoryHonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                await user.UserPackage.AwardItemAsync(723912);
            }

            if (user.TeamQualifierDayGames == 20)
            {
                user.HonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                user.HistoryHonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                await user.UserPackage.AwardItemAsync(723912);
            }

            await TeamArenaQualifier.SendArenaInformationAsync(user);

            await user.SendAsync(new MsgTeamArenaInteractive
            {
                Action = MsgQualifierInteractive.InteractionType.Dialog,
                Option = MsgQualifierInteractive.QualifierDialogButton.Win,
                Identity = user.Identity,
                Name = user.Name,
                Rank = user.TeamQualifierRank,
                Points = (int)user.TeamQualifierPoints,
                Level = user.Level,
                Profession = user.Profession
            });
        }

        public async Task DefeatAsync(TeamArenaQualifierCompany company)
        {
            company.Team.QualifierStatus = ArenaStatus.WaitingInactive;
            foreach (Character participant in company.Participants.Values)
            {
                await DefeatAsync(participant);
            }
        }

        public async Task DefeatAsync(Character user)
        {
            user.TeamQualifierPoints = (uint)(user.TeamQualifierPoints * 0.97d);
            user.TeamQualifierDayLoses++;
            user.TeamQualifierHistoryLoses++;

            if (user.TeamQualifierDayGames == 20)
            {
                user.HonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                user.HistoryHonorPoints += TeamArenaQualifier.TRIUMPH_HONOR_REWARD;
                await user.UserPackage.AwardItemAsync(723912);
            }

            await TeamArenaQualifier.SendArenaInformationAsync(user);

            await user.SendAsync(new MsgTeamArenaInteractive
            {
                Action = MsgQualifierInteractive.InteractionType.Dialog,
                Option = MsgQualifierInteractive.QualifierDialogButton.Lose,
                Identity = user.Identity,
                Name = user.Name,
                Rank = user.TeamQualifierRank,
                Points = (int)user.TeamQualifierPoints,
                Level = user.Level,
                Profession = user.Profession
            });
        }

        public Task<bool> FinishAsync()
        {
            if (Score1 > Score2)
                return FinishAsync(TeamId1, TeamId2);
            if (Score2 > Score1) return FinishAsync(TeamId2, TeamId1);
            return DrawAsync();
        }

        public async Task<bool> DrawAsync()
        {
            await DefeatAsync(Company1);
            await DefeatAsync(Company2);

            await qualifier.FinishMatchAsync(this);

            if (Status != MatchStatus.Running) QueueAction(() => DestroyAsync(true));

            Status = MatchStatus.Finished;
            leaveMap.Startup(5);
            return true;
        }

        public async Task<bool> FinishAsync(uint idWinner, uint idLoser)
        {
            var force = Status != MatchStatus.Running;

            TeamArenaQualifierCompany winner;
            TeamArenaQualifierCompany loser;

            Winner = idWinner;

            if (idWinner == Company1.Identity)
            {
                winner = Company1;
                loser = Company2;
            }
            else if (idWinner == Company2.Identity)
            {
                winner = Company2;
                loser = Company1;
            }
            else if (idWinner == 0 && idLoser == 0)
            {
                return await DrawAsync();
            }
            else
            {
                return false;
            }

            await VictoryAsync(winner);
            await DefeatAsync(loser);

            await qualifier.FinishMatchAsync(this);

            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrRes.StrTeamArenaMatchEnd, winner.Name, loser.Name),
                TalkChannel.Qualifier);

            if (force) QueueAction(() => DestroyAsync(true));

            Status = MatchStatus.Finished;
            leaveMap.Startup(5);
            return true;
        }

        private async Task DestroyAsync(TeamArenaQualifierCompany company, bool notStarted = false)
        {
            foreach (Character participant in company.Participants.Values)
            {
                if (!notStarted)
                {
                    if (participant.MapIdentity == Map.Identity)
                        await participant.FlyMapAsync(participant.RecordMapIdentity, participant.RecordMapX,
                            participant.RecordMapY);

                    if (!participant.IsAlive)
                    {
                        await participant.RebornAsync(false, true);
                    }
                    else
                    {
                        await participant.SetAttributesAsync(ClientUpdateType.Hitpoints, participant.MaxLife);
                        await participant.SetAttributesAsync(ClientUpdateType.Mana, participant.MaxMana);
                        await participant.SetAttributesAsync(ClientUpdateType.Stamina, Role.DEFAULT_USER_ENERGY);
                    }

                    if (Company1.PreviousPkModes.TryGetValue(participant.Identity, out PkModeType pkMode))
                        await participant.SetPkModeAsync(pkMode);
                }

                await participant.SendAsync(new MsgTeamArenaInteractive
                {
                    Action = MsgQualifierInteractive.InteractionType.EndDialog,
                    Option = (MsgQualifierInteractive.QualifierDialogButton)(Winner == participant.Team.TeamId ? 1 : 0),
                    Identity = participant.Identity,
                    Name = participant.Name,
                    Rank = participant.QualifierRank,
                    Points = (int)participant.QualifierPoints,
                    Level = participant.Level,
                    Profession = participant.Profession
                });
            }
        }

        public async Task DestroyAsync(bool notStarted = false)
        {
            await DestroyAsync(Company1, notStarted);
            await DestroyAsync(Company2, notStarted);

            if (Map != null)
            {
                foreach (Character user in Map.QueryPlayers(x => x.IsArenicWitness()))
                {
                    await user.FlyMapAsync(user.RecordMapIdentity, user.RecordMapX, user.RecordMapY);
                    await user.SendAsync(new MsgQualifierWitness
                    {
                        Action = WitnessAction.Leave
                    });
                }

                await MapManager.RemoveMapAsync(Map.Identity);
            }

            TeamArenaQualifier.MapIdentityGenerator.ReturnIdentity(MapIdentity);
            Status = MatchStatus.ReadyToDispose;
        }

        public Task SendAsync(IPacket msg)
        {
            return Map.BroadcastMsgAsync(msg);
        }

        public async Task SendBoardAsync()
        {
            await SendAsync(new MsgTeamArenaScore
			{
                TeamId0 = Company1.Identity,
                Name0 = Company1.Name,
                Rank0 = Company1.Rank,
                Damage0 = Score1,

                TeamId1 = Company2.Identity,
                Name1 = Company2.Name,
                Rank1 = Company2.Rank,
                Damage1 = Score2
            });

			MsgQualifierWitness msg = new MsgQualifierWitness
			{
                Action = WitnessAction.Watchers,
                Cheers1 = Cheers1,
                Cheers2 = Cheers2
            };
            foreach (Character user in Map.QueryPlayers(x => x.IsArenicWitness()).Take(15))
                msg.Witnesses.Add(new WitnessModel
                {
                    Id = user.Identity,
                    Level = user.Level,
                    Name = user.Name,
                    Mesh = user.Mesh,
                    Profession = user.Profession,
                    Rank = user.QualifierRank
                });
            await SendAsync(msg);
        }

        public void QueueAction(Func<Task> task)
        {
			WorldProcessor.Instance.Queue(Map?.Partition ?? 0, task);
        }
    }
}