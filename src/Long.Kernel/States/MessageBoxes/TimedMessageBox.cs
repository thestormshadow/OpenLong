using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;

namespace Long.Kernel.States.MessageBoxes
{
    public sealed class TimedMessageBox : MessageBox
    {
        public TimedMessageBox(Character user, int seconds)
            : base(user)
        {
            TimeOut = seconds;
        }

        public int MessageId { get; set; }
        public int AcceptMsgId { get; set; }
        public int InviteId { get; set; }

        public uint TargetMapIdentity { get; set; } = 1002;
        public ushort[] TargetMapX { get; set; } = { 300 };
        public ushort[] TargetMapY { get; set; } = { 278 };

        public override async Task OnAcceptAsync()
        {
            if (HasExpired)
            {
                return;
            }

            if (user.Map.IsChgMapDisable() || user.Map.IsTeleportDisable() || user.Map.IsPrisionMap())
            {
                return;
            }

            if (user.Map.IsRecordDisable())
            {
                await user.SavePositionAsync(user.RecordMapIdentity, user.RecordMapX, user.RecordMapY);
            }
            else
            {
                await user.SavePositionAsync(user.MapIdentity, user.X, user.Y);
            }

            int idx = await NextAsync(TargetMapX.Length) % TargetMapX.Length;
            ushort x = TargetMapX[idx],
                   y = TargetMapY[idx];

            await user.FlyMapAsync(TargetMapIdentity, x, y);
        }

        public override Task OnTimerAsync()
        {
            return base.OnTimerAsync();
        }

        public override Task SendAsync()
        {
            expiration.Startup(TimeOut);
            return user.SendAsync(new MsgInviteTrans
            {
                Mode = MsgInviteTrans.Action.Pop,
                Message = MessageId,
                Priority = InviteId,
                Seconds = TimeOut
            });
        }
    }
}
