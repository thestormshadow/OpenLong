using Long.Kernel.States.User;

namespace Long.Kernel.States.Events
{
    public abstract class SyndicateGameEvent : GameEvent
    {
        protected SyndicateGameEvent(string name, int timeCheck = 1000)
            : base(name, timeCheck)
        {
        }

        public virtual Task OnEnterSyndicate(Character user, uint syndicateId)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnExitSyndicate(Character user, uint syndicateId)
        {
            return Task.CompletedTask;
        }

        public virtual Task SetMeedAsync(Character user, ulong money, uint emoney)
        {
            return Task.CompletedTask;
        }
    }
}
