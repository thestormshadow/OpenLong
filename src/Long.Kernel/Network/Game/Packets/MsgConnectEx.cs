using Long.Network.Packets.Game;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgConnectEx : MsgConnectEx<GameClient>
    {
        public MsgConnectEx(RejectionCode rejectionCode) : base(rejectionCode)
        {
        }

        public MsgConnectEx(string ipAddress, uint port, ulong token) : base(ipAddress, port, token)
        {
        }
    }
}
