using Long.World.Map;
using Serilog;
using System.Collections.Concurrent;

namespace Long.World
{
    public class MapDataManager
    {
        private static readonly ILogger logger = Log.ForContext<MapDataManager>();

        public static async Task LoadDataAsync()
        {
            FileStream stream = File.OpenRead(Path.Combine(Environment.CurrentDirectory, "ini", "GameMap.dat"));
            var reader = new BinaryReader(stream);

            int mapDataCount = reader.ReadInt32();
            logger.Debug("Loading {0} maps...", mapDataCount);

            for (var i = 0; i < mapDataCount; i++)
            {
                uint idMap = reader.ReadUInt32();
                int length = reader.ReadInt32();
                var name = new string(reader.ReadChars(length));
                uint puzzle = reader.ReadUInt32();

                mapData.TryAdd(idMap, new MapData
                {
                    ID = idMap,
                    Length = length,
                    Name = name,
                    Puzzle = puzzle
                });
            }

            reader.Close();
            stream.Close();
            reader.Dispose();
            await stream.DisposeAsync();
        }

        public static GameMapData GetMapData(uint idDoc)
        {
            if (!MapDataManager.mapData.TryGetValue(idDoc, out MapData value))
            {
                return null;
            }

            GameMapData mapData = new(idDoc);
            if (mapData.Load(value.Name.Replace("\\", Path.DirectorySeparatorChar.ToString())))
            {
                return mapData;
            }
            return null;
        }

        private struct MapData
        {
            public uint ID;
            public int Length;
            public string Name;
            public uint Puzzle;
        }

        private static readonly ConcurrentDictionary<uint, MapData> mapData = new();
    }
}
