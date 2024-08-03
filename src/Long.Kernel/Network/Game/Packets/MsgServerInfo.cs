using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgServerInfo : MsgBase<GameClient>
    {
        public int ClassicMode { get; set; }
        public int PotencyMode { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgServerInfo);
            writer.Write(ClassicMode);
            writer.Write(PotencyMode);
            return writer.ToArray();
        }
    }
}
