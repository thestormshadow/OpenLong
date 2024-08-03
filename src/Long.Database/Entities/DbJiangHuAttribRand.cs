namespace Long.Database.Entities
{
    [Table("cq_jianghu_attrib_rand")]
    public class DbJiangHuAttribRand
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("power_level")]
        public virtual byte PowerLevel { get; set; }
        [Column("power_attrib")]
        public virtual byte PowerAttribute { get; set; }
        [Column("rate")]
        public virtual ushort Rate { get; set; }
    }
}
