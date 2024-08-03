using Long.Shared.Mathematics;

namespace Long.Shared
{
	public sealed class TimeOut
	{
		private int interval;
		private long updateTime;

		public TimeOut(int nInterval = 0)
		{
			interval = nInterval;
			updateTime = 0;
		}

		public long Clock()
		{
			return Environment.TickCount / 1000;
		}

		public bool Update()
		{
			updateTime = Clock();
			return true;
		}

		public bool IsTimeOut()
		{
			return Clock() >= updateTime + interval;
		}

		public bool ToNextTime()
		{
			if (IsTimeOut())
			{
				return Update();
			}

			return false;
		}

		public void SetInterval(int nSecs)
		{
			interval = nSecs;
		}

		public void Startup(int nSecs)
		{
			interval = nSecs;
			Update();
		}

		public bool TimeOver()
		{
			if (IsActive() && IsTimeOut())
			{
				return Clear();
			}

			return false;
		}

		public bool IsActive()
		{
			return updateTime != 0;
		}

		public bool Clear()
		{
			updateTime = interval = 0;
			return true;
		}

		public void IncInterval(int nSecs, int nLimit)
		{
			interval = Calculations.CutOverflow(interval + nSecs, nLimit);
		}

		public void DecInterval(int nSecs)
		{
			interval = Calculations.CutTrail(interval - nSecs, 0);
		}

		public bool IsTimeOut(int nSecs)
		{
			return Clock() >= updateTime + nSecs;
		}

		public bool ToNextTime(int nSecs)
		{
			if (IsTimeOut(nSecs))
			{
				return Update();
			}

			return false;
		}

		public bool TimeOver(int nSecs)
		{
			if (IsActive() && IsTimeOut(nSecs))
			{
				return Clear();
			}

			return false;
		}

		public bool ToNextTick(int nSecs)
		{
			if (IsTimeOut(nSecs))
			{
				if (Clock() >= updateTime + nSecs * 2)
				{
					return Update();
				}

				updateTime += nSecs;
				return true;
			}

			return false;
		}

		public int GetRemain()
		{
			return updateTime != 0
					   ? Calculations.CutRange(interval - ((int)Clock() - (int)updateTime), 0, interval)
					   : 0;
		}

		public int GetInterval()
		{
			return interval;
		}

		public static implicit operator bool(TimeOut ms)
		{
			return ms.ToNextTime();
		}
	}

	public sealed class TimeOutMS
	{
		private int interval;
		private long updateTime;

		public TimeOutMS(int nInterval = 0)
		{
			if (nInterval < 0)
			{
				nInterval = int.MaxValue;
			}

			interval = nInterval;
			updateTime = 0;
		}

		public long Clock()
		{
			return Environment.TickCount;
		}

		public bool Update()
		{
			updateTime = Clock();
			return true;
		}

		public bool IsTimeOut()
		{
			return Clock() >= updateTime + interval;
		}

		public bool ToNextTime()
		{
			if (IsTimeOut())
			{
				return Update();
			}

			return false;
		}

		public void SetInterval(int nMilliSecs)
		{
			interval = nMilliSecs;
		}

		public void Startup(int nMilliSecs)
		{
			interval = Math.Min(nMilliSecs, int.MaxValue);
			Update();
		}

		public bool TimeOver()
		{
			if (IsActive() && IsTimeOut())
			{
				return Clear();
			}

			return false;
		}

		public bool IsActive()
		{
			return updateTime != 0;
		}

		public bool Clear()
		{
			updateTime = interval = 0;
			return true;
		}

		public void IncInterval(int nMilliSecs, int nLimit)
		{
			interval = Calculations.CutOverflow(interval + nMilliSecs, nLimit);
		}

		public void DecInterval(int nMilliSecs)
		{
			interval = Calculations.CutTrail(interval - nMilliSecs, 0);
		}

		public bool IsTimeOut(int nMilliSecs)
		{
			return Clock() >= updateTime + nMilliSecs;
		}

		public bool ToNextTime(int nMilliSecs)
		{
			if (IsTimeOut(nMilliSecs))
			{
				return Update();
			}

			return false;
		}

		public bool TimeOver(int nMilliSecs)
		{
			if (IsActive() && IsTimeOut(nMilliSecs))
			{
				return Clear();
			}

			return false;
		}

		public bool ToNextTick(int nMilliSecs)
		{
			if (IsTimeOut(nMilliSecs))
			{
				if (Clock() >= updateTime + nMilliSecs * 2)
				{
					return Update();
				}

				updateTime += nMilliSecs;
				return true;
			}

			return false;
		}

		public int GetRemain()
		{
			return updateTime != 0
					   ? Calculations.CutRange(interval - ((int)Clock() - (int)updateTime), 0, interval)
					   : 0;
		}

		public int GetInterval()
		{
			return interval;
		}

		public static implicit operator bool(TimeOutMS ms)
		{
			return ms.ToNextTime();
		}
	}
}
