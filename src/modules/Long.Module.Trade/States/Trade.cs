using Long.Database.Entities;
using Long.Kernel;
using Long.Kernel.Database;
using Long.Kernel.Modules.Systems.Trade;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Module.Trade.Network;
using Long.Shared.Helpers;
using Serilog;
using System.Collections.Concurrent;

namespace Long.Module.Trade.States
{
    public sealed class Trade : ITrade
    {
        private static readonly ILogger suspiciousLogger = Logger.CreateConsoleLogger("trade_suspicious");

        private const int MaxTradeItems = 20;
        private const int MaxTradeMoney = 1_000_000_000;
        private const int MaxTradeEmoney = 1_000_000_000;
        private const double MinValueCoefficient = 0.05d;

        private readonly ConcurrentDictionary<uint, Item> itemsPlayer1 = new();
        private readonly ConcurrentDictionary<uint, Item> itemsPlayer2 = new();

        private uint suspiciousWarningTarget;

        private uint money1, money2;
        private uint emoney1, emoney2;

        private bool accept1, accept2;

        public Trade(Character p1, Character p2)
        {
            User1 = p1;
            User2 = p2;

            User1.Trade = this;
            User2.Trade = this;
        }

        public Character User1 { get; }
        public Character User2 { get; }

        public bool Accepted => accept1 && accept2;

        public bool ContainsItem(uint idItem)
        {
            return itemsPlayer1.ContainsKey(idItem) || itemsPlayer2.ContainsKey(idItem);
        }

