using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public class MsgFlower : MsgBase<GameClient>
    {
        public RequestMode Mode { get; set; }
        public uint Identity { get; set; }
        public uint ItemIdentity { get; set; }
        public uint FlowerIdentity { get; set; }
        public string SenderName { get; set; } = "";
        public uint Amount { get; set; }
        public FlowerType Flower { get; set; }
        public string ReceiverName { get; set; } = "";
        public uint SendAmount { get; set; }
        public FlowerType SendFlowerType { get; set; }
        public FlowerEffect SendFlowerEffect { get; set; }

        public uint RedRoses { get; set; }
        public uint RedRosesToday { get; set; }
        public uint WhiteRoses { get; set; }
        public uint WhiteRosesToday { get; set; }
        public uint Orchids { get; set; }
        public uint OrchidsToday { get; set; }
        public uint Tulips { get; set; }
        public uint TulipsToday { get; set; }

        public List<string> Strings { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();              // 0
            Type = (PacketType)reader.ReadUInt16();   // 2
            Mode = (RequestMode)reader.ReadUInt32();  // 4
            Identity = reader.ReadUInt32();            // 8
            ItemIdentity = reader.ReadUInt32();        // 12
            FlowerIdentity = reader.ReadUInt32();      // 16
            Amount = reader.ReadUInt32();              // 20
            Flower = (FlowerType)reader.ReadUInt32(); // 24
            Strings = reader.ReadStrings();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgFlower);
            writer.Write((uint)Mode);
            writer.Write(Identity);
            writer.Write(ItemIdentity);
            if (Mode == RequestMode.QueryFlower
                || Mode == RequestMode.QueryGift)
            {
                writer.Write(RedRoses);
                writer.Write(RedRosesToday);
                writer.Write(WhiteRoses);
                writer.Write(WhiteRosesToday);
                writer.Write(Orchids);
                writer.Write(OrchidsToday);
                writer.Write(Tulips);
                writer.Write(TulipsToday);
            }
            else
            {
                writer.Write(SenderName, 16);
                writer.Write(ReceiverName, 16);
            }

            writer.Write(SendAmount);
            writer.Write((uint)SendFlowerType);
            writer.Write((uint)SendFlowerEffect);
            return writer.ToArray();
        }

        public enum FlowerEffect : uint
        {
            None = 0,

            RedRose,
            WhiteRose,
            Orchid,
            Tulip,

            Kiss = RedRose,
            Love = WhiteRose,
            Tins = Orchid,
            Jade = Tulip
        }

        public enum FlowerType
        {
            RedRose,
            WhiteRose,
            Orchid,
            Tulip,

            Kiss,
            Love,
            Tins,
            Jade
        }

        public enum RequestMode
        {
            SendFlower,
            SendGift,
            QueryFlower,
            QueryGift
        }
    }
}
