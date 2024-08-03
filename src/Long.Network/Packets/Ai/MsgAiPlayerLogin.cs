using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiPlayerLogin<TActor> : MsgProtoBufBase<TActor, MsgAiPlayerLoginContract> where TActor : TcpServerActor
	{
		protected MsgAiPlayerLogin() : base(PacketType.MsgAiPlayerLogin) { }
	}

	[ProtoContract]
	public struct MsgAiPlayerLoginContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Timestamp { get; set; }
		[ProtoMember(2, IsRequired = false)]
		public uint Id { get; set; }
		[ProtoMember(3, IsRequired = false)]
		public string Name { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public int Level { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public int Metempsychosis { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public ulong Flag1 { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public ulong Flag2 { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public ulong Flag3 { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public int BattlePower { get; set; }
		[ProtoMember(10, IsRequired = true)]
		public int Life { get; set; }
		[ProtoMember(11, IsRequired = true)]
		public int MaxLife { get; set; }
		[ProtoMember(12, IsRequired = true)]
		public int Money { get; set; }
		[ProtoMember(13, IsRequired = true)]
		public int ConquerPoints { get; set; }
		[ProtoMember(14, IsRequired = true)]
		public int Nobility { get; set; }
		[ProtoMember(15, IsRequired = true)]
		public int Syndicate { get; set; }
		[ProtoMember(16, IsRequired = true)]
		public int SyndicatePosition { get; set; }
		[ProtoMember(17, IsRequired = true)]
		public int Family { get; set; }
		[ProtoMember(18, IsRequired = true)]
		public int FamilyPosition { get; set; }
		[ProtoMember(19, IsRequired = true)]
		public uint MapId { get; set; }
		[ProtoMember(20, IsRequired = true)]
		public ushort X { get; set; }
		[ProtoMember(21, IsRequired = true)]
		public ushort Y { get; set; }
	}
}
