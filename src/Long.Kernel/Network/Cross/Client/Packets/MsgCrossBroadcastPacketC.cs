using Long.Kernel.Managers;
using Long.Kernel.Settings;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossBroadcastPacketC : MsgCrossBroadcastPacket<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
        {
            if (GameServerSettings.IsRealm)
            {
                return BroadcastToServersAsync(this, Data.IgnoreServerId);
            }
            return RoleManager.BroadcastWorldMsgAsync(Data.Packet, Data.IgnoreUserId);
        }
    }
}
