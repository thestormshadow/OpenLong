using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletLogin : MsgPigletLogin<PigletActor>
    {
        public MsgPigletLogin(string userName, string password, string realmName)
            : base(userName, password, realmName)
        {            
        }
    }
}
