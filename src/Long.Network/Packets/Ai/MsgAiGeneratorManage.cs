
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiGeneratorManage<TActor> : MsgProtoBufBase<TActor, MsgAiGeneratorManageContract> where TActor : TcpServerActor
	{
		protected MsgAiGeneratorManage() : base(PacketType.MsgAiGeneratorManage)
		{
		}
	}
	[ProtoContract]
	public struct MsgAiGeneratorManageContract
	{
		[ProtoMember(1, IsRequired = true)]
		public uint MapId { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public ushort BoundX { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public ushort BoundY { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public ushort BoundCx { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public ushort BoundCy { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public int MaxNpc { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public int RestSecs { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public int MaxPerGen { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public uint Npctype { get; set; }
		[ProtoMember(10, IsRequired = true)]
		public int TimerBegin { get; set; }
		[ProtoMember(11, IsRequired = true)]
		public int TimerEnd { get; set; }
		[ProtoMember(12, IsRequired = true)]
		public int BornX { get; set; }
		[ProtoMember(13, IsRequired = true)]
		public int BornY { get; set; }
	}
}
