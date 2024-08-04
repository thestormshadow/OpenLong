using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.TaskDetail.Network;
using Long.Network.Packets;

namespace Long.Module.TaskDetail
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
			MsgBase<GameClient> msg = null;

            switch (type)
            {
                case PacketType.MsgTaskStatus:
                    {
						msg = new MsgTaskStatus();
						break;
					}
				case PacketType.MsgTaskDetailInfo:
					{
						msg = new MsgTaskDetailInfo();
						break;
					}
				default:
					return false;
            }
            
            if (actor?.Character?.Map == null)
            {
                return true;
            }

            msg.Decode(message);
            actor.Character.QueueAction(() => msg.ProcessAsync(actor));
            return true;
        }
    }
}
