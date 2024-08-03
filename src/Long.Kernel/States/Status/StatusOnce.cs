using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;

namespace Long.Kernel.States.Status
{
	public sealed class StatusOnce : IStatus
	{
		private static readonly ILogger logger = Log.ForContext<StatusOnce>();

		private Role owner;
		private TimeOutMS keep;
		private long autoFlash;
		private TimeOutMS interval;

		public StatusOnce()
		{
			owner = null;
			Identity = 0;
		}

		public StatusOnce(Role pOwner)
		{
			owner = pOwner;
			Identity = 0;
		}

		public int Identity { get; private set; }

		public bool IsValid => keep.IsActive() && !keep.IsTimeOut();

		public int Power { get; set; }

		public DbStatus Model { get; set; }

		public byte Level => Magic?.Level ?? 0;

		public int RemainingTimes => 0;

		public int Time => keep.GetInterval();

		public int RemainingTime => keep.GetRemain() / 1000;

		public bool GetInfo(ref StatusInfoStruct info)
		{
			info.Power = Power;
			info.Seconds = keep.GetRemain() / 1000;
			info.Status = Identity;
			info.Times = 0;

			return IsValid;
		}

		public async Task<bool> ChangeDataAsync(int power, int secs, int times = 0, uint caster = 0U)
		{
			try
			{
				Power = power;
				keep.SetInterval((int)Math.Min(int.MaxValue, (long)secs * 1000));
				keep.Update();

				await StatusSet.SubmitStatusDataAsync(this, owner);

				if (Model != null)
				{
					Model.Power = power;
					Model.IntervalTime = (uint)secs;
					Model.EndTime = (uint)DateTime.Now.AddSeconds(secs).ToUnixTimestamp();
					await ServerDbContext.UpdateAsync(Model);
				}

				if (caster != 0)
				{
					CasterId = caster;
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
			if (!IsValid)
			{
				return;
			}

			try
			{
				switch (Identity)
				{
					case StatusSet.LUCKY_DIFFUSE:
						{
							if (owner is not Character user)
								return;

							if (!interval.ToNextTime(1000))
								return;

							await user.ChangeLuckyTimerAsync(3);
							break;
						}
					case StatusSet.LUCKY_ABSORB:
						{
							if (owner is not Character user)
								return;

							if (!interval.ToNextTime(1000))
								return;

							Role sender = user.QueryRole(CasterId);
							if (sender == null || sender.GetDistance(user) > 3)
							{
								keep.Clear(); // cancel
								return;
							}

							await user.ChangeLuckyTimerAsync(1);
							break;
						}
				}
			}
			catch (Exception ex)
			{
				logger.Fatal(ex, "StatusOnce.OnTimerAsync: {Message}", ex.Message);
			}
		}

		public async Task<bool> CreateAsync(Role role, int status, int power, int secs, int times, uint caster = 0,
											Magic magic = null, bool save = false)
		{
			owner = role;
			CasterId = caster;
			Identity = status;
			Power = power;
			keep = new TimeOutMS(Math.Min(int.MaxValue, secs * 1000));
			keep.Startup((int)Math.Min((long)secs * 1000, int.MaxValue));
			keep.Update();
			interval = new TimeOutMS(1000);
			interval.Update();
			Magic = magic;

			if (save && owner is Character)
			{
				Model = new DbStatus
				{
					Status = (uint)status,
					Power = power,
					IntervalTime = (uint)secs,
					LeaveTimes = 0,
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
