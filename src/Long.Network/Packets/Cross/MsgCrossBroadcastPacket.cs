using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossBroadcastPacket<TActor>
        : MsgProtoBufBase<TActor, CrossBroadcastPacketPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossBroadcastPacket()
            : base(PacketType.MsgCrossBroadcastPacket)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossBroadcastPacketPB
    {
        [ProtoMember(1)]
        public uint IgnoreUserId { get; set; }
        [ProtoMember(2)]
        public uint IgnoreServerId { get; set; }
        [ProtoMember(3)]
        public byte[] Packet { get; set; }
    }
}
