using Long.Network.Sockets;
using ProtoBuf;

namespace Long.Network.Packets.Cross
{
    public abstract class MsgCrossAstProfInfo<TActor>
        : MsgProtoBufBase<TActor, CrossAstProfListInfoPB>
        where TActor : TcpServerActor
    {
        protected MsgCrossAstProfInfo()
            : base(PacketType.MsgCrossAstProfInfo)
        {
            serializeWithHeaders = true;
        }
    }

    [ProtoContract]
    public struct CrossAstProfListInfoPB
    {
        [ProtoMember(1)]
        public ulong SessionId { get; set; }
        [ProtoMember(2)]
        public ulong AstProfRank { get; set; }
        [ProtoMember(3)]
        public List<CrossAstProfInfoPB> List { get; set; }
    }

    [ProtoContract]
    public struct CrossAstProfInfoPB
    {
        [ProtoMember(1)]
        public byte AstProf { get; set; }
        [ProtoMember(2)]
        public byte Level { get; set; }
    }
}
