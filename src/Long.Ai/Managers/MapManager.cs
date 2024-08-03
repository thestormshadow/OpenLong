using Long.Ai.Database.Repositories;
using Long.Ai.States.World;
using Long.Database.Entities;
using Serilog;
using System.Collections.Concurrent;
using System.Text;

namespace Long.Ai.Managers
{
    public class MapManager
    {
        private static readonly Serilog.ILogger logger = Log.ForContext<MapManager>();

        private MapManager() { }

        public static ConcurrentDictionary<uint, GameMap> GameMaps { get; } = new();

        public static async Task InitializeAsync()
        {
            List<DbMap> maps = await MapsRepository.GetAsync();
            foreach (DbMap dbmap in maps)
            {
                var map = new GameMap(dbmap);
                if (await map.InitializeAsync())
                {
//#if DEBUG
//                    logger.Debug($"Map[{map.Identity:000000}] MapDoc[{map.MapDoc:000000}] {map.Name,-32} Partition: {map.Partition:00} loaded...");
//#endif
                    GameMaps.TryAdd(map.Identity, map);
                }
                else
                {
                    logger.Error("Could not load map {Identity} {Name}", dbmap.Identity, dbmap.Name);
                }
            }

            List<DbDynamap> dynaMaps = await MapsRepository.GetDynaAsync();
            foreach (DbDynamap dbmap in dynaMaps)
            {
                var map = new GameMap(dbmap);
                if (await map.InitializeAsync())
                {
#if DEBUG
                    logger.Debug($"Dynamic Map [{map.Identity:0000000}] MapDoc[{map.MapDoc:000000}] {map.Name,-32} Partition: {map.Partition:00} loaded...");
#endif
                    GameMaps.TryAdd(map.Identity, map);
                }
            }

#if DEBUG
            const string partitionLogFile = "AiMapPartition";
            string path = Path.Combine(Environment.CurrentDirectory, $"{partitionLogFile}.log");
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using StreamWriter writer = new(path, false, Encoding.UTF8);
            foreach (var map in GameMaps.Values.OrderBy(x => x.Partition).ThenBy(x => x.Identity))
            {
                writer.WriteLine($"{map.Identity:0000000},{map.Name:-32},{map.Partition}");
            }
            writer.Flush();
#endif
        }

        public static GameMap GetMap(uint idMap)
        {
            return GameMaps.TryGetValue(idMap, out GameMap value) ? value : null;
        }

        public static bool AddMap(GameMap map)
        {
            if (GameMaps.TryAdd(map.Identity, map))
            {
                return true;
            }
            return false;
        }

        public static bool RemoveMap(uint idMap)
        {
            GameMaps.TryRemove(idMap, out GameMap map);
            return true;
        }
    }
}
