using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Managers;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Module.Family.Network;
using Long.Shared;
using Serilog;
using System.Collections.Concurrent;
using System.Globalization;
using static Long.Kernel.StrRes;

namespace Long.Module.Family.Managers
{
    public class FamilyWarManager : IFamilyWarManager
    {
        private static readonly ILogger logger = Log.ForContext<FamilyWarManager>();

        private const uint RewardStcU = 100020;

        private IReadOnlyDictionary<int, uint[]> prizePool { get; } = new Dictionary<int, uint[]>
        {
            {1, new uint[] {722458, 722457, 722456, 722455, 722454}},
            {2, new uint[] {722478, 722477, 722476, 722475, 722474}},
            {3, new uint[] {722473, 722472, 722471, 722470, 722469}},
            {4, new uint[] {722468, 722467, 722466, 722465, 722464}},
            {5, new uint[] {722463, 722462, 722461, 722460, 722459}},
        };

        private readonly double[] experienceRewards =
        {
            0.01,
            0.015d,
            0.02,
            0.025d,
            0.03,
            0.035d,
            0.05
        };

        private int lastUpdate;
        private FamilyWarStage stage = FamilyWarStage.Idle;

        //private const int ChallengeTime = 6;
        private const int TakeRewardTime = 6;
        private const int OccupyDate = 7;

        private const string OwnerCity = "data0";
        private const string ChallengeMap = "data1";
        private const string ChallengeFee = "data2";
        private const string PrizePool = "data3";

        private static ConcurrentDictionary<uint, DynamicNpc> familyWarNpcs = new();

        public static FamilyWarManager Instance { get; private set; }

        public bool IsInTime => uint.Parse(DateTime.Now.ToString("HHmmss")) >= 203000 && uint.Parse(DateTime.Now.ToString("HHmmss")) < 210000;

        public bool IsAllowedToJoin(Role sender)
        {
            return uint.Parse(DateTime.Now.ToString("HHmmss")) >= 203000
                   && uint.Parse(DateTime.Now.ToString("HHmmss")) < 203500;
        }

        public async Task OnTimerAsync()
        {
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int time = int.Parse(DateTime.Now.ToString("HHmm"));
            if (stage == FamilyWarStage.Idle)
            {
                if (time is >= 2000 and < 2030)
                {
                    logger.Information("Starting Clan Wars!");
                    stage = FamilyWarStage.Preparing;
                }
            }
            else if (stage == FamilyWarStage.Preparing)
            {
                if (time == 2030)
                {
                    foreach (var npc in familyWarNpcs.Values)
                    {
                        foreach (var challenger in GetChallengersByNpc(npc.Identity))
                        {
                            await challenger.SendAsync(new MsgFamilyOccupy
                            {
                                Action = MsgFamilyOccupy.FamilyPromptType.AnnounceWarBegin
                            });
                        }

                        IFamily owner = ModuleManager.FamilyManager.GetFamily(npc.OwnerIdentity);
                        if (owner != null && IsNpcChallenged(owner.Occupy))
                        {
                            await owner.SendAsync(new MsgFamilyOccupy
                            {
                                Action = MsgFamilyOccupy.FamilyPromptType.AnnounceWarBegin
                            });
                        }
                    }

                    stage = FamilyWarStage.Running;
                }
            }
            else if (stage == FamilyWarStage.Running)
            {
                if (time == 2045)
                {
                    stage = FamilyWarStage.WaitingConfirmation;
                    // hm?
                }
            }
            else if (stage == FamilyWarStage.WaitingConfirmation)
            {
                if (time == 2055 && lastUpdate != now)
                {
                    foreach (IFamily family in ModuleManager.FamilyManager.QueryFamilies(x => x.Challenge != 0))
                    {
                        family.Challenge = 0;
                        await family.SaveAsync();
                    }

                    lastUpdate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    stage = FamilyWarStage.Idle;
                }
            }
        }

