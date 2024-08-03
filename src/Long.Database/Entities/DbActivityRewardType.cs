namespace Long.Database.Entities
{
    [Table("cq_activity_reward_type")]
    public class DbActivityRewardType
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("metempsychosis")] public virtual byte Metempsychosis { get; set; }
        [Column("reward_grade")] public virtual byte RewardGrade { get; set; }
        [Column("activity_req")] public virtual ushort ActivityReq { get; set; }
        [Column("reward1")] public virtual uint Reward1 { get; set; }
        [Column("reward1_num")] public virtual uint Reward1Num { get; set; }
        [Column("reward1_mono")] public virtual byte Reward1Mono { get; set; }
        [Column("reward2")] public virtual uint Reward2 { get; set; }
        [Column("reward2_num")] public virtual uint Reward2Num { get; set; }
        [Column("reward2_mono")] public virtual byte Reward2Mono { get; set; }
        [Column("reward3")] public virtual uint Reward3 { get; set; }
        [Column("reward3_num")] public virtual uint Reward3Num { get; set; }
        [Column("reward3_mono")] public virtual byte Reward3Mono { get; set; }
    }
}
