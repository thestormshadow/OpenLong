using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueRank : MsgBase<GameClient>
    {
        public enum RankMode
        {
            Top3Union,
            General
        }

        public RankMode Action { get; set; }
        public int Count { get; set; }
        public ushort Page { get; set; }
        public byte Param { get; set; }
        public int PageCount { get; set; }
        public List<UnionData> Data { get; set; } = new();

        public struct UnionData
        {
            public uint Bricks { get; set; }
            public uint ServerId { get; set; }
            public string Name { get; set; }
            public string LeaderName { get; set; }
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueRank);
            writer.Write((ushort)Action);
            if (Action == RankMode.Top3Union) // bullshit
            {
                writer.Write(PageCount);
                writer.Write(Param);
                writer.Write(Data.Count);
                writer.Write(Page);
                foreach (var data in Data)
                {
                    writer.Write(data.Bricks);
                    writer.Write(data.Name, 16);
                    writer.Write(data.LeaderName, 16);
                    writer.Write(data.ServerId);
                }
            }
            else
            {
                writer.Write((ushort)Count);
                writer.Write(Page);
                writer.Write(Param);
                writer.Write((ushort)PageCount);
                foreach (var data in Data)
                {
                    writer.Write(data.ServerId);
                    writer.Write(data.Bricks);
                    writer.Write(data.Name, 16);
                    writer.Write(data.LeaderName, 16);
                }
            }
            return writer.ToArray();
        }
    }
}
