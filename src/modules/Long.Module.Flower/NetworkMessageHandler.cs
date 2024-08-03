using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Flower.Network;
using Long.Network.Packets;

namespace Long.Module.Flower
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            if (type == PacketType.MsgFlower)
            {
                msg = new MsgFlower();
            }
            else if (type == PacketType.MsgSuitStatus)
            {
                msg = new MsgSuitStatus();
            }
            else
            {
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
