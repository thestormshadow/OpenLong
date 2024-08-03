namespace Long.Database.Entities
{
    [Table("cq_golden_league_data")]
    public class DbGoldenLeagueData
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("current_point")] public virtual uint CurrentPoint { get; set; }
        [Column("total_point")] public virtual uint TotalPoint { get; set; }
    }
}
