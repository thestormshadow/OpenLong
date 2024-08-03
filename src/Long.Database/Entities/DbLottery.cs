namespace Long.Database.Entities
{
    [Table("cq_lottery")]
    public class DbLottery
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }

        [Column("type")] public virtual uint Type { get; set; }
        [Column("rank")] public virtual byte Rank { get; set; }
        [Column("chance")] public virtual uint Chance { get; set; }
        [Column("prize_name")] public virtual string Itemname { get; set; }
        [Column("prize_item")] public virtual uint ItemIdentity { get; set; }
        [Column("color")] public virtual byte Color { get; set; }
        [Column("hole_num")] public virtual byte SocketNum { get; set; }
        [Column("addition_lev")] public virtual byte Plus { get; set; }
    }
}
