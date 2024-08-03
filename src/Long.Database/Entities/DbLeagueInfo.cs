namespace Long.Database.Entities
{
    [Table("cq_league_info")]
    public class DbLeagueInfo
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("idserver")] public virtual uint IdServer { get; set; }
        [Column("idleague")] public virtual uint IdLeague { get; set; }
        [Column("amount")] public virtual uint Amount { get; set; }
        [Column("country_date")] public virtual uint CountryDate { get; set; }
        [Column("league_name")] public virtual string LeagueName { get; set; }
        [Column("leader_name")] public virtual string LeaderName { get; set; }
        [Column("title")] public virtual string Title { get; set; }
    }
}
