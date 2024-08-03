using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Managers;
using static Long.Kernel.Network.Game.Packets.MsgName;
using System.Drawing;
using System.Runtime.InteropServices;
using Long.Kernel.Database;
using static Long.Kernel.Network.Game.Packets.MsgMapItem;

namespace Long.Kernel.States.Items
{
    public sealed class MapItem : Role
    {
        /// <summary>
        /// Timer to keep the object alive in the map.
        /// </summary>
        private TimeOut keepAlive = new();
        /// <summary>
        /// Time to lock object so non-teammates cannot pick it up.
        /// </summary>
        private TimeOut protectionTimer = new();

        private MapItemInfo info = default;
        private Item itemInfo = null;
        private DbItemtype itemtype = null;

        private uint idOwner = 0;
        private uint moneyAmount = 0;

        public MapItem(uint idRole)
        {
            Identity = idRole;
        }

        public MapItem(uint idRole, DbItemDrop itemDrop)
        {
            Identity = idRole;
            ItemDrop = itemDrop;
        }

        ~MapItem()
        {
            IdentityManager.MapItem.ReturnIdentity(Identity);
        }

        public DbItemDrop ItemDrop { get; init; }
		public DropMode Mode { get; private set; } = DropMode.Common;

		#region Creation

		public bool Create(GameMap map, Point pos, uint idType, uint idOwner, byte nPlus, byte nDmg, ushort nDura, DropMode mode = MapItem.DropMode.Common)
        {
            itemtype = ItemManager.GetItemtype(idType);
            return itemtype != null && Create(map, pos, itemtype, idOwner, nPlus, nDmg, nDura, mode);
        }

        public bool Create(GameMap map, Point pos, DbItemtype idType, uint idOwner, byte nPlus,
            byte nDmg,
            ushort nDura, DropMode mode = MapItem.DropMode.Common)
        {
            if (map == null || idType == null)
            {
                return false;
            }

            keepAlive = new TimeOut(_DISAPPEAR_TIME);
            keepAlive.Startup(_DISAPPEAR_TIME);

            idMap = map.Identity;
            Map = map;
            X = (ushort)pos.X;
            Y = (ushort)pos.Y;

            info.Addition = nPlus;
            info.ReduceDamage = nDmg;
            info.MaximumDurability = nDura;
            info.Color = Item.ItemColor.Orange;

            itemtype = idType;
            info.Type = itemtype.Type;

            Name = itemtype.Name;

            if (idOwner != 0)
            {
                this.idOwner = idOwner;
                protectionTimer = new TimeOut(_MAPITEM_PRIV_SECS);
                protectionTimer.Startup(_MAPITEM_PRIV_SECS);
                protectionTimer.Update();
            }

			Mode = mode;
			return true;
        }

        public bool Create(GameMap map, Point pos, MapItemInfo info, uint idOwner, DropMode mode = MapItem.DropMode.Common)
		{
            if (map == null || info.Equals(default) || info.Type == 0)
            {
                return false;
            }

            itemtype = ItemManager.GetItemtype(info.Type);
            if (itemtype == null)
            {
                return false;
            }

            keepAlive = new TimeOut(_DISAPPEAR_TIME);
            keepAlive.Startup(_DISAPPEAR_TIME);

            idMap = map.Identity;
            Map = map;
            X = (ushort)pos.X;
            Y = (ushort)pos.Y;

            this.info = info;
            Name = itemtype.Name;

            if (idOwner != 0)
            {
                this.idOwner = idOwner;
                OwnerIdentity = idOwner;
                protectionTimer = new TimeOut(_MAPITEM_PRIV_SECS);
                protectionTimer.Startup(_MAPITEM_PRIV_SECS);
                protectionTimer.Update();
            }

			Mode = mode;
			return true;
        }

        public bool Create(GameMap map, Point pos, Item pInfo, uint idOwner, DropMode mode = MapItem.DropMode.Common)
        {
            if (map == null || pInfo == null)
            {
                return false;
            }

            int nAliveSecs = _MAPITEM_USERMAX_ALIVESECS;
            if (pInfo.Itemtype != null)
            {
                nAliveSecs = (int)(pInfo.Itemtype.Price / _MAPITEM_ALIVESECS_PERPRICE + _MAPITEM_USERMIN_ALIVESECS);
            }

            if (nAliveSecs > _MAPITEM_USERMAX_ALIVESECS)
            {
                nAliveSecs = _MAPITEM_USERMAX_ALIVESECS;
            }

            keepAlive = new TimeOut(nAliveSecs);
            keepAlive.Update();

            this.idOwner = idOwner;
            idMap = map.Identity;
            Map = map;
            X = (ushort)pos.X;
            Y = (ushort)pos.Y;

            Name = pInfo.Itemtype?.Name ?? "";

            itemInfo = pInfo;
            info.Type = pInfo.Type;
            info.Color = pInfo.Color;
            itemInfo.OwnerIdentity = 0;
            itemInfo.Position = Item.ItemPosition.Floor;

			Mode = mode;
			return true;
        }

