
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserMassMail<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletUserMassMail<TActor>.UserMassMailStruct> where TActor : TcpServerActor
	{
        public MsgPigletUserMassMail() 
            : base(PacketType.MsgPigletUserMassMail)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct UserMassMailStruct
        {
            [ProtoMember(1)]
            public uint[] Users { get; set; }
            [ProtoMember(2)]
            public MailStructure Mail { get; set; }
        }

        [ProtoContract]
        public struct MailStructure
        {
            [ProtoMember(1)]
            public string SenderName { get; set; }
            [ProtoMember(2)]
            public string Subject { get; set; }
            [ProtoMember(3)]
            public string Content { get; set; }
            [ProtoMember(4)]
            public ulong Money { get; set; }
            [ProtoMember(5)]
            public uint ConquerPoints { get; set; }
            [ProtoMember(6)]
            public bool IsMono { get; set; }
            [ProtoMember(7)]
            public uint ItemId { get; set; }
            [ProtoMember(8)]
            public uint ItemType { get; set; }
            [ProtoMember(9)]
            public uint ActionId { get; set; }
            [ProtoMember(10)]
            public uint ExpirationSeconds { get; set; }
        }
    }
}
