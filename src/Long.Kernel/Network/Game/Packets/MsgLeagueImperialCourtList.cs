using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueImperialCourtList : MsgBase<GameClient>
    {
        public enum CourtType
        {
            Officials,
            OfficialsList
        }

        public CourtType Action { get; set; }
        public List<CourtMember> Officials { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueImperialCourtList);
            writer.Write((byte)Action);
            writer.Write((byte)Officials.Count);
            foreach (var member in Officials)
            {
                writer.Write(member.BattlePower);
                writer.Write(member.Mesh);
                writer.Write(member.Exploits);
                writer.Write(member.NobilityRank);
                writer.Write(member.Identity);
                writer.Write(member.Level);
                writer.Write(member.Profession);
                writer.Write(member.UnionRank);
                writer.Write(member.Online);
                writer.Write(member.Name, 16);
            }
            return writer.ToArray();
        }

        public struct CourtMember
        {
            public uint BattlePower { get; set; }
            public uint Mesh { get; set; }
            public uint Exploits { get; set; }
            public uint NobilityRank { get; set; }
            public uint Identity { get; set; }
            public ushort Level { get; set; }
            public ushort Profession { get; set; }
            public ushort UnionRank { get; set; }
            public bool Online { get; set; }
            public string Name { get; set; }
        }
    }
}
