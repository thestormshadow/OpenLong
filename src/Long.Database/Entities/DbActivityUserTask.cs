namespace Long.Database.Entities
{
    [Table("cq_activity_user_task")]
    public class DbActivityUserTask
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("activity_id")] public virtual uint ActivityId { get; set; }
        [Column("complete_flag")] public virtual byte CompleteFlag { get; set; }
        [Column("schedule")] public virtual byte Schedule { get; set; }
    }
}
