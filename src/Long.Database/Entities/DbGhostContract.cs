namespace Long.Database.Entities
{
    [Table("ghostcontract")]
    public class DbGhostContract
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("Item_id")] public virtual uint ItemId { get; set; }
        [Column("owner_id")] public virtual uint OwnerId { get; set; }
        [Column("timeout")] public virtual uint Timeout { get; set; }
    }
}
