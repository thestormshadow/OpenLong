using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Modules.Systems.Totem;

namespace Long.Module.Totem.Managers
{
    public sealed class TotemManager : ITotemManager
    {
        public Task CreateAsync(ISyndicate syndicate)
        {
            syndicate.Totem = new States.Totem(syndicate);
            return syndicate.Totem.CreateAsync();
        }

        public Task InitializeAsync(ISyndicate syndicate)
        {
            syndicate.Totem = new States.Totem(syndicate);
            return syndicate.Totem.InitializeAsync();
        }
    }
}
