using Long.Network.Packets.Game;

namespace Long.Login.Network.Login.Packets
{
    public sealed class MsgConnectEx : MsgConnectEx<LoginClient>
    {
        public MsgConnectEx(RejectionCode rejectionCode) 
            : base(rejectionCode)
        {
        }

        public MsgConnectEx(string ipAddress, uint port, ulong token) 
            : base(ipAddress, port, token)
        {
        }
    }
}
