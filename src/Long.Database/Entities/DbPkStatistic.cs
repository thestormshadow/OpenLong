namespace Long.Database.Entities
{
    [Table("cq_pk_statistic")]
    public class DbPkStatistic
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("killer_id")] public virtual uint KillerId { get; set; }
        [Column("target_id")] public virtual uint TargetId { get; set; }
        [Column("pk_time")] public virtual uint PkTime { get; set; }
        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("target_battle_effect")] public virtual ushort TargetBattleEffect { get; set; }
        [Column("kill_times")] public virtual uint KillTimes { get; set; }
    }
}
