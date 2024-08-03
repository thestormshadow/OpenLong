using Long.Ai.Managers;
using Long.Ai.States.World;
using Long.Database.Entities;
using Long.Network.Packets.Ai;
using Serilog;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiDynaMap : MsgAiDynaMap<GameServer>
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<MsgAiDynaMap>();

		public override async Task ProcessAsync(GameServer client)
        {
            if (Data.Mode == 0) // add
            {
                var dynaMap = new DbDynamap
                {
                    Identity = Data.Identity,
                    Name = Data.Name,
                    Description = Data.Description,
                    Type = Data.MapType,
                    LinkMap = Data.LinkMap,
                    LinkX = Data.LinkX,
                    LinkY = Data.LinkY,
                    MapDoc = Data.MapDoc,
                    MapGroup = Data.MapGroup,
                    OwnerIdentity = Data.OwnerIdentity,
                    OwnerType = Data.OwnerType,
                    PortalX = Data.PortalX,
                    PortalY = Data.PortalY,
                    RebornMap = Data.RebornMap,
                    RebornPortal = Data.RebornPortal,
                    ResourceLevel = Data.ResourceLevel,
                    ServerIndex = Data.ServerIndex,
                    Weather = Data.Weather,
                    BackgroundMusic = Data.BackgroundMusic,
                    BackgroundMusicShow = Data.BackgroundMusicShow,
                    Color = Data.Color
                };

                var map = new GameMap(dynaMap);
                if (!await map.InitializeAsync())
                    return;

                MapManager.AddMap(map);

                if (map.IsInstanceMap)
                {
                    var generators = GeneratorManager.GetByMapId(Data.InstanceMapId);
                    foreach (var generator in generators)
                    {
                        await GeneratorManager.AddGeneratorAsync(new Generator(Data.Identity, generator.Npctype, generator.BoundX, generator.BoundY, generator.BoundCx, generator.BoundCy));
                    }
                }

#if DEBUG
                logger.Debug($"Map {map.Identity} {map.Name} {Data.Description} has been added to the pool.");
#endif
            }
            else
            {
                GameMap map = MapManager.GetMap(Data.Identity);
                if (map != null)
                {
                    if (map.IsInstanceMap)
                    {
                        var generators = GeneratorManager.GetGenerators(map.Identity);
                        foreach (var generator in generators)
                        {
                            GeneratorManager.RemoveGenerator(generator.Identity);
                        }
                    }
                    MapManager.RemoveMap(Data.Identity);
                }               

#if DEBUG
                logger.Debug($"Map {Data.Identity} has been removed from the pool.");
#endif
            }
        }
    }
}
