namespace Long.Database.Entities
{
    [Table("cq_minectrl")]
    public class DbMineCtrl
    {
        [Key][Column("id")] public virtual uint Id { get; set; }

        [Column("map_id")] public virtual uint MapId { get; set; }
        [Column("item_id")] public virtual uint ItemId { get; set; }
        [Column("percent")] public virtual uint Percent { get; set; }
        [Column("interval")] public virtual ushort Interval { get; set; }
        [Column("amount_limit")] public virtual ushort AmountLimit { get; set; }
    }
}