using Long.Kernel.Settings;
using Long.Network;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Kernel.Network.Login
{
    public sealed class LoginServer : TcpServerActor
    {
        private static GameServerSettings.LoginClient settings;

        static LoginServer()
        {
            settings = GameServerSettings.Instance.Login;
        }

        public LoginServer(Socket socket, Memory<byte> buffer, uint readPartition = 0, uint writePartition = 0)
            : base(socket, buffer, AesCipher.Create(settings.Encryption.Key, settings.Encryption.EncryptIV, settings.Encryption.DecryptIV), readPartition, writePartition, NetworkDefinition.ACCOUNT_FOOTER)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public DiffieHellman DiffieHellman { get; }

        public uint LastPing { get; set; }

        public override Task SendAsync(byte[] packet)
        {
            LoginClientSocket.Instance?.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            LoginClientSocket.Instance?.Send(this, packet, task);
            return Task.CompletedTask;
        }
    }
}
