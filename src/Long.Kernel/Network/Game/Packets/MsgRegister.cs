using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    /// <remarks>Packet Type 1001</remarks>
    /// <summary>
    ///     Message containing character creation details, such as the new character's name,
    ///     body size, and profession. The character name should be verified, and may be
    ///     rejected by the server if a character by that name already exists.
    /// </summary>
    public sealed class MsgRegister : MsgBase<GameClient>
    {
        public int Cancel { get; set; }
        public string Username { get; set; }
        public string CharacterName { get; set; }
        public string MaskedPassword { get; set; }
        public string UnknownString { get; set; }
        public ushort Mesh { get; set; }
        public ushort Class { get; set; }
        public uint Token { get; set; }
        public uint UnknownUint { get; set; }

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
            Cancel = reader.ReadInt32(); // 4
            Username = reader.ReadString(16); // 8
            CharacterName = reader.ReadString(16); // 24
            MaskedPassword = reader.ReadString(16); // 40
            UnknownString = reader.ReadString(16); // 56
            Mesh = reader.ReadUInt16(); // 72
            Class = reader.ReadUInt16(); // 74
            Token = reader.ReadUInt32(); // 76
            UnknownUint = reader.ReadUInt32();
        }

        /// <summary>
        ///     Process can be invoked by a packet after decode has been called to structure
        ///     packet fields and properties. For the server implementations, this is called
        ///     in the packet handler after the message has been dequeued from the server's
        ///     <see cref="PacketProcessor{TClient}" />.
        /// </summary>
        /// <param name="client">Client requesting packet processing</param>
        public override async Task ProcessAsync(GameClient client)
        {
            if (Cancel != 0)
            {
                client.Disconnect();
                return;
            }

            await RegistrationManager.RegisterAsync(client, Token, Class, Mesh, CharacterName);
        }
    }
}
