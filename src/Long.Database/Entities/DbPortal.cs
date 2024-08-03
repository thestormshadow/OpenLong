namespace Long.Database.Entities
{
    [Table("cq_portal")]
    public class DbPortal
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("mapid")] public virtual uint MapId { get; set; }

        [Column("portal_idx")] public virtual uint PortalIndex { get; set; }

        [Column("portal_x")] public virtual uint PortalX { get; set; }

        [Column("portal_y")] public virtual uint PortalY { get; set; }
    }
}