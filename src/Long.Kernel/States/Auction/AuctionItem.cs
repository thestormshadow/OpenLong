using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using static Long.Kernel.Managers.AuctionManager;

namespace Long.Kernel.States.Auction
{
    public class AuctionItem
    {
        private static readonly ILogger logger = Log.ForContext<AuctionItem>();

        private readonly DbAuction auction;
        private Item item;
        private readonly Dictionary<uint, AuctionBid> AskBuy = new();
        private readonly object Lock = new();

        public AuctionItem(DbAuction auction)
        {
            this.auction = auction;
        }

        public Character User => RoleManager.GetUser(auction.Seller);

        public async Task<bool> InitializeAsync()
        {
            if (User == null)
            {
                DbUser user = await UserRepository.FindByIdentityAsync(auction.Seller);
                if (user == null)
                {
                    logger.Error("Could not create auction item for auction id [invalid seller]: {}", auction.Id);
                    return false;
                }

                SellerName = user.Name;

                DbItem dbItem = await ItemRepository.GetByIdAsync(auction.ItemId);
                Item tempItem = new();
                if (!await tempItem.CreateAsync(dbItem))
                {
                    logger.Error("Could not create auction item for auction id [invalid item]: {}", auction.Id);
                    return false;
                }
                item = tempItem;
                item.Position = Item.ItemPosition.Auction;
                await item.SaveAsync();
            }
            else
            {
                Character user = User;
                SellerName = user.Name;
                item = user.UserPackage.FindItemByIdentity(auction.ItemId);
            }
            ItemName = item.Name;

            if (auction.Id != 0)
            {
                foreach (var dbAskBuy in await AuctionRepository.GetAskBuyAsync(auction.Id))
                {
                    DbUser user = await UserRepository.FindByIdentityAsync(auction.Seller);
                    if (user == null)
                    {
                        logger.Error("Could not create auction askbuy for auction id [invalid bidder({})]: {}", dbAskBuy.Buyer, auction.Id);
                        continue;
                    }

                    AskBuy.Add(dbAskBuy.Buyer, new AuctionBid(dbAskBuy, user.Name));
                }
            }
            return true;
        }

        public Item GetItem()
        {
            return item;
        }

        public uint SellerId => auction.Seller;
        public string SellerName { get; set; }

        public string ItemName { get; set; }

        public int SocketNum
        {
            get
            {
                int num = 0;
                num += item.SocketOne != Item.SocketGem.NoSocket ? 1 : 0;
                num += item.SocketTwo != Item.SocketGem.NoSocket ? 1 : 0;
                return num;
            }
        }

        public bool Sold { get; private set; }

        public AuctionMoneyType MoneyType => (AuctionMoneyType)auction.MoneyType;

        public uint CurrentPrice => BiggestBid?.Price ?? auction.Price;

        public uint Buyout => auction.Buyout;

        public ushort Category => auction.Class;

        public AuctionBid BiggestBid
        {
            get
            {
                lock (Lock)
                {
                    return AskBuy.Values.OrderByDescending(x => x.Price).FirstOrDefault();
                }
            }
        }

        public AuctionBid UserHasBid(uint idUser)
        {
            lock (Lock)
            {
                return AskBuy.TryGetValue(idUser, out var bid) ? bid : null;
            }
        }

        public AuctionRemainingTime RemainingTime
        {
            get
            {
                if (Sold)
                {
                    return AuctionRemainingTime.Expired;
                }

                DateTime timeOut = UnixTimestamp.ToDateTime(auction.TimeOut);
                var difference = timeOut - DateTime.Now;
                if (difference.TotalDays > 1)
                {
                    return AuctionRemainingTime.VeryLong;
                }
                else if (difference.TotalDays > 12)
                {
                    return AuctionRemainingTime.Long;
                }
                else if (difference.TotalHours > 6)
                {
                    return AuctionRemainingTime.Medium;
                }
                else if (difference.TotalMinutes > 30)
                {
                    return AuctionRemainingTime.Short;
                }

                return AuctionRemainingTime.Expired;
            }
        }

        public async Task CancelAsync()
        {
            if (Sold)
            {
                return;
            }

            if (RemainingTime == AuctionRemainingTime.Expired)
            {
                return;
            }

            List<DbMail> emails = new();
            lock (Lock)
            {
                emails.Add(new DbMail
                {
                    ReceiverId = SellerId,
                    SenderName = StrMailAuctionSender,
                    Action = 0,
                    ConquerPoints = 0,
                    Money = 0,
                    ItemId = item.Identity,
                    EmoneyRecordType = 0,
                    ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                    Title = string.Format(StrMailAuctionCancelSenderTitle, item.FullName),
                    Content = string.Format(StrMailAuctionNoBidsContent, item.FullName),
                });

                foreach (var bid in AskBuy.Values)
                {
                    ulong backMoney = 0;
                    uint backEmoney = 0;
                    if (MoneyType == AuctionMoneyType.Money)
                    {
                        backMoney = bid.Price;
                    }
                    else
                    {
                        backEmoney = bid.Price;
                    }

                    emails.Add(new DbMail
                    {
                        ReceiverId = bid.BidderId,
                        SenderName = StrMailAuctionSender,
                        Action = 0,
                        ConquerPoints = backEmoney,
                        Money = backMoney,
                        ItemId = 0,
                        EmoneyRecordType = 0,
                        ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                        Title = string.Format(StrMailAuctionCancelBidTitle, item.FullName),
                        Content = string.Format(StrMailAuctionCancelBidContent, item.FullName, bid.Price),
                    });
                }
            }
            await ServerDbContext.CreateAsync(emails);
        }

