namespace Long.Database.Entities
{
    [Table("cq_levexp")]
    public class DbLevelExperience
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("level")] public virtual byte Level { get; set; }
        [Column("exp")] public virtual ulong Exp { get; set; }
        [Column("up_lev_time")] public virtual int UpLevTime { get; set; }
        [Column("mentor_uplev_time")] public virtual uint MentorUpLevTime { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
    }
}
