namespace Long.Database.Entities
{
    [Table("cq_itemtype")]
    public class DbItemtype
    {
        [Key][Column("id")] public virtual uint Type { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("req_profession")] public virtual uint ReqProfession { get; set; }
        [Column("req_weaponskill")] public virtual byte ReqWeaponskill { get; set; }
        [Column("req_level")] public virtual byte ReqLevel { get; set; }
        [Column("req_sex")] public virtual byte ReqSex { get; set; }
        [Column("req_force")] public virtual ushort ReqForce { get; set; }
        [Column("req_speed")] public virtual ushort ReqSpeed { get; set; }
        [Column("req_health")] public virtual ushort ReqHealth { get; set; }
        [Column("req_soul")] public virtual ushort ReqSoul { get; set; }
        [Column("monopoly")] public virtual uint Monopoly { get; set; }
        [Column("price")] public virtual uint Price { get; set; }
        [Column("id_action")] public virtual uint IdAction { get; set; }
        [Column("attack_max")] public virtual ushort AttackMax { get; set; }
        [Column("attack_min")] public virtual ushort AttackMin { get; set; }
        [Column("defense")] public virtual short Defense { get; set; }
        [Column("dexterity")] public virtual short Dexterity { get; set; }
        [Column("dodge")] public virtual short Dodge { get; set; }
        [Column("life")] public virtual short Life { get; set; }
        [Column("mana")] public virtual short Mana { get; set; }
        [Column("amount")] public virtual ushort Amount { get; set; }
        [Column("amount_limit")] public virtual ushort AmountLimit { get; set; }
        [Column("ident")] public virtual byte Ident { get; set; }
        [Column("gem1")] public virtual byte Gem1 { get; set; }
        [Column("gem2")] public virtual byte Gem2 { get; set; }
        [Column("magic1")] public virtual uint Magic1 { get; set; }
        [Column("magic2")] public virtual byte Magic2 { get; set; }
        [Column("magic3")] public virtual byte Magic3 { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
        [Column("magic_atk")] public virtual ushort MagicAtk { get; set; }
        [Column("magic_def")] public virtual ushort MagicDef { get; set; }
        [Column("atk_range")] public virtual ushort AtkRange { get; set; }
        [Column("atk_speed")] public virtual ushort AtkSpeed { get; set; }
        [Column("fray_mode")] public virtual byte FrayMode { get; set; }
        [Column("repair_mode")] public virtual byte RepairMode { get; set; }
        [Column("type_mask")] public virtual byte TypeMask { get; set; }
        [Column("emoney_price")] public virtual uint EmoneyPrice { get; set; }
        [Column("emoney_mono_price")] public virtual uint BoundEmoneyPrice { get; set; } // 2014-12-14
        [Column("save_time")] public virtual uint SaveTime { get; set; }                 // 2020-08-06
        [Column("critical_rate")] public virtual uint CriticalStrike { get; set; }
        [Column("magic_critical_rate")] public virtual uint SkillCritStrike { get; set; }
        [Column("anti_critical_rate")] public virtual uint Immunity { get; set; }
        [Column("magic_penetration")] public virtual uint Penetration { get; set; }
        [Column("shield_block")] public virtual uint Block { get; set; }
        [Column("crash_attack")] public virtual uint Breakthrough { get; set; }
        [Column("stable_defence")] public virtual uint Counteraction { get; set; }
        [Column("accumulate_limit")] public virtual uint AccumulateLimit { get; set; }
        [Column("attr_metal")] public virtual uint ResistMetal { get; set; }
        [Column("attr_wood")] public virtual uint ResistWood { get; set; }
        [Column("attr_water")] public virtual uint ResistWater { get; set; }
        [Column("attr_fire")] public virtual uint ResistFire { get; set; }
        [Column("attr_earth")] public virtual uint ResistEarth { get; set; }
        [Column("godsoullev")] public virtual byte Phase { get; set; }
        [Column("type_desc")] public virtual string TypeDesc { get; set; }
        [Column("item_desc")] public virtual string ItemDesc { get; set; }
        [Column("meteor_count")] public virtual uint MeteorAmount { get; set; } // 2014-12-14
        [Column("auction_class")] public virtual ushort AuctionClass { get; set; } // 2022-11-19 Felipe
        [Column("recover_energy")] public virtual uint RecoverEnergy { get; set; }
        [Column("final_dmg_add")] public virtual ushort FinalDmgAdd { get; set; }
        [Column("final_dmg_add_mgc")] public virtual ushort FinalDmgAddMgc { get; set; }
        [Column("final_dmg_reduce")] public virtual ushort FinalDmgReduce { get; set; }
        [Column("final_dmg_reduce_mgc")] public virtual ushort FinalDmgReduceMgc { get; set; }
    }
}
