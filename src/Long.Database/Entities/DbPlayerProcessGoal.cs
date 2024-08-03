namespace Long.Database.Entities
{
    [Table("cq_player_process_goal")]
    public class DbPlayerProcessGoal
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("goal_id")] public virtual uint GoalId { get; set; }
        [Column("task_complete_state")] public virtual uint TaskCompleteState { get; set; }
        [Column("task_award_state")] public virtual uint TaskAwardState { get; set; }
        [Column("process_award")] public virtual bool ProcessAward { get; set; }
    }
}
