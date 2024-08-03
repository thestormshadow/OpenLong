using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginRealmChildInfo<TActor>
        : MsgProtoBufBase<TActor, MsgLoginRealmChildInfo<TActor>.RealmChildInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgLoginRealmChildInfo()
            : base(PacketType.MsgLoginRealmChildInfo)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct RealmChildInfoPB
        {
            [ProtoMember(1)]
            public ChildServerInfoPB[] Infos { get; set; }
        }

        [ProtoContract]
        public struct ChildServerInfoPB
        {
            [ProtoMember(1)]
            public string ServerGUID { get; set; }
            [ProtoMember(2)]
            public int ServerID { get; set; }
            [ProtoMember(3)]
            public string ServerName { get; set; }
            [ProtoMember(4)]
            public string IpAddress { get; set; }
        }
    }
}
