using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class MonsterypeRepository
    {
        public static async Task<List<DbMonstertype>> GetAsync()
        {
            await using var context = new ServerDbContext();
            return await context.Monstertypes.ToListAsync();
        }
    }
}