        public async Task<bool> AddItemAsync(uint idItem, Character sender)
        {
            if (sender.Identity != User1.Identity
                && sender.Identity != User2.Identity)
            {
                return false;
            }

            Character target = sender.Identity == User1.Identity ? User2 : User1;
            ConcurrentDictionary<uint, Item> items = User1.Identity == sender.Identity ? itemsPlayer1 : itemsPlayer2;

            Item item = sender.UserPackage.GetInventory(idItem);
            if (item == null)
            {
                await sender.SendAsync(StrRes.StrNotToTrade);
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            if (items.ContainsKey(idItem))
            {
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            if (!sender.IsPm() && !target.IsPm())
            {
                if (item.IsMonopoly() || item.IsBound)
                {
                    await sender.SendAsync(StrRes.StrNotToTrade);
                    await sender.SendAsync(RemoveMsg(idItem));
                    return false;
                }

                if (item.IsSuspicious())
                {
                    await sender.SendAsync(StrRes.StrNotToTrade);
                    await sender.SendAsync(RemoveMsg(idItem));
                    return false;
                }
            }

            if (!sender.IsGm() && !target.IsGm())
            {
                if (item.IsLocked() && sender?.TradePartnerRelation.IsValidTradePartner(target.Identity) == false)
                {
                    await sender.SendAsync(StrRes.StrNotToTrade);
                    await sender.SendAsync(RemoveMsg(idItem));
                    return false;
                }
            }

            if (item.SyndicateIdentity != 0)
            {
                await sender.SendAsync(StrRes.StrNotToTrade);
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            if (sender.Booth?.HasItem(item.Identity) == true)
            {
                await sender.SendAsync(StrRes.StrNotToTrade);
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            if (items.Count >= MaxTradeItems)
            {
                await sender.SendAsync(StrRes.StrTradeSashFull);
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            if (!target.UserPackage.IsPackSpare(items.Count + 1))
            {
                await target.SendAsync(StrRes.StrTradeYourBagIsFull);
                await sender.SendAsync(StrRes.StrTradeTargetBagIsFull);
                await sender.SendAsync(RemoveMsg(idItem));
                return false;
            }

            items.TryAdd(item.Identity, item);
            await target.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Trade));
            if (item.ItemStatus != null)
            {
                await item.ItemStatus.SendToAsync(target);
            }
            return true;
        }

        public async Task<bool> AddMoneyAsync(uint amount, Character sender)
        {
            if (sender.Identity != User1.Identity
                && sender.Identity != User2.Identity)
            {
                return false;
            }

            Character target = sender.Identity == User1.Identity ? User2 : User1;
            if (amount > MaxTradeMoney)
            {
                await sender.SendAsync(string.Format(StrRes.StrTradeMuchMoney, MaxTradeMoney));
                await SendCloseAsync();
                return false;
            }

            if (sender.Silvers < amount)
            {
                await sender.SendAsync(StrRes.StrNotEnoughMoney);
                await SendCloseAsync();
                return false;
            }

            if (sender.Identity == User1.Identity)
            {
                money1 = amount;
            }
            else
            {
                money2 = amount;
            }

            await target.SendAsync(new MsgTrade
            {
                Data = amount,
                Action = MsgTrade.TradeAction.ShowMoney
            });
            return true;
        }

        public async Task<bool> AddEmoneyAsync(uint amount, Character sender)
        {
            if (sender.Identity != User1.Identity
                && sender.Identity != User2.Identity)
            {
                return false;
            }

            Character target = sender.Identity == User1.Identity ? User2 : User1;

            if (amount > MaxTradeEmoney)
            {
                await sender.SendAsync(string.Format(StrRes.StrTradeMuchEmoney, MaxTradeEmoney));
                await SendCloseAsync();
                return false;
            }

            if (sender.ConquerPoints < amount)
            {
                await sender.SendAsync(StrRes.StrNotEnoughMoney);
                await SendCloseAsync();
                return false;
            }

            if (sender.Identity == User1.Identity)
            {
                emoney1 = amount;
            }
            else
            {
                emoney2 = amount;
            }

            await target.SendAsync(new MsgTrade
            {
                Data = amount,
                Action = MsgTrade.TradeAction.ShowConquerPoints
            });
            return true;
        }

        public async Task AcceptAsync(uint acceptId, bool skipSuspiciousCheck)
        {
            if (skipSuspiciousCheck && suspiciousWarningTarget != acceptId)
            {
                // must not allow cheaters to use the suspicious check accept
                suspiciousWarningTarget = 0;
                skipSuspiciousCheck = false;
            }

            bool isSuspicious = false;
            if (acceptId == User1.Identity)
            {
                isSuspicious = await IsSuspiciousTradeRequestAsync(User1);
                if (!skipSuspiciousCheck && !User1.TradePartnerRelation.IsValidTradePartner(User2.Identity) && isSuspicious)
                {
                    suspiciousWarningTarget = User1.Identity;
                    return;
                }

                accept1 = true;
                await User2.SendAsync(new MsgTrade
                {
                    Action = MsgTrade.TradeAction.Accept,
                    Data = acceptId
                });
            }
            else if (acceptId == User2.Identity)
            {
                isSuspicious = await IsSuspiciousTradeRequestAsync(User2);
                if (!skipSuspiciousCheck && !User2.TradePartnerRelation.IsValidTradePartner(User1.Identity) && isSuspicious)
                {
                    suspiciousWarningTarget = User2.Identity;
                    return;
                }

                accept2 = true;
                await User1.SendAsync(new MsgTrade
                {
                    Action = MsgTrade.TradeAction.Accept,
                    Data = acceptId
                });
            }

            if (!Accepted)
            {
                return;
            }

            bool success1 = itemsPlayer1.Values.All(x => User1.UserPackage.GetInventory(x.Identity) != null && !x.IsBound && !x.IsMonopoly());
            bool success2 = itemsPlayer2.Values.All(x => User2.UserPackage.GetInventory(x.Identity) != null && !x.IsBound && !x.IsMonopoly());

            bool success = success1 && success2;

            if (!User1.UserPackage.IsPackSpare(itemsPlayer2.Count))
            {
                success = false;
            }

            if (!User2.UserPackage.IsPackSpare(itemsPlayer1.Count))
            {
                success = false;
            }

            if (money1 > User1.Silvers || emoney1 > User1.ConquerPoints)
            {
                success = false;
            }

            if (money2 > User2.Silvers || emoney2 > User2.ConquerPoints)
            {
                success = false;
            }

            if (!success)
            {
                await SendCloseAsync();
                return;
            }

            var dbTrade = new DbTrade
            {
                Type = DbTrade.TradeType.Trade,
                UserIpAddress = User1.Client.IpAddress,
                UserMacAddress = User1.Client.MacAddress,
                TargetIpAddress = User2.Client.IpAddress,
                TargetMacAddress = User2.Client.MacAddress,
                MapIdentity = User1.MapIdentity,
                TargetEmoney = emoney2,
                TargetMoney = money2,
                UserEmoney = emoney1,
                UserMoney = money1,
                TargetIdentity = User2.Identity,
                UserIdentity = User1.Identity,
                TargetX = User2.X,
                TargetY = User2.Y,
                UserX = User1.X,
                UserY = User1.Y,
                IsSuspicious = isSuspicious,
                Timestamp = DateTime.Now
            };
            await ServerDbContext.CreateAsync(dbTrade);

            if (isSuspicious)
            {
                suspiciousLogger.Information("Identified suspicious trade!!! Check trade [{0}] for more information", dbTrade.Identity);
            }

            await SendCloseAsync();

            if (money1 > 0)
            {
                await User1.SpendMoneyAsync((int)money1);
                await User2.AwardMoneyAsync((int)money1);
            }

            if (money2 > 0)
            {
                await User2.SpendMoneyAsync((int)money2);
                await User1.AwardMoneyAsync((int)money2);
            }

            if (emoney1 > 0)
            {
                await User1.SpendConquerPointsAsync((int)emoney1);
                await User2.AwardConquerPointsAsync((int)emoney1);
                await User1.SaveEmoneyLogAsync(Character.EmoneyOperationType.Trade, User2.Identity, User2.ConquerPoints, emoney1);
            }

            if (emoney2 > 0)
            {
                await User2.SpendConquerPointsAsync((int)emoney2);
                await User1.AwardConquerPointsAsync((int)emoney2);
                await User2.SaveEmoneyLogAsync(Character.EmoneyOperationType.Trade, User1.Identity, User1.ConquerPoints, emoney2);
            }

            // TODO something to lock suspicious items!

            var dbItemsRecordTrack = new List<DbTradeItem>(41);
            foreach (Item item in itemsPlayer1.Values)
            {
                if (item.IsMonopoly() || item.IsBound)
                {
                    continue;
                }

                if (item.IsSuperFlag())
                {
                    await item.ClearSuperFlagAsync();
                }

                await User1.UserPackage.RemoveFromInventoryAsync(item, UserPackage.RemovalType.RemoveAndDisappear);
                await User2.UserPackage.AddItemAsync(item);

                dbItemsRecordTrack.Add(new DbTradeItem
                {
                    TradeIdentity = dbTrade.Identity,
                    SenderIdentity = User1.Identity,
                    ItemIdentity = item.Identity,
                    Itemtype = item.Type,
                    Chksum = (uint)item.ToJson().GetHashCode(),
                    JsonData = item.ToJson()
                });
            }

            foreach (Item item in itemsPlayer2.Values)
            {
                if (item.IsMonopoly() || item.IsBound)
                {
                    continue;
                }

                if (item.IsSuperFlag())
                {
                    await item.ClearSuperFlagAsync();
                }

                await User2.UserPackage.RemoveFromInventoryAsync(item, UserPackage.RemovalType.RemoveAndDisappear);
                await User1.UserPackage.AddItemAsync(item);

                dbItemsRecordTrack.Add(new DbTradeItem
                {
                    TradeIdentity = dbTrade.Identity,
                    SenderIdentity = User2.Identity,
                    ItemIdentity = item.Identity,
                    Itemtype = item.Type,
                    Chksum = (uint)item.GetHashCode(),
                    JsonData = item.ToJson()
                });
            }

            await ServerDbContext.CreateRangeAsync(dbItemsRecordTrack);

            await User1.SendAsync(StrRes.StrTradeSuccess);
            await User2.SendAsync(StrRes.StrTradeSuccess);
        }

        public async Task SendCloseAsync()
        {
            User1.Trade = null;
            User2.Trade = null;

            await User1.SendAsync(new MsgTrade
            {
                Action = MsgTrade.TradeAction.Fail,
                Data = User2.Identity
            });

            await User2.SendAsync(new MsgTrade
            {
                Action = MsgTrade.TradeAction.Fail,
                Data = User1.Identity
            });
        }

        private MsgTrade RemoveMsg(uint id)
        {
            return new MsgTrade
            {
                Action = MsgTrade.TradeAction.AddItemFail,
                Data = id
            };
        }

        private async Task<bool> IsSuspiciousTradeRequestAsync(Character sender)
        {
            var target = sender.Identity == User1.Identity ? User2 : User1;
            if (sender.IsGm() || target.IsGm())
            {
                return false;
            }

            var senderItems = sender.Identity == User1.Identity ? itemsPlayer1 : itemsPlayer2;
            var targetItems = sender.Identity == User1.Identity ? itemsPlayer2 : itemsPlayer1;

            var senderMoney = sender.Identity == User1.Identity ? money1 : money2;
            var targetMoney = sender.Identity == User1.Identity ? money2 : money1;

            var senderEmoney = sender.Identity == User1.Identity ? emoney1 : emoney2;
            var targetEmoney = sender.Identity == User1.Identity ? emoney2 : emoney1;

            int senderItemsValue = (int)(senderItems.Values.Sum(EstimateItemValue) * MinValueCoefficient);
            int targetItemsValue = (int)(targetItems.Values.Sum(EstimateItemValue) * MinValueCoefficient);

            if (senderItemsValue < 645)
            {
                return false;
            }

            if (senderItems.Count > 0 && targetItems.Count == 0)
            {
                if (senderItemsValue > targetEmoney)
                {
                    await sender.SendAsync(new MsgTrade
                    {
                        Action = MsgTrade.TradeAction.SuspiciousTradeNotify
                    });
                    return true;
                }
            }

            if (senderItemsValue / 2 > targetItemsValue)
            {
                await sender.SendAsync(new MsgTrade
                {
                    Action = MsgTrade.TradeAction.SuspiciousTradeNotify
                });
                return true;
            }

            return false;
        }

        private int EstimateItemValue(Item item)
        {
            if (item == null)
            {
                return 0;
            }

            switch (item.Type)
            {
                case Item.TYPE_METEOR:
                    return 13;
                case Item.TYPE_DRAGONBALL:
                    return 215;
                case Item.TYPE_METEOR_SCROLL:
                    return 130;
                case Item.TYPE_DRAGONBALL_SCROLL:
                    return 2150;
            }

            if (item.IsAccessory())
            {
                if (item.Itemtype.EmoneyPrice > 0)
                {
                    return (int)item.Itemtype.EmoneyPrice;
                }
                return 215;
            }

            if (item.IsGarment())
            {
                if (item.Itemtype.EmoneyPrice > 0)
                {
                    return (int)item.Itemtype.EmoneyPrice;
                }
                return 645;
            }

            int value = 0;
            if (item.IsEquipEnable())
            {
                switch (item.GetQuality())
                {
                    case 6: value += 13; break;
                    case 7: value += 27; break;
                    case 8: value += 54; break;
                    case 9: value += 215; break;                        
                }

                switch (item.Plus)
                {
                    case 1: value += 13; break;
                    case 2: value += 37; break;
                    case 3: value += 108; break;
                    case 4: value += 324; break;
                    case 5: value += 972; break;
                    case 6: value += 2916; break;
                    case 7: value += 8748; break;
                    case 8: 
                    case 9: 
                    case 10: 
                    case 11: 
                    case 12: value += 26244; break;
                }

                if (item.SocketOne != Item.SocketGem.NoSocket)
                {
                    int gemQuality = ((int)item.SocketOne % 10);
                    value += 215;
                    switch (gemQuality)
                    {
                        case 1: value += 3; break;
                        case 2: value += 54; break;
                        case 3: value += 215; break;
                    }
                }

                if (item.SocketTwo != Item.SocketGem.NoSocket)
                {
                    int gemQuality = ((int)item.SocketTwo % 10);
                    value += 645;
                    switch (gemQuality)
                    {
                        case 1: value += 3; break;
                        case 2: value += 54; break;
                        case 3: value += 215; break;
                    }
                }

                switch (item.Blessing)
                {
                    case 1: value += 645; break;
                    case 3: value += 1290; break;
                    case 5: value += 2580; break;
                    case 7: value += 5160; break;
                }
            }
            return value;
        }
    }
}