        public async Task<bool> BuyoutAsync(Character buyer)
        {
            if (RemainingTime == AuctionRemainingTime.Expired)
            {
                logger.Warning("Attempt to buy expired auction!");
                return false;
            }

            if (MoneyType == AuctionMoneyType.Money)
            {
                if (!await buyer.SpendMoneyAsync(Buyout, true))
                {
                    return false;
                }
            }
            else
            {
                if (!await buyer.SpendConquerPointsAsync((int)Buyout, true))
                {
                    return false;
                }
                await buyer.SaveEmoneyLogAsync(Character.EmoneyOperationType.AuctionBuy, 0, 0, Buyout);
            }

            await PlaceBidAsync(buyer, Buyout);
            Sold = true;
            return true;
        }

        public async Task TimeOutAsync()
        {
            if (RemainingTime != AuctionRemainingTime.Expired)
            {
                logger.Warning("Attempt of expire not expired auction!");
                return;
            }

            List<DbMail> emails = new();
            AuctionBid auctionBid = BiggestBid;

            lock (Lock)
            {
                if (auctionBid == null)
                {
                    emails.Add(new DbMail
                    {
                        ReceiverId = SellerId,
                        SenderName = StrMailAuctionSender,
                        Action = 0,
                        ConquerPoints = 0,
                        Money = 0,
                        ItemId = item.Identity,
                        EmoneyRecordType = 0,
                        ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                        Title = string.Format(StrMailAuctionNoBidsTitle, item.FullName),
                        Content = string.Format(StrMailAuctionNoBidsContent, item.FullName),
                    });
                }
                else
                {
                    uint fee = 0;
                    ulong money = 0;
                    uint emoney = 0;
                    if (MoneyType == AuctionMoneyType.Money)
                    {
                        money = auctionBid.Price;
                    }
                    else
                    {
                        if (auctionBid.Price >= 20)
                        {
                            fee = (uint)(auctionBid.Price * 0.03d);
                        }
                        emoney = auctionBid.Price - fee;
                    }

                    emails.Add(new DbMail
                    {
                        ReceiverId = SellerId,
                        SenderName = StrMailAuctionSender,
                        Action = 0,
                        ConquerPoints = emoney,
                        Money = money,
                        ItemId = 0,
                        EmoneyRecordType = 0,
                        ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                        Title = string.Format(StrMailAuctionAuctionedSellerTitle, item.FullName),
                        Content = string.Format(StrMailAuctionAuctionedSellerContent, item.FullName, auctionBid.BidderName, auctionBid.Price, fee, auctionBid.Price - fee),
                    }); // seller

                    emails.Add(new DbMail
                    {
                        ReceiverId = auctionBid.BidderId,
                        SenderName = StrMailAuctionSender,
                        Action = 0,
                        ConquerPoints = 0,
                        Money = 0,
                        ItemId = item.Identity,
                        EmoneyRecordType = 0,
                        ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                        Title = string.Format(StrMailAuctionAuctionedBuyerTitle, item.FullName),
                        Content = string.Format(StrMailAuctionAuctionedBuyerContent, item.FullName, SellerName, auctionBid.Price),
                    }); // buyer

                    foreach (var bid in AskBuy.Values)
                    {
                        if (bid.BidderId == auctionBid.BidderId)
                        {
                            continue;
                        }

                        ulong backMoney = 0;
                        uint backEmoney = 0;
                        if (MoneyType == AuctionMoneyType.Money)
                        {
                            backMoney = bid.Price;
                        }
                        else
                        {
                            backEmoney = bid.Price;
                        }
                        emails.Add(new DbMail
                        {
                            ReceiverId = bid.BidderId,
                            SenderName = StrMailAuctionSender,
                            Action = 0,
                            ConquerPoints = backEmoney,
                            Money = backMoney,
                            ItemId = 0,
                            EmoneyRecordType = 0,
                            ExpirationDate = (uint)(UnixTimestamp.Now + DEFAULT_MAIL_TIME),
                            Title = string.Format(StrMailAuctionAuctionedCancelledTitle, item.FullName),
                            Content = string.Format(StrMailAuctionAuctionedCancelledContent, item.FullName, bid.Price),
                        });
                    }
                }
            }

            await ServerDbContext.CreateAsync(emails);
        }

        public async Task PlaceBidAsync(Character user, uint value, int amount = 1)
        {
            if (Sold)
            {
                return;
            }

            AuctionBid bid = null;
            uint payment = value;
            bool add = false;
            lock (Lock)
            {
                if (!AskBuy.TryGetValue(user.Identity, out bid))
                {
                    add = true;
                }
                else
                {
                    payment = value - bid.Price;
                }
            }

            if (MoneyType == AuctionMoneyType.Money)
            {
                if (!await user.SpendMoneyAsync((int)payment, true))
                {
                    return;
                }
            }
            else
            {
                if (!await user.SpendConquerPointsAsync((int)payment, true))
                {
                    return;
                }
                await user.SaveEmoneyLogAsync(Character.EmoneyOperationType.AuctionBid, 0, 0, payment);
            }

            bid ??= new AuctionBid(new DbAuctionAskBuy
            {
                Amount = (uint)amount,
                AuctionId = auction.Id,
                Buyer = user.Identity,
                TimeOut = 0
            }, user.Name);
            bid.Price = value;

            if (add)
            {
                lock (Lock)
                {
                    AskBuy.Add(bid.BidderId, bid);
                }
            }

            await bid.SaveAsync();
        }

        public Task SaveAsync()
        {
            if (auction.Id == 0)
            {
                return ServerDbContext.CreateAsync(auction);
            }
            return ServerDbContext.UpdateAsync(auction);
        }

        public async Task DeleteAsync()
        {
            await ServerDbContext.DeleteAsync(auction);
            foreach (var askBuy in AskBuy.Values)
            {
                await askBuy.DeleteAsync();
            }
        }
    }
}
