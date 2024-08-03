namespace Long.Database.Entities
{
    [Table("cq_coat_storage_type")]
    public class DbCoatStorageType
    {
        [Key]
        [Column("item_type_id")] public virtual uint ItemType { get; set; }
        [Column("coat_id")] public virtual uint CoatId { get; set; }
        [Column("coat_type")] public virtual uint Type { get; set; }
        [Column("cost_7")] public virtual uint Cost7 { get; set; }
        [Column("cost_30")] public virtual uint Cost30 { get; set; }
        [Column("cost_forever")] public virtual uint CostForever { get; set; }
        [Column("forever_itemtype")] public virtual uint CostForeverItemType { get; set; }
        [Column("star")] public virtual uint Star { get; set; }
    }
}