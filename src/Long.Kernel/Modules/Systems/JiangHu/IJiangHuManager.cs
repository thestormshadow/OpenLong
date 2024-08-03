using Long.Database.Entities;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Systems.JiangHu
{
    public interface IJiangHuManager
    {
        Task InitializeAsync();
        DbJiangHuPowerEffect GetPowerEffect(JiangHuAttrType type, JiangHuQuality quality);
        List<DbJiangHuAttribRand> GetAttributeRates(byte powerLevel);
        List<DbJiangHuQualityRand> GetQualityRates(byte powerLevel);
        DbJiangHuCaltivateCondition GetCaltivateCondition(byte powerLevel);
        void StoreJiangHuRemainingTime(uint idUser, int seconds);
        int GetJiangHuRemainingTime(uint idUser);
        int GetCaltivateProgress(Character user);
        int GetCaltivateProgress(byte talent);
        int GetMaximumFreeCultivationValue();
        byte GetMaxTalentLevel();
        byte GetMaxFreeCourse();
        IJiangHu InitializeOSData(Character user);

        public enum JiangHuQuality : byte
        {
            None,
            Common,
            Sharp,
            Pure,
            Rare,
            Ultra,
            Epic
        }

        public enum JiangHuAttrType : byte
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
            MaxMana
        }

        public enum KongFuImproveFeedbackMode
        {
            FreeCourse,
            PaidCourse,
            FavouredTraining
        }
    }
}
