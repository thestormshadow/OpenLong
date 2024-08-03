using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Shared.Helpers;

namespace Long.Kernel.Managers
{
    public class LotteryManager
    {
        private static readonly ILogger logger = Log.ForContext<LotteryManager>();

        private static readonly List<DbConfig> config = new();

        protected LotteryManager() 
        {            
        }

        public static async Task<bool> InitializeAsync()
        {
            logger.Information("Lottery manager initialization");

            for (var i = 0; i < 5; i++)
            {
                int minConfig = 11000 + i * 10;
                int maxConfig = 11009 + i * 10;

                config.AddRange(await ConfigRepository.GetAsync(x => x.Type >= minConfig && x.Type <= maxConfig));
            }
            return true;
        }

        public static async Task<LotteryItemTempInfo?> GetRandomItemAsync(int pool, uint oldItemType = 0)
        {
            List<DbLottery> allItems = await LotteryRepository.GetAsync();
            List<DbConfig> lotteryConfiguration = config.Where(x => x.Data1 == pool).ToList();

            var ranks = new List<LotteryRankTempInfo>();
            var chance = 0;
            foreach (DbConfig config in lotteryConfiguration.OrderBy(x => x.Data2))
            {
                chance += config.Data2;
                ranks.Add(new LotteryRankTempInfo
                {
                    Chance = chance,
                    Rank = config.Type % 10
                });
            }

            LotteryRankTempInfo tempRank = default;
            int rand = await NextAsync(chance);
            foreach (LotteryRankTempInfo rank in ranks)
            {
                if (rand <= rank.Chance)
                {
                    tempRank = rank;
                    break;
                }
            }

            chance = 0;
            var infos = new List<LotteryItemTempInfo>();
            foreach (DbLottery item in allItems.Where(x => x.Rank == tempRank.Rank && x.Color == pool))
            {
                chance += (int)item.Chance;
                infos.Add(new LotteryItemTempInfo
                {
                    Chance = chance,
                    ItemIdentity = item.ItemIdentity,
                    ItemName = item.Itemname,
                    Plus = item.Plus,
                    Color = item.Color,
                    SocketNum = item.SocketNum,
                    Rank = item.Rank
                });
            }

            DbItemtype itemType = null;
            LotteryItemTempInfo reward = default;

            for (int i = 0; i < 20; i++)
            {
                rand = await NextAsync(chance);
                foreach (LotteryItemTempInfo info in infos.OrderBy(x => x.Chance))
                {
                    if (rand <= info.Chance)
                    {
                        reward = info;
                        break;
                    }
                }

                if (reward.ItemIdentity == 0 || reward.ItemIdentity == oldItemType)
                {
                    continue;
                }

                itemType = ItemManager.GetItemtype(reward.ItemIdentity);
                if (itemType == null)
                {
                    logger.Error("Lottery failed, invalid itemtype {0} {1}", reward.ItemIdentity, reward.ItemName);
                    continue;
                }
                return reward;
            }
            return null;
        }

        private struct LotteryRankTempInfo
        {
            public int Chance { get; init; }
            public int Rank { get; init; }
        }

        public struct LotteryItemTempInfo
        {
            public int Chance { get; init; }
            public int Rank { get; init; }
            public string ItemName { get; init; }
            public uint ItemIdentity { get; init; }
            public byte Color { get; init; }
            public byte SocketNum { get; init; }
            public byte Plus { get; init; }
        }
    }
}
