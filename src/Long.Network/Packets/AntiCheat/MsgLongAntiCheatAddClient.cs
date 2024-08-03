using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.AntiCheat
{
    /// <summary>
    /// This packet will fetch some client data. 
    /// IP Address will be taken from the connection, but we will also validate it. The client must
    /// send his IP addresses and it must contain the same IP as the connection.
    /// Also this will be used to identify connections that are in the same network (for lan house users).
    /// This packet is sent when a new client is open, if we open a client with a wrong client version or
    /// patch version, then we will not allow it to connect.
    /// </summary>
    /// <remarks>This packet is sent from Launcher to Anti Cheat server.</remarks>
    public abstract class MsgLongAntiCheatAddClient<TActor> 
        : MsgProtoBufBase<TActor, MsgLongAntiCheatAddClient<TActor>.AddClientData> where TActor : TcpServerActor
    {
        public MsgLongAntiCheatAddClient()
            : base(PacketType.MsgLongAntiCheatAddClient)
        {
            serializeWithHeaders = true;
        }

        [ProtoContract]
        public struct AddClientData
        {
            [ProtoMember(1)]
            public long SystemTickCount { get; set; }
            [ProtoMember(2)]
            public string MacAddress { get; set; }
            [ProtoMember(3)]
            public List<string> IpAddresses { get; set; }
            [ProtoMember(4)]
            public long ConnectionId { get; set; }
            [ProtoMember(5)]
            public int ClientVersion { get; set; }
            [ProtoMember(6)]
            public int PatchVersion { get; set; }
        }
    }
}
