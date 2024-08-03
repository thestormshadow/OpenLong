using Long.Network.Packets;
using Long.Network.Security;
using Long.Network.Services;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace Long.Kernel.Network.Game.Packets
{
    /// <summary>
    ///     Message containing keys necessary for conducting the Diffie-Hellman key exchange.
    ///     The initial message to the client is sent on connect, and contains initial seeds
    ///     for Cast5. The response message from the client then contains the shared key.
    /// </summary>
    public sealed class MsgHandshake : MsgBase<GameClient>
    {
        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgHandshake" />. This constructor
        ///     is called to accept the client response.
        /// </summary>
        public MsgHandshake()
        {
        }

        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgHandshake" />. This constructor
        ///     is called to construct the initial request to the client.
        /// </summary>
        /// <param name="dh">Diffie-Hellman key exchange instance for the actor</param>
        /// <param name="encryptionIV">Initial seed for Cast5's encryption IV</param>
        /// <param name="decryptionIV">Initial seed for Cast5's decryption IV</param>
        public MsgHandshake(NDDiffieHellman dh, byte[] encryptionIV, byte[] decryptionIV)
        {
            PrimeRoot = Hex.ToHexString(dh.PrimeRoot.ToByteArrayUnsigned()).ToUpper();
            Generator = Hex.ToHexString(dh.Generator.ToByteArrayUnsigned()).ToUpper();
            ServerKey = Hex.ToHexString(dh.PublicKey.ToByteArrayUnsigned()).ToUpper();
            EncryptionIV = (byte[])encryptionIV.Clone();
            DecryptionIV = (byte[])decryptionIV.Clone();
        }

        /// <summary>Randomizes padding for the message.</summary>
        public async Task RandomizeAsync()
        {
            Padding = new byte[await RandomnessService.Instance.NextIntegerAsync(24, 48)];
            await RandomnessService.Instance.NextBytesAsync(Padding);
        }

        // Packet Properties
        public byte[] DecryptionIV { get; }
        public byte[] EncryptionIV { get; }
        public string PrimeRoot { get; }
        public string Generator { get; }
        public string ServerKey { get; }
        public string ClientKey { get; private set; }
        public byte[] Padding { get; set; }

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            reader.BaseStream.Seek(7, SeekOrigin.Begin);
            Length = (ushort)reader.ReadUInt32();
            reader.BaseStream.Seek(reader.ReadUInt32(), SeekOrigin.Current);
            ClientKey = Encoding.ASCII.GetString(reader.ReadBytes(reader.ReadInt32()));
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
            int messageLength = 36 + Padding.Length + EncryptionIV.Length
                                + DecryptionIV.Length + PrimeRoot.Length + Generator.Length
                                + ServerKey.Length;

            // The packet writer class reserves 2 bytes for the send method to fill in for
            // the packet length. This message is an outlier to this pattern; however, 
            // leaving the reserved bytes does not affect the body of the message, so it
            // can be left in.

            writer.Write(Padding.AsSpan(0, 9));
            writer.Write(messageLength - 11);
            writer.Write(Padding.Length - 11);
            writer.Write(Padding.AsSpan(9, Padding.Length - 11));
            writer.Write(EncryptionIV.Length);
            writer.Write(EncryptionIV);
            writer.Write(DecryptionIV.Length);
            writer.Write(DecryptionIV);
            writer.Write(PrimeRoot.Length);
            writer.Write(PrimeRoot, PrimeRoot.Length);
            writer.Write(Generator.Length);
            writer.Write(Generator, Generator.Length);
            writer.Write(ServerKey.Length);
            writer.Write(ServerKey, ServerKey.Length);
            return writer.ToArray();
        }
    }
}
