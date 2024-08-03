using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;
using Long.Shared.Mathematics;

namespace Long.Kernel.States.Status
{
	public sealed class StatusMore : IStatus
	{
		private static readonly ILogger logger = Log.ForContext<StatusMore>();

		private Role owner;
		private TimeOut keep;
		private long autoFlash;

		public StatusMore()
		{
			owner = null;
			Identity = 0;
		}

		public StatusMore(Role owner)
		{
			this.owner = owner;
			Identity = 0;
		}

		public int Identity { get; private set; }

		public bool IsValid => RemainingTimes > 0;

		public int Power { get; set; }

		public DbStatus Model { get; set; }

		public byte Level => Magic?.Level ?? 0;

		public int RemainingTimes { get; private set; }

		public int RemainingTime => keep.GetRemain();

		public int Time => keep.GetInterval();

		public bool GetInfo(ref StatusInfoStruct info)
		{
			info.Power = Power;
			info.Seconds = keep.GetRemain();
			info.Status = Identity;
			info.Times = RemainingTimes;

			return IsValid;
		}

		public async Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0U)
		{
			try
			{
				Power = power;
				keep.SetInterval(secs);
				keep.Update();
				CasterId = caster;

				if (times > 0)
				{
					RemainingTimes = times;
				}

				if (Model != null)
				{
					Model.Power = power;
					Model.LeaveTimes = (uint)times;
					Model.IntervalTime = (uint)secs;
					Model.EndTime = (uint)DateTime.Now.AddSeconds(secs).ToUnixTimestamp();
					await ServerDbContext.UpdateAsync(Model);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool IncTime(int ms, int limit)
		{
			int nInterval = Math.Min(ms + keep.GetRemain(), limit);
			keep.SetInterval(nInterval);
			return keep.Update();
		}

		public bool ToFlash()
		{
			if (!IsValid)
			{
				return false;
			}

			if (autoFlash == 0 && keep.GetRemain() <= 5000)
			{
				autoFlash = 1;
				return true;
			}

			return false;
		}

		public uint CasterId { get; private set; }

		public bool IsUserCast => CasterId == owner.Identity || CasterId == 0;

		public Magic Magic { get; private set; }

		public async Task OnTimerAsync()
		{
			try
			{
				if (!IsValid || !keep.ToNextTime())
				{
					return;
				}

				if (owner != null)
				{
					var user = owner as Character;
					var currentEvent = user?.GetCurrentEvent();
					var loseLife = 0;
					switch (Identity)
					{
						case StatusSet.POISONED: // poison
							{
								if (!owner.IsAlive)
									return;

								loseLife = (int)Calculations.CutOverflow(200, owner.Life - 1);

								if (currentEvent != null)
								{
									Role attacker = RoleManager.GetRole(CasterId);
									await currentEvent.OnBeAttackAsync(attacker, owner, (int)Math.Min(owner.Life, loseLife));
								}

								if (loseLife > 0)
								{
									await owner.AddAttributesAsync(ClientUpdateType.Hitpoints, loseLife * -1);

									if (user != null)
									{
										await user.BroadcastTeamLifeAsync();
									}
								}

								var msg = new MsgMagicEffect
								{
									AttackerIdentity = owner.Identity,
									MagicIdentity = MagicData.POISON_MAGIC_TYPE
								};
								msg.Append(owner.Identity, loseLife, true);
								await owner.BroadcastRoomMsgAsync(msg, true);

								if (!owner.IsAlive)
									await owner.BeKillAsync(null);
								break;
							}

						case StatusSet.TOXIC_FOG:
							{
								if (!owner.IsAlive)
								{
									RemainingTimes = 1;
									break;
								}

								loseLife = Calculations.AdjustData((int)owner.Life, Power);
								if (owner.Life - loseLife <= 0)
									loseLife = 0;

								int percent = 100 - Math.Max(0, Math.Min(100, owner.Detoxication));
								loseLife = (int)(loseLife * percent / 100d);

								if (currentEvent != null)
								{
									Role attacker = RoleManager.GetRole(CasterId);
									await currentEvent.OnBeAttackAsync(attacker, owner, (int)Math.Min(owner.Life, loseLife));
								}

								if (loseLife > 0)
								{
									await owner.AddAttributesAsync(ClientUpdateType.Hitpoints, loseLife * -1);

									if (user != null)
									{
										await user.BroadcastTeamLifeAsync();
									}
								}

								var msg = new MsgMagicEffect
								{
									AttackerIdentity = owner.Identity,
									MagicIdentity = MagicData.POISON_MAGIC_TYPE
								};
								msg.Append(owner.Identity, loseLife, true);
								await owner.Map.BroadcastRoomMsgAsync(owner.X, owner.Y, msg);
								break;
							}

						case StatusSet.SHURIKEN_VORTEX:
							{
								if (!owner.IsAlive)
								{
									RemainingTimes = 1;
									break;
								}

								await owner.ProcessMagicAttackAsync(6010, 0, owner.X, owner.Y);
								break;
							}

						case StatusSet.DRAGON_FLOW:
							{
								if (!owner.IsAlive || Magic == null)
								{
									RemainingTimes = 1;
									break;
								}

								if (user != null)
								{
									await user.AddAttributesAsync(ClientUpdateType.Stamina, Magic.Power);
								}
								break;
							}
					}

					RemainingTimes--;
				}
			}
			catch (Exception ex)
			{
				logger.Fatal(ex, "StatusMore.OnTimerAsync: {Message}", ex.Message);
			}
		}

		~StatusMore()
		{
			// todo destroy and detach status
		}

		public async Task<bool> CreateAsync(Role role, int status, int power, int secs, int times, uint caster = 0,
											Magic magic = null, bool save = false)
		{
			owner = role;
			Identity = status;
			Power = power;
			keep = new TimeOut(secs);
			keep.Update(); // no instant start
			RemainingTimes = times;
			CasterId = caster;
			Magic = magic;

			if (save && owner is Character)
			{
				Model = new DbStatus
				{
					Status = (uint)status,
					Power = power,
					IntervalTime = (uint)secs,
					LeaveTimes = (uint)times,
					RemainTime = (uint)secs,
					EndTime = (uint)DateTime.Now.AddSeconds(secs).ToUnixTimestamp(),
					OwnerId = owner.Identity,
					Sort = 0
				};
				await ServerDbContext.UpdateAsync(Model);
			}

			return true;
		}
	}
}
