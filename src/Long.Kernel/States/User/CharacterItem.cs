using Long.Database.Entities;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Shared.Managers;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using System.Drawing;
using Long.Kernel.Database;
using Long.Shared.Helpers;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Trade;
using Long.Kernel.Modules.Systems.Booth;
using Long.Kernel.States.Storage;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Long.Kernel.Database.Repositories;

namespace Long.Kernel.States.User
{
    public partial class Character
    {
        public const int SASH_SPACE_PRICE = 90;
        public const int MAXIMUM_SASH_SLOTS = 300;

        public UserPackage UserPackage { get; }

        public Item GetEquipment(Item.ItemPosition itemPosition) => UserPackage?.GetEquipment(itemPosition);

        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Headgear => GetEquipment(Item.ItemPosition.Headwear);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Necklace => GetEquipment(Item.ItemPosition.Necklace);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Ring => GetEquipment(Item.ItemPosition.Ring);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item RightHand => GetEquipment(Item.ItemPosition.RightHand);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item LeftHand => GetEquipment(Item.ItemPosition.LeftHand);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Armor => GetEquipment(Item.ItemPosition.Armor);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Boots => GetEquipment(Item.ItemPosition.Boots);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Garment => GetEquipment(Item.ItemPosition.Garment);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Mount => GetEquipment(Item.ItemPosition.Mount);
        /// <summary>
        /// The specified EQUIPED item. Takes in consideration secondary equipment.
        /// </summary>
        public Item Wings => GetEquipment(Item.ItemPosition.Wing);

        #region Map Item

        private static readonly ILogger dropMoneyLogger =   Logger.CreateLogger("money_drop");
        private static readonly ILogger pickupMoneyLogger = Logger.CreateLogger("money_pickup");
        private static readonly ILogger dropItemLogger =    Logger.CreateLogger("item_drop");
        private static readonly ILogger pickupItemLogger =  Logger.CreateLogger("item_pickup");

        public async Task<bool> DropItemAsync(uint idItem, int x, int y)
        {
            var pos = new Point(x, y);
            if (!Map.FindDropItemCell(9, ref pos))
            {
                return false;
            }

            Item item = UserPackage.FindItemByIdentity(idItem);
            if (item == null)
            {
                return false;
            }

            //if (Booth?.QueryItem(idItem) != null)
            //{
            //    return false;
            //}

            //if (Trade != null)
            //{
            //    if (Trade.ContainsItem(idItem))
            //    {
            //        return false;
            //    }
            //}

            if (item.IsSuspicious())
            {
                return false;
            }

            if (item.IsDisappearWhenDropped())
            {
                return await UserPackage.RemoveFromInventoryAsync(item, UserPackage.RemovalType.Delete);
            }
            if (item.CanBeDropped())
            {
                await UserPackage.RemoveFromInventoryAsync(item, UserPackage.RemovalType.RemoveAndDisappear);
            }
            else
            {
                await SendAsync(string.Format(StrItemCannotDiscard, item.Name));
                return false;
            }

            dropItemLogger.Information($"{Name}({Identity}) drop item:[id={item.Identity}, type={item.Type}], dur={item.Durability}, max_dur={item.OriginalMaximumDurability}");

            var dropItemLog = new DbItemDrop
            {
                UserId = Identity,
                ItemId = item.Identity,
                ItemType = item.Type,
                MapId = MapIdentity,
                X = X,
                Y = Y,
                Addition = item.Plus,
                Gem1 = (byte)item.SocketOne,
                Gem2 = (byte)item.SocketTwo,
                ReduceDmg = item.ReduceDamage,
                AddLife = item.Enchantment,
                Data = item.Data,
                DropTime = UnixTimestamp.Now
            };
            await ServerDbContext.CreateAsync(dropItemLog);

            var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity, dropItemLog);
            if (await mapItem.CreateAsync(Map, pos, item, Identity))
            {
                item.Position = Item.ItemPosition.Floor;
                await mapItem.EnterMapAsync();
                await item.SaveAsync();
            }
            else
            {
                IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                if (IsGm())
                {
                    await SendAsync("The MapItem object could not be created. Check Output log");
                }

                return false;
            }

