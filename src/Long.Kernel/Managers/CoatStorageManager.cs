using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class CoatStorageManager
    {
        private static readonly ILogger logger = Log.ForContext<CoatStorageManager>();

        private static readonly ConcurrentDictionary<uint, DbCoatStorageType> coatStorageTypes = new();
        private static readonly ConcurrentDictionary<uint, DbCoatStorageAttr> coatStorageAttrs = new();

        protected CoatStorageManager()
        {            
        }

        public static async Task InitializeAsync()
        {
            logger.Information("Loading coat storage manager");

            foreach (var type in CoatStorageRepository.GetCoatTypes())
            {
                coatStorageTypes.TryAdd(type.ItemType, type);
            }

            foreach (var attr in CoatStorageRepository.GetCoatAttributes())
            {
                coatStorageAttrs.TryAdd(attr.Id, attr);
            }
        }

        public static uint GetCoatStorageId(uint itemType)
        {
            if (coatStorageTypes.TryGetValue(itemType, out var coatStorageType))
            {
                return coatStorageType.CoatId;
            }
            return itemType;
        }

        public static List<DbCoatStorageAttr> GetStorageAttributes(uint type, int level)
        {
            return coatStorageAttrs.Values.Where(x => x.CoatType == type && x.Amount <= level).ToList();
        }

        public enum CoatStorageAttributeType
        {
            None,
            MaxLife,
            Attack,
            MagicAttack,
            Defense,
            MagicDefense,
            FinalDamage,
            FinalMagicDamage,
            FinalDefense,
            FinalMagicDefense,
            CriticalStrike,
            SkillCriticalStrike,
            Immunity,
            Breakthrough,
            Counteraction,
            HeavenBlessing,
            ExtraChampionPoints,
            HuntingExperienceBonus
        }
    }
}
