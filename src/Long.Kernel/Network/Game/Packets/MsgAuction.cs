using Long.Kernel.Managers;
using Long.Network.Packets;
using Newtonsoft.Json;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgAuction : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgAuction>();

        public MsgAuction()
        {
            Timestamp = Environment.TickCount;
        }

        public int Timestamp { get; set; }
        public Mode Action { get; set; }
        public uint ItemId { get; set; }
        public int MoneyType { get; set; }
        public uint Bid { get; set; }
        public uint Buyout { get; set; }
        public int Duration { get; set; }
        public bool Confirm { get; set; }
        public ushort UnknownLeft { get; set; }
        public ushort UnknownRight { get; set; }

        // get { return Duration == 12 ? 500 : Duration == 24 ? 1100 : Duration >= 48 ? 2250 : 1000000; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Timestamp = reader.ReadInt32();
            Action = (Mode)reader.ReadInt32();
            ItemId = reader.ReadUInt32();
            MoneyType = reader.ReadInt32();
            Bid = reader.ReadUInt32();
            Buyout = reader.ReadUInt32();
            Duration = reader.ReadInt32();
            Confirm = reader.ReadInt32() != 0;
            UnknownLeft = reader.ReadUInt16();
            UnknownRight = reader.ReadUInt16();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgAuction);
            writer.Write(Timestamp);
            writer.Write((int)Action);
            writer.Write(ItemId);
            writer.Write(MoneyType);
            writer.Write(Bid);
            writer.Write(Buyout);
            writer.Write(Duration);
            writer.Write(Confirm ? 1 : 0);
            writer.Write(UnknownLeft);
            writer.Write(UnknownRight);
            return writer.ToArray();
        }

        public enum Mode : byte
        {
            NewListing,
            ListingCancel,
            ListingBid,
            ListingBuyout
        }

        public override async Task ProcessAsync(GameClient client)
        {
            switch (Action)
            {
                case Mode.NewListing:
                    {
                        await AuctionManager.AddNewItemAsync(client.Character, ItemId, (AuctionManager.AuctionMoneyType)MoneyType, Bid, Buyout, Duration);
                        await client.SendAsync(this);
                        break;
                    }

                case Mode.ListingBid:
                    {
                        Confirm = await AuctionManager.PlaceBidAsync(client.Character, ItemId, Bid);
                        await client.SendAsync(this);
                        break;
                    }

                case Mode.ListingCancel:
                    {
                        Confirm = await AuctionManager.CancelAsync(client.Character, ItemId);
                        await client.SendAsync(this);
                        break;
                    }

                case Mode.ListingBuyout:
                    {
                        Confirm = await AuctionManager.BuyAtFixedPriceAsync(client.Character, ItemId);
                        await client.SendAsync(this);
                        break;
                    }

                default:
                    {
                        logger.Warning("MsgAuction>> {0}\n" + JsonConvert.SerializeObject(this), Action);
                        break;
                    }
            }
        }
    }
}
