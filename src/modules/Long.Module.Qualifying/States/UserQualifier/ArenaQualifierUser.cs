
using static Long.Kernel.States.User.Character;

namespace Long.Module.Qualifying.States.UserQualifier
{
    public sealed class ArenaQualifierUser
    {
        public ArenaQualifierUser()
        {
            JoinTime = DateTime.Now;
        }

        public uint Identity { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Profession { get; set; }
        public PkModeType PreviousPkMode { get; set; }
        public int Points { get; set; }

        public int Grade
        {
            get
            {
                if (Points >= 4000)
                {
                    return 5;
                }

                if (Points >= 3300 && Points < 4000)
                {
                    return 4;
                }

                if (Points >= 2800 && Points < 3300)
                {
                    return 3;
                }

                if (Points >= 2200 && Points < 2800)
                {
                    return 2;
                }

                if (Points >= 1500 && Points < 2200)
                {
                    return 1;
                }

                return 0;
            }
        }

        public DateTime JoinTime { get; }
    }
}
