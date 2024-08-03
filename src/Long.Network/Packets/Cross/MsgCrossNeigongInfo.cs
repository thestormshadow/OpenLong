using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossNeigongInfo<TActor>
        : MsgProtoBufBase<TActor, CrossNeigongInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossNeigongInfo()
            : base(PacketType.MsgCrossNeigongInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossNeigongInfoPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public List<byte> Secrets { get; set; }
        [ProtoMember(3)]
        public List<CrossNeigongPowerInfoPB> Powers { get; set; }
    }

    [ProtoContract]
    public struct CrossNeigongPowerInfoPB
    {
        [ProtoMember(1)]
        public ushort Type { get; set; }
        [ProtoMember(2)]
        public byte Level { get; set; }
        [ProtoMember(3)]
        public byte Value { get; set; }
        [ProtoMember(4)]
        public byte FinishValue { get; set; }
        [ProtoMember(5)]
        public byte Status { get; set; }
        [ProtoMember(6)]
        public byte AbolishNum { get; set; }
        [ProtoMember(7)]
        public uint MaxLife { get; set; }
        [ProtoMember(8)]
        public uint PhysicAttackNew { get; set; }
        [ProtoMember(9)]
        public uint MagicAttack { get; set; }
        [ProtoMember(10)]
        public uint PhysicDefenseNew { get; set; }
        [ProtoMember(11)]
        public uint MagicDefense { get; set; }
        [ProtoMember(12)]
        public ushort FinalPhysicAdd { get; set; }
        [ProtoMember(13)]
        public ushort FinalMagicAdd { get; set; }
        [ProtoMember(14)]
        public ushort FinalPhysicReduce { get; set; }
        [ProtoMember(15)]
        public ushort FinalMagicReduce { get; set; }
        [ProtoMember(16)]
        public ushort PhysicCrit { get; set; }
        [ProtoMember(17)]
        public ushort MagicCrit { get; set; }
        [ProtoMember(18)]
        public ushort DefenseCrit { get; set; }
        [ProtoMember(19)]
        public ushort SmashRate { get; set; }
        [ProtoMember(20)]
        public ushort FirmDefenseRate { get; set; }
    }
}
