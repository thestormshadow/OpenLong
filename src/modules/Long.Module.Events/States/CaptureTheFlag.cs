

using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Events;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Module.Events.Network;
using Long.Module.Events.Repositories;
using Long.Shared;
using Long.World.Enums;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Module.Events.Network.MsgSelfSynMemAwardRank;
using static Long.Kernel.Service.RandomService;
using static Long.Kernel.StrRes;
using System.Globalization;
using Long.Kernel.States;
using Long.Kernel.States.World;
using Long.Kernel.States.Magics;

namespace Long.Module.Events.States
{
    public sealed class CaptureTheFlag : SyndicateGameEvent
	{
        private static readonly ILogger logger = Log.ForContext<CaptureTheFlag>();

        private readonly ulong[] MoneyReward =
        {
            0,
            120_000_000,
            100_000_000,
            80_000_000,
            65_000_000,
            50_000_000,
            40_000_000,
            30_000_000,
            20_000_000
        };

        private readonly uint[] EmoneyReward =
        {
            0,
            3000,
            2000,
            1000,
            600,
            500,
            400,
            300,
            200
        };

        private const int CTF_START_TIME = 6210000;
        private const int FLAG_ALIVE_TIME_SECS = 60;
        private const int MIN_DOUBLE_EXPLOIT_SECS = 300;

        private uint doubleExploitFlagId = 0;
        private readonly ConcurrentDictionary<uint, CtfFlag> flags = new();
        private readonly ConcurrentDictionary<uint, DynamicNpc> poles = new();
        private List<FlagRegion> FlagRegions = new();

        private readonly ConcurrentDictionary<uint, SetMeedRank> SyndicateMeed = new();
        private readonly ConcurrentDictionary<uint, MeedRecordRank> MeedRecord = new();
        private readonly ConcurrentDictionary<uint, Score> Scores = new();
        private readonly ConcurrentDictionary<byte, SynCompeteRank> CompeteRankings = new();

        private DateTime startTime;
        private DateTime endTime;

        private readonly TimeOut doubleExploitTimer = new();
        private readonly TimeOutMS rankingRefreshTimer = new();

        private const int CTF_MAP_ID = 2057;

        public CaptureTheFlag()
            : base("Capture The Flag", 1000)
        {
        }

        public override EventType Identity => EventType.CaptureTheFlag;

        public override bool IsActive => Stage == EventStage.Running;

        public override async Task<bool> CreateAsync()
        {
            logger.Information("Loading Capture the flag required data");

            Map = MapManager.GetMap(CTF_MAP_ID);
            if (Map == null)
            {
                logger.Error($"Could not find map {CTF_MAP_ID} for ctf");
                return false;
            }

            var setMeed = await MeedRepository.GetSyndicateMeedAsync((byte)SetMeed.CaptureTheFlag);
            foreach (var meed in setMeed)
            {
                SyndicateMeed.TryAdd(meed.SynId, new SetMeedRank(meed));
            }

            var meedRecord = await MeedRepository.GetUserMeedAsync((byte)SetMeed.CaptureTheFlag);
            foreach (var meed in meedRecord)
            {
                MeedRecordRank meedRank = new(meed);
                await meedRank.CreateAsync();
                MeedRecord.TryAdd(meed.Id, meedRank);
            }

            var competeRank = await CompeteRankRepository.GetSynCompeteRankAsync();
            foreach (var rank in competeRank)
            {
                CompeteRankings.TryAdd(rank.Rank, new SynCompeteRank(rank));
            }

            logger.Information($"{SyndicateMeed.Count} meed set for event");

            var flgs = RoleManager.QueryRoles(x => x is DynamicNpc d && d.IsFlag());
            foreach (var flag in flgs.Cast<DynamicNpc>())
            {
                if (flag.MapIdentity != 5000)
                {
                    await flag.ChangePosAsync(5000, 109, 88);
                }

                flags.TryAdd(flag.Identity, new CtfFlag { Npc = flag });
            }

            logger.Information($"{flags.Count} flags loaded at GM map");

            var tempPoles = Map.QueryRoles(x => x is DynamicNpc d && d.IsCtfFlag());
            foreach (var pole in tempPoles.Cast<DynamicNpc>())
            {
                if (pole.Life < pole.MaxLife)
                {
                    await pole.SetAttributesAsync(ClientUpdateType.Hitpoints, pole.MaxLife);
                }

                poles.TryAdd(pole.Identity, pole);
            }

            logger.Information($"{poles.Count} CTF poles loaded with success");

            FlagRegions = Map.QueryRegions(RegionType.FlagSpawnArea).Select(x => new FlagRegion
            {
                Region = x
            }).ToList();

            return true;
        }

