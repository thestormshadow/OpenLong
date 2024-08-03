using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossItemInfo<TActor>
        : MsgProtoBufBase<TActor, CrossItemInfoListPB>
        where TActor : TcpServerActor
    {
        public const int MAX_PER_MSG = 15;

        protected MsgCrossItemInfo()
            : base(PacketType.MsgCrossItemInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossItemInfoListPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public List<CrossItemInfoPB> Items { get; set; }
    }

    [ProtoContract]
    public struct CrossItemInfoPB
    {
        [ProtoMember(1)]
        public uint Id { get; set; }
        [ProtoMember(2)]
        public uint Type { get; set; }
        [ProtoMember(3)]
        public ushort Amount { get; set; }
        [ProtoMember(4)]
        public ushort AmountLimit { get; set; }
        [ProtoMember(5)]
        public byte Position { get; set; }
        [ProtoMember(6)]
        public byte Gem1 { get; set; }
        [ProtoMember(7)]
        public byte Gem2 { get; set; }
        [ProtoMember(8)]
        public ushort Magic1 { get; set; }
        [ProtoMember(9)]
        public byte Magic2 { get; set; }
        [ProtoMember(10)]
        public byte Magic3 { get; set; }
        [ProtoMember(11)]
        public uint Data { get; set; }
        [ProtoMember(12)]
        public byte ReduceDmg { get; set; }
        [ProtoMember(13)]
        public byte AddLife { get; set; }
        [ProtoMember(14)]
        public uint ChkSum { get; set; }
        [ProtoMember(15)]
        public ushort Plunder { get; set; }
        [ProtoMember(16)]
        public uint Specialflag { get; set; }
        [ProtoMember(17)]
        public uint Color { get; set; }
        [ProtoMember(18)]
        public uint AddlevelExp { get; set; }
        [ProtoMember(19)]
        public byte Monopoly { get; set; }
        [ProtoMember(20)]
        public uint Syndicate { get; set; }
        [ProtoMember(21)]
        public int DeleteTime { get; set; }
        [ProtoMember(22)]
        public uint SaveTime { get; set; }
        [ProtoMember(23)]
        public uint AccumulateNum { get; set; }
        [ProtoMember(24)]
        public CrossItemStatusInfoPB? Artifact { get; set; }
        [ProtoMember(25)]
        public CrossItemStatusInfoPB? Refinery { get; set; }
    }

    [ProtoContract]
    public struct CrossItemStatusInfoPB
    {
        [ProtoMember(1)]
        public uint Id { get; set; }
        [ProtoMember(2)]
        public uint ItemId { get; set; }
        [ProtoMember(3)]
        public uint Status { get; set; }
        [ProtoMember(4)]
        public uint Level { get; set; }
        [ProtoMember(5)]
        public uint Power1 { get; set; }
        [ProtoMember(6)]
        public uint Power2 { get; set; }
        [ProtoMember(7)]
        public uint RealSeconds { get; set; }
        [ProtoMember(8)]
        public uint Data { get; set; }
        [ProtoMember(9)]
        public byte Position { get; set; }
    }
}
