using Long.Database.Entities;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.NeiGong
{
    public interface INeiGongManager
    {
        const byte FINAL_RAND_INFO = 10;

        Task InitializeAsync();
        DbInnerStrenghtTypeLevInfo QueryTypeLevInfo(byte type, byte level);
        DbInnerStrenghtSecretType QuerySecretType(byte type);
        InnerStrengthTypeInfo QueryTypeInfo(byte type);
        List<DbInnerStrenghtTypeLevInfo> QueryTypeLevelInfosForAttributes(byte type, byte level);
        int GetStrenghtMaxLevel(byte type);
        Task<int> CalculateCurrentValueAsync(int type, int currentLevel, int currentAbolish);
        int CalculateMaxValue(int type, int currentValue, int currentLevel, int currentAbolish);
        int GetMaxValueAllowed(int currentAbolish, int maxAbolish);
        INeiGong InitializeOSData(Character user);

        public enum InnerStrengthAttrType
        {
            None,
            MaxLife,
            Attack,
            MagicAttack,
            Defense,
            MagicDefense,
            FinalPhysicalDamage,
            FinalMagicalDamage,
            FinalPhysicalDefense,
            FinalMagicalDefense,
            CriticalStrike,
            SkillCriticalStrike,
            Immunity,
            Breakthrough,
            Counteraction
        }

        public class InnerStrengthTypeInfo
        {
            private readonly DbInnerStrenghtTypeInfo innerStrenghtTypeInfo;

            public InnerStrengthTypeInfo(DbInnerStrenghtTypeInfo innerStrenghtTypeInfo)
            {
                this.innerStrenghtTypeInfo = innerStrenghtTypeInfo;
            }

            public byte SecretType => innerStrenghtTypeInfo.SecretType;

            public byte Rand1 => (byte)innerStrenghtTypeInfo.RandType1;
            public byte Rand2 => (byte)innerStrenghtTypeInfo.RandType2;
            public byte Rand3 => (byte)innerStrenghtTypeInfo.RandType3;

            public int AbolishCulture => (int)innerStrenghtTypeInfo.AbolishCulture;

            public int AbolishCount
            {
                get
                {
                    if (Rand1 == FINAL_RAND_INFO)
                    {
                        return 0;
                    }
                    if (Rand2 == FINAL_RAND_INFO)
                    {
                        return 1;
                    }
                    return 2;
                }
            }

            public int MaxLife => (int)innerStrenghtTypeInfo.MaxLife;
            public int PhysicAttackNew => (int)innerStrenghtTypeInfo.PhysicAttackNew;
            public int MagicAttack => (int)innerStrenghtTypeInfo.MagicAttack;
            public int PhysicDefenseNew => (int)innerStrenghtTypeInfo.PhysicDefenseNew;
            public int MagicDefense => (int)innerStrenghtTypeInfo.MagicDefense;
            public int FinalPhysicAdd => innerStrenghtTypeInfo.FinalPhysicAdd;
            public int FinalMagicAdd => innerStrenghtTypeInfo.FinalMagicAdd;
            public int FinalPhysicReduce => innerStrenghtTypeInfo.FinalPhysicReduce;
            public int FinalMagicReduce => innerStrenghtTypeInfo.FinalMagicReduce;
            public int PhysicCrit => innerStrenghtTypeInfo.PhysicCrit;
            public int MagicCrit => innerStrenghtTypeInfo.MagicCrit;
            public int DefenseCrit => innerStrenghtTypeInfo.DefenseCrit;
            public int SmashRate => innerStrenghtTypeInfo.SmashRate;
            public int FirmDefenseRate => innerStrenghtTypeInfo.FirmDefenseRate;
        }
    }
}
