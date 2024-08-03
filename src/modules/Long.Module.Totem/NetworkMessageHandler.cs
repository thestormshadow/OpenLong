using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Totem.Network;
using Long.Network.Packets;

namespace Long.Module.Totem
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgTotemPole:
                    msg = new MsgTotemPole();
                    break;
                case PacketType.MsgWeaponsInfo:
                    msg = new MsgWeaponsInfo();
                    break;
                case PacketType.MsgTotemsRegister:
                    msg = new MsgTotemsRegister();
                    break;
                default:
                    return Task.FromResult(false);
            }

            if (actor?.Character?.Map == null)
            {
                return Task.FromResult(true);
            }

            msg.Decode(message);
            actor.Character.QueueAction(() => msg.ProcessAsync(actor));
            return Task.FromResult(true);
        }
    }
}
