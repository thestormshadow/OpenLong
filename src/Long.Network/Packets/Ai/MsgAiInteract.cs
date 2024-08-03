using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiInteract<TActor> : MsgProtoBufBase<TActor, MsgAiInteractContract> where TActor : TcpServerActor
	{
		protected MsgAiInteract() : base(PacketType.MsgAiInteract) { }
	}

	[ProtoContract]
	public struct MsgAiInteractContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Timestamp { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public AiInteractAction Action { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public uint Identity { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public uint TargetIdentity { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public int X { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public int Y { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public ushort MagicType { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public ushort MagicLevel { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public int Data { get; set; }
	}
	public enum AiInteractAction
	{
		None,
		Attack,
		MagicAttack,
		MagicAttackWarning
	}
}
