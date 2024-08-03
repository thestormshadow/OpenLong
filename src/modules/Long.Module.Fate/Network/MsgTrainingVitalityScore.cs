using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Fate.Network
{
    public sealed class MsgTrainingVitalityScore : MsgBase<GameClient>
    {
        public byte AttrType { get; set; }
        public int Power { get; set; }
        public string Name { get; set; }

        public override byte[] Encode()
        {
            PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTrainingVitalityScore);
            writer.Write(AttrType);
            writer.Write(Power);
            writer.Write(Name, 16);
            return writer.ToArray();
        }
    }
}
