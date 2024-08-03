using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class SupermanManager
    {
        private static readonly ILogger logger = Log.ForContext<SupermanManager>();
        private static readonly ConcurrentDictionary<uint, DbSuperman> superman = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Initializing Superman manager");

            var supermen = await SupermanRepository.GetAsync();
            foreach (var superman in supermen)
            {
                SupermanManager.superman.TryAdd(superman.UserIdentity, superman);
            }
        }

        public static async Task AddOrUpdateSupermanAsync(uint idUser, int amount)
        {
            if (!superman.TryGetValue(idUser, out var sm))
            {
                superman.TryAdd(idUser, sm = new DbSuperman
                {
                    UserIdentity = idUser,
                    Amount = (uint)amount
                });
                await ServerDbContext.CreateAsync(sm);
            }
            else
            {
                sm.Amount = (uint)amount;
                await ServerDbContext.UpdateAsync(sm);
            }
        }

        public static int GetSupermanPoints(uint idUser)
        {
            return (int)(superman.TryGetValue(idUser, out var value) ? value.Amount : 0);
        }

        public static int GetSupermanRank(uint idUser)
        {
            int result = 1;
            foreach (var super in superman.Values.OrderByDescending(x => x.Amount))
            {
                if (super.UserIdentity == idUser)
                {
                    return result;
                }

                result++;
            }
            return result;
        }
    }
}
