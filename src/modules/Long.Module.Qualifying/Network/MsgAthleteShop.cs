using Long.Network.Packets;
using Long.Kernel.Network.Game;
using ProtoBuf;
using Long.Kernel.Network.Ai;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Long.Module.Qualifying.Network
{
	public sealed class MsgAthleteShop : MsgProtoBufBase<GameClient, MsgAthleteShopContract>
	{
		public MsgAthleteShop() : base(PacketType.MsgAthleteShop) { }
		public override async Task ProcessAsync(GameClient client)
		{
			switch (Data.Unknown1)
			{
				case 0:
				case 8:
					{
						Data.Unknown1 = 34056;
						Data.Unknown2 = 919;
						await client.SendAsync(this);
						break;
					}
				default: Console.WriteLine("[ChampionShop] Unknown Type: " + Data.Unknown1 + ""); break;
			}
		}
	}
	
	[ProtoContract]
	public class MsgAthleteShopContract
	{
		[ProtoMember(1, IsRequired = true)]
		public uint Unknown1 { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public uint Unknown2 { get; set; }
	}
}
