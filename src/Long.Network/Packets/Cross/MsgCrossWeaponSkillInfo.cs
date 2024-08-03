using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossWeaponSkillInfo<TActor>
        : MsgProtoBufBase<TActor, CrossWeaponSkillInfoListPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossWeaponSkillInfo()
            : base(PacketType.MsgCrossWeaponSkillInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossWeaponSkillInfoListPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public List<CrossWeaponSkillinfoPB> Infos { get; set; }
    }

    [ProtoContract]
    public struct CrossWeaponSkillinfoPB
    {
        [ProtoMember(1)]
        public uint Type { get; set; }
        [ProtoMember(2)]
        public byte Level { get; set; }
        [ProtoMember(3)]
        public uint Experience { get; set; }
    }
}
