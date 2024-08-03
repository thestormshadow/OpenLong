using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Kernel.Network.Game;
using Long.Module.Qualifying.Network;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;

namespace Long.Module.Qualifying
{
    public sealed class NetworkMessageHandler : INetworkMessageHandler
    {
        public async Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message)
        {
			MsgBase<GameClient> msg;

			switch (type)
			{
				case PacketType.MsgQualifyingInteractive:
					msg = new MsgQualifierInteractive();
					break;

				case PacketType.MsgQualifyingFightersList:
					msg = new MsgQualifierFightersList();
					break;

				case PacketType.MsgQualifyingRank:
					msg = new MsgQualifierRank();
					break;

				case PacketType.MsgQualifyingSeasonRankList:
					msg = new MsgQualifierSeasonRankList();
					break;

				case PacketType.MsgQualifyingDetailInfo:
					msg = new MsgQualifierDetailInfo();
					break;

				case PacketType.MsgAthleteShop:
					msg = new MsgAthleteShop();
					break;

				case PacketType.MsgQualifyingWitness:
					msg = new MsgQualifierWitness();
					break;
				default:
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
