using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueMemList : MsgBase<GameClient>
    {
        public uint SyndicateId { get; set; }
        public uint LeaderId { get; set; }
        public int TotalCount { get; set; }
        public byte Param { get; set; } = 1;
        public List<MemberListData> Members { get; set; } = new();

        public struct MemberListData
        {
            public uint Exploits { get; set; }
            public uint Profession { get; set; }
            public ushort Level { get; set; }
            public uint Mesh { get; set; }
            public bool Online { get; set; }
            public string Name { get; set; }
            public uint OfficialPosition { get; set; }
            public uint NobilityRank { get; set; }
            public uint Identity { get; set; }
            public ushort BattlePower { get; set; }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueMemList);
            writer.Write(SyndicateId);
            writer.Write(LeaderId);
            writer.Write(TotalCount);
            writer.Write((ushort)Members.Count);
            writer.Write(Param);
            foreach (var member in Members)
            {
                writer.Write(member.Exploits);
                writer.Write(member.Profession);
                writer.Write(member.Level);
                writer.Write(member.Mesh);
                writer.Write(member.Online);
                writer.Write(member.Name, 16);
                writer.Write(member.OfficialPosition);
                writer.Write(member.NobilityRank);
                writer.Write(member.Identity);
                writer.Write(member.BattlePower);
            }
            return writer.ToArray();
        }
    }
}
