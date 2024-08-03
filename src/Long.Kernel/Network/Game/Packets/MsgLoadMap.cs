using Long.Kernel.Network.Ai;
using Long.Kernel.States;
using Long.Kernel.States.User;
using Long.Network.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Long.Kernel.Network.Game.Packets.MsgLoadMap;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Long.Kernel.Network.Game.Packets
{
	public sealed class MsgLoadMap : MsgProtoBufBase<GameClient, MsgLoadMapContract>
	{
        public MsgLoadMap() : base(PacketType.MsgLoadMap)
        {
            
        }

		public MsgLoadMap(Character client, uint targetIdentity) : base(PacketType.MsgLoadMap)
		{
			Data = new MsgLoadMapContract();
			Data.BossUID = targetIdentity;
			Data.Type = 1;
			var target = (Monster)client.Map.QueryRoles().Where(i => i is Monster && i.Identity == Data.BossUID).FirstOrDefault();
			if (target != null)
			{
				var scores = target.Score.OrderByDescending(i => i.Value).ToArray();
				if (scores.Length != 0)
				{
					Data.Hunters = new Hunter[scores.Length];
					for (int i = 0; i < Data.Hunters.Length; i++)
					{
						Data.Hunters[i] = new Hunter();
						Data.Hunters[i].UID = scores[i].Key.Identity;
						Data.Hunters[i].Name = scores[i].Key.Name;
						Data.Hunters[i].Damage = (uint)scores[i].Value;
						Data.Hunters[i].ServerID = scores[i].Key.ServerID;
						Data.Hunters[i].Rank = (uint)(i + 1);
					}
				}
			}
			
		}

		public override async Task ProcessAsync(GameClient client)
		{
			switch (Data.Type)
			{
				case 0:
					{
						Data.Type = 1;
						var target = (Monster)client.Character.Map.QueryRoles().Where(i => i is Monster && i.Identity == Data.BossUID).FirstOrDefault();
						var scores = target.Score.OrderByDescending(i => i.Value).ToArray();
						if (scores.Length != 0)
						{
							Data.Hunters = new Hunter[scores.Length];
							for (int i = 0; i < Data.Hunters.Length; i++)
							{
								Data.Hunters[i] = new Hunter();
								Data.Hunters[i].UID = scores[i].Key.Identity;
								Data.Hunters[i].Name = scores[i].Key.Name;
								Data.Hunters[i].Damage = (uint)scores[i].Value;
								Data.Hunters[i].ServerID = scores[i].Key.ServerID;
								Data.Hunters[i].Rank = (uint)(i + 1);
							}
						}
						await client.SendAsync(this);
						break;
					}
			}
		}

		[ProtoContract]
		public class MsgLoadMapContract
		{
			[ProtoMember(1, IsRequired = true)]
			public uint Type;
			[ProtoMember(2, IsRequired = true)]
			public uint BossUID;
			[ProtoMember(3, IsRequired = true)]
			public Hunter[] Hunters;
		}
		[ProtoContract]
		public class Hunter
		{
			[ProtoMember(1, IsRequired = true)]
			public uint Rank;
			[ProtoMember(2, IsRequired = true)]
			public uint ServerID;
			[ProtoMember(3, IsRequired = true)]
			public uint UID;
			[ProtoMember(4, IsRequired = true)]
			public string Name;
			[ProtoMember(5, IsRequired = true)]
			public uint Damage;
		}
	}
}
