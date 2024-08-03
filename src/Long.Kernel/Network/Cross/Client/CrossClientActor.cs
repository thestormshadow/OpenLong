using Long.Kernel.Settings;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Cross.Client
{
    public sealed class CrossClientActor : TcpServerActor
    {
        public CrossClientActor(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition, string packetFooter)
            : base(socket, buffer, AesCipher.Create(
                GameServerSettings.Instance.CrossCipher.Key,
                GameServerSettings.Instance.CrossCipher.EncryptIV,
                GameServerSettings.Instance.CrossCipher.DecryptIV
                ), readPartition, writePartition, packetFooter)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public uint ServerId => Session.ServerID;
        public DiffieHellman DiffieHellman { get; }
        public CrossClientSession Session { get; init; }

        public override Task SendAsync(byte[] packet)
        {
            Session.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            Session.Send(this, packet, task);
            return Task.CompletedTask;
        }
    }
}
