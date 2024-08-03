namespace Long.Database.Entities
{
    [Table("cq_trap")]
    public class DbTrap
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual uint TypeId { get; set; }
        [Column("look")] public virtual uint Look { get; set; }
        [Column("owner_id")] public virtual uint OwnerId { get; set; }
        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("pos_x")] public virtual ushort PosX { get; set; }
        [Column("pos_y")] public virtual ushort PosY { get; set; }
        [Column("data")] public virtual uint Data { get; set; }

        public virtual DbTrapType Type { get; set; }
    }
}