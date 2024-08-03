using Long.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Long.Kernel.Database.Repositories
{
    public static class MonsterTypeMagicRepository
    {
        public static async Task<List<DbMonsterTypeMagic>> GetAsync()
        {
            await using var ctx = new ServerDbContext();
            return await ctx.MonsterTypeMagic.ToListAsync();
        }

        public static async Task<List<DbMonsterTypeMagic>> GetAsync(uint monsterType)
        {
            await using var ctx = new ServerDbContext();
            return await ctx.MonsterTypeMagic.Where(x => x.MonsterType == monsterType).ToListAsync();
        }
    }
}