            return true;
        }

        public async Task<bool> DropSilverAsync(uint amount)
        {
            if (amount > 10_000_000)
            {
                return false;
            }

            //if (Trade != null)
            //{
            //    return false;
            //}

            var pos = new Point(X, Y);
            if (!Map.FindDropItemCell(1, ref pos))
            {
                return false;
            }

            if (!await SpendMoneyAsync((int)amount, true))
            {
                return false;
            }

            dropMoneyLogger.Information($"{Identity},{Name},{amount}");

            var dropItemLog = new DbItemDrop
            {
                UserId = Identity,
                ItemId = 0,
                ItemType = amount,
                MapId = MapIdentity,
                X = X,
                Y = Y,
                Addition = 0,
                Gem1 = 0,
                Gem2 = 0,
                ReduceDmg = 0,
                AddLife = 0,
                Data = 0,
                DropTime = UnixTimestamp.Now
            };
            await ServerDbContext.CreateAsync(dropItemLog);

            var mapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity, dropItemLog);
            if (mapItem.CreateMoney(Map, pos, amount, 0u, MapItem.DropMode.Common))
            {
                await mapItem.EnterMapAsync();
            }
            else
            {
                IdentityManager.MapItem.ReturnIdentity(mapItem.Identity);
                if (IsGm())
                {
                    await SendAsync("The DropSilver MapItem object could not be created. Check Output log");
                }

                return false;
            }

