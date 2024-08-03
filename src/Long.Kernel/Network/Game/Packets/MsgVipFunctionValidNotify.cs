using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgVipFunctionValidNotify : MsgBase<GameClient>
    {
        public int Flags { get; set; }

        public override byte[] Encode()
        {
            PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgVipFunctionValidNotify);
            writer.Write(Flags);
            return writer.ToArray();
        }
    }
}
