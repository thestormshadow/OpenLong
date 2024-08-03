namespace Long.Database.Entities
{
    [Table("cq_weapon_skill_up")]
    public class DbWeaponSkillUp
    {
        [Key]
        [Column("id")]
        public virtual uint Id { get; set; }
        [Column("weapontype")]
        public virtual uint WeaponType { get; set; }
        [Column("level")]
        public virtual byte Level { get; set; }
        [Column("req_exp")]
        public virtual uint ReqExp { get; set; }
        [Column("req_uplevtime")]
        public virtual uint ReqUplevtime { get; set; }
    }
}