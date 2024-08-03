using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Module.Rank.States;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Rank.Repositories
{
    public static class DynaRankRepository
    {
        public static List<UserRanking> Get(uint rankType, int page, int pageSize)
        {
            using var context = new ServerDbContext();
            int skip = page * pageSize;
            return context.DynaRankRecs
                .Include(x => x.User)
                .Where(x => x.RankType == rankType && x.User != null)
                .OrderByDescending(x => x.Value)
                .Skip(skip)
                .Take(pageSize)
                .ToList()
                .Select((rank, index) => new UserRanking
                {
                    Position = (uint)(index + 1 + skip),
                    Identity = rank.UserId,
                    Name = rank.User.Name,
                    Value = rank.Value
                })
                .ToList();
        }

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

        public static List<DbDynaRankRec> QueryRank(uint rankType)
        {
            using var ctx = new ServerDbContext();
            return ctx.DynaRankRecs.Where(x => x.RankType == rankType).ToList();
        }
    }
}
