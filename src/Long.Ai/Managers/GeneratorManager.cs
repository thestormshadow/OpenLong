using Long.Ai.Database.Repositories;
using Long.Ai.Sockets;
using Long.Ai.States.World;
using Long.Database.Entities;
using Serilog;
using System.Collections.Concurrent;

namespace Long.Ai.Managers
{
    public class GeneratorManager
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<GeneratorManager>();

        private static bool initialized;

        private GeneratorManager() { }

        public static async Task<bool> InitializeAsync()
        {
            try
            {
                foreach (DbGenerator dbGen in await GeneratorRepository.GetAsync())
                {
                    var gen = new Generator(dbGen);
                    if (gen.Ready)
                    {
                        await AddGeneratorAsync(gen);
                    }
                    dbGenerators.TryAdd(dbGen.Id, dbGen);
                }
                initialized = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task OnTimerAsync(int partition)
        {
            if (!initialized)
            {
                return;
            }

            if (generators.ContainsKey(partition))
            {
                foreach (Generator gen in generators[partition])
                {
                    await gen.GenerateAsync();
                }
            }
        }

        public static async Task<bool> AddGeneratorAsync(Generator generator)
        {
            try
            {
                if (!generator.Ready)
                    return false;

                GameMap map = MapManager.GetMap(generator.MapIdentity);
                if (generators.ContainsKey(map.GeneratorPartition))
                {
                    generators[map.GeneratorPartition].Add(generator);
                }
                else
                {
                    generators.TryAdd(map.GeneratorPartition, new List<Generator>());
                    generators[map.GeneratorPartition].Add(generator);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Add Generator error: {}", e.Message);
                return false;
            }

            return true;
        }

        public static void RemoveGenerator(uint idGen)
        {
            foreach (var genArray in generators.Values)
            {
                genArray.RemoveAll(x => x.Identity == idGen);
            }
        }

        public static void SynchroGenerators()
        {
            if (GameServerHandler.Instance == null)
                return;

            var count = 0;
            foreach (List<Generator> partition in generators.Values)
            {
                foreach (Generator generator in partition)
                {
                    count += generator.SendAll();
                }
            }

            logger.Information($"Total {count} NPCs sent to the game server!!!");
        }

        public static Generator GetGenerator(uint idGen)
        {
            return generators.Keys.SelectMany(partition => generators[partition])
                              .FirstOrDefault(gen => gen.Identity == idGen);
        }

        public static List<Generator> GetGenerators(uint idMap)
        {
            return (from partition in generators.Keys
                    from gen in generators[partition]
                    where gen.MapIdentity == idMap
                    select gen).ToList();
        }

        public static List<Generator> GetGenerators(uint idMap, string monsterName)
        {
            return (from partition in generators.Keys
                    from gen in generators[partition]
                    where gen.MapIdentity == idMap && gen.MonsterName.Equals(monsterName)
                    select gen).ToList();
        }

        public static List<Generator> GetByMonsterType(uint idType)
        {
            return (from partition in generators.Keys
                    from gen in generators[partition]
                    where gen.RoleType == idType
                    select gen).ToList();
        }

        public static List<DbGenerator> GetByMapId(uint idMap)
        {
            return dbGenerators.Values.Where(x => x.Mapid == idMap).ToList();
        }

        private static readonly ConcurrentDictionary<uint, DbGenerator> dbGenerators = new();
        private static readonly ConcurrentDictionary<int, List<Generator>> generators = new();
    }
}
