
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletUserLogin<TActor>
        : MsgProtoBufBase<TActor, MsgPigletUserLogin<TActor>.UserLoginData> where TActor : TcpServerActor
	{
        public MsgPigletUserLogin() 
            : base(PacketType.MsgPigletUserLogin)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct UserLoginData
        {
            [ProtoMember(1)]
            public List<UserData> Users { get; set; }
            [ProtoMember(2)]
            public bool ServerSync { get; set; }
            [ProtoMember(3)]
            public int MaxPlayerOnline { get; set; }
        }

        [ProtoContract]
        public struct UserData
        {
            [ProtoMember(1)]
            public uint UserId { get; set; }
            [ProtoMember(2)]
            public uint AccountId { get; set; }
            [ProtoMember(3)]
            public bool IsLogin { get; set; }
        }
    }
}
