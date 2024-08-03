using Long.Kernel.Modules.Systems.Fate;
using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Fate.Network
{
    public sealed class MsgTrainingVitalityExpiryNotify : MsgBase<GameClient>
    {
        public int Count => Fates.Count;
        public List<IFate.FateType> Fates { get; private set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgTrainingVitalityExpiryNotify);
            writer.Write(Count);
            foreach (var fate in Fates)
            {
                writer.Write((byte)fate);
            }
            return writer.ToArray();
        }
    }
}
