using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.World.Enums;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteActionItemAddAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (!user.UserPackage.IsPackSpare(1, action.Data))
            {
                return false;
            }

            DbItemtype itemtype = ItemManager.GetItemtype(action.Data);
            if (itemtype == null)
            {
                logger.Warning("Invalid itemtype: {0}, {1}, {2}", action.Id, action.Type, action.Data);
                return false;
            }

            string[] splitParam = SplitParam(param);
            DbItem newItem = Item.CreateEntity(action.Data);
            newItem.PlayerId = user.Identity;

            if (item != null)
            {
                newItem.Monopoly = item.Monopoly;
            }

            bool autoActive = false;
            for (var i = 0; i < splitParam.Length; i++)
            {
                if (!int.TryParse(splitParam[i], out int value))
                {
                    continue;
                }

                switch (i)
                {
                    case 0: // amount
                        if (value > 0)
                        {
                            newItem.Amount = (ushort)Math.Min(value, ushort.MaxValue);
                        }

                        break;
                    case 1: // amount limit
                        if (value > 0)
                        {
                            newItem.AmountLimit = (ushort)Math.Min(value, ushort.MaxValue);
                        }

                        break;
                    case 2: // socket progress
                        newItem.Data = (uint)Math.Min(value, ushort.MaxValue);
                        break;
                    case 3: // gem 1
                        if (Enum.IsDefined(typeof(Item.SocketGem), (byte)value))
                        {
                            newItem.Gem1 = (byte)value;
                        }

                        break;
                    case 4: // gem 2
                        if (Enum.IsDefined(typeof(Item.SocketGem), (byte)value))
                        {
                            newItem.Gem2 = (byte)value;
                        }

                        break;
                    case 5: // effect magic 1
                        if (Enum.IsDefined(typeof(Item.ItemEffect), (ushort)value))
                        {
                            newItem.Magic1 = (byte)value;
                        }

                        break;
                    case 6: // magic 2
                        newItem.Magic2 = (byte)value;
                        break;
                    case 7: // magic 3
                        newItem.Magic3 = (byte)value;
                        break;
                    case 8: // reduce dmg
                        newItem.ReduceDmg = (byte)Math.Min(byte.MaxValue, value); // R
                        break;
                    case 9: // add life
                        newItem.AddLife = (byte)Math.Min(byte.MaxValue, value); // B
                        break;
                    case 10: // anti monster
                        newItem.AntiMonster = (byte)Math.Min(byte.MaxValue, value); // G
                        break;
                    case 11: // color
                        if (Enum.IsDefined(typeof(Item.ItemColor), (byte)value))
                        {
                            newItem.Color = (byte)value;
                        }
                        break;
                    case 12: // monopoly
                        newItem.Monopoly = (byte)Math.Min(byte.MaxValue, Math.Max(0, value));
                        break;
                    case 13: // mount color
                        newItem.Data = (uint)value;
                        break;
                    case 16: // active
                        autoActive = value != 0;
                        break;
                    case 17: // Accumulate Num
                        newItem.AccumulateNum = (uint)value;
                        break;
                    case 18: // save time
                        if (value > 0)
                        {
                            newItem.SaveTime = (uint)value;
                        }
                        else if (itemtype.SaveTime != 0)
                        {
                            newItem.SaveTime = itemtype.SaveTime;
                        }
                        break;
                }
            }

            item = new Item(user);
            if (!await item.CreateAsync(newItem))
            {
                return false;
            }

            if (autoActive && item.IsActivable())
            {
                await item.ActivateAsync();
            }

            return await user.UserPackage.AddItemAsync(item);
        }

        private static async Task<bool> ExecuteActionItemDelAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (action.Data != 0)
            {
                if (item != null && item.Type == action.Data)
                {
                    return await user.UserPackage.SpendItemAsync(item);
                }
                return await user.UserPackage.MultiSpendItemAsync(action.Data, action.Data, 1);
            }

            if (!string.IsNullOrEmpty(param))
            {
                item = user.UserPackage[param];
                if (item == null)
                {
                    return false;
                }

                return await user.UserPackage.SpendItemAsync(item);
            }

            return false;
        }

        private static async Task<bool> ExecuteActionItemCheckAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (action.Data != 0)
            {
                return user.UserPackage.MultiCheckItem(action.Data, action.Data, 1);
            }

            if (!string.IsNullOrEmpty(param))
            {
                return user.UserPackage[param] != null;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionItemHoleAsync(DbAction action, string param, Character user,
                                                                   Role role,
                                                                   Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 2);
            if (param.Length < 2)
            {
                logger.Warning($"ExecuteActionItemHole invalid [{param}] param split length for action {action.Id}");
                return false;
            }

            string opt = splitParam[0];
            if (!int.TryParse(splitParam[1], out int value))
            {
                logger.Warning($"ExecuteActionItemHole invalid value number [{param}] for action {action.Id}");
                return false;
            }

            Item target = user.UserPackage[Item.ItemPosition.RightHand];
            if (target == null)
            {
                await user.SendAsync(StrItemErrRepairItem);
                return false;
            }

            if (opt.Equals("chkhole", StringComparison.InvariantCultureIgnoreCase))
            {
                if (value == 1)
                {
                    return target.SocketOne > Item.SocketGem.NoSocket;
                }

                if (value == 2)
                {
                    return target.SocketTwo > Item.SocketGem.NoSocket;
                }

                return false;
            }

            if (opt.Equals("makehole", StringComparison.InvariantCultureIgnoreCase))
            {
                if (value == 1 && target.SocketOne == Item.SocketGem.NoSocket)
                {
                    target.SocketOne = Item.SocketGem.EmptySocket;
                }
                else if (value == 2 && target.SocketTwo == Item.SocketGem.NoSocket)
                {
                    target.SocketTwo = Item.SocketGem.EmptySocket;
                }
                else
                {
                    return false;
                }

                await user.SendAsync(new MsgItemInfo(target, MsgItemInfo.ItemMode.Update));
                await target.SaveAsync();
                return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionItemMultidelAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param);
            var first = 0;
            var last = 0;
            byte amount = 0;

            if (action.Data == Item.TYPE_METEOR)
            {
                if (!byte.TryParse(splitParams[0], out amount))
                {
                    amount = 1;
                }

                if (splitParams.Length <= 1 || !int.TryParse(splitParams[1], out first))
                {
                    first = 0; // bound check
                }
                // todo set meteor bind check
                return await user.UserPackage.SpendMeteorsAsync(amount);
            }

            if (action.Data == Item.TYPE_DRAGONBALL)
            {
                if (!byte.TryParse(splitParams[0], out amount))
                {
                    amount = 1;
                }

                if (splitParams.Length <= 1 || !int.TryParse(splitParams[1], out first))
                {
                    first = 0;
                }

                return await user.UserPackage.SpendDragonBallsAsync(amount, first != 0);
            }

            if (action.Data != 0)
            {
                return false; // only Mets and DBs are supported
            }

            if (splitParams.Length < 3)
            {
                return false; // invalid format
            }

            first = int.Parse(splitParams[0]);
            last = int.Parse(splitParams[1]);
            amount = byte.Parse(splitParams[2]);

            if (splitParams.Length < 4)
            {
                return await user.UserPackage.MultiSpendItemAsync((uint)first, (uint)last, amount, true);
            }

            return await user.UserPackage.MultiSpendItemAsync((uint)first, (uint)last, amount,
                                                              int.Parse(splitParams[3]) != 0);
        }

        private static async Task<bool> ExecuteActionItemMultichkAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param);
            int first;
            byte amount;
            if (action.Data == Item.TYPE_METEOR)
            {
                if (!byte.TryParse(splitParams[0], out amount))
                {
                    amount = 1;
                }

                if (splitParams.Length <= 1 || !int.TryParse(splitParams[1], out first))
                {
                    first = 0; // bound check
                }
                // todo set meteor bind check
                return user.UserPackage.MeteorAmount() >= amount;
            }

            if (action.Data == Item.TYPE_DRAGONBALL)
            {
                if (!byte.TryParse(splitParams[0], out amount))
                {
                    amount = 1;
                }

                if (splitParams.Length <= 1 || !int.TryParse(splitParams[1], out first))
                {
                    first = 0;
                }

                return user.UserPackage.DragonBallAmount(first != 0) >= amount;
            }

            if (action.Data != 0)
            {
                return false; // only Mets and DBs are supported
            }

            if (splitParams.Length < 3)
            {
                return false; // invalid format
            }

            first = int.Parse(splitParams[0]);
            int last = int.Parse(splitParams[1]);
            amount = byte.Parse(splitParams[2]);

            if (splitParams.Length < 4)
            {
                return user.UserPackage.MultiCheckItem((uint)first, (uint)last, amount, true);
            }

            return user.UserPackage.MultiCheckItem((uint)first, (uint)last, amount, int.Parse(splitParams[3]) != 0);
        }

        private static async Task<bool> ExecuteActionItemLeavespaceAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            return user?.UserPackage != null && user.UserPackage.IsPackSpare((int)action.Data);
        }

        private static async Task<bool> ExecuteActionItemUpequipmentAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 2)
            {
                return false;
            }

            string command = splitParam[0];
            byte pos = byte.Parse(splitParam[1]);

            Item pItem = user.UserPackage[(Item.ItemPosition)pos];
            if (pItem == null)
            {
                return false;
            }

            switch (command)
            {
                case "up_lev":
                    {
                        return await pItem.UpEquipmentLevelAsync();
                    }

                case "recover_dur":
                    {
                        var szPrice = (uint)pItem.GetRecoverDurCost();
                        return await user.SpendMoneyAsync((int)szPrice) && await pItem.RecoverDurabilityAsync();
                    }

                case "up_levultra":
                case "up_levultra2":
                    {
                        return await pItem.UpUltraEquipmentLevelAsync();
                    }

                case "up_quality":
                    {
                        return await pItem.UpItemQualityAsync();
                    }

                default:
                    logger.Warning($"ERROR: [509] [0] [{param}] not properly handled.");
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionItemEquiptestAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            /* param: position type opt value (4 quality == 9) */
            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 4)
            {
                return false;
            }

            byte position = byte.Parse(splitParam[0]);
            string command = splitParam[1];
            string opt = splitParam[2];
            int data = int.Parse(splitParam[3]);

            Item pItem = user.UserPackage[(Item.ItemPosition)position];
            if (pItem == null)
            {
                return false;
            }

            var nTestData = 0;
            switch (command)
            {
                case "level":
                    nTestData = pItem.GetLevel();
                    break;
                case "quality":
                    nTestData = pItem.GetQuality();
                    break;
                case "durability":
                    if (data == -1)
                    {
                        data = pItem.MaximumDurability / 100;
                    }

                    nTestData = pItem.MaximumDurability / 100;
                    break;
                case "max_dur":
                    {
                        if (data == -1)
                        {
                            data = pItem.Itemtype.AmountLimit / 100;
                        }
                        // TODO Kylin Gem Support
                        nTestData = pItem.MaximumDurability / 100;
                        break;
                    }

                default:
                    logger.Warning($"ACTION: EQUIPTEST error {command}");
                    return false;
            }

            if (opt == "==")
            {
                return nTestData == data;
            }

            if (opt == "<=")
            {
                return nTestData <= data;
            }

            if (opt == ">=")
            {
                return nTestData >= data;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionItemEquipexistAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);
            if (param.Length >= 1 && user.UserPackage[(Item.ItemPosition)action.Data] != null)
            {
                return user.UserPackage[(Item.ItemPosition)action.Data].GetItemSubType() == ushort.Parse(splitParam[0]);
            }

            return user.UserPackage[(Item.ItemPosition)action.Data] != null;
        }

        private static async Task<bool> ExecuteActionItemEquipcolorAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);

            if (splitParam.Length < 2)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(Item.ItemColor), byte.Parse(splitParam[1])))
            {
                return false;
            }

            Item pItem = user.UserPackage[(Item.ItemPosition)byte.Parse(splitParam[0])];
            if (pItem == null)
            {
                return false;
            }

            Item.ItemPosition pos = pItem.GetPosition();
            if (pos != Item.ItemPosition.Armor
                && pos != Item.ItemPosition.Headwear
                && (pos != Item.ItemPosition.LeftHand || pItem.GetItemSort() != Item.ItemSort.ItemsortWeaponShield))
            {
                return false;
            }

            pItem.Color = (Item.ItemColor)byte.Parse(splitParam[1]);
            await pItem.SaveAsync();
            await user.SendAsync(new MsgItemInfo(pItem, MsgItemInfo.ItemMode.Update));
            return true;
        }

        private static async Task<bool> ExecuteActionItemTransformAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 4);
            int unknown0 = int.Parse(splitParam[0]);
            int unknown1 = int.Parse(splitParam[1]);
            var transformation = uint.Parse(splitParam[2]);
            int seconds = int.Parse(splitParam[3]);
            return await user.TransformAsync(transformation, seconds, true);
        }

        private static async Task<bool> ExecuteActionItemCheckrandAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 6)
            {
                return false;
            }

            byte initValue = byte.Parse(pszParam[3]), endValue = byte.Parse(pszParam[5]);

            var lPos = new List<Item.ItemPosition>(15);

            byte pIdx = byte.Parse(pszParam[1]);

            if (initValue == 0 && pIdx == 14)
            {
                initValue = 1;
            }

            for (var i = Item.ItemPosition.EquipmentBegin; i <= Item.ItemPosition.EquipmentEnd; i++)
            {
                if (user.UserPackage[i] != null)
                {
                    if (pIdx == 14 && user.UserPackage[i].Position == Item.ItemPosition.Mount)
                    {
                        continue;
                    }

                    if (user.UserPackage[i].IsArrowSort())
                    {
                        continue;
                    }

                    switch (pIdx)
                    {
                        case 14:
                            if (user.UserPackage[i].ReduceDamage >= initValue
                                && user.UserPackage[i].ReduceDamage <= endValue)
                            {
                                continue;
                            }

                            break;
                    }

                    lPos.Add(i);
                }
            }

            byte pos = 0;

            if (lPos.Count > 0)
            {
                pos = (byte)lPos[await NextAsync(lPos.Count) % lPos.Count];
            }

            if (pos == 0)
            {
                return false;
            }

            Item pItem = user.UserPackage[(Item.ItemPosition)pos];
            if (pItem == null)
            {
                return false;
            }

            byte pPos = byte.Parse(pszParam[0]);
            string opt = pszParam[2];

            switch (pIdx)
            {
                case 14: // bless
                    user.VarData[7] = pos;
                    return true;
                default:
                    logger.Warning($"ACTION: 516: {pIdx} not handled id: {action.Id}");
                    break;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionItemModifyAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            // structure param:
            // pos  type    action  value   update
            // 1    7       ==      1       1
            // pos = Item Position
            // type = 7 Reduce Damage
            // action = Operator == or set
            // value = value lol
            // update = if the client will update live

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 5)
            {
                logger.Warning($"ACTION: incorrect param, pos type action value update, for action (id:{action.Id})");
                return false;
            }

            int pos = int.Parse(pszParam[0]);
            int type = int.Parse(pszParam[1]);
            string opt = pszParam[2];
            long cmpValue = 0;
            long value = int.Parse(pszParam[3]);
            bool update = int.Parse(pszParam[4]) > 0;

            Item updateItem = user.UserPackage[(Item.ItemPosition)pos];
            if (updateItem == null)
            {
                await user.SendAsync(StrUnableToUseItem);
                return false;
            }

            switch (type)
            {
                case 1: // itemtype
                    {
                        if (opt == "set")
                        {
                            DbItemtype itemt = ItemManager.GetItemtype((uint)value);
                            if (itemt == null)
                            {
                                // new item doesnt exist
                                logger.Warning($"ACTION: itemtype not found (type:{value}, action:{action.Id})");
                                return false;
                            }

                            if (updateItem.Type / 1000 != itemt.Type / 1000)
                            {
                                logger.Warning($"ACTION: cant change to different type (type:{updateItem.Type}, new:{value}, action:{action.Id})");
                                return false;
                            }

                            if (!await updateItem.ChangeTypeAsync(itemt.Type))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            cmpValue = updateItem.Type;
                        }
                        break;
                    }

                case 2: // owner id
                case 3: // player id
                    return false;
                case 4: // dura
                    {
                        if (opt == "set")
                        {
                            if (value > ushort.MaxValue)
                            {
                                value = ushort.MaxValue;
                            }
                            else if (value < 0)
                            {
                                value = 0;
                            }

                            updateItem.Durability = (ushort)value;
                        }
                        else
                        {
                            cmpValue = updateItem.Durability;
                        }

                        break;
                    }

                case 5: // max dura
                    {
                        if (opt == "set")
                        {
                            if (value > ushort.MaxValue)
                            {
                                value = ushort.MaxValue;
                            }
                            else if (value < 0)
                            {
                                value = 0;
                            }

                            if (value < updateItem.Durability)
                            {
                                updateItem.Durability = (ushort)value;
                            }

                            updateItem.MaximumDurability = (ushort)value;
                        }
                        else
                        {
                            cmpValue = updateItem.MaximumDurability;
                        }
                        break;
                    }

                case 6:
                case 7: // position
                    {
                        return false;
                    }

                case 8: // gem1
                    {
                        if (opt == "set")
                        {
                            updateItem.SocketOne = (Item.SocketGem)value;
                        }
                        else
                        {
                            cmpValue = (long)updateItem.SocketOne;
                        }

                        break;
                    }

                case 9: // gem2
                    {
                        if (opt == "set")
                        {
                            updateItem.SocketTwo = (Item.SocketGem)value;
                        }
                        else
                        {
                            cmpValue = (long)updateItem.SocketTwo;
                        }
                        break;
                    }

                case 10: // magic1
                    {
                        if (opt == "set")
                        {
                            if (value is < 200 or > 203)
                            {
                                return false;
                            }

                            updateItem.Effect = (Item.ItemEffect)value;
                        }
                        else
                        {
                            cmpValue = (long)updateItem.Effect;
                        }

                        break;
                    }

                case 11: // magic2
                    return false;
                case 12: // magic3
                    {
                        if (opt == "set")
                        {
                            if (value < 0)
                            {
                                value = 0;
                            }
                            else if (value > 12)
                            {
                                value = 12;
                            }

                            updateItem.ChangeAddition((byte)value);
                        }
                        else
                        {
                            cmpValue = updateItem.Plus;
                        }
                        break;
                    }

                case 13: // data
                    {
                        if (opt == "set")
                        {
                            if (value < 0)
                            {
                                value = 0;
                            }
                            else if (value > 20000)
                            {
                                value = 20000;
                            }

                            updateItem.SocketProgress = (ushort)value;
                        }
                        else
                        {
                            cmpValue = updateItem.SocketProgress;
                        }
                        break;
                    }

                case 14: // reduce damage
                    {
                        if (opt == "set")
                        {
                            if (value < 0)
                            {
                                value = 0;
                            }
                            else if (value > 7)
                            {
                                value = 7;
                            }

                            updateItem.ReduceDamage = (byte)value;
                        }
                        else
                        {
                            cmpValue = updateItem.ReduceDamage;
                        }
                        break;
                    }

                case 15: // add life
                    {
                        if (opt == "set")
                        {
                            if (value < 0)
                            {
                                value = 0;
                            }
                            else if (value > 255)
                            {
                                value = 255;
                            }

                            updateItem.Enchantment = (byte)value;
                        }
                        else
                        {
                            cmpValue = updateItem.Enchantment;
                        }
                        break;
                    }

                case 16: // anti monster
                case 17: // chk sum
                case 18: // plunder
                case 19: // special flag
                    return false;
                case 20: // color
                    {
                        if (opt == "set")
                        {
                            if (!Enum.IsDefined(typeof(Item.ItemColor), value))
                            {
                                return false;
                            }

                            updateItem.Color = (Item.ItemColor)value;
                        }
                        else
                        {
                            cmpValue = (long)updateItem.Color;
                        }
                        break;
                    }

                case 21: // add lev exp
                    {
                        if (opt == "set")
                        {
                            if (value < 0)
                            {
                                value = 0;
                            }

                            if (value > ushort.MaxValue)
                            {
                                value = ushort.MaxValue;
                            }

                            updateItem.CompositionProgress = (ushort)value;
                        }
                        else
                        {
                            cmpValue = updateItem.CompositionProgress;
                        }
                        break;
                    }
                default:
                    return false;
            }

            if ("==".Equals(opt))
            {
                return cmpValue == value;
            }
            if ("<".Equals(opt))
            {
                return cmpValue < value;
            }
            if (">".Equals(opt))
            {
                return cmpValue > value;
            }
            if (">=".Equals(opt))
            {
                return cmpValue >= value;
            }
            if ("<=".Equals(opt))
            {
                return cmpValue <= value;
            }

            await updateItem.SaveAsync();
            if (update)
            {
                await user.SendAsync(new MsgItemInfo(updateItem, MsgItemInfo.ItemMode.Update));
            }

            return true;
        }

        private static async Task<bool> ExecuteActionItemDelAllAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            Item delItem;
            while ((delItem = user.UserPackage.GetItemByType(action.Data)) != null)
            {
                await user.UserPackage.SpendItemAsync(delItem);
            }
            return true;
        }

        private static async Task<bool> ExecuteActionItemJarCreateAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (!user.UserPackage.IsPackSpare(1))
            {
                return false;
            }

            if (user.UserPackage.GetItemByType(Item.TYPE_JAR) != null)
            {
                await user.UserPackage.SpendItemAsync(user.UserPackage.GetItemByType(Item.TYPE_JAR));
            }

            DbItemtype itemtype = ItemManager.GetItemtype(action.Data);
            if (itemtype == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);

            var newItem = new DbItem
            {
                AddLife = 0,
                AddlevelExp = 0,
                AntiMonster = 0,
                ChkSum = 0,
                Color = 3,
                Data = 0,
                Gem1 = 0,
                Gem2 = 0,
                Ident = 0,
                Magic1 = 0,
                Magic2 = 0,
                ReduceDmg = 0,
                Plunder = 0,
                Specialflag = 0,
                Type = itemtype.Type,
                Position = 0,
                PlayerId = user.Identity,
                Monopoly = 0,
                Magic3 = itemtype.Magic3,
                Amount = 0,
                AmountLimit = 0
            };
            for (var i = 0; i < pszParam.Length; i++)
            {
                uint value = uint.Parse(pszParam[i]);
                if (value <= 0)
                {
                    continue;
                }

                switch (i)
                {
                    case 0:
                        newItem.Amount = (ushort)value;
                        break;
                    case 1:
                        newItem.AmountLimit = (ushort)value; //(ushort) (1 << ((ushort) value));
                        break;
                    case 2:
                        // Socket Progress
                        newItem.Data = value;
                        break;
                    case 3:
                        if (Enum.IsDefined(typeof(Item.SocketGem), (byte)value))
                        {
                            newItem.Gem1 = (byte)value;
                        }

                        break;
                    case 4:
                        if (Enum.IsDefined(typeof(Item.SocketGem), (byte)value))
                        {
                            newItem.Gem2 = (byte)value;
                        }

                        break;
                    case 5:
                        if (Enum.IsDefined(typeof(Item.ItemEffect), (ushort)value))
                        {
                            newItem.Magic1 = (byte)value;
                        }

                        break;
                    case 6:
                        // magic2.. w/e
                        break;
                    case 7:
                        if (value < 256)
                        {
                            newItem.Magic3 = (byte)value;
                        }

                        break;
                    case 8:
                        if (value < 8)
                        {
                            newItem.ReduceDmg = (byte)value;
                        }

                        break;
                    case 9:
                        if (value < 256)
                        {
                            newItem.AddLife = (byte)value;
                        }

                        break;
                    case 10:
                        newItem.Specialflag = value;
                        break;
                    case 11:
                        if (Enum.IsDefined(typeof(Item.ItemColor), value))
                        {
                            newItem.Color = (byte)value;
                        }

                        break;
                    case 12:
                        if (value < 256)
                        {
                            newItem.Monopoly = (byte)value;
                        }

                        break;
                    case 13:
                    case 14:
                    case 15:
                        // R -> For Steeds only
                        // G -> For Steeds only
                        // B -> For Steeds only
                        // G == 8 R == 16
                        newItem.Data = value | (uint.Parse(pszParam[14]) << 8) | (uint.Parse(pszParam[13]) << 16);
                        break;
                }
            }

            var createItem = new Item(user);
            if (!await createItem.CreateAsync(newItem))
            {
                return false;
            }

            await user.UserPackage.AddItemAsync(createItem);

            await user.SendAsync(new MsgInteract
            {
                Action = MsgInteract.MsgInteractType.IncreaseJar,
                SenderIdentity = user.Identity,
                TargetIdentity = user.Identity,
                MagicLevel = createItem.MaximumDurability,
                Padding = Environment.TickCount,
                Timestamp = Environment.TickCount
            });
            return true;
        }

        private static async Task<bool> ExecuteActionItemJarVerifyAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (!user.UserPackage.IsPackSpare(1))
            {
                return false;
            }

            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 2)
            {
                return false;
            }

            uint amount = uint.Parse(pszParam[1]);
            uint monster = uint.Parse(pszParam[0]);

            Item jar = user.UserPackage.GetItemByType(action.Data);
            if (jar != null && jar.MaximumDurability == monster && amount <= jar.Data)
            {
                await user.UserPackage.SpendItemAsync(jar);
                return true;
            }
            return false;
        }

        private static async Task<bool> ExecuteActionItemRefineryAddAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null || item == null)
            {
                return false;
            }

            Item minor = user.UserPackage.FindItemByIdentity(user.InteractingItem);
            if (minor == null || !minor.IsRefinery())
            {
                logger.Warning("User tried to refine a non refinery item");
                return false;
            }

            int refineryType = (int)action.Data;
            if (!ItemManager.GetQuenchInfoData(refineryType, out _))
            {
                logger.Warning("User tried to refiner an invalid type {0}", refineryType);
                return false;
            }

            string[] splitParams = SplitParam(param, 8);
            byte level = byte.Parse(splitParams[0]);
            int power1 = int.Parse(splitParams[1]);
            int unknown = int.Parse(splitParams[2]);
            int duration = int.Parse(splitParams[3]);
            int[] acceptableEquipment = new int[3];
            for (int i = 0; i < 3; i++)
            {
                acceptableEquipment[i] = int.Parse(splitParams[4 + i]);
            }
            int power2 = 0;
            if (splitParams.Length > 7)
            {
                power2 = int.Parse(splitParams[7]);
            }

            bool success = false;
            foreach (int weaponSubtype in acceptableEquipment.Where(x => x != 0))
            {
                switch (weaponSubtype.ToString().Length)
                {
                    case 1:
                        {
                            Item.ItemPosition checkPos = item.GetPosition();
                            if (item.IsWeaponTwoHand())
                            {
                                checkPos = Item.ItemPosition.LeftHand;
                            }
                            if (checkPos != (Item.ItemPosition)weaponSubtype)
                            {
                                continue;
                            }
                            success = true;
                            break;
                        }
                    case 2:
                        {
                            if (item.Type / 10000 != weaponSubtype)
                            {
                                continue;
                            }
                            success = true;
                            break;
                        }
                    case 3:
                        {
                            int itemSubtype = (int)(item.Type / 1000);
                            if (itemSubtype != weaponSubtype)
                            {
                                if (itemSubtype == 150 && itemSubtype == 151)
                                {
                                    success = true;
                                    break;
                                }
                                continue;
                            }
                            success = true;
                            break;
                        }
                    default:
                        {
                            logger.Warning("Invalid refinery weapon type for action[{0}] param[{1}]", action.Id, param);
                            return false;
                        }
                }

                if (success)
                {
                    break;
                }
            }

            if (!success)
            {
                return false;
            }

            switch (user.VipLevel)
            {
                case 1:
                case 2:
                    {
                        duration += (int)user.VipLevel * 60 * 60 * 24;
                        break;
                    }
                case 3:
                    {
                        duration += 4 * 60 * 60 * 24;
                        break;
                    }
                case 4:
                case 5:
                case 6:
                    {
                        duration += 7 * 60 * 60 * 24;
                        break;
                    }
            }

            DbItemStatus status = new()
            {
                ItemId = item.Identity,
                Level = level,
                RealSeconds = (uint)UnixTimestamp.FromDateTime(DateTime.Now.AddSeconds(duration)),
                Status = minor.Type,
                Power1 = (uint)power1,
                Power2 = (uint)power2,
                UserId = user.Identity,
                Data = (uint)refineryType
            };
            var data = await item.ItemStatus.AppendAsync(status);
            if (data == null)
            {
                return false;
            }
            item.ItemStatus.ActivateNextRefinery();
            await item.ItemStatus.SendToAsync(user);
            return true;
        }

        private static async Task<bool> ExecuteActionItemSuperFlagAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                logger.Warning($"Invalid actor for action {action.Id} 544");
                return false;
            }
            if (item == null)
            {
                logger.Warning($"Invalid item for action {action.Id} 544");
                return false;
            }

            if (user.Map == null)
            {
                logger.Warning($"Actor {user.Identity} not in map group");
                return false;
            }

            if (user.Map.IsChgMapDisable() || user.Map.IsPrisionMap())
            {
                logger.Warning("Agate not allowed");
                await user.SendAsync(StrSuperFlagNotAllowedInMap);
                return false;
            }

            await item.SendSuperFlagListAsync();
            return true;
        }

        private static async Task<bool> ExecuteActionItemWeaponRChangeSubtypeAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 2);
            if (splitParams.Length < 2)
            {
                logger.Warning($"Invalid param count for ExecuteActionItemWeaponRChangeSubtypeAsync: [pos] [newSubType]");
                return false;
            }

            Item.ItemPosition position = (Item.ItemPosition)byte.Parse(splitParams[0]);
            ushort newSubType = ushort.Parse(splitParams[1]);

            item = user.UserPackage[position];
            if (item == null)
            {
                logger.Warning($"No equipment found on position [{position}], user {user.Name}");
                return false;
            }

            uint newType = (uint)(item.Type % 1000 + newSubType * 1000);
            DbItemtype newItemtype = ItemManager.GetItemtype(newType);
            if (newItemtype == null)
            {
                logger.Warning($"Change subtype invalid new itemtype for item: {item.Type} >> {newType}");
                return false;
            }

            if (Item.GetPosition(newType) != Item.GetPosition(item.Type))
            {
                logger.Warning($"Change subtype invalid new item positon: {item.Type} >> {newType}");
                return false;
            }

            return await item.ChangeTypeAsync(newType);
        }

        private static async Task<bool> ExecuteActionItemAddSpecialAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user?.UserPackage == null)
            {
                return false;
            }

            if (!user.UserPackage.IsPackSpare(1, action.Data))
            {
                return false;
            }

            DbItemtype itemtype = ItemManager.GetItemtype(action.Data);
            if (itemtype == null)
            {
                logger.Warning($"Invalid itemtype: {action.Id}, {action.Type}, {action.Data}");
                return false;
            }

            string[] splitParam = SplitParam(param);
            bool mergeable = itemtype.AccumulateLimit > 1;
            int amount = 1;
            if (splitParam.Length > 2 && !mergeable)
            {
                amount = Math.Max(1, int.Parse(splitParam[1]));
            }

            for (int itemCounter = 0; itemCounter < amount; itemCounter++)
            {
                DbItem newItem = Item.CreateEntity(action.Data);
                newItem.PlayerId = user.Identity;

                if (item != null)
                {
                    newItem.Monopoly = item.Monopoly;
                }

                for (var i = 0; i < splitParam.Length; i++)
                {
                    if (!int.TryParse(splitParam[i], out int value))
                    {
                        continue;
                    }

                    switch (i)
                    {
                        case 0:
                            {
                                break;
                            }
                        case 1:
                            {
                                if (mergeable)
                                {
                                    newItem.AccumulateNum = (uint)value;
                                }
                                break;
                            }
                        case 2:
                            {
                                newItem.Monopoly = (byte)value;
                                break;
                            }
                        case 3:
                            {
                                if (value > 0)
                                {
                                    newItem.SaveTime = (uint)value;
                                }
                                break;
                            }
                        case 4:
                            {
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                newItem.Data = (uint)value;
                                break;
                            }
                        case 7:
                            {
                                newItem.ReduceDmg = (byte)value;
                                break;
                            }
                        case 8:
                            {
                                newItem.AddLife = (byte)value;
                                break;
                            }
                        case 9:
                            {
                                newItem.AntiMonster = (byte)value;
                                break;
                            }
                        case 10:
                            {
                                newItem.Magic3 = (byte)value;
                                break;
                            }
                    }
                }

                item = new Item(user);
                if (!await item.CreateAsync(newItem))
                {
                    break;
                }

                if (item.IsActivable())
                {
                    await item.ActivateAsync();
                }

                await user.UserPackage.AddItemAsync(item);
            }
            return true;
        }

        private static async Task<bool> ExecuteActionItemRequestlaynpcAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 5);

            uint idNextTask = uint.Parse(splitParams[0]);
            uint dwType = uint.Parse(splitParams[1]);
            uint dwSort = uint.Parse(splitParams[2]);
            uint dwLookface = uint.Parse(splitParams[3]);
            uint dwRegion = 0;

            if (splitParams.Length > 4)
            {
                uint.TryParse(splitParams[4], out dwRegion);
            }

            if (idNextTask != 0)
            {
                user.InteractingItem = idNextTask;
            }

            await user.SendAsync(new MsgNpc
            {
                Identity = dwRegion,
                Data = dwLookface,
                Event = (ushort)dwType,
                RequestType = MsgNpc.NpcActionType.LayNpc
            });
            return true;
        }

        private static async Task<bool> ExecuteActionItemCountnpcAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user?.Map == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 4);
            if (splitParam.Length != 4)
            {
                return false;
            }

            string field = splitParam[0];
            string data = splitParam[1];
            string opt = splitParam[2];
            int num = int.Parse(splitParam[3]);

            int count = 0;
            if (field.Equals("all"))
            {
                count = user.Map.QueryRoles().Count;
            }
            else if (field.Equals("furniture"))
            {
                count = user.Map.QueryRoles(x => x is BaseNpc npc && (npc.Type == BaseNpc.ROLE_FURNITURE_NPC && npc.Type == BaseNpc.ROLE_3DFURNITURE_NPC)).Count;
            }
            else if (field.Equals("name"))
            {
                count = user.Map.QueryRoles(x => x is BaseNpc && x.Name.Equals(data)).Count;
            }
            else if (field.Equals("type"))
            {
                count = user.Map.QueryRoles(x => x is BaseNpc npc && npc.Type == int.Parse(data)).Count;
            }
            else return false;

            switch (opt)
            {
                case "==": return count == num;
                case ">=": return count >= num;
                case "<=": return count <= num;
                case ">": return count > num;
                case "<": return count < num;
            }
            return false;
        }

        private static async Task<bool> ExecuteActionItemLaynpcAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string input = inputs[0];
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            string[] splitParam = SplitParam(input, 5);
            if (splitParam.Length < 3)
            {
                logger.Warning($"Invalid input count for action [{action.Id}]: {input}");
                return false;
            }

            if (!ushort.TryParse(splitParam[0], out ushort mapX)
                || !ushort.TryParse(splitParam[1], out ushort mapY)
                || !uint.TryParse(splitParam[2], out uint lookface))
            {
                logger.Warning($"Invalid input params for action [{action.Id}]1: {input}");
                return false;
            }

            uint frame = 0;
            uint pose = 0;
            if (splitParam.Length >= 4)
            {
                uint.TryParse(splitParam[3], out frame);
                uint.TryParse(splitParam[4], out pose);
            }

            if (user.Map.IsSuperPosition(mapX, mapY))
            {
                await user.SendAsync(StrLayNpcSuperPosition);
                return false;
            }

            splitParam = SplitParam(param, 21);

            if (param.Length < 5)
            {
                return false;
            }

            uint nRegionType = 0;
            string szName = splitParam[0];
            ushort usType = ushort.Parse(splitParam[1]);
            ushort usSort = ushort.Parse(splitParam[2]);
            uint dwOwnerType = uint.Parse(splitParam[4]);
            uint dwLife = 0;
            uint idBase = 0;
            uint idLink = 0;
            uint idTask0 = 0;
            uint idTask1 = 0;
            uint idTask2 = 0;
            uint idTask3 = 0;
            uint idTask4 = 0;
            uint idTask5 = 0;
            uint idTask6 = 0;
            uint idTask7 = 0;
            var idData0 = 0;
            var idData1 = 0;
            var idData2 = 0;
            var idData3 = 0;

            if (splitParam.Length >= 6)
            {
                dwLife = uint.Parse(splitParam[5]);
            }

            if (splitParam.Length >= 7)
            {
                nRegionType = uint.Parse(splitParam[6]);
            }

            if (splitParam.Length >= 8)
            {
                idBase = uint.Parse(splitParam[7]);
            }

            if (splitParam.Length >= 9)
            {
                idLink = uint.Parse(splitParam[8]);
            }

            if (splitParam.Length >= 10)
            {
                idTask0 = uint.Parse(splitParam[9]);
            }

            if (splitParam.Length >= 11)
            {
                idTask1 = uint.Parse(splitParam[10]);
            }

            if (splitParam.Length >= 12)
            {
                idTask2 = uint.Parse(splitParam[11]);
            }

            if (splitParam.Length >= 13)
            {
                idTask3 = uint.Parse(splitParam[12]);
            }

            if (splitParam.Length >= 14)
            {
                idTask4 = uint.Parse(splitParam[13]);
            }

            if (splitParam.Length >= 15)
            {
                idTask5 = uint.Parse(splitParam[14]);
            }

            if (splitParam.Length >= 16)
            {
                idTask6 = uint.Parse(splitParam[15]);
            }

            if (splitParam.Length >= 17)
            {
                idTask7 = uint.Parse(splitParam[16]);
            }

            if (splitParam.Length >= 18)
            {
                idData0 = int.Parse(splitParam[17]);
            }

            if (splitParam.Length >= 19)
            {
                idData1 = int.Parse(splitParam[18]);
            }

            if (splitParam.Length >= 20)
            {
                idData2 = int.Parse(splitParam[19]);
            }

            if (splitParam.Length >= 21)
            {
                idData3 = int.Parse(splitParam[20]);
            }

            if (usType == BaseNpc.SYNTRANS_NPC && user.Map.IsTeleportDisable())
            {
                await user.SendAsync(StrLayNpcSynTransInvalidMap);
                return false;
            }

            if (usType == BaseNpc.ROLE_STATUARY_NPC)
            {
                szName = user.Name;
                lookface = user.Mesh % 10;
                idTask0 = user.Headgear?.Type ?? 0;
                idTask1 = user.Armor?.Type ?? 0;
                idTask2 = user.RightHand?.Type ?? 0;
                idTask3 = user.LeftHand?.Type ?? 0;
                idTask4 = frame;
                idTask5 = pose;
                idTask6 = user.Mesh;
                idTask7 = ((uint)user.SyndicateRank << 16) + user.Hairstyle;
            }

            if (nRegionType > 0 && !user.Map.QueryRegion((RegionType)nRegionType, mapX, mapY))
            {
                return false;
            }

            uint idOwner = 0;
            switch (dwOwnerType)
            {
                case 1:
                    if (user.Identity == 0)
                    {
                        return false;
                    }

                    idOwner = user.Identity;
                    break;
                case 2:
                    if (user.SyndicateIdentity == 0)
                    {
                        return false;
                    }

                    idOwner = user.SyndicateIdentity;
                    break;
            }

            DynamicNpc npc;
            if (usType != 15)
            {
                npc = new DynamicNpc(new DbDynanpc
                {
                    Name = szName,
                    Ownerid = idOwner,
                    OwnerType = dwOwnerType,
                    Type = usType,
                    Sort = usSort,
                    Life = dwLife,
                    Maxlife = dwLife,
                    Base = idBase,
                    Linkid = idLink,
                    Task0 = idTask0,
                    Task1 = idTask1,
                    Task2 = idTask2,
                    Task3 = idTask3,
                    Task4 = idTask4,
                    Task5 = idTask5,
                    Task6 = idTask6,
                    Task7 = idTask7,
                    Data0 = idData0,
                    Data1 = idData1,
                    Data2 = idData2,
                    Data3 = idData3,
                    Datastr = "",
                    Defence = 0,
                    Cellx = mapX,
                    Celly = mapY,
                    Idxserver = 0,
                    Itemid = 0,
                    Lookface = (ushort)lookface,
                    MagicDef = 0,
                    Mapid = user.MapIdentity
                });

                if (!await npc.InitializeAsync())
                {
                    return false;
                }
            }
            else
            {
                npc = RoleManager.QueryRoleByType<DynamicNpc>().FirstOrDefault(x => x.LinkId == idLink);
                npc.SetType(usType);
                npc.OwnerIdentity = idOwner;
                npc.OwnerType = (byte)dwOwnerType;
                await npc.SetOwnerAsync(idOwner);
                await npc.SetAttributesAsync(ClientUpdateType.Mesh, lookface);
                npc.SetSort(usSort);
                npc.SetTask(0, idTask0);
                npc.SetTask(1, idTask1);
                npc.SetTask(2, idTask2);
                npc.SetTask(3, idTask3);
                npc.SetTask(4, idTask4);
                npc.SetTask(5, idTask5);
                npc.SetTask(6, idTask6);
                npc.SetTask(7, idTask7);
                npc.Data0 = idData0;
                npc.Data1 = idData1;
                npc.Data2 = idData2;
                npc.Data3 = idData3;
                await npc.SetAttributesAsync(ClientUpdateType.TeamMemberMaxHP, dwLife);
                npc.X = mapX;
                npc.Y = mapY;
            }

            /**
             * Reminder:
             * This action will not be queued! This requires user action and packets are processed in map queue.
             */
            await npc.SaveAsync();
            await npc.ChangePosAsync(user.MapIdentity, mapX, mapY);

            role = npc;
            user.InteractingNpc = npc.Identity;
            return true;
        }

        private static async Task<bool> ExecuteActionItemDelthisAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            user.InteractingItem = 0;
            if (item != null)
            {
                _ = user.UserPackage.SpendItemAsync(item);
            }
            _ = user.SendAsync(StrUseItem);
            return true;
        }
    }
}
