using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginSubmitCrossInfo<TActor> 
        : MsgProtoBufBase<TActor, LoginSubmitCrossInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgLoginSubmitCrossInfo()
            : base(PacketType.MsgLoginSubmitCrossInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct LoginSubmitCrossInfoPB
    {
        [ProtoMember(1)]
        public int RealmId { get; set; }
        [ProtoMember(2)]
        public List<LoginSubmitCrossServerInfoPB> CrossServers { get; set; }
    }

    [ProtoContract]
    public struct LoginSubmitCrossServerInfoPB
    {
        [ProtoMember(1)]
        public int ServerId { get; set; }
        [ProtoMember(2)]
        public string ServerName { get; set; }
    }
}
