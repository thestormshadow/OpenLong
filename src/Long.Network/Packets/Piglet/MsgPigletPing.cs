
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletPing<TActor>
       : MsgProtoBufBase<TActor, MsgPigletPing<TActor>.PingData> where TActor : TcpServerActor
	{
        public MsgPigletPing()
            : base(PacketType.MsgPigletPing)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct PingData
        {
            [ProtoMember(1, IsRequired = true)]
            public long TickCount { get; set; }
        }
    }
}
