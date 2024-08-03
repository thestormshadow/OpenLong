
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletLogin<TActor> 
        : MsgProtoBufBase<TActor, MsgPigletLogin<TActor>.PigletData> where TActor : TcpServerActor
	{
        public MsgPigletLogin() 
            : base(PacketType.MsgPigletLogin)
        {
            serializeWithHeaders = true;
        }

        public MsgPigletLogin(string userName, string password, string realmName)
            : base(PacketType.MsgPigletLogin)
        {
            serializeWithHeaders = true;
            Data = new PigletData
            {
                UserName = userName,
                Password = password,
                RealmName = realmName
            };
        }

        [ProtoContract]
        public struct PigletData
        {
            [ProtoMember(1, IsRequired = true)]
            public string UserName { get; set; }

            [ProtoMember(2, IsRequired = true)]
            public string Password { get; set; }
            [ProtoMember(3, IsRequired = true)]
            public string RealmName { get; set; }
        }
    }
}