        public async Task<bool> CreateAsync(GameMap map, Point pos, Item pInfo, uint idOwner)
        {
            if (map == null || pInfo == null)
            {
                return false;
            }

            int nAliveSecs = _MAPITEM_USERMAX_ALIVESECS;
            if (pInfo.Itemtype != null)
            {
                nAliveSecs = (int)(pInfo.Itemtype.Price / _MAPITEM_ALIVESECS_PERPRICE + _MAPITEM_USERMIN_ALIVESECS);
            }

            if (nAliveSecs > _MAPITEM_USERMAX_ALIVESECS)
            {
                nAliveSecs = _MAPITEM_USERMAX_ALIVESECS;
            }

            keepAlive = new TimeOut(nAliveSecs);
            keepAlive.Update();

            this.idOwner = idOwner;
            idMap = map.Identity;
            Map = map;
            X = (ushort)pos.X;
            Y = (ushort)pos.Y;

            Name = pInfo.Itemtype?.Name ?? "";

            itemInfo = pInfo;
            info.Type = pInfo.Type;
            info.Color = pInfo.Color;
            itemInfo.OwnerIdentity = 0;
            itemInfo.PlayerIdentity = 0;
            itemInfo.Position = Item.ItemPosition.Floor;
            return true;
        }

        public bool CreateMoney(GameMap map, Point pos, uint dwMoney, uint idOwner, DropMode mode = MapItem.DropMode.Common)
		{
            if (map == null || Identity == 0)
            {
                return false;
            }

            int nAliveSecs = _MAPITEM_MONSTER_ALIVESECS;
            if (idOwner == 0)
            {
                nAliveSecs = (int)(dwMoney / _MAPITEM_ALIVESECS_PERPRICE + _MAPITEM_USERMIN_ALIVESECS);
                if (nAliveSecs > _MAPITEM_USERMAX_ALIVESECS)
                {
                    nAliveSecs = _MAPITEM_USERMAX_ALIVESECS;
                }
            }

            keepAlive = new TimeOut(nAliveSecs);
            keepAlive.Update();

            idMap = map.Identity;
            Map = map;
            X = (ushort)pos.X;
            Y = (ushort)pos.Y;

            uint idType;
            if (dwMoney < _ITEM_SILVER_MAX)
            {
                idType = 1090000;
            }
            else if (dwMoney < _ITEM_SYCEE_MAX)
            {
                idType = 1090010;
            }
            else if (dwMoney < _ITEM_GOLD_MAX)
            {
                idType = 1090020;
            }
            else if (dwMoney < _ITEM_GOLDBULLION_MAX)
            {
                idType = 1091000;
            }
            else if (dwMoney < _ITEM_GOLDBAR_MAX)
            {
                idType = 1091010;
            }
            else
            {
                idType = 1091020;
            }

            moneyAmount = dwMoney;

            info.Type = idType;

            if (idOwner != 0)
            {
                this.idOwner = idOwner;
                protectionTimer = new TimeOut(_MAPITEM_PRIV_SECS);
                protectionTimer.Startup(_MAPITEM_PRIV_SECS);
                protectionTimer.Update();
            }

			Mode = mode;
			return true;
        }

        #endregion

        #region Identity

        public uint ItemIdentity => itemInfo?.Identity ?? 0;

        public uint Itemtype
        {
            get => info.Type;
            private set => info.Type = value;
        }

        public override uint OwnerIdentity => idOwner;

        public uint Money => moneyAmount;

        public bool IsPrivate()
        {
            return protectionTimer.IsActive() && !protectionTimer.IsTimeOut();
        }

        public bool IsMoney()
        {
            return moneyAmount > 0;
        }

        public bool IsJewel()
        {
            return false;
        }

        public bool IsItem()
        {
            return !IsMoney() && !IsJewel();
        }

        public MapItemInfo Info => info;

        #endregion

        #region Generation

        public Item GetInfo()
        {
            return itemInfo;
        }

        public async Task<Item> GetInfoAsync(Character owner)
        {
            if (itemtype == null && itemInfo == null)
            {
                return null;
            }

            if (itemInfo == null)
            {
                itemInfo = new Item(owner);

                await itemInfo.CreateAsync(itemtype);

                itemInfo.AccumulateNum = 1;
                itemInfo.Color = info.Color;

                itemInfo.ChangeAddition(info.Addition);
                itemInfo.ReduceDamage = info.ReduceDamage;

                if (info.SocketNum > 0)
                {
                    itemInfo.SocketOne = Item.SocketGem.EmptySocket;
                }

                if (info.SocketNum > 1)
                {
                    itemInfo.SocketTwo = Item.SocketGem.EmptySocket;
                }

                if (itemtype?.AmountLimit > 100)
                {
                    itemInfo.Durability = (ushort)await NextAsync(10, itemtype.AmountLimit / 4);
                }
                else if (itemtype != null)
                {
                    itemInfo.Durability = itemtype.AmountLimit;
                }
            }

            itemInfo.Position = Item.ItemPosition.Inventory;
            await itemInfo.SaveAsync();
            return itemInfo;
        }

