using Long.Kernel.Settings;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Cross.Server
{
    public sealed class CrossServerActor : TcpServerActor
    {
        public CrossServerActor(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition, string packetFooter)
            : base(socket, buffer, AesCipher.Create(
                GameServerSettings.Instance.Cross.Encryption.Key,
                GameServerSettings.Instance.Cross.Encryption.EncryptIV,
                GameServerSettings.Instance.Cross.Encryption.DecryptIV
                ), readPartition, writePartition, packetFooter)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public uint ServerId { get; set; }
        public DiffieHellman DiffieHellman { get; }

        public override Task SendAsync(byte[] packet)
        {
            CrossServerListener.Instance.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            CrossServerListener.Instance.Send(this, packet);
            return Task.CompletedTask;
        }
    }
}
