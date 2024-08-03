using Long.Game.Network.Ai.Packets;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Network.Packets.Ai;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgWalk : MsgProtoBufBase<GameClient, MsgWalk.WalkData>
    {
        public MsgWalk()
            : base(PacketType.MsgWalk)
        {
        }

        [ProtoContract]
        public struct WalkData
        {
            [ProtoMember(1, IsRequired = true)]
            public uint Direction { get; set; }
            [ProtoMember(2, IsRequired = true)]
            public uint Identity { get; set; }
            [ProtoMember(3, IsRequired = true)]
            public uint Mode { get; set; }
            [ProtoMember(4, IsRequired = true)]
            public uint Timestamp { get; set; }
            [ProtoMember(5, IsRequired = true)]
            public uint Map { get; set; }
        }

        public enum RoleMoveMode
        {
            Walk = 0,

            // PathMove()
            Run,
            Shift,

            // to server only
            Jump,
            Trans,
            Chgmap,
            JumpMagicAttack,
            Collide,
            Synchro,

            // to server only
            Track,

            RunDir0 = 20,

            RunDir7 = 27
        }

        /// <summary>
        ///     Process can be invoked by a packet after decode has been called to structure
        ///     packet fields and properties. For the server implementations, this is called
        ///     in the packet handler after the message has been dequeued from the server's
        ///     <see cref="PacketProcessor{TClient}" />.
        /// </summary>
        /// <param name="client">Client requesting packet processing</param>
        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                await user.SendCrossMsgAsync(this);
                return;
            }

            if (client != null && Data.Identity == user.Identity)
            {
                await user.ProcessOnMoveAsync();

                bool moved = await user.MoveTowardAsync((int)Data.Direction, (int)Data.Mode);
                if (moved)
                {
                    await user.SendAsync(this);
                    await user.Screen.UpdateAsync(this);
                    await user.ProcessAfterMoveAsync();
					MsgAiAction action1 = new MsgAiAction
					{
						Data = new MsgAiActionContract 
                        {
							Action = AiActionType.Walk,
							Identity = user.Identity,
							X = user.X,
							Y = user.Y,
							Direction = (int)user.Direction
						}
					};
					NpcServer.Instance.Send(NpcServer.NpcClient, action1.Encode());
                }
                return;
            }

            Role target = RoleManager.GetRole(Data.Identity);
            if (target == null)
            {
                return;
            }

            await target.ProcessOnMoveAsync();
            await target.MoveTowardAsync((int)Data.Direction, (int)Data.Mode);
            if (target is Character targetUser)
            {
                await targetUser.Screen.UpdateAsync(this);
            }
            else
            {
                await target.BroadcastRoomMsgAsync(this, false);
            }
            await target.ProcessAfterMoveAsync();
			MsgAiAction action2 = new MsgAiAction
			{
				Data = new MsgAiActionContract
				{
					Action = AiActionType.Jump,
					Identity = target.Identity,
					X = target.X,
					Y = target.Y,
					Direction = (int)target.Direction
				}
			};
			NpcServer.Instance.Send(NpcServer.NpcClient, action2.Encode());
		}
    }
}
