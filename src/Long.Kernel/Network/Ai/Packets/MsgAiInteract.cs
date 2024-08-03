using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiInteract : MsgAiInteract<AiClient>
    {
        public override async Task ProcessAsync(AiClient client)
        {
			Role attacker = RoleManager.GetRole(Data.Identity);
            if (attacker == null || !attacker.IsAlive)
            {
                return;
            }

            switch (Data.Action)
            {
                case AiInteractAction.Attack:
                    {
                        attacker.BattleSystem.CreateBattle(Data.TargetIdentity);
                        break;
                    }

                case AiInteractAction.MagicAttack:
                    {
                        if (attacker is Monster monster && monster.SpeciesType != 0)
                        {
                            await attacker.BroadcastRoomMsgAsync(new MsgInteract
                            {
                                SenderIdentity = attacker.Identity,
                                MagicType = Data.MagicType,
                                Action = MsgInteract.MsgInteractType.AnnounceAttack,
                                PosX = attacker.X,
                                PosY = attacker.Y
                            }, false);
                        }

                        await attacker.ProcessMagicAttackAsync(Data.MagicType, Data.TargetIdentity, (ushort)Data.X, (ushort)Data.Y);
                        break;
                    }

                case AiInteractAction.MagicAttackWarning:
                    {
                        await attacker.BroadcastRoomMsgAsync(new MsgInteract
                        {
                            SenderIdentity = attacker.Identity,
                            TargetIdentity = Data.TargetIdentity,
                            MagicType = Data.MagicType,
                            Action = MsgInteract.MsgInteractType.AnnounceAttack,
                            PosX = attacker.X,
                            PosY = attacker.Y
                        }, false);
                        break;
                    }
            }
        }
    }
}
