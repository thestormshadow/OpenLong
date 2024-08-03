using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgDutyMinContri : MsgBase<GameClient>
    {
        public List<MinContriStruct> Members = new();
        public ushort Action { get; set; }
        public ushort Count { get; set; }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgDutyMinContri);
            writer.Write(Action);
            writer.Write(Count = (ushort)Members.Count);
            foreach (MinContriStruct member in Members)
            {
                writer.Write(member.Position);
                writer.Write(member.Donation);
            }

            return writer.ToArray();
        }

        public struct MinContriStruct
        {
            public int Position;
            public uint Donation;
        }
    }
}
