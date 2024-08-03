using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginRequestUserId<TActor> 
        : MsgProtoBufBase<TActor, MsgLoginRequestUserId<TActor>.LoginRequestUserIdPB> where TActor : TcpServerActor
    {
        protected MsgLoginRequestUserId()
            : base(PacketType.MsgLoginRequestUserId)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LoginRequestUserIdPB
        {
            [ProtoMember(1)]
            public long AccountID { get; set; }
            [ProtoMember(2)]
            public string RequestID { get; set; }
            [ProtoMember(3)]
            public uint UserID { get; set; }
        }
    }
}
