namespace Long.Database.Entities
{
    [Table("dyna_rank_rec")]
    public class DbNobility
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("rank_type")] public virtual uint RankType { get; set; }
        [Column("value")] public virtual ulong Value { get; set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
    }
}
