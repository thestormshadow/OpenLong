using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Events.Network;
using Long.Network.Packets;

namespace Long.Module.Events
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
			MsgBase<GameClient> msg;
			switch (type)
			{
				case PacketType.MsgWarFlag:
					msg = new MsgWarFlag();
					break;
				case PacketType.MsgSelfSynMemAwardRank:
					msg = new MsgSelfSynMemAwardRank();
					break;
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
