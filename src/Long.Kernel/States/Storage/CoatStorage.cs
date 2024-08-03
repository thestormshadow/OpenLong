using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using static Long.Kernel.Network.Game.Packets.MsgCoatStorage;
using static Long.Kernel.States.User.UserPackage;

namespace Long.Kernel.States.Storage
{
    public sealed class CoatStorage
    {
        private readonly TimeOut timeOut = new(10);
        private readonly Character user;
        private readonly ConcurrentDictionary<uint, Item> wardrobeStorage = new();

        public CoatStorage(Character user)
        {
            this.user = user;
        }

        public int MaxLife { get; private set; }
        public int Attack { get; private set; }
        public int MagicAttack { get; private set; }
        public int Defense { get; private set; }
        public int MagicDefense { get; private set; }
        public int FinalDamage { get; private set; }
        public int FinalMagicDamage { get; private set; }
        public int FinalDefense { get; private set; }
        public int FinalMagicDefense { get; private set; }
        public int CriticalStrike { get; private set; }
        public int SkillCriticalStrike { get; private set; }
        public int Immunity { get; private set; }
        public int Breakthrough { get; private set; }
        public int Counteraction { get; private set; }
        public int HeavenBlessing { get; private set; }
        public int ExtraChampionPoints { get; private set; }
        public int HuntingExperienceBonus { get; private set; }

        public bool Add(Item item)
        {
            return wardrobeStorage.TryAdd(item.Identity, item);
        }

        public bool Get(uint itemId, out Item item)
        {
            return wardrobeStorage.TryGetValue(itemId, out item);
        }

        public bool Remove(uint itemId, out Item item)
        {
            return wardrobeStorage.TryRemove(itemId, out item);
        }

        public bool IsCoatTypeStored(uint idType)
        {
            return wardrobeStorage.Values.FirstOrDefault(x => x.Type == idType) != null;
        }

        public bool GetFromCoatStorage(uint idItem, out Item item)
        {
            return wardrobeStorage.TryGetValue(idItem, out item);
        }

        public bool IsCoatStored(uint idItem)
        {
            return wardrobeStorage.ContainsKey(idItem);
        }

        public async Task<bool> EquipCoatAsync(uint idItem)
        {
            if (wardrobeStorage.TryGetValue(idItem, out var item))
            {
                return await EquipCoatAsync(item);
            }
            return false;
        }

        public async Task<bool> EquipCoatAsync(Item item)
        {
            Item.ItemPosition itemPosition = item.GetPosition();

            if (itemPosition != Item.ItemPosition.Garment
                && itemPosition != Item.ItemPosition.MountArmor)
            {
                return false;
            }

            if (!user.UserPackage.TryItem(item, itemPosition))
            {
                return false;
            }

            await UnequipCoatAsync(itemPosition);

            item.Position = itemPosition;
            await item.SaveAsync();

            user.UserPackage.ForceRemoveEquipItem(itemPosition, out _); // JustInCase
            user.UserPackage.ForceAddEquipItem(itemPosition, item);

            await user.SendAsync(new MsgItem(item.Identity, MsgItem.ItemActionType.EquipmentWear, (uint)itemPosition));
            await user.SendAsync(new MsgItemInfo(item));
            await user.UserPackage.SyncEquipmentAsync();
            await user.SendAsync(new MsgPlayerAttribInfo(user));

            await user.SendAsync(new MsgCoatStorage(item.Identity, item.Type, item, CoatStorageAction.EquipWrap));
            await user.SendAsync(new MsgCoatStorage(item, CoatStorageAction.AddWrapItem));
            return true;
        }

        public async Task<bool> UnequipCoatAsync(Item.ItemPosition itemPosition, bool sync, RemovalType mode = RemovalType.RemoveOnly)
        {
            var item = user.UserPackage.GetEquipment(itemPosition);
            if (item == null) 
            {
                return false; 
            }

            if (await UnequipCoatAsync(itemPosition, mode))
            {
                if (sync)
                {
                    await user.SendAsync(new MsgCoatStorage(item.Identity, item.Type, 0, CoatStorageAction.UnequipWrap));
                }
                return true;
            }
            return false;
        }

        public async Task<bool> UnequipCoatAsync(Item.ItemPosition itemPosition, RemovalType mode = RemovalType.RemoveOnly)
        {
            var item = user.UserPackage.GetEquipment(itemPosition);
            if (item == null)
            {
                return false;
            }

			await user.SendAsync(new MsgCoatStorage(item.Identity, item.Type, 0, CoatStorageAction.UnequipWrap));

			if (mode == RemovalType.Delete)
            {
                await item.DeleteAsync();
            }
            else
            {
                item.Position = Item.ItemPosition.CoatStorage;
                await item.SaveAsync();
            }
            await user.SendAsync(new MsgItemInfo(item));
            await user.UserPackage.SyncEquipmentAsync();
            await user.Screen.BroadcastRoomMsgAsync(new MsgPlayer(user), false);
            await user.SendAsync(new MsgPlayerAttribInfo(user));
            return true;
        }

        public async Task<bool> AddToCoatStorageAsync(Item item)
        {
            if (!item.IsGarment() && !item.IsMountArmor())
            {
                return false;
            }

            if (IsCoatTypeStored(item.Type))
            {
                return false;
            }

            if (!await user.UserPackage.RemoveFromInventoryAsync(item, RemovalType.RemoveAndDisappear))
            {
                return false;
            }

            item.Position = Item.ItemPosition.CoatStorage;
            await item.SaveAsync();
            wardrobeStorage.TryAdd(item.Identity, item);

            await user.SendAsync(new MsgCoatStorage(item, CoatStorageAction.AddWrapItem));
            await user.SendAsync(new MsgCoatStorage(item, CoatStorageAction.PutItemToWrapPackage));

            UpdateAttributes();
            return await EquipCoatAsync(item);
        }

