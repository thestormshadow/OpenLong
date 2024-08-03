using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossRedirectUserPacket<TActor>
        : MsgProtoBufBase<TActor, CrossRedirectUserPacketPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossRedirectUserPacket()
            : base(PacketType.MsgCrossRedirectUserPacket)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossRedirectUserPacketPB
    {
        [ProtoMember(1)]
        public uint UserID { get; set; }
        [ProtoMember(2)]
        public byte[] Packet { get; set; }
    }
}
