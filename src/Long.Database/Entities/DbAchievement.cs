namespace Long.Database.Entities
{
    [Table("cq_achievement")]
    public class DbAchievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("userid")] public virtual uint UserIdentity { get; set; }
        [Column("point")] public virtual uint Point { get; set; }
        [Column("achieve1")] public virtual uint Achieve1 { get; set; }
        [Column("achieve2")] public virtual uint Achieve2 { get; set; }
        [Column("achieve3")] public virtual uint Achieve3 { get; set; }
        [Column("achieve4")] public virtual uint Achieve4 { get; set; }
        [Column("achieve5")] public virtual uint Achieve5 { get; set; }
        [Column("achieve6")] public virtual uint Achieve6 { get; set; }
        [Column("achieve7")] public virtual uint Achieve7 { get; set; }
        [Column("achieve8")] public virtual uint Achieve8 { get; set; }
        [Column("achieve9")] public virtual uint Achieve9 { get; set; }
        [Column("achieve10")] public virtual uint Achieve10 { get; set; }
        [Column("achieve11")] public virtual uint Achieve11 { get; set; }
        [Column("achieve12")] public virtual uint Achieve12 { get; set; }
        [Column("achieve13")] public virtual uint Achieve13 { get; set; }
    }
}
