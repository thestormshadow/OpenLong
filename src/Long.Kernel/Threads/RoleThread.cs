using Long.Kernel.Managers;
using Long.Shared.Threads;
using Quartz;

namespace Long.Kernel.Threads
{
	[DisallowConcurrentExecution]
	public sealed class RoleThread : IJob
	{
		static RoleThread()
        {
        }

		public async Task Execute(IJobExecutionContext context)
		{
			await RoleManager.OnRoleTimerAsync();
		}
    }
}
