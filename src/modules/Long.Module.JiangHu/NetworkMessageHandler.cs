using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.JiangHu.Network;
using Long.Network.Packets;

namespace Long.Module.JiangHu
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgOwnKongfuBase:
                    msg = new MsgOwnKongfuBase();
                    break;
                case PacketType.MsgOwnKongfuImproveFeedback:
                    msg = new MsgOwnKongfuImproveFeedback();
                    break;
                case PacketType.MsgOwnKongfuPkSetting:
                    msg = new MsgOwnKongfuPKSetting();
                    break;
                case PacketType.MsgOwnKongRank:
                    msg = new MsgOwnKongRank();
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
