using Long.Database.Entities;

namespace Long.Ai.Database.Repositories
{
    public static class MonsterClusterRepository
    {
        public static DbMonsterCluster GetById(uint idCluster)
        {
            using var ctx = new ServerDbContext();
            return ctx.MonsterCluster.FirstOrDefault(x => x.Id == idCluster);
        }
    }
}
