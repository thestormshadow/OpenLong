namespace Long.Database.Entities
{
    [Table("cq_inner_strength_type_lev_info")]
    public class DbInnerStrenghtTypeLevInfo
    {
        [Key]
        [Column("id")] public virtual uint Identity { get; set; }
        [Column("type")] public virtual ushort Type { get; set; }
        [Column("level")] public virtual byte Level { get; set; }
        [Column("culture_value")] public virtual uint CultureValue { get; set; }
        [Column("max_life")] public virtual int MaxLife { get; set; }
        [Column("physic_attack_new")] public virtual int PAttackNew { get; set; }
        [Column("magic_attack")] public virtual int MagicAttack { get; set; }
        [Column("physic_defense_new")] public virtual int PDefenseNew { get; set; }
        [Column("magic_defense")] public virtual int MagicDefense { get; set; }
        [Column("final_physic_add")] public virtual int FinalDmgAdd { get; set; }
        [Column("final_magic_add")] public virtual int FinalMgcDmgAdd { get; set; }
        [Column("final_physic_reduce")] public virtual int FinalDmgReduce { get; set; }
        [Column("final_magic_reduce")] public virtual int FinalMgcDmgReduce { get; set; }
        [Column("physic_crit")] public virtual int CriticalAdd { get; set; }
        [Column("magic_crit")] public virtual int MgcCriticalAdd { get; set; }
        [Column("defense_crit")] public virtual int AntiCriticalAdd { get; set; }
        [Column("smash_rate")] public virtual int SmashAdd { get; set; }
        [Column("firm_defense_rate")] public virtual int FirmDefenseAdd { get; set; }
    }
}
