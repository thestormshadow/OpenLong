namespace Long.Database.Entities
{
    [Table("cq_superman")]
    public class DbSuperman
    {
        [Key]
        [Column("id")] public uint UserIdentity { get; set; }
        [Column("number")] public uint Amount { get; set; }
    }
}
