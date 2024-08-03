using Long.Kernel.Modules.Systems.Syndicate;

namespace Long.Kernel.Modules.Systems.Totem
{
    public interface ITotemManager
    {
        Task CreateAsync(ISyndicate syndicate);
        Task InitializeAsync(ISyndicate syndicate);
    }
}
