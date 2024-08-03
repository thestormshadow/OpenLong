
using Long.Network.Packets;
using static Long.Kernel.Network.Game.Packets.MsgInteract;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMagicEffect : MsgBase<GameClient>
    {
        private readonly List<MagicTarget> Targets = new();
        public uint AttackerIdentity { get; set; }
        public uint Command { get; set; }
        public ushort MapX
        {
            get => (ushort)(Command - (MapY << 16));
            set => Command = (uint)(MapY << 16 | value);
        }

        public ushort MapY
        {
            get => (ushort)(Command >> 16);
            set => Command = (uint)(value << 16) | Command;
        }
        public ushort MagicIdentity { get; set; }
        public ushort MagicLevel { get; set; }
        public ushort NextMagic { get; set; }
        public byte MagicSoul { get; set; }
        public uint Count { get; set; }
        public int SpellEffect { get; set; }

        public void Append(uint idTarget, int damage, bool showValue, InteractionEffect effect = InteractionEffect.None, int effectValue = 0, int x = 0, int y = 0, bool hideDamage = false)
        {
            Count++;
            Targets.Add(new MagicTarget
            {
                Identity = idTarget,
                Damage = damage,
                Show = showValue ? 1 : 0,
                Effect = (int)effect,
                EffectValue = effectValue,
                X = x,
                Y = y,
                HideDamage = hideDamage ? 1 : 0
            });
        }

        public void ClearTargets()
        {
            Count = 0;
            Targets.Clear();
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgMagicEffect);
            writer.Write(AttackerIdentity); // 4
            writer.Write(MapX); // 8
            writer.Write(MapY); // 10
            writer.Write(MagicIdentity); // 12
            writer.Write(MagicLevel); // 14
            writer.Write(NextMagic); // 16
            writer.Write(MagicSoul); // 18
            if (Count != 0)
            {
                writer.Write((byte)Count); // 19
            }
            else
            {
                writer.Write((byte)(Count = (uint)Targets.Count)); // 19
            }
            writer.Write(SpellEffect);
            foreach (MagicTarget target in Targets)
            {
                writer.Write(target.Identity); // 20+ 0
                writer.Write(target.Damage); // 4
                writer.Write(target.Show); // 8
                writer.Write(target.Effect); // 12
                writer.Write(target.EffectValue); // 16
                writer.Write(target.X); // 20
                writer.Write(target.Y); // 24
                writer.Write(target.HideDamage); // 28
            }
            return writer.ToArray();
        }

        private struct MagicTarget
        {
            public uint Identity { get; set; }
            public int Damage { get; set; }
            public int Show { get; set; }
            public int Effect { get; set; }
            public int EffectValue { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int HideDamage { get; set; }
        }
    }
}
