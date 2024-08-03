namespace Long.Database.Entities
{
    [Table("cq_task")]
    public class DbTask
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("id_next")] public virtual uint IdNext { get; set; }
        [Column("id_nextfail")] public virtual uint IdNextfail { get; set; }
        [Column("itemname1")] public virtual string Itemname1 { get; set; }
        [Column("itemname2")] public virtual string Itemname2 { get; set; }
        [Column("money")] public virtual uint Money { get; set; }
        [Column("profession")] public virtual uint Profession { get; set; }
        [Column("sex")] public virtual uint Sex { get; set; }
        [Column("min_pk")] public virtual int MinPk { get; set; }
        [Column("max_pk")] public virtual int MaxPk { get; set; }
        [Column("team")] public virtual uint Team { get; set; }
        [Column("metempsychosis")] public virtual uint Metempsychosis { get; set; }
        [Column("query")] public virtual ushort Query { get; set; }
        [Column("marriage")] public virtual short Marriage { get; set; }
        [Column("client_active")] public virtual ushort ClientActive { get; set; }
    }
}