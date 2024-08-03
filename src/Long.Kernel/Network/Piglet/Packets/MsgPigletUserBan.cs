using Long.Kernel.Managers;
using Long.Network.Packets.Piglet;

namespace Long.Kernel.Network.Piglet.Packets
{
    public sealed class MsgPigletUserBan : MsgPigletUserBan<PigletActor>
    {
        private static readonly ILogger logger = Log.ForContext<MsgPigletUserBan>();

        public override async Task ProcessAsync(PigletActor client)
        {
            var user = RoleManager.GetUser(Data.UserId);
            if (user == null)
            {
                user = RoleManager.GetUserByAccount(Data.UserId);
                if (user == null)
                {
                    return;
                }
            }

            logger.Information($"GM[{Data.GameMaster}] has banned [{user.Identity},{user.Name}]. Reason: {Data.Reason}");
            await RoleManager.KickOutAsync(user.Identity, StrYouHaveBeenBanned);
        }
    }
}