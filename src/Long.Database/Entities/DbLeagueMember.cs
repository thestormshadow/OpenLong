namespace Long.Database.Entities
{
    [Table("cq_league_member")]
    public class DbLeagueMember
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("league_id")] public virtual uint LeagueId { get; set; }
        [Column("salary_flag")] public virtual byte SalaryFlag { get; set; }
    }
}
