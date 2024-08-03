using Long.Game.Network.Ai.Packets;
using Long.Network;
using Long.Network.Packets;
using Long.Network.Packets.Ai;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Ai
{
	public sealed class NpcServer : TcpServerListener<AiClient>
	{
		private static readonly ILogger logger = Log.ForContext<NpcServer>();
		public static NpcServer Instance { get; private set; }
		public static AiClient NpcClient { get; private set; }
		public NpcServer(): base(1, exchange: false, footer: NetworkDefinition.NPC_FOOTER)
		{
			if (Instance != null)
			{
				throw new ApplicationException("Cannot start server socket twice!");
			}

			Instance = this;
		}

		protected override async Task<AiClient> AcceptedAsync(Socket socket, Memory<byte> buffer)
		{
			uint readPartition = packetProcessor.SelectReadPartition();
			uint writePartition = packetProcessor.SelectWritePartition();
			var client = new AiClient(socket, buffer, readPartition, writePartition);
			NpcClient = client;
			logger.Information($"Accepting connection from npc server on [{client.IpAddress}].");
			return client;
		}

		protected override void Received(AiClient actor, ReadOnlySpan<byte> packet)
		{
			NetworkMonitor.Receive(packet.Length);
			packetProcessor.QueueRead(actor, packet.ToArray());
		}

		protected override async Task ProcessAsync(AiClient actor, byte[] packet)
		{
			// Read in TQ's binary header
			var length = BitConverter.ToUInt16(packet, 0);
			var type = (PacketType)BitConverter.ToUInt16(packet, 2);

			// Validate connection
			if (!actor.Socket.Connected)
			{
				return;
			}

			try
			{
				// Switch on the packet type
				MsgBase<AiClient> msg = null;
				switch (type)
				{
					case PacketType.MsgAiLoginExchange:
						msg = new MsgAiLoginExchange();
						break;

					case PacketType.MsgAiPing:
						msg = new MsgAiPing();
						break;

					case PacketType.MsgAiAction:
						msg = new MsgAiAction();
						break;

					case PacketType.MsgAiSpawnNpc:
						msg = new MsgAiSpawnNpc();
						break;

					case PacketType.MsgAiInteract:
						msg = new MsgAiInteract();
						break;

					default:
						{
							logger.Warning($"Missing packet {type}, Length {length}\n{PacketDump.Hex(packet)}");
							return;
						}
				}

				// Decode packet bytes into the structure and process
				msg.Decode(packet);
				await msg.ProcessAsync(actor);
			}
			catch (Exception e)
			{
				logger.Fatal(e, "{Message}", e.Message);
			}
		}

		protected override void Disconnected(AiClient actor)
		{
			if (actor == null)
			{
				logger.Error(@"Disconnected with ai server null ???");
				return;
			}

			packetProcessor.DeselectReadPartition(actor.ReadPartition);

			if (NpcClient?.GUID.Equals(actor.GUID) == true)
			{
				NpcClient = null;
			}

			logger.Warning($"NPC Server [{actor.GUID}] has disconnected!");
		}

		public new void Close()
		{
			base.Close();
		}
		public static async Task SendAsync(MsgAiAction<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiAction<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
		public static async Task SendAsync(MsgAiRoleStatusFlag<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiRoleStatusFlag<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
		public static async Task SendAsync(MsgAiDynaMap<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiDynaMap<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
		public static async Task SendAsync(MsgAiGeneratorManage<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiGeneratorManage<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
		public static async Task SendAsync(MsgAiPlayerLogout<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiPlayerLogout<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
		public static async Task SendAsync(MsgAiPlayerLogin<AiClient> obj)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode());
			}
		}
		public static async Task SendAsync(MsgAiPlayerLogin<AiClient> obj, Func<Task> task)
		{
			if (Instance != null)
			{
				Instance.Send(NpcClient, obj.Encode(), task);
			}
		}
	}
}
