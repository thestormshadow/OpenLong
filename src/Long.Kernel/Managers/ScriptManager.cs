using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.Items;
using Long.Kernel.States;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using Long.Kernel.States.Npcs;

namespace Long.Kernel.Managers
{
    public class ScriptManager
    {
        private static readonly ILogger logger = Log.ForContext<ScriptManager>();

        private static readonly ConcurrentDictionary<uint, DbAction> actions = new();
        private static readonly ConcurrentDictionary<uint, DbTask> tasks = new();
        private static readonly List<QueuedAction> queuedActions = new();
        private static readonly ConcurrentDictionary<uint, DbConfig> inviteTrans = new();

        private static ConcurrentDictionary<int, List<uint>> inviteTransInvitations = new();
        private static readonly TimeOut rankingBroadcast = new(10);

        public static async Task InitializeAsync()
        {
            logger.Information("Loading actions and tasks");

            await LoadActionsAsync();

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
                logger.Error(ex, "Failed to load LUA Scripts!!! {0}", ex.Message);
            }
        }

        private static Dictionary<string, string> LoadActionRes()
        {
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
            return actionRes;
        }

        private static void OverrideActionStrings(DbAction action, Dictionary<string, string> actionRes)
        {
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
        }

        public static async Task<bool> ReloadActionAsync(uint idAction)
        {
            DbAction action = await ActionRepository.GetAsync(idAction);
            if (action == null)
            {
                return false;
            }

            actions.TryRemove(idAction, out _);
            Dictionary<string, string> actionRes = LoadActionRes();
            OverrideActionStrings(action, actionRes);
            return actions.TryAdd(action.Id, action);
        }

        public static async Task LoadActionsAsync()
        {
            tasks.Clear();
            foreach (DbTask task in await TaskRepository.GetAsync()) tasks.TryAdd(task.Id, task);

            logger.Information("Loaded {0} tasks from database", tasks.Count);

            actions.Clear();

            Dictionary<string, string> actionRes = LoadActionRes();
            foreach (DbAction action in await ActionRepository.GetAsync())
            {
                if (action.Type == 102)
                {
                    var response = action.Param.Split(' ');
                    if (response.Length < 2)
                    {
                        logger.Warning("Action [{0}] Type 102 doesn't set a task [param: {1}]", action.Id, action.Param);
                        action.Param = action.Param.Trim() + " 0";
                        await ServerDbContext.UpdateAsync(action);
                    }
                    else if (response[1] != "0")
                        if (!uint.TryParse(response[1], out var taskId) || !tasks.ContainsKey(taskId))
                            logger.Warning("Task not found for action {0}", action.Id);
                }

                OverrideActionStrings(action, actionRes);

                actions.TryAdd(action.Id, action);
            }

            logger.Information("Loaded {0} actions from database", actions.Count);
        }

        public static DbAction GetAction(uint idAction)
        {
            return actions.TryGetValue(idAction, out DbAction result) ? result : null;
        }

        public static DbTask GetTask(uint idTask)
        {
            return tasks.TryGetValue(idTask, out DbTask result) ? result : null;
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
                invitations.Clear();
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

        public static bool QueueAction(QueuedAction action)
        {
            queuedActions.Add(action);
            return true;
        }

        public static async Task OnTimerAsync()
        {
            var ranking = rankingBroadcast.ToNextTime();
            foreach (DynamicNpc dynaNpc in RoleManager.QueryRoleByType<DynamicNpc>())
            {
                if (dynaNpc.IsGoal() || !dynaNpc.IsSynFlag()) continue;
                await dynaNpc.CheckFightTimeAsync();

                if (ranking)
                {
                    await dynaNpc.BroadcastRankingAsync();
                }
            }

            if (ranking)
            {
                _ = ServerStatisticManager.SaveAsync();
            }

            if (SyndicateManager != null)
            {
                await SyndicateManager.OnTimerAsync();
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
                        if (user.InteractingItem != 0)
                        {
                            item = user.UserPackage.FindItemByIdentity(user.InteractingItem);
                        }

                        if (user.InteractingNpc != 0)
                        {
                            role = RoleManager.GetRole(user.InteractingNpc);
                        }

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
        }
    }
}
