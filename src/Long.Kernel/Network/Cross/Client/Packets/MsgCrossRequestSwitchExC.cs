using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossRequestSwitchExC : MsgCrossRequestSwitchEx<CrossClientActor>
    {
        public override async Task ProcessAsync(CrossClientActor client)
        {
            var user = RoleManager.GetUser(Data.UserId);
            if (user == null)
            {
                return;
            }
            await user.EnterServerAsync(Data.ServerId, Data);
        }
    }
}
