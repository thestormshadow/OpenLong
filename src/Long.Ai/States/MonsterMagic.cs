namespace Long.Ai.States
{
    public sealed class MonsterMagic
    {
        private readonly TimeOutMS timeOutMS = new();
        private readonly TimeOutMS warningTimeOutMS = new();

        public MonsterMagic(
            uint magicType,
            uint coldTime,
            ushort warningTime
        )
        {
            MagicType = magicType;
            ColdTime = coldTime;
            WarningTime = warningTime;
            LastTick = 0;

            timeOutMS.Startup((int)ColdTime);
        }

        public uint MonsterType { get; }
        public uint MagicType { get; }
        public uint MagicLev { get; } = 0;
        public uint ColdTime { get; }
        public ushort WarningTime { get; }
        public long LastTick { get; set; }

        public bool IsReady()
        {
            return timeOutMS.IsTimeOut();
        }

        public void Use()
        {
            timeOutMS.Startup((int)ColdTime);
            LastTick = Environment.TickCount64;
        }

        public void StartWarningTimer()
        {
            warningTimeOutMS.Startup(WarningTime);
        }

        public bool IsWarningTimeOut()
        {
            return warningTimeOutMS.IsActive() && warningTimeOutMS.IsTimeOut();
        }

        public void ResetWarningTimeOut()
        {
            warningTimeOutMS.Clear();
        }

        public override string ToString()
        {
            return $"Type:{MagicType},ColdTime:{ColdTime},WarningTime:{WarningTime},Ready:{IsReady()}";
        }
    }
}
