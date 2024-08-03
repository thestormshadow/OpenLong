using Long.Kernel.Network.Game;
using Long.Network.Sockets;

namespace Long.Kernel.Network.Cross
{
    public sealed class CrossGameClient : GameClient
    {
        private readonly TcpServerActor actor;

        public CrossGameClient(TcpServerActor actor)
            : base(actor)
        {
            this.actor = actor;
        }

        public override Task SendAsync(byte[] packet)
        {
            return actor.SendAsync(packet);
        }

        public override Task SendAsync(byte[] packet, Func<Task> task)
        {
            return actor.SendAsync(packet);
        }
    }
}