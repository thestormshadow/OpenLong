namespace Long.Database.Entities
{
    [Table("cq_rebirth")]
    public class DbRebirth
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("need_prof")] public virtual ushort NeedProfession { get; set; }
        [Column("new_prof")] public virtual ushort NewProfession { get; set; }
        [Column("need_level")] public virtual byte NeedLevel { get; set; }
        [Column("new_level")] public virtual byte NewLevel { get; set; }
        [Column("metepsychosis")] public virtual byte Metempsychosis { get; set; }
    }
}