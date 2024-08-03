
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    /// <summary>
    /// This message is sent on server startup and server shutdown for a graceful startup or shutdown.
    /// It will update the server status in the web interface for players and GM panel.
    /// </summary>
    public abstract class MsgPigletRealmStatus<TActor>
        : MsgProtoBufBase<TActor, MsgPigletRealmStatus<TActor>.RealmStatusData> where TActor : TcpServerActor
	{
        public MsgPigletRealmStatus() 
            : base(PacketType.MsgPigletRealmStatus)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct RealmStatusData
        {
            [ProtoMember(1)]
            public int Status { get; set; }
        }
    }
}