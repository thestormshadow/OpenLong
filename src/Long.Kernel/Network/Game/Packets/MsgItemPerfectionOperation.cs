using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
	public sealed class MsgItemPerfectionOperation : MsgProtoBufBase<GameClient, PayloadProto>
	{
		public MsgItemPerfectionOperation() : base(PacketType.MsgItemRefine) { }

		public MsgItemPerfectionOperation(uint itemUID, uint playerUID, uint stars, uint progress, uint ownerUID, string ownerName) : base(PacketType.MsgItemRefine)
		{
			Data = new PayloadProto
			{
				ItemUID = itemUID,
				PlayerUID = playerUID,
				Stars = stars,
				Progress = progress,
				OwnerUID = ownerUID,
				OwnerName = ownerName
			};
		}
	}
	[ProtoContract]
	public struct PayloadProto
	{
		[ProtoMember(1, IsRequired = true)]
		public uint ItemUID;
		[ProtoMember(2, IsRequired = true)]
		public uint PlayerUID;
		[ProtoMember(3, IsRequired = true)]
		public uint Stars;
		[ProtoMember(4, IsRequired = true)]
		public uint Progress;
		[ProtoMember(5, IsRequired = true)]
		public uint OwnerUID;
		[ProtoMember(6, IsRequired = true)]
		public string OwnerName;
	}
}
