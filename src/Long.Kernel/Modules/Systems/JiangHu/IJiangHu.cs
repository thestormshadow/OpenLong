using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;
using System.Runtime.InteropServices;
using static Long.Kernel.Modules.Systems.JiangHu.IJiangHuManager;

namespace Long.Kernel.Modules.Systems.JiangHu
{
    public interface IJiangHu
    {
        int Attack { get; }
        int Breakthrough { get; }
        int Counteraction { get; }
        int CriticalStrike { get; }
        int CurrentStage { get; }
        int Defense { get; }
        int FinalDamage { get; }
        int FinalDefense { get; }
        int FinalMagicDamage { get; }
        int FinalMagicDefense { get; }
        uint FreeCaltivateParam { get; set; }
        uint FreeCourses { get; }
        byte FreeCoursesUsedToday { get; set; }
        int Grade { get; }
        bool HasJiangHu { get; }
        int Immunity { get; }
        uint InnerPower { get; set; }
        bool IsActive { get; }
        int MagicAttack { get; }
        int MagicDefense { get; }
        uint MaxInnerPowerHistory { get; set; }
        int MaxLife { get; }
        int MaxMana { get; }
        string Name { get; }
        uint PaidCoursesUsedToday { get; set; }
        int SecondsTillNextCourse { get; }
        int SkillCriticalStrike { get; }
        byte Talent { get; set; }

        void AddAttribute(JiangHuAttrType type, int value);
        Task AwardTalentAsync(byte talent = 1);
        Task<bool> CreateAsync(string gongFuName);
        Task DailyClearAsync();
        Task ExitJiangHuAsync();
        Task GenerateAsync(byte powerLevel, byte star, int high, KongFuImproveFeedbackMode mode);
        byte GetLatestStar();
        int GetStageInnerPower(byte powerLevel);
        Task InitializationNotifyAsync();
        Task InitializeAsync();
        Task InitializeOSDataAsync(CrossOwnKongFuListInfoPB info);
        Task LogoutAsync();
        Task OnTimerAsync();
        bool QueryStar(byte level, byte star, out JiangHuStar jiangHuStar);
        Task<bool> RestoreAsync(byte powerLevel, byte star);
        Task SaveAsync();
        Task SendInfoAsync(Character target = null);
        Task SendStarAsync(Character target = null);
        Task SendStarsAsync(Character target = null);
        Task SendStatusAsync();
        Task SendTalentAsync(Character target = null);
        Task SendTimeAsync(Character target = null);
        DbJiangHuPlayerPower SetAttribute(byte level, byte star, JiangHuQuality quality, JiangHuAttrType type);
        Task<bool> SpendTalentAsync(byte talent = 1);
        Task StudyAsync(byte powerLevel, byte star, byte high, KongFuImproveFeedbackMode mode);
        Task SubmitOSDataAsync(ulong sessionId, uint serverId);
        void UpdateAllAttributes();
        Task UpdateAsync();
        void UpdateAttributes(byte level, out int grade, out JiangHuQuality qualityAlign);

        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 4)]
        public struct JiangHuStar
        {
            public JiangHuStar(
                JiangHuQuality quality,
                JiangHuAttrType type,
                byte stage,
                byte star
                )
            {
                Quality = quality;
                Type = type;
                PowerLevel = stage;
                Star = star;
            }

            public JiangHuQuality Quality { get; init; }
            public JiangHuAttrType Type { get; init; }
            public byte PowerLevel { get; init; }
            public byte Star { get; init; }
        }
    }
}