        public override async Task SetMeedAsync(Character user, ulong money, uint emoney)
        {
            if (user?.Syndicate == null)
            {
                // user dont have a syndicate
                return;
            }

            if (user.SyndicateRank != ISyndicateMember.SyndicateRank.GuildLeader)
            {
                // user is no leader
                return;
            }

            if (money == 0 && emoney == 0)
            {
                // ????
                return;
            }

            ISyndicate syndicate = user.Syndicate;
            if (money > 0 && (long)money > syndicate.Money)
            {
                // syndicate has no fund
                return;
            }

            if (emoney > 0 && emoney > syndicate.ConquerPoints)
            {
                // syndicate has no fund
                return;
            }

            syndicate.Money -= (long)money;
            syndicate.ConquerPoints -= emoney;
            await syndicate.SaveAsync();

            SetMeedRank setMeed = SyndicateMeed.GetOrAdd(syndicate.Identity, new SetMeedRank(new DbSetMeed
            {
                SynId = syndicate.Identity,
                Type = (byte)SetMeed.CaptureTheFlag
            }));

            setMeed.Money += money;
            setMeed.Emoney += emoney;
            await setMeed.SaveAsync();

            logger.Information($"{user.Identity},{user.Name},{syndicate.Identity},{syndicate.Name},{money},{setMeed.Money},{emoney},{setMeed.Emoney}");

            if (setMeed.Money > 0 && setMeed.Emoney > 0)
            {
                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynSetMeedBoth, user.Name, syndicate.Name, setMeed.Money, setMeed.Emoney, Name), TalkChannel.System, Color.White);
            }
            else if (setMeed.Money > 0)
            {
                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynSetMeedMoney, user.Name, syndicate.Name, setMeed.Money, Name), TalkChannel.System, Color.White);
            }
            else if (setMeed.Emoney > 0)
            {
                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynSetMeedEmoney, user.Name, syndicate.Name, setMeed.Emoney, Name), TalkChannel.System, Color.White);
            }

            await SubmitSetMeedAsync(user, 0);
        }

        public override async Task OnEnterMapAsync(Character sender)
        {
            if (poles.TryGetValue(doubleExploitFlagId, out var pole))
            {
                await sender.SendAsync(new MsgWarFlag
                {
                    Action = WarFlagType.Location2XBonus,
                    Identity = pole.Identity % 10,
                    Active = 1,
                    MapX = pole.X,
                    MapY = pole.Y
                });
            }
        }

        public override async Task OnExitMapAsync(Character sender, GameMap currentMap)
        {
            await sender.DetachStatusAsync(StatusSet.CTF_FLAG);
            await sender.SendAsync(new MsgWarFlag
            {
                Action = WarFlagType.Initialize
            });
        }

        public override async Task OnBeKillAsync(Role attacker, Role target, Magic magic = null)
        {
            if (target is DynamicNpc dynamicNpc)
            {
                if (!poles.TryGetValue(target.Identity, out _))
                {
                    return;
                }

                var topScore = dynamicNpc.GetTopScore();
                if (topScore != null)
                {
					ISyndicate syndicate = ModuleManager.SyndicateManager.GetSyndicate(topScore.Identity);
					if (syndicate != null && !syndicate.Deleted)
                    {
                        await dynamicNpc.SetOwnerAsync(syndicate.Identity);

                        await syndicate.SendAsync(new MsgWarFlag
                        {
                            Action = WarFlagType.WarBaseDominate,
                            Identity = target.Identity % 10,
                            Active = 1,
                            MapX = target.X,
                            MapY = target.Y
                        });
                    }
                }

                dynamicNpc.ClearScores();
                await dynamicNpc.SetAttributesAsync(ClientUpdateType.Hitpoints, dynamicNpc.MaxLife);
                return;
            }
        }

        public override async Task<RevivePosition> GetRevivePositionAsync(Character sender)
        {
            ushort x = (ushort)(466 + await NextAsync(35));
            ushort y = (ushort)(351 + await NextAsync(35));
            return new RevivePosition(Map.Identity, x, y);
        }

        public override async Task OnTimerAsync()
        {
            DateTime dtNow = DateTime.Now;
            int weekDay = int.Parse((dtNow.DayOfWeek == 0 ? 7 : (int)dtNow.DayOfWeek).ToString(CultureInfo.InvariantCulture));
            int now = int.Parse($"{weekDay}{dtNow:HHmmss}");
            if (Stage == EventStage.Idle)
            {
                const int tolleranceSecs = 15;
                int difference = CTF_START_TIME - now;
                if (difference >= 0 && difference <= tolleranceSecs)
                {
                    await PrepareEventAsync();
                }
            }
            else if (Stage == EventStage.Running)
            {
                if (dtNow > endTime)
                {
                    Stage = EventStage.Ending;
                    return;
                }

                await RefreshFlagsAsync();

                if (doubleExploitTimer.ToNextTime())
                {
                    await RefreshDoubleExploitFlagAsync();
                }

                if (rankingRefreshTimer.ToNextTime())
                {
                    await BroadcastEventRankAsync();
                }
            }
            else if (Stage == EventStage.Ending)
            {
                logger.Information("Ending event!");
                Stage = EventStage.Idle;
                await EndEventAsync();
            }
        }

        public async Task PrepareEventAsync()
        {
            // clear old meeds history
            await ServerDbContext.DeleteRangeAsync(MeedRecord.Values.Select(x => x.GetRecord()).ToList());

            foreach (var pole in poles.Values)
            {
                await pole.SetOwnerAsync(0);
                await pole.SetAttributesAsync(ClientUpdateType.Hitpoints, pole.MaxLife);
            }

            // spawn flags
            await RefreshFlagsAsync(true);

            await RefreshDoubleExploitFlagAsync(true);

            rankingRefreshTimer.Startup(RANK_REFRESH_RATE_MS);

            startTime = DateTime.Now;
            endTime = DateTime.Now.AddHours(1);

            await Map.SetStatusAsync(1, true);

            Stage = EventStage.Running;
        }

        public async Task EndEventAsync()
        {
            await Map.SetStatusAsync(1, false);

            foreach (var flag in flags.Values)
            {
                if (flag.Npc.MapIdentity != 5000)
                {
                    flag.Npc.QueueAction(() => flag.Npc.ChangePosAsync(5000, 88, 88));
                    continue;
                }
            }

            foreach (var player in Map.QueryPlayers(x => x.QueryStatus(StatusSet.CTF_FLAG) != null))
            {
                player.QueueAction(() => player.DetachStatusAsync(StatusSet.CTF_FLAG));
            }

            await Map.BroadcastMsgAsync(new MsgWarFlag
            {
                Action = WarFlagType.Location2XBonus
            });

            await ClearRankingAsync();

            byte position = 1;
            foreach (var syn in Scores.Values
                .OrderByDescending(x => x.Points)
                .Take(8))
            {
                SynCompeteRank rank = CompeteRankings.GetOrAdd(position, new SynCompeteRank(new DbSynCompeteRank
                {
                    Rank = position
                }));
                rank.SyndicateId = syn.SynId;
                rank.Points = (uint)syn.Points;
                rank.Members = (ushort)syn.UserScores.Count;
                await rank.SaveAsync();
                position++;
            }

            await DeliverMeedAsync();
            await DeliverSyndicateRewardAsync();
            Stage = EventStage.Idle;
        }

        public async Task ClearRankingAsync()
        {
            foreach (var rank in CompeteRankings.Values)
            {
                rank.SyndicateId = 0;
                rank.Points = 0;
                rank.Members = 0;
                await rank.SaveAsync();
            }
        }

        public async Task DeliverSyndicateRewardAsync()
        {
            int idx = 1;
            foreach (var score in Scores.Values.OrderByDescending(x => x.Points).Take(8))
            {
                ISyndicate syndicate = ModuleManager.SyndicateManager.GetSyndicate(score.SynId);

                ulong money = MoneyReward[idx];
                uint emoney = EmoneyReward[idx];

                if (idx == 1)
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynCompeteCtfChampion, score.SynName, money, emoney), TalkChannel.Talk, Color.White);
                    if (syndicate != null)
                    {
                        await syndicate.SendAsync(string.Format(StrSynCompeteCtfChampionSyn, money, emoney), 0, Color.White);
                    }
                }
                else if (idx == 2)
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynCompeteCtf2nd, score.SynName, money, emoney), TalkChannel.Talk, Color.White);
                    if (syndicate != null)
                    {
                        await syndicate.SendAsync(string.Format(StrSynCompeteCtf2ndSyn, money, emoney), 0, Color.White);
                    }
                }
                else if (idx == 3)
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynCompeteCtf3rd, score.SynName, money, emoney), TalkChannel.Talk, Color.White);
                    if (syndicate != null)
                    {
                        await syndicate.SendAsync(string.Format(StrSynCompeteCtf3rdSyn, money, emoney), 0, Color.White);
                    }
                }
                else
                {
                    await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynCompeteCtf4th, score.SynName, idx, money, emoney), TalkChannel.Talk, Color.White);
                    if (syndicate != null)
                    {
                        await syndicate.SendAsync(string.Format(StrSynCompeteCtf4thSyn, money, emoney), 0, Color.White);
                    }
                }

                if (syndicate == null)
                {
					logger.Information($"{idx},{score.SynId},{score.SynName},0,{money},{emoney}");
                    idx++;
                    continue;
                }

                syndicate.Money += (long)money;
                syndicate.ConquerPoints += emoney;
                await syndicate.SaveAsync();
                idx++;
            }
        }

        public async Task DeliverMeedAsync()
        {
            List<DbMail> mails = new();
            foreach (var setMeed in SyndicateMeed.Values)
            {
                int synRank = GetSynRank(setMeed.SyndicateId);
                ISyndicate syndicate = ModuleManager.SyndicateManager.GetSyndicate(setMeed.SyndicateId);
                if (syndicate == null)
                {
					logger.Information($"{setMeed.SyndicateId},{setMeed.SyndicateName},0,None,{synRank},0,{setMeed.Money},{setMeed.Emoney}");
                    continue;
                }

                if (!Scores.TryGetValue(setMeed.SyndicateId, out var score))
                {
                    ISyndicateMember randomMember = await syndicate.QueryRandomMemberAsync();
                    if (randomMember == null)
                    {
						logger.Information($"{setMeed.SyndicateId},{setMeed.SyndicateName},0,None,{synRank},0,{setMeed.Money},{setMeed.Emoney}");
                        continue;
                    }

                    mails.Add(CreateMail(randomMember.UserIdentity, syndicate.Name, GetSynRank(syndicate.Identity), 0, setMeed.Money, setMeed.Emoney));
					logger.Information($"{setMeed.SyndicateId},{setMeed.SyndicateName},{randomMember.UserIdentity},{randomMember.UserName},{synRank},0,{setMeed.Money},{setMeed.Emoney}");
                    continue;
                }

                List<UserScore> validScores = new();
                double validPoints = 0;
                foreach (var userScore in score.UserScores.Values)
                {
                    ISyndicateMember member = syndicate.QueryMember(userScore.UserId);
                    if (member != null)
                    {
                        validPoints += userScore.Points;
                        validScores.Add(userScore);
                    }
                }

                ulong remainingMoney = setMeed.Money;
                uint remainingConquerPoints = setMeed.Emoney;
                foreach (var userScore in validScores.OrderByDescending(x => x.Points))
                {
                    double rate = userScore.Points / validPoints;
                    ulong money = (ulong)(setMeed.Money * rate);
                    uint emoney = (uint)(setMeed.Emoney * rate);
                    remainingMoney -= money;
                    remainingConquerPoints -= emoney;

                    logger.Information($"{setMeed.SyndicateId},{setMeed.SyndicateName},{userScore.UserId},{userScore.Name},{synRank},{rate * 100:0.000},{setMeed.Money},{setMeed.Emoney}");
                    mails.Add(CreateMail(userScore.UserId, syndicate.Name, synRank, userScore.Points, money, emoney));

                    MeedRecord.TryAdd(userScore.UserId, new MeedRecordRank(new DbMeedRecord
                    {
                        UserId = userScore.UserId,
                        SynId = syndicate.Identity,
                        Money = money,
                        Emoney = emoney,
                        Type = (byte)SetMeed.CaptureTheFlag,
                        Point = (ushort)userScore.Points
                    }, userScore.Name));
                }
            }
            await ServerDbContext.UpdateRangeAsync(mails);
            await ServerDbContext.UpdateRangeAsync(MeedRecord.Values.Select(x => x.GetRecord()).ToList());
            
            foreach (var setMeed in SyndicateMeed.Values)
            {
                await setMeed.DeleteAsync();
            }
            SyndicateMeed.Clear();
        }

        private static DbMail CreateMail(uint idUser, string synName, int synRank, int userPoints, ulong money, uint emoney)
        {
            return new DbMail
            {
                ReceiverId = idUser,
                SenderName = StrMailSynCompeteCtfSender,
                Title = StrMailSynCompeteCtfRewardTitle,
                Content = string.Format(StrMailSynCompeteCtfRewardContent, synName, userPoints == 0 ? "no" : userPoints, synRank == 0 ? "Unranked" : synRank, money, emoney),
                Money = money,
                ConquerPoints = emoney,
                ExpirationDate = (uint)(UnixTimestamp.Now + 60 * 60 * 24 * 30)
            };
        }

        public async Task RefreshDoubleExploitFlagAsync(bool force = false)
        {
            doubleExploitTimer.Startup(MIN_DOUBLE_EXPLOIT_SECS);

            int rate = await NextAsync(100);
            if (!force && rate > 50)
            {
                return;
            }

            var p = poles.Values.Where(x => x.Identity != doubleExploitFlagId).ToList();
            int baseRate = 100 / p.Count;
            rate = baseRate;
            foreach (var pole in p)
            {
                if (await ChanceCalcAsync(rate))
                {
                    doubleExploitFlagId = pole.Identity;

                    await Map.BroadcastMsgAsync(new MsgWarFlag
                    {
                        Action = WarFlagType.Location2XBonus,
                        Identity = pole.Identity % 10,
                        Active = 1,
                        MapX = pole.X,
                        MapY = pole.Y
                    });
                    break;
                }
            }
        }

        public Task SubmitCurrentSyndicateRankAsync(Character user, int currentPage)
        {
            const int ipp = 5;
            SetMeedRank setMeed = null;
            SyndicateMeed.TryGetValue(user.SyndicateIdentity, out setMeed);

            if (!Scores.TryGetValue(user.SyndicateIdentity, out var synScore))
            {
                return Task.CompletedTask;
            }

            MsgSelfSynMemAwardRank msg = new()
            {
                Action = SynMemAwardRankType.CaptureTheFlagCurrentSynRank,
                Money = setMeed?.Money ?? 0,
                Exploits = (int)(setMeed?.Emoney ?? 0),
                Page = synScore.UserScores.Count,
                Running = 1
            };
            foreach (var rw in synScore.UserScores.Values
                .OrderByDescending(x => x.Points)
                .Skip(currentPage * ipp)
                .Take(ipp))
            {
                msg.ExploitRank.Add(new Exploit
                {
                    Name = rw.Name,
                    Points = rw.Points
                });
            }
            msg.Data = msg.ExploitRank.Count;
            return user.SendAsync(msg);
        }

        public Task SubmitSetMeedAsync(Character user, int currentPage)
        {
            const int ipp = 5;
            SetMeedRank setMeed = null;
            SyndicateMeed.TryGetValue(user.SyndicateIdentity, out setMeed);
            MsgSelfSynMemAwardRank msg = new()
            {
                Action = SynMemAwardRankType.RewardsSetForEvent,
                Money = setMeed?.Money ?? 0,
                Exploits = (int)(setMeed?.Emoney ?? 0),
                Page = SyndicateMeed.Count
            };
            foreach (var rw in SyndicateMeed.Values
                .OrderByDescending(x => x.Emoney)
                .ThenByDescending(x => x.Money)
                .Skip(currentPage * ipp)
                .Take(ipp))
            {
                msg.BestRewardsRank.Add(new BestRewards
                {
                    Name = rw.SyndicateName,
                    Money = rw.Money,
                    ConquerPoints = rw.Emoney
                });
            }
            msg.Data = msg.BestRewardsRank.Count;
            return user.SendAsync(msg);
        }

        public Task SubmitMeedHistoryAsync(Character user, int currentPage)
        {
            if (user.SyndicateIdentity == 0)
            {
                return Task.CompletedTask;
            }

            const int ipp = 4;
            SetMeedRank setMeed = null;
            SyndicateMeed.TryGetValue(user.SyndicateIdentity, out setMeed);
            MsgSelfSynMemAwardRank msg = new()
            {
                Action = SynMemAwardRankType.LastEventRewardsRanking,
                Money = setMeed?.Money ?? 0,
                Exploits = (int)(setMeed?.Emoney ?? 0),
                Page = MeedRecord.Count
            };

            var syndicateMeedRecord = MeedRecord.Values.Where(x => x.SynId == user.SyndicateIdentity).ToList();

            int position = 1;
            MeedRecordRank myRank = null;
            foreach (var meedHistory in syndicateMeedRecord.OrderByDescending(x => x.Points))
            {
                if (meedHistory.PlayerId == user.Identity)
                {
                    myRank = meedHistory;
                    break;
                }
                position++;
            }

            int currentPosition = currentPage * ipp + 1;
            foreach (var rank in syndicateMeedRecord
                .OrderByDescending(x => x.Points)
                .Skip(currentPage * ipp)
                .Take(ipp))
            {
                msg.StatusRank.Add(new EventStatus
                {
                    Rank = currentPosition++,
                    Identity = rank.PlayerId,
                    Name = rank.PlayerName,
                    Points = rank.Points,
                    Money = rank.Money,
                    ConquerPoints = rank.Emoney
                });
            }

            msg.Data = msg.StatusRank.Count;
            return user.SendAsync(msg);
        }

        public Task SubmitWindowRankingAsync(Character user)
        {
            MsgSelfSynMemAwardRank msg = new()
            {
                Action = SynMemAwardRankType.CaptureTheFlagTop8,
                Data = 8 // ranking count, this window will always be 8
            };
            if (Stage == EventStage.Idle)
            {
                int i = 1;
                foreach (var rank in CompeteRankings.Values.OrderBy(x => x.Rank))
                {
                    msg.RewardsRank.Add(new StaticRewardUI
                    {
                        Name = rank.SyndicateName,
                        Members = rank.Members,
                        Points = (int)rank.Points,
                        Money = MoneyReward[rank.Rank],
                        ConquerPoints = EmoneyReward[rank.Rank]
                    });
                    i++;
                }

                for (; i < 9; i++)
                {
                    msg.RewardsRank.Add(new StaticRewardUI
                    {
                        Money = MoneyReward[i],
                        ConquerPoints = EmoneyReward[i++]
                    });
                }
            }
            else
            {
                msg.Running = 1;
                int i = 1;
                foreach (var rank in Scores.Values.OrderByDescending(x => x.Points)
                    .Take(8))
                {
                    msg.RewardsRank.Add(new StaticRewardUI
                    {
                        Name = rank.SynName,
                        Members = rank.UserCount,
                        Points = rank.Points,
                        Money = MoneyReward[i],
                        ConquerPoints = EmoneyReward[i++]
                    });

                    if (rank.UserScores.TryGetValue(user.Identity, out var userScore))
                    {
                        msg.Exploits = userScore.Points;
                    }
                }

                for (; i < 9; i++)
                {
                    msg.RewardsRank.Add(new StaticRewardUI
                    {
                        Money = MoneyReward[i],
                        ConquerPoints = EmoneyReward[i++]
                    });
                }
            }
            return user.SendAsync(msg);
        }

        public int SynUserCount(uint idSyn)
        {
            return Map.QueryRoles(x => x is Character user && user.SyndicateIdentity == idSyn).Count;
        }

        public async Task<bool> PickUpFlagAsync(Character user, uint idFlag)
        {
            if (!flags.TryGetValue(idFlag, out var flag))
            {
                return false;
            }

            if (user.Syndicate == null)
            {
                return true;
            }

            if (flag.UserId != 0)
            {
                return false;
            }

            foreach (var checkFlag in flags.Values)
            {
                if (checkFlag.UserId == user.Identity)
                {
                    checkFlag.UserId = 0;
                    checkFlag.Deliver();
                    break;
                }
            }

            await flag.Npc.ChangePosAsync(5000, 88, 88);

            flag.UserId = user.Identity;
            //flag.Region.Flags.Remove(flag);

            await user.BroadcastRoomMsgAsync(new MsgWarFlag
            {
                Action = WarFlagType.GrabFlagEffect,
                Identity = user.Identity
            }, true);
            await user.AttachStatusAsync(user, StatusSet.CTF_FLAG, 0, FLAG_ALIVE_TIME_SECS, 0);
            await user.SendAsync(new MsgWarFlag
            {
                Action = WarFlagType.GenerateTimer,
                Identity = FLAG_ALIVE_TIME_SECS
            });

            AwardPoints(user, 1);
            return true;
        }

        public async Task<bool> DeliverFlagAsync(Character user)
        {
            const int points = 15;

            if (user.Syndicate == null)
            {
                return false;
            }

            DynamicNpc npc = GetClosePole(user);
            if (npc == null)
            {
                return false;
            }

            CtfFlag flag = null;
            foreach (var checkFlag in flags.Values)
            {
                if (checkFlag.UserId == user.Identity)
                {
                    flag = checkFlag;
                    break;
                }
            }

            if (npc.OwnerIdentity != user.SyndicateIdentity)
            {
                return false;
            }

            await user.DetachStatusAsync(StatusSet.CTF_FLAG);

            if (flag == null)
            {
                return false;
            }

            await user.BroadcastRoomMsgAsync(new MsgWarFlag
            {
                Action = WarFlagType.GrabFlagEffect,
                Identity = user.Identity
            }, true);
            await user.BroadcastRoomMsgAsync(new MsgWarFlag
            {
                Action = WarFlagType.DeliverFlagEffect,
                Identity = user.Identity
            }, true);

            int awardPoints = points;
            if (npc.Identity == doubleExploitFlagId)
            {
                awardPoints *= 2;
            }

            AwardPoints(user, awardPoints);
            return true;
        }

        public Task RefreshFlagsAsync(bool force = false)
        {
            foreach (var flag in flags.Values)
            {
                // if npc is in map
                if (flag.Npc.MapIdentity == CTF_MAP_ID)
                {
                    continue; // nothing to do
                }

                // flag has been pick up?
                if (flag.UserId != 0)
                {
                    var user = Map.QueryRole<Character>(flag.UserId);
                    if (user != null && user.QueryStatus(StatusSet.CTF_FLAG) != null) // user still in map carrying flag
                    {
                        continue;
                    }

                    flag.UserId = 0;

                    // user left map or flag expired
                    //flag.Region.Flags.Remove(flag);
                    flag.Deliver();
                }

                if (!force && !flag.CanRespawn()) // no time over, wait for next tick
                {
                    continue;
                }

                // two threads only... really necessary?
                async Task repositionNpc()
                {
                    FlagRespawnInfo respawnPoint = await GetFlagPositionAsync();
                    if (respawnPoint == default)
                    {
                        if (force)
                        {
                            flag.Deliver();
                        }
                        return; // :( failed, wait next tick
                    }

                    if (await flag.Npc.ChangePosAsync(CTF_MAP_ID, (ushort)respawnPoint.Point.X, (ushort)respawnPoint.Point.Y))
                    {
                        flag.Respawn();
                        //respawnPoint.Region.Flags.Add(flag);
                        //flag.Region = respawnPoint.Region;

                        logger.Debug($"Flag spawned at {Map.Name} {flag.Npc.X},{flag.Npc.Y}");
                    }
                    else
                    {
                        flag.Deliver();
                    }
                }

                flag.Npc.QueueAction(repositionNpc);
            }
            return Task.CompletedTask;
        }

        private async Task<FlagRespawnInfo> GetFlagPositionAsync()
        {
            //var region = FlagRegions.Aggregate((aggr, next) => next.Flags.Count.CompareTo(aggr.Flags.Count) < 0
            //                                                                    ? next
            //                                                                    : aggr);

            //ushort x = (ushort)await NextAsync((int)region.Region.BoundX, (int)(region.Region.BoundX + region.Region.BoundCX));
            //ushort y = (ushort)await NextAsync((int)region.Region.BoundY, (int)(region.Region.BoundY + region.Region.BoundCY));

            Point p = await Map.QueryRandomPositionAsync();
            ushort x = (ushort)p.X;
            ushort y = (ushort)p.Y;

            if (!Map.IsStandEnable(x, y) || Map.IsSuperPosition(x, y))
            {
                return default;
            }

            if (Map.QueryRegion(RegionType.FlagProtection, x, y))
            {
                return default;
            }

            if (Map.QueryRegion(RegionType.FlagBase, x, y))
            {
                return default;
            }

            return new FlagRespawnInfo
            {
                Point = new Point(x, y),
                Region = null
            };
        }

        public DynamicNpc GetClosePole(Character user)
        {
            foreach (var pole in poles.Values)
            {
                if (pole.GetDistance(user) <= 10)
                {
                    return pole;
                }
            }
            return null;
        }

        public async Task BroadcastEventRankAsync()
        {
            MsgWarFlag msg = new()
            {
                Action = WarFlagType.Initialize
            };
            await Map.BroadcastMsgAsync(msg);

            msg = new()
            {
                Action = WarFlagType.WarFlagTop4,
            };
            int position = 1;
            foreach (var score in Scores.Values
                .OrderByDescending(x => x.Points)
                .Take(3))
            {
                msg.Ranking.Add(new MsgWarFlag.WarFlagRanking
                {
                    Rank = position++,
                    Name = score.SynName,
                    Score = score.Points
                });
            }
            await Map.BroadcastMsgAsync(msg);

            foreach (var pole in poles.Values)
            {
                msg = new()
                {
                    Action = WarFlagType.WarFlagBaseRank
                };

                position = 1;
                foreach (var score in pole.GetTopScores().Take(3))
                {
                    msg.Ranking.Add(new MsgWarFlag.WarFlagRanking
                    {
                        Rank = position++,
                        Name = score.Name,
                        Score = (int)score.Points
                    });
                }

                await Map.BroadcastRoomMsgAsync(pole.X, pole.Y, msg);
            }

            if (doubleExploitFlagId == 0)
            {
                await RefreshDoubleExploitFlagAsync(true);
            }

#if DEBUG
            foreach (var gm in Map.QueryPlayers(x => x.IsGm()))
            {
                await gm.SendAsync("Capture the Flag Statistics", TalkChannel.GuildWarRight1);
                await gm.SendAsync($"Start time: {startTime}", TalkChannel.GuildWarRight2);
                await gm.SendAsync($"End time: {endTime}", TalkChannel.GuildWarRight2);
                await gm.SendAsync($"Flags in the map: {flags.Values.Count(x => x.Npc.MapIdentity == CTF_MAP_ID)}", TalkChannel.GuildWarRight2);
                await gm.SendAsync($"Flags in GM map: {flags.Values.Count(x => x.Npc.MapIdentity == 5000)}", TalkChannel.GuildWarRight2);
                await gm.SendAsync($"Flags with players: {flags.Values.Count(x => x.UserId != 0)}", TalkChannel.GuildWarRight2);
            }
#endif
        }

        public void AwardPoints(Character user, int points)
        {
            if (user.Syndicate == null)
            {
                return;
            }

            var synScore = Scores.GetOrAdd(user.SyndicateIdentity, new Score
            {
                SynId = user.SyndicateIdentity,
                SynName = user.SyndicateName
            });
            synScore.Points += points;
            synScore.UserCount = Math.Max(Map.QueryPlayers(x => x is Character tmp && tmp.SyndicateIdentity == user.SyndicateIdentity).Count, synScore.UserCount);

            var userScore = synScore.UserScores.GetOrAdd(user.Identity, new UserScore
            {
                UserId = user.Identity,
                Name = user.Name
            });
            userScore.Points += points;
        }

        public int GetSynRank(uint synId)
        {
            if (synId == 0)
            {
                return 0;
            }
            int rank = 1;
            foreach (var score in Scores.Values
                .OrderByDescending(x => x.Points))
            {
                if (score.SynId == synId)
                {
                    return rank;
                }
                rank++;
            }
            return 0;
        }

        private class CtfFlag
        {
            private readonly TimeOut respawn = new();

            public uint UserId { get; set; }
            public DynamicNpc Npc { get; set; }
            public FlagRegion Region { get; set; }

            public void Deliver()
            {
                respawn.Startup(5);
            }

            public bool CanRespawn()
            {
                return respawn.IsActive() && respawn.IsTimeOut(); // not using timeover, flag position may fail
            }

            public void Respawn()
            {
                respawn.Clear();
            }
        }

        private class FlagRegion
        {
            public DbRegion Region { get; set; }
            public List<CtfFlag> Flags { get; set; } = new();
        }

        private class Score
        {
            public uint SynId { get; set; }
            public string SynName { get; set; }
            public int Points { get; set; }
            public int UserCount { get; set; }

            public ConcurrentDictionary<uint, UserScore> UserScores { get; set; } = new();
        }

        private class UserScore
        {
            public uint UserId { get; set; }
            public string Name { get; set; }
            public int Points { get; set; }
        }

        private class SetMeedRank
        {
            private readonly DbSetMeed setMeed;

            public SetMeedRank(DbSetMeed setMeed)
            {
                this.setMeed = setMeed;
                SyndicateId = setMeed.SynId;
            }

            public uint SyndicateId
            {
                get => setMeed.SynId;
                set
                {
                    ISyndicate syn =  ModuleManager.SyndicateManager.GetSyndicate(value);
                    if (syn == null)
                    {
                        setMeed.SynId = 0;
                        SyndicateName = "None";
                        return;
                    }
                    setMeed.SynId = value;
                    SyndicateName = syn.Name;
                }
            }

            public string SyndicateName { get; private set; }

            public ulong Money
            {
                get => setMeed.Money;
                set => setMeed.Money = value;
            }

            public uint Emoney
            {
                get => setMeed.Emoney;
                set => setMeed.Emoney = value;
            }

            public Task SaveAsync()
            {
                return ServerDbContext.UpdateAsync(setMeed);
            }

            public Task DeleteAsync()
            {
                return ServerDbContext.DeleteAsync(setMeed);
            }
        }

        private class MeedRecordRank
        {
            private readonly DbMeedRecord meedRecord;

            public MeedRecordRank(DbMeedRecord meedRecord)
            {
                this.meedRecord = meedRecord;
                PlayerName = "Error";
            }

            public MeedRecordRank(DbMeedRecord meedRecord, string playerName)
            {
                this.meedRecord = meedRecord;
                PlayerName = playerName;
            }

            public async Task CreateAsync()
            {
                DbUser user = await UserRepository.FindByIdentityAsync(PlayerId);
                if (user != null)
                {
                    PlayerName = user.Name;
                }
            }

            public uint SynId => meedRecord.SynId;
            public SetMeed MeedType => (SetMeed)meedRecord.Type;
            public uint PlayerId => meedRecord.UserId;
            public string PlayerName { get; private set; }
            public int Points => meedRecord.Point;
            public ulong Money => meedRecord.Money;
            public uint Emoney => meedRecord.Emoney;

            public DbMeedRecord GetRecord() => meedRecord;
        }

        private class SynCompeteRank
        {
            private readonly DbSynCompeteRank rank;
            public SynCompeteRank(DbSynCompeteRank rank)
            {
                this.rank = rank;
                SyndicateId = rank.SynId;
            }

            public byte Rank => rank.Rank;

            public uint SyndicateId
            {
                get => rank.SynId;
                set
                {
					ISyndicate syn = ModuleManager.SyndicateManager.GetSyndicate(value);
					if (syn == null)
                    {
                        rank.SynId = 0;
                        SyndicateName = "None";
                        return;
                    }
                    rank.SynId = value;
                    SyndicateName = syn.Name;
                }
            }

            public string SyndicateName { get; private set; }

            public uint Points
            {
                get => rank.Point;
                set => rank.Point = value;
            }

            public ushort Members
            {
                get => rank.Relation;
                set => rank.Relation = value;
            }

            public Task SaveAsync()
            {
                return ServerDbContext.UpdateAsync(rank);
            }
        }

        private class FlagRespawnInfo
        {
            public Point Point { get; set; }
            public FlagRegion Region { get; set; }
        }
    }
}
