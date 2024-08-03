using Long.Kernel.States.User;
using Long.Network.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Long.Kernel.Network.Game.Packets.Msg2ndPsw;

namespace Long.Kernel.Network.Game.Packets.TeamArena
{
	public sealed class MsgQualifierTeamPKStatistic : MsgProtoGenericBase<GameClient, MsgQualifierTeamPKStatisticContract>
	{
		public MsgQualifierTeamPKStatistic() : base(PacketType.MsgTeamArenaHeroData) {}

		public override async Task ProcessAsync(GameClient client)
		{
			Character user = client.Character;
			Data = new MsgQualifierTeamPKStatisticContract
			{
				Rank = 0,
				Status = 0,
				TotalWin = 0,
				TotalLose = 0,
				TodayWin = 0,
				TodayBattles = 0,
				HistoryHonor = user.HistoryHonorPoints,
				CurrentHonor = user.HonorPoints,
				ArenaPoints = 0,
			};			
			await client.SendAsync(this);
		}
	}
	[Contract]
	public class MsgQualifierTeamPKStatisticContract
	{
		[ContractProperty(8)]
		public uint CurrentHonor { get; set; }
		[ContractProperty(1)]
		public uint Rank { get; set; }
		[ContractProperty(2)]
		public uint Status { get; set; }
		[ContractProperty(3)]
		public uint TotalWin { get; set; }
		[ContractProperty(4)]
		public uint TotalLose { get; set; }
		[ContractProperty(5)]
		public uint TodayWin { get; set; }
		[ContractProperty(6)]
		public uint TodayBattles { get; set; }
		[ContractProperty(7)]
		public uint HistoryHonor { get; set; }		
		public uint ArenaPoints { get; set; }
	}
}
