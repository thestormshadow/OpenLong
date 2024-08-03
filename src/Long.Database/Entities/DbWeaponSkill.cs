namespace Long.Database.Entities
{
    [Table("cq_weapon_skill")]
    public class DbWeaponSkill
    {
        /// <summary>
        ///     The unique identification of the weapon skill.
        /// </summary>
        [Key]
        [Column("id")]
        public virtual uint Identity { get; set; }

        /// <summary>
        ///     The actual level of the weapon skill.
        /// </summary>
        [Column("level")]
        public virtual byte Level { get; set; }

        /// <summary>
        ///     The amount of experience of the actual level.
        /// </summary>
        [Column("exp")]
        public virtual uint Experience { get; set; }

        /// <summary>
        ///     The owner unique identity (character identity).
        /// </summary>
        [Column("owner_id")]
        public virtual uint OwnerIdentity { get; set; }

        /// <summary>
        ///     The old level of the weapon skill before reborn (if higher)
        /// </summary>
        [Column("old_level")]
        public virtual byte OldLevel { get; set; }

        /// <summary>
        ///     If the weapon skill is active. 1 Means that it is waiting the level hit the
        ///     old level to restore the old status.
        /// </summary>
        [Column("unlearn")]
        public virtual byte Unlearn { get; set; }

        /// <summary>
        ///     The 3 digit type of weapon. (410 - Blade)
        /// </summary>
        [Column("type")]
        public virtual uint Type { get; set; }

        [NotMapped]
        public DbWeaponSkillUp WeaponSkillUp { get; set; }
    }
}
