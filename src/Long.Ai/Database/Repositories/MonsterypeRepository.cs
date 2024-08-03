using Long.Database.Entities;

namespace Long.Ai.Database.Repositories
{
    public static class MonsterypeRepository
    {
        public static async Task<List<DbMonstertype>> GetAsync()
        {
            await using var context = new ServerDbContext();
            return context.Monstertype.ToList();
        }
    }
}
