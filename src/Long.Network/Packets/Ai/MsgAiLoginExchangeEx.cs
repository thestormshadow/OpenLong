using Long.Network.Sockets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long.Network.Packets.Ai
{
	public abstract class MsgAiLoginExchangeEx<TActor> : MsgProtoBufBase<TActor, MsgAiLoginExchangeExContract> where TActor : TcpServerActor
	{
		protected MsgAiLoginExchangeEx() : base(PacketType.MsgAiLoginExchangeEx)
		{
		}
	}
	[ProtoContract]
	public struct MsgAiLoginExchangeExContract
	{
		[ProtoMember(1, IsRequired = true)]
		public AiLoginResult Result { get; set; }
		[ProtoMember(2, IsRequired = true)]
		public string Message { get; set; }
	}
	public enum AiLoginResult
	{
		Success,
		AlreadySignedIn,
		InvalidPassword,
		InvalidAddress,
		AlreadyBound,
		UnknownError
	}
}
