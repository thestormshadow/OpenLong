using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class AchievementRepository
    {
        public static async Task<DbAchievement> GetAsync(uint idUser)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.Achievements.FindAsync(idUser);
        }

        public static async Task<List<DbAchievementType>> GetTypesAsync()
        {
            await using var db = new ServerDbContext();
            return await db.AchievementTypes.ToListAsync();
        }
    }
}
