using Long.Network.Packets;
using System.Drawing;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgPaint : MsgBase<GameClient>
    {
        public List<Point> Points { get; } = new List<Point>();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgPaint);
            writer.Write(Points.Count);
            foreach (var point in Points)
            {
                writer.Write(point.X * 10000 + point.Y);
            }
            return writer.ToArray();
        }
    }
}
