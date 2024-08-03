namespace Long.Database.Entities
{
    [Table("cq_tutor_contributions")]
    public class DbTutorContribution
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("tutor_id")] public virtual uint TutorIdentity { get; set; }
        [Column("Student_id")] public virtual uint StudentIdentity { get; set; }
        [Column("God_time")] public virtual ushort GodTime { get; set; }
        [Column("Exp")] public virtual uint Experience { get; set; }
        [Column("Addlevel")] public virtual uint PlusStone { get; set; }
    }
}
