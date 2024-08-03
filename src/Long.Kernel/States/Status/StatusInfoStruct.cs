using System.Runtime.InteropServices;

namespace Long.Kernel.States.Status
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 16)]
    public struct StatusInfoStruct
    {
        public int Status;
        public int Power;
        public int Seconds;
        public int Times;

        public StatusInfoStruct(int status, int power, int secs, int times)
            : this()
        {
            Status = status;
            Power = power;
            Seconds = secs;
            Times = times;
        }
    }
}
