using Long.Shared.Mathematics;
using Microsoft.Extensions.Configuration.Ini;
using Serilog;
using SharpCompress.Archives;
using SharpCompress.Readers;
using System.Drawing;
using System.Text;

namespace Long.World.Map
{
    public class GameMapData
    {
        private static readonly ILogger logger = Log.ForContext<GameMapData>();

        private readonly uint idDoc;
        private readonly List<MapObject> mapObjects = new();
        private readonly List<PassageData> passageData = new();

        private readonly List<List<Layer>> setLayer = new();
        private Tile[,] cell;
        private Tile @default;

        public GameMapData(uint idMapDoc)
        {
            idDoc = idMapDoc;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ref Tile this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                {
                    return ref @default;
                }

                return ref cell[x, y];
            }
        }

        public int GetPassage(int x, int y)
        {
            for (var cx = 0; cx < 9; cx++)
            {
                for (var cy = 0; cy < 9; cy++)
                {
                    int testX = x + WalkXCoords[cx];
                    int testY = y + WalkYCoords[cy];

                    for (var i = 0; i < passageData.Count; i++)
                    {
                        if (passageData[i].X == testX
                            && passageData[i].Y == testY)
                        {
                            return passageData[i].Index;
                        }
                    }
                }
            }

            return -1;
        }

        public int GetClosestPassage(int x, int y)
        {
            var passage = passageData.OrderBy(p => Calculations.GetDistance(x, y, p.X, p.Y)).FirstOrDefault();
            if (!passage.Equals(default) && Calculations.GetDistance(x, y, passage.X, passage.Y) <= 7)
            {
                return passage.Index;
            }
            return -1;
        }

        public bool Load(string path)
        {
            //path = path.Replace(".7z", ".dmap");
            string realPath = GetRealPath(path);
            if (FileExists(realPath))
            {
                if (string.IsNullOrEmpty(realPath))
                {
                    logger.Warning("Map data for file {0} {1} (realPath:{2}) has not been found.", idDoc, path, realPath);
                    return false;
                }

                Stream stream;
                if (realPath.EndsWith(".7z"))
                {
                    stream = Read7zipFile(realPath);
                }
                else
                {
                    stream = File.OpenRead(realPath);
                }
                 
                using BinaryReader reader = new(stream, Encoding.ASCII);

                LoadData(reader);
                LoadPassageData(reader);
                LoadLayerData(reader);

                reader.Close();
                reader.Dispose();
                return true;
            }

            logger.Warning("Map data for file {0} {1} has not been found.", idDoc, path);
            return false;
        }

        private void LoadData(BinaryReader reader)
        {
            reader.ReadUInt32(); // version
            reader.ReadUInt32(); // data
            reader.ReadBytes(260); // pul file
            Width = reader.ReadInt32();
            Height = reader.ReadInt32();

            cell = new Tile[Width, Height];
            for (var y = 0; y < Height; y++)
            {
                uint checkSum = 0;
                for (var x = 0; x < Width; x++)
                {
                    short access = reader.ReadInt16();
                    short surface = reader.ReadInt16();
                    short elevation = reader.ReadInt16();

                    checkSum += (uint)((uint)access * (surface + y + 1) +
                                        (elevation + 2) * (x + 1 + surface));

                    cell[x, y] = new Tile(elevation, access, surface);
                }

                uint tmp = reader.ReadUInt32();
                if (checkSum != tmp)
                {
                    logger.Error("Invalid checksum for block of cells (mapdata: {IdDoc}), y: {Y}", idDoc, y);
                }
            }
        }

