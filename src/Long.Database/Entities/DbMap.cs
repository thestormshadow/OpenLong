namespace Long.Database.Entities
{
    [Table("cq_map")]
    public class DbMap
    {
        // Column Properties
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("name")] public virtual string Name { get; set; }
        [Column("describe_text")] public virtual string Description { get; set; }
        [Column("mapdoc")] public virtual uint MapDoc { get; set; }
        [Column("type")] public virtual ulong Type { get; set; }
        [Column("owner_id")] public virtual uint OwnerIdentity { get; set; }
        [Column("mapgroup")] public virtual uint MapGroup { get; set; }
        [Column("idxserver")] public virtual int ServerIndex { get; set; }
        [Column("weather")] public virtual uint Weather { get; set; }
        [Column("bgmusic")] public virtual uint BackgroundMusic { get; set; }
        [Column("bgmusic_show")] public virtual uint BackgroundMusicShow { get; set; }
        [Column("portal0_x")] public virtual uint PortalX { get; set; }
        [Column("portal0_y")] public virtual uint PortalY { get; set; }
        [Column("reborn_map")] public virtual uint RebornMap { get; set; }
        [Column("reborn_portal")] public virtual uint RebornPortal { get; set; }
        [Column("res_lev")] public virtual byte ResourceLevel { get; set; }
        [Column("owner_type")] public virtual byte OwnerType { get; set; }
        [Column("link_map")] public virtual uint LinkMap { get; set; }
        [Column("link_x")] public virtual ushort LinkX { get; set; }
        [Column("link_y")] public virtual ushort LinkY { get; set; }
        [Column("del_flag")] public virtual byte DeletionFlag { get; set; }
        [Column("color")] public virtual uint Color { get; set; }
    }
}