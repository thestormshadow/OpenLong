namespace Long.Database.Entities
{
    [Table("cq_point_allot")]
    public class DbPointAllot
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("profession")] public virtual byte Profession { get; set; }
        [Column("level")] public virtual byte Level { get; set; }
        [Column("force")] public virtual ushort Strength { get; set; }
        [Column("speed")] public virtual ushort Agility { get; set; }
        [Column("health")] public virtual ushort Vitality { get; set; }
        [Column("soul")] public virtual ushort Spirit { get; set; }
    }
}