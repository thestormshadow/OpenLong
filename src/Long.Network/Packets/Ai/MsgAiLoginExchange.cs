using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiLoginExchange<TActor> : MsgProtoBufBase<TActor, MsgAiLoginExchangeContract> where TActor : TcpServerActor
	{		
		protected MsgAiLoginExchange() : base(PacketType.MsgAiLoginExchange) {  }
	}

	[ProtoContract]
	public struct MsgAiLoginExchangeContract
	{
		[ProtoMember(1, IsRequired = true)]
		public string UserName { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public string Password { get; set; }
		[ProtoMember(3, IsRequired = true)]
		public string ServerName { get; set; }
	}
}
