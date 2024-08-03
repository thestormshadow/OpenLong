namespace Long.Database.Entities
{
    [Table("cq_totem_add")]
    public class DbTotemAdd
    {
        [Key][Column("id")] public uint Identity { get; set; }

        [Column("totem_type")] public uint TotemType { get; set; }
        [Column("owner_id")] public uint OwnerIdentity { get; set; }
        [Column("battle_add")] public byte BattleAddition { get; set; }
        [Column("time_limit")] public uint TimeLimit { get; set; }
    }
}