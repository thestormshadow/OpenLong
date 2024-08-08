namespace Long.Database.Entities
{
    [Table("cq_syncompeterank")]
    public class DbSynCompeteRank
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("rank")] public virtual byte Rank { get; set; }
        [Column("synid")] public virtual uint SynId { get; set; }
        [Column("relation")] public virtual ushort Relation { get; set; }
        [Column("point")] public virtual uint Point { get; set; }
    }
}
