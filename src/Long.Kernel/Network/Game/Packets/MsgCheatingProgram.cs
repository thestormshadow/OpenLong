using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgCheatingProgram : MsgBase<GameClient>
    {
        public MsgCheatingProgram(uint id, string message)
        {
            Identity = id;
            Messages.Add(message);
        }

        public uint Identity { get; set; }
        public List<string> Messages { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgCheatingProgram);
            writer.Write(Identity);
            writer.Write(Messages);
            return writer.ToArray();
        }
    }
}
