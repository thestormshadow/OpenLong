using System.Net.Sockets;
using System.Text;
using Long.Network.Packets;
using Serilog;

namespace Long.Network.Sockets
{
    public abstract class TcpClientSocket<TActor> : TcpClientEvents<TActor>
        where TActor : TcpServerActor
    {
        private static readonly ILogger logger = Log.ForContext<TcpClientSocket<TActor>>();

        private readonly Memory<byte> buffer;
        private readonly int expectedFooterLength;
        private readonly string expectedFooter;
        private readonly CancellationTokenSource shutdownToken;
        private readonly Socket socket;
        private readonly bool exchange;
#if !DEBUG
        private readonly int receiveTimeoutSecond;
#endif

        protected int ExchangeStartPosition = 0;
        protected readonly PacketProcessor<TActor> packetProcessor;

        protected TcpClientSocket(string footer, bool exchange = false)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                LingerState = new LingerOption(false, 0),
                NoDelay = true
            };

            shutdownToken = new CancellationTokenSource();

            expectedFooter = footer;
            expectedFooterLength = expectedFooter?.Length ?? 0;
            this.exchange = exchange;

#if !DEBUG
            receiveTimeoutSecond = RECEIVE_TIMEOUT_SECONDS;
#endif

            buffer = new Memory<byte>(new byte[MAX_BUFFER_SIZE]);
            packetProcessor = new PacketProcessor<TActor>(ProcessAsync, 1, 1);
        }

        public NetworkMonitor NetworkMonitor { get; init; } = new NetworkMonitor();

        protected abstract Task ProcessAsync(TActor actor, byte[] message);

        protected override void Received(TActor actor, ReadOnlySpan<byte> packet)
        {
            NetworkMonitor.Receive(packet.Length);
            packetProcessor.QueueRead(actor, packet.ToArray());
        }

        public override void Send(TActor actor, ReadOnlySpan<byte> packet)
        {
            NetworkMonitor.Send(packet.Length);
            packetProcessor.QueueWrite(actor, packet.ToArray());
        }

        public override void Send(TActor actor, ReadOnlySpan<byte> packet, Func<Task> task)
        {
            NetworkMonitor.Send(packet.Length);
            packetProcessor.QueueWrite(actor, packet.ToArray(), task);
        }

        public async Task<bool> ConnectToAsync(string address, int port)
        {
            try
            {
                await socket.ConnectAsync(address, port, shutdownToken.Token);
                _ = packetProcessor.StartAsync(shutdownToken.Token);
                TActor actor = await ConnectedAsync(socket, buffer);
                if (actor == null)
                {
                    if (socket.Connected)
                    {
                        await socket.DisconnectAsync(false);
                    }

                    logger.Error("Could not complete connection with Server!");
                    return false;
                }

                if (exchange)
                {
                    var receiveTask = new TaskFactory().StartNew(ExchangingAsync, actor, shutdownToken.Token)
                                                   .ConfigureAwait(false);
                }
                else
                {
                    var receiveTask = new TaskFactory().StartNew(ReceivingAsync, actor, shutdownToken.Token)
                                                   .ConfigureAwait(false);
                }

                return socket.Connected;
            }
            catch (Exception ex)
            {
                if (ex is SocketException socketException && socketException.ErrorCode == 10061)
                {
                    logger.Error("Failed to connect to the server...");
                }
                else
                {
                    logger.Error(ex, "Failed to connect to the server... [{}]", ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        ///     Exchanging receives bytes from the accepted client socket when bytes become
        ///     available as a raw buffer of bytes. This method is called once and then invokes
        ///     <see cref="ReceivingAsync(object)" />.
        /// </summary>
        /// <param name="state">Created actor around the accepted client socket</param>
        /// <returns>Returns task details for fault tolerance processing.</returns>
        private async Task ExchangingAsync(object state)
        {
            // Initialize multiple receive variables
            var actor = state as TActor;
            var timeout = new CancellationTokenSource();
            int consumed = 0, examined = 0, remaining = 0;

            if (actor.Socket.Connected && !shutdownToken.IsCancellationRequested)
            {
                try
                {
                    using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(
                        timeout.Token, shutdownToken.Token);
                    // Receive data from the client socket
                    ValueTask<int> receiveOperation = actor.Socket.ReceiveAsync(
                        actor.Buffer[..],
                        SocketFlags.None,
                        cancellation.Token);

                    timeout.CancelAfter(TimeSpan.FromSeconds(30));
                    examined = await receiveOperation;
                    if (examined < ExchangeStartPosition + 2)
                    {
                        throw new Exception("Invalid length");
                    }
                }
                catch (Exception e)
                {
                    if (e is SocketException socketEx)
                    {
                        if (socketEx.SocketErrorCode is < SocketError.ConnectionAborted or > SocketError.Shutdown)
                        {
                            logger.Error(socketEx.ToString());
                        }
                    }
                    else
                    {
                        logger.Verbose(e, e.Message);
                    }

                    actor.Disconnect();
                    Disconnecting(actor);
                    return;
                }

                actor.Cipher?.Decrypt(actor.Buffer.Span,
                                    actor.Buffer.Span);
                consumed = BitConverter.ToUInt16(actor.Buffer.Span[ExchangeStartPosition..2]) + expectedFooterLength;

                if (consumed > examined)
                {
                    logger.Error($"Exchange error length [{consumed},{examined}] for [IP: {actor.IpAddress}]");
                    actor.Disconnect();
                    Disconnecting(actor);
                    return;
                }

                // Process the exchange now that bytes are decrypted
                if (!await ExchangedAsync(actor, new Memory<byte>(actor.Buffer[..consumed].Span.ToArray())))
                {
                    logger.Error($"Exchange error for [IP: {actor.IpAddress}]");
                    actor.Disconnect();
                    Disconnecting(actor);
                    return;
                }

                // Now that the key has changed, decrypt the rest of the bytes in the buffer
                // and prepare to start receiving packets on a standard receive loop.
                if (consumed < examined)
                {
                    actor.Cipher?.Decrypt(
                        actor.Buffer[consumed..examined].Span,
                        actor.Buffer[consumed..examined].Span);

                    if (!Splitting(actor, examined, ref consumed))
                    {
                        logger.Error("[Exchange] Client disconnected due to invalid packet.");
                        actor.Disconnect();
                        Disconnecting(actor);
                        return;
                    }

                    remaining = examined - consumed;
                    actor.Buffer[consumed..examined].CopyTo(actor.Buffer);
                }
            }

            // Start receiving packets
            await ReceivingAsync(state, remaining);
        }

        /// <summary>
        ///     Receiving receives bytes from the accepted client socket when bytes become
        ///     available. While the client is connected and the server hasn't issued the
        ///     shutdown signal, bytes will be received in a loop.
        /// </summary>
        /// <param name="state">Created actor around the accepted client socket</param>
        /// <returns>Returns task details for fault tolerance processing.</returns>
        private Task ReceivingAsync(object state)
        {
            return ReceivingAsync(state, 0);
        }

        /// <summary>
        ///     Receiving receives bytes from the accepted client socket when bytes become
        ///     available. While the client is connected and the server hasn't issued the
        ///     shutdown signal, bytes will be received in a loop.
        /// </summary>
        /// <param name="state">Created actor around the accepted client socket</param>
        /// <param name="remaining">Starting offset to receive bytes to</param>
        /// <returns>Returns task details for fault tolerance processing.</returns>
        private async Task ReceivingAsync(object state, int remaining)
        {
            // Initialize multiple receive variables
            var actor = state as TActor;
            var timeout = new CancellationTokenSource();
            int examined = 0, consumed = 0;

            while (actor.Socket.Connected && !shutdownToken.IsCancellationRequested)
            {
                try
                {
#if !DEBUG
                    using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(
                        timeout.Token, shutdownToken.Token);
                    // Receive data from the client socket
                    var receiveOperation = actor.Socket.ReceiveAsync(
                        actor.Buffer.Slice(remaining),
                        SocketFlags.None,
                        cancellation.Token);

                    timeout.CancelAfter(TimeSpan.FromSeconds(receiveTimeoutSecond));
#else
                    ValueTask<int> receiveOperation = actor.Socket.ReceiveAsync(
                        actor.Buffer[remaining..],
                        SocketFlags.None, timeout.Token);
#endif
                    examined = await receiveOperation;
                    if (examined == 0)
                    {
                        break;
                    }
                }
                catch (OperationCanceledException e)
                {
                    logger.Warning(e, e.Message);
                    break;
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode is < SocketError.ConnectionAborted or > SocketError.Shutdown)
                    {
                        logger.Warning(e, e.Message);
                    }

                    break;
                }

                // Decrypt traffic
                actor.Cipher?.Decrypt(
                    actor.Buffer.Slice(remaining, examined).Span,
                    actor.Buffer.Slice(remaining, examined).Span);

                // Handle splitting and processing of data
                consumed = 0;
                if (!Splitting(actor, examined + remaining, ref consumed))
                {
                    string error = string.Format("Client disconnected due to invalid packet.\n" +
                        "Examined: {0}\n" +
                        "Remaining: {1}\n" +
                        "Consumed: {2}\n" + PacketDump.Hex(actor.Buffer.ToArray()),
                        examined, remaining, consumed);
                    logger.Error(error);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(error);
					Console.ResetColor();
					break;
                }

                remaining = examined + remaining - consumed;
                actor.Buffer.Slice(consumed, remaining).CopyTo(actor.Buffer);
            }

            actor.Disconnect();
            // Disconnect the client
            Disconnecting(actor);
        }

        /// <summary>
        ///     Splitting splits the actor's receive buffer into multiple packets that can
        ///     then be processed by Received individually. The default behavior of this method
        ///     unless otherwise overridden is to split packets from the buffer using an unsigned
        ///     short packet header for the length of each packet.
        /// </summary>
        /// <param name="actor">Actor for consuming bytes from the buffer</param>
        /// <param name="examined">Number of examined bytes from the receive</param>
        /// <param name="consumed">Number of consumed bytes by the split reader</param>
        /// <returns>Returns true if the client should remain connected.</returns>
        protected virtual bool Splitting(TActor actor, int examined, ref int consumed)
        {
            // Consume packets from the socket buffer
            Span<byte> buffer = actor.Buffer.Span;
            while (consumed + 2 < examined)
            {
                var length = BitConverter.ToUInt16(buffer.Slice(consumed, 2));
                if (length == 0)
                {
                    return false;
                }

                int expected = consumed + length + expectedFooterLength;
                if (length > buffer.Length)
                {
                    return false;
                }

                if (expected > examined)
                {
                    break;
                }

                Span<byte> processBytes = buffer.Slice(consumed, length);
                if (!string.IsNullOrEmpty(expectedFooter))
                {
                    Span<byte> footerBytes = buffer.Slice(consumed + length, expectedFooterLength);
                    string receivedFooter = Encoding.ASCII.GetString(footerBytes.ToArray());
                    if (!receivedFooter.Equals(expectedFooter) && !receivedFooter.Equals("WUuTxfpe"))
                    {
                        logger.Error("Invalid packet footer.");
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid packet footer.");
						Console.ResetColor();
						return false;
                    }
                }

                Received(actor, processBytes); // skip the footer
                consumed += length + expectedFooterLength;
            }

            return true;
        }

        /// <summary>
        ///     Disconnecting is called when the client is disconnecting from the server. Allows
        ///     the server to handle client events post-disconnect, and reclaim resources first
        ///     leased to the client on accept.
        /// </summary>
        /// <param name="actor">Actor being disconnected</param>
        private void Disconnecting(TActor actor)
        {
            // Reclaim resources and release back to server pools
            actor.Buffer.Span.Clear();
            // Complete processing for disconnect
            Disconnected(actor);
        }

        public const int MAX_BUFFER_SIZE = 4096;
        public const int RECEIVE_TIMEOUT_SECONDS = 600;
    }
}
