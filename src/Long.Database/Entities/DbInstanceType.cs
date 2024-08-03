namespace Long.Database.Entities
{
    [Table("cq_instancetype")]
    public class DbInstanceType
    {
        [Key]
        [Column("id")] public virtual uint Id { get; protected set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("mapid")] public virtual uint MapId { get; set; }
        [Column("type")] public virtual byte Type { get; set; }
        [Column("lev_min")] public virtual uint LevelMin { get; set; }
        [Column("lev_max")] public virtual uint LevelMax { get; set; }
        [Column("battle_min")] public virtual ushort BattleMin { get; set; }
        [Column("time_limit")] public virtual ushort TimeLimit { get; set; }
        [Column("action")] public virtual uint Action { get; set; }
        [Column("mapid_return")] public virtual uint ReturnMapId { get; set; }
        [Column("posx_return")] public virtual ushort ReturnMapX { get; set; }
        [Column("posy_return")] public virtual ushort ReturnMapY { get; set; }
    }
}
