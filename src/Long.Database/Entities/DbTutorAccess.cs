namespace Long.Database.Entities
{
    [Table("cq_tutor_access")]
    public class DbTutorAccess
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("tutor_id")] public virtual uint GuideIdentity { get; set; }
        [Column("Exp")] public virtual ulong Experience { get; set; }
        [Column("God_time")] public virtual ushort Blessing { get; set; }
        [Column("Addlevel")] public virtual ushort Composition { get; set; }
    }
}
