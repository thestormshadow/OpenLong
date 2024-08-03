using Long.Ai.Managers;
using Long.Ai.States;
using Long.Ai.States.World;
using Long.Database.Entities;
using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiRoleLogin : MsgAiRoleLogin<GameServer>
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiRoleLogin>();

		public override async Task ProcessAsync(GameServer client)
        {
            switch (Data.NpcType)
            {
                case RoleLoginNpcType.Monster:
                    {
                        // must not use
                        break;
                    }

                case RoleLoginNpcType.CallPet:
                    {
                        DbMonstertype monsterType = RoleManager.GetMonstertype((uint)Data.LookFace);
                        if (monsterType == null)
                        {
                            logger.Warning($"Could not create monster for type {Data.LookFace}");
                            return;
                        }

                        GameMap map = MapManager.GetMap(Data.MapId);
                        if (map == null)
                        {
                            logger.Warning($"Could not create monster for map {Data.MapId}");
                            return;
                        }

                        Monster pet = new Monster(monsterType, Data.Identity, new Generator(Data.MapId, (uint)Data.LookFace, Data.MapX, Data.MapY, 1, 1));
                        if (!await pet.InitializeAsync(Data.MapId, Data.MapX, Data.MapY))
                        {
                            return;
                        }

                        await pet.EnterMapAsync(false);
                        RoleManager.AddRole(pet);
                        break;
                    }
            }
        }
    }
}
