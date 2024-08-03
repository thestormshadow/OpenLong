using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;

namespace Long.Kernel.States.MessageBoxes
{
    public abstract class MessageBox
    {
        protected TimeOut expiration = new();
        protected Character user;

        protected MessageBox(Character user)
        {
            this.user = user;
        }

        public virtual string Message { get; protected set; }

        public virtual int TimeOut { get; protected set; }

        public bool HasExpired => TimeOut > 0 && expiration.IsTimeOut();

        public virtual Task OnAcceptAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnCancelAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnTimerAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task SendAsync()
        {
            expiration.Startup(TimeOut);
            return user.SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
                Text = Message,
                OptionIndex = 255
            });
        }
    }
}
