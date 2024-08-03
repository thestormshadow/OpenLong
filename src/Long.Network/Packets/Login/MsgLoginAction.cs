using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginAction<TActor>
        : MsgProtoBufBase<TActor, MsgLoginAction<TActor>.LoginActionPB>
        where TActor : TcpServerActor
    {
        public enum LoginActionEnum
        {
            None,
            First = 15000,
            RequestMasterRealm,
            RequestChildServers,
            RequestCredentials,
            RequestRealmData,
            Ping
        }

        protected MsgLoginAction()
            : base(PacketType.MsgLoginAction)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LoginActionPB
        {
            [ProtoMember(1)]
            public uint ServerID { get; set; }
            [ProtoMember(2)]
            public string ServerUUID { get; set; }
            [ProtoMember(3)]
            public uint Action { get; set; }
            [ProtoMember(4)]
            public uint Data { get; set; }
            [ProtoMember(5)]
            public uint Command { get; set; }
            [ProtoMember(6)]
            public uint Param { get; set; }
            [ProtoMember(7)]
            public ulong Data64 { get; set; }
            [ProtoMember(8)]
            public List<string> Strings { get; set; }
        }
    }
}
