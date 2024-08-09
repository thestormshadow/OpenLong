using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using static Long.Kernel.States.User.Character;

namespace Long.Module.Qualifying.States.TeamQualifier
{
    public sealed class TeamArenaQualifierCompany
    {
        public TeamArenaQualifierCompany(ITeam team)
        {
            Team = team;
            JoinTime = DateTime.Now;
        }

        public ITeam Team { get; init; }
        public uint Identity => Team?.TeamId ?? 0;
        public string Name => Team?.Leader?.Name ?? "Error";
        public Dictionary<uint, PkModeType> PreviousPkModes { get; set; } = new();

        public ConcurrentDictionary<uint, Character> Participants { get; set; } = new();

        public int Rank => Participants.Values.Min(x => x.TeamQualifierRank);

        public int Points => (int)Team.Members.Average(x => x.TeamQualifierPoints);

        public int Grade
        {
            get
            {
                if (Points >= 4000)
                {
                    return 5;
                }

                if (Points is >= 3300 and < 4000)
                {
                    return 4;
                }

                if (Points is >= 2800 and < 3300)
                {
                    return 3;
                }

                if (Points is >= 2200 and < 2800)
                {
                    return 2;
                }

                if (Points is >= 1500 and < 2200)
                {
                    return 1;
                }

                return 0;
            }
        }

        public DateTime JoinTime { get; }
    }
}
