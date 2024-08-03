namespace Long.Database.Entities
{
    [Table("cq_trade_item")]
    public class DbTradeItem
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("trade_id")] public virtual uint TradeIdentity { get; set; }
        [Column("user_id")] public virtual uint SenderIdentity { get; set; }
        [Column("item_id")] public virtual uint ItemIdentity { get; set; }
        [Column("itemtype")] public virtual uint Itemtype { get; set; }
        [Column("chksum")] public virtual uint Chksum { get; set; }
        [Column("json_data")] public virtual string JsonData { get; set; }
    }
}
