using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.NeiGong.Network
{
    public sealed class MsgInnerStrengthTotalInfo : MsgBase<GameClient>
    {
        public struct InnerStrengthInfo
        {
            public byte Id { get; set; }
            public int Data { get; set; }
        }

        public List<InnerStrengthInfo> InnerStrengths { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgInnerStrengthTotalInfo);
            writer.Write((ushort)InnerStrengths.Count);
            foreach (var innerStrength in InnerStrengths)
            {
                writer.Write(innerStrength.Id);
                writer.Write(innerStrength.Data);
            }
            return writer.ToArray();
        }
    }
}
