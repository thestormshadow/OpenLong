
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletShutdown<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletShutdown<TActor>.ShutdownData>
        where TActor : TcpServerActor
	{
        public MsgPigletShutdown() 
            : base(PacketType.MsgPigletShutdown)
        {
            this.serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct ShutdownData
        {
            [ProtoMember(1)]
            public int Id { get; set; }
        }

    }
}
