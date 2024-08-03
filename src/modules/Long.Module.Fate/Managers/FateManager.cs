using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.States.User;
using Long.Module.Fate.Repositories;
using Long.Kernel.Managers;
using Long.Module.Fate.Network;
using static Long.Kernel.Modules.Systems.Fate.IFate;
using RANK_DICT = System.Collections.Concurrent.ConcurrentDictionary<Long.Kernel.Modules.Systems.Fate.IFate.FateType, System.Collections.Concurrent.ConcurrentDictionary<uint, Long.Module.Fate.States.FateRankData>>;
using Long.Module.Fate.States;
using Long.Kernel.Database;
using System.Collections.Concurrent;
using Long.Kernel.Modules.Systems.Rank;

namespace Long.Module.Fate.Managers
{
    public sealed class FateManager : IFateManager
    {
        private static readonly ILogger logger = Log.ForContext<FateManager>();
        private readonly RANK_DICT mUserRankDict = new();
        private readonly List<DbFateRank> mFateRanks = new();
        private readonly List<DbFateRand> mFateRands = new();
        private readonly List<DbFateRule> mFateRules = new();
        private readonly List<DbInitFateAttrib> mInitFateAttribs = new();
        private readonly List<DbConfig> mFateOpenRule = new();
        private DbConfig mSpendPointsValue = null;
        private DbConfig mAwardPointsValue = null;

        public const int MAX_FATE_SCORE = 400;
        private const string ErrLogInit = "Could not initialize rank data for {0} {1}";

        public async Task InitializeAsync()
        {
            logger.Information("Initializing fate manager");

            if (DynamicRankManager != null)
            {
                ConcurrentDictionary<uint, FateRankData> dragonRankData = new();
                ConcurrentDictionary<uint, FateRankData> phoenixRankData = new();
                ConcurrentDictionary<uint, FateRankData> tigerRankData = new();
                ConcurrentDictionary<uint, FateRankData> turtleRankData = new();

                foreach (var r in DynamicRankManager.GetRank(IDynamicRankManager.DragonFate))
                {
                    var rank = new FateRankData(r);
                    if (!await rank.InitializeAsync())
                    {
                        logger.Error(ErrLogInit, r.RankType, r.UserId);
                        continue;
                    }
                    dragonRankData.TryAdd(r.UserId, rank);
                }
                foreach (var r in DynamicRankManager.GetRank(IDynamicRankManager.PhoenixFate))
                {
                    var rank = new FateRankData(r);
                    if (!await rank.InitializeAsync())
                    {
                        logger.Error(ErrLogInit, r.RankType, r.UserId);
                        continue;
                    }
                    phoenixRankData.TryAdd(r.UserId, rank);
                }
                foreach (var r in DynamicRankManager.GetRank(IDynamicRankManager.TigerFate))
                {
                    var rank = new FateRankData(r);
                    if (!await rank.InitializeAsync())
                    {
                        logger.Error(ErrLogInit, r.RankType, r.UserId);
                        continue;
                    }
                    tigerRankData.TryAdd(r.UserId, rank);
                }
                foreach (var r in DynamicRankManager.GetRank(IDynamicRankManager.TurtleFate))
                {
                    var rank = new FateRankData(r);
                    if (!await rank.InitializeAsync())
                    {
                        logger.Error(ErrLogInit, r.RankType, r.UserId);
                        continue;
                    }
                    turtleRankData.TryAdd(r.UserId, rank);
                }

                mUserRankDict.TryAdd(FateType.Dragon, dragonRankData);
                mUserRankDict.TryAdd(FateType.Phoenix, phoenixRankData);
                mUserRankDict.TryAdd(FateType.Tiger, tigerRankData);
                mUserRankDict.TryAdd(FateType.Turtle, turtleRankData);
            }            

            mFateRands.AddRange(await FateRandRepository.GetAsync());
            mFateRanks.AddRange(await FateRankRepository.GetAsync());
            mFateRules.AddRange(await FateRuleRepository.GetAsync());
            mInitFateAttribs.AddRange(await FateInitAttribRepository.GetAsync());
            mSpendPointsValue = (await ConfigRepository.GetAsync(90004)).FirstOrDefault();
            mAwardPointsValue = (await ConfigRepository.GetAsync(90005)).FirstOrDefault();
            mFateOpenRule.AddRange(await ConfigRepository.GetAsync(90006));
        }

