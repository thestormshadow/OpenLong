namespace Long.Database.Entities
{
    [Table("cq_item_cross")]
    public class DbItemCross
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; protected set; }
        [Column("item_type_id")]
        public virtual uint ItemType { get; set; }
    }
}
