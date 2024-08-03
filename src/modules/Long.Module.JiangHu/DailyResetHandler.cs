using Long.Kernel.Database;
using Long.Kernel.Modules.Interfaces;

namespace Long.Module.JiangHu
{
    public sealed class DailyResetHandler : IDailyResetHandler
    {
        public Task OnDailyResetAsync()
        {
            return ServerDbContext.ScalarAsync("DELETE FROM cq_jianghu_caltivate_times WHERE player_id != 0 LIMIT 1234567890");
        }
    }
}
