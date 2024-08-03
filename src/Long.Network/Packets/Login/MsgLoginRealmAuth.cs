using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginRealmAuth<TActor>
        : MsgProtoBufBase<TActor, MsgLoginRealmAuth<TActor>.RealmAuthData> where TActor : TcpServerActor
    {
        public MsgLoginRealmAuth()
            : base(PacketType.MsgLoginRealmAuth)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct RealmAuthData
        {
            [ProtoMember(1)]
            public string RealmID { get; set; }
            [ProtoMember(2)]
            public string Username { get; set; }
            [ProtoMember(3)]
            public string Password { get; set; }
        }
    }
}