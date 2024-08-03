using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginUserExchangeEx<TActor>
        : MsgProtoBufBase<TActor, MsgLoginUserExchangeEx<TActor>.LoginExchangeData> where TActor : TcpServerActor
    {
        public const int SUCCESS = 0,
            ALREADY_LOGGED_IN = 1;

        public MsgLoginUserExchangeEx()
            : base(PacketType.MsgLoginUserExchangeEx)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LoginExchangeData
        {
            [ProtoMember(1)]
            public ulong Token { get; set; }
            [ProtoMember(2)]
            public uint AccountId { get; set; }
            [ProtoMember(3)]
            public string Request { get; set; }
            public Guid RequestGuid
            {
                get => Guid.Parse(Request);
                set => Request = value.ToString();
            }
            [ProtoMember(4)]
            public int Response { get; set; }
        }
    }
}
