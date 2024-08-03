using Long.Kernel.States.User;
using Long.Network.Packets;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Kernel.Network.Game.Packets.TeamArena
{
	public sealed class MsgTeamArenaInteractive : MsgProtoGenericBase<GameClient, MsgTeamArenaInteractiveContract>
	{
		private static readonly ILogger logger = Log.ForContext<Msg2ndPsw>();
		public MsgTeamArenaInteractive() : base(PacketType.MsgTeamArenaHeroData) { }
		public override async Task ProcessAsync(GameClient client)
		{
			Character user = client.Character;
			switch (Data.DialogID)
			{
				case 0: 
					//Game.Arena.QualifyEngine.DoSignup(client); client.Send(packet); 
					break;
				case 1: 
					//Game.Arena.QualifyEngine.DoQuit(client); client.Send(packet); 
					break;
				case 3:
					{
						switch (Data.OptionID)
						{
							//case 2: Game.Arena.QualifyEngine.DoGiveUp(client); break;
							//case 1: Game.Arena.QualifyEngine.DoAccept(client); break;
						}
						break;
					}
				case 4:
					{
						switch (Data.OptionID)
						{
							case 0:
								{
									//Game.Arena.QualifyEngine.DoQuit(client);
									break;
								}
						}
						break;
					}
				case 5:
					{
						if (user.QualifierPoints <= 1500)
						{
							if (user.Silvers >= 9000000)
							{
								user.Silvers -= 9000000;
								user.QualifierPoints += 1500;
								await user.SendAsync(this);
							}
						}
						break;
					}
				case 11://Win/Lose Dialog
					{
						switch (Data.OptionID)
						{
							//case 0: Game.Arena.QualifyEngine.DoSignup(client); break;
						}
						break;
					}
				default:
					{
						logger.Warning("[{0}]MsgTeamArenaInteractive Dialog {1} NotFound\n", (ushort)PacketType.MsgTeamArenaHeroData, Data.DialogID);
						break;
					}
			}			
		}
	}

	[Contract]
	public class MsgTeamArenaInteractiveContract
	{
		[ContractProperty(1)]
		public uint DialogID { get; set; }
		[ContractProperty(2)]
		public uint OptionID { get; set; }
		[ContractProperty(3)]
		public uint UserIdentity { get; set; }
		[ContractProperty(4)]
		public uint Class { get; set; }
		[ContractProperty(5)]
		public uint Rank { get; set; }
		[ContractProperty(6)]
		public uint ArenaPoints { get; set; }
		[ContractProperty(7)]
		public uint Level { get; set; }
	}
}
