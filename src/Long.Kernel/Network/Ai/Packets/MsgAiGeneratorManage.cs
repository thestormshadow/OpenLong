using Long.Database.Entities;
using Long.Kernel.Network.Ai;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiGeneratorManage : MsgAiGeneratorManage<AiClient>
    {
        public MsgAiGeneratorManage(DbGenerator gen)
        {
            Data = new MsgAiGeneratorManageContract
			{
				MapId = gen.Mapid,
				BoundX = gen.BoundX,
				BoundY = gen.BoundY,
				BoundCx = gen.BoundCx,
				BoundCy = gen.BoundCy,
				MaxNpc = gen.MaxNpc,
				RestSecs = gen.RestSecs,
				MaxPerGen = gen.MaxPerGen,
				Npctype = gen.Npctype,
				TimerBegin = gen.TimerBegin,
				TimerEnd = gen.TimerEnd,
				BornX = gen.BornX,
				BornY = gen.BornY
			};
            
        }
    }
}
