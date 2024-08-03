using Long.Login.Managers;
using Long.Login.Network.Login.Packets;
using Long.Network.Packets.Game;
using Long.Network.Security;
using Long.Network.Sockets;
using System.Net.Sockets;

namespace Long.Login.Network.Login
{
    public sealed class LoginClient : TcpServerActor
    {
        public LoginClient(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition) 
            : base(socket, buffer, new TQCipher(), readPartition, writePartition)
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; }
        public uint AccountID { get; set; }
        public string Username { get; set; }
        public uint Seed { get; set; }

        public override Task SendAsync(byte[] packet)
        {
            LoginServerSocket.Instance.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            LoginServerSocket.Instance.Send(this, packet);
            return Task.CompletedTask;
        }

        public Task DisconnectWithRejectionCodeAsync(MsgConnectEx<LoginClient>.RejectionCode rejectionCode)
        {
            LoginStatisticManager.IncreaseErrorLogin();
            return SendAsync(new MsgConnectEx(rejectionCode), () =>
            {
                Disconnect();
                return Task.CompletedTask;
            });
        }
    }
}
