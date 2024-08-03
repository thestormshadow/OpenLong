using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgOverheadLeagueInfo : MsgProtoBufBase<GameClient, MsgOverheadLeagueInfo.MsgUnionRank>
    {
        public const byte IS_UNION_FLAG = 0x1,
            IS_KINGDOM_FLAG = 0x4;

        public MsgOverheadLeagueInfo() 
            : base(PacketType.MsgOverheadLeagueInfo)
        {
        }

        [ProtoContract]
        public struct MsgUnionRank
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Action { get; set; } // 4
            [ProtoMember(2, IsRequired = true)]
            public uint Target { get; set; } // 8
            [ProtoMember(3, IsRequired = true)]
            public uint IdLeague { get; set; } // 12
            [ProtoMember(4, IsRequired = true)]
            public byte IsLeagueLeader { get; set; } // 16
            [ProtoMember(5, IsRequired = true)]
            public uint IsCountryLeagueMember { get; set; } // 20
            [ProtoMember(6, IsRequired = true)]
            public uint IdServer { get; set; } // 24
            [ProtoMember(7, IsRequired = true)]
            public string Name { get; set; } // 28
        }
    }
}
