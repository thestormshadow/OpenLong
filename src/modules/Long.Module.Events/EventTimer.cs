using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Events.Managers;
using Long.Module.Events.States;

namespace Long.Module.Events
{
    public sealed class EventTimer : IEventTimer
    {
        public Task OnEventTimerAsync()
        {
			CaptureTheFlag ctf = EventManager.GetEvent<CaptureTheFlag>();			
			return ctf.OnTimerAsync();
		}
    }
}