        /// <inheritdoc />
        public async Task<bool> InitializeAsync()
        {
            var npcs = RoleManager
                .QueryRoles(x => x is DynamicNpc dynaNpc && dynaNpc.Type == BaseNpc.ROLE_FAMILY_OCCUPY_NPC)
                .Cast<DynamicNpc>();
            foreach (var npc in npcs)
            {
                familyWarNpcs.TryAdd(npc.Identity, npc);
            }

            Instance = this;
            return true;
        }

        private int GetExpRewardIdx(int occupyDays)
        {
            occupyDays = Math.Max(1, occupyDays);
            return (occupyDays - 1) % experienceRewards.Length;
        }

        public uint GetNextReward(uint idNpc)
        {
            if (familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                int prizePoolId = npc.GetData(PrizePool);
                IFamily family = ModuleManager.FamilyManager.GetFamily(npc.OwnerIdentity);
                if (family == null)
                {
                    return prizePool[prizePoolId][0];
                }
                return prizePool[prizePoolId][family.Rank];
            }
            return 0;
        }

        public uint GetNextWeekReward(uint idNpc)
        {
            if (idNpc == 0)
            {
                return 0;
            }

            if (!familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return 0;
            }

            int prizePoolId = npc.GetData(PrizePool);
            IFamily family = GetFamilyOwner(idNpc);
            if (family == null)
            {
                return prizePool[prizePoolId][0];
            }
            return prizePool[prizePoolId][family.Rank];
        }

        public IFamily GetFamilyOwner(uint idNpc)
        {
            if (!familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return null;
            }

            return ModuleManager.FamilyManager.GetOccupyOwner((uint)npc.Identity);
        }

        public DynamicNpc GetChallengeNpc(IFamily family)
        {
            if (family == null || family.Challenge == 0)
            {
                return null;
            }

            return familyWarNpcs.Values.FirstOrDefault(x => x.Identity == family.Challenge);
        }

        public DynamicNpc GetDominatingNpc(IFamily family)
        {
            if (family == null || family.Occupy == 0)
            {
                return null;
            }

            return familyWarNpcs.Values.FirstOrDefault(x => x.Identity == family.Occupy);
        }

        public uint GetGoldFee(uint idNpc)
        {
            return (uint)(familyWarNpcs.TryGetValue(idNpc, out var npc) ? npc.GetData(ChallengeFee) : 0);
        }

        public GameMap GetMapByNpc(uint idNpc)
        {
            if (familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return MapManager.GetMap((uint)npc.GetData(ChallengeMap));
            }
            return null;
        }

        public IList<IFamily> GetChallengersByNpc(uint idNpc)
        {
            return ModuleManager.FamilyManager.QueryFamilies(x => x.Challenge == idNpc);
        }

        public bool IsNpcChallenged(uint idNpc)
        {
            if (familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return GetChallengersByNpc(idNpc).Count > 0;
            }
            return false;
        }

        private bool ValidateRewardTime(DateTime time)
        {
            DateTime now = DateTime.Now;
            if (now.Year != time.Year)
            {
                return true;
            }

            uint nowTime = uint.Parse(now.ToString("HHmmss"));
            uint lastTime = uint.Parse(time.ToString("HHmmss"));
            if (lastTime is >= 210000 and <= 235959)
            {
                if (nowTime is >= 210000 and <= 235959 && now.DayOfYear != time.DayOfYear)
                {
                    return true;
                }

                if (nowTime < 203000)
                {
                    return true;
                }
            }
            else if (lastTime <= 202959)
            {
                if (nowTime <= 202959 && now.DayOfYear != time.DayOfYear)
                {
                    return true;
                }

                if (nowTime >= 210000)
                {
                    return true;
                }
            }

            // may be error - must fix manually
            return false;
        }

