using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Family.Network;
using Long.Network.Packets;

namespace Long.Module.Family
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgFamily: msg = new MsgFamily(); break;
                case PacketType.MsgFamilyOccupy: msg = new MsgFamilyOccupy(); break;
                default: return false;
            }
            msg.Decode(message);
            if (actor?.Character?.Map == null)
            {
                return true;
            }
            actor.Character.QueueAction(() => msg.ProcessAsync(actor));
            return true;
        }
    }
}
