namespace Long.Database.Entities
{
    [Table("cq_traptype")]
    public class DbTrapType
    {
        [Key][Column("id")] public virtual uint Id { get; set; }

        [Column("sort")] public virtual uint Sort { get; set; }
        [Column("look")] public virtual uint Look { get; set; }
        [Column("action_id")] public virtual uint ActionId { get; set; }
        [Column("level")] public virtual byte Level { get; set; }
        [Column("attack_max")] public virtual int AttackMax { get; set; }
        [Column("attack_min")] public virtual int AttackMin { get; set; }
        [Column("dexterity")] public virtual int Dexterity { get; set; }
        [Column("attack_speed")] public virtual int AttackSpeed { get; set; }
        [Column("active_times")] public virtual int ActiveTimes { get; set; }
        [Column("magic_type")] public virtual ushort MagicType { get; set; }
        [Column("magic_hitrate")] public virtual int MagicHitrate { get; set; }
        [Column("size")] public virtual int Size { get; set; }
        [Column("atk_mode")] public virtual int AtkMode { get; set; }
    }
}