        public bool HasExpToClaim(Character user)
        {
            if (IsInTime)
            {
                return false;
            }

            if (user?.Family == null)
            {
                return false;
            }

            DynamicNpc npc = GetDominatingNpc(user.Family);
            if (npc == null)
            {
                return false;
            }

            DateTime? last = UnixTimestamp.ToNullableDateTime(user.Statistic.GetStc(RewardStcU)?.Timestamp);
            if (last.HasValue)
            {
                return ValidateRewardTime(last.Value);
            }

            return true;
        }

        public async Task SetExpRewardAwardedAsync(Character user)
        {
            DbStatistic currStc = user.Statistic.GetStc(RewardStcU);
            if (currStc == null)
            {
                if (!await user.Statistic.AddOrUpdateAsync(RewardStcU, 0, 0, true))
                {
                    return;
                }

                currStc = user.Statistic.GetStc(RewardStcU);
                if (currStc == null)
                {
                    return;
                }
            }

            await user.Statistic.AddOrUpdateAsync(RewardStcU, 0, currStc.Data + 1, true);
        }

        public double GetNextExpReward(Character user)
        {
            if (!HasExpToClaim(user))
            {
                return 0;
            }

            return experienceRewards[GetExpRewardIdx(GetFamilyOccupyDays(user.FamilyIdentity))];
        }

        public int GetFamilyOccupyDays(uint idFamily)
        {
            IFamily family = ModuleManager.FamilyManager.GetFamily(idFamily);
            if (family == null)
            {
                return 0;
            }

            if (!familyWarNpcs.TryGetValue(family.Occupy, out var npc))
            {
                return 0;
            }

            uint occupyUint = npc.GetTask(OccupyDate);
            if (occupyUint == 0)
            {
                return 0;
            }

            DateTime occupyDate = DateTime.ParseExact($"{occupyUint}", "yyMMdd", CultureInfo.InvariantCulture);
            return Math.Max(1, (int)(occupyDate - DateTime.Now).TotalDays);
        }

        public int GetFamilyOccupyDaysByNpc(uint idNpc)
        {
            if (!familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return 0;
            }

            uint occupyUint = npc.GetTask(OccupyDate);
            if (occupyUint == 0)
            {
                return 0;
            }

            DateTime occupyDate = DateTime.ParseExact($"{occupyUint}", "yyMMdd", CultureInfo.InvariantCulture);
            return Math.Max(1, (int)(DateTime.Now - occupyDate).TotalDays);
        }

        public bool HasRewardToClaim(Character user)
        {
            if (IsInTime)
            {
                return false;
            }

            if (user?.Family == null)
            {
                return false;
            }

            DynamicNpc npc = GetDominatingNpc(user.Family);
            if (npc == null)
            {
                return false;
            }

            uint rewardTask = npc.GetTask(TakeRewardTime);
            if (rewardTask == 0)
            {
                return true;
            }

            if (DateTime.TryParseExact(rewardTask.ToString(), "yyMMddHH", Thread.CurrentThread.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime date)
                && !ValidateRewardTime(date))
            {
                return false;
            }
            return true;
        }

        public async Task SetRewardAwardedAsync(Character user)
        {
            DynamicNpc npc = GetDominatingNpc(user.Family);
            if (npc == null)
            {
                return;
            }

            DbStatistic currStc = user.Statistic.GetStc(RewardStcU, 1);
            if (currStc == null)
            {
                if (!await user.Statistic.AddOrUpdateAsync(RewardStcU, 1, 0, true))
                {
                    return;
                }

                currStc = user.Statistic.GetStc(RewardStcU, 1);
                if (currStc == null)
                {
                    return;
                }
            }

            npc.SetTask(TakeRewardTime, uint.Parse(DateTime.Now.ToString("yyMMddHH")));
            await npc.SaveAsync();
            await user.Statistic.AddOrUpdateAsync(RewardStcU, 1, currStc.Data + 1, true);
        }

