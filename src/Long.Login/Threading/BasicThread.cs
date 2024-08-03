using Long.Login.Managers;
using Quartz;

namespace Long.Login.Threading
{
    [DisallowConcurrentExecution]
    public sealed class BasicThread : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string version = Program.Version.Split('+')[0];
            Console.Title = $"[{version}] Conquer Online Login Server - {LoginStatisticManager.ToTitleString()} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            DateTime now = DateTime.Now;
            if (now.Minute % 5 == 0 && now.Second == 0)
            {
                LoginStatisticManager.Reset();
            }

            await UserManager.OnTimerAsync();
        }
    }
}
