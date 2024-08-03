using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public class MsgPigletRealmStatus : MsgPigletRealmStatus<PigletActor>
    {
        public MsgPigletRealmStatus()
        {
            serializeWithHeaders = true;
        }
    }
}
