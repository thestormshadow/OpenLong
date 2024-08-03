using System.Collections.Concurrent;
using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.JiangHu;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Module.JiangHu.Network;
using Long.Module.JiangHu.Repositories;
using Long.Network.Packets.Cross;
using Long.Shared.Helpers;
using static Long.Kernel.Modules.Systems.JiangHu.IJiangHu;
using static Long.Kernel.Modules.Systems.JiangHu.IJiangHuManager;
using static Long.Kernel.States.User.Character;
using static Long.Module.JiangHu.Network.MsgOwnKongfuBase;

namespace Long.Module.JiangHu.States
{
    public sealed class OwnKongFu : IJiangHu
    {
        private static readonly ILogger logger = Log.ForContext<OwnKongFu>();
        private static readonly ILogger gmLog = Logger.CreateLogger("kong_fu");

        private readonly Character user;
        private DbJiangHuPlayer jiangHu;
        private DbJiangHuCaltivateTimes jiangHuTimes;
        private ConcurrentDictionary<byte, DbJiangHuPlayerPower> powers = new();

        private readonly TimeOut exitKongFuTimer = new();
        private readonly TimeOut awardPointsTimer = new();

        private const uint TRAINING_COURSE_TYPE = 1;
        private const uint TRAINING_COURSE_SUBTYPE_FREE = 1;
        private const uint TRAINING_COURSE_SUBTYPE_PAID = 2;

        public const int MAX_FREE_COURSE = 100;
        public const double POINTS_TO_COURSE = 10000d;
        public const int EXIT_KONG_FU_SECONDS = 600;

        public OwnKongFu(Character user)
        {
            this.user = user;
        }

        public int MaxLife { get; private set; }

        public int MaxMana { get; private set; }

        public int Attack { get; private set; }

        public int MagicAttack { get; private set; }

        public int Defense { get; private set; }

        public int MagicDefense { get; private set; }

        public int FinalDamage { get; private set; }

        public int FinalMagicDamage { get; private set; }

        public int FinalDefense { get; private set; }

        public int FinalMagicDefense { get; private set; }

        public int CriticalStrike { get; private set; }

        public int SkillCriticalStrike { get; private set; }

        public int Breakthrough { get; private set; }

        public int Immunity { get; private set; }

        public int Counteraction { get; private set; }

        public bool HasJiangHu => jiangHu != null;

        public string Name => jiangHu?.Name ?? "None";

        public int CurrentStage => powers.Count; // power_level

        public int Grade { get; private set; }

        public byte Talent
        {
            get => jiangHu?.GenuineQiLevel ?? 0;
            set => jiangHu.GenuineQiLevel = Math.Max((byte)0, Math.Min(JiangHuManager.GetMaxTalentLevel(), value));
        }

        public bool IsActive => user.PkMode == PkModeType.JiangHu ||
                                (exitKongFuTimer.IsActive() && !exitKongFuTimer.IsTimeOut());

        public int SecondsTillNextCourse
        {
            get
            {
                int maxSeconds = (int)(10000d / JiangHuManager.GetCaltivateProgress(user) * 60) * 60;
                int amount = (int)(FreeCaltivateParam / POINTS_TO_COURSE * maxSeconds);
                return UnixTimestamp.Now + (int)(maxSeconds - maxSeconds * (amount % 10000 / 10000d));
            }
        }

        public uint FreeCourses => FreeCaltivateParam / 10000;

        public uint FreeCaltivateParam
        {
            get => jiangHu?.FreeCaltivateParam ?? 0;
            set => jiangHu.FreeCaltivateParam =
                (uint)Math.Max(0, Math.Min(value, JiangHuManager.GetMaxFreeCourse() * POINTS_TO_COURSE));
        }

        public byte FreeCoursesUsedToday
        {
            get => jiangHuTimes?.FreeTimes ?? 0;
            set
            {
                jiangHuTimes ??= new DbJiangHuCaltivateTimes
                {
                    PlayerId = user.Identity
                };
                jiangHuTimes.FreeTimes = value;
            }
        }

        public uint PaidCoursesUsedToday
        {
            get => jiangHuTimes?.PaidTimes ?? 0;
            set
            {
                jiangHuTimes ??= new DbJiangHuCaltivateTimes
                {
                    PlayerId = user.Identity
                };
                jiangHuTimes.PaidTimes = value;
            }
        }

        public uint InnerPower
        {
            get => jiangHu.TotalPowerValue;
            set => jiangHu.TotalPowerValue = value;
        }

        public uint MaxInnerPowerHistory { get; set; }

