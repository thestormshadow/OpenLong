namespace Long.Database.Entities
{
    [Table("cq_fate_rank")]
    public class DbFateRank
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("fate_no")] public virtual byte FateNo { get; set; }
        [Column("sort")] public virtual byte Sort { get; set; }
        [Column("attrib1")] public virtual int Attrib1 { get; set; }
        [Column("attrib2")] public virtual int Attrib2 { get; set; }
        [Column("attrib3")] public virtual int Attrib3 { get; set; }
        [Column("attrib4")] public virtual int Attrib4 { get; set; }
    }
}
