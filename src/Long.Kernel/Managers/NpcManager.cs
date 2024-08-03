using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Npcs;

namespace Long.Kernel.Managers
{
    public class NpcManager
    {
        private static readonly ILogger logger = Log.ForContext<NpcManager>();

        public static async Task InitializeAsync()
        {
            logger.Information("Loading NPCs");
            foreach (DbNpc dbNpc in await NpcRepository.GetAsync())
            {
                Npc npc = new Npc(dbNpc);

                if (!await npc.InitializeAsync())
                {
                    logger.Warning("Could not load NPC {0} {1}", dbNpc.Id, dbNpc.Name);
                    continue;
                }

                await npc.EnterMapAsync();
                if (npc.Task0 != 0 && ScriptManager.GetTask(npc.Task0) == null)
                {
                    logger.Warning("Npc {0} {1} no task found [taskid: {2}]", npc.Identity, npc.Name, npc.Task0);
                }
            }

            foreach (DbDynanpc dbDynaNpc in await NpcRepository.GetDynamicAsync())
            {
                try
                {
                    DynamicNpc npc = new DynamicNpc(dbDynaNpc);
                    if (!await npc.InitializeAsync())
                    {
                        logger.Warning("Could not load NPC {0} {1}", dbDynaNpc.Id, dbDynaNpc.Name);
                        continue;
                    }

                    await npc.EnterMapAsync();
                    if (npc.Task0 != 0 && ScriptManager.GetTask(npc.Task0) == null)
                    {
                        logger.Warning("Npc {0} {1} no task found [taskid: {2}]", npc.Identity, npc.Name, npc.Task0);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error initializing NPC({0},{1})! Message: {2}", dbDynaNpc.Id, dbDynaNpc.Name, ex.Message);
                }
            }
        }
    }
}
