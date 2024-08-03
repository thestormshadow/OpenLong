namespace Long.Database.Entities
{
    [Table("cq_config")]
    public class DbConfig
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual int Type { get; set; }
        [Column("data1")] public virtual int Data1 { get; set; }
        [Column("data2")] public virtual int Data2 { get; set; }
        [Column("data3")] public virtual int Data3 { get; set; }
        [Column("data4")] public virtual int Data4 { get; set; }
        [Column("data5")] public virtual int Data5 { get; set; }
        [Column("describ")] public virtual string Description { get; set; }
    }
}
