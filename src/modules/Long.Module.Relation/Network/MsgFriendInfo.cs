using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Relation.Network
{
    public sealed class MsgFriendInfo : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public uint Lookface { get; set; }
        public byte Level { get; set; }
        public byte Profession { get; set; }
        public ushort PkPoints { get; set; }
        public uint SyndicateIdentity { get; set; }
        public ushort SyndicateRank { get; set; }
        public string Mate { get; set; }
        public bool IsEnemy { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgFriendInfo);
            writer.Write(Identity); // 4
            writer.Write(Lookface); // 8
            writer.Write(Level); // 12
            writer.Write(Profession); // 13
            writer.Write(PkPoints); // 14
            writer.Write(SyndicateIdentity); // 16
            writer.Write(SyndicateRank); // 20
            writer.Write(0); // 22
            writer.Write(Mate, 16); // 26
            writer.Write(IsEnemy); // 42
            return writer.ToArray();
        }
    }
}
