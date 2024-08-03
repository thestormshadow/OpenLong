using Long.Kernel.Managers;
using Long.Network.Packets.Cross;

namespace Long.Kernel.Network.Cross.Client.Packets
{
    public sealed class MsgCrossTrainingVitalityInfoC : MsgCrossTrainingVitalityInfo<CrossClientActor>
    {
        public override Task ProcessAsync(CrossClientActor client)
        {
            return RealmManager.ReceiveFateDataAsync(Data, client);
        }
    }
}
