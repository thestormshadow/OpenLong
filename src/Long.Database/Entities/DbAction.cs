namespace Long.Database.Entities
{
    [Table("cq_action")]
    public class DbAction
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("id_next")] public virtual uint IdNext { get; set; }
        [Column("id_nextfail")] public virtual uint IdNextfail { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
        [Column("param")] public virtual string Param { get; set; }
    }
}