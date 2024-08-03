using Long.Network.Packets;

namespace Long.Login.Network.Login.Packets
{
    public sealed class MsgEncryptCode : MsgBase<LoginClient>
    {
        public MsgEncryptCode(uint seed)
        {
            Seed = seed;
        }

        // Packet Properties
        public uint Seed { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgEncryptCode);
            writer.Write(Seed);
            return writer.ToArray();
        }
    }
}
