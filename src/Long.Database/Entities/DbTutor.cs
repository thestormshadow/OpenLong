namespace Long.Database.Entities
{
    [Table("cq_tutor")]
    public class DbTutor
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("tutor_id")] public virtual uint GuideId { get; set; }
        [Column("Student_id")] public virtual uint StudentId { get; set; }
        [Column("Betrayal_flag")] public virtual int BetrayalFlag { get; set; }
        [Column("Date")] public virtual uint Date { get; set; }
    }
}
