using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Server.Packets
{
    public sealed class MsgCrossMagicInfoS : MsgCrossMagicInfo<CrossServerActor>
    {
        public override Task ProcessAsync(CrossServerActor client)
        {
            return RealmManager.ReceiveMagicUserDataAsync(Data, client);
        }
    }
}
