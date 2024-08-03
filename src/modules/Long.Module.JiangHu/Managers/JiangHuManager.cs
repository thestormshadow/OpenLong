using System.Collections.Concurrent;
using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules.Systems.JiangHu;
using Long.Kernel.States.User;
using Long.Module.JiangHu.Repositories;
using Long.Module.JiangHu.States;
using Long.World.Enums;
using static Long.Kernel.Modules.Systems.JiangHu.IJiangHuManager;

namespace Long.Module.JiangHu.Managers
{
    public sealed class JiangHuManager : IJiangHuManager
    {
        private static readonly ILogger logger = Log.ForContext<IJiangHuManager>();

        private const int CONFIG_JIANGHU_GROW_SPEED = 60001; // data1 quality, data2 normal speed, data3 high speed

        private const int
            CONFIG_JIANGHU_FREE_GROWTH =
                60002; // data1 max count, data2 idk, data3 limit? max count must be data3/data1?

        //private const uint CONFIG_JIANGHU_ITEMS = 60004; // 
        private const int CONFIG_JIANGHU_PAID_GROWTH_PRICE = 60005;

        private readonly Dictionary<byte, DbConfig> growthSpeedList = new();
        private DbConfig freeGrowthSettings;
        private DbConfig paidGrowthSettings;

        private readonly Dictionary<byte, List<DbJiangHuAttribRand>> AttributeRates = new();
        private readonly Dictionary<byte, List<DbJiangHuQualityRand>> QualityRates = new();
        private readonly Dictionary<byte, DbJiangHuCaltivateCondition> CaltivateConditions = new();
        private readonly Dictionary<JiangHuAttrType, List<DbJiangHuPowerEffect>> PowerEffects = new();

        private readonly ConcurrentDictionary<uint, int> remainingJiangHuTime = new();

        public async Task InitializeAsync()
        {
            logger.Information("Initializing Jiang Hu data");

            IList<DbConfig> config = await ConfigRepository.GetAsync(CONFIG_JIANGHU_GROW_SPEED);
            foreach (DbConfig cfg in config)
            {
                growthSpeedList.TryAdd((byte)cfg.Data1, cfg);
            }

            freeGrowthSettings = await ConfigRepository.GetSingleAsync(CONFIG_JIANGHU_FREE_GROWTH);
            paidGrowthSettings = await ConfigRepository.GetSingleAsync(CONFIG_JIANGHU_PAID_GROWTH_PRICE);

            List<DbJiangHuAttribRand> attributes = await JiangHuAttribRandRepository.GetAsync();
            foreach (DbJiangHuAttribRand attribute in attributes)
            {
                if (!AttributeRates.TryGetValue(attribute.PowerLevel, out List<DbJiangHuAttribRand> rates))
                {
                    rates = new List<DbJiangHuAttribRand>();
                    AttributeRates.Add(attribute.PowerLevel, rates);
                }

                rates.Add(attribute);
            }

            List<DbJiangHuQualityRand> qualities = await JiangHuQualityRandRepository.GetAsync();
            foreach (DbJiangHuQualityRand quality in qualities)
            {
                if (!QualityRates.TryGetValue(quality.PowerLevel, out List<DbJiangHuQualityRand> rates))
                {
                    rates = new List<DbJiangHuQualityRand>();
                    QualityRates.Add(quality.PowerLevel, rates);
                }

                rates.Add(quality);
            }

            List<DbJiangHuCaltivateCondition> conditions = await JiangHuCaltivateConditionRepository.GetAsync();
            foreach (DbJiangHuCaltivateCondition condition in conditions)
            {
                CaltivateConditions.TryAdd(condition.PowerLevel, condition);
            }

            List<DbJiangHuPowerEffect> powerEffects = await JiangHuPowerEffectRepository.GetAsync();
            foreach (DbJiangHuPowerEffect effect in powerEffects)
            {
                if (!PowerEffects.TryGetValue((JiangHuAttrType)effect.Type, out List<DbJiangHuPowerEffect> effects))
                {
                    effects = new List<DbJiangHuPowerEffect>();
                    PowerEffects.Add((JiangHuAttrType)effect.Type, effects);
                }

                effects.Add(effect);
            }
        }

        public int GetCaltivateProgress(Character user)
        {
            if (growthSpeedList.TryGetValue(user.JiangHu.Talent, out DbConfig talentSetting))
            {
                if (user.JiangHu.IsActive
                    && user.Map.QueryRegion(RegionType.JiangHuBonusArea, user.X, user.Y))
                {
                    return talentSetting.Data3;
                }

                return talentSetting.Data2;
            }

            return 0;
        }

        public int GetCaltivateProgress(byte talent)
        {
            if (growthSpeedList.TryGetValue(talent, out DbConfig talentSetting))
            {
                return talentSetting.Data2;
            }

            return 0;
        }

        public int GetMaximumFreeCultivationValue()
        {
            return freeGrowthSettings?.Data3 ?? 1_000_000;
        }

        public byte GetMaxTalentLevel()
        {
            return (byte)growthSpeedList.Values.Max(x => x.Data1);
        }

        public byte GetMaxFreeCourse()
        {
            return (byte)(freeGrowthSettings?.Data1 ?? 10);
        }

        public DbJiangHuPowerEffect GetPowerEffect(JiangHuAttrType type, JiangHuQuality quality)
        {
            if (!PowerEffects.TryGetValue(type, out List<DbJiangHuPowerEffect> value))
            {
                return null;
            }

            return value.Find(x => x.Quality == (byte)quality);
        }

        public List<DbJiangHuAttribRand> GetAttributeRates(byte powerLevel)
        {
            return AttributeRates.TryGetValue(powerLevel, out List<DbJiangHuAttribRand> rates)
                ? new List<DbJiangHuAttribRand>(rates)
                : new();
        }

        public List<DbJiangHuQualityRand> GetQualityRates(byte powerLevel)
        {
            return QualityRates.TryGetValue(powerLevel, out List<DbJiangHuQualityRand> rates)
                ? new List<DbJiangHuQualityRand>(rates)
                : new();
        }

        public DbJiangHuCaltivateCondition GetCaltivateCondition(byte powerLevel)
        {
            return CaltivateConditions.GetValueOrDefault(powerLevel);
        }

        public void StoreJiangHuRemainingTime(uint idUser, int seconds)
        {
            remainingJiangHuTime.TryAdd(idUser, seconds);
        }

        public int GetJiangHuRemainingTime(uint idUser)
        {
            return remainingJiangHuTime.TryRemove(idUser, out int value) ? value : 0;
        }

        public IJiangHu InitializeOSData(Character user)
        {
            return new OwnKongFu(user);
        }

        public static readonly double[] SequenceBonus =
        [
            1.0d,
            1.0d,
            1.1d,
            1.13d,
            1.15d,
            1.18d,
            1.21d,
            1.25d,
            1.3d,
            1.5d
        ];

        public static readonly double[] SequenceInnerStrength =
        [
            1.0d,
            1.0d,
            1.1d,
            1.2d,
            1.3d,
            1.4d,
            1.5d,
            1.6d,
            1.8d,
            2.0d
        ];

        public static readonly uint[] PowerValue =
        [
            100, 120, 150, 200, 300, 500
        ];
    }
}