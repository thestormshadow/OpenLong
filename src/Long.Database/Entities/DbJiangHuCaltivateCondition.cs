namespace Long.Database.Entities
{
    [Table("cq_jianghu_caltivate_condition")] // cultivate? XD
    public class DbJiangHuCaltivateCondition
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("power_level")] public virtual byte PowerLevel { get; set; }
        [Column("need_power_value")] public virtual ushort NeedPowerValue { get; set; }
        [Column("need_caltivate_value")] public virtual uint NeedCultivateValue { get; set; }
        [Column("crit_cost")] public virtual uint CritCost { get; set; }
        [Column("high_crit_cost")] public virtual uint HighCritCost { get; set; }
        [Column("keep_cost")] public virtual ushort KeepCost { get; set; }
    }
}
