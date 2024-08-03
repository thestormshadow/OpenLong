using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSlotResult : MsgBase<GameClient>
    {
        public SlotResultType Action { get; set; }
        public byte One { get; set; }
        public byte Two { get; set; }
        public byte Three { get; set; }
        public ulong Reward { get; set; }
        public uint Identity { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (SlotResultType)reader.ReadByte();
            One = reader.ReadByte();
            Two = reader.ReadByte();
            Three = reader.ReadByte();
            Reward = reader.ReadUInt64();
            Identity = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSlotResult);
            writer.Write((byte)Action);
            writer.Write(One);
            writer.Write(Two);
            writer.Write(Three);
            writer.Write(Reward);
            writer.Write(Identity);
            return writer.ToArray();
        }

        public enum SlotResultType : ushort
        {
            Start = 0,
            Stop = 1,
            Finish = 2
        }
    }
}
