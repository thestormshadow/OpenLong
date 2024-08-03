using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossMagicInfoC : MsgCrossMagicInfo<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
        {
            return RealmManager.ReceiveMagicUserDataAsync(Data, client);
        }
    }
}