        public T GetFateManager<T>() where T : IFateManager
        {
            if (this is T t) // ? lol
            {
                return t;
            }
            return default;
        }

        public async Task<bool> GenerateAsync(Character user, FateType type, DbFatePlayer fate, TrainingSave flag)
        {
            int oldRank = GetPlayerRank(user.Identity, type);
            int oldScore = GetScore(fate, type);

            DbFatePlayer backup = new()
            {
                Fate1Attrib1 = fate.Fate1Attrib1,
                Fate1Attrib2 = fate.Fate1Attrib2,
                Fate1Attrib3 = fate.Fate1Attrib3,
                Fate1Attrib4 = fate.Fate1Attrib4,
                Fate2Attrib1 = fate.Fate2Attrib1,
                Fate2Attrib2 = fate.Fate2Attrib2,
                Fate2Attrib3 = fate.Fate2Attrib3,
                Fate2Attrib4 = fate.Fate2Attrib4,
                Fate3Attrib1 = fate.Fate3Attrib1,
                Fate3Attrib2 = fate.Fate3Attrib2,
                Fate3Attrib3 = fate.Fate3Attrib3,
                Fate3Attrib4 = fate.Fate3Attrib4,
                Fate4Attrib1 = fate.Fate4Attrib1,
                Fate4Attrib2 = fate.Fate4Attrib2,
                Fate4Attrib3 = fate.Fate4Attrib3,
                Fate4Attrib4 = fate.Fate4Attrib4
            };

            bool success = true;

            var fateRands = mFateRands.Where(x => x.FateNo == (int)type).OrderBy(x => x.RangeRate).ToArray();
            var rules = mFateRules.Where(x => x.FateNo == (int)type).ToList();
            List<TrainingAttrType> generated = new();
            for (int i = 1; i < 5; i++)
            {
                if (((int)flag & (1 << (i - 1))) != 0)
                {
                    generated.Add(ReferenceType(GetPowerByIndex(fate, type, i)));
                    continue;
                }
            }

            for (int i = 1; i < 5; i++)
            {
                if (((int)flag & (1 << (i - 1))) != 0)
                {
                    continue;
                }

                int rand = await NextAsync(rules.Count) % rules.Count;
                var rule = rules[rand];
                TrainingAttrType attr = (TrainingAttrType)rule.AttrType;

                rules.RemoveAt(rand);

                if (generated.Any(x => (int)x == (int)attr))
                {
                    generated.Add(attr);
                    i--;
                    continue;
                }

                generated.Add(attr);

                int rate = await NextAsync(100000);
                double dRate = await NextRateAsync(0.999);
                DbFateRand fateRand = null;
                for (int x = 0; x < fateRands.Length; x++)
                {
                    if (rate < fateRands[x].RangeRate)
                    {
                        fateRand = fateRands[x];
                        break;
                    }
                }

                if (fateRand == null)
                {
                    success = false;
                    break;
                }

                dRate += fateRand.RangeNo;
                if (fateRand.FateNo == 100)
                {
                    dRate = Math.Floor(dRate);
                }

                int delta = (int)((rule.AttribValueMax - rule.AttribValueMin) * (dRate / 100));
                int power = Math.Min(rule.AttribValueMax, Math.Max(rule.AttribValueMin, rule.AttribValueMin + delta));
                power += ((int)attr * 10000);
                if (GetScore(type, power) >= 90)
                {
                    await RoleManager.BroadcastWorldMsgAsync(new MsgTrainingVitalityScore
                    {
                        Name = user.Name,
                        AttrType = (byte)type,
                        Power = power
                    });
                }

                SetAttribute(fate, type, i, power);
            }

            if (!success)
            {
                fate.Fate1Attrib1 = backup.Fate1Attrib1;
                fate.Fate1Attrib2 = backup.Fate1Attrib2;
                fate.Fate1Attrib3 = backup.Fate1Attrib3;
                fate.Fate1Attrib4 = backup.Fate1Attrib4;

                fate.Fate2Attrib1 = backup.Fate2Attrib1;
                fate.Fate2Attrib2 = backup.Fate2Attrib2;
                fate.Fate2Attrib3 = backup.Fate2Attrib3;
                fate.Fate2Attrib4 = backup.Fate2Attrib4;

                fate.Fate3Attrib1 = backup.Fate3Attrib1;
                fate.Fate3Attrib2 = backup.Fate3Attrib2;
                fate.Fate3Attrib3 = backup.Fate3Attrib3;
                fate.Fate3Attrib4 = backup.Fate3Attrib4;

                fate.Fate4Attrib1 = backup.Fate4Attrib1;
                fate.Fate4Attrib2 = backup.Fate4Attrib2;
                fate.Fate4Attrib3 = backup.Fate4Attrib3;
                fate.Fate4Attrib4 = backup.Fate4Attrib4;
                return false;
            }

            int typeInt = (int)type;
            byte flagByte = (byte)flag;

            fate.AttribLockInfo &= ~(fate.AttribLockInfo >> (4 * (typeInt - 1)));
            fate.AttribLockInfo |= (uint)(flagByte << (4 * (typeInt - 1)));

            await user.Fate.SaveAsync();
            await user.Fate.SendAsync(true);

            int currentRank = GetPlayerRank(user.Identity, type);
            int currentScore = GetScore(fate, type);
            UpdateStatus(user);

            if (currentRank != oldRank && mUserRankDict.TryGetValue(type, out var ranking))
            {
                foreach (var fatePlayer in ranking.Values)
                {
                    Character player = RoleManager.GetUser(fatePlayer.Identity);
                    if (player != null)
                    {
                        player.QueueAction(() =>
                        {
                            UpdateStatus(player);
                            return Task.CompletedTask;
                        });
                    }
                }
                await user.Fate.SubmitRankAsync();
            }
            else if (currentRank >= 0 && currentRank < 50 && oldScore != currentScore)
            {
                await user.Fate.SubmitRankAsync();
            }
            return true;
        }

