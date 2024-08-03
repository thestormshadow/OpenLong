using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Module.Flower.States;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Flower.Repositories
{
    public class FlowerRepository
    {
        public static async Task<DbFlower> GetFromUserAsync(uint idUser)
        {
            await using var context = new ServerDbContext();
            return await context.Flowers.FirstOrDefaultAsync(x => x.UserId == idUser);
        }

        public static async Task ResetTodayFlowersAsync()
        {
            await using var context = new ServerDbContext();
            foreach (var f in context.Flowers)
            {
                f.RedRose = 0;
                f.WhiteRose = 0;
                f.Orchids = 0;
                f.Tulips = 0;
            }
            await context.SaveChangesAsync();
        }

        public static UserRanking GetRedRoseTodayRank(uint idUser, int limit)
        {
            using var context = new ServerDbContext();
            return context.Flowers
                .Include(x => x.User)
                .Where(x => x.User != null)
                .OrderByDescending(x => x.RedRose)
                .Take(limit)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.RedRose
                })
                .ToList()
                .FirstOrDefault(x => x.Identity == idUser);
        }

        public static UserRanking GetWhiteRoseTodayRank(uint idUser, int limit)
        {
            using var context = new ServerDbContext();
            return context.Flowers
                .Include(x => x.User)
                .Where(x => x.User != null)
                .OrderByDescending(x => x.WhiteRose)
                .Take(limit)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.WhiteRose
                })
                .ToList()
                .FirstOrDefault(x => x.Identity == idUser);
        }

        public static UserRanking GetOrchidsTodayRank(uint idUser, int limit)
        {
            using var context = new ServerDbContext();
            return context.Flowers
                .Include(x => x.User)
                .Where(x => x.User != null)
                .OrderByDescending(x => x.Orchids)
                .Take(limit)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.Orchids
                })
                .ToList()
                .FirstOrDefault(x => x.Identity == idUser);
        }

        public static UserRanking GetTulipsTodayRank(uint idUser, int limit)
        {
            using var context = new ServerDbContext();
            return context.Flowers
                .Include(x => x.User)
                .Where(x => x.User != null)
                .OrderByDescending(x => x.Tulips)
                .Take(limit)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.Tulips
                })
                .ToList()
                .FirstOrDefault(x => x.Identity == idUser);
        }
    }
}
