using Long.Kernel.Network.Ai;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiPing : MsgAiPing<AiClient>
    {
        public override Task ProcessAsync(AiClient client)
        {
            Data = new MsgAiPingContract()
            {
                RecvTimestamp = Environment.TickCount,
                RecvTimestampMs = Environment.TickCount64
            };      
            return client.SendAsync(this);
        }
    }
}
