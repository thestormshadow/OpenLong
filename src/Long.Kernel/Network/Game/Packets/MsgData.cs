using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgData : MsgBase<GameClient>
    {
        public MsgData()
        {
        }

        public MsgData(DateTime time)
        {
            Action = DataAction.SetServerTime;
            Year = time.Year - 1900;
            Month = time.Month - 1;
            DayOfYear = time.DayOfYear;
            Day = time.Day;
            Hours = time.Hour;
            Minutes = time.Minute;
            Seconds = time.Second;
        }

        public DataAction Action { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int DayOfYear { get; set; }
        public int Day { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgData);
            writer.Write(Environment.TickCount);
            writer.Write((uint)Action);
            writer.Write(Year);
            writer.Write(Month);
            writer.Write(DayOfYear);
            writer.Write(Day);
            writer.Write(Hours);
            writer.Write(Minutes);
            writer.Write(Seconds);
            return writer.ToArray();
        }

        public enum DataAction
        {
            SetServerTime = 0,
            SetMountMovePoint = 2,
            AntiCheatAnswerMsgTypeCount = 3,
            AntiCheatAskMsgTypeCount = 4
        }
    }
}
