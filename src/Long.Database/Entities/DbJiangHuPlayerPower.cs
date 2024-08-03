namespace Long.Database.Entities
{
    [Table("cq_jianghu_player_power")]
    public class DbJiangHuPlayerPower
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("level")] public virtual byte Level { get; set; }
        [Column("type1")] public virtual byte Type1 { get; set; }
        [Column("quality1")] public virtual byte Quality1 { get; set; }
        [Column("type2")] public virtual byte Type2 { get; set; }
        [Column("quality2")] public virtual byte Quality2 { get; set; }
        [Column("type3")] public virtual byte Type3 { get; set; }
        [Column("quality3")] public virtual byte Quality3 { get; set; }
        [Column("type4")] public virtual byte Type4 { get; set; }
        [Column("quality4")] public virtual byte Quality4 { get; set; }
        [Column("type5")] public virtual byte Type5 { get; set; }
        [Column("quality5")] public virtual byte Quality5 { get; set; }
        [Column("type6")] public virtual byte Type6 { get; set; }
        [Column("quality6")] public virtual byte Quality6 { get; set; }
        [Column("type7")] public virtual byte Type7 { get; set; }
        [Column("quality7")] public virtual byte Quality7 { get; set; }
        [Column("type8")] public virtual byte Type8 { get; set; }
        [Column("quality8")] public virtual byte Quality8 { get; set; }
        [Column("type9")] public virtual byte Type9 { get; set; }
        [Column("quality9")] public virtual byte Quality9 { get; set; }
    }
}
