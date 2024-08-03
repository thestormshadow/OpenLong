using Long.Database.Entities;
using Long.Kernel.Modules.Systems.NeiGong;
using static Long.Kernel.Modules.Systems.NeiGong.INeiGongManager;
using System.Collections.Concurrent;
using Long.Module.NeiGong.Repositories;
using Long.Kernel.States.User;

namespace Long.Module.NeiGong.Managers
{
    public sealed class NeiGongManager : INeiGongManager
    {
        private static readonly ILogger logger = Log.ForContext<NeiGongManager>();

        private static readonly ConcurrentDictionary<ushort, Dictionary<byte, DbInnerStrenghtTypeLevInfo>> typeLevInfo = new();
        private static readonly ConcurrentDictionary<byte, InnerStrengthTypeInfo> typeInfo = new();
        private static readonly ConcurrentDictionary<byte, DbInnerStrenghtSecretType> secretTypeInfo = new();
        private static readonly ConcurrentDictionary<byte, List<DbInnerStrengthRand>> rands = new();

        public async Task InitializeAsync()
        {
            logger.Information("Initializing Inner Strength Manager");

            foreach (var info in await InnerStrenghtRepository.GetTypeLevAsync())
            {
                if (typeLevInfo.ContainsKey(info.Type))
                {
                    typeLevInfo[info.Type].Add(info.Level, info);
                }
                else
                {
                    typeLevInfo.TryAdd(info.Type, new Dictionary<byte, DbInnerStrenghtTypeLevInfo>());
                    typeLevInfo[info.Type].Add(info.Level, info);
                }
            }

            foreach (var info in await InnerStrenghtRepository.GetTypesAsync())
            {
                typeInfo.TryAdd((byte)info.Identity, new InnerStrengthTypeInfo(info));
            }

            foreach (var info in await InnerStrenghtRepository.GetSecretTypeAsync())
            {
                secretTypeInfo.TryAdd((byte)info.Identity, info);
            }

            foreach (var range in await InnerStrenghtRepository.GetRandRangeAsync())
            {
                if (rands.ContainsKey(range.StrengthNo))
                {
                    rands[range.StrengthNo].Add(range);
                }
                else
                {
                    rands.TryAdd(range.StrengthNo, new());
                    rands[range.StrengthNo].Add(range);
                }
            }
        }

        public DbInnerStrenghtTypeLevInfo QueryTypeLevInfo(byte type, byte level)
        {
            if (typeLevInfo.TryGetValue(type, out var infos))
            {
                if (infos.TryGetValue(level, out var info))
                {
                    return info;
                }
            }
            return null;
        }

        public DbInnerStrenghtSecretType QuerySecretType(byte type)
        {
            if (secretTypeInfo.TryGetValue(type, out var info))
            {
                return info;
            }
            return null;
        }

        public InnerStrengthTypeInfo QueryTypeInfo(byte type)
        {
            if (typeInfo.TryGetValue(type, out var info))
            {
                return info;
            }
            return null;
        }

        public List<DbInnerStrenghtTypeLevInfo> QueryTypeLevelInfosForAttributes(byte type, byte level)
        {
            if (typeLevInfo.TryGetValue(type, out var infos))
            {
                return infos.Values.Where(x => x.Level < level).ToList();
            }
            return new List<DbInnerStrenghtTypeLevInfo>();
        }

        public int GetStrenghtMaxLevel(byte type)
        {
            if (typeLevInfo.TryGetValue(type, out var typeInfo))
            {
                return typeInfo.Values.Max(x => x.Level);
            }
            return 0;
        }

        public async Task<int> CalculateCurrentValueAsync(int type, int currentLevel, int currentAbolish)
        {
            var typeInfo = QueryTypeInfo((byte)type);
            var maxLevel = GetStrenghtMaxLevel((byte)type);
            var maxAllowedValue = GetMaxValueAllowed(currentAbolish, typeInfo.AbolishCount);
            if (currentLevel == maxLevel)
            {
                return maxAllowedValue;
            }

            // to simplify the code, i'll just do this calc
            int deltaLevelValue = (int)Math.Ceiling(maxAllowedValue / (float)maxLevel);
            int newValue = await NextAsync(deltaLevelValue * (currentLevel - 1), deltaLevelValue * currentLevel);
            return Math.Min(maxAllowedValue, newValue);
        }

        public int CalculateMaxValue(int type, int currentValue, int currentLevel, int currentAbolish)
        {
            var typeInfo = QueryTypeInfo((byte)type);
            if (currentValue < 100)
            {
                return 0;
            }
            if (currentLevel < GetStrenghtMaxLevel((byte)type))
            {
                return 0;
            }
            if (currentAbolish < typeInfo.AbolishCount)
            {
                return 0;
            }
            return 100;
        }

        public int GetMaxValueAllowed(int currentAbolish, int maxAbolish)
        {
            if (currentAbolish == maxAbolish)
            {
                return 100;
            }
            if ((currentAbolish == 0 && maxAbolish == 1) || currentAbolish == 1)
            {
                return 55;
            }
            return 33;
        }

        public INeiGong InitializeOSData(Character user)
        {
            return new States.InnerStrength(user);
        }
    }
}
