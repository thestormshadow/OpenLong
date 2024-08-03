using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossRequestSwitch<TActor>
        : MsgProtoBufBase<TActor, RequestSwitchPB> 
        where TActor : TcpServerActor
    {
        protected MsgCrossRequestSwitch()
            : base(PacketType.MsgCrossRequestSwitch)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct RequestSwitchPB
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
        public ulong SessionId { get; set; }
    }
}
