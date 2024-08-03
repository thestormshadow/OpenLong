namespace Long.Database.Entities
{
    [Table("cq_item_drop")]
    public class DbItemDrop
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("user_id")] public virtual uint UserId { get; set; }
        [Column("item_id")] public virtual uint ItemId { get; set; }
        [Column("item_type")] public virtual uint ItemType { get; set; }
        [Column("map")] public virtual uint MapId { get; set; }
        [Column("x")] public virtual ushort X { get; set; }
        [Column("y")] public virtual ushort Y { get; set; }
        [Column("addition")] public virtual byte Addition { get; set; }
        [Column("gem1")] public virtual byte Gem1 { get; set; }
        [Column("gem2")] public virtual byte Gem2 { get; set; }
        [Column("reduce_dmg")] public virtual byte ReduceDmg { get; set; }
        [Column("add_life")] public virtual byte AddLife { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
        [Column("drop_time")] public virtual int DropTime { get; set; }
    }
}