        private void LoadPassageData(BinaryReader reader)
        {
            int count = reader.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                int index = reader.ReadInt32();
                passageData.Add(new PassageData(x, y, index));
            }
        }

        private void LoadLayerData(BinaryReader reader)
        {
            int count = reader.ReadInt32();

            for (var i = 0; i < count; i++)
            {
                int type = reader.ReadInt32();

                switch (type)
                {
                    case MAP_COVER:
                        reader.ReadChars(416);
                        break;

                    case MAP_TERRAIN:
                        var file = new string(reader.ReadChars(260));
                        int startX = reader.ReadInt32();
                        int startY = reader.ReadInt32();

                        file = file[..file.IndexOf('\0')].Replace("\\", Path.DirectorySeparatorChar.ToString());
                        if (File.Exists(file))
                        {
                            var scenery = new BinaryReader(File.OpenRead(file));
                            var objTerrain = TerrainObject.CreateNew(scenery);
                            objTerrain.SetPos(new Point(startX, startY));
                            AddMapObject(objTerrain);
                            scenery.Close();
                            scenery.Dispose();
                        }

                        break;

                    case MAP_SCENE:
                        break;

                    case MAP_SOUND:
                        reader.ReadChars(276);
                        break;

                    case MAP_3DEFFECT:
                        reader.ReadChars(72);
                        break;

                    case MAP_3DEFFECTNEW:
                        reader.ReadChars(276);
                        break;
                }
            }
        }

        public int GetFloorAttr(int x, int y)
        {
            Tile cell = this[x, y];
            if (cell.Equals(@default))
            {
                return 0;
            }

            return cell.GetFloorAttr(setLayer);
        }

        public int GetFloorAlt(int x, int y)
        {
            Tile cell = this[x, y];
            if (cell.Equals(@default))
            {
                return 0;
            }

            return cell.GetFloorAlt(setLayer);
        }

        public int GetFloorMask(int x, int y)
        {
            Tile cell = this[x, y];
            if (cell.Equals(@default))
            {
                return 0;
            }

            return cell.GetFloorMask(setLayer);
        }

        /// <summary>
        ///     This method has been created to avoid the case sensitive Linux path system, which would case us some trouble.
        /// </summary>
        private static bool FileExists(string path)
        {
            string name = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            foreach (string file in Directory.GetFiles(name))
            {
                if (file.Equals(
                        path.Replace("\\", Path.DirectorySeparatorChar.ToString())
                            .Replace("/", Path.DirectorySeparatorChar.ToString()),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetRealPath(string path)
        {
            foreach (string file in Directory.GetFiles(Path.GetDirectoryName(path)))
            {
                if (file.Equals(
                        path.Replace("\\", Path.DirectorySeparatorChar.ToString())
                            .Replace("/", Path.DirectorySeparatorChar.ToString()),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    return file;
                }
            }

            return path;
        }

        public const int MAP_NONE = 0;
        public const int MAP_TERRAIN = 1;
        public const int MAP_TERRAIN_PART = 2;
        public const int MAP_SCENE = 3;
        public const int MAP_COVER = 4;
        public const int MAP_ROLE = 5;
        public const int MAP_HERO = 6;
        public const int MAP_PLAYER = 7;
        public const int MAP_PUZZLE = 8;
        public const int MAP_3DSIMPLE = 9;
        public const int MAP_3DEFFECT = 10;
        public const int MAP_2DITEM = 11;
        public const int MAP_3DNPC = 12;
        public const int MAP_3DOBJ = 13;
        public const int MAP_3DTRACE = 14;
        public const int MAP_SOUND = 15;
        public const int MAP_2DREGION = 16;
        public const int MAP_3DMAGICMAPITEM = 17;
        public const int MAP_3DITEM = 18;
        public const int MAP_3DEFFECTNEW = 19;
        public static readonly sbyte[] WalkXCoords = { 0, -1, -1, -1, 0, 1, 1, 1, 0 };
        public static readonly sbyte[] WalkYCoords = { 1, 1, 0, -1, -1, -1, 0, 1, 0 };

        public static readonly sbyte[] RideXCoords =
            {0, -2, -2, -2, 0, 2, 2, 2, -1, -2, -2, -1, 1, 2, 2, 1, -1, -2, -2, -1, 1, 2, 2, 1};

        public static readonly sbyte[] RideYCoords =
            {2, 2, 0, -2, -2, -2, 0, 2, 2, 1, -1, -2, -2, -1, 1, 2, 2, 1, -1, -2, -2, -1, 1, 2};

        #region Indexes

        public static int Pos2Index(int x, int y, int cx)
        {
            return x + y * cx;
        }

        public static int Index2X(int idx, int cy)
        {
            return idx % cy;
        }

        public static int Index2Y(int idx, int cy)
        {
            return idx / cy;
        }

        #endregion

        #region Map Object

        public bool AddMapObject(TerrainObject terrain)
        {
            if (terrain == null)
            {
                return false;
            }

            mapObjects.Add(terrain);
            return PlaceTerrainObject(terrain);
        }

        public bool DelMapObject(int idx)
        {
            if (idx < 0 || idx >= mapObjects.Count)
            {
                return false;
            }

            if (mapObjects[idx] is TerrainObject terrain && DisplaceTerrainObject(terrain))
            {
                mapObjects.RemoveAt(idx);
                return true;
            }

            return false;
        }

        public bool PlaceTerrainObject(TerrainObject terrain)
        {
            if (terrain == null)
            {
                return false;
            }

            try
            {
                //Console.WriteLine("================= Start =================");
                foreach (TerrainObjectPart part in terrain.Parts)
                {
                    for (var j = 0; j < part.SizeBaseCX; j++)
                    {
                        for (var k = 0; k < part.SizeBaseCY; k++)
                        {
                            Layer layer = part.GetLayer(j, k);
                            if (layer.Equals(default))
                            {
                                return false;
                            }

                            int x = terrain.X + part.PosSceneOffsetX + j - part.SizeBaseCX;
                            int y = terrain.Y + part.PosSceneOffsetY + k - part.SizeBaseCY;
                            if (x < 0 || y < 0)
                            {
                                continue;
                            }
                            if (x >= Width || y >= Height)
                            {
                                continue;
                            }
                            ref Tile tile = ref cell[x, y];
                            if (tile.Equals(default))
                            {
                                return false;
                            }
                            tile.AddLayer(setLayer, new Layer
                            {
                                Altitude = layer.Altitude,
                                Mask = layer.Mask,
                                Terrain = layer.Terrain
                            });
                        }
                    }
                }
                //Console.WriteLine("=================  End  =================");

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to add terrain object on doc [{0}] {1},{2}", idDoc, terrain.X, terrain.Y);
                return false;
            }
        }

        public bool DisplaceTerrainObject(TerrainObject terrain)
        {
            if (terrain == null)
            {
                return false;
            }

            foreach (TerrainObjectPart part in terrain.Parts)
            {
                for (var j = 0; j < part.SizeBaseCX; j++)
                {
                    for (var k = 0; k < part.SizeBaseCY; k++)
                    {
                        int x = terrain.X + part.PosSceneOffsetX + j - part.SizeBaseCX;
                        int y = terrain.Y + part.PosSceneOffsetY + k - part.SizeBaseCY;
                        if (x < 0 || y < 0)
                        {
                            continue;
                        }
                        ref Tile tile = ref cell[x, y];
                        if (tile.Equals(default))
                        {
                            return false;
                        }
                        tile.DelLayer(setLayer);
                    }
                }
            }

            return true;
        }

        #endregion

        #region Terrain Object

        public bool AddTerrainItem(uint idOwner, int x, int y, uint idTerrainType)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "ini");
            IniConfigurationSource source = new();
            string[] content = Directory.GetFiles(path);
            string fileName =
                content.FirstOrDefault(x => x.ToLower()
                                             .EndsWith("terrainnpc.ini", StringComparison.CurrentCultureIgnoreCase));
            path = Path.Combine(path, fileName);
            source.ReloadOnChange = false;
            IniConfigurationProvider reader = new(source);
            try
            {
                reader.Load(File.OpenRead(path));
            }
            catch
            {
                return false;
            }

            string entry = $"NpcType{idTerrainType / 10}:Dir{idTerrainType % 10}";
            if (!reader.TryGet(entry, out path))
            {
                return false;
            }

            var terrain = TerrainObject.CreateNew(new BinaryReader(File.OpenRead(path)), idOwner);
            if (terrain == null)
            {
                return false;
            }

            terrain.SetPos(new Point(x, y));
            if (!AddMapObject(terrain))
            {
                return false;
            }
            return true;
        }

        public bool DelTerrainItem(uint idOwner)
        {
            for (var i = 0; i < mapObjects.Count; i++)
            {
                if (mapObjects[i] is TerrainObject terrain)
                {
                    if (terrain.OwnerIdentity == idOwner)
                    {
                        return DelMapObject(i);
                    }
                }
            }

            return true;
        }

        #endregion

        #region Unzip Data

        private Stream Read7zipFile(string path)
        {
            if (ArchiveFactory.IsArchive(path, out _))
            {
                using MemoryStream stream = new MemoryStream();
                using IArchive archive = ArchiveFactory.Open(path);
                using IReader reader = archive.ExtractAllEntries();
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        using var entry = reader.OpenEntryStream();
                        entry.CopyTo(stream);
                    }
                }
                return new MemoryStream(stream.ToArray());
            }
            return null;
        }
        #endregion
    }

    public struct PassageData
    {
        public PassageData(int x, int y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }

        public int X;
        public int Y;
        public int Index;
    }
}
