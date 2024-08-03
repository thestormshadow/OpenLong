using Long.Login.States;
using Long.Network;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Login.Network.Game
{
    public sealed class GameClient : TcpServerActor
    {
        private static ServerSettings.SocketListenerSettings settings;

        static GameClient()
        {
            settings = ServerSettings.Instance.Game;
        }

        public GameClient(Socket socket, Memory<byte> buffer, uint readPartition = 0, uint writePartition = 0) 
            : base(socket, buffer, AesCipher.Create(settings.Encryption.Key, settings.Encryption.EncryptIV, settings.Encryption.DecryptIV), readPartition, writePartition, NetworkDefinition.ACCOUNT_FOOTER)
        {
            DiffieHellman = DiffieHellman.Create();
        }

        public Realm Realm { get; set; }
        public DiffieHellman DiffieHellman { get; }
        public uint LastPing { get; set; }

        public override Task SendAsync(byte[] packet)
        {
            GameServerSocket.Instance.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            GameServerSocket.Instance.Send(this, packet, task);
            return Task.CompletedTask;
        }        
    }
}
