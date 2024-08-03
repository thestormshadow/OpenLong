using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States;
using Long.Kernel.States.Events;
using Long.Kernel.States.Events.Interfaces;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Long.Kernel.Managers;
public class EventManager
{
    private static readonly ILogger logger = Log.ForContext<EventManager>();

    private static readonly ConcurrentDictionary<uint, DbAction> actions = new();
    private static readonly ConcurrentDictionary<uint, DbTask> tasks = new();
    private static readonly ConcurrentDictionary<GameEvent.EventType, GameEvent> events = new();
    private static readonly List<QueuedAction> queuedActions = new();
    private static readonly ConcurrentDictionary<uint, DbConfig> inviteTrans = new();

    private static ConcurrentDictionary<int, List<uint>> inviteTransInvitations = new();

    private static readonly TimeOut rankingBroadcast = new(10);

    private static DbDailyReset dailyReset;

    public static async Task<bool> InitializeAsync()
    {
        logger.Information("Loading actions and tasks");

        await LoadActionsAsync();

        logger.Information("Loading NPCs");

        foreach (DbNpc dbNpc in await NpcRepository.GetAsync())
        {
            Npc npc = new Npc(dbNpc);

            if (!await npc.InitializeAsync())
            {
                logger.Warning($"Could not load NPC {dbNpc.Id} {dbNpc.Name}");
                continue;
            }

            await npc.EnterMapAsync();
            if (npc.Task0 != 0 && !tasks.ContainsKey(npc.Task0))
                logger.Warning($"Npc {npc.Identity} {npc.Name} no task found [taskid: {npc.Task0}]");
        }

        foreach (DbDynanpc dbDynaNpc in await DynamicNpcRespository.GetAsync())
        {
            try
            {
                DynamicNpc npc = new DynamicNpc(dbDynaNpc);
                if (!await npc.InitializeAsync())
                {
                    logger.Warning($"Could not load NPC {dbDynaNpc.Id} {dbDynaNpc.Name}");
                    continue;
                }

                if (npc.Type == 31)
                {
                    logger.Debug($"[{npc.Identity}] {npc.Name} {npc.Type}");
                }

                await npc.EnterMapAsync();
                if (npc.Task0 != 0 && !tasks.ContainsKey(npc.Task0))
                    logger.Warning($"Npc {npc.Identity} {npc.Name} no task found [taskid: {npc.Task0}]");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error initializing NPC({},{})! Message: {}", dbDynaNpc.Id, dbDynaNpc.Name, ex.Message);
            }
        }

        var bulletinInvitation = await ConfigRepository.GetAsync(6007);
        foreach (var inv in bulletinInvitation)
        {
            inviteTrans.TryAdd((uint)inv.Data1, inv);
        }

        try
        {
            await LuaScriptManager.InitializeAsync();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "Failed to load LUA Scripts!!! {}", ex.Message);
        }

//        await RegisterEventAsync(new QuizShow());
//        await RegisterEventAsync(new FamilyWar());
//        await RegisterEventAsync(new CaptureTheFlag());
//        await RegisterEventAsync(new HorseRacing());

//#if DEBUG
//        await RegisterEventAsync(new LineSkillPK());
//        await RegisterEventAsync(new ArenaQualifier());
//        await RegisterEventAsync(new ElitePkTournament());
//        await RegisterEventAsync(new TeamArenaQualifier());
//#endif

