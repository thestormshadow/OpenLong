namespace Long.Database.Entities
{
    [Table("cq_business")]
    public class DbBusiness
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("userid")] public virtual uint UserId { get; set; }
        [Column("business")] public virtual uint BusinessId { get; set; }
        [Column("date")] public virtual uint Date { get; set; }
    }
}
