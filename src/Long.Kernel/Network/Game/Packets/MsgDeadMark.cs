using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgDeadMark : MsgBase<GameClient>
    {
        public DeadMarkAction Action { get; set; }
        public uint TargetIdentity { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgDeadMark);
            writer.Write((int)Action);
            writer.Write(TargetIdentity);
            return writer.ToArray();
        }

        public enum DeadMarkAction
        {
            Add,
            Remove
        }
    }
}
