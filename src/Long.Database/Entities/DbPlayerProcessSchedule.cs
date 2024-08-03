namespace Long.Database.Entities
{
    [Table("cq_player_process_task")]
    public class DbPlayerProcessSchedule
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("task_id")] public virtual uint TaskId { get; set; }
        [Column("schedule")] public virtual uint Schedule { get; set; }
    }
}
