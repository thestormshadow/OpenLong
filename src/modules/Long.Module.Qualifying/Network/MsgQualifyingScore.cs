using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierScore : MsgBase<GameClient>
    {
        public uint Identity1 { get; set; }
        public string Name1 { get; set; }
        public int Damage1 { get; set; }

        public uint Identity2 { get; set; }
        public string Name2 { get; set; }
        public int Damage2 { get; set; }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingScore);
            writer.Write(Identity1);
			writer.Write((uint)0);
			writer.Write(Name1, 16);
            writer.Write(Damage1);
            writer.Write(Identity2);
			writer.Write((uint)0);
			writer.Write(Name2, 16);
            writer.Write(Damage2);
            return writer.ToArray();
        }
    }
}
