using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Relation.Network;
using Long.Network.Packets;

namespace Long.Module.Relation
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            if (type == PacketType.MsgFriend)
            {
                msg = new MsgFriend();
            }
            else if (type == PacketType.MsgEnemyList)
            {
                msg = new MsgEnemyList();
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
