using Long.Kernel.Managers;
using Long.Kernel.States.Auction;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAuctionItem : MsgBase<GameClient>
    {
        public MsgAuctionItem()
        {
            Timestamp = Environment.TickCount;
        }

        public int Timestamp { get; set; }
        public int Action { get; set; }
        public byte MoneyType { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public byte Quality { get; set; }
        public byte Sockets { get; set; }
        public byte Composition { get; set; }
        public ushort Category { get; set; }
        public ushort LevelLow { get; set; }
        public ushort LevelHigh { get; set; }
        public int Unknown38 { get; set; }
        public int Unknown42 { get; set; }
        public ushort Count { get; set; }
        public ushort TotalCount { get; set; }
        public ushort QueryPage { get; set; }
        public List<AuctionQueryItem> Items { get; set; } = new List<AuctionQueryItem>();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32(); // 4
            Action = reader.ReadInt32(); // 8
            MoneyType = reader.ReadByte(); // 12
            ItemName = reader.ReadString(16).Replace("\0", ""); // 13
            Quality = reader.ReadByte(); // 29
            Sockets = reader.ReadByte(); // 30
            Composition = reader.ReadByte(); // 31
            Category = reader.ReadUInt16(); // 32
            LevelLow = reader.ReadUInt16(); // 34
            LevelHigh = reader.ReadUInt16(); // 36
            Unknown38 = reader.ReadInt32(); // 38
            Unknown42 = reader.ReadInt32(); // 42
            Count = reader.ReadUInt16(); // 46
            TotalCount = reader.ReadUInt16(); // 48
            QueryPage = reader.ReadUInt16(); // 50
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAuctionItem);
            writer.Write(Timestamp); // 4
            writer.Write(Action); // 8
            writer.Write(MoneyType); // 12
            writer.Write(ItemName, 16); // 13
            writer.Write(Quality); // 29
            writer.Write(Sockets); // 30
            writer.Write(Composition); // 31
            writer.Write(Category); // 32
            writer.Write(LevelLow); // 34
            writer.Write(LevelHigh); // 36
            writer.Write(Unknown38); // 38
            writer.Write(Unknown42); // 42
            writer.Write(Count); // 46
            writer.Write(TotalCount); // 48
            writer.Write(QueryPage); // 50
            foreach (var item in Items)
            {
                writer.Write(item.ItemId); // 52
                writer.Write(item.UserId); // 56
                writer.Write(item.SellerId); // 60
                writer.Write(item.SellerName, 16); // 64
                writer.Write(item.MoneyType); // 80
                writer.Write(item.CurrentBid); // 84
                writer.Write(item.BuyoutPrice); // 88
                writer.Write(item.UserId); // 92
                writer.Write(item.RemainingDuration); // 96
                writer.Write(item.BidderId); // 100
                writer.Write(item.BidderName, 16); // 104
            }
            return writer.ToArray();
        }

        public struct AuctionQueryItem
        {
            public uint ItemId { get; set; }
            public uint UserId { get; set; }
            public uint SellerId { get; set; }
            public string SellerName { get; set; }
            public int MoneyType { get; set; }
            public uint CurrentBid { get; set; }
            public uint BuyoutPrice { get; set; }
            public int RemainingDuration { get; set; }
            public uint BidderId { get; set; }
            public string BidderName { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            if (Action == 0)
            {
                var items = AuctionManager.QueryItems(QueryPage, (AuctionManager.AuctionMoneyType)MoneyType, ItemName, Quality, Sockets, Composition, Category, LevelLow, LevelHigh, out var totalCount);
                TotalCount = (ushort)totalCount;
                foreach (var auctionItem in items)
                {
                    Item item = auctionItem.GetItem();
                    await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Auction));
                    if (item.ItemStatus != null)
                    {
                        await item.ItemStatus.SendToAsync(user);
                    }

                    AuctionBid bid = auctionItem.BiggestBid;

                    Count++;
                    Items.Add(new AuctionQueryItem
                    {
                        BidderId = bid?.BidderId ?? 0,
                        BidderName = bid?.BidderName ?? string.Empty,
                        BuyoutPrice = auctionItem.Buyout,
                        CurrentBid = auctionItem.CurrentPrice,
                        ItemId = item.Identity,
                        MoneyType = (int)auctionItem.MoneyType,
                        RemainingDuration = (int)auctionItem.RemainingTime,
                        SellerId = auctionItem.SellerId,
                        SellerName = auctionItem.SellerName,
                        UserId = user.Identity
                    });

                    if (Count >= 14)
                    {
                        await user.SendAsync(this);
                        Count = 0;
                        Items.Clear();
                    }
                }

                await user.SendAsync(this);
            }
        }
    }
}
