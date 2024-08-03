namespace Long.Database.Entities
{
    [Table("cq_token_type")]
    public class DbTokenType
    {
        [Key]
        [Column("token_id")] public virtual uint Id { get; set; }
        [Column("day_times")] public virtual byte DayTimes { get; set; }
        [Column("use_time")] public virtual uint UseTime { get; set; }
        [Column("cd")] public virtual uint Cooldown { get; set; }
        [Column("fee")] public virtual uint Fee { get; set; }
        [Column("user1")] public virtual int User1 { get; set; }
        [Column("user2")] public virtual int User2 { get; set; }
        [Column("user3")] public virtual int User3 { get; set; }
        [Column("clean_cd")] public virtual byte CleanCooldown { get; set; }
    }
}