        public DbConfig GetInitializationRequirements(FateType type)
        {
            return mFateOpenRule.Find(x => x.Data1 == (int)type);
        }

        public int GetPlayerRank(uint idUser, FateType fateType)
        {
            if (mUserRankDict.TryGetValue(fateType, out var rank))
            {
                if (rank.TryGetValue(idUser, out var ranking) && ranking.Value == MAX_FATE_SCORE)
                {
                    return 0;
                }
                return rank.Values.OrderByDescending(x => x.Value)
                    .Take(50)
                    .ToList()
                    .FindIndex(x => x.Identity == idUser);
            }
            return -1;
        }

        public int GetPower(DbFatePlayer fate, TrainingAttrType attr)
        {
            if (fate == null)
            {
                return 0;
            }

            int result = 0;
            if (ReferenceType(fate.Fate1Attrib1) == attr)
            {
                result += Power(fate.Fate1Attrib1);
            }
            else if (ReferenceType(fate.Fate1Attrib2) == attr)
            {
                result += Power(fate.Fate1Attrib2);
            }
            else if (ReferenceType(fate.Fate1Attrib3) == attr)
            {
                result += Power(fate.Fate1Attrib3);
            }
            else if (ReferenceType(fate.Fate1Attrib4) == attr)
            {
                result += Power(fate.Fate1Attrib4);
            }

            if (ReferenceType(fate.Fate2Attrib1) == attr)
            {
                result += Power(fate.Fate2Attrib1);
            }
            else if (ReferenceType(fate.Fate2Attrib2) == attr)
            {
                result += Power(fate.Fate2Attrib2);
            }
            else if (ReferenceType(fate.Fate2Attrib3) == attr)
            {
                result += Power(fate.Fate2Attrib3);
            }
            else if (ReferenceType(fate.Fate2Attrib4) == attr)
            {
                result += Power(fate.Fate2Attrib4);
            }

            if (ReferenceType(fate.Fate3Attrib1) == attr)
            {
                result += Power(fate.Fate3Attrib1);
            }
            else if (ReferenceType(fate.Fate3Attrib2) == attr)
            {
                result += Power(fate.Fate3Attrib2);
            }
            else if (ReferenceType(fate.Fate3Attrib3) == attr)
            {
                result += Power(fate.Fate3Attrib3);
            }
            else if (ReferenceType(fate.Fate3Attrib4) == attr)
            {
                result += Power(fate.Fate3Attrib4);
            }

            if (ReferenceType(fate.Fate4Attrib1) == attr)
            {
                result += Power(fate.Fate4Attrib1);
            }
            else if (ReferenceType(fate.Fate4Attrib2) == attr)
            {
                result += Power(fate.Fate4Attrib2);
            }
            else if (ReferenceType(fate.Fate4Attrib3) == attr)
            {
                result += Power(fate.Fate4Attrib3);
            }
            else if (ReferenceType(fate.Fate4Attrib4) == attr)
            {
                result += Power(fate.Fate4Attrib4);
            }

            return result;
        }

