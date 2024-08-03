using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Items;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class ServerStatisticManager
    {
        private static ILogger logger = Log.ForContext<ServerStatisticManager>();

        private const uint SPECIAL_GEMS_DROP_TOTAL = 100_000;
        private const uint SPECIAL_GEMS_DROP_SESSION = 100_001;
        private const uint MONEY_DROP = 100_002;
        private const uint EMONEY_DROP = 100_003;
        private const uint MONO_EMONEY_DROP = 100_004;
        private const uint QUALITY_ITEM_DROP_TOTAL = 100_005;
        private const uint QUALITY_ITEM_DROP_SESSION = 100_006;
        private const uint GEMS_DROP1_TOTAL = 100_007;
        private const uint GEMS_DROP2_TOTAL = 100_008;
        private const uint GEMS_DROP1_SESSION = 100_009;
        private const uint GEMS_DROP2_SESSION = 100_010;
        private const uint ITEM_STATUS_DROP_TOTAL = 100_011;
        private const uint ITEM_STATUS_DROP_SESSION = 100_012;

        private static readonly Dictionary<uint, string[]> statisticsIds = new()
        {
            { SPECIAL_GEMS_DROP_TOTAL, new string[] { "Meteors", "DragonBalls", "", "", "", "" } },
            { SPECIAL_GEMS_DROP_SESSION, new string[] { "Meteors", "DragonBalls", "", "", "", "" } },
            { MONEY_DROP, new string[] { "MoneyTotal", "MoneySession", "", "", "", "" } },
            { EMONEY_DROP, new string[] { "EmoneyTotal", "EmoneySession", "", "", "", "" } },
            { MONO_EMONEY_DROP, new string[] { "MonoEmoneyTotal", "MonoEmoneySession", "", "", "", "" } },
            { QUALITY_ITEM_DROP_TOTAL, new string[] { "Super", "Elite", "Unique", "Refined", "", "" } },
            { QUALITY_ITEM_DROP_SESSION, new string[] { "Super", "Elite", "Unique", "Refined", "", "" } },
            { GEMS_DROP1_TOTAL, new string[] { "PhoenixGem", "DragonGem", "FuryGem", "RainbowGem", "KylinGem", "VioletGem" } },
            { GEMS_DROP2_TOTAL, new string[] { "MoonGem", "TortoiseGem", "ThunderGem", "GloryGem", "", "" } },
            { GEMS_DROP1_SESSION, new string[] { "PhoenixGem", "DragonGem", "FuryGem", "RainbowGem", "KylinGem", "VioletGem" } },
            { GEMS_DROP2_SESSION, new string[] { "MoonGem", "TortoiseGem", "ThunderGem", "GloryGem", "", "" } },
            { ITEM_STATUS_DROP_TOTAL, new string[] { "Composition", "ReduceDmg", "OneSocket", "TwoSocket", "", "" } },
            { ITEM_STATUS_DROP_SESSION, new string[] { "Composition", "ReduceDmg", "OneSocket", "TwoSocket", "", "" } }
        };

        private static ConcurrentDictionary<uint, DbDynaGlobalData> dynaGlobalDataCache { get; } = new();

        public static async Task InitializeAsync()
        {
            logger.Information($"Initializing Server Statistic Manager...");

            var result = await DynamicGlobalDataRepository.GetAsync(
                statisticsIds.Keys.ToArray()
            );

            foreach (var res in result)
            {
                dynaGlobalDataCache.TryAdd(res.Id, res);
            }

            foreach (var vars in statisticsIds)
            {
                if (!dynaGlobalDataCache.TryGetValue(vars.Key, out var data))
                {
                    var dyna = new DbDynaGlobalData
                    {
                        Id = vars.Key,
                        Datastr0 = vars.Value[0],
                        Datastr1 = vars.Value[1],
                        Datastr2 = vars.Value[2],
                        Datastr3 = vars.Value[3],
                        Datastr4 = vars.Value[4],
                        Datastr5 = vars.Value[5]
                    };
                    dynaGlobalDataCache.TryAdd(dyna.Id, dyna);
                    await ServerDbContext.CreateAsync(dyna);
                }
                else
                {
                    switch (data.Id)
                    {
                        case SPECIAL_GEMS_DROP_SESSION:
                        case QUALITY_ITEM_DROP_SESSION:
                        case GEMS_DROP1_SESSION:
                        case GEMS_DROP2_SESSION:
                        case ITEM_STATUS_DROP_SESSION:
                            {
                                data.Data0 = 0;
                                data.Data1 = 0;
                                data.Data2 = 0;
                                data.Data3 = 0;
                                data.Data4 = 0;
                                data.Data5 = 0;
                                break;
                            }
                        case MONEY_DROP:
                        case EMONEY_DROP:
                        case MONO_EMONEY_DROP:
                            {
                                data.Data1 = 0;
                                break;
                            }
                    }
                }
            }

            await SaveAsync();
        }

        #region Increment

        public static void DropMeteor()
        {
            dynaGlobalDataCache[SPECIAL_GEMS_DROP_TOTAL].Data0 += 1;
            dynaGlobalDataCache[SPECIAL_GEMS_DROP_SESSION].Data0 += 1;
        }

        public static void DropDragonBall()
        {
            dynaGlobalDataCache[SPECIAL_GEMS_DROP_TOTAL].Data1 += 1;
            dynaGlobalDataCache[SPECIAL_GEMS_DROP_SESSION].Data1 += 1;
        }

        public static void DropMoney(long money)
        {
            dynaGlobalDataCache[MONEY_DROP].Data0 += money;
            dynaGlobalDataCache[MONEY_DROP].Data1 += money;
        }

        public static void DropConquerPoints(int conquerPoints)
        {
            dynaGlobalDataCache[EMONEY_DROP].Data0 += conquerPoints;
            dynaGlobalDataCache[EMONEY_DROP].Data1 += conquerPoints;
        }

        public static void DropBoundConquerPoints(int boundConquerPoints)
        {
            dynaGlobalDataCache[MONO_EMONEY_DROP].Data0 += boundConquerPoints;
            dynaGlobalDataCache[MONO_EMONEY_DROP].Data1 += boundConquerPoints;
        }

        public static void DropGem(Item.SocketGem socketGem)
        {
            switch (socketGem)
            {
                case Item.SocketGem.NormalPhoenixGem:
                case Item.SocketGem.RefinedPhoenixGem:
                case Item.SocketGem.SuperPhoenixGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data0 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data0 += 1;
                    break;
                case Item.SocketGem.NormalDragonGem:
                case Item.SocketGem.RefinedDragonGem:
                case Item.SocketGem.SuperDragonGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data1 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data1 += 1;
                    break;
                case Item.SocketGem.NormalFuryGem:
                case Item.SocketGem.RefinedFuryGem:
                case Item.SocketGem.SuperFuryGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data2 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data2 += 1;
                    break;
                case Item.SocketGem.NormalRainbowGem:
                case Item.SocketGem.RefinedRainbowGem:
                case Item.SocketGem.SuperRainbowGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data3 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data3 += 1;
                    break;
                case Item.SocketGem.NormalKylinGem:
                case Item.SocketGem.RefinedKylinGem:
                case Item.SocketGem.SuperKylinGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data4 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data4 += 1;
                    break;
                case Item.SocketGem.NormalVioletGem:
                case Item.SocketGem.RefinedVioletGem:
                case Item.SocketGem.SuperVioletGem:
                    dynaGlobalDataCache[GEMS_DROP1_TOTAL].Data5 += 1;
                    dynaGlobalDataCache[GEMS_DROP1_SESSION].Data5 += 1;
                    break;
                case Item.SocketGem.NormalMoonGem:
                case Item.SocketGem.RefinedMoonGem:
                case Item.SocketGem.SuperMoonGem:
                    dynaGlobalDataCache[GEMS_DROP2_TOTAL].Data0 += 1;
                    dynaGlobalDataCache[GEMS_DROP2_SESSION].Data0 += 1;
                    break;
                case Item.SocketGem.NormalTortoiseGem:
                case Item.SocketGem.RefinedTortoiseGem:
                case Item.SocketGem.SuperTortoiseGem:
                    dynaGlobalDataCache[GEMS_DROP2_TOTAL].Data1 += 1;
                    dynaGlobalDataCache[GEMS_DROP2_SESSION].Data1 += 1;
                    break;
                case Item.SocketGem.NormalThunderGem:
                case Item.SocketGem.RefinedThunderGem:
                case Item.SocketGem.SuperThunderGem:
                    dynaGlobalDataCache[GEMS_DROP2_TOTAL].Data2 += 1;
                    dynaGlobalDataCache[GEMS_DROP2_SESSION].Data2 += 1;
                    break;
                case Item.SocketGem.NormalGloryGem:
                case Item.SocketGem.RefinedGloryGem:
                case Item.SocketGem.SuperGloryGem:
                    dynaGlobalDataCache[GEMS_DROP2_TOTAL].Data3 += 1;
                    dynaGlobalDataCache[GEMS_DROP2_SESSION].Data3 += 1;
                    break;
            }
        }

        public static void DropQualityItem(int quality)
        {
            switch (quality)
            {
                case 9:
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_TOTAL].Data0 += 1;
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_SESSION].Data0 += 1;
                    break;
                case 8:
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_TOTAL].Data1 += 1;
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_SESSION].Data1 += 1;
                    break;
                case 7:
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_TOTAL].Data2 += 1;
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_SESSION].Data2 += 1;
                    break;
                case 6:
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_TOTAL].Data3 += 1;
                    dynaGlobalDataCache[QUALITY_ITEM_DROP_SESSION].Data3 += 1;
                    break;
            }
        }

        public static void DropComposedItem()
        {
            dynaGlobalDataCache[ITEM_STATUS_DROP_TOTAL].Data0 += 1;
            dynaGlobalDataCache[ITEM_STATUS_DROP_SESSION].Data0 += 1;
        }

        public static void DropReducedDamageItem()
        {
            dynaGlobalDataCache[ITEM_STATUS_DROP_TOTAL].Data1 += 1;
            dynaGlobalDataCache[ITEM_STATUS_DROP_SESSION].Data1 += 1;
        }

        public static void DropOneSocketItem()
        {
            dynaGlobalDataCache[ITEM_STATUS_DROP_TOTAL].Data2 += 1;
            dynaGlobalDataCache[ITEM_STATUS_DROP_SESSION].Data2 += 1;
        }

        public static void DropTwoSocketItem()
        {
            dynaGlobalDataCache[ITEM_STATUS_DROP_TOTAL].Data3 += 1;
            dynaGlobalDataCache[ITEM_STATUS_DROP_SESSION].Data3 += 1;
        }

        #endregion

        public static async Task SaveAsync()
        {
            await ServerDbContext.UpdateRangeAsync(dynaGlobalDataCache.Values.ToList());
        }
    }
}
