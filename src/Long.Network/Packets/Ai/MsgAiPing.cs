using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiPing<TActor> : MsgProtoBufBase<TActor, MsgAiPingContract> where TActor : TcpServerActor
	{
		protected MsgAiPing() : base(PacketType.MsgAiPing) { }
	}
	[ProtoContract]
	public struct MsgAiPingContract
	{
		[ProtoMember(1, IsRequired = true)]
		public int Timestamp { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public long TimestampMs { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public int RecvTimestamp { get; set; }
		[ProtoMember(4, IsRequired = true)]
		public long RecvTimestampMs { get; set; }
	}
}
