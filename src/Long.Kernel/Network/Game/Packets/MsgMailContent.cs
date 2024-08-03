using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMailContent : MsgBase<GameClient>
    {
        public uint Data { get; set; }
        public string Content { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgMailContent);
            writer.Write(Data);
            writer.Write(Content, 768);
            return writer.ToArray();
        }
    }
}
