using Long.Kernel.Managers;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletShutdown : MsgPigletShutdown<PigletActor>
    {
        public override Task ProcessAsync(PigletActor client)
        {
            if (Data.Id == int.MaxValue)
            {
                return MaintenanceManager.CloseServerAsync();
            }
            return Task.CompletedTask;
        }
    }
}
