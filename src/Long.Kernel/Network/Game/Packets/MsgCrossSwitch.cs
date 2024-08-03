using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgCrossSwitch : MsgBase<GameClient>
    {
        public enum CrossAction
        {
            Enter,
            Exit
        }

        public CrossAction Action { get; set; } // 4
        public uint PlayerID { get; set; } // 8
        public uint NewUserId { get; set; } // 12
        public int Count { get; set; } // 16
        public List<CrossItemInfo> ItemInfos { get; set; } = new();

        public struct CrossItemInfo
        {
            public uint OriginItemID { get; set; }
            public uint TargetItemID { get; set; }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgCrossSwitch);
            writer.Write((uint)Action);
            writer.Write(PlayerID);
            writer.Write(NewUserId);
            writer.Write(Count = ItemInfos.Count);
            foreach (var item in ItemInfos)
            {
                writer.Write(item.OriginItemID);
                writer.Write(item.TargetItemID);
            }
            return writer.ToArray();
        }
    }
}
