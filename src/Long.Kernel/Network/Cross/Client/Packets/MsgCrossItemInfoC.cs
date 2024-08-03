using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossItemInfoC : MsgCrossItemInfo<CrossClientActor>
    {
        public override async Task ProcessAsync(CrossClientActor client)
        {
            await RealmManager.ReceiveItemDataAsync(Data, client);
        }
    }
}
