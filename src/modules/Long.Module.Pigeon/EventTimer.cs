using Long.Kernel.Modules.Interfaces;
using Long.Module.Pigeon.Managers;

namespace Long.Module.Pigeon
{
    public sealed class EventTimer : IEventTimer
    {
        public Task OnEventTimerAsync()
        {
            return PigeonManager.OnTimerAsync();
        }
    }
}
