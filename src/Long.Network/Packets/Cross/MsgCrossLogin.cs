using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossLogin<TActor>
        : MsgProtoBufBase<TActor, MsgCrossLogin<TActor>.CrossLoginPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossLogin()
            : base(PacketType.MsgCrossLogin)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct CrossLoginPB
        {
            [ProtoMember(1)]
            public uint ServerIDX { get; set; }
            [ProtoMember(2)]
            public string ServerID { get; set; }
            [ProtoMember(3)]
            public string ServerName { get; set; }
            [ProtoMember(4)]
            public string Username { get; set; }
            [ProtoMember(5)]
            public string Password { get; set; }
        }
    }
}
