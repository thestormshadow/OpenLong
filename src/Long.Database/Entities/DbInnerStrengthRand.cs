namespace Long.Database.Entities
{
    [Table("cq_inner_strength_rand")]
    public class DbInnerStrengthRand
    {
        [Column("inner_strength_no")]
        public byte StrengthNo { get; set; }
        [Column("range_no")]
        public byte RangeNo { get; set; }
        [Column("range_rate")]
        public uint RangeRate { get; set; }
    }
}
