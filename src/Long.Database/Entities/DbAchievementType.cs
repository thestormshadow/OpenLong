namespace Long.Database.Entities
{
    [Table("cq_achievementtype")]
    public class DbAchievementType
    {
        [Key]
        [Column("id")]
        public uint Identity { get; set; }
        [Column("name")] public string Name { get; set; }
        [Column("position")] public ushort Position { get; set; }
        [Column("point")] public byte Point { get; set; }
        [Column("facebook")] public byte Facebook { get; set; }
    }
}
