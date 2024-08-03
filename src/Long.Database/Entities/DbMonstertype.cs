namespace Long.Database.Entities
{
    [Table("cq_monstertype")]
    public class DbMonstertype
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("lookface")] public virtual ushort Lookface { get; set; }
        [Column("life")] public virtual int Life { get; set; }
        [Column("mana")] public virtual uint Mana { get; set; }
        [Column("attack_max")] public virtual int AttackMax { get; set; }
        [Column("attack_min")] public virtual int AttackMin { get; set; }
        [Column("defence")] public virtual int Defence { get; set; }
        [Column("dexterity")] public virtual uint Dexterity { get; set; }
        [Column("dodge")] public virtual uint Dodge { get; set; }
        [Column("helmet_type")] public virtual uint HelmetType { get; set; }
        [Column("armor_type")] public virtual uint ArmorType { get; set; }
        [Column("weaponr_type")] public virtual uint WeaponrType { get; set; }
        [Column("weaponl_type")] public virtual uint WeaponlType { get; set; }
        [Column("attack_range")] public virtual int AttackRange { get; set; }
        [Column("view_range")] public virtual int ViewRange { get; set; }
        [Column("escape_life")] public virtual int EscapeLife { get; set; }
        [Column("attack_speed")] public virtual int AttackSpeed { get; set; }
        [Column("move_speed")] public virtual int MoveSpeed { get; set; }
        [Column("level")] public virtual ushort Level { get; set; }
        [Column("attack_user")] public virtual uint AttackUser { get; set; }
        [Column("drop_money")] public virtual uint DropMoney { get; set; }
        [Column("drop_itemtype")] public virtual uint DropItemtype { get; set; }
        [Column("size_add")] public virtual uint SizeAdd { get; set; }
        [Column("action")] public virtual uint Action { get; set; }
        [Column("run_speed")] public virtual uint RunSpeed { get; set; }
        [Column("drop_armet")] public virtual byte DropArmet { get; set; }
        [Column("drop_necklace")] public virtual byte DropNecklace { get; set; }
        [Column("drop_armor")] public virtual byte DropArmor { get; set; }
        [Column("drop_ring")] public virtual byte DropRing { get; set; }
        [Column("drop_weapon")] public virtual byte DropWeapon { get; set; }
        [Column("drop_shield")] public virtual byte DropShield { get; set; }
        [Column("drop_shoes")] public virtual byte DropShoes { get; set; }
        [Column("drop_hp")] public virtual uint DropHp { get; set; }
        [Column("drop_mp")] public virtual uint DropMp { get; set; }
        [Column("magic_type")] public virtual uint MagicType { get; set; }
        [Column("magic_def")] public virtual int MagicDef { get; set; }
        [Column("magic_hitrate")] public virtual uint MagicHitrate { get; set; }
        [Column("ai_type")] public virtual uint AiType { get; set; }
        [Column("defence2")] public virtual uint Defence2 { get; set; }
        [Column("stc_type")] public virtual ushort StcType { get; set; }
        [Column("anti_monster")] public virtual byte AntiMonster { get; set; }
        [Column("extra_battlelev")] public virtual ushort ExtraBattlelev { get; set; }
        [Column("extra_exp")] public virtual short ExtraExp { get; set; }

        [Column("extra_damage")] public virtual short ExtraDamage { get; set; }
        [Column("species_type")] public virtual byte SpeciesType { get; set; }

        [Column("stable_defence")] public virtual ushort StableDefence { get; set; }
        [Column("critical_rate")] public virtual ushort CriticalRate { get; set; }
        [Column("magic_critical_rate")] public virtual ushort MagicCriticalRate { get; set; }
        [Column("anti_critical_rate")] public virtual ushort AntiCriticalRate { get; set; }
        [Column("final_dmg_add")] public virtual ushort FinalDmgAdd { get; set; }
        [Column("final_dmg_add_mgc")] public virtual ushort FinalDmgAddMgc { get; set; }
        [Column("final_dmg_reduce")] public virtual ushort FinalDmgReduce { get; set; }
        [Column("final_dmg_reduce_mgc")] public virtual ushort FinalDmgReduceMgc { get; set; }
    }
}
