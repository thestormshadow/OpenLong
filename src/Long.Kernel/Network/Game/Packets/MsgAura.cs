using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAura : MsgBase<GameClient>
    {
        public int Timestamp => Environment.TickCount;
        public AuraAction Action { get; set; }
        public uint Identity { get; set; }
        public AuraType Aura { get; set; }
        public int Level { get; set; }
        public int Power0 { get; set; }
        public int Power1 { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAura);
            writer.Write(0); // 4
            writer.Write((int)Action); // 8
            writer.Write(Identity); // 12
            writer.Write((int)Aura); // 16
            writer.Write(Level); // 20 
            writer.Write(Power0); // 24
            writer.Write(Power1); // 28
            return writer.ToArray();
        }

        public enum AuraAction
        {
            Detach = 2,
            Attach = 3
        }

        public enum AuraType : uint
        {
            Tyrant = 1,
            Fend = 2,
            Metal = 3,
            Wood = 4,
            Water = 5,
            Fire = 6,
            Earth = 7,
            MagicDefender = 8,
        }
    }
}
