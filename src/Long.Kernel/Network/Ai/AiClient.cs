using Long.Network.Security;
using Long.Network.Sockets;
using Long.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Network.Ai
{
	public sealed class AiClient : TcpServerActor
	{
		public AiClient(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition)
			: base(socket, buffer, null, readPartition, writePartition, NetworkDefinition.NPC_FOOTER)
		{
			GUID = Guid.NewGuid().ToString();
			DiffieHellman = DiffieHellman.Create();
		}

		public DiffieHellman DiffieHellman { get; }

		public ConnectionStage Stage { get; set; }
		public string GUID { get; }

		public NetworkMonitor NetworkMonitor { get; init; } = new NetworkMonitor();
		public override Task SendAsync(byte[] packet)
		{
			NetworkMonitor.Send(packet.Length);
			NpcServer.Instance.Send(this, packet);
			return Task.CompletedTask;
		}

		public override Task SendAsync(byte[] packet, Func<Task> task)
		{
			NetworkMonitor.Send(packet.Length);
			NpcServer.Instance.Send(this, packet, task);
			return Task.CompletedTask;
		}

		public enum ConnectionStage
		{
			AwaitingAuth,
			Authenticated
		}
	}
}
