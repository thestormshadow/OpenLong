using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossLoginEx<TActor>
        : MsgProtoBufBase<TActor, MsgCrossLoginEx<TActor>.CrossLoginExPB>
        where TActor : TcpServerActor
    {
        public const int SUCCESS_RESPONSE = 0x13f5a5;

        protected MsgCrossLoginEx()
            : base(PacketType.MsgCrossLoginEx)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct CrossLoginExPB
        {
            [ProtoMember(1)]
            public uint ServerID { get; set; }
            [ProtoMember(2)]
            public uint Response { get; set; }
        }
    }
}
