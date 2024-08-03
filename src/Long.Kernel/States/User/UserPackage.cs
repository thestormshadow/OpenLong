using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Shared.Helpers;
using Long.Shared.Managers;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgPackage;

namespace Long.Kernel.States.User
{
    public sealed class UserPackage
    {
        private static readonly ILogger logger = Log.ForContext<UserPackage>();
        private static readonly ILogger spendMeteorLogger = Logger.CreateLogger("spend_meteor");
        private static readonly ILogger spendDragonBallLogger = Logger.CreateLogger("spend_dragonball");

        public const int MAX_INVENTORY_CAPACITY = 40;

        private readonly ConcurrentDictionary<Item.ItemPosition, Item> equipments = new();
        private readonly ConcurrentDictionary<uint, Item> inventory = new();
        private readonly ConcurrentDictionary<uint, ConcurrentDictionary<uint, Item>> warehouses = new();
        private readonly ConcurrentDictionary<uint, ConcurrentDictionary<uint, Item>> sashes = new();
        private readonly ConcurrentDictionary<uint, Item> chestPackage = new();
        private readonly TimeOut checkItemsTimer = new();
        private readonly Character user;

        public bool IsSecondaryEquipmentUser { get; set; }
        public uint LastAddItemIdentity { get; set; }

        public UserPackage(Character user)
        {
            this.user = user;
        }

        public Item this[Item.ItemPosition position] => equipments.TryGetValue(position, out var item) ? item : null;
        public Item this[string name] => inventory.Values.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public async Task InitializeAsync()
        {
            var items = ItemRepository.Get(user.Identity);
            foreach (var dbItem in items.OrderBy(x => x.OwnerId).ThenBy(x => x.Position).ThenBy(x => x.Id))
            {
                Item item = new(user);
                if (!await item.CreateAsync(dbItem))
                {
                    logger.Error($"Failed to load item {dbItem.Id} to user {user.Identity}");
                    await item.DeleteAsync();
                    continue;
                }

                if (item.HasExpired())
                {
                    await item.ExpireAsync();
                    continue;
                }

                if (item.IsSuspicious()
                    && item.Position >= Item.ItemPosition.EquipmentBegin &&
                    item.Position <= Item.ItemPosition.SecondaryEquipmentEnd)
                {
                    item.Position = Item.ItemPosition.Inventory;
                    await item.SaveAsync();
                }

                if (item.Position >= Item.ItemPosition.EquipmentBegin &&
                    item.Position <= Item.ItemPosition.SecondaryEquipmentEnd)
                {
                    if (item.IsShield() && user.ProfessionSort >= 10)
                    {
                        item.Position = Item.ItemPosition.Inventory;
                        inventory.TryAdd(item.Identity, item);
                    }
                    else if (item.IsShield() && user.ProfessionSort != 2 && item.RequiredLevel > 70)
                    {
                        item.Position = Item.ItemPosition.Inventory;
                        inventory.TryAdd(item.Identity, item);
                    }
                    else if (!equipments.TryAdd(item.Position, item))
                    {
                        item.Position = Item.ItemPosition.Inventory;
                        inventory.TryAdd(item.Identity, item);
                    }
                    else
                    {
                        if ((item.IsGarment() || item.IsMountArmor())
                            && user.CoatStorage.Add(item))
                        {
                            await user.SendAsync(new MsgCoatStorage(item, MsgCoatStorage.CoatStorageAction.AddWrapItem));
                            await user.SendAsync(new MsgCoatStorage(item.Identity, 0, 0, MsgCoatStorage.CoatStorageAction.PutItemToWrapPackage));
                            await user.SendAsync(new MsgCoatStorage(item.Identity, item.Type, item, MsgCoatStorage.CoatStorageAction.EquipWrap));
                            await user.SendAsync(new MsgCoatStorage(item, MsgCoatStorage.CoatStorageAction.AddWrapItem));
                        }
                    }
                }
                else if (item.Position == Item.ItemPosition.Inventory)
                {
                    if (!inventory.TryAdd(item.Identity, item))
                    {
                        logger.Warning("Failed to insert inventory item {0}: duplicate???", item.Identity);
                        continue;
                    }
                }
                else if (item.Position == Item.ItemPosition.Floor)
                {
                    await item.DeleteAsync();
                    continue;
                }
                else if (item.Position == Item.ItemPosition.Storage || item.Position == Item.ItemPosition.Trunk)
                {
                    BaseNpc npc = RoleManager.GetRole(item.OwnerIdentity) as BaseNpc;
                    if (npc == null)
                    {
                        logger.Error($"Unexistent warehouse {item.OwnerIdentity} for item {item.Identity}");
                        continue;
                    }

                    await AddToStorageAsync(npc.Identity, item, StorageType.Storage, false);
                }
                else if (item.Position == Item.ItemPosition.Chest)
                {
                    await AddToStorageAsync(item.OwnerIdentity, item, StorageType.Chest, false);
                }
                else if (item.Position == Item.ItemPosition.ChestPackage)
                {
                    await AddToStorageAsync(item.OwnerIdentity, item, StorageType.ChestPackage, false);
                }
                else if (item.Position == Item.ItemPosition.CoatStorage)
                {
                    if ((item.IsGarment() || item.IsMountArmor())
                            && user.CoatStorage.Add(item))
                    {
                        await user.SendAsync(new MsgCoatStorage(item, MsgCoatStorage.CoatStorageAction.AddWrapItem));
                        await user.SendAsync(new MsgCoatStorage(item.Identity, 0, 0, MsgCoatStorage.CoatStorageAction.PutItemToWrapPackage));
                    }
                    else
                    {
                        inventory.TryAdd(item.Identity, item);
                    }
                }
                else if (item.Position == Item.ItemPosition.Auction)
                {
                    // skip
                    continue;
                }
                else
                {
                    logger.Warning("Item {0} on '{1}' cannot be loaded (unhandled)", item.Identity, item.Position);
                    continue;
                }
            }
            user.CoatStorage.UpdateAttributes();
        }

        public List<Item> GetInventory()
        {
            return [.. inventory.Values];
        }

        public Item GetInventory(uint idItem)
        {
            return inventory.TryGetValue(idItem, out var item) ? item : null;
        }

        public List<Item> GetEquipment()
        {
            return [.. equipments.Values];
        }

        public Item GetEquipmentById(uint idItem)
        {
            return equipments.Values.FirstOrDefault(x => x.Identity == idItem);
        }

        public Item GetEquipment(Item.ItemPosition position)
        {
            Item main = this[position];
            if (!IsSecondaryEquipmentUser)
            {
                return main;
            }
            Item secondary = this[position + 20];
            if (secondary == null)
            {
                return main;
            }
            //
            return secondary;
        }

		public bool TryGetItem(uint idItem, out Item item)
		{
			item = FindItemByIdentity(idItem);
			return item != null;
		}

		public Item FindItemByIdentity(uint id)
        {
            return equipments.Values.FirstOrDefault(x => x.Identity == id) ?? inventory.Values.FirstOrDefault(x => x.Identity == id);
        }

