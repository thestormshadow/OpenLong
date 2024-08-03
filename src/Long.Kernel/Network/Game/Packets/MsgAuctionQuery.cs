using Long.Kernel.Managers;
using Long.Kernel.States.Auction;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Newtonsoft.Json;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAuctionQuery : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAuctionQuery>();

        public MsgAuctionQuery()
        {
            Timestamp = Environment.TickCount;
        }

        public int Timestamp { get; set; }
        public int Action { get; set; }
        public int ItemCount { get; set; }
        public int Unknown16 { get; set; }
        public List<ItemQuery> Items { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Action = reader.ReadInt32();
            ItemCount = reader.ReadInt32();
            Unknown16 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAuctionQuery);
            writer.Write(Timestamp); // 4
            writer.Write(Action); // 8
            writer.Write(ItemCount); // 12
            writer.Write(Unknown16); // 16
            foreach (var item in Items)
            {
                writer.Write(item.ItemId); // 20
                writer.Write(0); // 24
                writer.Write(item.SellerId); // 28
                writer.Write(item.SellerName, 16); // 32
                writer.Write(item.MoneyType); // 48
                writer.Write(item.CurrentBid); // 52
                writer.Write(item.BuyoutPrice); // 56
                writer.Write(item.Margin); // 60
                writer.Write(item.RemainingDuration); // 64
                writer.Write(item.BidderId); // 68
                writer.Write(item.BidderName, 16); // 72
            }
            return writer.ToArray();
        }

        public enum Mode
        {
            AuctionListing,
            ActiveBids
        }

        public struct ItemQuery
        {
            public uint ItemId { get; set; }
            public uint SellerId { get; set; }
            public string SellerName { get; set; }
            public int MoneyType { get; set; }
            public uint CurrentBid { get; set; }
            public uint BuyoutPrice { get; set; }
            public int Margin { get; set; }
            public int RemainingDuration { get; set; }
            public uint BidderId { get; set; }
            public string BidderName { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            /**
             * Duration
             *  0 Very Long
             *  1 Long
             *  2 Medium
             *  3 Short
             *  4 
             */
            Character user = client.Character;
            switch (Action)
            {
                case 0:
                    {
                        var activeSales = AuctionManager.GetActiveSales(user);
                        foreach (AuctionItem auctionItem in activeSales)
                        {
                            if (ItemCount >= 9)
                            {
                                await user.SendAsync(this);
                                Items.Clear();
                                ItemCount = 0;
                            }

                            ItemCount++;
                            Item item = auctionItem.GetItem();

                            await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Auction));
                            if (item.ItemStatus != null)
                            {
                                await item.ItemStatus.SendToAsync(user);
                            }

                            AuctionBid currentBid = auctionItem.BiggestBid;
                            Items.Add(new ItemQuery
                            {
                                BidderId = currentBid?.BidderId ?? 0,
                                BidderName = currentBid?.BidderName ?? string.Empty,
                                BuyoutPrice = auctionItem.Buyout,
                                CurrentBid = auctionItem.CurrentPrice,
                                ItemId = item.Identity,
                                MoneyType = (int)auctionItem.MoneyType,
                                RemainingDuration = (int)auctionItem.RemainingTime,
                                SellerId = auctionItem.SellerId,
                                SellerName = auctionItem.SellerName
                            });
                        }
                        break;
                    }

                case 1: // active bids
                    {
                        var activeBids = AuctionManager.GetCurrentBid(user);

                        foreach (AuctionItem auctionItem in activeBids)
                        {
                            if (ItemCount >= 9)
                            {
                                await user.SendAsync(this);
                                Items.Clear();
                                ItemCount = 0;
                            }

                            ItemCount++;
                            Item item = auctionItem.GetItem();

                            await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Auction));
                            if (item.ItemStatus != null)
                            {
                                await item.ItemStatus.SendToAsync(user);
                            }

                            AuctionBid currentBid = auctionItem.BiggestBid;
                            Items.Add(new ItemQuery
                            {
                                BidderId = currentBid?.BidderId ?? 0,
                                BidderName = currentBid?.BidderName ?? string.Empty,
                                BuyoutPrice = auctionItem.Buyout,
                                CurrentBid = currentBid?.Price ?? 0,
                                ItemId = item.Identity,
                                MoneyType = (int)auctionItem.MoneyType,
                                RemainingDuration = (int)auctionItem.RemainingTime,
                                SellerId = auctionItem.SellerId,
                                SellerName = auctionItem.SellerName
                            });
                        }
                        break;
                    }
                default:
                    {
                        logger.Warning("MsgAuction>> {0}\n" + JsonConvert.SerializeObject(this), Action);
                        break;
                    }
            }
            await user.SendAsync(this);
        }
    }
}