        public int GetPowerByIndex(DbFatePlayer fate, FateType fateType, int num)
        {
            if (fateType == FateType.Dragon)
            {
                if (num == 1)
                {
                    return fate.Fate1Attrib1;
                }

                if (num == 2)
                {
                    return fate.Fate1Attrib2;
                }

                if (num == 3)
                {
                    return fate.Fate1Attrib3;
                }

                if (num == 4)
                {
                    return fate.Fate1Attrib4;
                }
            }
            else if (fateType == FateType.Phoenix)
            {
                if (num == 1)
                {
                    return fate.Fate2Attrib1;
                }

                if (num == 2)
                {
                    return fate.Fate2Attrib2;
                }

                if (num == 3)
                {
                    return fate.Fate2Attrib3;
                }

                if (num == 4)
                {
                    return fate.Fate2Attrib4;
                }
            }
            else if (fateType == FateType.Tiger)
            {
                if (num == 1)
                {
                    return fate.Fate3Attrib1;
                }

                if (num == 2)
                {
                    return fate.Fate3Attrib2;
                }

                if (num == 3)
                {
                    return fate.Fate3Attrib3;
                }

                if (num == 4)
                {
                    return fate.Fate3Attrib4;
                }
            }
            else if (fateType == FateType.Turtle)
            {
                if (num == 1)
                {
                    return fate.Fate4Attrib1;
                }

                if (num == 2)
                {
                    return fate.Fate4Attrib2;
                }

                if (num == 3)
                {
                    return fate.Fate4Attrib3;
                }

                if (num == 4)
                {
                    return fate.Fate4Attrib4;
                }
            }
            return 0;
        }

        public DbFateRule GetRule(FateType type, TrainingAttrType attrType)
        {
            return mFateRules.Find(x => x.FateNo == (int)type && x.AttrType == (int)attrType);
        }

