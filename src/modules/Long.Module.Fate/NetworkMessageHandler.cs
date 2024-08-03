using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Fate.Network;
using Long.Network.Packets;

namespace Long.Module.Fate
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            if (type == PacketType.MsgTrainingVitality)
            {
                msg = new MsgTrainingVitality();
            }
            else if (type == PacketType.MsgTrainingVitalityProtect)
            {
                msg = new MsgTrainingVitalityProtect();
            }
            else
            {
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