            return true;
        }

        public async Task<bool> PickMapItemAsync(uint idItem, bool priorizeSash = false)
        {
            var mapItem = Map.QueryAroundRole(this, idItem) as MapItem;
            if (mapItem == null)
            {
                return false;
            }

            if (GetDistance(mapItem) > 0)
            {
                await SendAsync(StrTargetNotInRange);
                return false;
            }

			if ((mapItem.Mode.HasFlag(MapItem.DropMode.OnlyOwner) || mapItem.Mode.HasFlag(MapItem.DropMode.Bound))
				&& mapItem.OwnerIdentity != Identity)
			{
				await SendAsync(StrCannotPickupOtherItems);
				return false;
			}

			if (mapItem.OwnerIdentity != Identity && mapItem.IsPrivate())
            {
                Character owner = RoleManager.GetUser(mapItem.OwnerIdentity);
                if (owner != null && !IsMate(owner))
                {
                    if (Team == null || !Team.IsMember(mapItem.OwnerIdentity) ||
                        mapItem.IsMoney() && !Team.MoneyEnable || mapItem.IsJewel() && !Team.JewelEnable ||
                        mapItem.IsItem() && !Team.ItemEnable)
                    {
                        await SendAsync(StrCannotPickupOtherItems);
                        return false;
                    }
                }
            }

            if (mapItem.IsMoney())
            {
                await AwardMoneyAsync((int)mapItem.Money);
                if (mapItem.Money > 1000)
                {
                    await SendAsync(new MsgAction
                    {
                        Identity = Identity,
                        Command = mapItem.Money,
                        ArgumentX = X,
                        ArgumentY = Y,
                        Action = ActionType.MapGold
                    });
                }
                await SendAsync(string.Format(StrPickupSilvers, mapItem.Money));

                pickupMoneyLogger.Information($"{Identity},{Name},{mapItem.Money},{MapIdentity},{Map.Name},{X},{Y}");
            }
            else
            {
                Item checkItem = mapItem.GetInfo();
                int itemCount = (int)(checkItem?.AccumulateNum ?? 1);
                if (priorizeSash)
                {
                    var storageSize = UserPackage.StorageSize(Identity, MsgPackage.StorageType.ChestPackage);
                    if (storageSize >= SashSlots)
                    {
                        priorizeSash = false;
                    }
                }

                if (!priorizeSash && !UserPackage.IsPackSpare(itemCount, mapItem.Itemtype))
                {
                    await SendAsync(StrYourBagIsFull);
                    return false;
                }

                Item item = await mapItem.GetInfoAsync(this);
                if (item.IsBound && item.OwnerIdentity != Identity)
                {
                    logger.Warning("Attempt to get bound item from the floor!");
                    return false;
                }

                if (item != null)
                {
                    if (item.IsSuperFlag())
                    {
                        await item.ClearSuperFlagAsync();
                    }

                    if (!priorizeSash)
                    {
                        await UserPackage.AddItemAsync(item);
                        await SendAsync(string.Format(StrPickupItem, item.Name));

                        if (VipLevel > 0 && UserPackage.MultiCheckItem(Item.TYPE_METEOR, Item.TYPE_METEOR, 10, true))
                        {
                            await UserPackage.MultiSpendItemAsync(Item.TYPE_METEOR, Item.TYPE_METEOR, 10, true);
                            await UserPackage.AwardItemAsync(Item.TYPE_METEOR_SCROLL);
                        }

                        if (VipLevel > 0 && UserPackage.MultiCheckItem(Item.TYPE_DRAGONBALL, Item.TYPE_DRAGONBALL, 10, true))
                        {
                            await UserPackage.MultiSpendItemAsync(Item.TYPE_DRAGONBALL, Item.TYPE_DRAGONBALL, 10, true);
                            await UserPackage.AwardItemAsync(Item.TYPE_DRAGONBALL_SCROLL);
                        }
                    }
                    else
                    {
                        await UserPackage.AddItemAsync(item);
                        await UserPackage.AddToStorageAsync(Identity, item, MsgPackage.StorageType.ChestPackage, true);
                        await SendAsync(string.Format(StrPickupItem, item.Name));
                    }

                    pickupItemLogger.Information($"{Identity},{Name},{mapItem.ItemIdentity},{mapItem.Itemtype},{MapIdentity},{Map.Name},{X},{Y}");
                }
            }

            var itemPickUpLog = new DbItemPickUp
            {
                PickUpTime = UnixTimestamp.Now,
                UserId = Identity,
                ItemId = mapItem.IsMoney() ? 0 : mapItem.ItemIdentity,
                ItemType = mapItem.IsMoney() ? mapItem.Money : mapItem.Itemtype
            };
            if (mapItem.ItemDrop != null)
            {
                itemPickUpLog.DropId = mapItem.ItemDrop.Id;
            }
            await ServerDbContext.CreateAsync(itemPickUpLog);

            await mapItem.LeaveMapAsync();
            return true;
        }

        #endregion

        #region Sash

        public uint SashSlots
        {
            get => user.ChestPackageSize;
            set => user.ChestPackageSize = value;
        }

        public async Task SetSashSlotAmountAsync(int count)
        {
            SashSlots = (uint)Math.Min(MAXIMUM_SASH_SLOTS, count);
            await SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, SashSlots);
            await SaveAsync();
        }

        public async Task AddSashSpaceAsync(int count)
        {
            if (SashSlots >= MAXIMUM_SASH_SLOTS)
            {
                await SendAsync(StrQkdMax, TalkChannel.TopLeft, Color.White);
                return;
            }

            if (SashSlots + count > MAXIMUM_SASH_SLOTS)
            {
                await SendAsync(StrQkdExceedMax, TalkChannel.TopLeft, Color.White);
                return;
            }

            int price = count * SASH_SPACE_PRICE;
            if (!await SpendBoundConquerPointsAsync(EmoneyOperationType.ChestPackage, price, true))
            {
                return;
            }

            SashSlots = (uint)Math.Min(MAXIMUM_SASH_SLOTS, Math.Max(0, SashSlots + count));
            await SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, SashSlots);
            await SaveAsync();
        }

        public async Task OpenSashSlotsAsync(Item sash)
        {
            int amount = (int)(sash.Type % 10);
            if (amount == 9)
            {
                amount += 3;
            }

            if (UserPackage.StorageSize(sash.Identity, MsgPackage.StorageType.Chest) > 0)
            {
                await SendAsync(StrQkdNeedEmpty, TalkChannel.TopLeft, Color.White);
                return;
            }

            if (SashSlots >= MAXIMUM_SASH_SLOTS)
            {
                await SendAsync(StrQkdMax, TalkChannel.TopLeft, Color.White);
                return;
            }

            SashSlots = (uint)Math.Min(MAXIMUM_SASH_SLOTS, Math.Max(0, SashSlots + amount));
            await SynchroAttributesAsync(ClientUpdateType.CurrentSashSlots, SashSlots);
            await UserPackage.SpendItemAsync(sash);
            await SaveAsync();
        }

        #endregion

        #region Secondary Equipment

        private readonly TimeOut secondaryEquipmentToggleTimer = new(1);

        public async Task<bool> ToggleSecondaryEquipmentAsync()
        {
            if (!secondaryEquipmentToggleTimer.IsTimeOut())
            {
                return false;
            }

            if (QueryStatus(Status.StatusSet.PATH_OF_SHADOW) != null)
            {
                return false;
            }

            if (QueryStatus(Status.StatusSet.FLY) != null)
            {
                return false;
            }

            if (!UserPackage.IsSecondaryEquipmentUser && !UserPackage.HasSecondaryEquipment)
            {
                return false;
            }

            UserPackage.IsSecondaryEquipmentUser = !UserPackage.IsSecondaryEquipmentUser;
            secondaryEquipmentToggleTimer.Update();

            await UserPackage.SyncEquipmentAsync();
            await SendAsync(new MsgPlayerAttribInfo(this));
            await BroadcastRoomMsgAsync(new MsgPlayer(this), false);
            return true;
        }

        #endregion

        #region Trade

        public ITrade Trade { get; set; }
        public IBooth Booth { get; set; }

        #endregion

        #region Wardrobe

        private uint osWingId;
        private uint osTitleId;
        private uint osTitleScore;

        public CoatStorage CoatStorage { get; }
        public TitleStorage TitleStorage { get; }

        public uint WingId
        {
            get
            {
                if (IsOSUser())
                {
                    return osWingId;
                }
                return TitleStorage?.WingId ?? 0;
            }
            set
            {
                if (IsOSUser())
                {
                    osWingId = value;
                }
            }
        }

        public uint TitleId
        {
            get
            {
                if (IsOSUser())
                {
                    return osTitleId;
                }
                return TitleStorage?.TitleId ?? 0;
            }
            set
            {
                if (IsOSUser())
                {
                    osTitleId = value;
                }
            }
        }

        public uint TitleScore
        {
            get
            {
                if (IsOSUser())
                {
                    return osTitleScore;
                }
                return TitleStorage?.Score ?? 0;
            }
            set
            {
                if (IsOSUser())
                {
                    osTitleScore = value;
                }
            }
        }

		#endregion

		#region Battle

		private static readonly ILogger spendEquipItemLogger = Logger.CreateConsoleLogger("spend_item");

		public async Task<bool> SpendEquipItemAsync(uint dwItem, uint dwAmount, bool bSynchro)
		{
			if (dwItem <= 0)
			{
				return false;
			}

			Item item = null;
			if (UserPackage[Item.ItemPosition.RightHand]?.GetItemSubType() == dwItem &&
				UserPackage[Item.ItemPosition.RightHand]?.Durability >= dwAmount)
			{
				item = UserPackage[Item.ItemPosition.RightHand];
			}
			else if (UserPackage[Item.ItemPosition.LeftHand]?.GetItemSubType() == dwItem)
			{
				item = UserPackage[Item.ItemPosition.LeftHand];
			}

			if (item == null)
			{
				return false;
			}

			if (!item.IsExpend() && item.Durability < dwAmount && !item.IsArrowSort())
			{
				return false;
			}

			if (item.IsExpend())
			{
				item.Durability = (ushort)Math.Max(0, item.Durability - (int)dwAmount);
				if (bSynchro)
				{
					await SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
				}
			}

			if (item.IsArrowSort() && item.Durability == 0)
			{
				Item.ItemPosition pos = item.Position;
				await UserPackage.UnEquipAsync(item.Position, UserPackage.RemovalType.Delete);
				Item other = UserPackage.GetItemByType(item.Type);
				if (other != null)
				{
					await UserPackage.EquipItemAsync(other, pos);
				}
			}

			if (item.Durability > 0)
			{
				await item.SaveAsync();
			}

			return true;
		}

		public async Task<bool> DecEquipmentDurabilityAsync(bool beingAttacked, int hitByMagic, ushort useItemNum)
		{
			if (VipLevel > 0 && Flag.HasFlag(PrivilegeFlag.FirstCreditClaimed))
			{
				return true;
			}

			int nInc = -1 * useItemNum;
			for (var i = Item.ItemPosition.Headwear; i <= Item.ItemPosition.Crop; i++)
			{
				if (i == Item.ItemPosition.Garment || i == Item.ItemPosition.Gourd || i == Item.ItemPosition.Mount
					|| i == Item.ItemPosition.MountArmor || i == Item.ItemPosition.LeftHandAccessory ||
					i == Item.ItemPosition.RightHandAccessory)
				{
					continue;
				}

				if (hitByMagic == 1)
				{
					if (i == Item.ItemPosition.Ring
						|| i == Item.ItemPosition.RightHand
						|| i == Item.ItemPosition.LeftHand)
					{
						if (!beingAttacked)
						{
							await AddEquipmentDurabilityAsync(i, nInc);
						}
					}
					else
					{
						if (beingAttacked)
						{
							await AddEquipmentDurabilityAsync(i, nInc);
						}
					}
				}
				else
				{
					if (i == Item.ItemPosition.Ring
						|| i == Item.ItemPosition.RightHand
						|| i == Item.ItemPosition.LeftHand)
					{
						if (!beingAttacked)
						{
							await AddEquipmentDurabilityAsync(i, -1);
						}
					}
					else
					{
						if (beingAttacked)
						{
							await AddEquipmentDurabilityAsync(i, nInc);
						}
					}
				}
			}

			return true;
		}

		public async Task AddEquipmentDurabilityAsync(Item.ItemPosition pos, int nInc)
		{
			if (nInc >= 0)
			{
				return;
			}

			Item item = UserPackage[pos];
			if (item == null
				|| !item.IsEquipment()
				|| item.GetItemSubType() == 2100)
			{
				return;
			}

			ushort oldDurability = item.Durability;
			var newDurability = (ushort)Math.Max(0, item.Durability + nInc);

			if (newDurability < 100)
			{
				if (newDurability % 10 == 0)
				{
					await SendAsync(string.Format(StrDamagedRepair, item.Itemtype.Name));
				}
			}
			else if (newDurability < 200)
			{
				if (newDurability % 10 == 0)
				{
					await SendAsync(string.Format(StrDurabilityRepair, item.Itemtype.Name));
				}
			}

			item.Durability = newDurability;
			await item.SaveAsync();

			var noldDur = (int)Math.Floor(oldDurability / 100f);
			var nnewDur = (int)Math.Floor(newDurability / 100f);

			if (newDurability <= 0)
			{
				await SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
			}
			else if (noldDur != nnewDur)
			{
				await SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
			}
		}

		public bool CheckWeaponSubType(uint idItem, uint dwNum = 0)
		{
			var items = new List<uint>();
			string stringIdItem = idItem.ToString();
			if (stringIdItem.Length == 8)
			{
				int mode = int.Parse(stringIdItem.Substring(0, 2));
				int weaponType1 = int.Parse(stringIdItem.Substring(2, 3));
				int rightHandSubtype = UserPackage[Item.ItemPosition.RightHand]?.GetItemSubType() ?? 0;
				int weaponType2 = int.Parse(stringIdItem.Substring(5, 3));
				int leftHandSubtype = UserPackage[Item.ItemPosition.RightHand]?.GetItemSubType() ?? 0;

				if (mode == 61)
				{
					return weaponType1 == rightHandSubtype && weaponType2 == leftHandSubtype;
				}

				// 61 both weapons
				// 60 any weapon
				return weaponType1 == rightHandSubtype || weaponType2 == rightHandSubtype || weaponType1 == leftHandSubtype || weaponType2 == leftHandSubtype;
			}

			for (var i = 0; i < stringIdItem.Length / 3; i++)
			{
				if (idItem > 999 && idItem != 40000 && idItem != 50000)
				{
					int idx = i * 3; // + (i > 0 ? -1 : 0);
					items.Add(uint.Parse(idItem.ToString().Substring(idx, 3)));
				}
				else
				{
					items.Add(uint.Parse(idItem.ToString()));
				}
			}

			if (items.Count <= 0)
			{
				return false;
			}

			foreach (uint dwItem in items)
			{
				if (dwItem <= 0)
				{
					continue;
				}

				if (UserPackage[Item.ItemPosition.RightHand] != null &&
					UserPackage[Item.ItemPosition.RightHand].GetItemSubType() == dwItem &&
					UserPackage[Item.ItemPosition.RightHand].Durability >= dwNum)
				{
					return true;
				}

				if (UserPackage[Item.ItemPosition.LeftHand] != null &&
					UserPackage[Item.ItemPosition.LeftHand].GetItemSubType() == dwItem &&
					UserPackage[Item.ItemPosition.LeftHand].Durability >= dwNum)
				{
					return true;
				}

				ushort[] set1Hand = { 410, 420, 421, 430, 440, 450, 460, 480, 481, 490 };
				ushort[] set2Hand = { 510, 511, 530, 540, 560, 561, 580 };
				ushort[] setSword = { 420, 421 };
				ushort[] setProfessional = { 601, 610, 611, 612, 613, 614, 616, 617, 619, 620 };

				if (dwItem == 40000 || dwItem == 400)
				{
					if (UserPackage[Item.ItemPosition.RightHand] != null)
					{
						Item item = UserPackage[Item.ItemPosition.RightHand];
						if (item != null)
						{
							for (var i = 0; i < set1Hand.Length; i++)
							{
								bool subTypeMatch = item.GetItemSubType() == set1Hand[i] || item.GetItemSubType() == 614;
								if (subTypeMatch && item.Durability >= dwNum)
								{
									return true;
								}
							}
						}
					}

					if (UserPackage[Item.ItemPosition.LeftHand] != null)
					{
						Item item = UserPackage[Item.ItemPosition.LeftHand];
						if (item != null)
						{
							for (var i = 0; i < set1Hand.Length; i++)
							{
								bool subTypeMatch = item.GetItemSubType() == set1Hand[i] || item.GetItemSubType() == 614;
								if (subTypeMatch && item.Durability >= dwNum)
								{
									return true;
								}
							}
						}
					}
				}

				if (dwItem == 50000)
				{
					if (UserPackage[Item.ItemPosition.RightHand] != null)
					{
						if (dwItem == 50000)
						{
							return true;
						}

						Item item = UserPackage[Item.ItemPosition.RightHand];
						for (var i = 0; i < set2Hand.Length; i++)
						{
							if (item.GetItemSubType() == set2Hand[i] && item.Durability >= dwNum)
							{
								return true;
							}
						}
					}
				}

				if (dwItem == 50) // arrow
				{
					if (UserPackage[Item.ItemPosition.RightHand] != null &&
						UserPackage[Item.ItemPosition.LeftHand] != null)
					{
						Item item = UserPackage[Item.ItemPosition.RightHand];
						Item arrow = UserPackage[Item.ItemPosition.LeftHand];
						if (arrow.GetItemSubType() == 1050 && arrow.Durability >= dwNum)
						{
							return true;
						}
					}
				}

				if (dwItem == 500)
				{
					if (UserPackage[Item.ItemPosition.RightHand] != null &&
						UserPackage[Item.ItemPosition.LeftHand] != null)
					{
						Item item = UserPackage[Item.ItemPosition.RightHand];
						if (item.GetItemSubType() == idItem && item.Durability >= dwNum)
						{
							return true;
						}
					}
				}

				if (dwItem == 420)
				{
					if (UserPackage[Item.ItemPosition.RightHand] != null)
					{
						Item item = UserPackage[Item.ItemPosition.RightHand];
						for (var i = 0; i < setSword.Length; i++)
						{
							if (item.GetItemSubType() == setSword[i] && item.Durability >= dwNum)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		#endregion

		#region Equipment Detain

		public async Task SendDetainedEquipmentAsync()
		{
			List<DbDetainedItem> items = await DetainedItemRepository.GetFromDischargerAsync(Identity);
			foreach (DbDetainedItem dbDischarged in items)
			{
				if (dbDischarged.ItemIdentity == 0)
				{
					continue; // item already claimed back
				}

				DbItem dbItem = await ItemRepository.GetByIdAsync(dbDischarged.ItemIdentity);
				if (dbItem == null)
				{
					await ServerDbContext.DeleteAsync(dbDischarged);
					continue;
				}

				Item item = new();
				if (!await item.CreateAsync(dbItem))
				{
					continue;
				}

				await SendAsync(new MsgDetainItemInfo(dbDischarged, item, MsgDetainItemInfo.Mode.DetainPage));
			}

			if (items.Count > 0)
			{
				await SendAsync(StrHasDetainEquip, TalkChannel.Talk);
			}
		}

		public async Task SendDetainRewardAsync()
		{
			List<DbDetainedItem> items = await DetainedItemRepository.GetFromHunterAsync(Identity);
			foreach (DbDetainedItem dbDetained in items)
			{
				DbItem dbItem = null;
				Item item = null;

				if (dbDetained.ItemIdentity != 0)
				{
					dbItem = await ItemRepository.GetByIdAsync(dbDetained.ItemIdentity);
					if (dbItem == null)
					{
						await ServerDbContext.DeleteAsync(dbDetained);
						continue;
					}

					item = new Item();
					if (!await item.CreateAsync(dbItem))
					{
						continue;
					}
				}

				bool expired = dbDetained.HuntTime + 60 * 60 * 24 * 7 < UnixTimestamp.Now;
				bool notClaimed = dbDetained.ItemIdentity != 0;

				MsgDetainItemInfo.Mode mode = MsgDetainItemInfo.Mode.ReadyToClaim;
				if (!expired && notClaimed)
				{
					// ? send message? do nothing
					mode = MsgDetainItemInfo.Mode.ClaimPage;
				}
				else if (expired && notClaimed && item != null)
				{
					// ? send message, item ready to be claimed
					if (item.IsBound)
					{
						await item.DeleteAsync();
						await ServerDbContext.DeleteAsync(dbDetained);
						continue;
					}

					if (ItemManager.Confiscator != null)
					{
						await SendAsync(
							string.Format(StrHasEquipBonus, dbDetained.TargetName,
										  ItemManager.Confiscator.Name, ItemManager.Confiscator.X,
										  ItemManager.Confiscator.Y), TalkChannel.Talk);
					}
					mode = MsgDetainItemInfo.Mode.ClaimPage;
				}
				else if (!notClaimed)
				{
					if (ItemManager.Confiscator != null)
					{
						await SendAsync(
							string.Format(StrHasEmoneyBonus, dbDetained.TargetName,
										  ItemManager.Confiscator.Name, ItemManager.Confiscator.X,
										  ItemManager.Confiscator.Y), TalkChannel.Talk);
					}

					// claimed, show CPs reward
					await SendAsync(new MsgItem
					{
						Action = MsgItem.ItemActionType.RedeemEquipment,
						Identity = dbDetained.Identity,
						Command = dbDetained.TargetIdentity,
						Argument2 = dbDetained.RedeemPrice
					});
				}

				await SendAsync(new MsgDetainItemInfo(dbDetained, item, mode));
				//if (item?.Quench != null)
				//{
				//	await item.Quench.SendToAsync(this);
				//}
			}

			if (items.Count > 0 && ItemManager.Confiscator != null)
			{
				await SendAsync(
					string.Format(StrPkBonus, ItemManager.Confiscator.Name, ItemManager.Confiscator.X,
								  ItemManager.Confiscator.Y), TalkChannel.Talk);
			}
		}

		#endregion
	}
}
