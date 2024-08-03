using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMagicInfo : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgMagicInfo>();

        public int Timestamp { get; set; } = Environment.TickCount;
        public uint Experience { get; set; }
        public ushort Magictype { get; set; }
        public ushort Level { get; set; }
        public MagicAction Action { get; set; }
        public uint CurrentEffect { get; set; }
        public uint AvailableEffects { get; set; }
        public uint ExorbitantEffects { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Experience = reader.ReadUInt32();
            Magictype = (ushort)reader.ReadUInt32();
            Level = reader.ReadUInt16();
            Action = (MagicAction)reader.ReadUInt16();
            CurrentEffect = reader.ReadUInt32();
            AvailableEffects = reader.ReadUInt32();
            ExorbitantEffects = reader.ReadUInt32();
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
            writer.Write((ushort)PacketType.MsgMagicInfo);
            writer.Write(Timestamp); // 4
            writer.Write(Experience); // 8
            writer.Write((int)Magictype); // 12
            writer.Write(Level); // 16
            writer.Write((ushort)Action); // 18
            writer.Write(CurrentEffect); // 20 
            writer.Write(AvailableEffects); // 24
            writer.Write(ExorbitantEffects); // 28
            return writer.ToArray();
        }

        //public override async Task ProcessAsync(Client client)
        //{
        //    Character user = client.Character;
        //    switch (Action)
        //    {
        //        case MagicAction.SelectMagicEffect:
        //            {
        //                Magic magic = user.MagicData[Magictype];
        //                if (magic == null)
        //                {
        //                    return;
        //                }

        //                uint currentEffectFlag = (uint)(1 << (int)CurrentEffect);
        //                if ((magic.AvailableEffectType & currentEffectFlag) == 0 && CurrentEffect != 0)
        //                {
        //                    return;
        //                }

        //                magic.CurrentEffectType = (byte)CurrentEffect;

        //                await magic.SaveAsync();

        //                Action = MagicAction.SetMagicEffect;
        //                await user.SendAsync(this);
        //                break;
        //            }

        //        default:
        //            {
        //                logger.Warning($"Unhandled action {Action}");
        //                if (user.IsGm())
        //                {
        //                    await user.SendAsync($"[MsgMagicInfo] unhandled action {Action}");
        //                }
        //                break;
        //            }
        //    }
        //}

        public enum MagicAction : ushort
        {
            AddExsisting = 0,
            AddNew = 1,
            SelectMagicEffect = 2,
            SetMagicEffect = 3,
            AddEffectType = 4,
            /// <summary>
            /// Sent to Server only.
            /// </summary>
            RemoveMagicSoul = 5,
            DropMagicSuccess = 6,
            Unknown = 7
        }
    }
}
