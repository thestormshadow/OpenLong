using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiAction<TActor> : MsgProtoBufBase<TActor, MsgAiActionContract> where TActor : TcpServerActor
	{
		protected MsgAiAction() : base(PacketType.MsgAiAction) { }
	}
	public enum AiActionType
	{
		None,
		RequestLogin,
		Walk,
		Run,
		Jump,
		SetDirection,
		SetAction,
		SynchroPosition,
		LeaveMap,
		FlyMap,
		SetProtection,
		ClearProtection,
		QueryRole,
		Shutdown
	}
	[ProtoContract]
	public struct MsgAiActionContract
	{
		[ProtoMember(1, IsRequired = true)]
		public AiActionType Action { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public uint Identity { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public ushort X { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public ushort Y { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public int Direction { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public uint TargetIdentity { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public ushort TargetX { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public ushort TargetY { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public long Timestamp { get; set; }
	}
}
