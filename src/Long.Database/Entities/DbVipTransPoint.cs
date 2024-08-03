namespace Long.Database.Entities
{
    [Table("cq_viptranspoint")]
    public class DbVipTransPoint
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("map_x")] public virtual ushort MapX { get; set; }
        [Column("map_y")] public virtual ushort MapY { get; set; }
        [Column("point_name")] public virtual string Name { get; set; }
    }
}
