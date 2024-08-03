using Long.Network.Sockets;
using Serilog;

namespace Long.Network.Packets
{
    /// <summary>
    ///     Base message class that provides a derived message class with methods and properties
    ///     for defining decoding and encoding rules for a Conquer Online packet structure. Adds
    ///     functions for byte slice manipulation.
    /// </summary>
    /// <typeparam name="TClient">Type of client being processed with the packet</typeparam>
    public abstract class MsgBase<TClient> : IPacket
    {
        private static readonly ILogger logger = Log.ForContext<MsgBase<TClient>>();

        // Base Properties
        public ushort Length { get; protected set; }
        public PacketType Type { get; protected set; }

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public virtual void Decode(byte[] bytes)
        {
            var length = BitConverter.ToUInt16(bytes, 0);
            var type = (PacketType)BitConverter.ToUInt16(bytes, 2);
            throw new NotImplementedException($"Packet decode not implemented!\nLength:{length}\tType{type}\n{PacketDump.Hex(bytes)}");
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public virtual byte[] Encode()
        {
            throw new NotImplementedException("Packet Encode not implemented!");
        }

        /// <summary>
        ///     Process can be invoked by a packet after decode has been called to structure
        ///     packet fields and properties. For the server implementations, this is called
        ///     in the packet handler after the message has been dequeued from the server's
        ///     <see cref="PacketProcessor{TClient}" />.
        /// </summary>
        /// <param name="client">Client requesting packet processing</param>
        public virtual Task ProcessAsync(TClient client)
        {
            logger.Information("Process packet not being handled!!!\nPacketType: {0} Length: {1}", Type, Length);
            return Task.CompletedTask;
        }
    }
}
