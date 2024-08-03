using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface IFamilyActionHandler
    {
        Task OnFamilyJoinAsync(Character user, IFamily family);
        Task OnFamilyExitAsync(uint userId, IFamily family);
    }
}
