using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Service;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Module.Qualifying.Network;
using Long.Shared;
using Serilog;
using static Long.Kernel.States.User.Character;
using Long.Network.Packets;
using Long.Kernel;
using Long.Kernel.Processors;
using Org.BouncyCastle.Asn1.X509;
using Long.Kernel.Modules.Systems.Qualifier;
using static Long.Module.Qualifying.Network.MsgQualifierWitness;

namespace Long.Module.Qualifying.States.UserQualifier
{
	public sealed class ArenaQualifierUserMatch
    {
		private static readonly ILogger logger = Log.ForContext<ArenaQualifierUserMatch>();

		public enum MatchStatus
        {
            Awaiting,
            Running,
            Finished,
            ReadyToDispose,
            GiveUp
        }

        private readonly TimeOut startMatch = new();
        private readonly TimeOut leaveMap = new();
        private readonly TimeOut confirmation = new();
        private readonly TimeOut matchTime = new();

        private readonly List<uint> wavers = new();

        public GameMap Map { get; private set; }
        public uint Winner { get; private set; }

        public bool Accepted1 { get; set; }
        public Character Player1 { get; private set; }
        public int Points1 { get; set; }
        public int Cheers1 { get; set; }
        public ArenaQualifierUser Object1 { get; set; }

        public bool Accepted2 { get; set; }
        public Character Player2 { get; private set; }
        public int Points2 { get; set; }
        public int Cheers2 { get; set; }
        public ArenaQualifierUser Object2 { get; set; }

        public uint MapIdentity { get; private set; }

        public MatchStatus Status { get; private set; } = MatchStatus.Awaiting;

        public bool InvitationExpired => confirmation.IsActive() && confirmation.IsTimeOut();
        public bool IsRunning => Status == MatchStatus.Running;

        public bool IsAttackEnable => startMatch.IsActive() && startMatch.IsTimeOut() && Status == MatchStatus.Running;
        public bool TimeOut => matchTime.IsActive() && matchTime.IsTimeOut();

        private uint idGiveUp;

        ~ArenaQualifierUserMatch()
        {
            if (MapIdentity > 0)
            {
                ArenaQualifier.MapIdentityGenerator.ReturnIdentity(MapIdentity);
            }
        }

        public async Task<bool> CreateAsync(Character user1, ArenaQualifierUser obj1, Character user2,
                                            ArenaQualifierUser obj2)
        {
            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return false;
            }

            if (!qualifier.IsAllowedToJoin(user1) || !qualifier.IsAllowedToJoin(user2))
            {
                return false;
            }

            MapIdentity = (uint)ArenaQualifier.MapIdentityGenerator.GetNextIdentity;

            Player1 = user1;
            Object1 = obj1;
            Player2 = user2;
            Object2 = obj2;

            var msg = new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Countdown,
                Identity = (uint)ArenaStatus.WaitingInactive
            };

            await Player1.SendAsync(msg);
            await Player2.SendAsync(msg);

