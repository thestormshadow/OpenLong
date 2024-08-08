namespace Long.Database.Entities
{
    [Table("cq_meedrecord")]
    public class DbMeedRecord
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("synid")] public virtual uint SynId { get; set; }
        [Column("userid")] public virtual uint UserId { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("point")] public virtual ushort Point { get; set; }
        [Column("money")] public virtual ulong Money { get; set; }
        [Column("emoney")] public virtual uint Emoney { get; set; }
    }
}
