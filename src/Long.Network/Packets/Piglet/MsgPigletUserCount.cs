
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserCount<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletUserCount<TActor>.UserCountData> where TActor : TcpServerActor
	{
        public MsgPigletUserCount() 
            : base(PacketType.MsgPigletUserCount)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct UserCountData
        {
            [ProtoMember(1)]
            public int Current { get; set; }
            [ProtoMember(2)]
            public int Max { get; set; }
        }
    }
}
