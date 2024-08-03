namespace Long.Database.Entities
{
    [Table("cq_auction")]
    public class DbAuction
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }
        [Column("class")]
        public ushort Class { get; set; }
        [Column("seller")]
        public uint Seller { get; set; }
        [Column("seller_lookface")]
        public uint SellerLookface { get; set; }
        [Column("moneytype")]
        public byte MoneyType { get; set; }
        [Column("item_id")]
        public uint ItemId { get; set; }
        [Column("price")]
        public uint Price { get; set; }
        [Column("buyout")]
        public uint Buyout { get; set; }
        [Column("time_out")]
        public int TimeOut { get; set; }
    }
}
