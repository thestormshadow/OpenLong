using Long.Database.Entities;
using Long.Kernel.Network.Ai;
using Long.Network.Packets.Ai;

namespace Long.Game.Network.Ai.Packets
{
    public sealed class MsgAiDynaMap : MsgAiDynaMap<AiClient>
    {
        public MsgAiDynaMap(uint idMap)
        {
			Data = new MsgAiDynaMapContract
			{
				Mode = 1,
				Identity = idMap,
				Name = string.Empty,
				Description = string.Empty
			};
            
        }

        public MsgAiDynaMap(DbDynamap dyna, uint instanceType = 0, uint instanceMapId = 0)
        {
			Data = new MsgAiDynaMapContract
			{
				Identity = dyna.Identity,
				Name = dyna.Name,
				Description = dyna.Description,
				MapDoc = dyna.MapDoc,
				MapType = dyna.Type,
				OwnerIdentity = dyna.OwnerIdentity,
				MapGroup = dyna.MapGroup,
				ServerIndex = dyna.ServerIndex,
				Weather = dyna.Weather,
				BackgroundMusic = dyna.BackgroundMusic,
				BackgroundMusicShow = dyna.BackgroundMusicShow,
				PortalX = dyna.PortalX,
				PortalY = dyna.PortalY,
				RebornMap = dyna.RebornMap,
				RebornPortal = dyna.RebornPortal,
				ResourceLevel = dyna.ResourceLevel,
				OwnerType = dyna.OwnerType,
				LinkMap = dyna.LinkMap,
				LinkX = dyna.LinkX,
				LinkY = dyna.LinkY,
				Color = dyna.Color,
				InstanceType = instanceType,
				InstanceMapId = instanceMapId
			};
            
        }
    }
}
