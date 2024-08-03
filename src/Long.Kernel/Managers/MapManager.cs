using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Processors;
using Long.Kernel.States.World;
using Long.Shared.Managers;
using System.Collections.Concurrent;
using System.Text;

namespace Long.Kernel.Managers
{
    public class MapManager
    {
        private static readonly ILogger logger = Log.ForContext<MapManager>();
        private static TimeOut instanceCheckTimer = new();

        public static ConcurrentDictionary<uint, GameMap> GameMaps { get; } = new();
        public static int ActiveMapCount => GameMaps.Values.Count(x => x.PlayerCount > 0);

        public static async Task InitializeAsync()
        {
            List<DbMap> maps = await MapsRepository.GetAsync();
            foreach (DbMap dbmap in maps)
            {
                var map = new GameMap(dbmap);
                if (await map.InitializeAsync())
                {
#if DEBUG
                    logger.Debug("Map[{0:000000}] MapDoc[{1:000000}] {2,-32} Partition: {3:00} loaded...", map.Identity, map.MapDoc, map.Name, map.Partition);
#endif
                    GameMaps.TryAdd(map.Identity, map);
                }
                else
                {
                    logger.Error("Could not load map {0} {1}", dbmap.Identity, dbmap.Name);
                }
            }

            List<DbDynamap> dynaMaps = await MapsRepository.GetDynaAsync();
            foreach (DbDynamap dbmap in dynaMaps)
            {
                var map = new GameMap(dbmap);
                if (await map.InitializeAsync())
                {
#if DEBUG
                    logger.Debug("Dynamic Map[{0:000000}] MapDoc[{1:000000}] {2,-32} Partition: {3:00} loaded...", map.Identity, map.MapDoc, map.Name, map.Partition);
#endif
                    GameMaps.TryAdd(map.Identity, map);
                }
            }

#if DEBUG
            const string partitionLogFile = "MapPartition";
            string path = Path.Combine(Environment.CurrentDirectory, $"{partitionLogFile}_{RealmManager.ServerIdentity}.log");
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using StreamWriter writer = new(path, false, Encoding.UTF8);
            foreach (var map in GameMaps.Values.OrderBy(x => x.Partition).ThenBy(x => x.Identity))
            {
                await writer.WriteLineAsync($"{map.Identity:0000000},{map.Name:-32},{map.Partition}");
            }
            await writer.FlushAsync();
#endif
        }

        public static GameMap GetMap(int idMap)
        {
            if (GameMaps.TryGetValue((uint)idMap, out GameMap value))
            {
                return value;
            }
            return null;
        }

        public static GameMap GetMap(uint idMap)
        {
            if (GameMaps.TryGetValue(idMap, out GameMap value))
            {
                return value;
            }
            return null;
        }

        public static InstanceMap FindInstanceByUser(uint instanceId, uint userId)
        {
            return GameMaps.Values
                .Where(x => x is InstanceMap)
                .Cast<InstanceMap>()
                .FirstOrDefault(x => x.InstanceType == instanceId && x.OwnerIdentity == userId);
        }

		public static async Task<bool> AddMapAsync(GameMap map)
		{
			if (GameMaps.TryAdd(map.Identity, map))
			{
				await map.SendAddToNpcServerAsync();
				return true;
			}

			return false;
		}

		public static async Task<bool> RemoveMapAsync(uint idMap)
		{
			if (GameMaps.TryRemove(idMap, out GameMap map))
			{
				await map.SendRemoveToNpcServerAsync();
			}
			return true;
		}

		public static async Task OnTimerAsync()
        {
            if (instanceCheckTimer.ToNextTime(1))
            {
                foreach (var map in GameMaps.Values
                    .Where(x => x is InstanceMap)
                    .Cast<InstanceMap>())
                {
                    if (map.HasExpired && map.PlayerCount > 0)
                    {
                        WorldProcessor.Instance.Queue(map.Partition, map.OnTimeOverAsync);
                    }
                    
                    if (map.HasExpired)
                    {
                        WorldProcessor.Instance.Queue(map.Partition, async () =>
                        {
                            await RemoveMapAsync(map.Identity);
                            IdentityManager.Instances.ReturnIdentity(map.Identity);
                        });
                    }
                }
            }
        }
    }
}
