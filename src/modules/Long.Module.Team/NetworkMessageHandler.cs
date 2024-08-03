using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Team.Network;
using Long.Network.Packets;

namespace Long.Module.Team
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            if (type == PacketType.MsgTeam) 
            {
                msg = new MsgTeam();
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
