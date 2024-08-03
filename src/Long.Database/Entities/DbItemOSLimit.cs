namespace Long.Database.Entities
{
    [Table("cq_item_os_limit")]
    public class DbItemOSLimit
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; protected set; }
        [Column("item_type")]
        public virtual uint ItemType { get; set; }
    }
}
