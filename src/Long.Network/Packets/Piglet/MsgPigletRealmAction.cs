
using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Piglet
{
    public abstract class MsgPigletRealmAction<TActor> : MsgProtoBufBase<TActor, MsgPigletRealmAction<TActor>.ActionData> where TActor : TcpServerActor
	{
        public MsgPigletRealmAction()
            : base(PacketType.MsgPigletRealmAction)
        {
            this.serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct ActionData
        {
            [ProtoMember(1)]
            public int Action { get; set; }
            [ProtoIgnore]
            public ActionDataType ActionType => (ActionDataType)Action;
            [ProtoMember(2)]
            public int Data { get; set; }
            [ProtoMember(3)]
            public int Param { get; set; }
            [ProtoMember(4)]
            public List<string> Strings { get; set; }
        }

        public enum ActionDataType
        {
            None,
            MinActionDataType = 1_000,
            StartServer
        }
    }
}
