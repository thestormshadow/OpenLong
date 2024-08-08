namespace Long.Database.Entities
{
    [Table("cq_setmeed")]
    public class DbSetMeed
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("synid")] public virtual uint SynId { get; set; }
        [Column("money")] public virtual ulong Money { get; set; }
        [Column("emoney")] public virtual uint Emoney { get; set; }
    }
}
