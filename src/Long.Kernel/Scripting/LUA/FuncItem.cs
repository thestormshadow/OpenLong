using Canyon.Game.Scripting.Attributes;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using static Long.Kernel.Scripting.LUA.LuaScriptConst;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private Item GetItem(int itemId)
        {
            if (itemId <= 0)
            {
                return item;
            }
            else
            {
                return user?.UserPackage.FindItemByIdentity((uint)itemId);
            }
        }

        [LuaFunction]
        public long GetItemInt(int itemId, int index)
        {
            Item item = GetItem(itemId);
            if (item == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_ITEM_ID: return item.Identity;
                case G_ITEM_Type: return item.Type;
                case G_ITEM_OwnerID: return item.OwnerIdentity;
                case G_ITEM_PlayerID: return item.PlayerIdentity;
                case G_ITEM_Amount: return item.Durability;
                case G_ITEM_AmountLimit: return item.MaximumDurability;
                case G_ITEM_Ident: return 0;
                case G_ITEM_Position: return (long)item.Position;
                case G_ITEM_Gem1: return (long)item.SocketOne;
                case G_ITEM_Gem2: return (long)item.SocketTwo;
                case G_ITEM_Magic1: return (long)item.Effect;
                case G_ITEM_Magic2: return 0;
                case G_ITEM_Addition: return item.Plus;
                case G_ITEM_Data: return item.Data;
                case G_ITEM_ReduceDmg: return item.ReduceDamage;
                case G_ITEM_AddLife: return item.Enchantment;
                case G_ITEM_AntiMonster: return item.AntiMonster;
                case G_ITEM_Color: return (long)item.Color;
                case G_ITEM_Monopoly: return item.Monopoly;
                case G_ITEM_AddExp: return item.CompositionProgress;
                case G_ITEM_DelTime: return item.DeleteTime;
                case G_ITEM_SaveTime: return item.SaveTime;
                case G_ITEM_AcutionDeposit:
                case G_ITEM_ITEM_VALUE:
                default: return 0;
            }
        }

        [LuaFunction]
        public string GetItemStr(int itemId, int index)
        {
            Item item = GetItem(itemId);
            if (item == null)
            {
                return StrNone;
            }

            switch (index)
            {
                case G_ITEM_Name: return item.Name;
            }
            return StrNone;
        }

        [LuaFunction]
        public bool AddNewItem(int userId,
            uint itemType,
            byte flag,
            int addAmount,
            int monopoly,
            int saveTime,
            int active,
            int onlineTime,
            int data,
            int reduceDamage,
            int addLife,
            int addLeveExp,
            int magic3,
            int gem1,
            int gem2,
            int magic1,
            int magic2,
            int amount,
            int amountLimit,
            int antiMonster,
            int ident,
            int color)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            DbItemtype it = ItemManager.GetItemtype(itemType);
            if (it == null)
            {
                logger.Warning("[Script: {0}] itemtype [{1}] not found.", currentScript, itemType);
                return false;
            }

            addAmount = Math.Max(1, addAmount);

            int deleteTime = 0;
            if (active != 0)
            {
                if (saveTime == 0)
                {
                    saveTime = (int)it.SaveTime;
                }
                deleteTime = UnixTimestamp.Now + saveTime * 60;
            }

            if (amount == 0)
            {
                amount = it.Amount;
            }

            if (amountLimit == 0)
            {
                amountLimit = it.AmountLimit;
            }

            if (color == 0)
            {
                color = 3;
            }

            DbItem dbItem = new DbItem
            {
                Type = itemType,
                Monopoly = (byte)monopoly,
                AccumulateNum = (uint)addAmount,
                SaveTime = (uint)saveTime,
                Data = (uint)data,
                ReduceDmg = (byte)reduceDamage,
                AddLife = (byte)addLife,
                AddlevelExp = (uint)addLeveExp,
                Magic1 = (ushort)magic1,
                Magic2 = (byte)magic2,
                Magic3 = (byte)magic3,
                Gem1 = (byte)gem1,
                Gem2 = (byte)gem2,
                Amount = (ushort)amount,
                AmountLimit = (ushort)amountLimit,
                AntiMonster = (byte)antiMonster,
                Ident = (byte)ident,
                DeleteTime = deleteTime,
                PlayerId = user.Identity,
                Color = (uint)color
            };

            Item item = new Item(user);
            if (!item.CreateAsync(dbItem).GetAwaiter().GetResult())
            {
                return false;
            }

            user.UserPackage.AddItemAsync(item).GetAwaiter().GetResult();
            return true;
        }

        [LuaFunction]
        public bool CheckMultiItem(int userId, int fromItemtype, int toItemtype, int amount, int monopoly, int sash)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.UserPackage.MultiCheckItem((uint)fromItemtype, (uint)toItemtype, amount, monopoly == 0);
        }

        [LuaFunction]
        public bool DeleteMultiItem(int userId, int fromItemtype, int toItemtype, int amount, int monopoly, int sash)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.UserPackage.MultiSpendItemAsync((uint)fromItemtype, (uint)toItemtype, amount, monopoly == 0).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public int CountItemType(int userId, int ownerId, int itemType, int type, int monopoly)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }
            return user.UserPackage.CountItemType((uint)ownerId, (uint)itemType, type, monopoly != 0);
        }

        [LuaFunction]
        public bool DelAllItemByType(int userId, uint itemType)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            return user.UserPackage.DelAllItemByTypeAsync(itemType).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool CheckItem(int userId, int itemId, int monopoly, int sash)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            bool allowBound = monopoly != 0;
            bool searchSash = sash != 0;
            return user.UserPackage.CheckItem((uint)itemId, allowBound, searchSash);
        }

        [LuaFunction]
        public int GetEquipSubType(int userId, int pos)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }
            return user.UserPackage[(Item.ItemPosition)pos]?.GetItemSubType() ?? 0;
        }

        [LuaFunction]
        public int GetLastAddItemID(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }
            // TODO
            return 0;
        }

        [LuaFunction]
        public int GetLastDelItemID(int userId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return 0;
            }
            // TODO
            return 0;
        }

        [LuaFunction]
        public bool DeleteItem(int userId, int itemId, int monopoly, int sash)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }
            bool allowBound = monopoly != 0;
            bool searchSash = sash != 0;
            return user.UserPackage.DeleteItemAsync((uint)itemId, allowBound, searchSash).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool DeleteTaskItem(int userId, int itemId)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            Item item = GetItem(itemId);
            if (item == null)
            {
                return false;
            }
            return user.UserPackage.SpendItemAsync(item, true).GetAwaiter().GetResult();
        }

        [LuaFunction]
        public bool CheckAccumulate(int userId, int itemId, int itemNum, int sash)
        {
            Character user = GetUser(userId);
            if (user == null)
            {
                return false;
            }

            return user.UserPackage.CheckAccumulate((uint)itemId, itemNum, true, sash != 0);
        }

        private DbItemtype GetItemType(int itemType)
        {
            if (itemType <= 0)
            {
                return item?.Itemtype;
            }
            else
            {
                return ItemManager.GetItemtype((uint)itemType);
            }
        }

        [LuaFunction]
        public long GetItemTypeInt(int itemType, int index)
        {
            DbItemtype it = GetItemType(itemType);
            if (it == null)
            {
                return 0;
            }

            switch (index)
            {
                case G_ITEMTYPE_Profession: return it.ReqProfession;
                case G_ITEMTYPE_Skill: return it.ReqWeaponskill;
                case G_ITEMTYPE_Level: return it.ReqLevel;
                case G_ITEMTYPE_Sex: return it.ReqSex;
                case G_ITEMTYPE_Monopoly: return it.Monopoly;
                case G_ITEMTYPE_Mask: return it.TypeMask;
                case G_ITEMTYPE_EmoneyPrice: return it.EmoneyPrice;
                case G_ITEMTYPE_EmoneyMonoPrice: return it.BoundEmoneyPrice;
                case G_ITEMTYPE_SaveTime: return it.SaveTime;
                case G_ITEMTYPE_AccumulateLimit: return it.AccumulateLimit;
                default: return 0;
            }
        }

        [LuaFunction]
        public string GetItemTypeStr(int itemType, int index)
        {
            DbItemtype it = GetItemType(itemType);
            if (it == null)
            {
                return string.Empty;
            }

            switch (index)
            {
                case G_ITEMTYPE_Name: return it.Name;
                case G_ITEMTYPE_TypeDesc: return it.TypeDesc;
                case G_ITEMTYPE_ItemDesc: return it.ItemDesc;
                default: return string.Empty;
            }
        }
    }
}
