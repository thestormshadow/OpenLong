using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossRedirectToUserPacketC : MsgCrossRedirectToUserPacket<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
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
