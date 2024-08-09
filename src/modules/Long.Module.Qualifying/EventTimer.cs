using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Qualifying.Network.States;
using Long.Module.Qualifying.States;

namespace Long.Module.Qualifying
{
    public sealed class EventTimer : IEventTimer
    {
        public async Task OnEventTimerAsync()
        {
			TeamArenaQualifier TeamQualifier = EventManager.GetEvent<TeamArenaQualifier>();
			await TeamQualifier.OnTimerAsync();
			ArenaQualifier Qualifier = EventManager.GetEvent<ArenaQualifier>();
            await Qualifier.OnTimerAsync();
        }
    }
}
