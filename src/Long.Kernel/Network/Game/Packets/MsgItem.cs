using Long.Network.Packets;
using Long.Kernel.States.Items;
using System.Drawing;
using Long.Kernel.States.User;
using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Database.Repositories;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.World;
using Long.Shared.Helpers;
using Long.Kernel.States;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgItem : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgItem>();
        private static readonly ILogger shopPurchaseLogger = Logger.CreateLogger("shop_purchase");
        private static readonly ILogger shopEnchantItem = Logger.CreateLogger("item_enchant");
        private static readonly ILogger blessItemLogger = Logger.CreateLogger("item_blessed");

        public MsgItem()
        {
        }

        public MsgItem(uint identity, ItemActionType action, uint cmd = 0, uint param = 0)
        {
            Identity = identity;
            Command = cmd;
            Action = action;
            Timestamp = (uint)Environment.TickCount;
            Argument = param;
        }

        // Packet Properties
        public int Padding { get; set; }
        public uint Identity { get; set; }
        public uint Command { get; set; }
        public uint Data { get; set; }
        public uint Timestamp { get; set; }
        public uint Argument { get; set; } // ??? Count
        public ItemActionType Action { get; set; }
        public uint Argument2 { get; set; }
        public byte MoneyType { get; set; }
        public uint Headgear { get; set; }
        public uint Necklace { get; set; }
        public uint Armor { get; set; }
        public uint RightHand { get; set; }
        public uint LeftHand { get; set; }
        public uint Ring { get; set; }
        public uint Talisman { get; set; }
        public uint Boots { get; set; }
        public uint Garment { get; set; }
        public uint RightAccessory { get; set; }
        public uint LeftAccessory { get; set; }
        public uint MountArmor { get; set; }
        public uint Crop { get; set; }
        public uint Wings { get; set; }
        public List<uint> Consumables { get; } = new List<uint>();

        /// <summary>
        ///     Decodes a byte packet into the packet structure defined by this message class.
        ///     Should be invoked to structure data from the client for processing. Decoding
        ///     follows TQ Digital's byte ordering rules for an all-binary protocol.
        /// </summary>
        /// <param name="bytes">Bytes from the packet processor or client socket</param>
        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Padding = reader.ReadInt32(); // 4
            Identity = reader.ReadUInt32(); // 8
            Command = reader.ReadUInt32(); // 12
            Data = reader.ReadUInt32(); // 16
            Action = (ItemActionType)reader.ReadUInt16(); // 20
            Timestamp = reader.ReadUInt32(); // 22
            Argument = reader.ReadUInt32(); // 26
            Argument2 = reader.ReadUInt32(); // 30
            MoneyType = reader.ReadByte(); // 34
            Headgear = reader.ReadUInt32(); // 35
            Necklace = reader.ReadUInt32(); // 39
            Armor = reader.ReadUInt32(); // 43
            RightHand = reader.ReadUInt32(); // 47
            LeftHand = reader.ReadUInt32(); // 51
            Ring = reader.ReadUInt32(); // 55
            Talisman = reader.ReadUInt32(); // 59
            Boots = reader.ReadUInt32(); // 63
            Garment = reader.ReadUInt32(); // 67
            RightAccessory = reader.ReadUInt32(); // 71
            LeftAccessory = reader.ReadUInt32(); // 75
            MountArmor = reader.ReadUInt32(); // 79
            Crop = reader.ReadUInt32(); // 83
            Wings = reader.ReadUInt32(); // 87
            switch (Action)
            {
                case ItemActionType.TalismanProgress:
                case ItemActionType.GemCompose:
                case ItemActionType.TortoiseCompose:
                case ItemActionType.SocketEquipment:
                    {
                        for (int i = 0; i < Argument; i++)
                        {
                            Consumables.Add(reader.ReadUInt32());
                        }
                        break;
                    }
            }
        }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgItem);
            writer.Write(Environment.TickCount);
            writer.Write(Identity); // 8
            writer.Write(Command); // 12
            writer.Write(Data); // 16
            writer.Write((ushort)Action); // 20
            writer.Write(Timestamp); // 22
            writer.Write(Argument); // 26
            writer.Write(Argument2); // 30
            writer.Write(MoneyType); // 34
            writer.Write(Headgear); // 35
            writer.Write(Necklace); // 39
            writer.Write(Armor); // 43
            writer.Write(RightHand); // 47
            writer.Write(LeftHand); // 51
            writer.Write(Ring); // 55
            writer.Write(Talisman); // 59
            writer.Write(Boots); // 63
            writer.Write(Garment); // 67
            writer.Write(RightAccessory); // 71
            writer.Write(LeftAccessory); // 75
            writer.Write(MountArmor); // 79
            writer.Write(Crop); // 83
            writer.Write(Wings); // 87
            foreach (var id in Consumables)
            {
                writer.Write(id);
            }
            return writer.ToArray();
        }

        /// <summary>
        ///     Enumeration type for defining item actions that may be requested by the user,
        ///     or given to by the server. Allows for action handling as a packet subtype.
        ///     Enums should be named by the action they provide to a system in the context
        ///     of the player item.
        /// </summary>
        public enum ItemActionType
        {
            ShopPurchase = 1,
            ShopSell,
            InventoryRemove,
            InventoryEquip,
            EquipmentWear,
            EquipmentRemove,
            EquipmentSplit,
            EquipmentCombine,
            BankQuery,
            BankDeposit,
            BankWithdraw,
            EquipmentRepair = 14,
            EquipmentRepairAll,
            EquipmentImprove = 19,
            EquipmentLevelUp,
            BoothQuery,
            BoothSell,
            BoothRemove,
            BoothPurchase,
            EquipmentAmount,
            Fireworks,
            ClientPing = 27,
            EquipmentEnchant,
            BoothEmoneySell,
            RedeemEquipment = 32,
            DetainEquipment = 33,
            DetainRewardClose = 34,
            TalismanProgress = 35,
            TalismanProgressEmoney = 36,
            InventoryDropItem = 37,
            InventoryDropSilver = 38,
            GemCompose = 39,
            TortoiseCompose = 40,
            ActivateAccessory = 41,
            SocketEquipment = 43,
            MainEquipment = 44,
            AlternativeEquipment = 45,
            DisplayGears = 46,
            MergeItems = 48,
            SplitItems = 49,
            ComposeRefinedTortoiseGem = 51,
            RequestItemTooltip = 52,
            DegradeEquipment = 54,
            ForgingBuy = 55,
            MergeSash = 56
        }

        public enum Moneytype
        {
            Silver,
            ConquerPoints,

            /// <summary>
            ///     CPs(B)
            /// </summary>
            ConquerPointsMono
        }

        public void Append(Item.ItemPosition pos, uint id)
        {
            switch (pos)
            {
                case Item.ItemPosition.Headwear:
                    Headgear = id;
                    break;
                case Item.ItemPosition.Necklace:
                    Necklace = id;
                    break;
                case Item.ItemPosition.Ring:
                    Ring = id;
                    break;
                case Item.ItemPosition.RightHand:
                    RightHand = id;
                    break;
                case Item.ItemPosition.LeftHand:
                    LeftHand = id;
                    break;
                case Item.ItemPosition.Armor:
                    Armor = id;
                    break;
                case Item.ItemPosition.Boots:
                    Boots = id;
                    break;
                case Item.ItemPosition.MountArmor:
                    MountArmor = id;
                    break;
                case Item.ItemPosition.Crop:
                    Crop = id;
                    break;
                case Item.ItemPosition.Gourd:
                    Talisman = id;
                    break;
                case Item.ItemPosition.RightHandAccessory:
                    RightAccessory = id;
                    break;
                case Item.ItemPosition.LeftHandAccessory:
                    LeftAccessory = id;
                    break;
                case Item.ItemPosition.Garment:
                    Garment = id;
                    break;
                case Item.ItemPosition.Wing:
                    Wings = id;
                    break;
            }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            BaseNpc npc;
            Item item;

            if (user.CurrentServer.HasValue && user.CurrentServerID != RealmManager.ServerIdentity)
            {
                switch (Action)
                {
                    case ItemActionType.ClientPing:
                        {
                            await user.SendCrossMsgAsync(this);
                            return;
                        }
                }
            }

            switch (Action)
            {
                case ItemActionType.ShopPurchase:
                case ItemActionType.ForgingBuy:
                case ItemActionType.ShopSell:
                case ItemActionType.BankWithdraw:
                case ItemActionType.BoothSell:
                case ItemActionType.TalismanProgress:
                case ItemActionType.TalismanProgressEmoney:
                case ItemActionType.InventoryDropItem:
                case ItemActionType.InventoryRemove:
                case ItemActionType.InventoryDropSilver:
                    {
                        if (!user.IsUnlocked())
                        {
                            await user.SendSecondaryPasswordInterfaceAsync();
                            return;
                        }
                        break;
                    }
            }

            switch (Action)
            {
                case ItemActionType.ShopPurchase: // 1
                case ItemActionType.ForgingBuy: // 55
                    {
                        int[] remoteShopping =
                        {
                            2888, 6000, 6001, 6002, 6003
                        };

                        npc = user.Map.QueryRole<BaseNpc>(Identity);
                        if (npc == null)
                        {
                            npc = RoleManager.GetRole<BaseNpc>(Identity);
                            if (npc == null)
                            {
                                return;
                            }

                            if (npc.MapIdentity != GameMap.NPC_JAIL_ID && npc.MapIdentity != user.MapIdentity)
                            {
                                return;
                            }
                        }

                        if (npc.MapIdentity != GameMap.NPC_JAIL_ID 
                            && remoteShopping.All(x => x != npc.Identity) 
                            && npc.GetDistance(user) > Screen.VIEW_SIZE)
                        {
                            return;
                        }

                        DbGoods goods = npc.ShopGoods.FirstOrDefault(x => x.Itemtype == Command);
                        if (goods == null)
                        {
                            logger.Warning("Invalid goods itemtype {0} for Shop {1}", Command, Identity);
                            return;
                        }

                        DbItemtype itemtype = ItemManager.GetItemtype(Command);
                        if (itemtype == null)
                        {
                            logger.Warning("Invalid goods itemtype (not existent) {0} for Shop {1}", Command, Identity);
                            return;
                        }

                        var amount = (int)Math.Max(1, (int)Argument);
                        if (!user.UserPackage.IsPackSpare(amount, itemtype.Type))
                        {
                            await user.SendAsync(StrYourBagIsFull);
                            return;
                        }

                        int price;
                        string moneyTypeString = ((Moneytype)goods.Moneytype).ToString();
                        const byte MONOPOLY_NONE_B = 0;
                        const byte MONOPOLY_BOUND_B = Item.ITEM_MONOPOLY_MASK;
                        byte monopoly = MONOPOLY_NONE_B;
                        switch ((Moneytype)goods.Moneytype)
                        {
                            case Moneytype.Silver:
                                {
                                    if (goods.HonorPrice != 0)
                                    {
                                        price = (int)goods.HonorPrice;
                                        //if (user.HonorPoints < goods.HonorPrice)
                                        //{
                                        //    return;
                                        //}

                                        monopoly = MONOPOLY_BOUND_B;
                                        //user.HonorPoints -= goods.HonorPrice;
                                        //await user.SendAsync(new MsgAthleteShop(user.HonorPoints, user.HistoryHonorPoints));
                                        moneyTypeString = "HonorPoints";
                                    }
                                    else if (goods.RidingPrice != 0)
                                    {
                                        price = (int)goods.RidingPrice;
                                        //if (!await user.SpendHorseRacePointsAsync(price))
                                        //{
                                        //    return;
                                        //}
                                        moneyTypeString = "RidingPoints";
                                    }
                                    else if (goods.GoldenLeaguePrice != 0)
                                    {
                                        price = (int)goods.GoldenLeaguePrice;
                                        //if (!await user.SpendGoldenLeaguePointsAsync(price))
                                        //{
                                        //    return;
                                        //}
                                        moneyTypeString = "GoldenLeaguePoints";
                                    }
                                    else
                                    {
                                        if (itemtype.Price == 0)
                                        {
                                            return;
                                        }

                                        price = (int)(itemtype.Price * amount);
                                        if (!await user.SpendMoneyAsync(price, true))
                                        {
                                            return;
                                        }
                                    }
                                    break;
                                }

                            case Moneytype.ConquerPoints:
                                {
                                    if (MoneyType == 1)
                                    {
                                        if (itemtype.EmoneyPrice == 0)
                                        {
                                            return;
                                        }

                                        price = (int)(itemtype.EmoneyPrice * amount);
                                        if (!await user.SpendConquerPointsAsync(price, true))
                                        {
                                            return;
                                        }
                                        await user.SaveEmoneyLogAsync(Character.EmoneyOperationType.Npc, 0, 0, (uint)price);
                                    }
                                    else if (MoneyType == 2)
                                    {
                                        if (itemtype.BoundEmoneyPrice == 0)
                                        {
                                            return;
                                        }

                                        price = (int)(itemtype.BoundEmoneyPrice * amount);
                                        if (!await user.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.EmoneyShop, price, true))
                                        {
                                            return;
                                        }

                                        monopoly = MONOPOLY_BOUND_B;
                                        moneyTypeString += "(B)";
                                    }
                                    else
                                    {
                                        logger.Warning("Invalid money type {0}", MoneyType);
                                        return;
                                    }
                                    break;
                                }

                            default:
                                {
                                    logger.Warning("Invalid moneytype {0}/{1}/{2} - {3}({4})", (Moneytype)Argument, Identity, Command, user.Identity, user.Name);
                                    return;
                                }
                        }

                        await user.UserPackage.AwardItemAsync(itemtype.Type, amount, monopoly != 0);

                        shopPurchaseLogger.Information($"Purchase,{user.Identity},{user.Name},{user.Level},{user.MapIdentity},{user.X},{user.Y},{goods.OwnerIdentity},{goods.Itemtype},{goods.Moneytype},{moneyTypeString},{amount},{price}");
                        break;
                    }

                case ItemActionType.InventoryDropItem: // 37
                case ItemActionType.InventoryRemove: // 3
                    {
                        await user.DropItemAsync(Identity, user.X, user.Y);
                        break;
                    }

                case ItemActionType.InventoryDropSilver: // 38
                    {
                        await user.DropSilverAsync(Identity);
                        break;
                    }

                case ItemActionType.InventoryEquip: // 4
                case ItemActionType.EquipmentWear: // 5
                    {
                        if (!await user.UserPackage.UseItemAsync(Identity, (Item.ItemPosition)Command))
                        {
                            await user.SendAsync(StrUnableToUseItem, TalkChannel.TopLeft, Color.Red);
                        }
                        break;
                    }

                case ItemActionType.EquipmentRemove: // 6
                    {
                        if (!await user.UserPackage.UnEquipAsync((Item.ItemPosition)Command))
                        {
                            await user.SendAsync(StrYourBagIsFull, TalkChannel.TopLeft, Color.Red);
                        }

                        break;
                    }

                case ItemActionType.EquipmentCombine: // 8
                    {
                        item = user.UserPackage.GetInventory(Identity);
                        Item target = user.UserPackage.GetInventory(Command);
                        if (item.IsArrowSort() && target.IsArrowSort())
                        {
                            await user.UserPackage.CombineArrowAsync(item, target);
                        }
                        break;
                    }

                case ItemActionType.BankQuery: // 9
                    {
                        Command = user.StorageMoney;
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.BankDeposit: // 10
                    {
                        if (user.Silvers < Command)
                        {
                            return;
                        }

                        if (Command + (long)user.StorageMoney > Role.MAX_STORAGE_MONEY)
                        {
                            await user.SendAsync(string.Format(StrSilversExceedAmount, int.MaxValue));
                            return;
                        }

                        if (!await user.SpendMoneyAsync((int)Command, true))
                        {
                            return;
                        }

                        user.StorageMoney += Command;

                        Action = ItemActionType.BankQuery;
                        Command = user.StorageMoney;
                        await user.SendAsync(this);
                        await user.SaveAsync();
                        break;
                    }

                case ItemActionType.BankWithdraw: // 11
                    {
                        if (Command > user.StorageMoney)
                        {
                            return;
                        }

                        if (Command + user.Silvers > int.MaxValue)
                        {
                            await user.SendAsync(string.Format(StrSilversExceedAmount, int.MaxValue));
                            return;
                        }

                        user.StorageMoney -= Command;

                        await user.AwardMoneyAsync((int)Command);

                        Action = ItemActionType.BankQuery;
                        Command = user.StorageMoney;
                        await user.SendAsync(this);
                        await user.SaveAsync();
                        break;
                    }

                case ItemActionType.EquipmentRepair: // 14
                    {
                        item = user.UserPackage.FindItemByIdentity(Identity);
                        if (item != null && item.Position == Item.ItemPosition.Inventory)
                        {
                            await item.RepairItemAsync();
                        }
                        break;
                    }

                case ItemActionType.EquipmentRepairAll: // 15
                    {
                        if (user.VipLevel < 2)
                        {
                            return;
                        }

                        for (Item.ItemPosition pos = Item.ItemPosition.EquipmentBegin;
                            pos <= Item.ItemPosition.EquipmentEnd;
                            pos++)
                        {
                            item = user.GetEquipment(pos);
                            if (item != null && user.UserPackage.TryItem(item.Identity, item.Position))
                            {
                                await item.RepairItemAsync();
                            }
                            item = null;
                        }

                        break;
                    }

                case ItemActionType.ClientPing: // 27
                    {
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.EquipmentEnchant: // 28
                    {
                        item = user.UserPackage.FindItemByIdentity(Identity);
                        Item gem = user.UserPackage.GetInventory(Command);

                        if (item == null || gem == null)
                        {
                            return;
                        }

                        if (item.Durability / 100 != item.MaximumDurability / 100)
                        {
                            await user.SendAsync(StrItemErrRepairItem);
                            return;
                        }

                        if (item.IsSuspicious())
                        {
                            return;
                        }

                        if (item.Enchantment >= byte.MaxValue)
                        {
                            return;
                        }

                        if (!gem.IsGem())
                        {
                            return;
                        }

                        await user.UserPackage.SpendItemAsync(gem);

                        byte min, max;
                        switch ((Item.SocketGem)(gem.Type % 1000))
                        {
                            case Item.SocketGem.NormalPhoenixGem:
                            case Item.SocketGem.NormalDragonGem:
                            case Item.SocketGem.NormalFuryGem:
                            case Item.SocketGem.NormalKylinGem:
                            case Item.SocketGem.NormalMoonGem:
                            case Item.SocketGem.NormalTortoiseGem:
                            case Item.SocketGem.NormalVioletGem:
                                min = 1;
                                max = 59;
                                break;
                            case Item.SocketGem.RefinedPhoenixGem:
                            case Item.SocketGem.RefinedVioletGem:
                            case Item.SocketGem.RefinedMoonGem:
                                min = 60;
                                max = 109;
                                break;
                            case Item.SocketGem.RefinedFuryGem:
                            case Item.SocketGem.RefinedKylinGem:
                            case Item.SocketGem.RefinedTortoiseGem:
                                min = 40;
                                max = 89;
                                break;
                            case Item.SocketGem.RefinedDragonGem:
                                min = 100;
                                max = 159;
                                break;
                            case Item.SocketGem.RefinedRainbowGem:
                                min = 80;
                                max = 129;
                                break;
                            case Item.SocketGem.SuperPhoenixGem:
                            case Item.SocketGem.SuperTortoiseGem:
                            case Item.SocketGem.SuperRainbowGem:
                                min = 170;
                                max = 229;
                                break;
                            case Item.SocketGem.SuperVioletGem:
                            case Item.SocketGem.SuperMoonGem:
                                min = 140;
                                max = 199;
                                break;
                            case Item.SocketGem.SuperDragonGem:
                                min = 200;
                                max = 255;
                                break;
                            case Item.SocketGem.SuperFuryGem:
                                min = 90;
                                max = 149;
                                break;
                            case Item.SocketGem.SuperKylinGem:
                                min = 70;
                                max = 119;
                                break;
                            default:
                                return;
                        }

                        byte enchant = (byte)await NextAsync(min, max);
                        if (enchant > item.Enchantment)
                        {
                            item.Enchantment = enchant;
                            await item.SaveAsync();
                            shopEnchantItem.Information($"User[{user.Identity}] Enchant[Gem: {gem.Type}|{gem.Identity}][Target: {item.Type}|{item.Identity}] with {enchant} points.");
                        }
                        else
                        {
                            shopEnchantItem.Information($"User[{user.Identity}] Enchant[Gem: {gem.Type}|{gem.Identity}][Target: {item.Type}|{item.Identity}] failed for [new:{enchant}/old:{item.Enchantment}].");
                        }

                        Command = enchant;
                        await user.SendAsync(this);
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        break;
                    }

                case ItemActionType.TalismanProgress:
                    {
                        item = user.UserPackage.FindItemByIdentity(Identity);

                        if (item == null)
                        {
                            return;
                        }

                        if (item.Durability / 100 != item.MaximumDurability / 100)
                        {
                            await user.SendAsync(StrItemErrRepairItem);
                            return;
                        }

                        if (!item.IsTalisman())
                        {
                            return;
                        }

                        foreach (var idItem in Consumables)
                        {
                            Item target = user.UserPackage.GetInventory(idItem);
                            if (target == null || target.Position != Item.ItemPosition.Inventory)
                            {
                                continue;
                            }

                            if (target.IsBound && !item.IsBound)
                            {
                                continue;
                            }

                            if (target.IsTalisman() || target.IsMount() || !target.IsEquipment())
                            {
                                continue;
                            }

                            if (target.GetQuality() < 6)
                            {
                                continue;
                            }

                            item.SocketProgress += target.CalculateSocketProgress();
                            await user.UserPackage.RemoveFromInventoryAsync(target, UserPackage.RemovalType.Delete);
                            if (item.SocketOne == Item.SocketGem.NoSocket && item.SocketProgress >= 8000)
                            {
                                item.SocketProgress = 0;
                                item.SocketOne = Item.SocketGem.EmptySocket;
                            }
                            else if (item.SocketOne != Item.SocketGem.NoSocket && item.SocketTwo == Item.SocketGem.NoSocket && item.SocketProgress >= 20000)
                            {
                                item.SocketProgress = 0;
                                item.SocketTwo = Item.SocketGem.EmptySocket;
                                break;
                            }
                        }

                        await item.SaveAsync();
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.TalismanProgressEmoney:
                    {
                        item = user.UserPackage.GetEquipmentById(Identity);

                        if (item == null)
                        {
                            return;
                        }

                        if (item.Durability / 100 != item.MaximumDurability / 100)
                        {
                            await user.SendAsync(StrItemErrRepairItem);
                            return;
                        }

                        if (item.SocketOne == Item.SocketGem.NoSocket)
                        {
                            if (item.SocketProgress < 2400)
                            {
                                return;
                            }

                            if (!await user.SpendConquerPointsAsync((int)(5600 * (1 - item.SocketProgress / 8000f)), true))
                            {
                                return;
                            }

                            item.SocketProgress = 0;
                            item.SocketOne = Item.SocketGem.EmptySocket;
                        }
                        else if (item.SocketOne != Item.SocketGem.NoSocket && item.SocketTwo == Item.SocketGem.NoSocket)
                        {
                            if (item.SocketProgress < 2400)
                            {
                                return;
                            }

                            if (!await user.SpendConquerPointsAsync((int)(14000 * (1 - item.SocketProgress / 20000f)), true))
                            {
                                return;
                            }

                            item.SocketProgress = 0;
                            item.SocketTwo = Item.SocketGem.EmptySocket;
                        }

                        await item.SaveAsync();
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.TortoiseCompose:
                    {
                        item = user.UserPackage.FindItemByIdentity(Identity);

                        if (item == null)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        if (item.Durability / 100 != item.MaximumDurability / 100)
                        {
                            await user.SendAsync(this);
                            await user.SendAsync(StrItemErrRepairItem);
                            return;
                        }

                        bool isWeapon = item.IsWeapon();
                        bool isEquipment = item.IsHelmet() || item.IsNeck() || item.IsRing() || item.IsBangle() || item.IsArmor() || item.IsShoes() || item.IsShield();

                        if (!isWeapon && !isEquipment)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        string effect;
                        byte nextBless;
                        int consumeNum;
                        if (item.ReduceDamage < 1)
                        {
                            effect = "Aegis1";
                            consumeNum = 5;
                            nextBless = 1;
                        }
                        else if (item.ReduceDamage < 3)
                        {
                            effect = "Aegis2";
                            consumeNum = 1;
                            nextBless = 3;
                        }
                        else if (item.ReduceDamage < 5)
                        {
                            effect = "Aegis3";
                            consumeNum = 3;
                            nextBless = 5;
                        }
                        else if (item.ReduceDamage < 7)
                        {
                            effect = "Aegis4";
                            consumeNum = 5;
                            nextBless = 7;
                        }
                        else
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        List<Item> usable = new();
                        foreach (var consumableId in Consumables)
                        {
                            Item consumable = user.UserPackage.GetInventory(consumableId);
                            if (consumable == null)
                            {
                                await user.SendAsync(this);
                                return;
                            }

                            if (consumable.Type != Item.TYPE_SUPER_TORTOISE_GEM)
                            {
                                await user.SendAsync(this);
                                return;
                            }

                            usable.Add(consumable);
                            if (usable.Count >= consumeNum)
                            {
                                break;
                            }
                        }

                        if (consumeNum == 0 || usable.Count < consumeNum)
                        {
                            await user.SendAsync(this);
                            await user.SendAsync(StrEmbedNoRequiredItem);
                            return;
                        }

                        foreach (var consumable in usable)
                        {
                            await user.UserPackage.SpendItemAsync(consumable);
                        }

                        item.ReduceDamage = nextBless;

                        Command = 1;
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        await user.SendAsync(this);
                        await user.SendEffectAsync(effect, true);
                        await item.SaveAsync();

                        blessItemLogger.Information($"{user.GetDefaultLoggerPrefix()},{item.Identity},{item.Blessing},{Item.TYPE_SUPER_TORTOISE_GEM},{consumeNum}");
                        break;
                    }

                case ItemActionType.ActivateAccessory: // 41
                    {
                        item = user.UserPackage.GetInventory(Identity);
                        if (item == null)
                        {
                            return;
                        }

                        if (!item.IsActivable()) // support for old items before update
                        {
                            DbItemtype itemType = ItemManager.GetItemtype(item.Type);
                            if (itemType == null)
                            {
                                return;
                            }

                            if (itemType.SaveTime == 0)
                            {
                                return;
                            }

                            item.SaveTime = (int)itemType.SaveTime;
                        }

                        await item.ActivateAsync();
                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.SocketEquipment: // 43
                    {
                        item = user.UserPackage.FindItemByIdentity(Identity);

                        if (item == null)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        if (item.Position != Item.ItemPosition.Inventory && item.Durability / 100 != item.MaximumDurability / 100)
                        {
                            await user.SendAsync(StrItemErrRepairItem);
                            await user.SendAsync(this);
                            return;
                        }

                        bool isWeapon = item.IsWeapon();
                        bool isEquipment = item.IsHelmet() || item.IsNeck() || item.IsRing() || item.IsBangle() || item.IsArmor() || item.IsShoes() || item.IsShield();

                        if (!isWeapon && !isEquipment)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        if (item.SocketTwo != Item.SocketGem.NoSocket)
                        {
                            await user.SendAsync(this);
                            return;
                        }

                        List<Item> consumables = new();
                        foreach (var consumableId in Consumables)
                        {
                            Item consumable = user.UserPackage.GetInventory(consumableId);
                            if (consumable != null)
                            {
                                consumables.Add(consumable);
                            }
                        }

                        int consumeNum = 0;
                        if (isWeapon)
                        {
                            if (consumables.Any(x => x.Type != Item.TYPE_DRAGONBALL))
                            {
                                await user.SendAsync(this);
                                return;
                            }

                            consumeNum = item.SocketOne == Item.SocketGem.NoSocket ? 1 : 5;
                        }
                        else if (item.SocketOne == Item.SocketGem.NoSocket)
                        {
                            if (consumables.Any(x => x.Type != Item.TYPE_DRAGONBALL))
                            {
                                await user.SendAsync(this);
                                return;
                            }

                            consumeNum = 12;
                        }
                        else if (item.SocketTwo == Item.SocketGem.NoSocket)
                        {
                            if (consumables.All(x => x.Type == Item.TYPE_TOUGHDRILL))
                            {
                                consumeNum = 1;
                            }
                            else if (consumables.All(x => x.Type == Item.TYPE_STARDRILL))
                            {
                                consumeNum = 7;
                            }
                        }

                        if (consumeNum == 0 || consumables.Count < consumeNum)
                        {
                            await user.SendAsync(StrEmbedNoRequiredItem);
                            await user.SendAsync(this);
                            return;
                        }

                        foreach (var consumable in consumables.Take(consumeNum))
                        {
                            await user.UserPackage.SpendItemAsync(consumable);

                            if (consumable.Type == Item.TYPE_TOUGHDRILL)
                            {
                                if (await NextAsync(100) > 10)
                                {
                                    await user.UserPackage.AwardItemAsync(Item.TYPE_STARDRILL);
                                    await user.SendAsync(this);
                                    return;
                                }
                                
                                await RoleManager.BroadcastWorldMsgAsync(string.Format(StrBroadcastMakeSocket, user.Name), TalkChannel.Center);
                            }
                        }

                        if (item.SocketOne == Item.SocketGem.NoSocket)
                        {
                            item.SocketOne = Item.SocketGem.EmptySocket;
                        }
                        else if (item.SocketTwo == Item.SocketGem.NoSocket)
                        {
                            item.SocketTwo = Item.SocketGem.EmptySocket;
                        }

                        await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                        Command = 1;
                        await user.SendAsync(this);

                        await item.SaveAsync();
                        break;
                    }

                case ItemActionType.MainEquipment: // 44
                case ItemActionType.AlternativeEquipment: // 45
                    {
                        if (await user.ToggleSecondaryEquipmentAsync())
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case ItemActionType.MergeItems: // 48
                    {
                        await user.UserPackage.CombineItemAsync(Command, Identity);
                        break;
                    }

                case ItemActionType.SplitItems: // 49
                    {
                        await user.UserPackage.SplitItemAsync(Identity, (int)Command);
                        break;
                    }

                case ItemActionType.RequestItemTooltip: // 52
                    {
                        DbItem dbItem = await ItemRepository.GetByIdAsync(Identity);
                        if (dbItem == null)
                        {
                            return;
                        }

                        item = new Item();
                        if (!await item.CreateAsync(dbItem))
                        {
                            return;
                        }

                        await user.SendAsync(new MsgItemInfo(item, (MsgItemInfo.ItemMode)9));
                        if (item.ItemStatus != null)
                        {
                            await item.ItemStatus.SendToAsync(user);
                        }
                        break;
                    }

                case ItemActionType.DegradeEquipment: // 54
                    {
                        item = user.UserPackage.GetInventory(Identity);
                        if (item == null)
                        {
                            return;
                        }

                        if (!await user.SpendBoundConquerPointsAsync(Character.EmoneyOperationType.DegradeItem, 54, true))
                        {
                            return;
                        }

                        await item.DegradeItemAsync();
                        await user.SendAsync(new MsgItemInfo(item));
                        Command = 1;
                        await user.SendAsync(this);
                        break;
                    }

                case ItemActionType.MergeSash: // 56
                    {
                        item = user.UserPackage.GetInventory(Identity);
                        if (item == null)
                        {
                            return;
                        }

                        if (item.GetItemSort() != (Item.ItemSort?)11)
                        {
                            return;
                        }

                        await user.OpenSashSlotsAsync(item);
                        break;
                    }
            }
        }
    }
}
