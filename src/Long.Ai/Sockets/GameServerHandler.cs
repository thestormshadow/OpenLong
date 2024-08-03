using Long.Network.Packets;
using Long.Network.Sockets;
using Long.Network;
using System.Net.Sockets;
using Long.Ai.Sockets.Packets;
using Long.Ai.Threading;
using Long.Ai.Managers;
using Serilog;
using Long.Network.Packets.Ai;

namespace Long.Ai.Sockets
{
	public sealed class GameServerHandler : TcpClientSocket<GameServer>
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<GameServerHandler>();

        private readonly PacketProcessor<GameServer> processor;

        public static GameServerHandler Instance { get; set; }

        public GameServerHandler()
            : base(NetworkDefinition.NPC_FOOTER)
        {
            processor = new PacketProcessor<GameServer>(ProcessAsync, 1);
            _ = processor.StartAsync(CancellationToken.None).ConfigureAwait(false);
            ExchangeStartPosition = 0;
        }

        public GameServer GameServer { get; private set; }

        protected override async Task<GameServer> ConnectedAsync(Socket socket, Memory<byte> buffer)
        {
			uint readProcessor = packetProcessor.SelectReadPartition();
			uint writeProcessor = packetProcessor.SelectWritePartition();
			GameServer client = new(socket, buffer, readProcessor, writeProcessor);
            if (socket.Connected)
            {
                GameServer = client;
                Instance = this;
                BasicThread.ResetReconnectTimer();
                client.Stage = GameServer.ConnectionStage.Authenticating;
                await client.SendAsync(new MsgAiLoginExchange
                {
                    Data = new MsgAiLoginExchangeContract
					{
						UserName = ServerConfiguration.Configuration.Ai.Username,
						Password = ServerConfiguration.Configuration.Ai.Password,
						ServerName = ServerConfiguration.Configuration.Ai.Name
					}
                });
                return client;
            }
            return null;
        }

        protected override void Received(GameServer actor, ReadOnlySpan<byte> packet)
        {
            Kernel.NetworkMonitor.Receive(packet.Length);
            processor.QueueRead(actor, packet.ToArray());
        }

        public override void Send(GameServer actor, ReadOnlySpan<byte> packet)
        {
            processor.QueueWrite(actor, packet.ToArray());
        }

		protected override async Task ProcessAsync(GameServer actor, byte[] packet)
        {
            // Validate connection
            if (!actor.Socket.Connected)
            {
                return;
            }

            var length = BitConverter.ToUInt16(packet, 0);
            var type = (PacketType)BitConverter.ToUInt16(packet, 2);
            if (type != PacketType.MsgAiPing)
            {

            }
            try
            {
                MsgBase<GameServer> msg = null;
                switch (type)
                {
					case PacketType.MsgAiLoginExchangeEx:
						msg = new MsgAiLoginExchangeEx();
						break;

					case PacketType.MsgAiAction:
						msg = new MsgAiAction();
						break;

					case PacketType.MsgAiPing:
						msg = new MsgAiPing();
						break;

					case PacketType.MsgAiDynaMap:
						msg = new MsgAiDynaMap();
						break;

					case PacketType.MsgAiPlayerLogin:
						msg = new MsgAiPlayerLogin();
						break;

					case PacketType.MsgAiPlayerLogout:
						msg = new MsgAiPlayerLogout();
						break;

					case PacketType.MsgAiRoleLogin:
						msg = new MsgAiRoleLogin();
						break;

					case PacketType.MsgAiSpawnNpc:
						msg = new MsgAiSpawnNpc();
						break;

					case PacketType.MsgAiRoleStatusFlag:
						msg = new MsgAiRoleStatusFlag();
						break;

					case PacketType.MsgAiGeneratorManage:
						msg = new MsgAiGeneratorManage();
						break;

					default:
                        string message = string.Format("Missing packet {Type}, Length {Length}\n{Packet}", type, length, PacketDump.Hex(packet));
                        logger.Warning(message);
                        return;
                }

                // Decode packet bytes into the structure and process
                msg.Decode(packet);
                await msg.ProcessAsync(actor);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "ProcessAsync: {Message}", e.Message);
				logger.Warning(e.Message);
			}
        }

        protected override void Disconnected(GameServer actor)
        {
            logger.Information("Disconnected from the game server!");
            Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Disconnected from the game server!");
            Console.ResetColor();
			RoleManager.ClearUserSet();
            BasicThread.ResetReconnectTimer();
            Instance = null;
        }
    }
}
