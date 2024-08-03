namespace Long.Database.Entities
{
    [Table("cq_league")]
    public class DbLeague
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("leader_syn")] public virtual uint LeaderSyn { get; set; }
        [Column("country_date")] public virtual uint CountryDate { get; set; }
        [Column("title")] public virtual string Title { get; set; }
        [Column("money")] public virtual ulong Money { get; set; }
        [Column("announcement")] public virtual string Announcement { get; set; }
        [Column("declaration")] public virtual string Declaration { get; set; }
        [Column("name_flag")] public virtual byte NameFlag { get; set; }
        [Column("brick")] public virtual uint Brick { get; set; }
    }
}
