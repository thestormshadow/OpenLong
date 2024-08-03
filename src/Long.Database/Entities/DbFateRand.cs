namespace Long.Database.Entities
{
    [Table("cq_fate_rand")]
    public class DbFateRand
    {
        [Column("fate_no")] public virtual byte FateNo { get; set; }
        [Column("range_no")] public virtual byte RangeNo { get; set; }
        [Column("range_rate")] public virtual uint RangeRate { get; set; }
    }
}
