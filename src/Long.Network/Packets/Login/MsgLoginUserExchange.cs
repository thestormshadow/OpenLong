using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Login
{
    public abstract class MsgLoginUserExchange<TActor>
        : MsgProtoBufBase<TActor, MsgLoginUserExchange<TActor>.LoginExchangeData> where TActor : TcpServerActor
    {
        public MsgLoginUserExchange()
            : base(PacketType.MsgLoginUserExchange)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct LoginExchangeData
        {
            [ProtoMember(1)]
            public uint AccountId { get; set; }
            [ProtoMember(2)]
            public string IpAddress { get; set; }
            [ProtoMember(3)]
            public int VipLevel { get; set; }
            [ProtoMember(4)]
            public string Request { get; set; }
            public Guid RequestGuid
            {
                get => Guid.Parse(Request);
                set => Request = value.ToString();
            }
            [ProtoMember(5)]
            public ushort AuthorityId { get; set; }
        }
    }
}
