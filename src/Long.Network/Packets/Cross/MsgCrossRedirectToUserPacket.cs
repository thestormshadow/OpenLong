using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossRedirectToUserPacket<TActor>
        : MsgProtoBufBase<TActor, CrossRedirectToUserPacketPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossRedirectToUserPacket()
            : base(PacketType.MsgCrossRedirectToUserPacket)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossRedirectToUserPacketPB
    {
        [ProtoMember(1)]
        public uint UserId { get; set; }
        [ProtoMember(2)]
        public byte[] Packet { get; set; }
    }
}
