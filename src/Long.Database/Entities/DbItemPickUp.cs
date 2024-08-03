namespace Long.Database.Entities
{
    [Table("cq_item_pick_up")]
    public class DbItemPickUp
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("drop_id")] public virtual uint DropId { get; set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("item_id")] public virtual uint ItemId { get; set; }
        [Column("item_type")] public virtual uint ItemType { get; set; }
        [Column("pickup_time")] public virtual int PickUpTime { get; set; }
    }
}
