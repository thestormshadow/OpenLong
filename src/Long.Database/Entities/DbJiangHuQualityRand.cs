namespace Long.Database.Entities
{
    [Table("cq_jianghu_quality_rand")]
    public class DbJiangHuQualityRand
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("power_level")] public virtual byte PowerLevel { get; set; }
        [Column("power_quality")] public virtual byte PowerQuality { get; set; }
        [Column("common_rate")] public virtual ushort CommonRate { get; set; }
        [Column("crit_rate")] public virtual ushort CritRate { get; set; }
        [Column("high_crit_rate")] public virtual ushort HighCritRate { get; set; }
    }
}
