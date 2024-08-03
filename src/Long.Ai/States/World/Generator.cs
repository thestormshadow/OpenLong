using Long.Ai.Database.Repositories;
using Long.Ai.Managers;
using Long.Ai.Sockets;
using Long.Ai.Sockets.Packets;
using Long.Database.Entities;
using Long.Network.Packets.Ai;
using Long.Shared.Managers;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;

namespace Long.Ai.States.World
{
    public sealed class Generator
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<Generator>();
		private static uint idGenerator = 2000000;

        private readonly DbGenerator generator;
        private readonly DbMonstertype monstertype;
        private readonly Point center;
        private readonly GameMap gameMap;
        private readonly List<MonsterRatio> monsterRatios = new List<MonsterRatio>();

        private readonly ConcurrentDictionary<uint, TimeOut> awaitingReborn = new();
        private readonly ConcurrentDictionary<uint, Monster> monsters = new();

        private readonly TimeOut timeBetweenGens = new(1);

        public Generator(DbGenerator generator)
        {
            this.generator = generator;
            center = new Point(generator.BoundX + generator.BoundCx / 2, generator.BoundY + generator.BoundCy / 2);

            gameMap = MapManager.GetMap(generator.Mapid);
            if (gameMap == null)
            {
                logger.Error($"Could not load map [{generator.Mapid}] for generator [{generator.Id}]");
                return;
            }


            if (generator.Npctype != 0 && !HasCluster)
            {
                monstertype = RoleManager.GetMonstertype(generator.Npctype);
                if (monstertype == null)
                {
                    logger.Error($"Could not load monstertype [{generator.Npctype}] for generator [{generator.Id}]");
                    return;
                }
            }
            else if (HasCluster)
            {
                DbMonsterCluster cluster = MonsterClusterRepository.GetById(generator.Npctype);
                if (cluster != null)
                {
                    FillMonsterRatios(cluster);
                }
            }
        }

        public Generator(uint idMap, uint idMonster, ushort usX, ushort usY, ushort usCx, ushort usCy)
        {
            generator = new DbGenerator
            {
                Mapid = idMap,
                BoundX = usX,
                BoundY = usY,
                BoundCx = usCx,
                BoundCy = usCy,
                Npctype = idMonster,
                MaxNpc = 0,
                MaxPerGen = 0,
                Id = idGenerator++
            };

            center = new Point(generator.BoundX + generator.BoundCx / 2, generator.BoundY + generator.BoundCy / 2);

            gameMap = MapManager.GetMap(generator.Mapid);
            if (gameMap == null)
            {
                logger.Error($"Could not load map [{generator.Mapid}] for generator [{generator.Id}]");
                return;
            }

            if (idMonster != 0)
            {
                monstertype = RoleManager.GetMonstertype(generator.Npctype);
                if (monstertype == null)
                {
                    logger.Error($"Could not load monstertype [{generator.Npctype}] for generator [{generator.Id}]");
                    return;
                }
            }
        }

        public int Generated => monsters.Count;

        public bool Ready => (generator.Npctype == 0 || monsterRatios.Count > 0 || monstertype != null) && gameMap != null;

        public bool IsActive => gameMap.PlayerCount > 0 || monsters.Values.Any(x => !x.IsAlive || x.Map?.IsInstanceMap == true);

        public uint Identity => generator.Id;

        public uint RoleType => generator.Npctype;

        public int RestSeconds => Math.Max(generator.RestSecs, MIN_TIME_BETWEEN_GEN);

        public uint MapIdentity => generator?.Mapid ?? 0;

        public string MonsterName => monstertype?.Name ?? "None";

        public bool HasCluster => generator.ClusterType;

