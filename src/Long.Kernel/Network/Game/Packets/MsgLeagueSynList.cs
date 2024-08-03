using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgLeagueSynList : MsgBase<GameClient>
    {
        public struct SyndicateData
        {
            public uint Identity { get; set; }
            public long Silvers { get; set; }
            public ushort Members { get; set; }
            public ushort Level { get; set; }
            public string Name { get; set; }
            public string LeaderName { get; set; }
        }

        public int Count { get; set; }
        public ushort From { get; set; }
        public byte Param { get; set; }
        public List<SyndicateData> Data { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgLeagueSynList);
            writer.Write(Count);
            writer.Write(From);
            writer.Write(Param);
            foreach (var syn in Data)
            {
                writer.Write(syn.Identity);
                writer.Write(syn.Silvers);
                writer.Write(syn.Members);
                writer.Write(syn.Level);
                writer.Write(syn.Name, 16);
                writer.Write(syn.LeaderName, 16);
            }
            return writer.ToArray();
        }
    }
}
