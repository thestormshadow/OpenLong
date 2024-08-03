using Long.Kernel.States.User;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Modules.Systems.Fate
{
    public interface IFate
    {
        int Attack { get; }
        int Breakthrough { get; }
        int Counteraction { get; }
        int CriticalStrike { get; }
        int FinalDamage { get; }
        int FinalDefense { get; }
        int FinalMagicDamage { get; }
        int FinalMagicDefense { get; }
        int HealthPoints { get; }
        int Immunity { get; }
        int MagicAttack { get; }
        int MagicDefense { get; }
        int SkillCriticalStrike { get; }

        Task<bool> AbandonAsync(FateType fateType);
        void AddAttribute(int value);
        Task<bool> ExtendAsync(FateType fateType);
        Task GenerateAsync(FateType type, TrainingSave save);
        int GetPower(TrainingAttrType attr);
        int GetRestorationCost(FateType fateType);
        int GetScore(FateType type);
        Task InitializeAsync();
        Task InitializeOSDataAsync(CrossTrainingVitalityInfoPB fateInfo);
        bool IsLocked(FateType type);
        bool IsValidProtection(FateType fateType);
        Task<bool> ProtectAsync(FateType fateType, bool update);
        void RefreshPower();
        Task<bool> RestoreProtectionAsync(FateType fateType);
        Task<bool> SaveAsync();
        Task SendAsync(bool update, Character target = null);
        Task SendExpiryInfoAsync();
        Task SendProtectInfoAsync();
        Task SubmitRankAsync();
        Task UnlockAsync(FateType type);
        Task TransferOSDataAsync(ulong sessionId, uint serverId);

        public enum FateType
        {
            None,
            Dragon = 1,
            Phoenix,
            Tiger,
            Turtle
        }

        [Flags]
        public enum TrainingSave
        {
            None = 0,
            Attr1 = 0x1,
            Attr2 = 0x2,
            Attr3 = 0x4,
            Attr4 = 0x8,
            All = Attr1 | Attr2 | Attr3 | Attr4
        }

        public enum TrainingAttrType
        {
            None = 0,
            Criticalstrike = 1,
            Skillcriticalstrike = 2,
            Immunity = 3,
            Breakthrough = 4,
            Counteraction = 5,
            Health = 6,
            Attack = 7,
            Magicattack = 8,
            Mdefense = 9,
            Finalattack = 10,
            Finalmagicattack = 11,
            Damagereduction = 12,
            Magicdamagereduction = 13
        }
    }
}