            confirmation.Startup(60);
            return true;
        }

        public async Task<bool> StartAsync()
        {
            confirmation.Clear();

            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return false;
            }

            DbDynamap dynaMap;
            Map = new GameMap(dynaMap = new DbDynamap
            {
                Identity = MapIdentity,
                Name = "ArenaQualifier",
                Description = $"{Player1.Name} x {Player2.Name}`s map",
                Type = (uint)qualifier.Map.Type,
                OwnerIdentity = Player1.Identity,
                LinkMap = 1002,
                LinkX = 300,
                LinkY = 278,
                MapDoc = qualifier.Map.MapDoc,
                OwnerType = 1
            });

            if (!await Map.InitializeAsync())
            {
                logger.Error("Could not initialize map for arena qualifier!!");
                return false;
            }

             await MapManager.AddMapAsync(Map);

            if (!await PrepareAsync(Player1, Object1, Player2, Object2))
            {
                await FinishAsync(Player1, Player2);
                return false;
            }

            if (!await PrepareAsync(Player2, Object2, Player1, Object1))
            {
                await FinishAsync(Player2, Player1);
                return false;
            }

            await MoveToMapAsync(Player1, Player2);
            await MoveToMapAsync(Player2, Player1);

            Status = MatchStatus.Running;
            startMatch.Startup(11);
            return true;
        }

        private async Task<bool> PrepareAsync(Character sender, ArenaQualifierUser senderObj, Character target,
                                              ArenaQualifierUser targetObj)
        {
            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return false;
            }

            if (!qualifier.IsAllowedToJoin(sender))
            {
                return false;
            }

            await sender.DetachAllStatusAsync();

            if (!sender.IsAlive)
            {
                await sender.RebornAsync(false, true);
            }

            if (!sender.Map.IsRecordDisable())
            {
                await sender.SavePositionAsync(sender.MapIdentity, sender.X, sender.Y);
            }

            senderObj.PreviousPkMode = sender.PkMode;
            await sender.SetPkModeAsync(PkModeType.FreePk);
            return true;
        }

        public async Task MoveToMapAsync(Character sender, Character target)
        {
            int x = 32 + await RandomService.NextAsync(37);
            int y = 32 + await RandomService.NextAsync(37);

            await sender.FlyMapAsync(MapIdentity, x, y);

            await sender.SendAsync(new MsgQualifierInteractive
			{
                Interaction = MsgQualifierInteractive.InteractionType.StartTheFight,
                Identity = target.Identity,
                Rank = new ArenaQualifier().GetPlayerRanking(target.Identity),
				Name = target.Name,
                Level = target.Level,
                Profession = target.Profession,
                Points = (int)target.QualifierPoints
            });

            await sender.SendAsync(new MsgQualifierScore
            {
                Identity1 = Player1.Identity,
                Name1 = Player1.Name,
                Damage1 = Points1,

                Identity2 = Player2.Identity,
                Name2 = Player2.Name,
                Damage2 = Points2
            });

            await sender.SendAsync(new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Match,
                Option = 5
            });

            await sender.SetAttributesAsync(ClientUpdateType.Hitpoints, sender.MaxLife);
            await sender.SetAttributesAsync(ClientUpdateType.Mana, sender.MaxMana);
            await sender.SetAttributesAsync(ClientUpdateType.Stamina, sender.MaxEnergy);
            await sender.ClsXpValAsync();
        }

        public async Task DestroyAsync(uint idDisconnect = 0, bool notStarted = false)
        {
            Character temp = RoleManager.GetUser(Player1.Identity);
            if (Player1 != null && Player1.Identity != idDisconnect && temp != null)
            {
                if (Player1 != null && Player1.MapIdentity == Map?.Identity)
                {
                    if (!Player1.IsAlive)
                    {
                        await Player1.RebornAsync(false, true);
                    }
                    else
                    {
                        await Player1.DetachBadlyStatusAsync();
                        await Player1.SetAttributesAsync(ClientUpdateType.Hitpoints, Player1.MaxLife);
                        await Player1.SetAttributesAsync(ClientUpdateType.Mana, Player1.MaxMana);
                    }

                    await Player1.FlyMapAsync(Player1.RecordMapIdentity, Player1.RecordMapX,
                                              Player1.RecordMapY);
                }

                await Player1.SetPkModeAsync(Object1.PreviousPkMode);

                await Player1.SendAsync(new MsgQualifierInteractive
                {
                    Interaction = MsgQualifierInteractive.InteractionType.EndDialog,
                    Option = Winner == Player1.Identity ? 1 : 0,
                    Identity = Player1.Identity,
					Name = Player1.Name,
                    Rank = new ArenaQualifier().GetPlayerRanking(Player1.Identity),
					Points = (int)Player1.QualifierPoints,
                    Level = Player1.Level,
                    Profession = Player1.Profession
                });
            }

            temp = RoleManager.GetUser(Player2.Identity);
            if (Player2 != null && Player2.Identity != idDisconnect && temp != null)
            {
                //if (!notStarted)
                {
                    if (Player2 != null && Player2.MapIdentity == Map?.Identity)
                    {
                        if (!Player2.IsAlive)
                        {
                            await Player2.RebornAsync(false, true);
                        }
                        else
                        {
                            await Player2.DetachBadlyStatusAsync();
                            await Player2.SetAttributesAsync(ClientUpdateType.Hitpoints, Player2.MaxLife);
                            await Player2.SetAttributesAsync(ClientUpdateType.Mana, Player2.MaxMana);
                        }

                        await Player2.FlyMapAsync(Player2.RecordMapIdentity, Player2.RecordMapX,
                                                  Player2.RecordMapY);
                    }

                    await Player2.SetPkModeAsync(Object2.PreviousPkMode);
                }

                await Player2.SendAsync(new MsgQualifierInteractive
                {
                    Interaction = MsgQualifierInteractive.InteractionType.EndDialog,
                    Option = Winner == Player2.Identity ? 1 : 0,
                    Identity = Player2.Identity,
                    Name = Player2.Name,
                    Rank = new ArenaQualifier().GetPlayerRanking(Player2.Identity),
					Points = (int)Player2.QualifierPoints,
                    Level = Player2.Level,
                    Profession = Player2.Profession
                });
            }

            if (Map != null)
            {
                foreach (var user in Map.QueryRoles(x => x is Character).Cast<Character>())
                {
                    try
                    {
                        await user.FlyMapAsync(user.RecordMapIdentity, user.RecordMapX, user.RecordMapY);
                        await user.SendAsync(new MsgQualifierWitness
                        {
                            Action = WitnessAction.Leave
                        });
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, ex.Message);
                    }
                }

                await MapManager.RemoveMapAsync(Map.Identity);
            }

            ArenaQualifier.MapIdentityGenerator.ReturnIdentity(MapIdentity);
            Status = MatchStatus.ReadyToDispose;
        }

        public void DoGiveUp(uint loser)
        {
            Status = MatchStatus.GiveUp;
            idGiveUp = loser;
        }

        public Task FinishAsync()
        {
            if (Points1 == 0 && Points1 == Points2)
            {
                return DrawAsync();
            }

            return Points1 > Points2 ? FinishAsync(Player1, Player2) : FinishAsync(Player2, Player1);
        }

        public async Task FinishAsync(Character winner, Character loser, uint disconnect = 0)
        {
            bool force = Status != MatchStatus.Running;

            if (Status == MatchStatus.GiveUp && Map != null)
            {
                force = false;
            }

            winner ??= Player1.Identity == loser.Identity ? Player2 : Player1;

            Status = MatchStatus.Finished;
            leaveMap.Startup(3);

            Winner = winner.Identity;

            winner.QualifierPoints = (uint)(winner.QualifierPoints * 1.03d);
            winner.QualifierDayWins++;
            winner.QualifierHistoryWins++;

            if (winner.QualifierDayWins == 9)
            {
                winner.HonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                winner.HistoryHonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                await winner.UserPackage.AwardItemAsync(723912);
            }

            if (winner.QualifierDayGames == 20)
            {
                winner.HonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                winner.HistoryHonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                await winner.UserPackage.AwardItemAsync(723912);
            }

            winner.QualifierStatus = ArenaStatus.WaitingInactive;
            await ArenaQualifier.SendArenaInformationAsync(winner);

            //LuaScriptManager.Run(winner, null, null, string.Empty, $"UserArenicWins({winner.Identity},{winner.QualifierDayWins})");

            await winner.SendAsync(new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Dialog,
                Option = 1,
                Identity = winner.Identity,
                Name = winner.Name,
                Rank = new ArenaQualifier().GetPlayerRanking(winner.Identity),
				Points = (int)winner.QualifierPoints,
                Level = winner.Level,
                Profession = winner.Profession
            });

            loser.QualifierPoints = (uint)(loser.QualifierPoints * 0.975d);
            loser.QualifierDayLoses++;
            loser.QualifierHistoryLoses++;

            if (loser.QualifierDayGames == 20)
            {
                loser.HonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                loser.HistoryHonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;

                await loser.UserPackage.AwardItemAsync(723912);
            }

            loser.QualifierStatus = ArenaStatus.WaitingInactive;
            await ArenaQualifier.SendArenaInformationAsync(loser);

            await loser.SendAsync(new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Dialog,
                Option = 3,
                Identity = loser.Identity,
                Name = loser.Name,
                Rank = new ArenaQualifier().GetPlayerRanking(loser.Identity),
				Points = (int)loser.QualifierPoints,
                Level = loser.Level,
                Profession = loser.Profession
            });

            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            await qualifier.FinishMatchAsync(this);

            var strWin = "";
            var strLose = "";

            if (winner.SyndicateIdentity != 0)
            {
                strWin += $"({winner.SyndicateName}) ";
            }

            strWin += winner.Name;

            if (loser.SyndicateIdentity != 0)
            {
                strLose += $"({loser.SyndicateName}) ";
            }

            strLose += loser.Name;

			await RoleManager.BroadcastWorldMsgAsync(
				string.Format(StrRes.StrArenicMatchEnd, strWin, strLose, new ArenaQualifier().GetPlayerRanking(winner.Identity)),
                TalkChannel.Qualifier);

            if (force)
            {
                QueueAction(() => DestroyAsync(disconnect, true));
            }

            await winner.SaveAsync();
            await loser.SaveAsync();
        }

        /// <remarks>Both lose!</remarks>
        public async Task DrawAsync()
        {
            bool force = Status != MatchStatus.Running;

            Status = MatchStatus.Finished;
            leaveMap.Startup(3);

            Player1.QualifierPoints = (uint)(Player1.QualifierPoints * 0.975d);
            Player1.QualifierDayLoses++;
            Player1.QualifierHistoryLoses++;

            if (Player1.QualifierDayGames == 20)
            {
                Player1.HonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                Player1.HistoryHonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                await Player1.UserPackage.AwardItemAsync(723912);
            }

            await Player1.SendAsync(new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Dialog,
                Option = 3
            });

            Player2.QualifierPoints = (uint)(Player2.QualifierPoints * 0.975d);
            Player2.QualifierDayLoses++;
            Player2.QualifierHistoryLoses++;

            if (Player2.QualifierDayGames == 20)
            {
                Player2.HonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                Player2.HistoryHonorPoints += ArenaQualifier.TRIUMPH_HONOR_REWARD;
                await Player2.UserPackage.AwardItemAsync(723912);
            }

            await Player2.SendAsync(new MsgQualifierInteractive
            {
                Interaction = MsgQualifierInteractive.InteractionType.Dialog,
                Option = 3
            });

            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            await qualifier.FinishMatchAsync(this);

            var strLose1 = "";
            var strLose2 = "";

            if (Player1.SyndicateIdentity != 0)
            {
                strLose1 += $"({Player1.SyndicateName}) ";
            }

            strLose1 += Player1.Name;

            if (Player2.SyndicateIdentity != 0)
            {
                strLose2 += $"({Player2.SyndicateName}) ";
            }

            strLose2 += Player2.Name;
            
			await RoleManager.BroadcastWorldMsgAsync(string.Format(StrRes.StrArenicMatchDraw, strLose1, strLose2),
                                                TalkChannel.Qualifier);

            if (force)
            {
                QueueAction(() => DestroyAsync(0, true));
            }
        }

        public async Task OnTimerAsync()
        {
            if (confirmation.IsActive() && confirmation.IsTimeOut())
            {
                if (!Accepted1 && !Accepted2)
                {
                    await DrawAsync();
                }
                else if (!Accepted1)
                {
                    await FinishAsync(Player2, Player1);
                }
                else if (!Accepted2)
                {
                    await FinishAsync(Player1, Player2);
                }
                return;
            }

            if (startMatch.IsActive() && !matchTime.IsActive())
            {
                matchTime.Startup(ArenaQualifier.MATCH_TIME_SECONDS);
            }

            if (Status == MatchStatus.GiveUp)
            {
                Character loser = idGiveUp == Player1.Identity ? Player1 : Player2;
                await FinishAsync(null, loser);
                return;
            }

            if (Status == MatchStatus.ReadyToDispose)
            {
                return; // do nothing :]
            }

            if (Status == MatchStatus.Running && TimeOut)
            {
                await FinishAsync();
                return; // finish match now!
            }

            if (leaveMap.IsActive() && leaveMap.IsTimeOut())
            {
                QueueAction(() => DestroyAsync());
            }
        }

        public bool Wave(Character user, uint target)
        {
            if (wavers.Contains(user.Identity))
            {
                return false;
            }

            if (target == Player1.Identity)
            {
                Cheers1 += 1;
            }
            else if (target == Player2.Identity)
            {
                Cheers2 += 1;
            }

            wavers.Add(user.Identity);
            return true;
        }

        public async Task SendBoardAsync()
        {
            await SendAsync(new MsgQualifierScore
            {
                Identity1 = Player1.Identity,
                Name1 = Player1.Name,
                Damage1 = Points1,

                Identity2 = Player2.Identity,
                Name2 = Player2.Name,
                Damage2 = Points2
            });

            var msg = new MsgQualifierWitness
            {
                Action = WitnessAction.Watchers,
                Cheers1 = Cheers1,
                Cheers2 = Cheers2
            };
            foreach (var user in Map.QueryPlayers(x => x.IsArenicWitness()).Take(15))
            {
                msg.Witnesses.Add(new WitnessModel
                {
                    Id = user.Identity,
                    Level = user.Level,
                    Name = user.Name,
                    Mesh = user.Mesh,
                    Profession = user.Profession,
                    Rank = new ArenaQualifier().GetPlayerRanking(user.Identity)
			});
            }
            await SendAsync(msg);
        }

        public Task SendAsync(IPacket msg)
        {
            return Map.BroadcastMsgAsync(msg);
        }

        public void QueueAction(Func<Task> task)
        {
			WorldProcessor.Instance.Queue(Map?.Partition ?? 0, task);
        }
    }
}
