namespace Long.Database.Entities
{
    [Table("cq_monster_kill")]
    public class DbMonsterKill
    {
        [Key][Column("id")] public uint Identity { get; set; }

        [Column("user_id")] public uint UserIdentity { get; set; }
        [Column("monstertype")] public uint Monster { get; set; }
        [Column("amount")] public ulong Amount { get; set; }
        [Column("created_at")] public DateTime CreatedAt { get; set; }
        [Column("updated_at")] public DateTime? UpdatedAt { get; set; }
    }
}