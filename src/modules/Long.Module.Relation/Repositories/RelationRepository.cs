using Long.Database.Entities;
using Long.Kernel.Database;
using Microsoft.EntityFrameworkCore;

namespace Long.Module.Relation.Repositories
{
    public static class RelationRepository
    {
        public static async Task<List<DbFriend>> GetFriendsAsync(uint idUser)
        {
            await using var context = new ServerDbContext();
            return await context.Friends
                .Include(x => x.User)
                .Include(x => x.Target)
                .Where(x => (x.UserId == idUser || x.TargetId == idUser) && x.User != null && x.Target != null)
                .ToListAsync();
        }

        public static async Task<List<DbEnemy>> GetEnemiesAsync(uint idUser)
        {
            await using var context = new ServerDbContext();
            return await context.Enemies
                .Include(x => x.Target)
                .Where(x => x.UserId == idUser && x.Target != null)
                .ToListAsync();
        }
    }
}
