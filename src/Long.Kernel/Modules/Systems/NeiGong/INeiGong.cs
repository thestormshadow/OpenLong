using Long.Kernel.States.User;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Modules.Systems.NeiGong
{
    public interface INeiGong
    {
        int Attack { get; }
        int Breakthrough { get; }
        int Counteraction { get; }
        int CriticalStrike { get; }
        int Defense { get; }
        int FinalMagicalDamage { get; }
        int FinalMagicalDefense { get; }
        int FinalPhysicalDamage { get; }
        int FinalPhysicalDefense { get; }
        int Immunity { get; }
        int MagicAttack { get; }
        int MagicDefense { get; }
        int MaxLife { get; }
        int SkillCriticalStrike { get; }
        int TotalValue { get; }

        int GetInnerStrengthLevelByType(int idType);
        bool HasLearnedStrengthType(int idType);
        Task<bool> InitializeAsync();
        Task InitializeOSDataAsync(CrossNeigongInfoPB info);
        Task<bool> PerfectAsync(byte type);
        Task<bool> ReshapeAsync(byte type);
        Task SendAsync(Character target = null);
        Task SendFullAsync();
        Task SendInfoAsync(byte secretType, Character target = null);
        Task<bool> UnlockAsync(byte type);
        Task<bool> UpLevelAsync(byte type, byte mode);
        Task TransferOSDataAsync(ulong sessionId, uint serverId);
    }
}
