using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.States;
using Long.Kernel.States.World;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiSpawnNpc : MsgAiSpawnNpc<AiClient>
    {
        public override async Task ProcessAsync(AiClient client)
        {
            switch (Mode)
            {
                case AiSpawnNpcMode.Spawn:
                    {
                        var msg = new MsgAiSpawnNpc
                        {
                            Mode = AiSpawnNpcMode.DestroyNpc
                        };
                        // Spawning ai npc to the world! npc server manages the monsters ids, probably no need to check
                        foreach (SpawnNpc npc in List)
                        {
							GameMap map = MapManager.GetMap(npc.MapId);
                            if (map == null)
                            {
                                // send result back?
                                msg.List.Add(npc);
                                continue;
                            }

                            DbMonstertype dbMonstertype = RoleManager.GetMonstertype(npc.MonsterType);
                            if (dbMonstertype == null)
                            {
                                // send result back?
                                msg.List.Add(npc);
                                continue;
                            }

                            var monster = new Monster(dbMonstertype, npc.Id, npc.GeneratorId, npc.OwnerId);
                            if (!await monster.InitializeAsync(npc.MapId, npc.X, npc.Y))
                            {
                                // send result back?
                                msg.List.Add(npc);
                                continue;
                            }

                            // Queue the map interaction
                            monster.QueueAction(monster.EnterMapAsync);
                        }
                        break;
                    }

                case AiSpawnNpcMode.DestroyNpc:
                    {
                        foreach (SpawnNpc npc in List)
                        {
                            Role role = RoleManager.GetRole(npc.Id);
                            if (role == null || role is not Monster)
                                continue;
                            role.QueueAction(role.LeaveMapAsync);
                        }
                        break;
                    }
            }
        }
    }
}
