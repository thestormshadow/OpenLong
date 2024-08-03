using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.NeiGong.Network
{
    public sealed class MsgInnerStrengthInfo : MsgBase<GameClient>
    {
        public uint Identity { get; set; }
        public int Score { get; set; }
        public int Unknown12 { get; set; }
        public InnerStrenghtInfoType Action { get; set; }
        public List<InnerStrengthGongData> GongData { get; set; } = new();
        public List<InnerStrengthAttrData> AttrData { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgInnerStrengthInfo);
            writer.Write(Identity);
            writer.Write(Score);
            writer.Write(Unknown12);
            writer.Write((ushort)Action);
            writer.Write((ushort)GongData.Count);
            writer.Write((ushort)AttrData.Count);
            foreach (var gong in GongData)
            {
                writer.Write(gong.Type);
                writer.Write(gong.Level);
                writer.Write(gong.Value);
                writer.Write(gong.Finished);
            }
            foreach (var attr in AttrData)
            {
                writer.Write(attr.Type);
                writer.Write(attr.AttributeType);
                writer.Write(attr.Power);
            }
            return writer.ToArray();
        }

        public struct InnerStrengthGongData
        {
            public ushort Type { get; set; }
            public byte Level { get; set; }
            public byte Value { get; set; }
            public bool Finished { get; set; }
        }

        public struct InnerStrengthAttrData
        {
            public ushort Type { get; set; }
            public byte AttributeType { get; set; }
            public int Power { get; set; }
        }

        public enum InnerStrenghtInfoType : ushort
        {
            SendStage,
            SendScore
        }
    }
}
