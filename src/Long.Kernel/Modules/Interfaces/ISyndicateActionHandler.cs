using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.States.User;

namespace Long.Kernel.Modules.Interfaces
{
    public interface ISyndicateActionHandler
    {
        Task OnSyndicateJoinAsync(Character user, ISyndicate syndicate);
        Task OnSyndicateExitAsync(uint userId, ISyndicate syndicate);
    }
}
