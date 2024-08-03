
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletLoginEx<TActor>
        : MsgProtoBufBase<TActor, MsgPigletLoginEx<TActor>.LoginExData> where TActor : TcpServerActor
	{
        public MsgPigletLoginEx() 
            : base(PacketType.MsgPigletLoginEx)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LoginExData
        {
            [ProtoMember(1)]
            public int Result { get; set; }
        }
    }
}
