
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletLogOutputRedirect<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletLogOutputRedirect<TActor>.LogData> where TActor : TcpServerActor
	{
        public MsgPigletLogOutputRedirect()
            : base(PacketType.MsgPigletLogOutputRedirect)
        {            
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LogData
        {
            [ProtoMember(1)]
            public string LogLevel { get; set; }
            [ProtoMember(2)]
            public string System { get; set; }
            [ProtoMember(3)]
            public string Message { get; set; }
        }
    }
}
