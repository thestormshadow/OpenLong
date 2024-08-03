namespace Long.Database.Entities
{
    [Table("cq_process_task")]
    public class DbProcessTask
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual ushort Type { get; set; }
        [Column("sort")] public virtual byte Sort { get; set; }
        [Column("condition")] public virtual uint Condition { get; set; }
        [Column("schedule")] public virtual ulong Schedule { get; set; }
        [Column("itemtype")] public virtual uint ItemType { get; set; }
        [Column("number")] public virtual ushort Number { get; set; }
        [Column("monoply")] public virtual byte Monopoly { get; set; }
        //[Column("activate")] public virtual byte Activate { get; set; }
        //[Column("itemtype2")] public virtual uint ItemType2 { get; set; }
        //[Column("number2")] public virtual ushort Number2 { get; set; }
        //[Column("monoply2")] public virtual byte Monopoly2 { get; set; }
        //[Column("activate2")] public virtual byte Activate2 { get; set; }
        //[Column("itemtype3")] public virtual uint ItemType3 { get; set; }
        //[Column("number3")] public virtual ushort Number3 { get; set; }
        //[Column("monoply3")] public virtual byte Monopoly3 { get; set; }
        //[Column("activate3")] public virtual byte Activate3 { get; set; }
    }
}
