using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Module.Fate.Network
{
    public sealed class MsgTrainingVitalityInfo : MsgBase<GameClient>
    {
        /// <remarks>
        /// 1 update 0 normal
        /// </remarks>
        public ushort Mode { get; set; }
        public uint Identity { get; set; }
        public uint Strength { get; set; }
        public uint Data { get; set; }
        public int Count => Datas.Count;
        public List<TrainingData> Datas { get; set; } = new List<TrainingData>();

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTrainingVitalityInfo);
            writer.Write(Mode);
            writer.Write(Identity);
            writer.Write(Strength);
            writer.Write(Data);
            writer.Write(Count);
            foreach (var data in Datas)
            {
                writer.Write(data.Type);
                writer.Write(data.Power1);
                writer.Write(data.Power2);
                writer.Write(data.Power3);
                writer.Write(data.Power4);
            }
            return writer.ToArray();
        }

        public struct TrainingData
        {
            public byte Type { get; set; }
            public int Power1 { get; set; }
            public int Power2 { get; set; }
            public int Power3 { get; set; }
            public int Power4 { get; set; }
        }
    }
}
