
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserBan<TActor>
        : MsgProtoBufBase<TActor, MsgPigletUserBan<TActor>.UserBanData> where TActor : TcpServerActor
	{
        public MsgPigletUserBan()
            : base(PacketType.MsgPigletUserBan)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct UserBanData
        {
            [ProtoMember(1)]
            public uint UserId { get; set; }
            [ProtoMember(2)]
            public string GameMaster { get; set; }
            [ProtoMember(3)]
            public string Reason { get; set; }
        }
    }
}
