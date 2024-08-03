namespace Long.Database.Entities
{
    [Table("cq_super_flag")]
    public class DbSuperFlag
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("item_id")] public virtual uint ItemId { get; set; }
        [Column("posindex")] public virtual uint PosIndex { get; set; }
        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("pos_x")] public virtual uint MapX { get; set; }
        [Column("pos_y")] public virtual uint MapY { get; set; }
        [Column("name")] public virtual string Name { get; set; }
    }
}
