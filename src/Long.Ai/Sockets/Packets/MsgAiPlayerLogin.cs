using Long.Ai.Managers;
using Long.Ai.States;
using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiPlayerLogin : MsgAiPlayerLogin<GameServer>
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiPlayerLogin>();

		public override async Task ProcessAsync(GameServer client)
        {
            Character user = RoleManager.GetUser(Data.Id);
            if (user != null)
            {
                logger.Warning($"User [{Data.Id}]{Data.Name} is already signed in. Invalid Call (FlyMap??)");
                return;
            }

            user = new Character(Data.Id);
            if (!await user.InitializeAsync(this))
            {
                logger.Warning($"User [{Data.Id}]{Data.Name} could not be initialized!");
                return;
            }

            RoleManager.LoginUser(user);
#if DEBUG
            logger.Debug($"User [{Data.Id}]{Data.Name} has signed in.");
#endif
        }
    }
}
