using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossRequestSwitchC : MsgCrossRequestSwitch<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
        {
            // this reply means we can transfer the user. It will not notify the client if it fails.
            Character user = RoleManager.GetUser(Data.UserId);
            if (user == null)
            {
                // but if we have a success transfer, we must cancel it.
                return Task.CompletedTask;
            }

            return RealmManager.ReceiveSwitchRequestAsync(Data, client);
        }
    }
}
