namespace Long.Database.Entities
{
    [Table("cq_region")]
    public class DbRegion
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("mapid")] public virtual uint MapIdentity { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("bound_x")] public virtual uint BoundX { get; set; }
        [Column("bound_y")] public virtual uint BoundY { get; set; }
        [Column("bound_cx")] public virtual uint BoundCX { get; set; }
        [Column("bound_cy")] public virtual uint BoundCY { get; set; }
        [Column("datastr")] public virtual string DataString { get; set; }
        [Column("data0")] public virtual uint Data0 { get; set; }
        [Column("data1")] public virtual uint Data1 { get; set; }
        [Column("data2")] public virtual uint Data2 { get; set; }
        [Column("data3")] public virtual uint Data3 { get; set; }
    }
}