        #endregion

        #region Map

        public void SetAliveTimeout(int durationSecs)
        {
            keepAlive.Startup(durationSecs);
        }

        public bool CanDisappear()
        {
            return keepAlive.IsTimeOut();
        }

        public async Task DisappearAsync()
        {
            if (itemInfo != null)
            {
                await itemInfo.DeleteAsync();
            }

            QueueAction(LeaveMapAsync); // This runs in a separate thread. Must be queued
        }

        public override async Task EnterMapAsync()
        {
            Map = MapManager.GetMap(MapIdentity);
            if (Map != null)
            {
                await Map.AddAsync(this);
            }
        }

        public override async Task LeaveMapAsync()
        {
            IdentityManager.MapItem.ReturnIdentity(Identity);
            if (Map != null)
            {
                var msg = new MsgMapItem
                {
                    Identity = Identity,
                    MapX = X,
                    MapY = Y,
                    Itemtype = Itemtype,
                    Mode = DropType.DisappearItem
                };
                await Map.BroadcastRoomMsgAsync(X, Y, msg);
                await Map.RemoveAsync(Identity);
                RoleManager.RemoveRole(Identity);
            }
            Map = null;
        }

        #endregion

        public override Task OnTimerAsync()
        {
            if (CanDisappear())
                QueueAction(DisappearAsync);
            return Task.CompletedTask;
        }

        #region Socket

        public override async Task SendSpawnToAsync(Character player)
        {
            await player.SendAsync(new MsgMapItem
            {
                Identity = Identity,
                MapX = X,
                MapY = Y,
                Itemtype = Itemtype,
                Mode = DropType.LayItem,
                Color = (ushort)info.Color,
                Composition = info.Addition,
                OwnerIdentity = (OwnerIdentity != 0 ? OwnerIdentity : player.Identity)
            });

            if (itemInfo == null)
            {
                string effect = "";
                if (Itemtype == Item.TYPE_DRAGONBALL)
                {
                    effect = "darcue";
                }

                if (!string.IsNullOrEmpty(effect))
                {
                    await player.SendAsync(new MsgName
                    {
                        Action = StringAction.MapEffect,
                        X = X,
                        Y = Y,
                        Strings = new List<string>
                            {
                                effect
                            }
                    });
                }
            }
        }

        #endregion

        #region Constants

        private const uint _ITEM_SILVER_MIN = 1;
        private const uint _ITEM_SILVER_MAX = 9;
        private const uint _ITEM_SYCEE_MIN = 10;
        private const uint _ITEM_SYCEE_MAX = 99;
        private const uint _ITEM_GOLD_MIN = 100;
        private const uint _ITEM_GOLD_MAX = 999;
        private const uint _ITEM_GOLDBULLION_MIN = 1000;
        private const uint _ITEM_GOLDBULLION_MAX = 1999;
        private const uint _ITEM_GOLDBAR_MIN = 2000;
        private const uint _ITEM_GOLDBAR_MAX = 4999;
        private const uint _ITEM_GOLDBARS_MIN = 5000;
        private const uint _ITEM_GOLDBARS_MAX = 10000000;

        private const int _PICKUP_TIME = 30;
        private const int _DISAPPEAR_TIME = 60;
        private const int _MAPITEM_ONTIMER_SECS = 5;
        private const int _MAPITEM_MONSTER_ALIVESECS = 60;
        private const int _MAPITEM_USERMAX_ALIVESECS = 90;
        private const int _MAPITEM_USERMIN_ALIVESECS = 60;

        private const int _MAPITEM_ALIVESECS_PERPRICE = 1000 / (_MAPITEM_USERMAX_ALIVESECS - _MAPITEM_USERMIN_ALIVESECS);

        private const int _MAPITEM_PRIV_SECS = 30;
        private const int _PICKMAPITEMDIST_LIMIT = 0;

        #endregion

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 2, Size = 12)]
        public struct MapItemInfo
        {
            public uint Type { get; set; }
            public ushort MaximumDurability { get; set; }
            public ushort Durability { get; set; }
            public byte ReduceDamage { get; set; }
            public byte Addition { get; set; }
            public byte SocketNum { get; set; }
            public Item.ItemColor Color { get; set; }
        }

		[Flags]
		public enum DropMode
		{
			Common,
			Bound,
			OnlyOwner
		}
	}
}

