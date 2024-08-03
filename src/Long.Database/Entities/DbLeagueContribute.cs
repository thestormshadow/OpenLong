namespace Long.Database.Entities
{
    [Table("cq_league_contribute")]
    public class DbLeagueContribute
    {
        [Key]
        [Column("level")] public virtual uint Level { get; protected set; }
        [Column("need_contribute")] public virtual uint NeedContribute { get; set; }
        [Column("official_name")] public virtual string OfficialName { get; set; }
    }
}
