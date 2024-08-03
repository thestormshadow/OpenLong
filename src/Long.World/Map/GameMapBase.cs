using Long.World.Enums;
using Long.World.Roles;
using Serilog;

namespace Long.World.Map
{
    public abstract class GameMapBase<TRole, TUser> where TRole : WorldObject
    {
        private static readonly ILogger logger = Log.ForContext<GameMapBase<TRole, TUser>>();

        private MapBlock<TRole, TUser>[,] blocks;
        protected GameMapData mapData;

        public virtual uint Identity { get; }
        public virtual string Name { get; }
        public virtual uint OwnerIdentity { get; set; }
        public virtual uint MapDoc { get; set; }
        public virtual MapTypeFlag Type { get; }

        public ulong Flag { get; set; }

        public int Width => mapData?.Width ?? 0;
        public int Height => mapData?.Height ?? 0;

        public int BlocksX => (int)Math.Ceiling(Width / (double)MapBlock<TRole, TUser>.BLOCK_SIZE);
        public int BlocksY => (int)Math.Ceiling(Height / (double)MapBlock<TRole, TUser>.BLOCK_SIZE);

        public virtual Task<bool> InitializeAsync()
        {
            return Task.FromResult(UpdateMapData(MapDoc));
        }

        public bool UpdateMapData(uint mapDoc)
        {
            mapData = MapDataManager.GetMapData(mapDoc);
            if (mapData == null)
            {
                logger.Error("Could not load map {0}({1}): map data not found", Identity, MapDoc);
                return false;
            }

            blocks = new MapBlock<TRole, TUser>[BlocksX, BlocksY];
            for (var y = 0; y < BlocksY; y++)
            {
                for (var x = 0; x < BlocksX; x++)
                {
                    blocks[x, y] = new MapBlock<TRole, TUser>(GameMapData.Pos2Index(x, y, BlocksX));
                }
            }

            return true;
        }

        #region Tiles

        public Tile this[int x, int y] => mapData[x, y];

        #endregion

        #region Terrain

        public int GetFloorAlt(int x, int y)
        {
            return mapData.GetFloorAlt(x, y);
        }

        public int GetFloorAttr(int x, int y)
        {
            return mapData.GetFloorAttr(x, y);
        }

        public int GetFloorMask(int x, int y)
        {
            return mapData.GetFloorMask(x, y);
        }

        #endregion

        #region Blocks

        public void EnterBlock(TRole role, int newX, int newY, int oldX = 0, int oldY = 0)
        {
            int currentBlockX = GetBlockX(newX);
            int currentBlockY = GetBlockY(newY);

            int oldBlockX = GetBlockX(oldX);
            int oldBlockY = GetBlockY(oldY);

            if (currentBlockX != oldBlockX || currentBlockY != oldBlockY)
            {
                if (GetBlock(oldBlockX, oldBlockY)?.RoleSet.ContainsKey(role.Identity) == true)
                {
                    LeaveBlock(role);
                }

                GetBlock(currentBlockX, currentBlockY)?.Add(role);
            }
        }

        public void LeaveBlock(TRole role)
        {
            GetBlock(GetBlockX(role.X), GetBlockY(role.Y))?.Remove(role);
        }

        public MapBlock<TRole, TUser> GetBlock(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BlocksX || y >= BlocksY)
            {
                return null;
            }

            return blocks[x, y];
        }

        public List<TRole> Query9BlocksByPos(int x, int y)
        {
            return Query9Blocks(GetBlockX(x), GetBlockY(y));
        }

        public List<TRole> Query9Blocks(int x, int y)
        {
            var result = new List<TRole>();
            for (var aroundBlock = 0; aroundBlock < GameMapData.WalkXCoords.Length; aroundBlock++)
            {
                int viewBlockX = x + GameMapData.WalkXCoords[aroundBlock];
                int viewBlockY = y + GameMapData.WalkYCoords[aroundBlock];
                if (viewBlockX < 0 || viewBlockY < 0 || viewBlockX >= BlocksX || viewBlockY >= BlocksY)
                {
                    continue;
                }

                result.AddRange(GetBlock(viewBlockX, viewBlockY).RoleSet.Values);
            }
            return result.DistinctBy(x => x.Identity).ToList();
        }

        public static int GetBlockX(int x)
        {
            return x / MapBlock<TRole, TUser>.BLOCK_SIZE;
        }

        public static int GetBlockY(int y)
        {
            return y / MapBlock<TRole, TUser>.BLOCK_SIZE;
        }

        #endregion

        #region Position Check

