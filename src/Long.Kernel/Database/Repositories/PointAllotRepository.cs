using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class PointAllotRepository
    {
        public static DbPointAllot Get(ushort prof, ushort level)
        {
            using var db = new ServerDbContext();
            return db.PointAllots.FirstOrDefault(x => x.Profession == prof && x.Level == level);
        }
		public static async Task<List<DbPointAllot>> GetAsync()
		{
			await using var db = new ServerDbContext();
			return await db.PointAllots.ToListAsync();
		}
	}
}
