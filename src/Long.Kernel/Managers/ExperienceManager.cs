using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class ExperienceManager
    {
        private static readonly ILogger logger = Log.ForContext<ExperienceManager>();

        private static readonly ConcurrentDictionary<byte, DbLevelExperience> userLevelExperience = new();
        private static ConcurrentDictionary<uint, ExperienceMultiplierData> experienceMultiplierData = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Starting experience manager");
            List<DbLevelExperience> levelExperiences = await LevelExperienceRepository.GetAsync();
            foreach (DbLevelExperience lev in levelExperiences.Where(x => x.Type == 0))
            {
                userLevelExperience.TryAdd(lev.Level, lev);
            }
        }

        public static DbLevelExperience GetLevelExperience(byte level)
        {
            return userLevelExperience.TryGetValue(level, out DbLevelExperience value) ? value : null;
        }

        public static int GetLevelLimit()
        {
            return userLevelExperience.Count + 1;
        }

        public static bool AddExperienceMultiplierData(uint idUser, float multiplier, int seconds)
        {
            ExperienceMultiplierData value = GetExperienceMultiplierData(idUser);
            if (!value.Equals(default) && value.ExperienceMultiplier > multiplier)
            {
                return false;
            }

            experienceMultiplierData.TryRemove(idUser, out _);

            value = new ExperienceMultiplierData
            {
                UserId = idUser,
                ExperienceMultiplier = multiplier,
                EndTime = DateTime.Now.AddSeconds(seconds)
            };
            return experienceMultiplierData.TryAdd(idUser, value);
        }

        public static ExperienceMultiplierData GetExperienceMultiplierData(uint idUser)
        {
            if (!experienceMultiplierData.TryGetValue(idUser, out var data))
            {
                return default;
            }
            if (DateTime.Now > data.EndTime)
            {
                experienceMultiplierData.TryRemove(idUser, out _);
                return default;
            }
            return data;
        }

        public struct ExperienceMultiplierData
        {
            public uint UserId { get; set; }
            public float ExperienceMultiplier { get; set; }
            public DateTime EndTime { get; set; }
            public readonly bool IsActive => EndTime > DateTime.Now;
            public readonly int RemainingSeconds => (int)(IsActive ? (EndTime - DateTime.Now).TotalSeconds : 0);
        }
    }
}
