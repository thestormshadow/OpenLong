using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public class MsgSuitStatus : MsgBase<GameClient>
    {
        public const int RedRose = 30000002;
        public const int WhiteRose = 30000102;
        public const int Orchid = 30000202;
        public const int Tulip = 30000302;

        public int Action { get; set; }
        public int Unknown { get; set; }
        public int Data { get; set; }
        public int Param { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = reader.ReadInt32();
            Unknown = reader.ReadInt32();
            Data = reader.ReadInt32();
            Param = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSuitStatus);
            writer.Write(Action); // 4
            writer.Write(Unknown); // 8
            writer.Write(Data); // 12
            writer.Write(Param); // 16
            return writer.ToArray();
        }
    }
}