        /// <summary>
        ///     Determinate if a coordinate is valid inside of a map.
        /// </summary>
        /// <returns></returns>
        public bool IsValidPoint(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool IsStandEnable(int x, int y)
        {
            return mapData.GetFloorMask(x, y) == 0;
        }

        public bool IsMoveEnable(int x, int y)
        {
            if (!IsValidPoint(x, y))
            {
                return false;
            }

            if (mapData.GetFloorMask(x, y) != 0)
            {
                return false;
            }

            return true;
        }

        public bool IsAltEnable(int sX, int sY, int x, int y, int altDiff = 26)
        {
            if (!IsValidPoint(x, y))
            {
                return false;
            }

            if (Math.Abs(mapData.GetFloorAlt(x, y) - mapData.GetFloorAlt(sX, sY)) >= altDiff)
            {
                return false;
            }

            return true;
        }

        public bool IsAltOver(int x, int y, int alt)
        {
            if (!IsValidPoint(x, y))
            {
                return false;
            }

            int floorAlt = mapData.GetFloorAlt(x, y);
            if (floorAlt > alt)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Map Checks

        /// <summary>
        ///     Checks if the map is a pk field. Wont add pk points.
        /// </summary>
        public bool IsPkField()
        {
            return Type.HasFlag(MapTypeFlag.PkField);
        }

        /// <summary>
        ///     Disable teleporting by skills or scrolls.
        /// </summary>
        public bool IsChgMapDisable()
        {
            return Type.HasFlag(MapTypeFlag.ChangeMapDisable);
        }

        /// <summary>
        ///     Disable recording the map position into the database.
        /// </summary>
        public bool IsRecordDisable()
        {
            return Type.HasFlag(MapTypeFlag.RecordDisable);
        }

        /// <summary>
        ///     Disable team creation into the map.
        /// </summary>
        public bool IsTeamDisable()
        {
            return Type.HasFlag(MapTypeFlag.TeamDisable);
        }

        /// <summary>
        ///     Disable use of pk on the map.
        /// </summary>
        public bool IsPkDisable()
        {
            return Type.HasFlag(MapTypeFlag.PkDisable);
        }

        /// <summary>
        ///     Disable teleporting by actions.
        /// </summary>
        public bool IsTeleportDisable()
        {
            return Type.HasFlag(MapTypeFlag.TeleportDisable);
        }

        /// <summary>
        ///     Checks if the map is a syndicate map
        /// </summary>
        /// <returns></returns>
        public bool IsSynMap()
        {
            return Type.HasFlag(MapTypeFlag.GuildMap);
        }

        /// <summary>
        ///     Checks if the map is a prision
        /// </summary>
        public bool IsPrisionMap()
        {
            return Type.HasFlag(MapTypeFlag.PrisonMap);
        }

        /// <summary>
        ///     If the map enable the fly skill.
        /// </summary>
        public bool IsWingDisable()
        {
            return Type.HasFlag(MapTypeFlag.WingDisable);
        }

        /// <summary>
        ///     Check if the map is in war.
        /// </summary>
        public bool IsWarTime()
        {
            return (Flag & 1) != 0;
        }

        /// <summary>
        ///     Check if the map is the training ground. [1039]
        /// </summary>
        public bool IsTrainingMap()
        {
            return Identity == 1039;
        }

        /// <summary>
        ///     Check if its the family (clan) map.
        /// </summary>
        public bool IsFamilyMap()
        {
            return Type.HasFlag(MapTypeFlag.Family);
        }

        /// <summary>
        ///     If the map enables booth to be built.
        /// </summary>
        public bool IsBoothEnable()
        {
            return Type.HasFlag(MapTypeFlag.BoothEnable);
        }

        public bool IsNewbieProtect()
        {
            return Type.HasFlag(MapTypeFlag.NewbieProtect);
        }

        public bool IsCallNewbieDisable()
        {
            return Type.HasFlag(MapTypeFlag.CallNewbieDisable);
        }

        public bool IsMineField()
        {
            return Type.HasFlag(MapTypeFlag.MineField);
        }

        public bool IsArenicMap()
        {
            return Type.HasFlag(MapTypeFlag.ArenicMap);
        }

        /// <summary>
        /// Map is couple PK map?
        /// </summary>
        public bool IsDoublePkMap() 
        {
            return Type.HasFlag(MapTypeFlag.DoublePkMap);
        }

        public bool IsRaceTrack()
        {
            return Type.HasFlag(MapTypeFlag.RaceTrackMap);
        }

        public bool IsFamilyArenicMap()
        {
            return Type.HasFlag(MapTypeFlag.FamilyArenicMap);
        }

        public bool IsFactionPkMap()
        {
            return Type.HasFlag(MapTypeFlag.FactionPkMap);
        }

        public bool IsEliteMap()
        {
            return Type.HasFlag(MapTypeFlag.EliteMap);
        }

        public bool IsTeamPkArenicMap()
        {
            return Type.HasFlag(MapTypeFlag.TeamPkArenicMap);
        }

        public bool IsTeamArenaMap()
        {
            return Type.HasFlag(MapTypeFlag.TeamArenaMap);
        }

        public bool IsBattleEffectLimitMap()
        {
            return Type.HasFlag(MapTypeFlag.BattleEffectLimitMap);
        }

        public bool IsTeamPopPkMap()
        {
            return Type.HasFlag(MapTypeFlag.TeamPopPkMap);
        }

        public bool IsNoExpMap()
        {
            return Type.HasFlag(MapTypeFlag.NoExpMap);
        }

        public bool IsAutoHungUpMap()
        {
            return !IsNoExpMap();
        }

        public bool IsGoldenLeagueAdditionLevelLimit()
        {
            return Type.HasFlag(MapTypeFlag.GoldenLeagueAdditionLevelLimit);
        }

        public bool IsForbidCampMap()
        {
            return Type.HasFlag(MapTypeFlag.ForbidCampMap);
        }

        public bool IsGoldenLeagueMap()
        {
            return Type.HasFlag(MapTypeFlag.GoldenLeagueMap);
        }

        public bool IsJiangHuMap()
        {
            return Type.HasFlag(MapTypeFlag.JiangHuMap);
        }

        public bool IsArenicMapInGeneral()
        {
            return IsArenicMap() || IsEliteMap() || IsTeamPkArenicMap() || IsTeamArenaMap() || IsTeamPopPkMap() || IsGoldenLeagueMap();
        }

        public bool IsDynamicMap()
        {
            return Identity > 999999;
        }

        #endregion
    }
}
