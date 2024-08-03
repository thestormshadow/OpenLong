using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Booth;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.StrRes;

namespace Long.Module.Booth.States
{
    public sealed class Booth : BaseNpc, IBooth
    {
        private static readonly ILogger logger = Log.ForContext<Booth>();

        private Npc ownerNpc;
        private readonly Character user;
        private readonly ConcurrentDictionary<uint, BoothItem> items = new();

        public Booth(Character user) 
            : base(user.Identity % 1000000 + user.Identity / 1000000 * 100000)
        {
            this.user = user;
        }

        public override ushort Type => ROLE_BOOTH_NPC;

        public string HawkMessage { get; set; }

        public override async Task<bool> InitializeAsync()
        {
            ownerNpc = user.Screen.Roles.Values.FirstOrDefault(x => x is Npc && x.X == user.X - 2 && x.Y == user.Y) as Npc;
            if (ownerNpc == null)
            {
                return false;
            }

            idMap = user.MapIdentity;
            currentX = (ushort)(user.X + 1);
            currentY = user.Y;

            Mesh = 406;
            Name = $"{user.Name}`Shop";

            await user.SetDirectionAsync(FacingDirection.SouthEast);
            await user.SetActionAsync(EntityAction.Sit);

            return await base.InitializeAsync();
        }

        public bool AddItem(Item item, uint value, MsgItem.Moneytype type)
        {
            var boothItem = new BoothItem();
            if (!boothItem.Create(item, Math.Min(value, int.MaxValue), type == MsgItem.Moneytype.Silver))
            {
                return false;
            }

            return items.TryAdd(boothItem.Identity, boothItem);
        }

        public bool HasItem(uint idItem)
        {
            return items.ContainsKey(idItem);
        }

        public async Task QueryItemsAsync(Character requester)
        {
            if (GetDistance(requester) > Screen.VIEW_SIZE)
            {
                return;
            }

            foreach (BoothItem item in items.Values)
            {
                if (!ValidateItem(item.Identity))
                {
                    items.TryRemove(item.Identity, out _);
                    continue;
                }

                await SendItemAsync(requester, item);
            }
        }

        private Task SendItemAsync(Character target, BoothItem item)
        {
            return target.SendAsync(new MsgItemInfoEx(item.Item, item.IsSilver ? MsgItemInfoEx.ViewMode.Silvers : MsgItemInfoEx.ViewMode.Emoney) 
            { 
                TargetIdentity = Identity, 
                Position = (ushort)Item.ItemPosition.Inventory,
                Price = item.Value
            });
        }

        public bool RemoveItem(uint idItem)
        {
            return items.TryRemove(idItem, out _);
        }

        public bool ValidateItem(uint id)
        {
            Item item = user.UserPackage.GetInventory(id);
            if (item == null)
            {
                return false;
            }

            if (item.IsBound)
            {
                return false;
            }

            if (item.IsLocked())
            {
                return false;
            }

            if (item.IsSuspicious())
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SellBoothItemAsync(uint idItem, Character buyer)
        {
            if (buyer.Identity == Identity)
            {
                return false;
            }

            if (!buyer.UserPackage.IsPackSpare(1))
            {
                return false;
            }

            if (GetDistance(buyer) > Screen.VIEW_SIZE)
            {
                return false;
            }

            if (!ValidateItem(idItem))
            {
                return false;
            }

            BoothItem item = items.Values.FirstOrDefault(x => x.Identity == idItem);
            var value = (int)item.Value;
            string moneyType = item.IsSilver ? StrSilvers : StrConquerPoints;
            if (item.IsSilver)
            {
                if (!await buyer.SpendMoneyAsync((int)item.Value, true))
                {
                    return false;
                }

                await user.AwardMoneyAsync(value);
            }
            else
            {
                if (!await buyer.SpendConquerPointsAsync((int)item.Value, true))
                {
                    return false;
                }

                await user.AwardConquerPointsAsync(value);
                await buyer.SaveEmoneyLogAsync(Character.EmoneyOperationType.Booth, user.Identity, user.ConquerPoints, item.Value);
            }

            RemoveItem(idItem);

            await BroadcastRoomMsgAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.BoothRemove)
            {
                Command = Identity
            }, true);
            await user.UserPackage.RemoveFromInventoryAsync(item.Item, UserPackage.RemovalType.RemoveAndDisappear);
            await buyer.UserPackage.AddItemAsync(item.Item);

            await user.SendAsync(string.Format(StrBoothSold, buyer.Name, item.Item.Name, value, moneyType),
                            TalkChannel.Talk, Color.White);
            await buyer.SendAsync(string.Format(StrBoothBought, item.Item.Name, value, moneyType),
                                   TalkChannel.Talk, Color.White);

            DbTrade trade = new()
            {
                Type = DbTrade.TradeType.Booth,
                UserIpAddress = user.Client.IpAddress,
                UserMacAddress = user.Client.MacAddress,
                TargetIpAddress = buyer.Client.IpAddress,
                TargetMacAddress = buyer.Client.MacAddress,
                MapIdentity = MapIdentity,
                TargetEmoney = item.IsSilver ? 0 : item.Value,
                TargetMoney = item.IsSilver ? item.Value : 0,
                UserEmoney = 0,
                UserMoney = 0,
                TargetIdentity = buyer.Identity,
                UserIdentity = Identity,
                TargetX = buyer.X,
                TargetY = buyer.Y,
                UserX = X,
                UserY = Y,
                Timestamp = DateTime.Now
            };

            if (!await ServerDbContext.CreateAsync(trade))
            {
                logger.Warning($"Booth sale: {item.Item.Identity},{item.Item.PlayerIdentity},{Identity},{item.Item.Type},{item.IsSilver},{item.Value},{item.Item.ToJson()}");
                return true;
            }

            DbTradeItem tradeItem = new()
            {
                TradeIdentity = trade.Identity,
                SenderIdentity = Identity,
                ItemIdentity = item.Identity,
                Itemtype = item.Item.Type,
                Chksum = (uint)item.Item.GetHashCode(),
                JsonData = item.Item.ToJson()
            };
            await ServerDbContext.CreateAsync(tradeItem);

            await buyer.Achievements.AwardAchievementAsync(AchievementManager.AchievementType.Paythebill);
            return true;
        }

        #region Enter and Leave Map

        public override Task EnterMapAsync()
        {
            return base.EnterMapAsync();
        }

        public override async Task LeaveMapAsync()
        {
            if (ownerNpc != null)
            {
                await user.SetActionAsync(EntityAction.Stand);
                ownerNpc = null;
            }

            items.Clear();
            await base.LeaveMapAsync();
        }

        #endregion

        #region Socket

        public override async Task SendSpawnToAsync(Character player)
        {
            await player.SendAsync(new Network.MsgNpcInfoEx(this));

            if (!string.IsNullOrEmpty(HawkMessage))
            {
                await player.SendAsync(new MsgTalk(TalkChannel.Vendor, Color.White, HawkMessage) { CurrentTime = user.Identity });
            }
        }        

        #endregion

        public class BoothItem
        {
            public Item Item { get; private set; }
            public uint Identity => Item?.Identity ?? 0;
            public uint Value { get; private set; }
            public bool IsSilver { get; private set; }

            public bool Create(Item item, uint dwMoney, bool bSilver)
            {
                Item = item;
                Value = dwMoney;
                IsSilver = bSilver;

                return Value > 0;
            }
        }
    }
}
