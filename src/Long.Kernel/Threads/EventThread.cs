using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.TaskDetail;
using Long.Kernel.Scripting.Action;
using Long.Kernel.Settings;
using Quartz;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Long.Kernel.Threads
{
    [DisallowConcurrentExecution]
    public sealed class EventThread : IJob
    {
        private static readonly ILogger logger = Log.ForContext<EventThread>();

        private const int _ACTION_SYSTEM_EVENT = 2030000;
        private const int _ACTION_SYSTEM_EVENT_LIMIT = 100;

        private static readonly ConcurrentDictionary<uint, DbAction> actions;

        static EventThread()
        {
            actions = new ConcurrentDictionary<uint, DbAction>(1, _ACTION_SYSTEM_EVENT_LIMIT);

            for (var a = 0; a < _ACTION_SYSTEM_EVENT_LIMIT; a++)
            {
                DbAction action = ScriptManager.GetAction((uint)(_ACTION_SYSTEM_EVENT + a));
                if (action != null)
                {
                    actions.TryAdd(action.Id, action);
                }
            }

            if (!GameServerSettings.IsRealm)
            {
				dailyReset = DailyResetRepository.GetLatest();
                if (dailyReset != null)
                {
                    logger.Information("Latest daily reset: {0}", dailyReset.RunTime);
                }
                else
                {
                    logger.Information("No daily resets yet.");

                    dailyReset = new DbDailyReset
                    {
                        RunTime = DateTime.Now,
                    };
                }
            }
        }

        public EventThread()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (DateTime.Now.Second == 0)
            {
                foreach (DbAction action in actions.Values)
                {
                    try
                    {
                        await GameAction.ExecuteActionAsync(action.Id, null, null, null, "");
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error on processing automatic actions!! {0}", ex.Message);
                    }
                }
            }

            try
            {
                await ScriptManager.OnTimerAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            try
            {
                await KingdomManager.OnTimerAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            try
            {
                await MapManager.OnTimerAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            try
            {
                await OnEventTimerAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            try
            {
                await AuctionManager.OnTimerAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }

            await DailyResetAsync();
        }

        private static bool IsDailyResetEnabled()
        {
            if (dailyReset == null)
            {
                return false;
            }

            // server was shutdown or midnight has passed (or daily reset has not been completed, duration = 0)
            if (dailyReset.RunTime.Date < DateTime.Now.Date || dailyReset.Duration == 0)
            {
                return true;
            }
            return false;
        }

        private static DbDailyReset dailyReset;

        private static async Task DailyResetAsync()
        {
            if (!IsDailyResetEnabled())
                return;

            logger.Information($"Running daily reset tasks");

            dailyReset = new DbDailyReset
            {
                RunTime = DateTime.Now
            };
            await ServerDbContext.CreateAsync(dailyReset);

            Stopwatch sw = Stopwatch.StartNew();

            await OnDailyResetAsync();

            try
            {
                foreach (var player in RoleManager.QueryUserSet()) // do online players
                {
                    try
                    {
                        await player.DoDailyResetAsync(false);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error when reseting online user! Message: {0}", ex.Message);
                    }
                }

                await ServerDbContext.ScalarAsync($"DELETE FROM task_detail WHERE task_id IN ({string.Join(",", ITaskDetail.DailyTasks.Select(x => x.ToString()).ToArray())}) AND complete_flag > 0 LIMIT 1234567890;");
                await ServerDbContext.ScalarAsync("DELETE FROM cq_activity_user_task LIMIT 1234567890;");
                await ServerDbContext.ScalarAsync("DELETE FROM cq_statistic_daily LIMIT 1234567890;");
                if (DateTime.Now.Day == 1)
                {
                    logger.Information("Daily reset cleanup for first day of month!!!");
                    await ServerDbContext.ScalarAsync("DELETE FROM cq_sign_everyday LIMIT 1234567890;");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error when reseting online user! Message: {0}", ex.Message);
            }
            finally
            {
                sw.Stop();
                dailyReset.Duration = (ulong)sw.ElapsedMilliseconds;
                await ServerDbContext.UpdateAsync(dailyReset);

                logger.Information("Daily reset tasks finished in {0} ns {1:N3} ms", sw.ElapsedTicks, sw.Elapsed.TotalMilliseconds);
            }
        }
    }
}
