using Long.Network.Security;
using Long.Network;
using Long.Network.Sockets;
using System.Net.Sockets;
using Long.Kernel.Network.Game.Packets;
using Long.Network.Packets;
using System.Drawing;
using Long.Kernel.States.User;
using Long.Kernel.States.Registration;

namespace Long.Kernel.Network.Game
{
    public class GameClient : TcpServerActor
    {
        protected GameClient(TcpServerActor actor)
            : base(actor.Socket, null, actor.Cipher)
        {
        }

        public GameClient(Socket socket, Memory<byte> buffer, uint readPartition, uint writePartition)
            : base(socket, buffer, new Cast5Cipher(), readPartition, writePartition, NetworkDefinition.GAME_SERVER_FOOTER)
        {
            NdDiffieHellman = new NDDiffieHellman();
            GUID = Guid.NewGuid();
        }

        public NDDiffieHellman NdDiffieHellman { get; set; }
        public uint Identity => Character?.Identity ?? 0;
        public uint AccountIdentity { get; set; }
        public ushort AuthorityLevel { get; set; }
        public string MacAddress { get; set; } = "Unknown";
        public int LastLogin { get; set; }
        public Guid GUID { get; }
        public AwaitingCreation Creation { get; set; }
        public Character Character { get; set; }

        public override Task SendAsync(byte[] packet)
        {
            GameServerSocket.Instance?.Send(this, packet);
            return Task.CompletedTask;
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            GameServerSocket.Instance?.Send(this, packet, task);
            return Task.CompletedTask;
        }

        public Task DisconnectWithMessageAsync(MsgConnectEx.RejectionCode rejectionCode)
        {
            return SendAsync(new MsgConnectEx(rejectionCode), () =>
            {
                Disconnect();
                return Task.CompletedTask;
            });
        }

        public Task DisconnectWithMessageAsync(string message)
        {
            return SendAsync(new MsgTalk(TalkChannel.Talk, Color.White, message), () =>
            {
                Disconnect();
                return Task.CompletedTask;
            });
        }

        public Task DisconnectWithMessageAsync(IPacket msg)
        {
            return SendAsync(msg, () =>
            {
                Disconnect();
                return Task.CompletedTask;
            });
        }
    }
}
