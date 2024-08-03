using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletUserCreditInfoEx : MsgPigletUserCreditInfoEx<PigletActor>
    {
        public override async Task ProcessAsync(PigletActor client)
        {
            Character user = RoleManager.GetUserByAccount(Data.UserIdentity);
            if (user == null)
            {
                return;
            }

            if (Data.HasFirstCreditToClaim)
            {
                await user.SetFirstCreditAsync();
            }

            // TODO check what need to be updated when crediting is detected!!!
        }
    }
}
