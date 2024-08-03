using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossOwnKongFuInfoC : MsgCrossOwnKongFuInfo<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
        {
            return RealmManager.ReceiveJiangHuDataAsync(Data, client);
        }
    }
}
