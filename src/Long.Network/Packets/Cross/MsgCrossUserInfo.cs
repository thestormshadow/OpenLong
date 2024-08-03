using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossUserInfo<TActor>
        : MsgProtoBufBase<TActor, CrossUserInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossUserInfo()
            : base(PacketType.MsgCrossUserInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossUserInfoPB
    {
        [ProtoMember(1)]
        public uint UserID { get; set; }
        [ProtoMember(2)]
        public string UserName { get; set; }
        [ProtoMember(3)]
        public string Mate { get; set; }
        [ProtoMember(4)]
        public byte Level { get; set; }
        [ProtoMember(5)]
        public ulong Experience { get; set; }
        [ProtoMember(6)]
        public ushort Force { get; set; }
        [ProtoMember(7)]
        public ushort Speed { get; set; }
        [ProtoMember(8)]
        public ushort Health { get; set; }
        [ProtoMember(9)]
        public ushort Spirit { get; set; }
        [ProtoMember(10)]
        public ushort AttributePoints { get; set; }
        [ProtoMember(11)]
        public ushort Hair { get; set; }
        [ProtoMember(12)]
        public uint Lookface { get; set; }
        [ProtoMember(13)]
        public ulong Money { get; set; }
        [ProtoMember(14)]
        public uint ConquerPoints { get; set; }
        [ProtoMember(15)]
        public uint PresentConquerPoints { get; set; }
        [ProtoMember(16)]
        public byte Metempsychosis { get; set; }
        [ProtoMember(17)]
        public ushort Profession { get; set; }
        [ProtoMember(18)]
        public ushort FirstProfession { get; set; }
        [ProtoMember(19)]
        public ushort LastProfession { get; set; }
        [ProtoMember(20)]
        public uint FlowerCharm { get; set; }
        [ProtoMember(21)]
        public byte VipLevel { get; set; }
        [ProtoMember(22)]
        public ulong SessionId { get; set; }
        [ProtoMember(23)]
        public byte ShowType { get; set; }
        [ProtoMember(24)]
        public uint RealmUserId { get; set; }
        [ProtoMember(25)]
        public byte NobilityRank { get; set; }
        [ProtoMember(26)]
        public uint SyndicateId { get; set; }
        [ProtoMember(27)]
        public uint SyndicateRank { get; set; }
        [ProtoMember(28)]
        public string SyndicateName { get; set; }
        [ProtoMember(29)]
        public uint FamilyId { get; set; }
        [ProtoMember(30)]
        public uint FamilyRank { get; set; }
        [ProtoMember(31)]
        public string FamilyName { get; set; }
        [ProtoMember(32)]
        public uint UnionId { get; set; }
        [ProtoMember(33)]
        public uint UnionOfficialFlag { get; set; }
        [ProtoMember(34)]
        public uint UnionContributionLevel { get; set; }
        [ProtoMember(35)]
        public bool IsKingdom { get; set; }
        [ProtoMember(36)]
        public string UnionName { get; set; }
        [ProtoMember(37)]
        public uint WingId { get; set; }
        [ProtoMember(38)]
        public uint TitleId { get; set; }
        [ProtoMember(39)]
        public uint TitleScore { get; set; }
    }
}
