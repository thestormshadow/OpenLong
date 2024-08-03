using Long.Kernel.States.User;
using Long.Kernel.States.World;

namespace Long.Kernel.Modules.Interfaces
{
    public interface IChangeMapHandler
    {
        Task OnEnterMapAsync(Character user, GameMap gameMap);
        Task OnLeaveMapAsync(Character user, GameMap gameMap);
    }
}
