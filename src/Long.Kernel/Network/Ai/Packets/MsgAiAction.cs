using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.States;
using Long.Network.Packets.Ai;
using Serilog.Context;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiAction : MsgAiAction<AiClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAiAction>();

        public override async Task ProcessAsync(AiClient client)
        {
            Role role = RoleManager.GetRole(Data.Identity);
            if (role == null || role.Map == null)
            {
                return;
            }

            if (!role.HasGenerator)
            {
                logger.Warning($"{role.Identity} - {role.Name} AI Jump request not AI Generated");
                return;
            }

            switch (Data.Action)
            {
                case AiActionType.Run:
                case AiActionType.Walk:
                    {
                        if (!role.IsAlive)
                        {
                            return;
                        }

                        role.QueueAction(() => role.MoveTowardAsync(Data.Direction, (int)Data.TargetIdentity, true));
                        break;
                    }

                case AiActionType.Jump:
                    {
                        if (!role.IsAlive)
                        {
                            return;
                        }

                        role.QueueAction(() => role.JumpPosAsync(Data.X, Data.Y, true));
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}