        public int GetScore(DbFatePlayer record, FateType type)
        {
            int total = 0;

            if (record == null)
            {
                return total;
            }

            if (type == FateType.Dragon)
            {
                total += GetScore(type, record.Fate1Attrib1);
                total += GetScore(type, record.Fate1Attrib2);
                total += GetScore(type, record.Fate1Attrib3);
                total += GetScore(type, record.Fate1Attrib4);
            }
            else if (type == FateType.Phoenix)
            {
                total += GetScore(type, record.Fate2Attrib1);
                total += GetScore(type, record.Fate2Attrib2);
                total += GetScore(type, record.Fate2Attrib3);
                total += GetScore(type, record.Fate2Attrib4);
            }
            else if (type == FateType.Tiger)
            {
                total += GetScore(type, record.Fate3Attrib1);
                total += GetScore(type, record.Fate3Attrib2);
                total += GetScore(type, record.Fate3Attrib3);
                total += GetScore(type, record.Fate3Attrib4);
            }
            else if (type == FateType.Turtle)
            {
                total += GetScore(type, record.Fate4Attrib1);
                total += GetScore(type, record.Fate4Attrib2);
                total += GetScore(type, record.Fate4Attrib3);
                total += GetScore(type, record.Fate4Attrib4);
            }
            return total;
        }

        private int GetScore(FateType type, int attrib)
        {
            if (attrib == 0)
            {
                return 0;
            }
            var rule = GetRule(type, ReferenceType(attrib));
            int refPower = ReferencePower(rule, attrib);
            double baseValue = rule.AttribValueMax - rule.AttribValueMin;
            return (int)((refPower / baseValue) * 100);
        }

        public async Task InitialFateAttributeAsync(Character user, FateType type, DbFatePlayer fate)
        {
            var initialFate = mInitFateAttribs.Find(x => x.ProfSort == user.ProfessionSort);
            if (initialFate == null)
            {
                await GenerateAsync(user, type, fate, TrainingSave.None);
                return;
            }

            switch (type)
            {
                case FateType.Dragon:
                    {
                        SetAttribute(fate, type, 1, initialFate.Fate1Attrib1);
                        SetAttribute(fate, type, 2, initialFate.Fate1Attrib2);
                        SetAttribute(fate, type, 3, initialFate.Fate1Attrib3);
                        SetAttribute(fate, type, 4, initialFate.Fate1Attrib4);
                        break;
                    }
                case FateType.Phoenix:
                    {
                        SetAttribute(fate, type, 1, initialFate.Fate2Attrib1);
                        SetAttribute(fate, type, 2, initialFate.Fate2Attrib2);
                        SetAttribute(fate, type, 3, initialFate.Fate2Attrib3);
                        SetAttribute(fate, type, 4, initialFate.Fate2Attrib4);
                        break;
                    }
                case FateType.Tiger:
                    {
                        SetAttribute(fate, type, 1, initialFate.Fate3Attrib1);
                        SetAttribute(fate, type, 2, initialFate.Fate3Attrib2);
                        SetAttribute(fate, type, 3, initialFate.Fate3Attrib3);
                        SetAttribute(fate, type, 4, initialFate.Fate3Attrib4);
                        break;
                    }
                case FateType.Turtle:
                    {
                        SetAttribute(fate, type, 1, initialFate.Fate4Attrib1);
                        SetAttribute(fate, type, 2, initialFate.Fate4Attrib2);
                        SetAttribute(fate, type, 3, initialFate.Fate4Attrib3);
                        SetAttribute(fate, type, 4, initialFate.Fate4Attrib4);
                        break;
                    }
            }

            await user.Fate.SaveAsync();
            await user.Fate.SendAsync(true);

            UpdateStatus(user);
            await user.Fate.SubmitRankAsync();
        }

