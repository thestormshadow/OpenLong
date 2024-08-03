using Long.Kernel.Managers;
using Long.Kernel.Modules.Interfaces;
using Long.Module.Flower.Repositories;

namespace Long.Module.Flower
{
    public sealed class DailyResetHandler : IDailyResetHandler
    {
        public async Task OnDailyResetAsync()
        {
            foreach (var user in RoleManager.QueryUserSet()) // set info on user
            {
                user.Flower.FlowerToday.RedRose = 0;
                user.Flower.FlowerToday.WhiteRose = 0;
                user.Flower.FlowerToday.Orchids = 0;
                user.Flower.FlowerToday.Tulips = 0;
            }

            // and reset global db
            await FlowerRepository.ResetTodayFlowersAsync();
        }
    }
}
