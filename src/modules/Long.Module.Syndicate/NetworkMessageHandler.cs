using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Syndicate.Network;
using Long.Network.Packets;

namespace Long.Module.Syndicate
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
            MsgBase<GameClient> msg;
            switch (type)
            {
                case PacketType.MsgSyndicate: msg = new MsgSyndicate(); break;
                case PacketType.MsgSynMemberList: msg = new MsgSynMemberList(); break;
                case PacketType.MsgFactionRankInfo: msg = new MsgFactionRankInfo(); break;
                case PacketType.MsgSynpOffer: msg = new MsgSynpOffer(); break;
                case PacketType.MsgSynRecuitAdvertising: msg = new MsgSynRecuitAdvertising(); break;
                case PacketType.MsgSynRecruitAdvertisingOpt: msg = new MsgSynRecruitAdvertisingOpt(); break;
                case PacketType.MsgSynRecruitAdvertisingList: msg = new MsgSynRecruitAdvertisingList(); break;
                default: return Task.FromResult(false);
            }

            if (actor?.Character?.Map == null)
            {
                return Task.FromResult(false);
            }

            msg.Decode(message);
            actor.Character.QueueAction(() => msg.ProcessAsync(actor));
            return Task.FromResult(true);
        }
    }
}
