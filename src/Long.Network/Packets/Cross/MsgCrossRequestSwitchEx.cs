using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossRequestSwitchEx<TActor>
        : MsgProtoBufBase<TActor, CrossRequestSwitchExPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossRequestSwitchEx()
            : base(PacketType.MsgCrossRequestSwitchEx)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossRequestSwitchExPB
    {
        [ProtoMember(1)]
        public uint UserId { get; set; }
        [ProtoMember(2)]
        public uint AccountId { get; set; }
        [ProtoMember(3)]
        public uint ServerId { get; set; }
        [ProtoMember(4)]
        public uint NewUserId { get; set; }
        [ProtoMember(5)]
        public uint TargetMapID { get; set; }
        [ProtoMember(6)]
        public ushort TargetX { get; set; }
        [ProtoMember(7)]
        public ushort TargetY { get; set; }
        [ProtoMember(8)]
        public ulong SessionID { get; set; }
    }
}
