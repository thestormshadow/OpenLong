using Long.Database.Entities;

namespace Long.Ai.States
{
    public sealed class MonsterRatio
    {
        private readonly DbMonstertype monstertype;

        public MonsterRatio(DbMonstertype monstertype)
        {
            this.monstertype = monstertype;
        }

        public int Ratio { get; init; }
        public DbMonstertype MonsterType => monstertype;
    }
}
