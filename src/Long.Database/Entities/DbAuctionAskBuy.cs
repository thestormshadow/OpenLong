namespace Long.Database.Entities
{
    [Table("cq_auction_askbuy")]
    public class DbAuctionAskBuy
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }
        [Column("auctionid")]
        public uint AuctionId { get; set; }
        [Column("buyer")]
        public uint Buyer { get; set; }
        [Column("price")]
        public uint Price { get; set; }
        [Column("amount")]
        public uint Amount { get; set; }
        [Column("timeout")]
        public uint TimeOut { get; set; }
    }
}
