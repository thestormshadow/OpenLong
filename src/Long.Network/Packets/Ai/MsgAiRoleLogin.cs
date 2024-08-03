using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiRoleLogin<TActor> : MsgProtoBufBase<TActor, MsgAiRoleLoginContract> where TActor : TcpServerActor
	{
		protected MsgAiRoleLogin() : base(PacketType.MsgAiRoleLogin) { }
	}
	public enum RoleLoginNpcType
	{
		None,
		Monster,
		CallPet,
		Npc,
		DynamicNpc
	}
	[ProtoContract]
	public struct MsgAiRoleLoginContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Timestamp { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public RoleLoginNpcType NpcType { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public int Generator { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public uint Identity { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public string Name { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public int LookFace { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public uint MapId { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public ushort MapX { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public ushort MapY { get; set; }
	}
}
