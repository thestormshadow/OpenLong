using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Qualifying.States.UserQualifier;

namespace Long.Module.Qualifying
{
    public sealed class EventTimer : IEventTimer
    {
        public Task OnEventTimerAsync()
        {
            ArenaQualifier Qualifier = EventManager.GetEvent<ArenaQualifier>();
            return Qualifier.OnTimerAsync();
        }
    }
}