        private void FillMonsterRatios(DbMonsterCluster cluster)
        {
            int currentRatio = 0;
            if (cluster.Ratio0 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster0);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster0);
                }
                else
                {
                    currentRatio += cluster.Ratio0;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio1 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster1);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster1);
                }
                else
                {
                    currentRatio += cluster.Ratio1;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio2 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster2);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster2);
                }
                else
                {
                    currentRatio += cluster.Ratio2;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio3 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster3);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster3);
                }
                else
                {
                    currentRatio += cluster.Ratio3;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio4 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster4);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster4);
                }
                else
                {
                    currentRatio += cluster.Ratio4;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio5 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster5);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster5);
                }
                else
                {
                    currentRatio += cluster.Ratio5;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio6 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster6);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster6);
                }
                else
                {
                    currentRatio += cluster.Ratio6;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio7 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster7);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster7);
                }
                else
                {
                    currentRatio += cluster.Ratio7;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio8 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster8);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster8);
                }
                else
                {
                    currentRatio += cluster.Ratio8;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }

            if (cluster.Ratio9 != 0)
            {
                DbMonstertype monstertype = RoleManager.GetMonstertype(cluster.Monster9);
                if (monstertype == null)
                {
                    logger.Warning("Cluster {} has invalid monster type {}", cluster.Id, cluster.Monster9);
                }
                else
                {
                    currentRatio += cluster.Ratio9;
                    monsterRatios.Add(new MonsterRatio(monstertype) { Ratio = currentRatio });
                }
            }
        }

        public bool Add(Monster monster)
        {
            return monsters.TryAdd(monster.Identity, monster);
        }

        public async Task<Monster> GenerateMonsterAsync()
        {
            uint identity = (uint)IdentityManager.Monster.GetNextIdentity;
            Monster monster = null;
            if (!HasCluster)
            {
                monster = new Monster(monstertype, identity, this);
            }
            else
            {
                int rate = await NextAsync(monsterRatios.Max(x => x.Ratio));
                foreach (var ratio in monsterRatios)
                {
                    if (ratio.Ratio < rate)
                    {
                        monster = new Monster(ratio.MonsterType, identity, this);
                        break;
                    }
                }

                if (monster == null)
                {
                    var monsterType = monsterRatios.FirstOrDefault(x => x.MonsterType != null)?.MonsterType;
                    if (monsterType == null)
                    {
                        return null;
                    }

                    monster = new Monster(monsterType, identity, this);
                }
            }

            Point pos = await gameMap.QueryRandomPositionAsync(generator.BoundX, generator.BoundY, generator.BoundCx, generator.BoundCy);

            if (pos == default || !await monster.InitializeAsync(MapIdentity, (ushort)pos.X, (ushort)pos.Y))
            {
                IdentityManager.Monster.ReturnIdentity(monster.Identity);
                return null;
            }

            return monster;
        }

        public async Task<int> GenerateAsync()
        {
            if (MapIdentity == 1002)
            {

            }

            //if (!IsActive)
            //{
            //    return 0;
            //}

            foreach (var monster in awaitingReborn.Where(x => x.Value.IsTimeOut()))
            {
                monsters.TryRemove(monster.Key, out _);
                awaitingReborn.TryRemove(monster.Key, out _);
            }

            if (timeBetweenGens.ToNextTime())
            {
                int generate = Math.Min(generator.MaxPerGen - Generated, Math.Min(MAX_PER_GEN, generator.MaxNpc));
                if (generate > 0)
                {
                    while (generate-- > 0)
                    {
                        Monster monster = await GenerateMonsterAsync();
                        if (monster == null || !monsters.TryAdd(monster.Identity, monster))
                            continue;
                        await monster.EnterMapAsync();
                    }
                }
            }

            return await OnTimerAsync();
        }

        public async Task<int> OnTimerAsync()
        {
            if (gameMap.IsRaceTrack())
            {
                return 0;
            }

            int count = 0;
            foreach (var monster in monsters.Values.Where(x => x.IsAlive))
            {
                await monster.OnTimerAsync();
                count++;
            }
            return count;
        }

        public int SendAll()
        {
            var result = 0;
            MsgAiSpawnNpc msg = new();
            msg.Mode = AiSpawnNpcMode.Spawn;
            foreach (Monster npc in monsters.Values.Where(x => x.IsAlive))
            {
                if (msg.List.Count >= 25)
                {
                    result += msg.List.Count;
                    BroadcastMsg(msg);
                    msg.List.Clear();
                }

                msg.List.Add(new MsgAiSpawnNpc<GameServer>.SpawnNpc
                {
                    Id = npc.Identity,
                    GeneratorId = Identity,
                    MonsterType = npc.Type,
                    MapId = npc.MapIdentity,
                    X = npc.X,
                    Y = npc.Y
                });
            }

            if (msg.List.Count > 0)
            {
                result += msg.List.Count;
                BroadcastMsg(msg);
            }

            return result;
        }

        public Point GetCenter()
        {
            return center;
        }

        public bool IsTooFar(ushort x, ushort y, int nRange)
        {
            return !(x >= generator.BoundX - nRange
                     && x < generator.BoundX + generator.BoundCx + nRange
                     && y >= generator.BoundY - nRange
                     && y < generator.BoundY + generator.BoundCy + nRange);
        }

        public bool IsInRegion(int x, int y)
        {
            return x >= generator.BoundX && x < generator.BoundX + generator.BoundCx
                                       && y >= generator.BoundY && y < generator.BoundY + generator.BoundCy;
        }

        public int GetWidth()
        {
            return generator.BoundCx;
        }

        public int GetHeight()
        {
            return generator.BoundCy;
        }

        public int GetPosX()
        {
            return generator.BoundX;
        }

        public int GetPosY()
        {
            return generator.BoundY;
        }

        public void Remove(uint role)
        {
            if (generator.MaxPerGen == 0)
            {
                monsters.TryRemove(role, out _);
                return;
            }

            if (monsters.TryGetValue(role, out Monster mob))
            {
                var tm = new TimeOut();
                tm.Startup(RestSeconds);
                awaitingReborn.TryAdd(role, tm);
            }
        }

        public override string ToString()
        {
            return $"[{MapIdentity}]{MonsterName}";
        }

        private const int MAX_PER_GEN = 20;
        private const int MIN_TIME_BETWEEN_GEN = 5;
    }
}
