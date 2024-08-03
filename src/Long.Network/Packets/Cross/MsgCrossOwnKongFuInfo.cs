using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossOwnKongFuInfo<TActor>
        : MsgProtoBufBase<TActor, CrossOwnKongFuListInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossOwnKongFuInfo()
            : base(PacketType.MsgCrossOwnKongFuInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossOwnKongFuListInfoPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public byte PowerLevel { get; set; }
        [ProtoMember(4)]
        public byte GenuineQiLevel { get; set; }
        [ProtoMember(5)]
        public uint FreeCaltivateParam { get; set; }
        [ProtoMember(6)]
        public uint TotalPowerValue { get; set; }
        [ProtoMember(7)]
        public List<CrossOwnKongFuInfoPB> List { get; set; }
    }

    [ProtoContract]
    public struct CrossOwnKongFuInfoPB
    {
        [ProtoMember(1)]
        public byte Level { get; set; }
        [ProtoMember(2)]
        public byte Type1 { get; set; }
        [ProtoMember(3)]
        public byte Quality1 { get; set; }
        [ProtoMember(4)]
        public byte Type2 { get; set; }
        [ProtoMember(5)]
        public byte Quality2 { get; set; }
        [ProtoMember(6)]
        public byte Type3 { get; set; }
        [ProtoMember(7)]
        public byte Quality3 { get; set; }
        [ProtoMember(8)]
        public byte Type4 { get; set; }
        [ProtoMember(9)]
        public byte Quality4 { get; set; }
        [ProtoMember(10)]
        public byte Type5 { get; set; }
        [ProtoMember(11)]
        public byte Quality5 { get; set; }
        [ProtoMember(12)]
        public byte Type6 { get; set; }
        [ProtoMember(13)]
        public byte Quality6 { get; set; }
        [ProtoMember(14)]
        public byte Type7 { get; set; }
        [ProtoMember(15)]
        public byte Quality7 { get; set; }
        [ProtoMember(16)]
        public byte Type8 { get; set; }
        [ProtoMember(17)]
        public byte Quality8 { get; set; }
        [ProtoMember(18)]
        public byte Type9 { get; set; }
        [ProtoMember(19)]
        public byte Quality9 { get; set; }
    }
}
