namespace Long.Database.Entities
{
    [Table("cq_jianghu_caltivate_times")]
    public class DbJiangHuCaltivateTimes
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("free_times")] public virtual byte FreeTimes { get; set; }
        [Column("paid_times")] public virtual uint PaidTimes { get; set; }
    }
}
