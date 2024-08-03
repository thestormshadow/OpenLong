using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletClaimFirstCredit : MsgPigletClaimFirstCredit<PigletActor>
    {
        public MsgPigletClaimFirstCredit(uint accountId)
        {
            Data = new ClaimFirstCreditData
            {
                AccountId = accountId
            };
        }
    }
}
