using Long.Network;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Piglet
{
    public sealed class PigletActor : TcpServerActor
	{
        private static readonly string AesKey = "sA%Bc#O(Hj&jibPbzD5W6&EeHsfbft!f";
        private static readonly string AesEncryptionIv = "l0h5*2RKy&Dop*PF";
        private static readonly string AesDecryptionIv = "l0h5*2RKy&Dop*PF";

        public PigletActor(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition)
            : base(socket, buffer, AesCipher.Create(AesKey, AesEncryptionIv, AesDecryptionIv), readPartition, writePartition, NetworkDefinition.GM_TOOLS_FOOTER)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public DiffieHellman DiffieHellman { get; }

        public override Task SendAsync(byte[] packet)
        {
			PigletClient.Instance.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            PigletClient.Instance.Send(this, packet, task);
            return Task.CompletedTask;
        }
    }
}
