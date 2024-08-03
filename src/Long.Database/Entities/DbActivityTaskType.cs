namespace Long.Database.Entities
{
    [Table("cq_activity_task_type")]
    public class DbActivityTaskType
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("open_lev")] public virtual uint OpenLev { get; set; }
        [Column("close_lev")] public virtual uint CloseLev { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("max_num")] public virtual byte MaxNum { get; set; }
        [Column("activity")] public virtual byte Activity { get; set; }
    }
}
