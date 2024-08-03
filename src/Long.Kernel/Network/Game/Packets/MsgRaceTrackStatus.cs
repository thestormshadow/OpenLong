using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgRaceTrackStatus : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public int Count { get; set; }
        public List<PropEffects> Effects { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgRaceTrackStatus);
            writer.Write(Identity); // 4 0x4
            writer.Write(Count = Effects.Count); // 8 0x8
            foreach (var e in Effects)
            {
                writer.Write((int)e.Attribute); // 12 0xC
                writer.Write(e.Active); // 16 0x10
                writer.Write(e.Display); // 20 0x14
                writer.Write(e.Time); // 24 0x18
                writer.Write(e.Amount); // 28 0x1C
            }
            return writer.ToArray();
        }

        public struct PropEffects
        {
            public AttrUpdateType Attribute { get; set; }
            public int Active { get; set; }
            public int Display { get; set; }
            public int Time { get; set; }
            public int Amount { get; set; }
        }
    }
}
