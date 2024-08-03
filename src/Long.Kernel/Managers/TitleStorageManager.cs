using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class TitleStorageManager
    {
        private static readonly ILogger logger = Log.ForContext<TitleStorageManager>();

        private static readonly ConcurrentDictionary<uint, DbTitleRule> titleRules = new();
        private static readonly List<DbTitleType> titleTypes = new();

        protected TitleStorageManager() { }

        public static async Task InitializeAsync()
        {
            logger.Information("Starting title storage manager...");

            foreach (var rule in await TitleStorageRepository.GetTitleRulesAsync())
            {
                titleRules.TryAdd(rule.Identity, rule);
            }

            titleTypes.AddRange(await TitleStorageRepository.GetTitleTypesAsync());
        }

        public static DbTitleType GetTitleType(uint titleType, uint titleId)
        {
            return titleTypes.FirstOrDefault(x => x.Type == titleType && x.Identity == titleId);
        }

        public static List<DbTitleType> GetTitlesType() => titleTypes;
	}
}
