using Long.Kernel.Modules.Interfaces;
using Long.Module.Competion.States;

namespace Long.Module.Competion
{
    public sealed class EventTimer : IEventTimer
    {
        public Task OnEventTimerAsync()
        {
            return QuizShowManager.OnTimerAsync();
        }
    }
}
