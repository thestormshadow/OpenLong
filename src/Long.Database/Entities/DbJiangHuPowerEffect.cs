namespace Long.Database.Entities
{
    [Table("cq_jianghu_power_effect")]
    public class DbJiangHuPowerEffect
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("quality")] public virtual byte Quality { get; set; }
        [Column("attrib_value")] public virtual ushort AttribValue { get; set; }
    }
}
