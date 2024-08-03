namespace Long.Database.Entities
{
    [Table("cq_slot_winning_rule")]
    public class DbSlotWinningRule
    {
        [Key][Column("id")] public uint Identity { get; set; }
        [Column("type")] public byte Type { get; set; }
        [Column("pattern")] public ushort Pattern { get; set; }
        [Column("weight")] public uint Weight { get; set; }
        [Column("multiple")] public uint Multiple { get; set; }
    }
}
