using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossRedirectToUserPacketS : MsgCrossRedirectToUserPacket<CrossServerActor>
    {
        public override Task ProcessAsync(CrossServerActor client)
        {
            Character user = RoleManager.GetUser(Data.UserId);
            if (user != null)
            {
                return user.SendAsync(Data.Packet);
            }
            return Task.CompletedTask;
        }
    }
}
