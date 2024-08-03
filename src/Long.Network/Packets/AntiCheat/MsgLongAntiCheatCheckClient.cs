using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.AntiCheat
{
    /// <summary>
    /// The connection will already have an ID attached to it and Login server will pass it to the game 
    /// server on login exchange. Let's just make sure clientless bots programmers don't forget to
    /// submit it so we can validate them, otherwise we will just disconnect them.
    /// </summary>
    /// <remarks>This packet is sent from Game Server to Anti Cheat Server to validate if player can connect.</remarks>
    public abstract class MsgLongAntiCheatCheckClient<TActor>
        : MsgProtoBufBase<TActor, MsgLongAntiCheatCheckClient<TActor>.CheckClientData>
        where TActor : TcpServerActor
    {
        public MsgLongAntiCheatCheckClient()
            : base(PacketType.MsgLongAntiCheatCheckClient)
        {            
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct CheckClientData
        {
            [ProtoMember(1)]
            public long ConnectionId { get; set; }
            [ProtoMember(2)]
            public string MacAddress { get; set; }
            [ProtoMember(3)]
            public List<string> IpAddresses { get; set; }
        }
    }
}
