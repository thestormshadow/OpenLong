using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossMagicInfo<TActor>
        : MsgProtoBufBase<TActor, CrossMagicListInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossMagicInfo()
            : base(PacketType.MsgCrossMagicInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossMagicListInfoPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public List<CrossMagicInfoPB> MagicList { get; set; }
    }

    [ProtoContract]
    public struct CrossMagicInfoPB
    {
        [ProtoMember(1)]
        public ushort Type { get; set; }
        [ProtoMember(2)]
        public ushort Level { get; set; }
        [ProtoMember(3)]
        public byte CurrentEffectType { get; set; }
        [ProtoMember(4)]
        public uint AvailableEffectType { get; set; }
        [ProtoMember(5)]
        public uint EffectMonopoly { get; set; }
        [ProtoMember(6)]
        public uint EffectExorbitant { get; set; }
    }
}
