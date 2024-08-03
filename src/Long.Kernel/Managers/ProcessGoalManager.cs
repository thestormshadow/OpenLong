using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class ProcessGoalManager
    {
        public const int CLIENT_GOAL = 1;
        public const int SERVER_GOAL = 0;

        private static readonly ILogger logger = Log.ForContext<ProcessGoalManager>();

        private static ConcurrentDictionary<uint, DbProcessGoal> goals = new();
        private static ConcurrentDictionary<uint, DbProcessTask> tasks = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Initializing process goal manager");

            foreach (var goal in StageGoalRepository.GetGoals())
            {
                goals.TryAdd(goal.Id, goal);
            }

            foreach (var task in StageGoalRepository.GetTasks())
            {
                tasks.TryAdd(task.Id, task);
            }
        }

        public static DbProcessGoal GetGoal(uint goal)
        {
            return goals.TryGetValue(goal, out var result) ? result : null;
        }

        public static List<DbProcessGoal> GetGoals()
        {
            return goals.Values.ToList();
        }

        public static DbProcessTask GetTask(ushort taskId)
        {
            return tasks.TryGetValue(taskId, out var result) ? result : null;
        }

        public static List<DbProcessTask> GetTasks()
        {
            return tasks.Values.ToList();
        }

        public static List<DbProcessTask> GetTasks(uint goalId)
        {
            var goal = GetGoal(goalId);
            if (goal != null)
            {
                return tasks.Values.Where(x => x.Id / 100 == goalId).ToList();
            }
            return new List<DbProcessTask>();
        }

        public static List<DbProcessTask> GetSystemTasks()
        {
            return tasks.Values.Where(x => x.Sort == SERVER_GOAL).ToList();
        }

        public enum GoalType
        {
            None,
            LevelUp = 1,
            Metempsychosis = 2,
            BegginerTutorialCompletion = 3,
            XpSkillKills = 4,
            EquipmentQuality = 5,
            ProfessionPromotion = 6,
            WinQualifier = 7,
            ExperienceMultiplier = 9,
            CreateJoinSyndicate = 11,
            MakeJoinTeam = 12,
            AddFriends = 13,
            WinTeamQualifier = 14,
            PlayLottery = 15,
            Composition = 16,
            ElitePkTournament = 17,
            TeamPkTournament = 18,
            SkillTeamPkTournament = 19,
            SuperTalismans = 20,
            TotalComposingLevel = 22,
            EquipmentPlus3 = 23,
            JoinSubClass = 24,
            ChampionsArena = 26,
            TotalEmbedGems = 27,
            TotalEmbedSuperGems = 28,
            DragonSoulLevel = 29,
            ChiStudyTotalPoints = 30,
            Enlightenment = 31,
            GuildPkTournament = 32,
            CaptureTheFlag = 33,
            JiangHuScore = 34,
            HouseLevel = 35,
            Marriage = 36,
            NobilityDonation = 37,
            ElitePkTopRank = 38,
            DisCity = 39,
            AllDailyQuests = 40,
            Tutor = 41,
            BossKiller = 42,
            BattlePower = 43,
            EquipSteed = 44,
            UpgradeEquipment = 45,
            RefineryLevel = 46
        }
    }
}
