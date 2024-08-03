
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletItemSuspicious<TActor>
        : MsgProtoBufBase<TActor, MsgPigletItemSuspicious<TActor>.ItemSuspiciousData> where TActor : TcpServerActor
	{
        public MsgPigletItemSuspicious() 
            : base(PacketType.MsgPigletItemSuspicious)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct ItemSuspiciousData
        {
            [ProtoMember(1)]
            public uint UserId { get; set; }
            [ProtoMember(2)]
            public uint ItemId { get; set; }
            [ProtoMember(3)]
            public bool Suspicious { get; set; }
        }
    }
}