        public async Task InitializeAsync()
        {
            jiangHu = await JiangHuPlayerRepository.GetAsync(user.Identity);
            if (jiangHu == null)
            {
                await InitializationNotifyAsync();
                return;
            }

            awardPointsTimer.Startup(60);

            jiangHuTimes = await JiangHuCaltivateTimesRepository.GetAsync(user.Identity);
            powers = new ConcurrentDictionary<byte, DbJiangHuPlayerPower>(
                await JiangHuPlayerPowerRepository.GetAsync(user.Identity));

            if (user.LastLogout.HasValue)
            {
                int minutesSinceLastLogin =
                    (int)(user.LastLogin - user.LastLogout.Value)
                    .TotalMinutes; // LoginTime is the previous login time, this initialization runs before Login completion, so this has not been overriten
                if (minutesSinceLastLogin > 0)
                {
                    FreeCaltivateParam += (uint)(JiangHuManager.GetCaltivateProgress(Talent) * minutesSinceLastLogin);
                }
            }

            FreeCaltivateParam = (uint)Math.Max(0,
                Math.Min(JiangHuManager.GetMaximumFreeCultivationValue(), FreeCaltivateParam));

            uint oldInnerPower = InnerPower;
            UpdateAllAttributes();
            if (oldInnerPower != InnerPower)
            {
                await SaveAsync();
            }

            int remainingJiangHu = JiangHuManager.GetJiangHuRemainingTime(user.Identity);
            if (remainingJiangHu > 0)
            {
                exitKongFuTimer.Startup(remainingJiangHu);
            }

            await SendStarsAsync();
            await SendStarAsync();
            await SendStatusAsync();
        }

        public Task InitializeOSDataAsync(CrossOwnKongFuListInfoPB info)
        {
            jiangHu = new DbJiangHuPlayer
            {
                TotalPowerValue = info.TotalPowerValue,
                FreeCaltivateParam = info.FreeCaltivateParam,
                GenuineQiLevel = info.GenuineQiLevel,
                Name = info.Name,
                PlayerId = user.Identity,
                PowerLevel = info.PowerLevel,
            };
            powers.Clear();
            foreach (var power in info.List)
            {
                powers.TryAdd(power.Level, new DbJiangHuPlayerPower
                {
                    PlayerId = user.Identity,
                    Level = power.Level,                    
                    Type1 = power.Type1,
                    Quality1 = power.Quality1,
                    Type2 = power.Type2,
                    Quality2 = power.Quality2,
                    Type3 = power.Type3,
                    Quality3 = power.Quality3,
                    Type4 = power.Type4,
                    Quality4 = power.Quality4,
                    Type5 = power.Type5,
                    Quality5 = power.Quality5,
                    Type6 = power.Type6,
                    Quality6 = power.Quality6,
                    Type7 = power.Type7,
                    Quality7 = power.Quality7,
                    Type8 = power.Type8,
                    Quality8 = power.Quality8,
                    Type9 = power.Type9,
                    Quality9 = power.Quality9
                });
            }
            UpdateAllAttributes();
            return Task.CompletedTask;
        }

        public async Task<bool> CreateAsync(string gongFuName)
        {
            logger.Information("Creating JiangHu {0} for user {1} {2}", gongFuName, user.Identity, user.Name);

            if (jiangHu != null)
            {
                return false;
            }

            if (user.Metempsychosis < 2)
            {
                return false;
            }

            if (user.Level < 30)
            {
                return false;
            }

            if (!RoleManager.IsValidName(gongFuName))
            {
                logger.Warning("JiangHu {0} denied for user {1} {2} due to invalid name.", gongFuName, user.Identity,
                    user.Name);
                return false;
            }

            DbJiangHuPlayer temp = await JiangHuPlayerRepository.GetAsync(gongFuName);
            if (temp != null)
            {
                return false;
            }

            jiangHu = new DbJiangHuPlayer
            {
                Name = gongFuName,
                PlayerId = user.Identity,
                GenuineQiLevel = 3,
                FreeCaltivateParam = (uint)(POINTS_TO_COURSE * 5)
            };

            await GenerateAsync(1, 1, 0, KongFuImproveFeedbackMode.FreeCourse);
            FreeCoursesUsedToday = 0;

            await SaveAsync();

            awardPointsTimer.Startup(60);

            await SendInfoAsync();
            await SendStarsAsync();
            await SendStarAsync();
            await SendTimeAsync();
            await SendTalentAsync();
            await SendStatusAsync();
            await user.SetPkModeAsync(PkModeType.JiangHu);
            return true;
        }

        public Task InitializationNotifyAsync()
        {
            if (user.Metempsychosis >= 2 && user.Level >= 30 && !HasJiangHu)
            {
                return user.SendAsync(new MsgOwnKongfuBase
                {
                    Mode = KongfuBaseMode.IconBar
                });
            }

            return Task.CompletedTask;
        }

