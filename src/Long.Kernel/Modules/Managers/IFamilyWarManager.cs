using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Kernel.States;

namespace Long.Kernel.Modules.Managers
{
    public interface IFamilyWarManager
    {
        bool IsInTime { get; }

        DynamicNpc GetChallengeNpc(IFamily family);
        IList<IFamily> GetChallengersByNpc(uint idNpc);
        DynamicNpc GetDominatingNpc(IFamily family);
        int GetFamilyOccupyDays(uint idFamily);
        int GetFamilyOccupyDaysByNpc(uint idNpc);
        IFamily GetFamilyOwner(uint idNpc);
        uint GetGoldFee(uint idNpc);
        GameMap GetMapByNpc(uint idNpc);
        double GetNextExpReward(Character user);
        uint GetNextReward(uint idNpc);
        uint GetNextWeekReward(uint idNpc);
        bool HasExpToClaim(Character user);
        bool HasRewardToClaim(Character user);
        Task<bool> InitializeAsync();
        bool IsAllowedToJoin(Role sender);
        bool IsNpcChallenged(uint idNpc);
        Task OnTimerAsync();
        Task SetExpRewardAwardedAsync(Character user);
        Task SetRewardAwardedAsync(Character user);
        Task<bool> ValidateResultAsync(Character user, uint idNpc);
    }
}
