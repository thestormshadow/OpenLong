using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.AstProf.Network;
using Long.Network.Packets;

namespace Long.Module.AstProf
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgSubPro: msg = new MsgSubPro(); break;
                default: return Task.FromResult(false);
            }
            msg.Decode(message);
            if (actor?.Character?.Map == null)
            {
                return Task.FromResult(true);
            }
            actor.Character.QueueAction(() => msg.ProcessAsync(actor));
            return Task.FromResult(true);
        }
    }
}
