using Long.Database.Entities;
using Long.Kernel.States.Magics;

namespace Long.Kernel.States.Status
{
    public interface IStatus
    {
        int Identity { get; }
        bool IsValid { get; }
        int Power { get; set; }
        int Time { get; }
        int RemainingTimes { get; }
        int RemainingTime { get; }
        Magic Magic { get; }
        DbStatus Model { get; set; }
        byte Level => Magic?.Level ?? 0;
        uint CasterId { get; }
        bool IsUserCast { get; }
        bool GetInfo(ref StatusInfoStruct info);
        Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0);
        bool IncTime(int ms, int limit);
        bool ToFlash();
        Task OnTimerAsync();
    }
}