        public Item FindItemByIdentityAnywhere(uint id)
        {
            Item item = FindItemByIdentity(id);
            if (item != null)
            {
                return item;
            }

            if (sashes.Values.Any(sash => sash.TryGetValue(id, out item)))
            {
                return item;
            }

            item = chestPackage.Values.FirstOrDefault(x => x.Identity == id);
            if (item != null)
            {
                return item;
            }

            if (warehouses.Values.Any(warehouse => warehouse.TryGetValue(id, out item)))
            {
                return item;
            }

            return null;
        }

        public bool HasSecondaryEquipment => equipments.Values.Any(x => x.Position >= Item.ItemPosition.SecondaryEquipmentBegin && x.Position <= Item.ItemPosition.SecondaryEquipmentEnd);

        public void EquipOSItem(Item item)
        {
            equipments.TryAdd(item.Position, item);
        }

        public bool ForceAddEquipItem(Item.ItemPosition itemPosition, Item item)
        {
            return equipments.TryAdd(itemPosition, item);
        }

        public bool ForceRemoveEquipItem(Item.ItemPosition itemPosition, out Item item)
        {
            return equipments.TryRemove(itemPosition, out item);
        }

        private static readonly ILogger awardItemErrorLogger = Logger.CreateLogger("award_item_error");

