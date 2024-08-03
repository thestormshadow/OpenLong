using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiPing : MsgAiPing<GameServer>
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiPing>();

		public override Task ProcessAsync(GameServer client)
        {
            if (Data.RecvTimestamp != 0 && Data.RecvTimestampMs != 0)
            {
                int ping = (Environment.TickCount - Data.RecvTimestamp) / 2;
                long pingMs = (Environment.TickCount64 - Data.RecvTimestampMs) / 2;

                if (ping > 1000 || pingMs > 1000)
                    logger.Warning($"Inter server network lag detected! Ping: {ping}s ({pingMs}ms)");
            }
            return Task.CompletedTask;
        }
    }
}
