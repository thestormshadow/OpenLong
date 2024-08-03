
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserCreditInfoEx<TActor> : MsgProtoBufBase<TActor, MsgPigletUserCreditInfoEx<TActor>.FirstCreditData> where TActor : TcpServerActor
	{
        public MsgPigletUserCreditInfoEx()
            : base(PacketType.MsgPigletUserCreditInfoEx)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct FirstCreditData
        {
            [ProtoMember(1)]
            public uint UserIdentity { get; set; }
            [ProtoMember(2)]
            public bool HasFirstCreditToClaim { get; set; }
            [ProtoMember(3)]
            public decimal TotalCreditAmount { get; set; }
            [ProtoMember(4)]
            public int TotalPurchases { get; set; }
            [ProtoMember(5)]
            public int TotalPurchasesThisMonth { get; set; }
        }
    }
}