        public async Task<bool> AwardItemAsync(uint type, int amount, bool monopoly = false, bool autoActivate = false)
        {
            DbItemtype itemtype = ItemManager.GetItemtype(type);
            if (itemtype == null)
            {
                return false;
            }

            if (!IsPackSpare(amount, type))
            {
                return false;
            }

            int tempAmount = amount;
            List<DbItem> items = new();
            while (tempAmount > 0)
            {
                int createAmount = (int)Math.Min(amount, itemtype.AccumulateLimit);
                DbItem item = Item.CreateEntity(type, monopoly);
                item.AccumulateNum = (uint)createAmount;
                items.Add(item);
                tempAmount -= Math.Max(1, createAmount);
            }
            
            if (await ServerDbContext.CreateRangeAsync(items))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    DbItem dbItem = items[i];
                    Item item = new Item(user);
                    if (!await item.CreateAsync(dbItem))
                    {
                        awardItemErrorLogger.Information("Could not create item {0}/{1} of type {2} for user {3} {4}", i, amount, type, user.Identity, user.Name);
                        continue;
                    }
                    await AddItemAsync(item);
                }
            }
            return false;
        }

        public async Task<bool> AwardItemAsync(uint type, Item.ItemPosition pos = Item.ItemPosition.Inventory, bool monopoly = false, bool autoActivate = false)
        {
            DbItemtype itemtype = ItemManager.GetItemtype(type);
            if (itemtype == null)
            {
                return false;
            }

            Item item = new(user);
            if (!await item.CreateAsync(itemtype, pos, monopoly))
            {
                return false;
            }
            if (item.IsCountable())
            {
                item.AccumulateNum = Math.Max(1, item.AccumulateNum);
            }
            if (item.IsActivable() && autoActivate)
            {
                await item.ActivateAsync();
            }
            return await AddItemAsync(item);
        }

        public async Task<bool> UseItemAsync(uint idItem, Item.ItemPosition position)
        {
            if (!user.IsAlive)
            {
                return true;
            }

            if (!TryItem(idItem, position))
            {
                return false;
            }

            Item item = GetInventory(idItem);
            if (item == null)
            {
                return false;
            }

            if (item.IsSuspicious())
            {
                return false;
            }

            if (item.Type == Item.TYPE_EXP_BALL)
            {
                if (user.Map != null && user.Map.IsNoExpMap())
                {
                    return false;
                }

                if (!user.CanUseExpBall())
                {
                    return false;
                }

                await user.IncrementExpBallAsync();
                await user.AwardExperienceAsync(user.CalculateExpBall());
                await SpendItemAsync(item);
                return true;
            }

            if (item.IsEquipEnable())
            {
                return await EquipItemAsync(item, position);
            }

            if (item.IsMedicine())
            {
                if (item.Life > 0 && user.Life == user.MaxLife ||
                    item.Mana > 0 && user.MaxMana == user.Mana)
                {
                    return true;
                }

                bool healEnable = item.Type == 1003010 || user.QueryStatus(StatusSet.POISON_STAR) == null;
                if (!healEnable || !await SpendItemAsync(item))
                {
                    return false;
                }

                if (item.Life > 0 && healEnable)
                {
                    await user.AddAttributesAsync(ClientUpdateType.Hitpoints, item.Life);
                    await user.DetachStatusAsync(StatusSet.POISON_STAR);
                }

                if (item.Mana > 0)
                {
                    await user.AddAttributesAsync(ClientUpdateType.Mana, item.Mana);
                }

                return true;
            }

            if (item.Itemtype.IdAction > 0)
            {
                user.InteractingNpc = 0;
                user.InteractingItem = item.Identity;
                user.ClearTaskId();

                bool result = await GameAction.ExecuteActionAsync(item.Itemtype.IdAction, user, null, item, string.Empty) && item.Itemtype.IdAction > 0;

                // todo validate auto
                if (item.GetItemSubType() == 60)
                {
                    await SpendItemAsync(item);
                }

                return result;
            }

            return false;
        }

        public async Task<bool> EquipItemAsync(Item item, Item.ItemPosition position)
        {
            if (item == null)
            {
                return false;
            }

            if (item.IsGarment() || item.IsMountArmor())
            {
                return await user.CoatStorage.EquipCoatAsync(item);
            }

            user.BattleSystem.ResetBattle();
            await user.MagicData.AbortMagicAsync(false);

            if (position == Item.ItemPosition.Inventory)
            {
                if (user.IsWing && !item.IsArrowSort())
                {
                    return false;
                }

                position = item.GetPosition();
                if (this[Item.ItemPosition.RightHand] != null
                    && this[Item.ItemPosition.LeftHand] == null
                    && item.IsWeaponOneHand()
                    && !item.IsBackswordType())
                {
                    position = Item.ItemPosition.LeftHand;
                }
            }

            switch (position)
            {
                case Item.ItemPosition.RightHand:
                case Item.ItemPosition.SecondaryRightHand:
                    {
                        if (!item.IsHoldEnable())
                        {
                            return false;
                        }

                        if (item.IsShield()
                            || item.IsHossuType()
                            || item.IsPistol())
                        {
                            return false;
                        }

                        Item.ItemPosition rightHandPosition = position;
                        Item.ItemPosition leftHandPosition = position + 1;
                        if (item.IsWeaponTwoHand())
                        {
                            if (!(this[rightHandPosition] != null
                                  && this[leftHandPosition] != null
                                  && !IsPackSpare(2)))
                            {
                                await UnEquipAsync(rightHandPosition);

                                if (this[leftHandPosition] != null
                                    && !this[leftHandPosition].IsArrowSort()
                                    && item.IsBow()
                                    && this[leftHandPosition]?.IsShield() == true)
                                {
                                    await UnEquipAsync(leftHandPosition);
                                }
                            }
                        }
                        else if (item.IsWeaponOneHand() || item.IsWeaponProBased())
                        {
                            Item leftHand = this[leftHandPosition];
                            if (!(leftHand != null && leftHand.IsArrowSort() && !IsPackSpare(2)))
                            {
                                await UnEquipAsync(rightHandPosition);
                                if (leftHand != null && leftHand.IsArrowSort())
                                {
                                    await UnEquipAsync(leftHandPosition);
                                }
                            }
                        }

                        break;
                    }

                case Item.ItemPosition.LeftHand:
                case Item.ItemPosition.SecondaryLeftHand:
                    {
                        if (!item.IsHoldEnable())
                        {
                            return false;
                        }

                        Item.ItemPosition rightHandPosition = position - 1;
                        Item.ItemPosition leftHandPosition = position;
                        Item rightHand = this[rightHandPosition];
                        if (rightHand == null)
                        {
                            return false;
                        }

                        if (item.IsBackswordType())
                        {
                            return false;
                        }

                        if (rightHand.IsBackswordType() && item.IsShield())
                        {
                            return false;
                        }

                        if (item.IsHossuType() && !rightHand.IsBackswordType())
                        {
                            return false;
                        }

                        if (user.ProfessionSort >= 10 && !item.IsHossuType())
                        {
                            return false;
                        }

                        if (!rightHand.IsBow() && item.IsArrowSort())
                        {
                            return false;
                        }

                        if (item.IsShield())
                        {
                            if (user.ProfessionSort >= 10)
                            {
                                return false;
                            }

                            if (rightHand != null && rightHand.IsWeaponTwoHand())
                            {
                                if (user.FirstProfession != 25 || user.PreviousProfession != 25 || user.Profession < 23)
                                {
                                    return false;
                                }
                            }

                            if (rightHand != null && rightHand.IsBackswordType())
                            {
                                return false;
                            }
                        }

                        if (rightHand.IsWeaponOneHand() && !rightHand.IsBackswordType() &&
                            (item.IsWeaponOneHand() || item.IsShield())
                            || rightHand.IsBow() && item.IsArrowSort())
                        {
                            await UnEquipAsync(leftHandPosition);
                        }
                        else if (rightHand.IsWeaponProBased())
                        {
                            if (item.IsShield())
                            {
                                return false;
                            }

                            if (rightHand.GetItemSubType() == item.GetItemSubType())
                            {
                                await UnEquipAsync(leftHandPosition);
                            }
                        }

                        break;
                    }

                default:
                    await UnEquipAsync(position);
                    break;
            }

            if (!await RemoveFromInventoryAsync(item, RemovalType.UnEquip))
            {
                return false;
            }

            if (!equipments.TryAdd(position, item))
            {
                await AddItemAsync(item);
                return false;
            }

            item.Position = position;
            await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.EquipmentWear, (uint)position));

            await item.SaveAsync();

            await SyncEquipmentAsync();

            switch (position)
            {
                case Item.ItemPosition.Headwear:
                case Item.ItemPosition.RightHand:
                case Item.ItemPosition.LeftHand:
                case Item.ItemPosition.Armor:
                case Item.ItemPosition.Garment:
                case Item.ItemPosition.Wing:
                case Item.ItemPosition.RightHandAccessory:
                case Item.ItemPosition.LeftHandAccessory:
                case Item.ItemPosition.Mount:
                case Item.ItemPosition.MountArmor:
                    await user.Screen.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
                    break;
                case Item.ItemPosition.SecondaryHeadwear:
                case Item.ItemPosition.SecondaryRightHand:
                case Item.ItemPosition.SecondaryLeftHand:
                case Item.ItemPosition.SecondaryArmor:
                case Item.ItemPosition.SecondaryGarment:
                    {
                        if (IsSecondaryEquipmentUser)
                        {
                            await user.Screen.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
                        }
                        break;
                    }
            }

            if (user.Team != null)
            {
                await user.Team.SyncFamilyBattlePowerAsync();
            }

            if (user.Guide?.ApprenticeCount > 0)
            {
                await user.Guide.SynchroApprenticesSharedBattlePowerAsync();
            }

            await user.SendAsync(new MsgPlayerAttribInfo(user));
            return true;
        }

        public async Task<bool> UnEquipAsync(Item.ItemPosition position, RemovalType mode = RemovalType.RemoveOnly)
        {
            Item item = this[position];
            if (item == null)
            {
                return false;
            }

            user.BattleSystem.ResetBattle();
            await user.MagicData.AbortMagicAsync(false);

            if (!IsSecondaryEquipmentUser)
            {
                if (position == Item.ItemPosition.RightHand
                    && this[Item.ItemPosition.LeftHand] != null)
                {
                    if (!IsPackSpare(2) && mode != RemovalType.Delete)
                    {
                        return false;
                    }

                    if (!await UnEquipAsync(Item.ItemPosition.LeftHand))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!IsPackSpare(1) && mode != RemovalType.Delete)
                    {
                        return false;
                    }
                }

                if (position == Item.ItemPosition.RightHand
                    && item.IsAssassinKnife()
                    && user.QueryStatus(StatusSet.PATH_OF_SHADOW) != null)
                {
                    await user.DetachStatusAsync(StatusSet.PATH_OF_SHADOW);
                }
            }
            else
            {
                if (position == Item.ItemPosition.SecondaryRightHand
                    && this[Item.ItemPosition.SecondaryLeftHand] != null)
                {
                    if (!IsPackSpare(2) && mode != RemovalType.Delete)
                    {
                        return false;
                    }

                    if (!await UnEquipAsync(Item.ItemPosition.SecondaryLeftHand))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!IsPackSpare(1) && mode != RemovalType.Delete)
                    {
                        return false;
                    }
                }

                if (position == Item.ItemPosition.SecondaryRightHand
                    && item.IsAssassinKnife()
                    && user.QueryStatus(StatusSet.PATH_OF_SHADOW) != null)
                {
                    await user.DetachStatusAsync(StatusSet.PATH_OF_SHADOW);
                }
            }

            equipments.TryRemove(position, out _);

            item.Position = Item.ItemPosition.Inventory;
            if (mode != RemovalType.Delete && mode != RemovalType.RemoveAndDisappear)
            {
                await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.EquipmentRemove, (uint)position));
                await user.SendAsync(new MsgItemInfo(item));
            }
            else
            {
                await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.EquipmentRemove, (uint)position));
                await user.SendAsync(new MsgItemInfo(item));
                await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.InventoryRemove));
            }

            if (mode == RemovalType.Delete)
            {
                await item.DeleteAsync();
            }
            else if (mode != RemovalType.RemoveAndDisappear)
            {
                await AddItemAsync(item);
                await item.SaveAsync();
            }
            else
            {
                await item.SaveAsync();
            }

            await SyncEquipmentAsync();

            switch (position)
            {
                case Item.ItemPosition.Headwear:
                case Item.ItemPosition.RightHand:
                case Item.ItemPosition.LeftHand:
                case Item.ItemPosition.Armor:
                case Item.ItemPosition.Garment:
                case Item.ItemPosition.Wing:
                case Item.ItemPosition.RightHandAccessory:
                case Item.ItemPosition.LeftHandAccessory:
                case Item.ItemPosition.Mount:
                case Item.ItemPosition.MountArmor:
                    {
                        await user.Screen.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
                        break;
                    }
                case Item.ItemPosition.SecondaryHeadwear:
                case Item.ItemPosition.SecondaryRightHand:
                case Item.ItemPosition.SecondaryLeftHand:
                case Item.ItemPosition.SecondaryArmor:
                case Item.ItemPosition.SecondaryGarment:
                    {
                        if (IsSecondaryEquipmentUser)
                        {
                            await user.Screen.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
                        }
                        break;
                    }
            }

            if (user.Team != null)
            {
                await user.Team.SyncFamilyBattlePowerAsync();
            }

            if (user.Guide?.ApprenticeCount > 0)
            {
                await user.Guide.SynchroApprenticesSharedBattlePowerAsync();
            }

            await user.SendAsync(new MsgPlayerAttribInfo(user));
            return true;
        }

        public bool TryItem(Item item, Item.ItemPosition position)
        {
            if (item == null)
            {
                return false;
            }

            if (item.IsSuspicious())
            {
                return false;
            }

            if (item.RequiredLevel > user.Level)
            {
                return false;
            }

            if (item.RequiredGender > 0 && item.RequiredGender != user.Gender)
            {
                return false;
            }

            if (item.Durability == 0 && !item.IsExpend())
            {
                return false;
            }

            if (user.Metempsychosis > 0 && user.Level >= 70)
            {
                return true;
            }

            if (item.RequiredProfession > 0)
            {
                int nRequireProfSort = item.RequiredProfession / 10;
                int nRequireProfLevel = item.RequiredProfession % 10;
                int nProfSort = user.ProfessionSort;
                int nProfLevel = user.ProfessionLevel;

                if (nRequireProfSort == 19)
                {
                    //if (nProfSort < 10
                    //    || position == Item.ItemPosition.LeftHand
                    //    || position == Item.ItemPosition.SecondaryLeftHand)
                    //{
                    //    return false;
                    //}
                }
                else
                {
                    if (nRequireProfSort != nProfSort)
                    {
                        return false;
                    }
                }

                if (nProfLevel < nRequireProfLevel)
                {
                    return false;
                }
            }

            if (item.RequiredForce > user.Strength
                || item.RequiredAgility > user.Speed
                || item.RequiredVitality > user.Vitality
                || item.RequiredSpirit > user.Spirit)
            {
                return false;
            }

            if (item.RequiredWeaponSkill > 0 &&
                user.WeaponSkill[(ushort)item.GetItemtype()]?.Level < item.RequiredWeaponSkill)
            {
                return false;
            }

            return true;
        }

        public bool TryItem(uint idItem, Item.ItemPosition position)
        {
            Item item = FindItemByIdentity(idItem);
            if (item == null)
            {
                return false;
            }
            return TryItem(item, position);
        }

        public async Task<bool> CombineArrowAsync(uint idItem, uint idOther)
        {
            return inventory.TryGetValue(idItem, out var item)
                   && inventory.TryGetValue(idOther, out var other)
                   && await CombineArrowAsync(item, other);
        }

        public async Task<bool> CombineArrowAsync(Item item, Item other)
        {
            if (item == null || other == null || !item.IsArrowSort() || item.Type != other.Type)
            {
                return false;
            }

            ushort nNewNum = (ushort)(item.Durability + other.Durability);
            if (nNewNum > item.MaximumDurability)
            {
                item.Durability = (ushort)(nNewNum - other.MaximumDurability);
                other.Durability = item.MaximumDurability;
                await user.SendAsync(new MsgItemInfo(other, MsgItemInfo.ItemMode.Update));
                await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
            }
            else
            {
                item.Durability = 0;
                await RemoveFromInventoryAsync(item.Identity, RemovalType.Delete);
                other.Durability = nNewNum;
                await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
            }

            return true;
        }

        public async Task<bool> AddItemAsync(Item item, bool combineEnable = true)
        {
            if (!IsPackSpare((int)item.AccumulateNum, item.Type))
            {
                return false;
            }

            if (item.PlayerIdentity != user.Identity)
            {
                item.ChangeOwner(user);
            }
            item.Position = Item.ItemPosition.Inventory;

            if (item.IsCountable() && combineEnable)
            {
                Item combine = FindCombineItem(item);
                if (combine != null)
                {
                    do
                    {
                        await CombineItemAsync(item, combine);
                    } while (item.AccumulateNum > 1 && item.AccumulateNum > item.MaxAccumulateNum &&
                             (combine = FindCombineItem(item)) != null);

                    if (item.AccumulateNum == 0)
                    {
                        // await RemoveFromInventoryAsync(item, RemovalType.Delete);
                        return true;
                    }
                }
            }

            if (item.AccumulateNum > item.MaxAccumulateNum)
            {
                var amount = item.AccumulateNum - item.MaxAccumulateNum;
                item.AccumulateNum = item.MaxAccumulateNum;

                DbItem newDbItem = Item.CreateEntity(item.Type, item.IsBound);
                newDbItem.AccumulateNum = amount;
                if (!item.IsPileEnable())
                {
                    newDbItem.ReduceDmg = item.ReduceDamage;
                    newDbItem.AddLife = item.Enchantment;
                    newDbItem.AntiMonster = item.AntiMonster;
                    newDbItem.Data = item.Data;
                    if (item.DeleteTime > 0)
                    {
                        newDbItem.DeleteTime = item.DeleteTime;
                    }
                    if (item.SaveTime > 0)
                    {
                        newDbItem.SaveTime = (uint)item.SaveTime;
                    }
                }

                Item newItem = new(user);
                if (await newItem.CreateAsync(newDbItem))
                {
                    await AddItemAsync(newItem);
                }
            }

            inventory.TryAdd(item.Identity, item);
            await item.SaveAsync();
            await user.SendAsync(new MsgItemInfo(item));
            if (item.ItemStatus != null)
            {
                await item.ItemStatus.SendToAsync(user);
            }
            return true;
        }

        public async Task<bool> RemoveFromInventoryAsync(uint idItem, RemovalType mode = RemovalType.RemoveOnly)
        {
            return inventory.TryGetValue(idItem, out var item) && await RemoveFromInventoryAsync(item, mode);
        }

        public async Task<bool> RemoveFromInventoryAsync(Item item, RemovalType mode = RemovalType.RemoveOnly)
        {
            if (!inventory.TryRemove(item.Identity, out _) && mode != RemovalType.Delete)
            {
                return false;
            }

            switch (mode)
            {
                case RemovalType.DropItem:
                    item.Position = Item.ItemPosition.Floor;
                    break;

                case RemovalType.Delete:
                    await item.DeleteAsync();
                    break;
            }

            if (mode != RemovalType.UnEquip)
            {
                await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.InventoryRemove));
            }

            return true;
        }

        public async Task<bool> SpendItemAsync(uint type, int amount = 1, bool denyBound = false)
        {
            if (type == Item.TYPE_METEOR)
            {
                return await SpendMeteorsAsync(amount);
            }
            if (type == Item.TYPE_DRAGONBALL)
            {
                return await SpendDragonBallsAsync(amount);
            }

            return await MultiSpendItemAsync(type, type, amount, denyBound);
        }

        public async Task<bool> SpendMeteorsAsync(int amount, bool refuseMonopoly = true)
        {
            int meteorAmount = MeteorAmount();
            if (meteorAmount < amount)
            {
                return false;
            }

            List<Item> items = new();
            int meteor = MultiGetItem(Item.TYPE_METEOR, Item.TYPE_METEORTEAR, 0, ref items, refuseMonopoly);
            int meteorScroll = MultiGetItem(Item.TYPE_METEOR_SCROLL, Item.TYPE_METEOR_SCROLL, 0, ref items, refuseMonopoly);            
            int taken = 0;
            int refund = 0;
            bool refundMonopoly = false;
            int remaining = amount;

            List<Item> consume = new();
            foreach (var item in items.OrderByDescending(x => x.Type)) // let's first consume all meteors
            {
                if (item.Type == Item.TYPE_METEOR || item.Type == Item.TYPE_METEORTEAR)
                {
                    taken += 1;
                    remaining -= 1;
                }
                else if (item.Type == Item.TYPE_METEOR_SCROLL)
                {
                    taken += 10;
                    remaining -= 10;
                }
                else
                {
                    continue;
                }

                consume.Add(item);

                if (remaining <= 0)
                {
                    if (remaining < 0)
                    {
                        refund = remaining * -1;
                        refundMonopoly = item.IsBound;
                    }
                    break;
                }
            }

            foreach (var item in consume)
            {
                await SpendItemAsync(item);
            }

            if (refund > 0)
            {
                await AwardItemAsync(Item.TYPE_METEOR, refund, refundMonopoly);
            }

            spendMeteorLogger.Information($"{user.Identity},{user.Name},Required:{amount},Taken:{taken},Refund:{refund},RefundMonopoly:{refundMonopoly},Remaining:{remaining}");
            return true;
        }

        public async Task<bool> SpendDragonBallsAsync(int amount, bool refuseMonopoly = true)
        {
            int dragonBallAmount = DragonBallAmount(!refuseMonopoly);
            if (dragonBallAmount < amount)
            {
                return false;
            }

            List<Item> items = new();
            int meteor = MultiGetItem(Item.TYPE_DRAGONBALL, Item.TYPE_DRAGONBALL, 0, ref items, refuseMonopoly);
            int meteorScroll = MultiGetItem(Item.TYPE_DRAGONBALL_SCROLL, Item.TYPE_DRAGONBALL_SCROLL, 0, ref items, refuseMonopoly);
            int taken = 0;
            int refund = 0;
            bool refundMonopoly = false;
            int remaining = amount;

            List<Item> consume = new();
            foreach (var item in items.OrderByDescending(x => x.Type))
            {
                if (item.Type == Item.TYPE_DRAGONBALL)
                {
                    taken += 1;
                    remaining -= 1;
                }
                else if (item.Type == Item.TYPE_DRAGONBALL_SCROLL)
                {
                    taken += 10;
                    remaining -= 10;
                }
                else
                {
                    continue;
                }

                consume.Add(item);

                if (remaining <= 0)
                {
                    if (remaining < 0)
                    {
                        refund = remaining * -1;
                        refundMonopoly = item.IsBound;
                    }
                    break;
                }
            }

            foreach (var item in consume)
            {
                await SpendItemAsync(item);
            }

            if (refund > 0)
            {
                await AwardItemAsync(Item.TYPE_DRAGONBALL, refund, refundMonopoly);
            }

            spendDragonBallLogger.Information($"{user.Identity},{user.Name},Required:{amount},Taken:{taken},Refund:{refund},RefundMonopoly:{refundMonopoly},Remaining:{remaining}");
            return true;
        }

        public async Task<bool> SpendItemAsync(Item item, bool deleteAll = false)
        {
            if (item == null)
            {
                return false;
            }

            if (!deleteAll && item.IsPileEnable() && item.AccumulateNum > 1)
            {
                item.AccumulateNum -= 1;
                await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                await item.SaveAsync();
                return true;
            }

            return item.Position == Item.ItemPosition.Inventory &&
                   await RemoveFromInventoryAsync(item.Identity, RemovalType.Delete);
        }

        public async Task<bool> CombineItemAsync(uint idItem, uint idCombine)
        {
            return inventory.TryGetValue(idItem, out var item)
                   && inventory.TryGetValue(idCombine, out var combine)
                   && await CombineItemAsync(item, combine);
        }

        public async Task<bool> CombineItemAsync(Item item, Item combine)
        {
            if (item == null || combine == null || !item.IsPileEnable() || item.Type != combine.Type)
            {
                return false;
            }

            if (item.IsBound && !combine.IsBound)
            {
                return false;
            }

            uint newNum = item.AccumulateNum + combine.AccumulateNum;
            if (newNum > item.MaxAccumulateNum)
            {
                item.AccumulateNum = newNum - combine.MaxAccumulateNum;
                combine.AccumulateNum = item.MaxAccumulateNum;
                await user.SendAsync(new MsgItemInfo(combine, MsgItemInfo.ItemMode.Update));
                await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                await item.SaveAsync();
                await combine.SaveAsync();
            }
            else
            {
                item.AccumulateNum = 0;
                await RemoveFromInventoryAsync(item, RemovalType.Delete);
                combine.AccumulateNum = newNum;
                await user.SendAsync(new MsgItemInfo(combine, MsgItemInfo.ItemMode.Update));
                await combine.SaveAsync();
            }
            return true;
        }

        public async Task<bool> SplitItemAsync(uint idItem, int amount)
        {
            return inventory.TryGetValue(idItem, out var item)
                   && await SplitItemAsync(item, amount);
        }

        public async Task<bool> SplitItemAsync(Item item, int amount)
        {
            if (item == null || !item.IsCountable() || !item.IsPileEnable())
            {
                return false;
            }

            if (amount <= 0)
            {
                return false;
            }

            if (amount >= item.MaxAccumulateNum)
            {
                return false;
            }

            if (!IsPackSpare(amount, item.Type))
            {
                return false;
            }

            DbItem split = Item.CreateEntity(item.Type, item.IsBound);
            split.PlayerId = user.Identity;

            item.AccumulateNum -= (uint)amount;
            await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
            await item.SaveAsync();

            split.AccumulateNum = (uint)amount;

            Item splitItem = new(user);
            if (!await splitItem.CreateAsync(split))
            {
                return false;
            }

            return await AddItemAsync(splitItem, false);
        }

        public Item FindCombineItem(Item item)
        {
            return inventory.Values.FirstOrDefault(x => x.Type == item.Type
                                                             && x.AccumulateNum < x.MaxAccumulateNum
                                                             && x.IsBound == item.IsBound);
        }

        public Item FindStorageCombineItem(Item item)
        {
            return GetStorageItems(item.OwnerIdentity, (StorageType)((int)item.Position % 10 * 10))
                        .Values.FirstOrDefault(x => x.Type == item.Type
                                                             && x.AccumulateNum < x.MaxAccumulateNum
                                                             && x.IsBound == item.IsBound);
        }

        public async Task<bool> CombineStorageItemAsync(Item item, Item combine)
        {
            if (item == null || combine == null || !item.IsPileEnable() || item.Type != combine.Type)
            {
                return false;
            }

            if (item.IsBound && !combine.IsBound)
            {
                return false;
            }

            uint newNum = item.AccumulateNum + combine.AccumulateNum;
            if (newNum > item.MaxAccumulateNum)
            {
                item.AccumulateNum = newNum - combine.MaxAccumulateNum;
                combine.AccumulateNum = item.MaxAccumulateNum;
                await user.SendAsync(new MsgPackage(item, WarehouseMode.Query, (StorageType)((int)item.Position % 10 * 10)));
                await user.SendAsync(new MsgPackage(combine, WarehouseMode.Query, (StorageType)((int)item.Position % 10 * 10)));
                await item.SaveAsync();
                await combine.SaveAsync();
            }
            else
            {
                item.AccumulateNum = 0;
                await RemoveFromInventoryAsync(item, RemovalType.Delete);
                combine.AccumulateNum = newNum;
                await user.SendAsync(new MsgPackage(combine, WarehouseMode.Query, (StorageType)((int)item.Position % 10 * 10)));
                await combine.SaveAsync();
            }
            return true;
        }

        public int MeteorAmount()
        {
            int amount = 0;
            foreach (var item in inventory.Values)
            {
                switch (item.Type)
                {
                    case Item.TYPE_METEOR:
                    case Item.TYPE_METEORTEAR:
                        amount++;
                        break;
                    case Item.TYPE_METEOR_SCROLL:
                        amount += 10;
                        break;
                }
            }

            return amount;
        }

        public int DragonBallAmount(bool bound = true, bool onlyBound = false)
        {
            int amount = 0;
            foreach (var item in inventory.Values)
            {
                if (!bound && item.IsBound)
                {
                    continue;
                }

                if (!item.IsBound && onlyBound)
                {
                    continue;
                }

                switch (item.Type)
                {
                    case Item.TYPE_DRAGONBALL:
                        amount++;
                        break;
                    case Item.TYPE_DRAGONBALL_SCROLL:
                        amount += 10;
                        break;
                }
            }

            return amount;
        }

        public async Task<bool> MultiSpendItemAsync(uint idFirst, uint idLast, int nNum, bool refuseMonopoly = false, bool saveTime = false)
        {
            int temp = nNum;
            List<Item> items = new();
            if (MultiGetItem(idFirst, idLast, nNum, ref items, refuseMonopoly, saveTime) < nNum)
            {
                return false;
            }

            foreach (var item in items)
            {
                if (refuseMonopoly && item.IsBound && (item.Itemtype.Monopoly & 1) == 0)
                {
                    continue;
                }

                if (item.IsPileEnable() && item.AccumulateNum > nNum)
                {
                    item.AccumulateNum -= (uint)nNum;
                    await item.SaveAsync();
                    await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                    return true;
                }

                nNum -= (int)item.AccumulateNum;
                await RemoveFromInventoryAsync(item, RemovalType.Delete);
            }

            if (nNum > 0)
            {
                logger.Error("Something went wrong when MultiSpendItem({0}, {1}, {2}) {3} left???", idFirst, idLast, temp, nNum);
            }

            return nNum == 0;
        }

        public int MultiGetItem(uint idFirst, uint idLast, int num, ref List<Item> items, bool refuseMonopoly = false, bool saveTime = false)
        {
            int amount = 0;
            foreach (var item in inventory
                .Values
                .Where(x => x.Type >= idFirst && x.Type <= idLast)
                .OrderBy(x => x.Type))
            {
                if (refuseMonopoly && item.IsBound && (item.Itemtype.Monopoly & 1) == 0)
                {
                    continue;
                }

                if (saveTime && item.IsActivable())
                {
                    continue;
                }

                items.Add(item);
                amount += (int)item.AccumulateNum;

                if (num > 0 && amount >= num)
                {
                    return amount;
                }
            }

            return amount;
        }

        public bool CheckItem(uint type, bool bound, bool sash)
        {
            List<Item> items = inventory.Values.Where(x => x.Type == type).ToList();
            if (sash)
            {
                items.AddRange(chestPackage.Values.Where(x => x.Type == type));
            }

            if (bound)
            {
                return items.Count > 0;
            }
            return items.Any(x => !x.IsBound);
        }

        public bool CheckAccumulate(uint type, int num, bool bound, bool sash)
        {
            List<Item> items = inventory.Values.Where(x => x.Type == type).ToList();
            if (sash)
            {
                items.AddRange(chestPackage.Values.Where(x => x.Type == type));
            }

            if (bound)
            {
                return items.Sum(x => x.AccumulateNum) >= num;
            }
            return items.Where(x => !x.IsBound).Sum(x => x.AccumulateNum) >= num;
        }

        public async Task<bool> DeleteItemAsync(uint itemType, bool bound, bool sash)
        {
            var item = GetItemByType(itemType, bound, sash);
            if (item == null)
            {
                return false;
            }
            return await SpendItemAsync(item);
        }

        public bool MultiCheckItem(uint idFirst, uint idLast, int num, bool denyBound = false, bool saveTime = false)
        {
            int amount = 0;
            foreach (var item in inventory.Values.Where(x => x.Type >= idFirst && x.Type <= idLast))
            {
                if (denyBound && item.IsBound && (item.Itemtype.Monopoly & 1) == 0)
                {
                    continue;
                }

                if (saveTime && item.IsActivable())
                {
                    continue;
                }

                amount += (int)item.AccumulateNum;
                if (amount >= num)
                {
                    return true;
                }
            }

            return amount >= num;
        }

        public int CountItemType(uint ownerId, uint itemType, int type, bool monopoly)
        {
            List<Item> items;
            if (type == 0)
            {
                // check on user
                items = inventory.Values.ToList();
            }
            else if (type == 10)
            {
                // check on warehouse (ownerId)
                if (!warehouses.TryGetValue(ownerId, out var wh))
                {
                    return 0;
                }
                items = wh.Values.ToList();
            }
            else if (type == 40)
            {
                // check on sash
                items = chestPackage.Values.ToList();
            }
            else
            {
                return 0;
            }

            return items.Count(x =>
            {
                if (x.Type != itemType)
                {
                    return false;
                }

                if (!monopoly && x.IsMonopoly())
                {
                    return false;
                }

                return true;
            });
        }

        public Item GetItemByType(uint type)
        {
            return inventory.Values.FirstOrDefault(x => x.Type == type);
        }

        public Item GetItemByType(uint type, bool bound, bool sash)
        {
            List<Item> items = inventory.Values.Where(x => x.Type == type).ToList();
            if (sash)
            {
                items.AddRange(chestPackage.Values.Where(x => x.Type == type));
            }
            if (bound)
            {
                return items.FirstOrDefault();
            }
            return items.FirstOrDefault(x => !x.IsBound);
        }

        public Item GetActiveItemByType(uint type)
        {
            foreach (var item in inventory.Values.Where(x => x.Type == type))
            {
                if (item.IsActivable())
                {
                    continue;
                }

                return item;
            }
            return null;
        }

		public Item FindByIdentity(uint id)
		{
			return equipments.Values.FirstOrDefault(x => x.Identity == id) ?? inventory.Values.FirstOrDefault(x => x.Identity == id);
		}

		public Item FindByIdentityAnywhere(uint id)
		{
			Item item = FindByIdentity(id);
			if (item != null)
			{
				return item;
			}

			if (sashes.Values.Any(sash => sash.TryGetValue(id, out item)))
			{
				return item;
			}

			item = chestPackage.Values.FirstOrDefault(x => x.Identity == id);
			if (item != null)
			{
				return item;
			}

			if (warehouses.Values.Any(warehouse => warehouse.TryGetValue(id, out item)))
			{
				return item;
			}

			return null;
		}

		public async Task<bool> DelAllItemByTypeAsync(uint itemType)
        {
            // TODO check if need to check other places
            bool success = false;
            foreach (var item in inventory.Values)
            {
                if (item.Type == itemType && await SpendItemAsync(item))
                {
                    success = true;
                }
            }
            return success;
        }

        public async Task<int> RandDropItemAsync(int mapType, int nChance)
        {
            if (user == null)
            {
                return 0;
            }

            int nDropNum = 0;
            foreach (var item in inventory.Values)
            {
                if (await ChanceCalcAsync(nChance))
                {
                    if (item.IsNeverDropWhenDead() || item.IsSuspicious())
                    {
                        continue;
                    }

                    switch (mapType)
                    {
                        case 0: // NONE
                            break;
                        case 1: // PK
                        case 2: // SYN
                            if (!item.IsArmor())
                            {
                                continue;
                            }

                            break;
                        case 3: // PRISON
                            break;
                    }

                    var pos = new Point(user.X, user.Y);
                    if (user.Map.FindDropItemCell(5, ref pos))
                    {
                        if (!await RemoveFromInventoryAsync(item.Identity, RemovalType.DropItem))
                        {
                            continue;
                        }

                        var pMapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
                        if (pMapItem.Create(user.Map, pos, item, user.Identity, MapItem.DropMode.Common))
                        {
                            await pMapItem.EnterMapAsync();
                            await item.SaveAsync();
                        }
                        else
                        {
                            IdentityManager.MapItem.ReturnIdentity(pMapItem.Identity);
                        }

                        nDropNum++;
                    }
                }
            }

            return nDropNum;
        }

        public async Task<int> RandDropItemAsync(int nDropNum)
        {
            if (user == null)
            {
                return 0;
            }

            var temp = new List<Item>();
            foreach (var item in inventory.Values)
            {
                if (item.IsNeverDropWhenDead() || item.IsSuspicious())
                {
                    continue;
                }

                temp.Add(item);
            }

            int nTotalItemCount = temp.Count;
            if (nTotalItemCount == 0)
            {
                return 0;
            }

            int nRealDropNum = 0;
            for (int i = 0; i < Math.Min(nDropNum, nTotalItemCount); i++)
            {
                int nIdx = await NextAsync(nTotalItemCount);
                Item item;
                try
                {
                    item = temp[nIdx];
                }
                catch
                {
                    continue;
                }

                var pos = new Point(user.X, user.Y);
                if (user.Map.FindDropItemCell(5, ref pos))
                {
                    if (!await RemoveFromInventoryAsync(item.Identity, RemovalType.DropItem))
                    {
                        continue;
                    }

                    var pMapItem = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
                    if (pMapItem.Create(user.Map, pos, item, user.Identity, MapItem.DropMode.Common))
                    {
                        await pMapItem.EnterMapAsync();
                        await item.SaveAsync();
                    }
                    else
                    {
                        IdentityManager.MapItem.ReturnIdentity(pMapItem.Identity);
                    }

                    nRealDropNum++;
                }
            }

            return nRealDropNum;
        }

        public int InventoryCount => inventory.Count;

        public bool IsPackSpare(int size, uint type = 0)
        {
            if (type != 0)
            {
                var itemType = ItemManager.GetItemtype(type);
                int free = 0;
                if (itemType != null)
                {
                    foreach (var item in inventory.Values
                        .Where(x => x.Type == type && x.AccumulateNum < x.MaxAccumulateNum))
                    {
                        free += (int)(item.MaxAccumulateNum - item.AccumulateNum);
                        if (free >= size)
                        {
                            return true;
                        }
                    }
                    int add = (int)((MAX_INVENTORY_CAPACITY - inventory.Count) * Math.Max(1u, itemType.AccumulateLimit));
                    return free + add >= size;
                }
            }
            return inventory.Count + size <= MAX_INVENTORY_CAPACITY;
        }

        public bool IsPackFull()
        {
            return inventory.Count >= MAX_INVENTORY_CAPACITY;
        }

        #region Storage

        public async Task<bool> AddToStorageAsync(uint idStorage, Item item, StorageType mode, bool sync)
        {
            if (item.Position != Item.ItemPosition.Inventory && sync)
            {
                return false;
            }

            Item chestItem;
            BaseNpc npc = null;
            ConcurrentDictionary<uint, Item> items = null;
            int maxStorage;
            if (mode == StorageType.Storage || mode == StorageType.Trunk)
            {
                npc = RoleManager.GetRole(idStorage) as BaseNpc;
                if (npc == null)
                {
                    return false;
                }

                if (npc.Data3 != 0)
                {
                    maxStorage = npc.Data3;
                }
                else
                {
                    maxStorage = 60;
                }

                if (!warehouses.TryGetValue(idStorage, out items))
                {
                    warehouses.TryAdd(idStorage, items = new());
                }
            }
            else if (mode == StorageType.Chest)
            {
                chestItem = GetInventory(idStorage);
                if (chestItem == null || chestItem.GetItemSort() != (Item.ItemSort)11)
                {
                    return false;
                }

                maxStorage = (int)(chestItem.Type % 1000);
                if (maxStorage == 9)
                {
                    maxStorage += 3;
                }
                if (!sashes.TryGetValue(idStorage, out items))
                {
                    sashes.TryAdd(idStorage, items = new());
                }
            }
            else if (mode == StorageType.ChestPackage)
            {
                maxStorage = (int)user.SashSlots;
            }
            else
            {
                if (user.IsPm())
                {
                    await user.SendAsync($"AddToStorageAsync::Invalid storage type: {mode}");
                }

                return false;
            }

            if (!item.CanBeStored())
            {
                return false;
            }

            if ((mode != StorageType.ChestPackage && items == null) || StorageSize(idStorage, mode) >= maxStorage)
            {
                return false;
            }

            if (sync && !await RemoveFromInventoryAsync(item, RemovalType.RemoveAndDisappear))
            {
                return false;
            }

            item.OwnerIdentity = idStorage;
            item.Position = (Item.ItemPosition)(200 + (byte)mode / 10);
            if (item.IsCountable())
            {
                Item combine = FindStorageCombineItem(item);
                if (combine != null)
                {
                    do
                    {
                        await CombineStorageItemAsync(item, combine);
                    } while (item.AccumulateNum > 1 && item.AccumulateNum > item.MaxAccumulateNum &&
                             (combine = FindStorageCombineItem(item)) != null);

                    if (item.AccumulateNum == 0)
                    {
                        await RemoveFromInventoryAsync(item, RemovalType.Delete);
                        return true;
                    }
                }
            }

            if (mode == StorageType.ChestPackage)
            {
                chestPackage.TryAdd(item.Identity, item);
            }
            else
            {
                items.TryAdd(item.Identity, item);
            }
            await item.SaveAsync();

            if (sync)
            {
                await user.SendAsync(new MsgPackage(item, WarehouseMode.CheckIn, mode));
                if (item.ItemStatus != null)
                {
                    await item.ItemStatus.SendToAsync(user);
                }
                await item.TryUnlockAsync();
            }
            return true;
        }

        public async Task<bool> GetFromStorageAsync(uint idStorage, uint idItem, StorageType mode, bool sync)
        {
            ConcurrentDictionary<uint, Item> storage = null;
            if (mode == StorageType.Storage || mode == StorageType.Trunk)
            {
                if (!warehouses.TryGetValue(idStorage, out storage))
                {
                    return false;
                }
            }
            else if (mode == StorageType.Chest)
            {
                if (!sashes.TryGetValue(idStorage, out storage))
                {
                    return false;
                }
            }

            if (mode != StorageType.ChestPackage)
            {
                if (storage == null)
                {
                    return false;
                }

                if (!storage.ContainsKey(idItem))
                {
                    return false;
                }
            }

            if (!user.UserPackage.IsPackSpare(1))
            {
                return false;
            }

            Item item;
            if (mode == StorageType.ChestPackage)
            {
                if (!chestPackage.TryRemove(idItem, out item))
                {
                    return false;
                }
            }
            else
            {
                if (storage == null)
                {
                    return false;
                }

                if (!storage.TryRemove(idItem, out item))
                {
                    return false;
                }
            }

            if (sync)
            {
                await user.SendAsync(new MsgPackage(item, WarehouseMode.CheckOut, mode));
            }

            await user.UserPackage.AddItemAsync(item);

            if (item.HasExpired())
            {
                await item.ExpireAsync();
                return false;
            }

            if (mode == StorageType.ChestPackage)
            {
                if (user.VipLevel > 0 && user.UserPackage.MultiCheckItem(Item.TYPE_METEOR, Item.TYPE_METEOR, 10, true))
                {
                    await user.UserPackage.MultiSpendItemAsync(Item.TYPE_METEOR, Item.TYPE_METEOR, 10, true);
                    await user.UserPackage.AwardItemAsync(Item.TYPE_METEOR_SCROLL);
                }
                else if (user.VipLevel > 0 && user.UserPackage.MultiCheckItem(Item.TYPE_DRAGONBALL, Item.TYPE_DRAGONBALL, 10, true))
                {
                    await user.UserPackage.MultiSpendItemAsync(Item.TYPE_DRAGONBALL, Item.TYPE_DRAGONBALL, 10, true);
                    await user.UserPackage.AwardItemAsync(Item.TYPE_DRAGONBALL_SCROLL);
                }
            }

            return true;
        }

        public ConcurrentDictionary<uint, Item> GetStorageItems(uint idStorage, StorageType type)
        {
            switch (type)
            {
                case StorageType.Storage:
                case StorageType.Trunk:
                    return warehouses.TryGetValue(idStorage, out var storage) ? storage : new();
                case StorageType.Chest:
                    return sashes.TryGetValue(idStorage, out var chest) ? chest : new();
                case StorageType.ChestPackage:
                    return chestPackage;
            }
            return new();
        }

        public int StorageSize(uint idStorage, StorageType type)
        {
            switch (type)
            {
                case StorageType.Storage:
                    return warehouses.TryGetValue(idStorage, out var storage) ? storage.Count : 0;
                case StorageType.Chest:
                    return sashes.TryGetValue(idStorage, out var chest) ? chest.Count : 0;
                case StorageType.ChestPackage:
                    return chestPackage.Count;
            }
            return 0;
        }

        #endregion

        public async Task ClearInventoryAsync()
        {
            foreach (var item in inventory.Values)
            {
                await RemoveFromInventoryAsync(item, RemovalType.RemoveAndDisappear);
                await item.DeleteAsync();
            }
        }

        /// <summary>
        ///     Sent only on login!!!
        /// </summary>
        public async Task SendAsync()
        {
            List<Item> lockedItems = new();
            MsgItem msg = new()
            {
                Action = MsgItem.ItemActionType.DisplayGears
            };
            foreach (var item in equipments.Values)
            {
                await user.SendAsync(new MsgItemInfo(item));
                if (item.ItemStatus != null)
                {
                    await item.ItemStatus.SendToAsync(user);
                }

                await item.TryUnlockAsync();
                if (item.IsLocked() && item.IsUnlocking())
                {
                    lockedItems.Add(item);
                }

                msg.Append(item.Position, item.Identity);
            }
            await user.SendAsync(msg);

            foreach (var item in inventory.Values)
            {
                await user.SendAsync(new MsgItemInfo(item));
                if (item.ItemStatus != null)
                {
                    await item.ItemStatus.SendToAsync(user);
                }
                await item.TryUnlockAsync();
                if (item.IsLocked() && item.IsUnlocking())
                {
                    lockedItems.Add(item);
                }
            }

            if (lockedItems.Count > 0)
            {
                await user.SendAsync(new MsgAction
                {
                    Action = MsgAction.ActionType.ClientCommand,
                    Identity = user.Identity,
                    Command = 1207,
                    ArgumentX = user.X,
                    ArgumentY = user.Y
                });

                foreach (var item in lockedItems)
                {
                    await user.SendAsync(new MsgEquipLock
                    {
                        Identity = item.Identity,
                        Action = MsgEquipLock.LockMode.LockedItemNotify
                    });
                }
            }
        }

        public Task SyncEquipmentAsync()
        {
            MsgItem msg = new()
            {
                Action = MsgItem.ItemActionType.DisplayGears,
                Command = IsSecondaryEquipmentUser ? 1u : 0u
            };
            for (Item.ItemPosition position = Item.ItemPosition.EquipmentBegin; position <= Item.ItemPosition.EquipmentEnd; position++)
            {
                Item item = GetEquipment(position);
                if (item != null)
                {
                    msg.Append(position, item.Identity);
                }
            }
            return user.SendAsync(msg);
        }


        public async Task OnTimerAsync()
        {
            if (!checkItemsTimer.ToNextTime())
            {
                return;
            }

            for (Item.ItemPosition p = Item.ItemPosition.EquipmentBegin; p <= Item.ItemPosition.EquipmentEnd; p++)
            {
                Item testItem = this[p];
                if (testItem == null)
                {
                    continue;
                }

                if (testItem.HasExpired())
                {
                    await testItem.ExpireAsync();
                    continue;
                }

                if (testItem.IsUnlocking())
                {
                    await testItem.TryUnlockAsync();
                }

                if (testItem.ItemStatus != null)
                {
                    await testItem.ItemStatus.OnTimerAsync(user);
                }
            }

            foreach (var testItem in inventory.Values)
            {
                if (testItem.HasExpired())
                {
                    await testItem.ExpireAsync();
                    continue;
                }

                if (testItem.IsUnlocking())
                {
                    await testItem.TryUnlockAsync();
                }

                if (testItem.ItemStatus != null)
                {
                    await testItem.ItemStatus.OnTimerAsync(user);
                }
            }
        }

        public enum RemovalType
        {
            /// <summary>
            ///     Item will be removed and disappear, but wont be deleted.
            /// </summary>
            RemoveAndDisappear,

            /// <summary>
            ///     Item will be internally removed only. No client interaction and also wont be deleted.
            /// </summary>
            RemoveOnly,

            /// <summary>
            ///     Item will be removed and deleted.
            /// </summary>
            Delete,

            /// <summary>
            ///     Item will be set to floor and will be updated. No delete.
            /// </summary>
            DropItem,
            UnEquip
        }
    }
}
