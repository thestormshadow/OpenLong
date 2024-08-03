using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgStatisticDaily : MsgBase<GameClient>
    {
        public List<DailyData> Data { get; set; } = new();

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgStatisticDaily);
            writer.Write((byte)Data.Count); // 4
            foreach (var m in Data)
            {
                writer.Write((int) m.EventId); // 5
                writer.Write((int) m.DataType); // 9
                writer.Write(m.ActivityPoints); // 13
            }
            return writer.ToArray();
        }

        public struct DailyData
        {
            public EventType EventId { get; set; }
            public DataMode DataType { get; set; }
            public int ActivityPoints { get; set; }
        }

        public enum EventType
        {
            None = 0,
            Reborn,
            ActivityTaskData
        }

        public enum DataMode
        {
            ImmediateRebornTimes = 1,
            TodayActiveValue = 1,
            UserRewardGrade = 2,
        }
    }
}
