using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueAllegianceList : MsgBase<GameClient>
    {
        public struct UnionData
        {
            public uint Identity { get; set; }
            public uint Bricks { get; set; }
            public ulong Funds { get; set; }
            public string Name { get; set; }
            public string LeaderName { get; set; }
            public string RecruitDeclaration { get; set; }
        }

        public int Count { get; set; }
        public ushort Page { get; set; }
        public ushort PageCount { get; set; }
        public byte Param { get; set; }
        public List<UnionData> Data { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueAllegianceList);
            writer.Write((uint)PageCount); // 4
            writer.Write(Page); // 8
            writer.Write((ushort)Data.Count); // 10
            writer.Write(Param); // 12
            foreach (var data in Data)
            {
                writer.Write(data.Funds); // 0
                writer.Write(data.Identity); // 4
                writer.Write(data.Bricks); // 8
                writer.Write(data.Name, 16); // 16
                writer.Write(data.LeaderName, 16); // 32
                writer.Write(data.RecruitDeclaration, 256); // 48
                // + 304
            }
            return writer.ToArray();
        }
    }
}
