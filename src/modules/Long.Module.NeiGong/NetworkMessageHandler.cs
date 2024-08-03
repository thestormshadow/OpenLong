using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.NeiGong.Network;
using Long.Network.Packets;

namespace Long.Module.NeiGong
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgInnerStrengthOpt:
                    msg = new MsgInnerStrengthOpt();
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
