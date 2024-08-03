using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgSynCompete : MsgBase<GameClient>
    {
        public enum SynCompeteAction
        {
            List,
            SetHeroMerit,
            SetHeroSynCompetePoint
        }

        public SynCompeteAction Action { get; set; }
        public int Count => Rank.Count;
        public int Merit { get; set; }
        public int Data { get; set; }
        public ulong CompetePoint { get; set; }
        public List<SynCompeteRank> Rank { get; init; } = new List<SynCompeteRank>();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgSynCompete);
            writer.Write((int)Action); // 4
            writer.Write(Count); // 8
            writer.Write(Merit); // 12
            if (Action == SynCompeteAction.List)
            {
                writer.Write(Data); // 16
                writer.Write(CompetePoint); // 20
            }
            else
            {
                foreach (var r in Rank)
                {
                    writer.Write(r.Data);
                    writer.Write(r.DataLong);
                    writer.Write(r.Name, 36);
                } // +48
            }
            return writer.ToArray();
        }


        public struct SynCompeteRank
        {
            public uint Data { get; set; }
            public ulong DataLong { get; set; }
            public string Name { get; set; }
        }
    }
}
