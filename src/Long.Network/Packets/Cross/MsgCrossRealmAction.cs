using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossRealmAction<TActor>
        : MsgProtoBufBase<TActor, CrossRealmActionPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossRealmAction()
            : base(PacketType.MsgCrossRealmAction)
        {
            serializeWithHeaders = true;
        }
    }

    public enum CrossRealmAction
    {
        None,
        Ping,
        KickoutPlayer,
        ReturnPlayer,
        TransferServer,
    }

    [ProtoContract]
    public struct CrossRealmActionPB
    {
        [ProtoMember(1)]
        public uint ServerID { get; set; }
        [ProtoMember(2)]
        public uint Action { get; set; }
        [ProtoMember(3)]
        public uint Data { get; set; }
        [ProtoMember(4)]
        public uint Command { get; set; }
        [ProtoMember(5)]
        public uint Param { get; set; }
        [ProtoMember(6)]
        public ulong Data64 { get; set; }
        [ProtoMember(7)]
        public List<string> Strings { get; set; }
    }
}
