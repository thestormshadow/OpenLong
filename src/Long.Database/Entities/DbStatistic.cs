namespace Long.Database.Entities
{
    [Table("cq_statistic")]
    public class DbStatistic
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("player_id")] public virtual uint PlayerIdentity { get; set; }
        [Column("event_type")] public virtual uint EventType { get; set; }
        [Column("data_type")] public virtual uint DataType { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
        [Column("timestamp")] public virtual uint Timestamp { get; set; }
    }
}
