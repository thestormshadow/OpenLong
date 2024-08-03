using Long.Network.Packets;
using Long.Network.Security;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Serilog;

namespace Long.Network.Sockets
{
    /// <summary>
    ///     Actors are assigned to accepted client sockets to give connected clients a state
    ///     across socket operations. This allows the server to handle multiple receive writes
    ///     across single processing reads, and keep a buffer alive for faster operations.
    /// </summary>
    public abstract class TcpServerActor
    {
        private static readonly ILogger logger = Log.ForContext<TcpServerActor>();

        private object SendLock = new();

        // Fields and Properties
        public Memory<byte> Buffer { get; }
        public ICipher Cipher { get; set; }
        public byte[] PacketFooter { get; }
        public uint ReadPartition { get; }
        public uint WritePartition { get; }
        public Socket Socket { get; }
        public int ReceiveTimeOutSeconds { get; set; } = 30;

        public Stage ConnectionStage { get; set; }


        /// <summary>
        ///     Instantiates a new instance of <see cref="TcpServerActor" /> using an accepted
        ///     client socket and pre-allocated buffer from the server listener.
        /// </summary>
        /// <param name="socket">Accepted client socket</param>
        /// <param name="buffer">Pre-allocated buffer for socket receive operations</param>
        /// <param name="cipher">Cipher for handling client encipher operations</param>
        /// <param name="readPartition">Packet processing partition, default is disabled</param>
        /// <param name="packetFooter">Length of the packet footer</param>
        protected TcpServerActor(
            Socket socket,
            Memory<byte> buffer,
            ICipher cipher,
            uint readPartition = 0,
            uint writePartition = 0,
            string packetFooter = "")
        {
            Buffer = buffer;
            Cipher = cipher;
            Socket = socket;
            PacketFooter = Encoding.ASCII.GetBytes(packetFooter);
            ReadPartition = readPartition;
            WritePartition = writePartition;

            IpAddress = (Socket.RemoteEndPoint as IPEndPoint)?.Address.MapToIPv4().ToString();
        }

        public Guid ID { get; } = Guid.NewGuid();

        /// <summary>
        ///     Returns the remote IP address of the connected client.
        /// </summary>
        public string IpAddress { get; }

        /// <summary>
        ///     Sends a packet to the game client after encrypting bytes. This may be called
        ///     as-is, or overridden to provide channel functionality and thread-safety around
        ///     the accepted client socket. By default, this method locks around encryption
        ///     and sending data.
        /// </summary>
        /// <param name="packet">Bytes to be encrypted and sent to the client</param>
        internal virtual bool InternalSend(byte[] packet)
        {
            lock (SendLock)
            {
                var data = new byte[packet.Length + PacketFooter.Length];
                packet.CopyTo(data, 0);

                BitConverter.TryWriteBytes(data, (ushort)packet.Length);
                Array.Copy(PacketFooter, 0, data, packet.Length, PacketFooter.Length);

                try
                {
                    Cipher?.Encrypt(data, data);
                    int sendResult = Socket.Send(data, SocketFlags.None);
                    return sendResult == data.Length;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode is < SocketError.ConnectionAborted or > SocketError.Shutdown)
                    {
                        logger.Error(ex.ToString());
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        ///     Sends a packet to the game client after encrypting bytes. This may be called
        ///     as-is, or overridden to provide channel functionality and thread-safety around
        ///     the accepted client socket. By default, this method locks around encryption
        ///     and sending data.
        /// </summary>
        /// <param name="packet">Packet to be encrypted and sent to the client</param>
        public virtual Task SendAsync(IPacket packet)
        {
            return SendAsync(packet.Encode());
        }

        public virtual Task SendAsync(IPacket packet, Func<Task> task)
        {
            return SendAsync(packet.Encode(), task);
        }

        public virtual Task DirectSendAsync(IPacket packet)
        {
            InternalSend(packet.Encode());
            return Task.CompletedTask;
        }

        public virtual Task DirectSendAsync(byte[] packet)
        {
            InternalSend(packet);
            return Task.CompletedTask;
        }

        public abstract Task SendAsync(byte[] packet);
        public abstract Task SendAsync(byte[] packet, Func<Task> task);

        /// <summary>
        ///     Force closes the client connection.
        /// </summary>
        public virtual void Disconnect()
        {
            Socket?.Disconnect(false);
        }

        public enum Stage
        {
            Connect,
            Exchange,
            Receiving
        }
    }
}
