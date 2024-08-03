using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletPing : MsgPigletPing<PigletActor>
    {
        public override Task ProcessAsync(PigletActor client)
        {
            return Task.CompletedTask;
        }
    }
}
