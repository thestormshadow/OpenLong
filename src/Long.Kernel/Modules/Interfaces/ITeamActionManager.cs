using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface ITeamActionManager
    {
        Task OnTeamCreateAsync(Character user, ITeam team);
        Task OnTeamDismissAsync(ITeam team);
        Task OnTeamJoinAsync(Character user, ITeam team);
        Task OnTeamExitAsync(Character user, ITeam team);
    }
}
