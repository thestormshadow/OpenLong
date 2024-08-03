using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiDynaMap<TActor> : MsgProtoBufBase<TActor, MsgAiDynaMapContract> where TActor : TcpServerActor
	{
		private const int NAME_LENGTH = 105;
		private const int DESCRIBE_LENGTH = 381;

		protected MsgAiDynaMap() : base(PacketType.MsgAiDynaMap)
		{
		}
    }
	[ProtoContract]
	public struct MsgAiDynaMapContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Mode { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public uint Identity { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public string Name { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public string Description { get; set; }
		[ProtoMember(5, IsRequired = true)]
		public uint MapDoc { get; set; }
		[ProtoMember(6, IsRequired = true)]
		public ulong MapType { get; set; }
		[ProtoMember(7, IsRequired = true)]
		public uint OwnerIdentity { get; set; }
		[ProtoMember(8, IsRequired = true)]
		public uint MapGroup { get; set; }
		[ProtoMember(9, IsRequired = true)]
		public int ServerIndex { get; set; }
		[ProtoMember(10, IsRequired = true)]
		public uint Weather { get; set; }
		[ProtoMember(11, IsRequired = true)]
		public uint BackgroundMusic { get; set; }
		[ProtoMember(12, IsRequired = true)]
		public uint BackgroundMusicShow { get; set; }
		[ProtoMember(13, IsRequired = true)]
		public uint PortalX { get; set; }
		[ProtoMember(14, IsRequired = true)]
		public uint PortalY { get; set; }
		[ProtoMember(15, IsRequired = true)]
		public uint RebornMap { get; set; }
		[ProtoMember(16, IsRequired = true)]
		public uint RebornPortal { get; set; }
		[ProtoMember(17, IsRequired = true)]
		public byte ResourceLevel { get; set; }
		[ProtoMember(18, IsRequired = true)]
		public byte OwnerType { get; set; }
		[ProtoMember(19, IsRequired = true)]
		public uint LinkMap { get; set; }
		[ProtoMember(20, IsRequired = true)]
		public ushort LinkX { get; set; }
		[ProtoMember(21, IsRequired = true)]
		public ushort LinkY { get; set; }
		[ProtoMember(22, IsRequired = true)]
		public uint Color { get; set; }
		[ProtoMember(23, IsRequired = true)]
		public uint InstanceType { get; set; }
		[ProtoMember(24, IsRequired = true)]
		public uint InstanceMapId { get; set; }
	}
}
