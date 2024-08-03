
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletRealmAnnounceMaintenance<TActor>
        : MsgProtoBufBase<TActor, MsgPigletRealmAnnounceMaintenance<TActor>.AnnounceData> where TActor : TcpServerActor
	{

        public MsgPigletRealmAnnounceMaintenance()
            : base(PacketType.MsgPigletRealmAnnounceMaintenance)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct AnnounceData
        {
            [ProtoMember(1)]
            public int WarningMinutes { get; set; }
        }
    }
}
