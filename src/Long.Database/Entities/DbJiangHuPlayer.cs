namespace Long.Database.Entities
{
    [Table("cq_jianghu_player")]
    public class DbJiangHuPlayer
    {
        [Key]
        [Column("id")] public virtual uint Id { get; set; }
        [Column("player_id")] public virtual uint PlayerId { get; set; }
        [Column("gongfu_name")] public virtual string Name { get; set; }
        [Column("power_level")] public virtual byte PowerLevel { get; set; }
        [Column("genuine_qi_level")] public virtual byte GenuineQiLevel { get; set; }
        [Column("free_caltivate_param")] public virtual uint FreeCaltivateParam { get; set; }
        [Column("total_power_value")] public virtual uint TotalPowerValue { get; set; }
        //[Column("history_max_total_power_value")] public virtual uint HistoryMaxTotalPowerValue { get; set; }

        public virtual DbUser? Player { get; set; }
    }
}
