using Long.Kernel.Modules.Systems.Team;
using Long.Shared.Managers;
using System.Collections.Concurrent;

namespace Long.Kernel.Managers
{
    public class TeamManager
    {
        private static readonly ILogger logger = Log.ForContext<TeamManager>();

        public static IdentityManager Identity = new(1_000_000_000, 1_000_100_000);
        private static ConcurrentDictionary<uint, ITeam> teams = new();

        public static void Disband(uint idTeam)
        {
            if (teams.TryRemove(idTeam, out _))
            {
                Identity.ReturnIdentity(idTeam);
            }
        }

        public static ITeam FindTeamById(uint idTeam)
        {
            return teams.TryGetValue(idTeam, out var team) ? team : null;
        }

        public static ITeam FindTeamByUserId(uint idUser)
        {
            foreach (var team in teams.Values)
            {
                if (team.IsMember(idUser))
                    return team;
            }
            return null;
        }
    }
}
