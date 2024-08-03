using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSameGroupServerList : MsgProtoBufBase<GameClient, MsgSameGroupServerList.MsgSameGroupServerListPB>
    {
        public MsgSameGroupServerList()
            : base(PacketType.MsgServerList)
        {
            
        }

        [ProtoContract]
        public struct MsgSameGroupServerListPB
        {
            [ProtoMember(1, IsRequired = true)]
            public ServerDetailPB[] Servers { get; set; }
        }

        [ProtoContract]
        public struct ServerDetailPB
        {
            [ProtoMember(1, IsRequired = true)]
            public uint ServerIdentity { get; set; }
            [ProtoMember(2, IsRequired = true)]
            public uint MapID { get; set; }
            [ProtoMember(3, IsRequired = true)]
            public uint X { get; set; }
            [ProtoMember(4, IsRequired = true)]
            public uint Y { get; set; }
            [ProtoMember(5, IsRequired = true)]
            public uint Attribute { get; set; }
            [ProtoMember(6, IsRequired = true)]
            public string Name { get; set; }
        }
    }
}