        public async Task<bool> ValidateResultAsync(Character user, uint idNpc)
        {
            if (!familyWarNpcs.TryGetValue(idNpc, out var npc))
            {
                return false;
            }

            if (npc.OwnerIdentity != 0 && npc.OwnerIdentity == user.FamilyIdentity)
            {
                return true;
            }

            if (npc.Identity != user.Family.Challenge && npc.Identity != user.Family.Occupy)
            {
                return false;
            }

            uint currentTime = uint.Parse(DateTime.Now.ToString("HHmmss"));
            if (currentTime is < 203500 or > 205459)
            {
                return false;
            }

            GameMap map = MapManager.GetMap((uint)npc.Data1);
            if (map == null)
            {
                return false;
            }

            var families = new Dictionary<uint, IFamily>();
            foreach (Character player in map.QueryPlayers(x =>
                                                              x.FamilyIdentity != 0 &&
                                                              (x.Family.Occupy == npc.Identity || x.Family.Challenge == npc.Identity)
                                                               && x.IsAlive))
            {
                if (!families.ContainsKey(player.FamilyIdentity))
                {
                    families.Add(player.FamilyIdentity, player.Family);
                }
            }

            if (families.Count == 1)
            {
                if (currentTime is < 203000 or > 205459)
                {
                    return false;
                }

                IFamily winner = families.Values.FirstOrDefault();
                if (winner != null)
                {
                    await SetWinnerAsync(npc, winner);
                }
            }
            else if (families.Count > 1)
            {
                if (currentTime is < 204500 or > 205459)
                {
                    return false;
                }

                IFamily current = ModuleManager.FamilyManager.GetOccupyOwner((uint)npc.Identity);
                if (families.All(x => x.Key != current?.Identity))
                {
                    var bpDict = new Dictionary<uint, int>();
                    foreach (Character player in map.QueryPlayers(x =>
                                                                      x.FamilyIdentity != 0 &&
                                                                      (x.Family.Occupy == npc.Identity || x.Family.Challenge == npc.Identity) &&
                                                                      x.IsAlive))
                    {
                        if (player.FamilyIdentity == 0)
                        {
                            continue;
                        }

                        if (bpDict.ContainsKey(player.FamilyIdentity))
                        {
                            bpDict[player.FamilyIdentity] += player.BattlePower;
                        }
                        else
                        {
                            bpDict.Add(player.FamilyIdentity, player.BattlePower);
                        }
                    }

                    IFamily winner = ModuleManager.FamilyManager.GetFamily(bpDict.OrderByDescending(x => x.Value).FirstOrDefault().Key);
                    await SetWinnerAsync(npc, winner);
                    await npc.SaveAsync();
                }
                else
                {
                    await SetWinnerAsync(npc, current);
                    // let's renew the champion
                }
                // return true even if false because the winner is the clan whose is already dominating. wont change
            }
            else
            {
                return false;
            }

            return true;
        }

        private async Task SetWinnerAsync(DynamicNpc npc, IFamily family)
        {
            if (npc.OwnerIdentity != family.Identity)
            {
                // remove the old owner from the NPC!
                IFamily oldOwner = ModuleManager.FamilyManager.GetFamily(npc.OwnerIdentity);
                if (oldOwner != null)
                {
                    oldOwner.Occupy = 0;
                    await oldOwner.SaveAsync();
                }
            }

            if (family.Occupy != npc.Identity) // family occupies another map, clean up
            {
                if (familyWarNpcs.TryGetValue(family.Occupy, out var oldNpc)
                    && oldNpc.OwnerIdentity == family.Identity)
                {
                    oldNpc.OwnerIdentity = 0;
                    oldNpc.OwnerName = StrNone;
                    await oldNpc.SaveAsync();
                }
            }

            npc.SetTask(OccupyDate, uint.Parse(DateTime.Now.ToString("yyMMdd")));

            family.Occupy = npc.Identity;
            family.Challenge = 0;
            await family.SaveAsync();

            npc.OwnerIdentity = family.Identity;
            npc.OwnerName = family.Name;
            await npc.SaveAsync();
        }

        private enum FamilyWarStage
        {
            Idle,
            Preparing,
            Running,
            WaitingConfirmation
        }
    }
}
