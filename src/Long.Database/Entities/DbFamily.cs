namespace Long.Database.Entities
{
    [Table("family")]
    public class DbFamily
    {
        [Key][Column("id")] public uint Identity { get; set; }

        [Column("family_name")] public string Name { get; set; }
        [Column("rank")] public byte Rank { get; set; }
        [Column("lead_id")] public uint LeaderIdentity { get; set; }
        [Column("annouce")] public string Announcement { get; set; }
        [Column("money")] public ulong Money { get; set; }
        [Column("repute")] public uint Repute { get; set; }
        [Column("amount")] public byte Amount { get; set; }
        [Column("enemy_family_0_id")] public uint EnemyFamily0 { get; set; }
        [Column("enemy_family_1_id")] public uint EnemyFamily1 { get; set; }
        [Column("enemy_family_2_id")] public uint EnemyFamily2 { get; set; }
        [Column("enemy_family_3_id")] public uint EnemyFamily3 { get; set; }
        [Column("enemy_family_4_id")] public uint EnemyFamily4 { get; set; }
        [Column("ally_family_0_id")] public uint AllyFamily0 { get; set; }
        [Column("ally_family_1_id")] public uint AllyFamily1 { get; set; }
        [Column("ally_family_2_id")] public uint AllyFamily2 { get; set; }
        [Column("ally_family_3_id")] public uint AllyFamily3 { get; set; }
        [Column("ally_family_4_id")] public uint AllyFamily4 { get; set; }
        [Column("create_date")] public uint CreationDate { get; set; }
        [Column("create_name")] public string CreateName { get; set; }
        [Column("challenge_map")] public uint ChallengeMap { get; set; }
        [Column("family_map")] public uint FamilyMap { get; set; }
        [Column("star_tower")] public byte StarTower { get; set; }
        [Column("challenge")] public uint Challenge { get; set; }
        [Column("occupy")] public uint Occupy { get; set; }
        [Column("del_flag")] public uint DeleteDate { get; set; }
    }
}