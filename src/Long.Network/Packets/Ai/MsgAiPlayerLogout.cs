using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiPlayerLogout<TActor> : MsgProtoBufBase<TActor, MsgAiPlayerLogoutContract> where TActor : TcpServerActor
	{
		protected MsgAiPlayerLogout() : base(PacketType.MsgAiPlayerLogout) { }
	}

	[ProtoContract]
	public struct MsgAiPlayerLogoutContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Timestamp { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public uint Id { get; set; }
	}
}
