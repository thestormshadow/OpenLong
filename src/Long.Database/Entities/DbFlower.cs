namespace Long.Database.Entities
{
    [Table("flower")]
    public class DbFlower
    {
        [Key]
        [Column("id")] public uint Identity { get; set; }
        [Column("player_id")] public uint UserId { get; set; }
        [Column("flower_r")] public uint RedRose { get; set; }
        [Column("flower_w")] public uint WhiteRose { get; set; }
        [Column("flower_lily")] public uint Orchids { get; set; }
        [Column("flower_tulip")] public uint Tulips { get; set; }

        public virtual DbUser User { get; set; }
    }
}
