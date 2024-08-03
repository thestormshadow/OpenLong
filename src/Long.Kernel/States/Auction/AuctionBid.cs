using Long.Database.Entities;
using Long.Kernel.Database;

namespace Long.Kernel.States.Auction
{
    public class AuctionBid
    {
        private readonly DbAuctionAskBuy askBuy;
        public AuctionBid(DbAuctionAskBuy askBuy, string bidderName)
        {
            this.askBuy = askBuy;
            BidderName = bidderName;
        }

        public uint BidderId => askBuy.Buyer;
        public string BidderName { get; init; }
        public uint Price
        {
            get => askBuy.Price;
            set => askBuy.Price = value;
        }

        public Task SaveAsync()
        {
            if (askBuy.Id == 0)
            {
                return ServerDbContext.CreateAsync(askBuy);
            }
            return ServerDbContext.UpdateAsync(askBuy);
        }

        public Task DeleteAsync()
        {
            return ServerDbContext.DeleteAsync(askBuy);
        }
    }
}
