using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Syndicate.Network
{
    public sealed class MsgSyndicateAttributeInfo : MsgBase<GameClient>
    {
        public MsgSyndicateAttributeInfo()
        {
            LeaderName = string.Empty;
        }

        public uint Identity { get; set; }
        public int PlayerDonation { get; set; }
        public long Funds { get; set; }
        public uint ConquerPointsFunds { get; set; }
        public int MemberAmount { get; set; }
        public int Rank { get; set; }
        public string LeaderName { get; set; }
        public int ConditionLevel { get; set; }
        public int ConditionMetempsychosis { get; set; }
        public int ConditionProfession { get; set; }
        public byte Level { get; set; }
        public uint PositionExpiration { get; set; }
        public uint EnrollmentDate { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSyndicateAttributeInfo);
            writer.Write(Identity); // 4
            writer.Write(PlayerDonation); // 8
            writer.Write(Funds); // 12
            writer.Write(ConquerPointsFunds); // 20
            writer.Write(MemberAmount); // 24
            writer.Write((uint)Rank); // 28
            writer.Write(LeaderName, 16); // 32
            writer.Write(ConditionLevel); // 48
            writer.Write(ConditionMetempsychosis); // 52
            writer.Write(ConditionProfession); // 56
            writer.Write(Level); // 60
            writer.BaseStream.Seek(2, SeekOrigin.Current); // 61
            writer.Write(PositionExpiration); // 63
            writer.Write(EnrollmentDate); // 67
            return writer.ToArray();
        }
    }
}
