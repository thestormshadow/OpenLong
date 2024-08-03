using Long.Database.Entities;
using Long.Kernel.Database.Repositories;

namespace Long.Kernel.Managers
{
    public class BattleSystemManager
    {
        private static readonly ILogger logger = Log.ForContext<BattleSystemManager>();
        private static readonly List<DbDisdain> disdains = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Battle system manager is initialing");
            disdains.AddRange(await DisdainRepository.GetAsync());
        }

        public static DbDisdain GetDisdain(int delta)
        {
            return disdains.Aggregate((x, y) => Math.Abs(x.DeltaLev - delta) < Math.Abs(y.DeltaLev - delta) ? x : y);
        }
    }
}
