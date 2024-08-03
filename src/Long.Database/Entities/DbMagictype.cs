namespace Long.Database.Entities
{
    [Table("cq_magictype")]
    public class DbMagictype
    {
        [Key][Column("id")] public virtual uint Id { get; set; }
        [Column("type")] public virtual uint Type { get; set; }
        [Column("sort")] public virtual uint Sort { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("crime")] public virtual byte Crime { get; set; }
        [Column("ground")] public virtual byte Ground { get; set; }
        [Column("multi")] public virtual byte Multi { get; set; }
        [Column("target")] public virtual uint Target { get; set; }
        [Column("level")] public virtual uint Level { get; set; }
        [Column("use_mp")] public virtual uint UseMp { get; set; }
        [Column("power")] public virtual int Power { get; set; }
        [Column("intone_speed")] public virtual uint IntoneSpeed { get; set; }
        [Column("percent")] public virtual uint Percent { get; set; }
        [Column("step_secs")] public virtual uint StepSecs { get; set; }
        [Column("range")] public virtual uint Range { get; set; }
        [Column("distance")] public virtual uint Distance { get; set; }
        [Column("status")] public virtual long Status { get; set; }
        [Column("need_prof")] public virtual uint NeedProf { get; set; }
        [Column("need_exp")] public virtual int NeedExp { get; set; }
        [Column("need_level")] public virtual uint NeedLevel { get; set; }
        [Column("use_xp")] public virtual byte UseXp { get; set; }
        [Column("weapon_subtype")] public virtual uint WeaponSubtype { get; set; }
        [Column("active_times")] public virtual uint ActiveTimes { get; set; }
        [Column("auto_active")] public virtual uint AutoActive { get; set; }
        [Column("floor_attr")] public virtual uint FloorAttr { get; set; }
        [Column("auto_learn")] public virtual byte AutoLearn { get; set; }
        [Column("learn_level")] public virtual uint LearnLevel { get; set; }
        [Column("drop_weapon")] public virtual byte DropWeapon { get; set; }
        [Column("use_ep")] public virtual uint UseEp { get; set; }
        [Column("weapon_hit")] public virtual byte WeaponHit { get; set; }
        [Column("use_item")] public virtual uint UseItem { get; set; }
        [Column("next_magic")] public virtual uint NextMagic { get; set; }
        [Column("delay_ms")] public virtual uint DelayMs { get; set; }
        [Column("use_item_num")] public virtual uint UseItemNum { get; set; }
        [Column("attr_type")] public virtual byte ElementType { get; set; }      // 2016-12-11
        [Column("attr_power")] public virtual uint ElementPower { get; set; }    // 2016-12-12
        [Column("coldtime")] public virtual uint Timeout { get; set; }           // 2017-03-13
        [Column("req_uplevtime")] public virtual uint ReqUplevTime { get; set; } // 2017-03-13
        [Column("status_data0")] public virtual uint StatusData0 { get; set; }
        [Column("status_data1")] public virtual uint StatusData1 { get; set; }
        [Column("status_data2")] public virtual uint StatusData2 { get; set; }
        [Column("width")] public virtual uint Width { get; set; }
        [Column("data")] public virtual uint Data { get; set; }
        [Column("dur_time")] public virtual uint DurTime { get; set; }
        [Column("atk_interval")] public virtual uint AtkInterval { get; set; }
    }
}
