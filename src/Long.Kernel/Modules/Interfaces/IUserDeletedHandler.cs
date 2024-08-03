using Long.Kernel.Database;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface IUserDeletedHandler
    {
        Task OnUserDeletedAsync(Character user, ServerDbContext ctx);
    }
}