        public async Task StudyAsync(byte powerLevel, byte star, byte high, KongFuImproveFeedbackMode mode)
        {
            DbJiangHuCaltivateCondition condition = JiangHuManager.GetCaltivateCondition((byte)CurrentStage);
            if (powerLevel > CurrentStage)
            {
                if (powerLevel != CurrentStage + 1)
                {
                    return;
                }

                if (powerLevel > 9)
                {
                    return;
                }

                if (GetStageInnerPower((byte)CurrentStage) < condition.NeedPowerValue)
                {
                    return;
                }

                if (powers.TryGetValue(powerLevel, out _))
                {
                    return;
                }

                powers.TryAdd(powerLevel, new DbJiangHuPlayerPower
                {
                    Level = powerLevel,
                    PlayerId = user.Identity
                });
            }

            byte latestStar = GetLatestStar();
            if (star > latestStar && star != latestStar + 1 && powerLevel >= CurrentStage)
            {
                return; // invalid star
            }

            int emoneyAmount = 0;
            List<Item> useItems = new();
            if (mode == KongFuImproveFeedbackMode.FreeCourse)
            {
                if (Talent == 0)
                {
                    return;
                }

                if (FreeCoursesUsedToday >= JiangHuManager.GetMaxFreeCourse())
                {
                    return;
                }

                if (FreeCourses == 0)
                {
                    Item freeTrainingPill = user.UserPackage.GetActiveItemByType(Item.FREE_TRAINING_PILL);
                    if (freeTrainingPill == null)
                    {
                        return;
                    }

                    useItems.Add(freeTrainingPill);
                }

                if (!await user.SpendCultivationAsync(10))
                {
                    return;
                }

                FreeCaltivateParam -= (uint)POINTS_TO_COURSE;
                await SpendTalentAsync();
            }
            else
            {
                emoneyAmount = (int)(condition.NeedCultivateValue * Math.Min(Math.Max(1, PaidCoursesUsedToday), 50) +
                                     condition.NeedCultivateValue);
                if (mode == KongFuImproveFeedbackMode.FavouredTraining)
                {
                    int needItem = emoneyAmount / 10;
                    int itemCount = user.UserPackage.MultiGetItem(Item.FAVORED_TRAINING_PILL,
                        Item.FAVORED_TRAINING_PILL, needItem, ref useItems);
                    if (itemCount < needItem)
                    {
                        return;
                    }

                    emoneyAmount = 0;
                }
            }

            switch (high)
            {
                case 1:
                {
                    if (user.VipLevel < 2)
                    {
                        high = 0;
                    }
                    else
                    {
                        Item specialTrainingPill = user.UserPackage.GetItemByType(Item.SPECIAL_TRAINING_PILL);
                        if (specialTrainingPill == null)
                        {
                            if (user.ConquerPoints >= 5)
                            {
                                emoneyAmount += (int)condition.CritCost;
                            }
                            else
                            {
                                high = 0;
                            }
                        }
                        else
                        {
                            useItems.Add(specialTrainingPill);
                        }
                    }

                    break;
                }
                case 2:
                {
                    if (user.VipLevel < 5)
                    {
                        high = 0;
                    }
                    else
                    {
                        Item seniorTrainingPill = user.UserPackage.GetItemByType(Item.SENIOR_TRAINING_PILL);
                        if (seniorTrainingPill == null)
                        {
                            if (user.ConquerPoints >= 50)
                            {
                                emoneyAmount += (int)condition.HighCritCost;
                            }
                            else
                            {
                                high = 0;
                            }
                        }
                        else
                        {
                            useItems.Add(seniorTrainingPill);
                        }
                    }

                    break;
                }
            }

            if (emoneyAmount > 0 &&
                !await user.SpendBoundConquerPointsAsync(EmoneyOperationType.JiangHuStudy, emoneyAmount, true))
            {
                return;
            }

            foreach (Item item in useItems)
            {
                await user.UserPackage.SpendItemAsync(item);
            }

            jiangHu.PowerLevel = (byte)CurrentStage;

            await GenerateAsync(powerLevel, star, high, mode);
            await SendTalentAsync();
            await SendTimeAsync(user);

            await SaveAsync();

            await ActivityManager.UpdateTaskActivityAsync(user, ActivityManager.ActivityType.JiangHu);

            if (mode == KongFuImproveFeedbackMode.FreeCourse)
            {
                await user.Statistic.IncrementDailyValueAsync(TRAINING_COURSE_TYPE, TRAINING_COURSE_SUBTYPE_FREE);
            }
            else
            {
                await user.Statistic.IncrementDailyValueAsync(TRAINING_COURSE_TYPE, TRAINING_COURSE_SUBTYPE_PAID);
                LuaScriptManager.Run(user, null, null, Array.Empty<string>(),
                    $"TrainingGongfu({user.Identity},{jiangHuTimes.PaidTimes})");
            }
        }

        public async Task AwardTalentAsync(byte talent = 1)
        {
            Talent += talent;
            await SendTalentAsync();
            await SaveAsync();
        }

        public async Task<bool> SpendTalentAsync(byte talent = 1)
        {
            if (Talent < talent)
            {
                return false;
            }

            Talent -= talent;
            await SendTalentAsync();
            await SaveAsync();
            return true;
        }

        public Task ExitJiangHuAsync()
        {
            exitKongFuTimer.Startup(EXIT_KONG_FU_SECONDS);
            return SendStatusAsync();
        }

        public async Task SendStatusAsync()
        {
            if (jiangHu == null)
            {
                return;
            }

            MsgOwnKongfuBase msg = new()
            {
                Mode = KongfuBaseMode.SendStatus
            };
            msg.Strings.Add(user.Identity.ToString());
            msg.Strings.Add((Talent + 1).ToString());
            msg.Strings.Add(IsActive ? "1" : "2");
            await user.SendAsync(msg);
        }

