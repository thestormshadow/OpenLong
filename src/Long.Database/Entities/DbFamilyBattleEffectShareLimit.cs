namespace Long.Database.Entities
{
    [Table("family_battle_effect_share_limit")]
    public class DbFamilyBattleEffectShareLimit
    {
        [Key][Column("id")] public uint Identity { get; set; }
        [Column("share_limit")] public ushort ShareLimit { get; set; }
    }
}
