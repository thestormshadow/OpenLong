using Long.Ai.Managers;
using Long.Ai.States;
using Long.Network.Packets;
using Long.Network.Packets.Ai;
using ProtoBuf;
using Serilog;
using static Long.Ai.Sockets.Packets.MsgAiAction;

namespace Long.Ai.Sockets.Packets
{
	public sealed class MsgAiAction : MsgAiAction<GameServer>
	{
        private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiAction>();

		public override async Task ProcessAsync(GameServer client)
		{
			switch (Data.Action)
			{
				case AiActionType.LeaveMap:
					{
						Role target = RoleManager.GetRole(Data.Identity);
						if (target?.Map == null)
							return;

						target.QueueAction(async () =>
						{
#if DEBUG
							logger.Debug($"Target '{target.Name}' LeaveMap {target.MapIdentity},{target.X},{target.Y}");
#endif
							await target.LeaveMapAsync();
						});
						break;
					}

				case AiActionType.FlyMap:
					{
						Role target = RoleManager.GetRole(Data.Identity);
						if (target == null) return;
						target.QueueAction(async () =>
						{
#if DEBUG
							logger.Debug($"Target '{target.Name}' FlyMap {target.MapIdentity},{target.X},{target.Y}=>{Data.TargetIdentity},{Data.X},{Data.Y}");
#endif
							await target.LeaveMapAsync(); // redundant? needed probably
							target.MapIdentity = Data.TargetIdentity;
							target.X = Data.X;
							target.Y = Data.Y;
							await target.EnterMapAsync();
						});
						break;
					}

				case AiActionType.Walk:
				case AiActionType.Run:
				case AiActionType.Jump:
				case AiActionType.SynchroPosition:
					{
						Role target = RoleManager.GetRole(Data.Identity);
						if (target?.Map == null)
							return;

						target.QueueAction(() =>
						{
							if (Data.Action == AiActionType.Walk
								|| Data.Action == AiActionType.Run)
							{
								target.MoveToward((int)Data.Direction, 0);
							}
							else
							{
								target.JumpPos(Data.X, Data.Y);
							}

							if (Data.Action != AiActionType.SynchroPosition && target is Character user)
							{
								user.ClearProtection();
							}
							return Task.CompletedTask;
						});
						break;
					}

				case AiActionType.SetProtection:
					{
						Role target = RoleManager.GetRole(Data.Identity);
						if (target?.Map == null)
							return;

						if (target is Character user)
						{
							user.SetProtection();
						}
						break;
					}

				case AiActionType.ClearProtection:
					{
						Role target = RoleManager.GetRole(Data.Identity);
						if (target?.Map == null)
							return;

						if (target is Character user)
						{
							user.ClearProtection();
						}
						break;
					}

				case AiActionType.Shutdown:
					{
						logger.Information($"Closing server due to game server shutdown request!!!");
						logger.Information($"Closing server due to game server shutdown request!!!");
						logger.Information($"Closing server due to game server shutdown request!!!");

						await Kernel.StopAsync();
						Environment.Exit(0);
						break;
					}
			}

		}

	}	
}
