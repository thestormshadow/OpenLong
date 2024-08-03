namespace Long.Database.Entities
{
    [Table("cq_process_goal")]
    public class DbProcessGoal
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("task_num")] public virtual ushort TaskNum { get; set; }
        [Column("open_lv")] public virtual uint OpenLev { get; set; }
        //[Column("award_condition")] public virtual uint AwardCondition { get; set; }
        //[Column("begintime")] public virtual uint BeginTime { get; set; }
        //[Column("endtime")] public virtual uint EndTime { get; set; }
        [Column("itemtype1")] public virtual uint ItemType1 { get; set; }
        [Column("number1")] public virtual ushort Number1 { get; set; }
        [Column("monoply1")] public virtual byte Monopoly1 { get; set; }
        [Column("itemtype2")] public virtual uint ItemType2 { get; set; }
        [Column("number2")] public virtual ushort Number2 { get; set; }
        [Column("monoply2")] public virtual byte Monopoly2 { get; set; }
        [Column("itemtype3")] public virtual uint ItemType3 { get; set; }
        [Column("number3")] public virtual ushort Number3 { get; set; }
        [Column("monoply3")] public virtual byte Monopoly3 { get; set; }
    }
}
