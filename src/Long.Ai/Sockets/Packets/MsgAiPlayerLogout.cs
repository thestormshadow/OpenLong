
using Long.Ai.Managers;
using Long.Ai.States;
using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiPlayerLogout : MsgAiPlayerLogout<GameServer>
	{
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiPlayerLogout>();

		public override async Task ProcessAsync(GameServer client)
		{
            if (!RoleManager.LogoutUser(Data.Id, out Character user))
                return;

            await user.LeaveMapAsync();

#if DEBUG
            logger.Information($"User [{Data.Id}]{user.Name} has signed out.");
#endif
        }
    }
}
