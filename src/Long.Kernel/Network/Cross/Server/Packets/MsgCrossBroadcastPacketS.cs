using Long.Kernel.Managers;
using Long.Kernel.Settings;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossBroadcastPacketS : MsgCrossBroadcastPacket<CrossServerActor>
    {
        public override Task ProcessAsync(CrossServerActor client)
        {
            if (GameServerSettings.IsRealm)
            {
                return BroadcastToServersAsync(this, Data.IgnoreServerId);
            }
            return RoleManager.BroadcastWorldMsgAsync(Data.Packet, Data.IgnoreUserId);
        }
    }
}
