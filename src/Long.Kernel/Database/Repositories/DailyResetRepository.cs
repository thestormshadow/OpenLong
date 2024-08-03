using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class DailyResetRepository
    {
		public static async Task<DbDailyReset> GetLatestAsync()
		{
			await using var ctx = new ServerDbContext();
			return await ctx.DailyResets.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
		}

		public static DbDailyReset GetLatest()
		{
			using var ctx = new ServerDbContext();
			return ctx.DailyResets.OrderByDescending(x => x.Id).FirstOrDefault();
		}
	}
}
