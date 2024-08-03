using Long.Kernel.Managers;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletRealmAnnounceMaintenance : MsgPigletRealmAnnounceMaintenance<PigletActor>
    {
        public override Task ProcessAsync(PigletActor client)
        {
            return MaintenanceManager.AnnounceMaintenanceAsync(Data.WarningMinutes);
        }
    }
}