        private static ushort GetStarIdentity(JiangHuAttrType type, JiangHuQuality quality)
        {
            return (ushort)((ushort)type + (ushort)quality * 256);
        }

        public async Task SendStarsAsync(Character target = null)
        {
            if (!HasJiangHu)
            {
                return;
            }

            MsgOwnKongfuImproveSummaryInfo msg = new()
            {
                Name = Name,
                Stage = (byte)CurrentStage,
                FreeTalentToday = JiangHuManager.GetMaxFreeCourse(),
                FreeTalentUsed = FreeCoursesUsedToday,
                Talent = (byte)(Talent + 1),
                Points = user.StudyPoints,
                BoughtTimes = (int)PaidCoursesUsedToday
            };
            foreach (DbJiangHuPlayerPower level in powers.OrderBy(x => x.Key).Select(x => x.Value))
            {
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type1, (JiangHuQuality)level.Quality1));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type2, (JiangHuQuality)level.Quality2));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type3, (JiangHuQuality)level.Quality3));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type4, (JiangHuQuality)level.Quality4));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type5, (JiangHuQuality)level.Quality5));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type6, (JiangHuQuality)level.Quality6));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type7, (JiangHuQuality)level.Quality7));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type8, (JiangHuQuality)level.Quality8));
                msg.Identities.Add(GetStarIdentity((JiangHuAttrType)level.Type9, (JiangHuQuality)level.Quality9));
            }

            if (target != null)
            {
                msg.Timer = 0xd1d401;
                await target.SendAsync(msg);
            }
            else
            {
                msg.Timer = 0xec8600;
                await user.SendAsync(msg);
            }
        }

        /// <summary>
        ///     Submits the latest star and power level to the target user.
        /// </summary>
        public Task SendStarAsync(Character target = null)
        {
            MsgOwnKongfuBase msg = new()
            {
                Mode = KongfuBaseMode.UpdateStar
            };
            msg.Strings.Add(user.Identity.ToString());
            msg.Strings.Add(CurrentStage.ToString());
            msg.Strings.Add(GetLatestStar().ToString());
            if (target != null)
            {
                return target.SendAsync(msg);
            }

            return user.Screen.BroadcastRoomMsgAsync(msg);
        }

        public Task SendTalentAsync(Character target = null)
        {
            MsgOwnKongfuBase msg = new()
            {
                Mode = KongfuBaseMode.UpdateTalent
            };
            msg.Strings.Add(user.Identity.ToString());
            msg.Strings.Add((Talent + 1).ToString());
            if (target != null)
            {
                return target.SendAsync(msg);
            }

            return user.Screen.BroadcastRoomMsgAsync(msg);
        }

        public Task SendTimeAsync(Character target = null)
        {
            MsgOwnKongfuBase msg = new()
            {
                Mode = KongfuBaseMode.UpdateTime
            };
            msg.Strings.Add(FreeCaltivateParam.ToString());
            msg.Strings.Add(SecondsTillNextCourse.ToString());
            if (target != null)
            {
                return target.SendAsync(msg);
            }

            return user.Screen.BroadcastRoomMsgAsync(msg);
        }

        public Task SendInfoAsync(Character target = null)
        {
            MsgOwnKongfuBase msg = new()
            {
                Mode = KongfuBaseMode.SendInfo
            };
            msg.Strings.Add(user.Identity.ToString());
            msg.Strings.Add(CurrentStage.ToString());
            msg.Strings.Add(GetLatestStar().ToString());
            if (target != null)
            {
                return target.SendAsync(msg);
            }

            return user.Screen.BroadcastRoomMsgAsync(msg);
        }

        public async Task SaveAsync()
        {
            if (jiangHu != null)
            {
                await ServerDbContext.UpdateAsync(jiangHu);

                if (jiangHuTimes != null)
                {
                    await ServerDbContext.UpdateAsync(jiangHuTimes);
                }

                await ServerDbContext.UpdateRangeAsync(powers.Values.ToList());
            }
        }

        public async Task LogoutAsync()
        {
            if (exitKongFuTimer.IsActive() && !exitKongFuTimer.IsTimeOut())
            {
                JiangHuManager.StoreJiangHuRemainingTime(user.Identity, exitKongFuTimer.GetRemain());
            }
            else if (IsActive)
            {
                JiangHuManager.StoreJiangHuRemainingTime(user.Identity, EXIT_KONG_FU_SECONDS);
            }

            if (jiangHu != null)
            {
                using ServerDbContext ctx = new();
                ctx.JiangHuPlayers.Update(jiangHu);
                if (jiangHuTimes != null)
                {
                    ctx.JiangHuCaltivateTimes.Update(jiangHuTimes);
                }

                ctx.JiangHuPlayerPowers.UpdateRange(powers.Values.ToList());
                await ctx.SaveChangesAsync();
            }
        }

        private JiangHuStar latestSavedStar;

        public async Task GenerateAsync(byte powerLevel, byte star, int high, KongFuImproveFeedbackMode mode)
        {
            QueryStar(powerLevel, star, out JiangHuStar currentStar);

            int max = 0;
            List<DbJiangHuQualityRand> qualityRates = JiangHuManager.GetQualityRates(powerLevel);
            Dictionary<int, JiangHuQuality> qualities = new();
            foreach (DbJiangHuQualityRand q in qualityRates)
            {
                if ((JiangHuQuality)q.PowerQuality == JiangHuQuality.Epic && currentStar.Quality < JiangHuQuality.Ultra)
                {
                    continue;
                }

                switch (high)
                {
                    case 0:
                    {
                        if (q.CommonRate == 0)
                        {
                            continue;
                        }

                        max += q.CommonRate;
                        qualities.Add(max, (JiangHuQuality)q.PowerQuality);
                        break;
                    }
                    case 1:
                    {
                        if (q.CritRate == 0)
                        {
                            continue;
                        }

                        max += q.CritRate;
                        qualities.Add(max, (JiangHuQuality)q.PowerQuality);
                        break;
                    }
                    case 2:
                    {
                        if (q.HighCritRate == 0)
                        {
                            continue;
                        }

                        max += q.HighCritRate;
                        qualities.Add(max, (JiangHuQuality)q.PowerQuality);
                        break;
                    }
                }
            }

            JiangHuQuality quality = JiangHuQuality.None;
            int rand = await NextAsync(max);
            foreach (KeyValuePair<int, JiangHuQuality> q in qualities.OrderBy(x => x.Key))
            {
                if (rand < q.Key)
                {
                    quality = q.Value;
                    break;
                }
            }

            max = 0;
            List<DbJiangHuAttribRand> attributeRates = JiangHuManager.GetAttributeRates(powerLevel);
            Dictionary<int, JiangHuAttrType> types = new();
            foreach (DbJiangHuAttribRand attr in attributeRates)
            {
                max += attr.Rate;
                types.Add(max, (JiangHuAttrType)attr.PowerAttribute);
            }

            JiangHuAttrType type = JiangHuAttrType.None;
            rand = await NextAsync(max);
            foreach (KeyValuePair<int, JiangHuAttrType> itype in types.OrderBy(x => x.Key))
            {
                if (rand < itype.Key)
                {
                    type = itype.Value;
                    break;
                }
            }

            if (!currentStar.Equals(default))
            {
                latestSavedStar = currentStar;
            }

            DbJiangHuPlayerPower attribute = SetAttribute(powerLevel, star, quality, type);
            if (attribute != null)
            {
                await SaveAttributeAsync(attribute);
            }

            if (mode == KongFuImproveFeedbackMode.FreeCourse)
            {
                FreeCoursesUsedToday += 1;
            }
            else
            {
                PaidCoursesUsedToday += 1;
            }

            await user.SendAsync(new MsgOwnKongfuImproveFeedback
            {
                FreeCourse = (int)FreeCaltivateParam,
                Star = star,
                Stage = powerLevel,
                FreeCourseUsedToday = FreeCoursesUsedToday,
                PaidRounds = (int)PaidCoursesUsedToday,
                Attribute = GetStarIdentity(type, quality)
            });

            if (jiangHuTimes.Id == 0)
            {
                await ServerDbContext.CreateAsync(jiangHuTimes);
            }
            else
            {
                await ServerDbContext.UpdateAsync(jiangHuTimes);
            }

            UpdateAllAttributes();
            await UpdateAsync();

            gmLog.Information(
                $"{user.Identity},{user.Name},generate,{type},{quality},{GetStageInnerPower(powerLevel)},{InnerPower},{MaxInnerPowerHistory},{Grade}");

#if DEBUG
            logger.Debug(
                $"[{user.Identity},{user.Name}] JiangHu New Attribute [{type},{quality}] InnerPower [{GetStageInnerPower(powerLevel)},{InnerPower},{MaxInnerPowerHistory}] Grade[{Grade}]");
#endif
        }

        public async Task<bool> RestoreAsync(byte powerLevel, byte star)
        {
            if (latestSavedStar.Equals(default))
            {
                return false;
            }

            if (!QueryStar(powerLevel, star, out JiangHuStar current))
            {
                return false;
            }

            if (current.PowerLevel != latestSavedStar.PowerLevel || current.Star != latestSavedStar.Star)
            {
                return false;
            }

            DbJiangHuPlayerPower attribute =
                SetAttribute(powerLevel, star, latestSavedStar.Quality, latestSavedStar.Type);
            if (attribute != null)
            {
                if (attribute.Id == 0)
                {
                    await ServerDbContext.CreateAsync(attribute);
                }
                else
                {
                    await ServerDbContext.UpdateAsync(attribute);
                }
            }

            latestSavedStar = default;

            UpdateAllAttributes();
            await UpdateAsync();

            gmLog.Information(
                $"{user.Identity},{user.Name},restore,{latestSavedStar.Type},{latestSavedStar.Quality},{GetStageInnerPower(powerLevel)},{InnerPower},{MaxInnerPowerHistory},{Grade}");

#if DEBUG
            logger.Debug(
                $"[{user.Identity},{user.Name}] JiangHu Retore Attribute [{latestSavedStar.Type},{latestSavedStar.Quality}] InnerPower [{GetStageInnerPower(powerLevel)},{InnerPower},{MaxInnerPowerHistory}] Grade[{Grade}]");
#endif

            return true;
        }

        public async Task UpdateAsync()
        {
            byte nextStage = (byte)(CurrentStage + 1);
            DbJiangHuCaltivateCondition condition = JiangHuManager.GetCaltivateCondition((byte)CurrentStage);
            if (GetLatestStar() == 9
                && CurrentStage < 9
                && condition.NeedCultivateValue < GetStageInnerPower((byte)CurrentStage)
                && !powers.TryGetValue(nextStage, out _))
            {
                powers.TryAdd(nextStage, new DbJiangHuPlayerPower
                {
                    Level = nextStage,
                    PlayerId = user.Identity
                });
                await SendStarsAsync();
            }

            await user.SendAsync(new MsgPlayerAttribInfo(user));
        }

        public int GetStageInnerPower(byte powerLevel)
        {
            if (powerLevel < 1 || powerLevel > 9)
            {
                return 0;
            }

            if (!powers.TryGetValue(powerLevel, out DbJiangHuPlayerPower powerValue))
            {
                return 0;
            }

            List<JiangHuStar> stars = new();
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality1, (JiangHuAttrType)powerValue.Type1,
                powerLevel, 1));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality2, (JiangHuAttrType)powerValue.Type2,
                powerLevel, 2));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality3, (JiangHuAttrType)powerValue.Type3,
                powerLevel, 3));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality4, (JiangHuAttrType)powerValue.Type4,
                powerLevel, 4));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality5, (JiangHuAttrType)powerValue.Type5,
                powerLevel, 5));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality6, (JiangHuAttrType)powerValue.Type6,
                powerLevel, 6));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality7, (JiangHuAttrType)powerValue.Type7,
                powerLevel, 7));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality8, (JiangHuAttrType)powerValue.Type8,
                powerLevel, 8));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality9, (JiangHuAttrType)powerValue.Type9,
                powerLevel, 9));

            int innerPower = 0;
            int currentInnerPower = 0;
            int align = 1;
            JiangHuAttrType lastType = JiangHuAttrType.None;
            foreach (JiangHuStar star in stars.Where(x => x.Type != JiangHuAttrType.None))
            {
                if (lastType != JiangHuAttrType.None && lastType != star.Type)
                {
                    innerPower += (int)(currentInnerPower * Managers.JiangHuManager.SequenceInnerStrength[align]);

                    align = 1;
                    currentInnerPower = 0;
                }

                currentInnerPower += (int)Managers.JiangHuManager.PowerValue[(int)(star.Quality - 1)];

                if (lastType != JiangHuAttrType.None && lastType == star.Type)
                {
                    align++;
                }

                lastType = star.Type;
            }

            if (currentInnerPower != 0)
            {
                innerPower += (int)(currentInnerPower * Managers.JiangHuManager.SequenceInnerStrength[align]);
            }

            return innerPower;
        }

        public byte GetLatestStar()
        {
            if (powers.TryGetValue((byte)CurrentStage, out DbJiangHuPlayerPower value))
            {
                if (value.Type1 == 0)
                {
                    return 0;
                }

                if (value.Type2 == 0)
                {
                    return 1;
                }

                if (value.Type3 == 0)
                {
                    return 2;
                }

                if (value.Type4 == 0)
                {
                    return 3;
                }

                if (value.Type5 == 0)
                {
                    return 4;
                }

                if (value.Type6 == 0)
                {
                    return 5;
                }

                if (value.Type7 == 0)
                {
                    return 6;
                }

                if (value.Type8 == 0)
                {
                    return 7;
                }

                if (value.Type9 == 0)
                {
                    return 8;
                }

                return 9;
            }

            return 0;
        }

        public DbJiangHuPlayerPower SetAttribute(byte level, byte star, JiangHuQuality quality, JiangHuAttrType type)
        {
            if (!powers.TryGetValue(level, out DbJiangHuPlayerPower powerValue))
            {
                powerValue = new DbJiangHuPlayerPower
                {
                    Level = level,
                    PlayerId = user.Identity
                };
                powers.TryAdd(level, powerValue);
            }

            switch (star)
            {
                case 1:
                {
                    powerValue.Quality1 = (byte)quality;
                    powerValue.Type1 = (byte)type;
                    break;
                }
                case 2:
                {
                    powerValue.Quality2 = (byte)quality;
                    powerValue.Type2 = (byte)type;
                    break;
                }
                case 3:
                {
                    powerValue.Quality3 = (byte)quality;
                    powerValue.Type3 = (byte)type;
                    break;
                }
                case 4:
                {
                    powerValue.Quality4 = (byte)quality;
                    powerValue.Type4 = (byte)type;
                    break;
                }
                case 5:
                {
                    powerValue.Quality5 = (byte)quality;
                    powerValue.Type5 = (byte)type;
                    break;
                }
                case 6:
                {
                    powerValue.Quality6 = (byte)quality;
                    powerValue.Type6 = (byte)type;
                    break;
                }
                case 7:
                {
                    powerValue.Quality7 = (byte)quality;
                    powerValue.Type7 = (byte)type;
                    break;
                }
                case 8:
                {
                    powerValue.Quality8 = (byte)quality;
                    powerValue.Type8 = (byte)type;
                    break;
                }
                case 9:
                {
                    powerValue.Quality9 = (byte)quality;
                    powerValue.Type9 = (byte)type;
                    break;
                }
            }

            return powerValue;
        }

        private void InternalResetAttributes()
        {
            MaxLife = 0;
            MaxMana = 0;
            Attack = 0;
            MagicAttack = 0;
            Defense = 0;
            MagicDefense = 0;
            FinalDamage = 0;
            FinalMagicDamage = 0;
            FinalDefense = 0;
            FinalMagicDefense = 0;
            CriticalStrike = 0;
            SkillCriticalStrike = 0;
            Breakthrough = 0;
            Immunity = 0;
            Counteraction = 0;
        }

        public bool QueryStar(byte level, byte star, out JiangHuStar jiangHuStar)
        {
            jiangHuStar = default;
            if (!powers.TryGetValue(level, out DbJiangHuPlayerPower powerValue))
            {
                return false;
            }

            switch (star)
            {
                case 1:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality1,
                        (JiangHuAttrType)powerValue.Type1, level, star);
                    break;
                case 2:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality2,
                        (JiangHuAttrType)powerValue.Type2, level, star);
                    break;
                case 3:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality3,
                        (JiangHuAttrType)powerValue.Type3, level, star);
                    break;
                case 4:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality4,
                        (JiangHuAttrType)powerValue.Type4, level, star);
                    break;
                case 5:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality5,
                        (JiangHuAttrType)powerValue.Type5, level, star);
                    break;
                case 6:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality6,
                        (JiangHuAttrType)powerValue.Type6, level, star);
                    break;
                case 7:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality7,
                        (JiangHuAttrType)powerValue.Type7, level, star);
                    break;
                case 8:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality8,
                        (JiangHuAttrType)powerValue.Type8, level, star);
                    break;
                case 9:
                    jiangHuStar = new JiangHuStar((JiangHuQuality)powerValue.Quality9,
                        (JiangHuAttrType)powerValue.Type9, level, star);
                    break;
                default:
                {
                    return false;
                }
            }

            return true;
        }

        public void UpdateAllAttributes()
        {
            InnerPower = 0;
            InternalResetAttributes();

            JiangHuQuality quality = JiangHuQuality.Epic;
            int grade = 0;
            for (byte i = 1; i <= CurrentStage; i++)
            {
                UpdateAttributes(i, out int g, out JiangHuQuality q);
                grade += g;
                if (q < quality)
                {
                    quality = q;
                }
            }

            Grade = grade + (int)quality;
        }

        public void UpdateAttributes(byte level, out int grade, out JiangHuQuality qualityAlign)
        {
            grade = 0;
            qualityAlign = JiangHuQuality.None;

            if (!powers.TryGetValue(level, out DbJiangHuPlayerPower powerValue))
            {
                return;
            }

            List<JiangHuStar> stars = new();
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality1, (JiangHuAttrType)powerValue.Type1, level,
                1));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality2, (JiangHuAttrType)powerValue.Type2, level,
                2));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality3, (JiangHuAttrType)powerValue.Type3, level,
                3));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality4, (JiangHuAttrType)powerValue.Type4, level,
                4));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality5, (JiangHuAttrType)powerValue.Type5, level,
                5));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality6, (JiangHuAttrType)powerValue.Type6, level,
                6));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality7, (JiangHuAttrType)powerValue.Type7, level,
                7));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality8, (JiangHuAttrType)powerValue.Type8, level,
                8));
            stars.Add(new JiangHuStar((JiangHuQuality)powerValue.Quality9, (JiangHuAttrType)powerValue.Type9, level,
                9));

            JiangHuQuality lowestQuality = JiangHuQuality.Epic;

            List<int> currentPowerList = new();
            int align = 1;
            JiangHuAttrType lastType = JiangHuAttrType.None;
            uint currentInnerPower = 0;
            foreach (JiangHuStar star in stars.Where(x => x.Type != JiangHuAttrType.None).OrderBy(x => x.Star))
            {
                if (lastType != JiangHuAttrType.None && lastType != star.Type)
                {
                    int power = 0;
                    foreach (int pow in currentPowerList)
                    {
                        power += (int)(pow * Managers.JiangHuManager.SequenceBonus[align]);
                    }

                    InnerPower += (uint)(currentInnerPower * Managers.JiangHuManager.SequenceInnerStrength[align]);

                    AddAttribute(lastType, power);

                    currentPowerList.Clear();
                    align = 1;
                    currentInnerPower = 0;
                }

                currentPowerList.Add(JiangHuManager.GetPowerEffect(star.Type, star.Quality)?.AttribValue ?? 0);
                currentInnerPower += Managers.JiangHuManager.PowerValue[(int)(star.Quality - 1)];

                if (lastType != JiangHuAttrType.None && lastType == star.Type)
                {
                    align++;
                }

                if (star.Quality < lowestQuality)
                {
                    lowestQuality = star.Quality;
                }

                lastType = star.Type;
            }

            if (currentPowerList.Count > 0)
            {
                int power = 0;
                foreach (int pow in currentPowerList)
                {
                    power += (int)(pow * Managers.JiangHuManager.SequenceBonus[align]);
                }

                if (currentInnerPower != 0)
                {
                    InnerPower += (uint)(currentInnerPower * Managers.JiangHuManager.SequenceInnerStrength[align]);
                }

                AddAttribute(lastType, power);

                if (stars.Count == 9)
                {
                    grade = 1;
                }

                qualityAlign = lowestQuality;
            }

            MaxInnerPowerHistory = Math.Max(InnerPower, MaxInnerPowerHistory);
        }

        public void AddAttribute(JiangHuAttrType type, int value)
        {
            switch (type)
            {
                case JiangHuAttrType.MaxLife:
                    MaxLife += value;
                    break;
                case JiangHuAttrType.Attack:
                    Attack += value;
                    break;
                case JiangHuAttrType.MagicAttack:
                    MagicAttack += value;
                    break;
                case JiangHuAttrType.Defense:
                    Defense += value;
                    break;
                case JiangHuAttrType.MagicDefense:
                    MagicDefense += value;
                    break;
                case JiangHuAttrType.FinalDamage:
                    FinalDamage += value;
                    break;
                case JiangHuAttrType.FinalMagicDamage:
                    FinalMagicDamage += value;
                    break;
                case JiangHuAttrType.FinalDefense:
                    FinalDefense += value;
                    break;
                case JiangHuAttrType.FinalMagicDefense:
                    FinalMagicDefense += value;
                    break;
                case JiangHuAttrType.CriticalStrike:
                    CriticalStrike += value;
                    break;
                case JiangHuAttrType.SkillCriticalStrike:
                    SkillCriticalStrike += value;
                    break;
                case JiangHuAttrType.Immunity:
                    Immunity += value;
                    break;
                case JiangHuAttrType.Breakthrough:
                    Breakthrough += value;
                    break;
                case JiangHuAttrType.Counteraction:
                    Counteraction += value;
                    break;
                case JiangHuAttrType.MaxMana:
                    MaxMana += value;
                    break;
            }
        }

        public Task SubmitOSDataAsync(ulong sessionId, uint serverId)
        {
            if (jiangHu == null) {  return Task.CompletedTask; }
            return RealmConnectionManager.SendOSMsgAsync(new MsgCrossOwnKongFuInfoC
            {
                Data = new()
                {
                    FreeCaltivateParam = FreeCaltivateParam,
                    GenuineQiLevel = Talent,
                    Name = Name,
                    PowerLevel = jiangHu.PowerLevel,
                    SessionId = sessionId,
                    TotalPowerValue = jiangHu.TotalPowerValue,
                    List = powers.Values.Select(x => new CrossOwnKongFuInfoPB
                    {
                        Level = x.Level,
                        Quality1 = x.Quality1,
                        Quality2 = x.Quality2,
                        Quality3 = x.Quality3,
                        Quality4 = x.Quality4,
                        Quality5 = x.Quality5,
                        Quality6 = x.Quality6,
                        Quality7 = x.Quality7,
                        Quality8 = x.Quality8,
                        Quality9 = x.Quality9,
                        Type1 = x.Type1,
                        Type2 = x.Type2,
                        Type3 = x.Type3,
                        Type4 = x.Type4,
                        Type5 = x.Type5,
                        Type6 = x.Type6,
                        Type7 = x.Type7,
                        Type8 = x.Type8,
                        Type9 = x.Type9
                    }).ToList()
                }
            }, serverId);
        }

        public async Task DailyClearAsync()
        {
            if (jiangHuTimes != null)
            {
                await ServerDbContext.DeleteAsync(jiangHuTimes);
                jiangHuTimes = null;
            }
        }

        public async Task OnTimerAsync()
        {
            if (!HasJiangHu || user.IsOSUser())
            {
                return;
            }

            if (awardPointsTimer.ToNextTime() && FreeCaltivateParam < MAX_FREE_COURSE * POINTS_TO_COURSE)
            {
                FreeCaltivateParam += (uint)JiangHuManager.GetCaltivateProgress(user);
            }

            if (exitKongFuTimer.IsActive() && exitKongFuTimer.IsTimeOut())
            {
                await SendStatusAsync();
                exitKongFuTimer.Clear();
            }
        }

        private static Task SaveAttributeAsync(DbJiangHuPlayerPower power)
        {
            if (power.Id == 0)
            {
                return ServerDbContext.CreateAsync(power);
            }

            return ServerDbContext.UpdateAsync(power);
        }
    }
}