namespace Long.Database.Entities
{
    [Table("cq_statistic_daily")]
    public class DbStatisticDaily
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("subtype")] public virtual uint SubType { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
    }
}
