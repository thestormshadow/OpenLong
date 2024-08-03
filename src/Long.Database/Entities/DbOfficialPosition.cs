namespace Long.Database.Entities
{
    [Table("cq_official_position")]
    public class DbOfficialPosition
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("league_id")] public virtual uint LeagueId { get; set; }
        [Column("official_type")] public virtual ushort OfficialType { get; set; }
        [Column("salary_time")] public virtual uint SalaryTime { get; set; }
        [Column("official_time")] public virtual uint OfficialTime { get; set; }
    }
}
