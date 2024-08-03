
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserMail<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletUserMail<TActor>.UserMailData> where TActor : TcpServerActor
	{
        public MsgPigletUserMail() 
            : base(PacketType.MsgPigletUserMail)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct UserMailData
        {
            [ProtoMember(1)]
            public uint UserId { get; set; }
            [ProtoMember(2)]
            public string SenderName { get; set; }
            [ProtoMember(3)]
            public string Subject { get; set; }
            [ProtoMember(4)]
            public string Content { get; set; }
            [ProtoMember(5)]
            public ulong Money { get; set; }
            [ProtoMember(6)]
            public uint ConquerPoints { get; set; }
            [ProtoMember(7)]
            public bool IsMono { get; set; }
            [ProtoMember(8)]
            public uint ItemId { get; set; }
            [ProtoMember(9)]
            public uint ItemType { get; set; }
            [ProtoMember(10)]
            public uint ActionId { get; set; }
            [ProtoMember(11)]
            public uint ExpirationSeconds { get; set; }
        }
    }
}
