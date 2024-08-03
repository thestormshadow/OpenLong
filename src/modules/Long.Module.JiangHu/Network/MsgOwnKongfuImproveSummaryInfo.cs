using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.JiangHu.Network
{
    public sealed class MsgOwnKongfuImproveSummaryInfo : MsgBase<GameClient>
    {
        public string Name { get; set; }
        public byte Stage { get; set; }
        public byte Talent { get; set; }
        public int Timer { get; set; }
        public byte Unknown26 { get; set; }
        public ulong Points { get; set; }
        public int FreeTalentToday { get; set; }
        public int Unknown39 { get; set; }
        public byte FreeTalentUsed { get; set; }
        public int BoughtTimes { get; set; }
        public List<ushort> Identities { get; set; } = new();

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgOwnKongfuImproveSummaryInfo);
            writer.Write(Name, 16); // 4
            writer.Write(Stage); // 20
            writer.Write(Talent); // 21
            writer.Write(Timer); // 22
            writer.Write(Unknown26); // 26
            writer.Write(Points); // 27
            writer.Write(FreeTalentToday); // 35
            writer.Write(Unknown39); // 39
            writer.Write(FreeTalentUsed); // 43
            writer.Write(BoughtTimes); // 44
            foreach (var id in Identities)
            {
                writer.Write(id);
            }
            return writer.ToArray();
        }
    }
}
