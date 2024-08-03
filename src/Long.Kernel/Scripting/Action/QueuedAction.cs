using Long.Kernel.Managers;

namespace Long.Kernel.Scripting.Action
{
    public class QueuedAction
    {
        private readonly TimeOut timeOut = new();

        public QueuedAction(int secs, uint action, uint idUser)
        {
            timeOut.Startup(secs);
            Action = action;
            UserIdentity = idUser;
        }

        public uint UserIdentity { get; }
        public uint Action { get; }
        public bool CanBeExecuted => timeOut.IsActive() && timeOut.IsTimeOut();
        public bool IsValid => UserIdentity == 0 || RoleManager.GetUser(UserIdentity) != null;

        public void Clear()
        {
            timeOut.Clear();
        }
    }
}
