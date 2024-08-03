namespace Long.Database.Entities
{
    [Table("daily_reset")]
    public class DbDailyReset
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("run_time")] public virtual DateTime RunTime { get; set; }
        [Column("ms")] public virtual ulong Duration { get; set; }
    }
}
