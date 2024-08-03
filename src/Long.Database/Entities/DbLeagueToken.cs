namespace Long.Database.Entities
{
    [Table("cq_league_token")]
    public class DbLeagueToken
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("league_id")] public virtual uint LeagueId { get; set; }
        [Column("token_type")] public virtual uint TokenType { get; set; }
        [Column("times")] public virtual byte Times { get; set; }
        [Column("last_use_time")] public virtual uint LastUseTime { get; set; }
    }
}
