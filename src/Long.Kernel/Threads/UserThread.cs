using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Shared.Threads;
using Quartz;
using System.Diagnostics;

namespace Long.Kernel.Threads
{
	[DisallowConcurrentExecution]
	public sealed class UserThread : IJob
	{
		private static readonly TimeOutMS basicProcessingTimer = new();

		private double elapsedMilliseconds;
		public double ElapsedMilliseconds => elapsedMilliseconds;

		static UserThread()
		{
			basicProcessingTimer.Startup(300);
		}

		public async Task Execute(IJobExecutionContext context)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();

			Task onBattleTimerAsync(Character user) => user.OnBattleTimerAsync();
			bool nextProcessing = basicProcessingTimer.ToNextTime();
			foreach (var user in RoleManager.QueryUserSet())
			{
				if (nextProcessing)
				{
					await user.OnTimerAsync();
				}

				user.QueueAction(() => onBattleTimerAsync(user));
			}

			Interlocked.Exchange(ref elapsedMilliseconds, stopwatch.Elapsed.TotalMilliseconds);
		}
	}
}