        public async Task<bool> CombineCoatAsync(uint targetItemId, uint tempItemId)
        {
            var tempCoat = user.UserPackage.GetInventory(tempItemId);
            if (tempCoat == null)
            {
                return false;
            }
;
            if (!wardrobeStorage.TryGetValue(targetItemId, out var targetCoat))
            {
                return false;
            }

            if (targetCoat.Type != tempCoat.Type)
            {
                return false;
            }

            if (!targetCoat.IsGarment() && !tempCoat.IsMountArmor())
            {
                return false;
            }

            if (!tempCoat.IsGarment() && !tempCoat.IsMountArmor())
            {
                return false;
            }

            if (targetCoat.IsGarment() != tempCoat.IsGarment())
            {
                return false;
            }

            if (targetCoat.IsMountArmor() != tempCoat.IsMountArmor())
            {
                return false;
            }

            if (targetCoat.IsBound && !tempCoat.IsBound)
            {
                targetCoat.IsBound = false;
            }

            if (targetCoat.ReduceDamage < tempCoat.ReduceDamage)
            {
                targetCoat.ReduceDamage = tempCoat.ReduceDamage;
            }

            if (targetCoat.RemainingSeconds > 0 && tempCoat.RemainingSeconds > 0)
            {
                targetCoat.UpdateAddTime(targetCoat.RemainingSeconds);
            }
            else if (targetCoat.RemainingSeconds > 0 && tempCoat.RemainingSeconds == 0)
            {
                targetCoat.UpdateAddTime(-1);
            }

            if (!await user.UserPackage.RemoveFromInventoryAsync(tempCoat, RemovalType.Delete))
            {
                return false;
            }

            await user.SendAsync(new MsgItemInfo(targetCoat, MsgItemInfo.ItemMode.Update));
            await targetCoat.SaveAsync();
            await user.SendAsync(new MsgCoatStorage(targetCoat.Identity, targetCoat.Type, targetCoat, CoatStorageAction.WrapCombine));
            UpdateAttributes();
            return true;
        }

        public async Task<bool> RetrieveFromCoatStorageAsync(uint itemId, bool sync)
        {
            if (!user.UserPackage.IsPackSpare(1))
            {
                return false;
            }

            if (!wardrobeStorage.TryRemove(itemId, out var coat))
            {
                return false;
            }

            await UnequipCoatAsync(coat.Position);

            if (sync)
            {
                await user.SendAsync(new MsgCoatStorage(coat.Identity, coat.Type, 0, CoatStorageAction.DelWrapItem));
                await user.SendAsync(new MsgCoatStorage(coat.Identity, coat.Type, coat, CoatStorageAction.DelWrapItem));
            }

            await user.UserPackage.AddItemAsync(coat, true);
            if (coat.HasExpired())
            {
                await coat.ExpireAsync();
            }

            UpdateAttributes();
            return true;
        }

        public void UpdateAttributes()
        {
            MaxLife = 0;
            Attack = 0;
            MagicAttack = 0;
            Defense = 0;
            FinalDamage = 0;
            FinalMagicDamage = 0;
            FinalDefense = 0;
            FinalMagicDefense = 0;
            CriticalStrike = 0;
            SkillCriticalStrike = 0;
            Immunity = 0;
            Breakthrough = 0;
            Counteraction = 0;
            HeavenBlessing = 0;
            ExtraChampionPoints = 0;
            HuntingExperienceBonus = 0;

            int coatCount = wardrobeStorage.Values.Count(x => x.IsGarment());
            foreach (var coatType in CoatStorageManager.GetStorageAttributes(1, coatCount))
            {
                AppendAttribute(coatType);
            }
            int mountCoatCount = wardrobeStorage.Values.Count(x => x.IsMountArmor());
            foreach (var coatType in CoatStorageManager.GetStorageAttributes(2, mountCoatCount))
            {
                AppendAttribute(coatType);
            }
        }

        private void AppendAttribute(DbCoatStorageAttr attribute)
        {
            for (int i = 0; i < 4; i++)
            {
                int value = attribute.GetAttrValue(i);
                switch ((CoatStorageManager.CoatStorageAttributeType)attribute.GetAttrType(i))
                {
                    case CoatStorageManager.CoatStorageAttributeType.MaxLife:
                        MaxLife += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.Attack:
                        Attack += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.MagicAttack:
                        MagicAttack += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.Defense:
                        Defense += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.MagicDefense:
                        MagicDefense += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.FinalDamage:
                        FinalDamage += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.FinalMagicDamage:
                        FinalMagicDamage += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.FinalDefense:
                        FinalDefense += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.FinalMagicDefense:
                        FinalMagicDefense += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.CriticalStrike:
                        CriticalStrike += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.SkillCriticalStrike:
                        SkillCriticalStrike += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.Immunity:
                        Immunity += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.Breakthrough:
                        Breakthrough += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.Counteraction:
                        Counteraction += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.HeavenBlessing:
                        HeavenBlessing += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.ExtraChampionPoints:
                        ExtraChampionPoints += value;
                        break;
                    case CoatStorageManager.CoatStorageAttributeType.HuntingExperienceBonus:
                        HuntingExperienceBonus += value;
                        break;
                }
            }
        }

        public async Task OnTimerAsync()
        {
            if (!timeOut.ToNextTime())
            {
                return;
            }

            foreach (var coat in wardrobeStorage.Values)
            {
                if (coat.HasExpired())
                {
                    await coat.ExpireAsync();
                    continue;
                }

                if (coat.IsUnlocking())
                {
                    await coat.TryUnlockAsync();
                }
            }
        }
    }
}
