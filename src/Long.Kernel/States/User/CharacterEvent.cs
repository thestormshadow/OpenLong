using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.Modules.Systems.TaskDetail;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Events;
using Long.Kernel.States.MessageBoxes;
using System.Collections.Concurrent;
using static Long.Kernel.Managers.ActivityManager;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        public MessageBox MessageBox { get; set; }

        public Task SendMenuMessageAsync(string message)
        {
            return SendAsync(new MsgTaskDialog
            {
                InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
                Text = message
            });
        }

		#region Game Action

		public long Iterator { get; set; } = -1;
        public long[] VarData { get; } = new long[MAX_VAR_AMOUNT];
        public string[] VarString { get; } = new string[MAX_VAR_AMOUNT];

        private readonly List<string> setTaskId = new();

        public uint LastAddItemIdentity { get; set; }
        public uint LastDelItemIdentity { get; set; }

        public uint InteractingItem { get; set; }
        public uint InteractingNpc { get; set; }
        public uint InteractingMouseAction { get; set; }
        public string InteractingMouseFunction { get; set; }

        public bool CheckItem(DbTask task)
        {
            if (!string.IsNullOrEmpty(task.Itemname1) && !StrNone.Equals(task.Itemname1))
            {
                if (UserPackage[task.Itemname1] == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(task.Itemname2) && !StrNone.Equals(task.Itemname2))
                {
                    if (UserPackage[task.Itemname2] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void CancelInteraction()
        {
            setTaskId.Clear();
            InteractingItem = 0;
            InteractingNpc = 0;
        }

        public byte PushTaskId(string idTask)
        {
            if (!"0".Equals(idTask) && setTaskId.Count < MAX_MENUTASKSIZE)
            {
                setTaskId.Add(idTask);
                return (byte)setTaskId.Count;
            }
            return 0;
        }

        public void ClearTaskId()
        {
            setTaskId.Clear();
        }

        public string GetTaskId(int idx)
        {
            return idx > 0 && idx <= setTaskId.Count ? setTaskId[idx - 1] : "0";
        }

        public bool TestTask(DbTask task)
        {
            if (task == null)
            {
                return false;
            }

            try
            {
                if (!CheckItem(task))
                {
                    return false;
                }

                if (Silvers < task.Money)
                {
                    return false;
                }

                if (task.Profession != 0 && Profession != task.Profession)
                {
                    return false;
                }

                if (task.Sex != 0 && task.Sex != 999 && task.Sex != Gender)
                {
                    return false;
                }

                if (PkPoints < task.MinPk || PkPoints > task.MaxPk)
                {
                    return false;
                }

                if (task.Marriage >= 0)
                {
                    if (task.Marriage == 0 && MateIdentity != 0)
                    {
                        return false;
                    }

                    if (task.Marriage == 1 && MateIdentity == 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Test task error", ex.Message);
                return false;
            }

            return true;
        }

        public async Task AddTaskMaskAsync(int idx)
        {
            if (idx < 0 || idx >= 32)
            {
                return;
            }

            user.TaskMask |= 1u << idx;
            await SaveAsync();
        }

        public async Task ClearTaskMaskAsync(int idx)
        {
            if (idx < 0 || idx >= 32)
            {
                return;
            }

            user.TaskMask &= ~(1u << idx);
            await SaveAsync();
        }

        public bool CheckTaskMask(int idx)
        {
            if (idx < 0 || idx >= 32)
            {
                return false;
            }

            return (user.TaskMask & (1u << idx)) != 0;
        }

        #endregion

        #region Statistic and Task Detail

        public UserStatistic Statistic { get; set; }
        public ITaskDetail TaskDetail { get; set; }

        #endregion

        #region Competion

        public uint QuizPoints
        {
            get => user.QuizPoints;
            set => user.QuizPoints = value;
        }

		#endregion

		#region Activity

		private readonly TimeOut activityLoginHalfHourTimer = new();

        public int ActivityPoints
        {
            get
            {
                int result = 0;
                foreach (var task in ActivityManager.GetUserTaskList(this))
                {
                    var activity = ActivityManager.GetTaskById(task.ActivityId);
                    result += (activity?.Activity ?? 0) * task.Schedule;
                }
                return result;
            }
        }

        public async Task InitializeActivityTasksAsync()
        {
            await SubmitActivityListAsync();
            await SubmitDailyActivityScoreAsync();

            foreach (var evt in Statistic.GetStcList(ActivityManager.StcEventId))
            {
                var evtStamp = UnixTimestamp.ToNullableDateTime(evt.Timestamp);
                if (evtStamp.HasValue
                    && DateTime.Now.Date <= evtStamp.Value.Date)
                {
                    await SendAsync(new MsgActivityTaskReward
                    {
                        RewardGrade = (byte)evt.DataType
                    });
                }
            }

            await ActivityManager.UpdateTaskActivityAsync(this, ActivityManager.ActivityType.LoginTheGame);
            if (VipLevel > 0)
            {
                await ActivityManager.UpdateTaskActivityAsync(this, ActivityManager.ActivityType.VipActiveness);
            }
            activityLoginHalfHourTimer.Startup(60 * 30);
        }

        public Task SubmitActivityListAsync()
        {
            MsgActivityTask msg = new();
            msg.Mode = MsgActivityTask.Action.UpdateActivityTask;
            foreach (var task in ActivityManager.GetUserTaskList(this))
            {
                msg.Activities.Add(new MsgActivityTask.Activity
                {
                    Id = task.ActivityId,
                    Completed = task.CompleteFlag,
                    Progress = task.Schedule
                });
            }
            return SendAsync(msg);
        }

        public Task SubmitDailyActivityScoreAsync()
        {
            return SendAsync(new MsgStatisticDaily
            {
                Data = new List<MsgStatisticDaily.DailyData> 
                {
                    new MsgStatisticDaily.DailyData()
                    {
                        EventId = MsgStatisticDaily.EventType.ActivityTaskData,
                        DataType = MsgStatisticDaily.DataMode.TodayActiveValue,
                        ActivityPoints = ActivityPoints
                    } 
                }
            });
        }

		#endregion

		#region Activity Tasks

		public ConcurrentDictionary<uint, ActivityTask> ActivityTasks = new();

		public async Task<bool> CheckForActivityTaskUpdatesAsync()
		{
			bool result = false;
			foreach (var availableTask in GetDisponibleTaskByUser(this))
			{
				if (ActivityTasks.Values.All(x => (byte)x.Type != availableTask.Type))
				{
					var dbTask = ActivityRepository.GetUserTasks(Identity).FirstOrDefault(x => x.ActivityId == availableTask.Id);
					dbTask ??= new DbActivityUserTask
					{
						ActivityId = availableTask.Id,
						UserId = Identity
					};
					var task = new ActivityTask(dbTask);
					ActivityTasks.TryAdd(availableTask.Id, task);
					await task.SaveAsync();
					result = true;
				}
			}
			return result;
		}

		public async Task UpdateTaskActivityAsync(ActivityType activityType)
		{
			ActivityTask activityTask = null;
			foreach (var activity in ActivityTasks.Values)
			{
				var act = GetTaskTypeById(activity.ActivityId);
				if (act == activityType)
				{
					activityTask = activity;
					break;
				}
			}

			if (activityTask == null)
			{
				return;
			}

			if (activityTask.CompleteFlag != 0)
			{
				return;
			}

			byte newAmount = (byte)(activityTask.Schedule + 1);
			var task = GetTaskById(activityTask.ActivityId);
			if (newAmount > task.MaxNum)
			{
				return;
			}

			activityTask.Schedule = newAmount;
			if (activityTask.Schedule >= task.MaxNum)
			{
				activityTask.CompleteFlag = 1;
			}

			await activityTask.SaveAsync();

			await SendAsync(new MsgActivityTask
			{
				Mode = MsgActivityTask.Action.UpdateActivityTask,
				Activities = new List<MsgActivityTask.Activity>
				{
					new MsgActivityTask.Activity
					{
						Id = activityTask.ActivityId,
						Completed = activityTask.CompleteFlag,
						Progress = activityTask.Schedule
					}
				}
			});

			await SubmitActivityPointsAsync();
		}

		public Task SubmitActivityPointsAsync()
		{
			return SendAsync(new MsgStatisticDaily
			{
				Data = new List<MsgStatisticDaily.DailyData>
				{
					new MsgStatisticDaily.DailyData()
					{
						EventId = MsgStatisticDaily.EventType.ActivityTaskData,
						DataType = MsgStatisticDaily.DataMode.TodayActiveValue,
						ActivityPoints = ActivityPoints
					}
				}
			});
		}

		public async Task ActivityTasksDailyResetAsync()
		{
			ActivityTasks.Clear();
			await CheckForActivityTaskUpdatesAsync();
			activityLoginHalfHourTimer.Update();
			await UpdateTaskActivityAsync(ActivityType.LoginTheGame);
			if (VipLevel > 0)
			{
				await UpdateTaskActivityAsync(ActivityType.VipActiveness);
			}
			await SubmitActivityListAsync();
			await SubmitActivityPointsAsync();
		}

		#endregion

		#region Process Goals

		public ProcessGoal StageGoal { get; }

        #endregion

        #region Daily Signin

        public DailySignIn SignIn { get; init; }

		#endregion

		#region Event

		public ConcurrentDictionary<GameEvent.EventType, GameEvent> GameEvents { get; init; } = new();

		public T GetEvent<T>() where T : GameEvent
		{
			return GameEvents.Values.FirstOrDefault(x => x.GetType().Equals(typeof(T))) as T;
		}

		public GameEvent GetCurrentEvent()
		{
			return GameEvents.Values.FirstOrDefault(x => x.IsInEventMap(MapIdentity));
		}

		public async Task<bool> SignInEventAsync(GameEvent e)
		{
			if (!e.IsAllowedToJoin(this) || !GameEvents.TryAdd(e.Identity, e))
			{
				return false;
			}

			await e.OnEnterAsync(this);
			return true;
		}

		public async Task<bool> SignOutEventAsync(GameEvent e)
		{
			if (GameEvents.TryRemove(e.Identity, out _))
			{
				await e.OnExitAsync(this);
			}
			return true;
		}

		public bool IsInQualifierEvent()
		{
			return GameEvents.Any(x => GameEvent.IsQualifierEvent(x.Key));
		}

		#endregion

		#region Arena Qualifier

		public ArenaStatus QualifierStatus { get; set; } = ArenaStatus.NotSignedUp;

		public uint QualifierPoints
		{
			get => user.AthletePoint;
			set => user.AthletePoint = value;
		}

		public uint QualifierDayWins
		{
			get => user.AthleteDayWins;
			set => user.AthleteDayWins = value;
		}

		public uint QualifierDayLoses
		{
			get => user.AthleteDayLoses;
			set => user.AthleteDayLoses = value;
		}

		public uint QualifierDayGames => QualifierDayWins + QualifierDayLoses;

		public uint QualifierHistoryWins
		{
			get => user.AthleteHistoryWins;
			set => user.AthleteHistoryWins = value;
		}

		public uint QualifierHistoryLoses
		{
			get => user.AthleteHistoryLoses;
			set => user.AthleteHistoryLoses = value;
		}

		public uint HonorPoints
		{
			get => user.AthleteCurrentHonorPoints;
			set => user.AthleteCurrentHonorPoints = value;
		}

		public uint HistoryHonorPoints
		{
			get => user.AthleteHistoryHonorPoints;
			set => user.AthleteHistoryHonorPoints = value;
		}

		public bool IsArenicWitness()
		{
			var arenicEvents = EventManager.QueryWitnessEvents();
			foreach (var witnessEvent in arenicEvents)
			{
				if (witnessEvent.IsWitness(this))
					return true;
			}
			return false;
		}

		#endregion
	}
}
