using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiRoleStatusFlag<TActor> : MsgProtoBufBase<TActor, MsgAiRoleStatusFlagContract> where TActor : TcpServerActor
	{
		protected MsgAiRoleStatusFlag() : base(PacketType.MsgAiRoleStatusFlag) { }
	}
	[ProtoContract]
	public struct MsgAiRoleStatusFlagContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Mode { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public uint Identity { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public int Flag { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public int Steps { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public int Duration { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public uint Caster { get; set; }
	}
}
