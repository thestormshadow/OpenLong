using Long.Kernel.Managers;
using Long.Kernel.Processors;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossRequestSwitchS : MsgCrossRequestSwitch<CrossServerActor>
    {
        public override Task ProcessAsync(CrossServerActor client)
        {
            return RealmManager.ReceiveSwitchRequestAsync(Data, client);
        }
    }
}
