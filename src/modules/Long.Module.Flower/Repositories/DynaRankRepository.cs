using Long.Kernel.Database;
using Long.Module.Flower.States;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Flower.Repositories
{
    public static class DynaRankRepository
    {
        public static UserRanking GetUserPos(uint userId, uint rankType, int limit)
        {
            using var context = new ServerDbContext();
            return context.DynaRankRecs
                .Include(x => x.User)
                .Where(x => x.RankType == rankType && x.User != null)
                .OrderByDescending(x => x.Value)
                .Take(limit)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.Value
                })
                .ToList()
                .FirstOrDefault(x => x.Identity == userId);
        }
    }
}
