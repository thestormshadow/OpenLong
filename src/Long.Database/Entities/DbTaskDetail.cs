namespace Long.Database.Entities
{
    [Table("task_detail")]
    public class DbTaskDetail
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("user_id")] public virtual uint UserIdentity { get; set; }
        [Column("task_id")] public virtual uint TaskIdentity { get; set; }
        [Column("complete_flag")] public virtual ushort CompleteFlag { get; set; }
        [Column("notify_flag")] public virtual byte NotifyFlag { get; set; }
        [Column("data1")] public virtual int Data1 { get; set; }
        [Column("data2")] public virtual int Data2 { get; set; }
        [Column("data3")] public virtual int Data3 { get; set; }
        [Column("data4")] public virtual int Data4 { get; set; }
        [Column("data5")] public virtual int Data5 { get; set; }
        [Column("data6")] public virtual int Data6 { get; set; }
        [Column("data7")] public virtual int Data7 { get; set; }
        [Column("task_overtime")] public virtual uint TaskOvertime { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("max_accumulate_times")] public virtual uint MaxAccumulateTimes { get; set; }
    }
}
