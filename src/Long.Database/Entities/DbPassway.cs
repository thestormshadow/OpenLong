namespace Long.Database.Entities
{
    [Table("cq_passway")]
    public class DbPassway
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("mapid")] public virtual uint MapId { get; set; }

        [Column("passway_idx")] public virtual uint MapIndex { get; set; }

        [Column("passway_mapid")] public virtual uint TargetMapId { get; set; }

        [Column("passway_mapportal")] public virtual uint TargetPortal { get; set; }
    }
}