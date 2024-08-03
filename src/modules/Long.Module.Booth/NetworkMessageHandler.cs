using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Booth.Network;
using Long.Network.Packets;

namespace Long.Module.Booth
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            if (actor?.Character?.Map == null)
            {
                return false;
            }

            if (type == PacketType.MsgAction)
            {
                var msg = new Kernel.Network.Game.Packets.MsgAction();
                msg.Decode(message);
                actor.Character.QueueAction(() => MsgAction.ProcessAsync(msg, actor.Character));
            }
            else if (type == PacketType.MsgItem)
            {
                var msg = new Kernel.Network.Game.Packets.MsgItem();
                msg.Decode(message);
                actor.Character.QueueAction(() => MsgItem.ProcessAsync(msg, actor.Character));
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
