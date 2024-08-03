using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgMapItem : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgMapItem>();

        public ushort Color { get; set; }
        public uint Identity { get; set; }
        public uint Itemtype { get; set; }
        public ushort MapX { get; set; }
        public ushort MapY { get; set; }
        public DropType Mode { get; set; }
        public int Composition { get; set; }
        public int Timestamp { get; set; }
        public uint OwnerIdentity { get; set; }
        public int Unknown { get; set; }

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Identity = reader.ReadUInt32();
            Itemtype = reader.ReadUInt32();
            MapX = reader.ReadUInt16();
            MapY = reader.ReadUInt16();
            Color = reader.ReadUInt16();
            Mode = (DropType)reader.ReadUInt16();
            Composition = reader.ReadInt32();
            OwnerIdentity = reader.ReadUInt32();
            Unknown = reader.ReadInt32();
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
            writer.Write((ushort)PacketType.MsgMapItem);
            writer.Write(Timestamp); // 4
            writer.Write(Identity);      // 8
            writer.Write(Itemtype);      // 12
            writer.Write(MapX);          // 16
            writer.Write(MapY);          // 18
            writer.Write(Color);         // 20
            writer.Write((ushort)Mode); // 22
            writer.Write((byte)0); // 24
            writer.Write((byte)0); // 25
            writer.Write((byte)0); // 26
            writer.Write((byte)Composition); // 27
            writer.Write(OwnerIdentity); // 28
            writer.Write(Unknown); // 32
            return writer.ToArray();
        }

        public enum DropType : ushort
        {
            Unknown = 0,
            LayItem = 1,
            DisappearItem = 2,
            PickupItem = 3,
            DetainItem = 4,
            LayTrap = 10,
            SynchroTrap = 11,
            DropTrap = 12
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            if (!user.IsAlive)
            {
                await user.SendAsync(StrDead);
                return;
            }

            switch (Mode)
            {
                case DropType.PickupItem:
                    if (await user.SynPositionAsync(MapX, MapY, 0))
                    {
                        await user.PickMapItemAsync(Identity, Composition != 0 && user.VipLevel >= 4);
                        await user.BroadcastRoomMsgAsync(this, true);
                    }

                    break;
                default:
                    {
                        if (user.IsGm())
                        {
                            await client.SendAsync(new MsgTalk(TalkChannel.Service, $"Missing packet {Type}, Action {Mode}, Length {Length}"));
                        }
                        logger.Warning("Missing packet {0}, Action {1}, Length {2}\n{3}", Type, Mode, Length, PacketDump.Hex(Encode()));
                        break;
                    }
            }
        }
    }
}
