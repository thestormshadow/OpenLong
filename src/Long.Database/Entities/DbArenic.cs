namespace Long.Database.Entities
{
    [Table("cq_arenic")]
    public class DbArenic
    {
        [Key][Column("id")] public uint Identity { get; set; }
        [Column("type")] public byte Type { get; set; }
        [Column("date")] public uint Date { get; set; }
        [Column("user_id")] public uint UserId { get; set; }
        [Column("athlete_point")] public uint AthletePoint { get; set; }
        [Column("cur_honor")] public uint CurrentHonor { get; set; }
        [Column("history_honor")] public uint HistoryHonor { get; set; }
        [Column("day_wins")] public uint DayWins { get; set; }
        [Column("day_loses")] public uint DayLoses { get; set; }
        [Column("asyn")] public ushort Asyn { get; set; }
    }
}