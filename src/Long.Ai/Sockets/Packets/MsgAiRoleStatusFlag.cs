using Long.Ai.Managers;
using Long.Ai.States;
using Long.Network.Packets.Ai;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiRoleStatusFlag : MsgAiRoleStatusFlag<GameServer>
    {
        public override async Task ProcessAsync(GameServer client)
        {
            Role target = RoleManager.GetRole(Data.Identity);
            if (target == null)
                return;

            Role sender = RoleManager.GetRole(Data.Caster);
            if (Data.Mode == 0)
                await target.AttachStatusAsync(sender, Data.Flag, 0, Data.Duration, Data.Steps, 0);
            else
                await target.DetachStatusAsync(Data.Flag);
        }
    }
}