        public void SetAttribute(DbFatePlayer fate, FateType type, int num, int value)
        {
            if (type == FateType.Dragon)
            {
                if (num == 1)
                {
                    fate.Fate1Attrib1 = value;
                }

                if (num == 2)
                {
                    fate.Fate1Attrib2 = value;
                }

                if (num == 3)
                {
                    fate.Fate1Attrib3 = value;
                }

                if (num == 4)
                {
                    fate.Fate1Attrib4 = value;
                }
            }
            else if (type == FateType.Phoenix)
            {
                if (num == 1)
                {
                    fate.Fate2Attrib1 = value;
                }

                if (num == 2)
                {
                    fate.Fate2Attrib2 = value;
                }

                if (num == 3)
                {
                    fate.Fate2Attrib3 = value;
                }

                if (num == 4)
                {
                    fate.Fate2Attrib4 = value;
                }
            }
            else if (type == FateType.Tiger)
            {
                if (num == 1)
                {
                    fate.Fate3Attrib1 = value;
                }

                if (num == 2)
                {
                    fate.Fate3Attrib2 = value;
                }

                if (num == 3)
                {
                    fate.Fate3Attrib3 = value;
                }

                if (num == 4)
                {
                    fate.Fate3Attrib4 = value;
                }
            }
            else if (type == FateType.Turtle)
            {
                if (num == 1)
                {
                    fate.Fate4Attrib1 = value;
                }

                if (num == 2)
                {
                    fate.Fate4Attrib2 = value;
                }

                if (num == 3)
                {
                    fate.Fate4Attrib3 = value;
                }

                if (num == 4)
                {
                    fate.Fate4Attrib4 = value;
                }
            }
        }

        public void UpdateStatus(Character user)
        {
            if (user?.Fate != null)
            {
                user.Fate.RefreshPower();
                UpdateRankStatus(user, 1); // dragon rank
                UpdateRankStatus(user, 2); // phoenix rank
                UpdateRankStatus(user, 3); // tiger rank
                UpdateRankStatus(user, 4); // turtle rank
            }
        }

        private void UpdateRankStatus(Character user, int fateType)
        {
            if (user.Fate.IsLocked((FateType)fateType))
            {
                return;
            }

            int score = user.Fate.GetScore((FateType)fateType);
            if (mUserRankDict.TryGetValue((FateType)fateType, out var ranking))
            {
                if (!ranking.TryGetValue(user.Identity, out var playerRank))
                {
                    playerRank = new FateRankData(user, (FateType)fateType);
                    ranking.TryAdd(user.Identity, playerRank);
                }
                bool update = playerRank.Value != score;
                playerRank.Value = (uint)score;
                if (update)
                {
                    _ = playerRank.SaveAsync();
                }
            }

            if (score >= MAX_FATE_SCORE)
            {
                var fateRank = mFateRanks.Find(x => x.FateNo == fateType && x.Sort == 1);
                if (fateRank != null)
                {
                    user.Fate.AddAttribute(fateRank.Attrib1);
                    user.Fate.AddAttribute(fateRank.Attrib2);
                    user.Fate.AddAttribute(fateRank.Attrib3);
                    user.Fate.AddAttribute(fateRank.Attrib4);
                }   
            }
            else
            {
                int rank = GetPlayerRank(user.Identity, (FateType)fateType);
                if (rank > -1 && rank < 50)
                {
                    var fateRank = mFateRanks.Find(x => x.FateNo == fateType && x.Sort == rank + 1);
                    if (fateRank != null)
                    {
                        user.Fate.AddAttribute(fateRank.Attrib1);
                        user.Fate.AddAttribute(fateRank.Attrib2);
                        user.Fate.AddAttribute(fateRank.Attrib3);
                        user.Fate.AddAttribute(fateRank.Attrib4);
                    }
                }
            }
        }

        public TrainingAttrType ReferenceType(int power)
        {
            return (TrainingAttrType)(power / 10000);
        }

        private static int ReferencePower(DbFateRule rule, int power)
        {
            return (power % 10000) - rule.AttribValueMin;
        }

        private static int Power(int power)
        {
            return power % 10000;
        }

        public Task SaveRankAsync()
        {
            List<FateRankData> fateRankDatas = new ();
            foreach (var ranking  in mUserRankDict.Values)
            {
                fateRankDatas.AddRange(ranking.Select(x => x.Value));
            }
            return ServerDbContext.UpdateRangeAsync(fateRankDatas.ToArray());
        }

        public IFate CreateOSFate(Character user)
        {
            return new States.Fate(user);
        }
    }
}
