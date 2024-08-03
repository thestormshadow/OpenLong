using Long.Ai.Database;
using Long.Ai.Managers;
using Long.Ai.States.World;
using Long.Database.Entities;
using Long.Network.Packets.Ai;

namespace Long.Ai.Sockets.Packets
{
    public sealed class MsgAiGeneratorManage : MsgAiGeneratorManage<GameServer>
    {
        public override async Task ProcessAsync(GameServer client)
        {
            var dbGen = new DbGenerator
            {
                Mapid = Data.MapId,
                BoundX = Data.BoundX,
                BoundY = Data.BoundY,
                BoundCx = Data.BoundCx,
                BoundCy = Data.BoundCy,
                MaxNpc = Data.MaxNpc,
                MaxPerGen = Data.MaxPerGen,
                Npctype = Data.Npctype,
                RestSecs = Data.RestSecs,
                TimerBegin = Data.TimerBegin,
                TimerEnd = Data.TimerEnd,
                BornX = Data.BornX,
                BornY = Data.BornY,
            };

            if (await ServerDbContext.SaveAsync(dbGen))
            {
                Generator generator = new Generator(dbGen);
                if (generator.Ready)
                {
                    await GeneratorManager.AddGeneratorAsync(generator);
                }
            }
        }
    }
}
