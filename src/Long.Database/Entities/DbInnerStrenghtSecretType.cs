namespace Long.Database.Entities
{
    [Table("cq_inner_strength_secret_type")]
    public class DbInnerStrenghtSecretType
    {
        [Key]
        [Column("id")]
        public virtual byte Identity { get; set; }
        [Column("max_life")]
        public virtual uint MaxLife { get; set; }
        [Column("physic_attack_new")]
        public virtual uint PhysicAttackNew { get; set; }
        [Column("magic_attack")]
        public virtual uint MagicAttack { get; set; }
        [Column("physic_defense_new")]
        public virtual uint PhysicDefenseNew { get; set; }
        [Column("magic_defense")]
        public virtual uint MagicDefense { get; set; }
        [Column("final_physic_add")]
        public virtual ushort FinalPhysicAdd { get; set; }
        [Column("final_magic_add")]
        public virtual ushort FinalMagicAdd { get; set; }
        [Column("final_physic_reduce")]
        public virtual ushort FinalPhysicReduce { get; set; }
        [Column("final_magic_reduce")]
        public virtual ushort FinalMagicReduce { get; set; }
        [Column("physic_crit")]
        public virtual ushort PhysicCrit { get; set; }
        [Column("magic_crit")]
        public virtual ushort MagicCrit { get; set; }
        [Column("defense_crit")]
        public virtual ushort DefenseCrit { get; set; }
        [Column("smash_rate")]
        public virtual ushort SmashRate { get; set; }
        [Column("firm_defense_rate")]
        public virtual ushort FirmDefenseRate { get; set; }
    }
}
