using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletUserCreditInfo : MsgPigletUserCreditInfo<PigletActor>
    {
        public MsgPigletUserCreditInfo(uint userId)
        {
            Data = new FirstCreditData
            {
                UserIdentity = userId,
            };
        }
    }
}
