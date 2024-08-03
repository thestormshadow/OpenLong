using Long.Network.Packets;
using Long.Network.Security;
using Long.Network.Sockets;
using Long.Network;
using System.Net.Sockets;
using Serilog;

namespace Long.Ai.Sockets
{
    public sealed class GameServer : TcpServerActor
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<GameServer>();

        private ConnectionStage stage;

        /// <summary>
        ///     Instantiates a new instance of <see cref="GameServer" /> using the Accepted event's
        ///     resulting socket and pre-allocated buffer. Initializes all account server
        ///     states, such as the cipher used to decrypt and encrypt data.
        /// </summary>
        /// <param name="socket">Accepted remote client socket</param>
        /// <param name="buffer">Pre-allocated buffer from the server listener</param>
        /// <param name="partition">Packet processing partition</param>
        public GameServer(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition)
            : base(socket, buffer, null, readPartition, writePartition, NetworkDefinition.NPC_FOOTER)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public ConnectionStage Stage 
        {
            get => stage;
            set
            {
                stage = value;
                logger.Debug($"Connection Stage changed to: {value}");
            }
        }
        public DiffieHellman DiffieHellman { get; }

        public override Task SendAsync(byte[] packet)
        {
            Kernel.NetworkMonitor.Send(packet.Length);
            GameServerHandler.Instance?.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            Kernel.NetworkMonitor.Send(packet.Length);
            GameServerHandler.Instance?.Send(this, packet);
            return Task.CompletedTask;
        }

        public enum ConnectionStage
        {
            Disconnected,
            Awaiting,
            Exchanging,
            Authenticating,
            Ready
        }
    }
}
