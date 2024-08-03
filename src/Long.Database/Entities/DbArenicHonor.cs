namespace Long.Database.Entities
{
    [Table("cq_arenic_honor")]
    public class DbArenicHonor
    {
        [Key][Column("id")] public virtual uint Id { get; set; }

        [Column("type")] public virtual byte Type { get; set; }

        [Column("day_prise")] public virtual uint DayPrize { get; set; }

        [Column("month_prise")] public virtual uint MonthPrize { get; set; }
    }
}