        dailyReset = await DailyResetRepository.GetLatestAsync();
        if (dailyReset != null)
        {
            logger.Information($"Latest daily reset: {dailyReset.RunTime}");
        }
        else
        {
            logger.Information("No daily resets yet.");
        }
        return true;
    }

    public static async Task LoadActionsAsync()
    {
        tasks.Clear();
        foreach (DbTask task in await TaskRepository.GetAsync()) tasks.TryAdd(task.Id, task);

        logger.Information("Loaded {Tasks} tasks from database", tasks.Count);

        actions.Clear();

        Dictionary<string, string> actionRes = new Dictionary<string, string>();
        string fileName = Path.Combine(Environment.CurrentDirectory, "ini", "action_Res.ini");
        if (File.Exists(fileName))
        {
            using StreamReader reader = new StreamReader(fileName);
            string row;
            while ((row = reader.ReadLine()) != null)
            {
                string[] kvp = row.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (kvp.Length > 1)
                {
                    if (actionRes.ContainsKey(kvp[0]))
                        actionRes[kvp[0]] = kvp[1];
                    else
                        actionRes.Add(kvp[0], kvp[1]);
                }
            }
        }

        foreach (DbAction action in await ActionRepository.GetAsync())
        {
            if (action.Type == 102)
            {
                var response = action.Param.Split(' ');
                if (response.Length < 2)
                {
                    logger.Warning($"Action [{action.Id}] Type 102 doesn't set a task [param: {action.Param}]");
                    action.Param = action.Param.Trim() + " 0";
                    await ServerDbContext.UpdateAsync(action);
                }
                else if (response[1] != "0")
                    if (!uint.TryParse(response[1], out var taskId) || !tasks.ContainsKey(taskId))
                        logger.Warning($"Task not found for action {action.Id}");
            }

            if (action.Param.Contains('@'))
            {
                string tempParam = action.Param;
                string stringId = "";
                bool started = false;
                bool finished = false;

                for (int i = 0; i < tempParam.Length; i++)
                {
                    if (tempParam[i] == '@')
                    {
                        if (string.IsNullOrEmpty(stringId))
                        {
                            // start
                            started = true;
                        }
                        else
                        {
                            // end
                            finished = true;
                        }
                    }
                    else
                    {
                        if (started && !finished)
                            stringId += tempParam[i];
                    }

                    if (finished && actionRes.TryGetValue(stringId, out string stringValue))
                    {
                        action.Param = action.Param.Replace($"@{stringId}@", stringValue);
                        started = false;
                        finished = false;
                        stringId = "";
                    }
                    else if (finished)
                    {
                        action.Param = "";
                    }
                }
            }

            actions.TryAdd(action.Id, action);
        }

        logger.Information("Loaded {Actions} actions from database", actions.Count);
    }

    public static async Task OnLoginAsync(Character user)
    {
        foreach (GameEvent e in events.Values) await e.OnLoginAsync(user);
    }

    public static async Task OnLogoutAsync(Character user)
    {
        foreach (GameEvent e in events.Values) await e.OnLogoutAsync(user);
    }

    public static bool IsInQualifierEvent(Character user)
    {
        return events.Values.Any(x => GameEvent.IsQualifierEvent(x.Identity) && x.IsInscribed(user.Identity));
    }

    public static bool IsInscribed(GameEvent.EventType eventType, Character user)
    {
        var evt = GetEvent(eventType);
        return evt != null && evt.IsInscribed(user.Identity);
    }

    public static bool IsInEventMap(GameEvent.EventType eventType, Character user)
    {
        var evt = GetEvent(eventType);
        return evt != null && evt.IsInEventMap(user.MapIdentity);
    }

    public static DbAction GetAction(uint idAction)
    {
        return actions.TryGetValue(idAction, out DbAction result) ? result : null;
    }

    public static DbTask GetTask(uint idTask)
    {
        return tasks.TryGetValue(idTask, out DbTask result) ? result : null;
    }

    public static bool QueueAction(QueuedAction action)
    {
        queuedActions.Add(action);
        return true;
    }

    public static async Task<bool> RegisterEventAsync(GameEvent @event)
    {
        if (events.ContainsKey(@event.Identity)) return false;

        if (await @event.CreateAsync() && events.TryAdd(@event.Identity, @event))
        {
            logger.Information($"Event '{@event.Name}' has been registered");
            return true;
        }

        logger.Error($"Event '{@event.Name}' has not been registered");
        return false;
    }

    public static void RemoveEvent(GameEvent.EventType type)
    {
        events.TryRemove(type, out _);
    }

    public static List<IWitnessEvent> QueryWitnessEvents()
    {
        return events.Values.Where(x => x is IWitnessEvent).Cast<IWitnessEvent>().ToList();
    }

    public static List<T> QueryEvents<T>()
    {
        return events.Values.Where(x => x is T).Cast<T>().ToList();
    }

    public static T GetEvent<T>() where T : GameEvent
    {
        return events.Values.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
    }

    public static GameEvent GetEvent(GameEvent.EventType type)
    {
        return events.TryGetValue(type, out GameEvent ev) ? ev : null;
    }

    public static GameEvent GetEvent(uint idMap)
    {
        return events.Values.FirstOrDefault(x => x.Map?.Identity == idMap);
    }

    public static async Task BulletinInvitationAsync(Character user, uint eventId)
    {
        if (inviteTrans.TryGetValue(eventId, out var invite))
        {
            await GameAction.ExecuteActionAsync((uint)invite.Data2, user, null, null, string.Empty);
        }
    }

    public static void ClearInvitationList(int eventId)
    {
        if (inviteTransInvitations.TryGetValue(eventId, out var invitations))
        {
            inviteTransInvitations.Clear();
        }
    }

    public static void AddToInvitationList(int eventId, uint userId)
    {
        var invitations = inviteTransInvitations.GetOrAdd(eventId, new List<uint>());
        if (!invitations.Contains(userId))
        {
            invitations.Add(userId);
        }
    }

    public static List<uint> QueryInvitationList(int eventId)
    {
        return inviteTransInvitations.TryGetValue(eventId, out var result) ? result : new List<uint>();
    }

    public static async Task OnTimerAsync()
    {
        //await PigeonManager.OnTimerAsync();
        await AuctionManager.OnTimerAsync();

        var ranking = rankingBroadcast.ToNextTime();
        foreach (DynamicNpc dynaNpc in RoleManager.QueryRoleByType<DynamicNpc>())
        {
            if (dynaNpc.IsGoal() || !dynaNpc.IsSynFlag()) continue;

            await dynaNpc.CheckFightTimeAsync();

            if (ranking && events.Values.All(x => x.Map?.Identity != dynaNpc.MapIdentity))
                await dynaNpc.BroadcastRankingAsync();
        }

        if (ranking)
        {
            _ = ServerStatisticManager.SaveAsync();
        }

        foreach (GameEvent @event in events.Values)
        {
            if (@event.ToNextTime())
            {
                await @event.OnTimerAsync();
            }
        }

        for (var i = queuedActions.Count - 1; i >= 0; i--)
        {
            QueuedAction action = queuedActions[i];
            if (action.CanBeExecuted)
            {
                Character user = RoleManager.GetUser(action.UserIdentity);
                Item item = null;
				Role role = null;
                if (user != null)
                {
                    if (user.InteractingItem != 0) item = user.UserPackage.FindByIdentity(user.InteractingItem);

                    if (user.InteractingNpc != 0) role = RoleManager.GetRole(user.InteractingNpc);

                    Task queuedActionTask()
                    {
                        return GameAction.ExecuteActionAsync(action.Action, user, role, item, string.Empty);
                    }

                    user.QueueAction(queuedActionTask);
                }
                else if (action.UserIdentity == 0)
                {
                    await GameAction.ExecuteActionAsync(action.Action, null, null, null, string.Empty);
                }
                queuedActions.RemoveAt(i);
                }
        }

        await SyndicateManager.OnTimerAsync();
        await MapManager.OnTimerAsync();

        await DailyResetAsync();
    }

    private static bool IsDailyResetEnabled()
    {
        if (dailyReset == null)
        {
			// server has started previously but no daily reset has been made
			//if (ServerConfiguration.Configuration.Realm.ReleaseDate < DateTime.Now)
   //         {
   //             return true;
   //         }
            return false;
        }

        // server was shutdown or midnight has passed (or daily reset has not been completed, duration = 0)
        if (dailyReset.RunTime.Date < DateTime.Now.Date || dailyReset.Duration == 0)
        {
            return true;
        }

        return false;
    }

    private static async Task DailyResetAsync()
    {
        if (!IsDailyResetEnabled())
            return;

        logger.Information($"Running daily reset tasks");

        dailyReset = new DbDailyReset
        {
            RunTime = DateTime.Now
        };
        await ServerDbContext.UpdateAsync(dailyReset);

        Stopwatch sw = Stopwatch.StartNew();

        try
        {
            await ServerDbContext.ScalarAsync("DELETE FROM cq_activity_user_task LIMIT 1234567890;");
            await ServerDbContext.ScalarAsync("DELETE FROM cq_statistic_daily LIMIT 1234567890;");

            foreach (var player in RoleManager.QueryUserSet()) // do online players
            {
                try
                {
                    await player.DoDailyResetAsync(false);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex, "Error when reseting online user! Message: {}", ex.Message);
                }
            }

            await ServerDbContext.ScalarAsync($"UPDATE cq_jianghu_caltivate_times SET free_times=0, paid_times=0;");
            await ServerDbContext.ScalarAsync($"UPDATE task_detail SET " +
                $"data1=0, data2=0, data3=0, data4=0, " +
                $"data5=0, data6=0, data7=0, " +
                $"complete_flag=0 " +
                $"WHERE task_id IN ({string.Join(",", TaskDetail.DailyTasks.Select(x => x.ToString()).ToArray())});");

            foreach (var e in events.Values)
            {
                await e.OnDailyResetAsync();
            }

            //await FlowerManager.DailyResetAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error during daily reset! Message: {message}", ex.Message);
        }
        finally
        {
            sw.Stop();
            dailyReset.Duration = (ulong)sw.ElapsedTicks;
            await ServerDbContext.UpdateAsync(dailyReset);

            logger.Information($"Daily reset tasks finished in {sw.Elapsed.TotalMilliseconds:N3} ms");
        }
    }
}