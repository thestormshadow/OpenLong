using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueOrderStatus : MsgProtoBufBase<GameClient, MsgLeagueOrderStatus.GroupToken>
    {
        [ProtoContract]
        public struct GroupToken
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Count { get; set; }
            [ProtoMember(2, IsRequired = true)]
            public Token[] Tokens { get; set; }
        }

        [ProtoContract]
        public struct Token
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Type { get; set; }
            [ProtoMember(2, IsRequired = true)]
            public uint UsageCountToday { get; set; }
            [ProtoMember(3, IsRequired = true)]
            public ulong LastTimeUsed { get; set; }
        }

        public MsgLeagueOrderStatus()
            : base(PacketType.MsgLeagueOrderStatus)
        {
        }
    }
}
