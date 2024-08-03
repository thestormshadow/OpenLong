using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items.Status;
using Long.Kernel.States.User;
using Long.Shared.Helpers;
using Long.Shared.Mathematics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.States.Items.MapItem;

namespace Long.Kernel.States.Items
{
    public sealed class Item
    {
        private const uint SPECIAL_FLAG_UNLOCKING = 0x2;
        private const uint SPECIAL_FLAG_LOCKED = 0x1;

        private static readonly ILogger logger = Log.ForContext<Item>();
        private static readonly ILogger deleteItemLogger = Logger.CreateLogger("delete_item");
        private static readonly ILogger deleteChkSumItemLogger = Logger.CreateConsoleLogger("delete_item_chksum");

        private Character user;
        private DbItem item;
        private DbItemtype itemType;
        private DbItemAddition itemAddition;

        public Item()
        {            
        }

        public Item(Character user)
        {
            this.user = user;
        }

        #region Creation

        public async Task<bool> CreateAsync(DbItemtype type, ItemPosition position = ItemPosition.Inventory, bool monopoly = false)
        {
            if (type == null)
            {
                return false;
            }

            item = new DbItem
            {
                PlayerId = user.Identity,
                Type = type.Type,
                Position = (byte)position,
                Amount = type.Amount,
                AmountLimit = type.AmountLimit,
                Magic1 = (byte)type.Magic1,
                Magic2 = type.Magic2,
                Magic3 = type.Magic3,
                Color = 3,
                Monopoly = (byte)(monopoly ? 3 : 0)
            };

            itemType = type;
            itemAddition = ItemManager.GetItemAddition(Type, Plus);

            await InitializeFunctionAsync();
            
            if (await SaveAsync() && (user.LastAddItemIdentity = Identity) != 0)
            {
                item.ChkSum = CalculateCheckSum(item);
                await SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CreateAsync(DbItem item)
        {
            if (item == null)
            {
                return false;
            }

            this.item = item;
            itemType = ItemManager.GetItemtype(item.Type);
            if (itemType == null)
            {
                return false;
            }

            itemAddition = ItemManager.GetItemAddition(item.Type, item.Magic3);
            uint chksum = CalculateCheckSum(item);

#if DEBUG
            if (this.item.ChkSum == 0)
            {
                item.ChkSum = chksum;
            }
#endif
            if (this.item.Id == 0)
            {
                item.ChkSum = chksum;
                await SaveAsync();
                user.LastAddItemIdentity = Identity;
            }
            else if (item.ChkSum != chksum)
            {
                deleteChkSumItemLogger.Warning("Invalid chksum [expected: {0}, got: {1}] for item {2}, user {3} {4}", chksum, item.ChkSum, item.Id, user?.Identity ?? 0, user?.Name ?? string.Empty);
                // await DeleteAsync();
                item.Plunder = 1;
                return true;
            }

            await InitializeFunctionAsync();
            return true;
        }

        public async Task<bool> InitializeOSItemAsync(DbItem item, DbItemStatus artifact, DbItemStatus refinery)
        {
            if (item == null)
            {
                return false;
            }

            this.item = item;
            itemType = ItemManager.GetItemtype(item.Type);
            if (itemType == null)
            {
                return false;
            }

            itemAddition = ItemManager.GetItemAddition(item.Type, item.Magic3);

            if (IsHelmet() || IsNeck() || IsBangle() || IsRing()
                    || IsWeapon() || IsShield() || IsArmor() || IsShoes())
            {
                ItemStatus = new ItemStatus();
                await ItemStatus.InitializeOSItemAsync(this, artifact, refinery);
            }
            return true;
        }

        private async Task InitializeFunctionAsync()
        {
            if (IsHelmet() || IsNeck() || IsBangle() || IsRing()
                    || IsWeapon() || IsShield() || IsArmor() || IsShoes())
            {
                ItemStatus = new ItemStatus();
                await ItemStatus.InitializeAsync(this);
            }

            if (IsSuperFlag())
            {
                superFlags.AddRange(await SuperFlagRepository.GetAsync(Identity));
            }

            if (IsLocked())
            {
                ghostContract = await GhostContractRepository.GetAsync(Identity);
            }
        }

        public void ChangeOwner(Character user)
        {
            PlayerIdentity = user.Identity;
            this.user = user;
        }

        #endregion

        #region Requirements

        public int RequiredLevel => itemType?.ReqLevel ?? 0;

        public int RequiredProfession => (int)(itemType?.ReqProfession ?? 0);

        public int RequiredGender => itemType?.ReqSex ?? 0;

        public int RequiredWeaponSkill => itemType?.ReqWeaponskill ?? 0;

        public int RequiredForce => itemType?.ReqForce ?? 0;

        public int RequiredAgility => itemType?.ReqSpeed ?? 0;

        public int RequiredVitality => itemType?.ReqHealth ?? 0;

        public int RequiredSpirit => itemType?.ReqSoul ?? 0;

        #endregion

        #region Attributes

        public DbItemtype Itemtype => itemType;

        public uint Identity => item.Id;

        public string Name => itemType?.Name ?? StrNone;

        public string FullName
        {
            get
            {
                StringBuilder builder = new();
                switch (GetQuality())
                {
                    case 9: builder.Append("Super"); break;
                    case 8: builder.Append("Elite"); break;
                    case 7: builder.Append("Unique"); break;
                    case 6: builder.Append("Refined"); break;
                }
                builder.Append(Name);
                if (IsBound)
                {
                    builder.Append("(B)");
                }
                if (Plus > 0)
                {
                    builder.Append($"(+{Plus})");
                }
                if (SocketOne != SocketGem.NoSocket)
                {
                    if (SocketTwo != SocketGem.NoSocket)
                    {
                        builder.Append(" 2-Socketed");
                    }
                    else
                    {
                        builder.Append(" 1-Socketed");
                    }
                }
                if (ReduceDamage > 0)
                {
                    builder.Append($" -{ReduceDamage}%");
                }
                if (Enchantment > 0)
                {
                    builder.Append($" +{Enchantment}HP");
                }
                if (Effect != ItemEffect.None && !IsMount())
                {
                    builder.Append($" {Effect}");
                }
                return builder.ToString();
            }
        }

        public uint Type => itemType?.Type ?? 0;
        public uint CheckSum => item?.ChkSum ?? 0;

        /// <summary>
        ///     May be an NPC or Sash ID.
        /// </summary>
        public uint OwnerIdentity
        {
            get => item.OwnerId;
            set => item.OwnerId = value;
        }

        /// <summary>
        ///     The current owner of the equipment.
        /// </summary>
        public uint PlayerIdentity
        {
            get => item.PlayerId;
            set => item.PlayerId = value;
        }

        public ushort Durability
        {
            get => item.Amount;
            set => item.Amount = Math.Min(MaximumDurability, value);
        }

        public ushort MaximumDurability
        {
            get
            {
                ushort result = OriginalMaximumDurability;
                switch (SocketOne)
                {
                    case SocketGem.NormalKylinGem:
                    case SocketGem.RefinedKylinGem:
                    case SocketGem.SuperKylinGem:
                        result += (ushort)(OriginalMaximumDurability * (CalculateGemPercentage(SocketOne) / 100.0d));
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalKylinGem:
                    case SocketGem.RefinedKylinGem:
                    case SocketGem.SuperKylinGem:
                        result += (ushort)(OriginalMaximumDurability * (CalculateGemPercentage(SocketTwo) / 100.0d));
                        break;
                }

                return result;
            }
            set
            {
                item.AmountLimit = value;
                if (value < Durability)
                {
                    Durability = value;
                }
            }
        }

        public ushort OriginalMaximumDurability => item.AmountLimit;

        public SocketGem SocketOne
        {
            get => (SocketGem)item.Gem1;
            set => item.Gem1 = (byte)value;
        }

        public SocketGem SocketTwo
        {
            get => (SocketGem)item.Gem2;
            set => item.Gem2 = (byte)value;
        }

        public ItemPosition Position
        {
            get => (ItemPosition)item.Position;
            set => item.Position = (byte)value;
        }

        public byte Plus
        {
            get => (byte)(GetItemSubType() == 730 ? Type % 100 : item.Magic3);
            set => item.Magic3 = value;
        }

        public uint SocketProgress
        {
            get => item.Data;
            set => item.Data = value;
        }

        public uint CompositionProgress
        {
            get => item.AddlevelExp;
            set => item.AddlevelExp = value;
        }

        public ItemEffect Effect
        {
            get => (ItemEffect)item.Magic1;
            set => item.Magic1 = (ushort)value;
        }

        public ushort Magic1
        {
            get => item.Magic1;
            set => item.Magic1 = value;
        }

        public byte Magic2
        {
            get => item.Magic2;
            set => item.Magic2 = value;
        }

        public byte ReduceDamage
        {
            get => item.ReduceDmg;
            set => item.ReduceDmg = value;
        }

        public byte Enchantment
        {
            get => (byte)(Position != ItemPosition.Mount ? item.AddLife : 0);
            set => item.AddLife = value;
        }

        public byte AntiMonster
        {
            get => item.AntiMonster;
            set => item.AntiMonster = value;
        }

        public ItemColor Color
        {
            get => (ItemColor)item.Color;
            set => item.Color = (byte)value;
        }

        public bool IsBound
        {
            get => item.Monopoly != 0;
            set
            {
                if (value)
                {
                    item.Monopoly |= ITEM_MONOPOLY_MASK;
                }
                else
                {
                    int monopoly = item.Monopoly;
                    monopoly &= ~ITEM_MONOPOLY_MASK;
                    item.Monopoly = (byte)monopoly;
                }
            }
        }

        /// <summary>
        /// If jar, the amount of monsters killed
        /// </summary>
        public uint Data
        {
            get => item.Data;
            set => item.Data = value;
        }

        public uint SyndicateIdentity
        {
            get => item.Syndicate;
            set => item.Syndicate = value;
        }

        public uint AccumulateNum
        {
            get
            {
                if (IsPileEnable())
                {
                    return item.AccumulateNum;
                }
                return Math.Max(1, item.AccumulateNum);
            }
            set => item.AccumulateNum = value;
        }

        public uint MaxAccumulateNum => Math.Max(itemType?.AccumulateLimit ?? 1u, 1);

        public byte Monopoly => item.Monopoly;

        public uint RecoverEnergy => itemType?.RecoverEnergy ?? 0;

        #endregion

        #region Battle Attributes

        public int BattlePower
        {
            get
            {
                if (!IsEquipment() && !IsMount() || IsGarment() || IsGourd())
                {
                    return 0;
                }

                if (IsHossuType())
                {
                    return 0;
                }

                if (IsBroken())
                {
                    return 0;
                }

                int ret = Math.Max(0, (int)Type % 10 - 5);
                ret += Plus;
                ret += SocketOne != SocketGem.NoSocket ? 1 : 0;
                ret += (int)SocketOne % 10 == 3 ? 1 : 0;
                ret += SocketTwo != SocketGem.NoSocket ? 1 : 0;
                ret += (int)SocketTwo % 10 == 3 ? 1 : 0;

                if (IsWeapon())
                {
                    bool isDoublePowerItem = IsBackswordType() || IsWeaponTwoHand();
                    bool isLeftHandEmpty = user?.GetEquipment(ItemPosition.LeftHand) == null
                        || user.GetEquipment(ItemPosition.LeftHand).IsArrowSort()
                        || user.GetEquipment(ItemPosition.LeftHand).IsHossuType();
                    if (isDoublePowerItem && isLeftHandEmpty)
                    {
                        ret *= 2;
                    }
                }
                return ret;
            }
        }

        public int Life
        {
            get
            {
                int result = itemType?.Life ?? 0;
                result += Enchantment;
                result += itemAddition?.Life ?? 0;
                if (ItemStatus?.CurrentArtifact != null)
                {
                    result += ItemStatus.CurrentArtifact.ItemType.Life;
                }
                if (ItemStatus?.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Intensification)
                {
                    result += (int)ItemStatus.CurrentRefinery.Power1;
                }
                if (ItemStatus?.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Intensification)
                {
                    result += (int)ItemStatus.CurrentRefinery.Power2;
                }
                return result;
            }
        }

        public int Mana => itemType?.Mana ?? 0;

        public int MinAttack
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.AttackMin ?? 0;
                if (!IsAttackTalisman() && !IsDefenseTalisman() && !IsWing())
                {
                    result += itemAddition?.AttackMin ?? 0;
                }
                DbWeaponSkill ws = user?.WeaponSkill[(ushort)GetItemSubType()];
                if (ws != null && ws.Level > 12)
                {
                    result = (int)(result * (1 + (12 - ws.Level) * -1 / 100d));
                }

                if (ItemStatus?.CurrentArtifact?.ItemType != null)
                {
                    result += ItemStatus.CurrentArtifact.ItemType.AttackMin;
                }

                return result;
            }
        }

        public int MaxAttack
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.AttackMax ?? 0;
                if (!IsAttackTalisman() && !IsDefenseTalisman() && !IsWing())
                {
                    result += itemAddition?.AttackMax ?? 0;
                }
                if (IsWeapon())
                {
                    DbWeaponSkill ws = user?.WeaponSkill[(ushort)GetItemSubType()];
                    if (ws != null && ws.Level > 12)
                    {
                        result = (int)(result * (1 + (12 - ws.Level) * -1 / 100d));
                    }
                }

                if (ItemStatus?.CurrentArtifact?.ItemType != null)
                {
                    result += ItemStatus.CurrentArtifact.ItemType.AttackMax;
                }

                return result;
            }
        }

        public int AddFinalDamage
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.FinalDmgAdd ?? 0;
                if (IsTalisman())
                {
                    result += itemAddition?.AttackMax ?? 0;
                }

                switch (SocketOne)
                {
                    case SocketGem.NormalThunderGem:
                    case SocketGem.RefinedThunderGem:
                    case SocketGem.SuperThunderGem:
                        {
                            result += CalculateGemPercentage(SocketOne);
                            break;
                        }
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalThunderGem:
                    case SocketGem.RefinedThunderGem:
                    case SocketGem.SuperThunderGem:
                        {
                            result += CalculateGemPercentage(SocketTwo);
                            break;
                        }
                }

                return result;
            }
        }

        public int MagicAttack
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.MagicAtk ?? 0;
                result += itemAddition?.MagicAtk ?? 0;
                DbWeaponSkill ws = user?.WeaponSkill[(ushort)GetItemSubType()];
                if (ws != null && ws.Type == 421 && ws.Level > 12)
                {
                    result += result * (1 + (Role.MAX_WEAPONSKILLLEVEL - ws.Level) / 100);
                }

                if (ItemStatus?.CurrentArtifact != null)
                {
                    result += ItemStatus.CurrentArtifact.ItemType.MagicAtk;
                }

                return result;
            }
        }

        public int AddFinalMagicDamage
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = 0;
                result = itemType?.FinalDmgAddMgc ?? 0;

                if (IsTalisman())
                {
                    result += itemAddition?.MagicAtk ?? 0;
                }

                switch (SocketOne)
                {
                    case SocketGem.NormalThunderGem:
                    case SocketGem.RefinedThunderGem:
                    case SocketGem.SuperThunderGem:
                        {
                            result += CalculateGemPercentage(SocketOne);
                            break;
                        }
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalThunderGem:
                    case SocketGem.RefinedThunderGem:
                    case SocketGem.SuperThunderGem:
                        {
                            result += CalculateGemPercentage(SocketTwo);
                            break;
                        }
                }
                return result;
            }
        }

        public int Defense
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.Defense ?? 0;
                if (IsArrowSort())
                {
                    return result;
                }

                result += itemAddition?.Defense ?? 0;

                if (ItemStatus?.CurrentArtifact != null)
                {
                    result += ItemStatus.CurrentArtifact.ItemType.Defense;
                }

                return result;
            }
        }

        public int AddFinalDefense
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.FinalDmgReduce ?? 0;
                if (IsTalisman())
                {
                    result += itemAddition?.Defense ?? 0;
                }

                switch (SocketOne)
                {
                    case SocketGem.NormalGloryGem:
                    case SocketGem.RefinedGloryGem:
                    case SocketGem.SuperGloryGem:
                        {
                            result += CalculateGemPercentage(SocketOne);
                            break;
                        }
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalGloryGem:
                    case SocketGem.RefinedGloryGem:
                    case SocketGem.SuperGloryGem:
                        {
                            result += CalculateGemPercentage(SocketTwo);
                            break;
                        }
                }
                return result;
            }
        }

        public int MagicDefense
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                if (Position == ItemPosition.Armor || Position == ItemPosition.Headwear || Position == ItemPosition.Necklace)
                {
                    return itemAddition?.MagicDef ?? 0;
                }

                return itemType?.MagicDef ?? 0;
            }
        }

        public int AddFinalMagicDefense
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                int result = itemType?.FinalDmgReduceMgc ?? 0;
                if (IsTalisman())
                {
                    result += itemAddition?.MagicDef ?? 0;
                }

                switch (SocketOne)
                {
                    case SocketGem.NormalGloryGem:
                    case SocketGem.RefinedGloryGem:
                    case SocketGem.SuperGloryGem:
                        {
                            result += CalculateGemPercentage(SocketOne);
                            break;
                        }
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalGloryGem:
                    case SocketGem.RefinedGloryGem:
                    case SocketGem.SuperGloryGem:
                        {
                            result += CalculateGemPercentage(SocketTwo);
                            break;
                        }
                }
                return result;
            }
        }

        public int MagicDefenseBonus
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                if (Position == ItemPosition.Armor || Position == ItemPosition.Headwear)
                {
                    int result = itemType?.MagicDef ?? 0;
                    if (ItemStatus?.CurrentArtifact != null)
                    {
                        result += ItemStatus.CurrentArtifact.ItemType.MagicDef;
                    }
                    return result;
                }

                return 0;
            }
        }

        public int Agility
        {
            get
            {
                if (IsBroken())
                {
                    return 0;
                }

                if (Position == ItemPosition.Mount)
                {
                    return 0;
                }

                int result = Itemtype?.Dexterity ?? 0;
                return result;
            }
        }

        public int Accuracy
        {
            get
            {
                if (IsBroken() || IsCrop())
                {
                    return 0;
                }

                if (Position == ItemPosition.Mount)
                {
                    return 0;
                }

                int result = itemAddition?.Dexterity ?? 0;
                return result;
            }
        }

        public int Dodge
        {
            get
            {
                if (IsBroken() || IsCrop() || IsMount())
                {
                    return 0;
                }

                int result = itemType?.Dodge ?? 0;
                result += itemAddition?.Dodge ?? 0;
                return result;
            }
        }

        public int Blessing => Position == ItemPosition.Mount ? 0 : item?.ReduceDmg ?? 0;

        public int DragonGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalDragonGem:
                    case SocketGem.RefinedDragonGem:
                    case SocketGem.SuperDragonGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalDragonGem:
                    case SocketGem.RefinedDragonGem:
                    case SocketGem.SuperDragonGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int PhoenixGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalPhoenixGem:
                    case SocketGem.RefinedPhoenixGem:
                    case SocketGem.SuperPhoenixGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalPhoenixGem:
                    case SocketGem.RefinedPhoenixGem:
                    case SocketGem.SuperPhoenixGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int RainbowGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalRainbowGem:
                    case SocketGem.RefinedRainbowGem:
                    case SocketGem.SuperRainbowGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalRainbowGem:
                    case SocketGem.RefinedRainbowGem:
                    case SocketGem.SuperRainbowGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int VioletGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalVioletGem:
                    case SocketGem.RefinedVioletGem:
                    case SocketGem.SuperVioletGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalVioletGem:
                    case SocketGem.RefinedVioletGem:
                    case SocketGem.SuperVioletGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int FuryGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalFuryGem:
                    case SocketGem.RefinedFuryGem:
                    case SocketGem.SuperFuryGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalFuryGem:
                    case SocketGem.RefinedFuryGem:
                    case SocketGem.SuperFuryGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int MoonGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalMoonGem:
                    case SocketGem.RefinedMoonGem:
                    case SocketGem.SuperMoonGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalMoonGem:
                    case SocketGem.RefinedMoonGem:
                    case SocketGem.SuperMoonGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int TortoiseGemEffect
        {
            get
            {
                int result = 0;
                switch (SocketOne)
                {
                    case SocketGem.NormalTortoiseGem:
                    case SocketGem.RefinedTortoiseGem:
                    case SocketGem.SuperTortoiseGem:
                        result += CalculateGemPercentage(SocketOne);
                        break;
                }

                switch (SocketTwo)
                {
                    case SocketGem.NormalTortoiseGem:
                    case SocketGem.RefinedTortoiseGem:
                    case SocketGem.SuperTortoiseGem:
                        result += CalculateGemPercentage(SocketTwo);
                        break;
                }

                return result;
            }
        }

        public int CriticalStrike
        {
            get
            {
                uint result = itemType?.CriticalStrike ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.CriticalStrike ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.CriticalStrike)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 100;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.CriticalStrike)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 100;
                    }
                }
                return (int)result;
            }
        }

        public int SkillCriticalStrike
        {
            get
            {
                uint result = itemType?.SkillCritStrike ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.SkillCritStrike ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.SkillCriticalStrike)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 100;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.SkillCriticalStrike)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 100;
                    }
                }
                return (int)result;
            }
        }

        public int Immunity
        {
            get
            {
                uint result = itemType?.Immunity ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.Immunity ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Immunity)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 100;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Immunity)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 100;
                    }
                }
                return (int)result;
            }
        }

        public int Penetration
        {
            get
            {
                uint result = itemType?.Penetration ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.Penetration ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Penetration)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 100;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Penetration)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 100;
                    }
                }
                return (int)result;
            }
        }

        public int Breakthrough
        {
            get
            {
                uint result = itemType?.Breakthrough ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.Breakthrough ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Breakthrough)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 10;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Breakthrough)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 10;
                    }
                }
                return (int)result;
            }
        }

        public int Counteraction
        {
            get
            {
                uint result = itemType?.Counteraction ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.Counteraction ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Counteraction)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 10;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Counteraction)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 10;
                    }
                }
                return (int)result;
            }
        }

        public int Block
        {
            get
            {
                uint result = itemType?.Block ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.Block ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Block)
                    {
                        result += ItemStatus.CurrentRefinery.Power1 * 100;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Block)
                    {
                        result += ItemStatus.CurrentRefinery.Power2 * 100;
                    }
                }
                return (int)result;
            }
        }

        public int FireResistance
        {
            get
            {
                uint result = itemType?.ResistFire ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.ResistFire ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.FireResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.FireResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int WaterResistance
        {
            get
            {
                uint result = itemType?.ResistWater ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.ResistWater ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.WaterResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.WaterResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int WoodResistance
        {
            get
            {
                uint result = itemType?.ResistWood ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.ResistWood ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.WoodResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.WoodResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int EarthResistance
        {
            get
            {
                uint result = itemType?.ResistEarth ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.ResistEarth ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.EarthResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.EarthResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int MetalResistance
        {
            get
            {
                uint result = itemType?.ResistMetal ?? 0;
                if (ItemStatus != null)
                {
                    result += ItemStatus.CurrentArtifact?.ItemType.ResistMetal ?? 0;
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.MetalResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.MetalResist)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int Detoxication
        {
            get
            {
                uint result = 0;
                if (ItemStatus != null)
                {
                    if (ItemStatus.CurrentRefinery?.Attribute1 == ItemStatusAttribute.Detoxication)
                    {
                        result += ItemStatus.CurrentRefinery.Power1;
                    }
                    if (ItemStatus.CurrentRefinery?.Attribute2 == ItemStatusAttribute.Detoxication)
                    {
                        result += ItemStatus.CurrentRefinery.Power2;
                    }
                }
                return (int)result;
            }
        }

        public int AttackRange => itemType?.AtkRange ?? 1;

        public int Vigor
        {
            get
            {
                int result = itemType.Dexterity;
                result += itemAddition?.Dexterity ?? 0;
                return result;
            }
        }

        #endregion

        #region Change Data

        public async Task<bool> ChangeTypeAsync(uint newType)
        {
            DbItemtype itemtype = ItemManager.GetItemtype(newType);
            if (itemtype == null)
            {
                logger.Error($"ChangeType() Invalid itemtype id {newType}");
                return false;
            }

            item.Type = itemtype.Type;
            itemType = itemtype;
            itemAddition = ItemManager.GetItemAddition(newType, item.Magic3);
            await user.SendAsync(new MsgItemInfo(this, MsgItemInfo.ItemMode.Update));
            await SaveAsync();
            return true;
        }

        public bool ChangeAddition(int level = -1)
        {
            if (level < 0)
            {
                level = (byte)(Plus + 1);
            }

            if (level > 0)
            {
                uint type = Type;
                DbItemAddition add = ItemManager.GetItemAddition(type, (byte)level);
                if (add == null)
                {
                    return false;
                }

                Plus = (byte)level;
                itemAddition = add;
                return true;
            }
            return false;
        }

        #endregion

        #region Item Status

        public ItemStatus ItemStatus { get; set; }

        #endregion

        #region Item broken

        public bool IsBroken()
        {
            return Durability == 0;
        }

        #endregion

        #region Update and Upgrade

        public bool GetUpLevelChance(out int chance, out int nextId)
        {
            nextId = 0;
            chance = 0;

            DbItemtype info = NextItemLevel((int)Type);
            if (info == null)
            {
                return false;
            }

            nextId = (int)info.Type;

            chance = 100;
            if (IsHelmet() || IsArmor() || IsShield()) //Head || Armor || Shield
            {
                switch (GetLevel(info.Type))
                {
                    case 6:
                        chance = 50;
                        break;
                    case 7:
                        chance = 40;
                        break;
                    case 8:
                        chance = 30;
                        break;
                    case 9:
                        chance = 20;
                        break;
                    default:
                        chance = 500;
                        break;
                }

                switch (GetQuality(info.Type))
                {
                    case 6:
                        chance = Calculations.MulDiv(chance, 90, 100);
                        break;
                    case 7:
                        chance = Calculations.MulDiv(chance, 70, 100);
                        break;
                    case 8:
                        chance = Calculations.MulDiv(chance, 30, 100);
                        break;
                    case 9:
                        chance = Calculations.MulDiv(chance, 10, 100);
                        break;
                }
            }
            else
            {
                if (IsNeck() || IsRing() || IsBangle() || IsShoes())
                {
                    switch (GetLevel(info.Type))
                    {
                        case 13:
                        case 14:
                            chance = 90;
                            break;
                        case 15:
                        case 16:
                            if (IsNeck())
                            {
                                chance = 90;
                            }
                            else
                            {
                                chance = 80;
                            }
                            break;
                        case 17:
                        case 18:
                            chance = 70;
                            break;
                        case 19:
                        case 20:
                            chance = 60;
                            break;
                        case 21:
                            if (IsNeck())
                            {
                                chance = 60;
                            }
                            else
                            {
                                chance = 50;
                            }
                            break;
                        case 22:
                            if (IsBangle())
                            {
                                chance = 50;
                            }
                            else
                            {
                                chance = 45;
                            }
                            break;
                        default:
                            chance = 500;
                            break;
                    }
                }
                else
                {
                    switch (GetLevel(info.Type))
                    {
                        case 12:
                        case 13:
                            chance = 90;
                            break;
                        case 14:
                        case 15:
                            chance = 80;
                            break;
                        case 16:
                        case 17:
                            chance = 70;
                            break;
                        case 18:
                        case 19:
                            chance = 60;
                            break;
                        case 20:
                        case 21:
                            chance = 50;
                            break;
                        case 22:
                            chance = 45;
                            break;
                        default:
                            chance = 500;
                            break;
                    }
                }

                switch (GetQuality(info.Type))
                {
                    case 6:
                        chance = Calculations.MulDiv(chance, 90, 100);
                        break;
                    case 7:
                        chance = Calculations.MulDiv(chance, 80, 100);
                        break;
                    case 8:
                        chance = Calculations.MulDiv(chance, 30, 100);
                        break;
                    case 9:
                        chance = Calculations.MulDiv(chance, 10, 100);
                        break;
                }
            }

            return true;
        }

        public DbItemtype NextItemLevel()
        {
            return NextItemLevel((int)Type);
        }

        public DbItemtype NextItemLevel(Int32 id)
        {
            // By CptSky
            Int32 nextId = id;

            var sort = (byte)(id / 100000);
            var type = (byte)(id / 10000);
            var subType = (short)(id / 1000);

            if (sort == 1) //!Weapon
            {
                if (type == 12 && (subType == 120 || subType == 121) || type == 15 || type == 16
                ) //Necklace || Ring || Boots
                {
                    var level = (byte)((id % 1000 - id % 10) / 10);
                    if (type == 12 && level < 8 || type == 15 && subType != 152 && level > 0 && level < 21 ||
                        type == 15 && subType == 152 && level >= 4 && level < 22 ||
                        type == 16 && level > 0 && level < 21)
                    {
                        //Check if it's still the same type of item...
                        if ((Int16)((nextId + 20) / 1000) == subType)
                        {
                            nextId += 20;
                        }
                    }
                    else if (type == 12 && level == 8 || type == 12 && level >= 21 ||
                             type == 15 && subType != 152 && level == 0
                             || type == 15 && subType != 152 && level >= 21 ||
                             type == 15 && subType == 152 && level >= 22 || type == 16 && level >= 21)
                    {
                        //Check if it's still the same type of item...
                        if ((short)((nextId + 10) / 1000) == subType)
                        {
                            nextId += 10;
                        }
                    }
                    else if (type == 12 && level >= 9 && level < 21 || type == 15 && subType == 152 && level == 1)
                    {
                        //Check if it's still the same type of item...
                        if ((short)((nextId + 30) / 1000) == subType)
                        {
                            nextId += 30;
                        }
                    }
                }
                else
                {
                    var Quality = (byte)(id % 10);
                    if (type == 11 || type == 14 || type == 13 || subType == 123) //Head || Armor
                    {
                        var level = (byte)((id % 100 - id % 10) / 10);

                        //Check if it's still the same type of item...
                        if ((short)((nextId + 10) / 1000) == subType)
                        {
                            nextId += 10;
                        }
                    }
                }
            }
            else if (sort == 4 || sort == 5 || sort == 6) //Weapon
            {
                //Check if it's still the same type of item...
                if ((short)((nextId + 10) / 1000) == subType)
                {
                    nextId += 10;
                }

                //Invalid Backsword ID
                if (nextId / 10 == 42103 || nextId / 10 == 42105 || nextId / 10 == 42109 || nextId / 10 == 42111)
                {
                    nextId += 10;
                }
            }
            else if (sort == 9)
            {
                var Level = (byte)((id % 100 - id % 10) / 10);
                if (Level != 30) //!Max...
                {
                    //Check if it's still the same type of item...
                    if ((short)((nextId + 10) / 1000) == subType)
                    {
                        nextId += 10;
                    }
                }
            }

            return ItemManager.GetItemtype((uint)nextId);
        }

        public uint ChkUpEqQuality(uint type)
        {
            if (type == TYPE_MOUNT_ID)
            {
                return 0;
            }

            uint nQuality = type % 10;

            if (nQuality < 3 || nQuality >= 9)
            {
                return 0;
            }

            nQuality = Math.Min(9, Math.Max(6, ++nQuality));

            type = type - type % 10 + nQuality;

            return ItemManager.GetItemtype(type)?.Type ?? 0;
        }

        public bool GetUpEpQualityInfo(out double nChance, out uint idNewType)
        {
            nChance = 0;
            idNewType = 0;

            if (Type == 150000 || Type == 150310 || Type == 150320 || Type == 410301 || Type == 421301 ||
                Type == 500301)
            {
                return false;
            }

            idNewType = ChkUpEqQuality(Type);
            nChance = 100;

            switch (Type % 10)
            {
                case 6:
                    nChance = 50;
                    break;
                case 7:
                    nChance = 33;
                    break;
                case 8:
                    nChance = 20;
                    break;
                default:
                    nChance = 100;
                    break;
            }

            DbItemtype itemtype = ItemManager.GetItemtype(idNewType);
            if (itemtype == null)
            {
                return false;
            }

            uint nFactor = itemtype.ReqLevel;

            if (nFactor > 70)
            {
                nChance = (uint)(nChance * (100 - (nFactor - 70) * 1.0) / 100);
            }

            nChance = Math.Max(1, nChance);
            return true;
        }

        public uint GetFirstId()
        {
            uint firstId = Type;

            var sort = (byte)(Type / 100000);
            var type = (byte)(Type / 10000);
            var subType = (short)(Type / 1000);

            if (Type == 150000 || Type == 150310 || Type == 150320 || Type == 410301 || Type == 421301 || Type == 500301
                || Type == 601301 || Type == 610301)
            {
                return Type;
            }

            if (Type >= 120310 && Type <= 120319)
            {
                return Type;
            }

            if (sort == 1) //!Weapon
            {
                if (IsNeck()) //Necklace
                {
                    firstId = Type - Type % 1000 + Type % 10;
                }
                else if (IsRing() || IsBangle() || IsShoes()) //Ring || Boots
                {
                    firstId = Type - Type % 1000 + 10 + Type % 10;
                }
                else if (IsHelmet()) //Head
                {
                    firstId = Type - Type % 1000 + Type % 10;
                }
                else if (IsArmor()) //Armor
                {
                    firstId = Type - Type % 1000 + Type % 10;
                }
            }
            else if (sort == 4 || sort == 5 || sort == 6) //Weapon
            {
                firstId = Type - Type % 1000 + 20 + Type % 10;
            }
            else if (sort == 9)
            {
                firstId = Type - Type % 1000 + Type % 10;
            }

            return ItemManager.GetItemtype(firstId)?.Type ?? Type;
        }

        public int GetUpQualityGemAmount()
        {
            if (!GetUpEpQualityInfo(out var nChance, out _))
            {
                return 0;
            }

            return (int)((100 / nChance + 1) * 12 / 10);
        }

        public int GetUpgradeGemAmount()
        {
            if (!GetUpLevelChance(out var nChance, out _))
            {
                return 0;
            }

            return (int)((int)(100d / nChance + 1) * 12d / 10d);
        }

        public async Task<bool> DegradeItemAsync(bool bCheckDura = true)
        {
            if (!IsEquipment())
            {
                return false;
            }

            if (bCheckDura)
            {
                if (Durability / 100 < MaximumDurability / 100)
                {
                    await user.SendAsync(StrItemErrRepairItem);
                    return false;
                }
            }

            uint newId = GetFirstId();
            DbItemtype newType = ItemManager.GetItemtype(newId);
            if (newType == null || newType.Type == Type)
            {
                return false;
            }

            return await ChangeTypeAsync(newType.Type);
        }

        public async Task<bool> UpItemQualityAsync()
        {
            if (Durability / 100 < MaximumDurability / 100)
            {
                await user.SendAsync(StrItemErrRepairItem);
                return false;
            }

            if (!GetUpEpQualityInfo(out var nChance, out var newId))
            {
                await user.SendAsync(StrItemErrMaxQuality);
                return false;
            }

            DbItemtype newType = ItemManager.GetItemtype(newId);
            if (newType == null)
            {
                await user.SendAsync(StrItemErrMaxLevel);
                return false;
            }

            int gemCost = (int)(100 / nChance + 1) * 12 / 10;
            if (!await user.UserPackage.SpendDragonBallsAsync(gemCost, !IsBound))
            {
                await user.SendAsync(string.Format(StrItemErrNotEnoughDragonBalls, gemCost));
                return false;
            }

            if (await ChanceCalcAsync(0.5d))
            {
                if (SocketOne == SocketGem.NoSocket)
                {
                    SocketOne = SocketGem.EmptySocket;
                }
                else if (SocketTwo == SocketGem.NoSocket)
                {
                    SocketTwo = SocketGem.EmptySocket;
                }
            }

            return await ChangeTypeAsync(newType.Type);
        }

        /// <summary>
        /// This method will upgrade an equipment level using meteors.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpEquipmentLevelAsync()
        {
            if (Durability / 100 < MaximumDurability / 100)
            {
                await user.SendAsync(StrItemErrRepairItem);
                return false;
            }

            if (!GetUpLevelChance(out var nChance, out var newId))
            {
                await user.SendAsync(StrItemErrMaxLevel);
                return false;
            }


            DbItemtype newType = ItemManager.GetItemtype((uint)newId);
            if (newType == null)
            {
                await user.SendAsync(StrItemErrMaxLevel);
                return false;
            }

            if (newType.ReqLevel > user.Level)
            {
                await user.SendAsync(StrItemErrNotEnoughLevel);
                return false;
            }

            int gemCost = (100 / nChance + 1) * 12 / 10;
            if (!await user.UserPackage.SpendMeteorsAsync(gemCost))
            {
                await user.SendAsync(string.Format(StrItemErrNotEnoughMeteors, gemCost));
                return false;
            }

            if (await ChanceCalcAsync(0.5d))
            {
                if (SocketOne == SocketGem.NoSocket)
                {
                    SocketOne = SocketGem.EmptySocket;
                }
                else if (SocketTwo == SocketGem.NoSocket)
                {
                    SocketTwo = SocketGem.EmptySocket;
                }
            }

            Durability = newType.AmountLimit;
            MaximumDurability = newType.AmountLimit;
            return await ChangeTypeAsync(newType.Type);
        }

        public async Task<bool> UpUltraEquipmentLevelAsync()
        {
            if (Durability / 100 < MaximumDurability / 100)
            {
                await user.SendAsync(StrItemErrRepairItem);
                return false;
            }

            DbItemtype newType = NextItemLevel((int)Type);

            if (newType == null || newType.Type == Type)
            {
                await user.SendAsync(StrItemErrMaxLevel);
                return false;
            }

            if (newType.ReqLevel > user.Level)
            {
                await user.SendAsync(StrItemErrNotEnoughLevel);
                return false;
            }

            return await ChangeTypeAsync(newType.Type);
        }

        #endregion

        #region Activable

        private readonly ILogger itemExpireLog = Logger.CreateLogger("item_expire");
        private readonly ILogger itemActivateLog = Logger.CreateLogger("item_activate");

        public int DeleteTime
        {
            get => item.DeleteTime;
            set => item.DeleteTime = value;
        }

        public int SaveTime
        {
            get => (int)item.SaveTime;
            set => item.SaveTime = (uint)value;
        }

        public int RemainingSeconds => item.DeleteTime != 0 ? item.DeleteTime - UnixTimestamp.Now : 0;

        public bool IsActivable()
        {
            return item.DeleteTime == 0 && (item.SaveTime != 0 || itemType.SaveTime != 0);
        }

        public bool HasExpired()
        {
            return item.DeleteTime != 0 && UnixTimestamp.Now > item.DeleteTime;
        }

        public async Task<bool> ActivateAsync()
        {
            if (!IsActivable())
            {
                return false;
            }

            uint saveTime = item.SaveTime;
            if (saveTime == 0 && itemType.SaveTime != 0)
            {
                saveTime = itemType.SaveTime;
            }

            item.DeleteTime = (int)(UnixTimestamp.Now + saveTime * 60);
            await SaveAsync();

            itemActivateLog.Information($"{PlayerIdentity},{OwnerIdentity},{Identity},{Name},{FullName},{SaveTime},{DeleteTime}");
            return true;
        }

        public async Task ExpireAsync()
        {
            if (!HasExpired())
            {
                return;
            }

            if (Position == ItemPosition.Inventory)
            {
                await user.UserPackage.RemoveFromInventoryAsync(this, UserPackage.RemovalType.Delete);
            }
            else if (Position is >= ItemPosition.EquipmentBegin and <= ItemPosition.EquipmentEnd)
            {
                await user.UserPackage.UnEquipAsync(Position, UserPackage.RemovalType.Delete);
            }
            else
            {
                return;
            }
            itemExpireLog.Information($"{PlayerIdentity},{OwnerIdentity},{Identity},{Name},{FullName},{SaveTime},{DeleteTime}");
        }

        public void UpdateAddTime(int addTime)
        {
            if (item.DeleteTime == 0)
            {
                return;
            }

            if (addTime < 0)
            {
                item.SaveTime = 0;
                item.DeleteTime = 0;
            }
            else
            {
                item.SaveTime += (uint)addTime;
                item.DeleteTime += addTime;
            }
            itemActivateLog.Information($"{PlayerIdentity},{OwnerIdentity},{Identity},{Name},{FullName},{addTime},{DeleteTime},UPDATE");
        }

		#endregion

		#region Perfection
		public uint PerfectionLevel
		{
			get => item.PerfectionLevel;
			set => item.PerfectionLevel = (uint)value;
		}
		public uint PerfectionProgress
		{
			get => item.PerfectionProgress;
			set => item.PerfectionProgress = (uint)value;
		}
		public uint PerfectionOwnerGuid
		{
			get => item.PerfectionOwnerGuid;
			set => item.PerfectionOwnerGuid = (uint)value;
		}
		public string PerfectionOwnerName
		{
			get => item.PerfectionOwnerName ?? "";
			set => item.PerfectionOwnerName = value;
		}
		public string PerfectionSignature
		{
			get => item.Signature ?? "";
			set => item.Signature = value;
		}
		#endregion

		#region Equip Lock

		private DbGhostContract ghostContract;

        public async Task<bool> TryUnlockAsync()
        {
            if (IsUnlocking() && ghostContract == null)
            {
                await DoUnlockAsync();
                return true;
            }

            if (HasUnlocked())
            {
                await user.SendAsync(new MsgEquipLock { Action = MsgEquipLock.LockMode.UnlockedItem, Identity = Identity });
                await user.SendAsync(new MsgEquipLock { Action = MsgEquipLock.LockMode.RequestUnlock, Identity = Identity });

                await DoUnlockAsync();
                return true;
            }

            if (IsUnlocking())
            {
                DateTime unlockTime = UnixTimestamp.ToDateTime(ghostContract.Timeout);
                await user.SendAsync(new MsgEquipLock
                {
                    Action = MsgEquipLock.LockMode.RequestUnlock,
                    Identity = Identity,
                    Mode = 3,
                    Param = (uint)(unlockTime.Year * 10000 + unlockTime.Month * 100 + unlockTime.Day)
                });
                return false;
            }

            return true;
        }

        public Task SetLockAsync()
        {
            item.Specialflag |= SPECIAL_FLAG_LOCKED;
            return SaveAsync();
        }

        public async Task SetUnlockAsync()
        {
            item.Specialflag |= SPECIAL_FLAG_UNLOCKING;
            ghostContract = new DbGhostContract
            {
                ItemId = Identity,
                OwnerId = PlayerIdentity,
                Timeout = (uint)UnixTimestamp.FromDateTime(DateTime.Now.AddDays(5))
            };
            await GhostContractRepository.CreateAsync(ghostContract);
            await SaveAsync();
        }

        public async Task DoUnlockAsync()
        {
            item.Specialflag = 0;
            if (ghostContract != null)
            {
                await GhostContractRepository.DeleteAsync(ghostContract);
            }
            await SaveAsync();
        }

        public bool HasUnlocked()
        {
            if (!IsUnlocking())
            {
                return false;
            }

            if (ghostContract == null)
            {
                return false;
            }

            return ghostContract.Timeout < UnixTimestamp.Now;
        }

        public bool IsLocked()
        {
            return (item.Specialflag & SPECIAL_FLAG_LOCKED) == SPECIAL_FLAG_LOCKED;
        }

        public bool IsUnlocking()
        {
            return (item.Specialflag & SPECIAL_FLAG_UNLOCKING) == SPECIAL_FLAG_UNLOCKING;
        }

        #endregion

        #region Check information

        public ItemPosition GetPosition()
        {
            if (IsHelmet())
            {
                return ItemPosition.Headwear;
            }

            if (IsNeck())
            {
                return ItemPosition.Necklace;
            }

            if (IsRing())
            {
                return ItemPosition.Ring;
            }

            if (IsBangle())
            {
                return ItemPosition.Ring;
            }

            if (IsWeapon())
            {
                return ItemPosition.RightHand;
            }

            if (IsShield())
            {
                return ItemPosition.LeftHand;
            }

            if (IsArrowSort())
            {
                return ItemPosition.LeftHand;
            }

            if (IsArmor())
            {
                return ItemPosition.Armor;
            }

            if (IsShoes())
            {
                return ItemPosition.Boots;
            }

            if (IsGourd())
            {
                return ItemPosition.Gourd;
            }

            if (IsGarment())
            {
                return ItemPosition.Garment;
            }

            if (IsAccessory())
            {
                if (IsShieldAccessory())
                {
                    return ItemPosition.LeftHandAccessory;
                }
                return ItemPosition.RightHandAccessory;
            }

            if (IsMountArmor())
            {
                return ItemPosition.MountArmor;
            }

            if (IsCrop())
            {
                return ItemPosition.Crop;
            }

            if (IsWing())
            {
                return ItemPosition.Wing;
            }

            return ItemPosition.Inventory;
        }

        public static ItemPosition GetPosition(uint type)
        {
            if (IsHelmet(type))
            {
                return ItemPosition.Headwear;
            }

            if (IsNeck(type))
            {
                return ItemPosition.Necklace;
            }

            if (IsRing(type))
            {
                return ItemPosition.Ring;
            }

            if (IsBangle(type))
            {
                return ItemPosition.Ring;
            }

            if (IsWeapon(type))
            {
                return ItemPosition.RightHand;
            }

            if (IsShield(type))
            {
                return ItemPosition.LeftHand;
            }

            if (IsArrowSort(type))
            {
                return ItemPosition.LeftHand;
            }

            if (IsArmor(type))
            {
                return ItemPosition.Armor;
            }

            if (IsShoes(type))
            {
                return ItemPosition.Boots;
            }

            if (IsGourd(type))
            {
                return ItemPosition.Gourd;
            }

            if (IsGarment(type))
            {
                return ItemPosition.Garment;
            }

            return ItemPosition.Inventory;
        }

        public bool IsSuspicious() => item.Plunder != 0;

        public bool IsMonopoly()
        {
            return (itemType.Monopoly & ITEM_MONOPOLY_MASK) != 0;
        }

        public bool IsNeverDropWhenDead()
        {
            return (itemType.Monopoly & ITEM_NEVER_DROP_WHEN_DEAD_MASK) != 0 || IsBound || IsMonopoly() || Plus > 5 || IsLocked();
        }

        public bool IsDisappearWhenDropped()
        {
            return IsMonopoly() || IsBound;
        }

        public bool CanBeDropped()
        {
            return !IsMonopoly() && !IsLocked() && !IsSuspicious() && BattlePower < 8;
        }

        public bool CanBeStored()
        {
            return (itemType.Monopoly & ITEM_STORAGE_MASK) == 0;
        }

        public bool IsHoldEnable()
        {
            return IsWeaponOneHand() || IsWeaponTwoHand() || IsWeaponProBased() || IsBow() || IsShield() || IsArrowSort();
        }

        public uint CalculateSocketProgress()
        {
            uint total = 0;
            total += TALISMAN_SOCKET_QUALITY_ADDITION[Type % 10];
            total += TALISMAN_SOCKET_PLUS_ADDITION[Plus];
            if (IsWeapon())
            {
                if (SocketTwo > 0)
                {
                    total += TALISMAN_SOCKET_HOLE_ADDITION0[2];
                }
                else if (SocketOne > 0)
                {
                    total += TALISMAN_SOCKET_HOLE_ADDITION0[1];
                }
            }
            else
            {
                if (SocketTwo > 0)
                {
                    total += TALISMAN_SOCKET_HOLE_ADDITION1[2];
                }
                else if (SocketOne > 0)
                {
                    total += TALISMAN_SOCKET_HOLE_ADDITION1[1];
                }
            }

            return total;
        }

		#endregion

		#region Super Flag

		private const int SUPER_FLAG_LIMIT = 10;
        private readonly List<DbSuperFlag> superFlags = new();

        public bool IsSuperFlag()
        {
            return Type == MEMORY_AGATE;
        }

        public int SuperFlagCount => superFlags.Count;

        public async Task SaveLocationAsync(string name, uint idMap, uint x, uint y)
        {
            if (superFlags.Count >= SUPER_FLAG_LIMIT)
            {
                return;
            }

            DbSuperFlag superFlag = new()
            {
                ItemId = Identity,
                MapId = idMap,
                MapX = x,
                MapY = y,
                Name = name,
                PosIndex = (uint)superFlags.Count
            };
            await ServerDbContext.CreateAsync(superFlag);
            superFlags.Add(superFlag);
            await SendSuperFlagListAsync();
        }

        public bool GetTeleportLocation(int idx, ref uint idMap, ref Point pos)
        {
            if (idx >= superFlags.Count)
            {
                return false;
            }

            idMap = superFlags[idx].MapId;
            pos = new Point((int)superFlags[idx].MapX, (int)superFlags[idx].MapY);
            return true;
        }

        public async Task UpdateLocationAsync(int index, string name, uint idMap, uint x, uint y)
        {
            if (index >= superFlags.Count)
            {
                return;
            }

            superFlags[index].Name = name;
            superFlags[index].MapId = idMap;
            superFlags[index].MapX = x;
            superFlags[index].MapY = y;
            await ServerDbContext.UpdateAsync(superFlags[index]);
            await SendSuperFlagListAsync();
        }

        public async Task UpdateNameAsync(int index, string name)
        {
            if (index >= superFlags.Count)
            {
                return;
            }

            superFlags[index].Name = name;
            await ServerDbContext.UpdateAsync(superFlags[index]);
            await SendSuperFlagListAsync();
        }

        public async Task ClearSuperFlagAsync()
        {
            await ServerDbContext.DeleteRangeAsync(superFlags);
            superFlags.Clear();
        }

        public Task SendSuperFlagListAsync()
        {
            MsgSuperFlag msg = new()
            {
                Durability = Durability,
                Identity = Identity
            };
            foreach (var superFlag in superFlags)
            {
                msg.Locations.Add(new MsgSuperFlag.LocationStruct
                {
                    LocationIdx = superFlag.PosIndex,
                    MapId = superFlag.MapId,
                    X = (int)superFlag.MapX,
                    Y = (int)superFlag.MapY,
                    Name = superFlag.Name
                });
            }
            return user.SendAsync(msg);
        }

        #endregion

        #region Durability

        public int GetRecoverDurCost()
        {
            if (Durability > 0 && Durability < MaximumDurability)
            {
                var price = (int)itemType.Price;
                double qualityMultiplier = 0;

                switch (Type % 10)
                {
                    case 9:
                        qualityMultiplier = 1.125;
                        break;
                    case 8:
                        qualityMultiplier = 0.975;
                        break;
                    case 7:
                        qualityMultiplier = 0.9;
                        break;
                    case 6:
                        qualityMultiplier = 0.825;
                        break;
                    default:
                        qualityMultiplier = 0.75;
                        break;
                }

                return (int)Math.Ceiling(price * ((MaximumDurability - Durability) / (float)MaximumDurability) * qualityMultiplier);
            }

            return 0;
        }

        public async Task<bool> RecoverDurabilityAsync()
        {
            MaximumDurability = OriginalMaximumDurability;
            await user.SendAsync(new MsgItemInfo(this, MsgItemInfo.ItemMode.Update));
            await SaveAsync();
            return true;
        }

        private static readonly ILogger repairItemLogger = Logger.CreateLogger("repair_item");
        public async Task RepairItemAsync()
        {
            if (user == null)
            {
                return;
            }

            if (!IsEquipment() && !IsWeapon())
            {
                return;
            }

            if (IsBroken())
            {
                if (!await user.UserPackage.SpendMeteorsAsync(5))
                {
                    await user.SendAsync(StrItemErrRepairMeteor);
                    return;
                }

                Durability = MaximumDurability;
                await SaveAsync();
                await user.SendAsync(new MsgItemInfo(this, MsgItemInfo.ItemMode.Update));
                repairItemLogger.Information(string.Format("User [{2}] repaired broken [{0}][{1}] with 5 meteors.", Type, Identity, PlayerIdentity));
                return;
            }

            if (Durability > MaximumDurability)
            {
                Durability = MaximumDurability;
                await SaveAsync();
                await user.SendAsync(new MsgItemInfo(this, MsgItemInfo.ItemMode.Update));
                return;
            }

            var nRecoverDurability = (ushort)(Math.Max(0u, MaximumDurability) - Durability);
            if (nRecoverDurability == 0)
            {
                return;
            }

            int nRepairCost = GetRecoverDurCost() - 1;
            if (!await user.SpendMoneyAsync(Math.Max(1, nRepairCost), true))
            {
                return;
            }

            Durability = MaximumDurability;
            await SaveAsync();
            await user.SendAsync(new MsgItemInfo(this, MsgItemInfo.ItemMode.Update));
            repairItemLogger.Information(string.Format("User [{2}] repaired broken [{0}][{1}] with {3} silvers.", Type, Identity, PlayerIdentity, nRepairCost));
        }

        #endregion

        #region Database

        public Task<bool> SaveAsync()
        {
            if (Identity == 0)
            {
                return ServerDbContext.CreateAsync(item);
            }

            item.ChkSum = CalculateCheckSum(item);
            return ServerDbContext.UpdateAsync(item);
        }

        public Task<bool> DeleteAsync()
        {
            if (Identity == 0)
            {
                return Task.FromResult(false);
            }

            if (user != null)
            {
                user.LastDelItemIdentity = Identity;
            }
            //item.OwnerId = 0;
            //item.PlayerId = 0;
            //return SaveAsync();
            deleteItemLogger.Information($"{Identity},{Name},{OwnerIdentity},{PlayerIdentity},{Durability},{MaximumDurability},{OriginalMaximumDurability},{Plus},{SocketOne},{SocketTwo},{SocketProgress},{Effect},{ReduceDamage},{Enchantment}");
            // todo delete item status?
            return ServerDbContext.DeleteAsync(item);
        }

		#endregion

		#region Socket

		public async Task SendJarAsync()
		{
			if (user == null)
			{
				return;
			}

			MsgInteract msg = new()
			{
				Action = MsgInteractType.Chop,
				SenderIdentity = PlayerIdentity,
				TargetIdentity = Identity,
				PosX = MaximumDurability,
				Command = (int)Data * 2
			};
			await user.SendAsync(msg);
		}

		#endregion

		#region Quench

		public ItemQuench Quench { get; set; }

		#endregion

		#region Static Getters
		public static int AdditionPoints(Item item)
		{
			int points = 0;
			for (int i = 0; i < item.Plus; i++)
			{
				points += MsgDataArray.GetAddLevelExp((uint)i, item.IsMount());
			}
			if (item.Plus >= 12 && item.IsMount())
			{
				points += (int)item.CompositionProgress;
			}
			return points;
		}
		public static uint CalculateCheckSum(DbItem item)
        {
            return CalculateCheckSum(item.Id, item.Type, item.AmountLimit, item.Gem1, item.Gem2, item.Magic1, item.Magic3, item.Data, item.ReduceDmg, item.AddLife, item.AddlevelExp);
        }

        private static uint CalculateCheckSum(params uint[] values)
        {
            uint result = 0;
            foreach (uint value in values)
            {
                result ^= value;
                result = (result + 0x7ed55d16) + (result << 12);
                result = (result ^ 0xc761c23c) ^ (result >> 19);
                result = (result + 0x165667b1) + (result << 5);
                result = (result + 0xd3a2646c) ^ (result << 9);
                result = (result + 0xfd7046c5) + (result << 3);
                result = (result ^ 0xb55a4f09) ^ (result >> 16);
            }
            return result;
        }

        public static DbItem CreateEntity(uint type, bool bound = false)
        {
            DbItemtype itemtype = ItemManager.GetItemtype(type);
            if (itemtype == null)
            {
                return null;
            }

            DbItem entity = new()
            {
                Magic1 = (byte)itemtype.Magic1,
                Magic2 = itemtype.Magic2,
                Magic3 = itemtype.Magic3,
                Type = type,
                Amount = itemtype.Amount,
                AmountLimit = itemtype.AmountLimit,
                Gem1 = itemtype.Gem1,
                Gem2 = itemtype.Gem2,
                Monopoly = (byte)(bound ? 3 : 0),
                Color = (byte)ItemColor.Orange,
                AccumulateNum = 1
            };
            return entity;
        }

        public static int CalculateGemPercentage(SocketGem gem)
        {
            switch (gem)
            {
                case SocketGem.NormalTortoiseGem:
                    return 2;
                case SocketGem.RefinedTortoiseGem:
                    return 4;
                case SocketGem.SuperTortoiseGem:
                    return 6;
                case SocketGem.NormalDragonGem:
                case SocketGem.NormalPhoenixGem:
                case SocketGem.NormalFuryGem:
                    return 5;
                case SocketGem.RefinedDragonGem:
                case SocketGem.RefinedPhoenixGem:
                case SocketGem.NormalRainbowGem:
                case SocketGem.RefinedFuryGem:
                    return 10;
                case SocketGem.SuperDragonGem:
                case SocketGem.SuperPhoenixGem:
                case SocketGem.RefinedRainbowGem:
                case SocketGem.SuperFuryGem:
                case SocketGem.NormalMoonGem:
                    return 15;
                case SocketGem.SuperRainbowGem:
                    return 25;
                case SocketGem.NormalVioletGem:
                case SocketGem.RefinedMoonGem:
                    return 30;
                case SocketGem.RefinedVioletGem:
                case SocketGem.SuperMoonGem:
                case SocketGem.NormalKylinGem:
                    return 50;
                case SocketGem.RefinedKylinGem:
                case SocketGem.SuperVioletGem:
                    return 100;
                case SocketGem.SuperKylinGem:
                    return 200;
                case SocketGem.NormalThunderGem:
                case SocketGem.NormalGloryGem:
                    return 100;
                case SocketGem.RefinedThunderGem:
                case SocketGem.RefinedGloryGem:
                    return 300;
                case SocketGem.SuperThunderGem:
                case SocketGem.SuperGloryGem:
                    return 500;
                default:
                    return 0;
            }
        }

        public int GetLevel()
        {
            return GetLevel(Type);
        }

        public static int GetLevel(uint type)
        {
            return (int)type % 1000 / 10;
        }

        public static bool IsGem(uint type)
        {
            return GetItemSubType(type) == 700;
        }

        public bool IsGem()
        {
            return GetItemSubType() == 700;
        }

        public ItemSort GetItemSort()
        {
            return GetItemSort(Type);
        }

        public static ItemSort GetItemSort(uint type)
        {
            return (ItemSort)(type % 10000000 / 100000);
        }

        public int GetItemtype()
        {
            return GetItemtype(Type);
        }

        public static int GetItemtype(uint type)
        {
            if (GetItemSort(type) == ItemSort.ItemsortWeaponSingleHand
                || GetItemSort(type) == ItemSort.ItemsortWeaponDoubleHand)
            {
                return (int)(type % 100000 / 1000 * 1000);
            }

            return (int)(type % 100000 / 10000 * 10000);
        }

        public int GetItemSubType()
        {
            return GetItemSubType(Type);
        }

        public static int GetItemSubType(uint type)
        {
            return (int)(type % 1000000 / 1000);
        }

        public int GetQuality() { return GetQuality(Type); }

        public static int GetQuality(uint type)
        {
            return (int)(type % 10);
        }

        public bool IsCountable()
        {
            return MaxAccumulateNum > 1;
        }

        public bool IsPileEnable()
        {
            return IsExpend() && MaxAccumulateNum > 1;
        }

        public bool IsMedicine()
        {
            return IsMedicine(Type);
        }

        public static bool IsMedicine(uint type)
        {
            return type >= 1000000 && type <= 1049999;
        }

        public bool IsEquipEnable()
        {
            return IsEquipment() || IsArrowSort() || IsGourd() || IsGarment() || IsTalisman() || IsMount() || IsMountArmor() || IsAccessory();
        }

        public bool IsExpend() => IsExpend(Type);

        public static bool IsExpend(uint type)
        {
            return IsArrowSort(type)
                   || GetItemSort(type) == ItemSort.ItemsortUsable
                   || GetItemSort(type) == ItemSort.ItemsortUsable2
                   || GetItemSort(type) == ItemSort.ItemsortUsable3
                   || GetItemSort(type) == (ItemSort)30;
        }

        public bool IsArrowSort()
        {
            return IsArrowSort(Type);
        }

        public static bool IsArrowSort(uint type)
        {
            return GetItemtype(type) == 50000 && type != TYPE_JAR && !IsRing(type) && !IsBangle(type);
        }

        public bool IsHelmet() => IsHelmet(Type);

		public static bool IsHelmet(uint type)
		{
			return type >= 110000 && type < 120000 || type >= 140000 && type < 150000 || type >= 123000 && type < 124000 || type >= 170000 && type < 180000;
		}

		public bool IsNeck() => IsNeck(Type);

        public static bool IsNeck(uint type)
        {
            return type >= 120000 && type < 123000;
        }

        public bool IsRing() => IsRing(Type);

        public static bool IsRing(uint type)
        {
            return type >= 150000 && type < 152000;
        }

        public bool IsBangle() => IsBangle(Type);

        public static bool IsBangle(uint type)
        {
            return type >= 152000 && type < 153000;
        }

        public bool IsWeaponOneHand() => IsWeaponOneHand(Type);

        public static bool IsWeaponOneHand(uint type)
        {
            return GetItemSort(type) == ItemSort.ItemsortWeaponSingleHand;
        } // single hand use

        public bool IsWeaponTwoHand() => IsWeaponTwoHand(Type);

        public static bool IsWeaponTwoHand(uint type)
        {
            return GetItemSort(type) == ItemSort.ItemsortWeaponDoubleHand;
        } // two hand use

        public bool IsWeaponProBased() => IsWeaponProBased(Type);

        public static bool IsWeaponProBased(uint type)
        {
            return GetItemSort(type) == ItemSort.ItemsortWeaponProfBased;
        } // professional hand use

        public bool IsWeapon() => IsWeapon(Type);

        public static bool IsWeapon(uint type)
        {
            return IsWeaponOneHand(type) || IsWeaponTwoHand(type) || IsWeaponProBased(type);
        }

        public bool IsBackswordType()
        {
            uint subType = Type / 1000;
            return subType == 421 || subType == 620;
        }

        public static bool IsHossuType(uint type)
        {
            return type / 1000 == 619;
        }

        public bool IsHossuType()
        {
            return IsHossuType(Type);
        }

        public bool IsAssassinKnife() => IsAssassinKnife(Type);

        public static bool IsAssassinKnife(uint type)
        {
            return GetItemSubType(type) == 613;
        }

        public bool IsRapier() => IsRapier(Type);

        public static bool IsRapier(uint type)
        {
            return GetItemSubType(type) == 611;
        }

        public bool IsPistol() => IsPistol(Type);

        public static bool IsPistol(uint type)
        {
            return GetItemSubType(type) == 612;
        }

        public bool IsShield() => IsShield(Type);

        public static bool IsShield(uint nType)
        {
            return nType / 1000 == 900;
        }

        public bool IsBow() => IsBow(Type);

        public static bool IsBow(uint type)
        {
            return GetItemSubType(type) == 500;
        }

        public bool IsArmor() => IsArmor(Type);

		public static bool IsArmor(uint type)
		{
			return (type / 10000 == 13 || type / 10000 == 10);
		}

		public bool IsShoes() => IsShoes(Type);

        public static bool IsShoes(uint type)
        {
            return type >= 160000 && type < 161000;
        }

        public bool IsGourd() => IsGourd(Type);

        public static bool IsGourd(uint type)
        {
            return type >= 2100000 && type < 2200000;
        }

        public bool IsGarment() => IsGarment(Type);

		public static bool IsGarment(uint type)
		{
			return type >= 180000 && type < 200000;
		}

		public bool IsArtifact() => IsArtifact(Type);

        public static bool IsArtifact(uint type)
        {
            return type >= 800000 && type < 900000;
        }

        public static bool IsRefinery(uint type)
        {
            return ItemManager.IsValidRefinery(type);
        }

        public bool IsRefinery()
        {
            return IsRefinery(Type);
        }

        public static bool IsMount(uint type)
        {
            return type == 300000;
        }

        public bool IsMount()
        {
            return IsMount(Type);
        }

        public static bool IsMountArmor(uint type)
        {
            return type / 1000 == 200;
        }

        public bool IsMountArmor()
        {
            return IsMountArmor(Type);
        }

        public static bool IsAccessory(uint type)
        {
            return IsOneHandedAccessory(type) || IsTwoHandedAccessory(type) || IsBowAccessory(type) || IsShieldAccessory(type);
        }

        public bool IsAccessory()
        {
            return IsAccessory(Type);
        }

        public static bool IsOneHandedAccessory(uint type)
        {
            return GetItemSubType(type) == 360;
        }

        public bool IsOneHandedAccessory()
        {
            return IsOneHandedAccessory(Type);
        }

        public static bool IsTwoHandedAccessory(uint type)
        {
            return GetItemSubType(type) == 350;
        }

        public bool IsTwoHandedAccessory()
        {
            return IsTwoHandedAccessory(Type);
        }

        public static bool IsBowAccessory(uint type)
        {
            return GetItemSubType(type) == 370;
        }

        public bool IsBowAccessory()
        {
            return IsBowAccessory(Type);
        }

        public static bool IsShieldAccessory(uint type)
        {
            return GetItemSubType(type) == 380;
        }

        public bool IsShieldAccessory()
        {
            return IsShieldAccessory(Type);
        }

        public static bool IsTalisman(uint type)
        {
            return IsAttackTalisman(type) || IsDefenseTalisman(type) || IsCrop(type) || IsWing(type);
        }

        public bool IsTalisman()
        {
            return IsTalisman(Type);
        }

        public static bool IsAttackTalisman(uint type)
        {
            return type >= 201000 && type < 202000;
        }

        public bool IsAttackTalisman()
        {
            return IsAttackTalisman(Type);
        }

        public static bool IsDefenseTalisman(uint type)
        {
            return type >= 202000 && type < 203000;
        }

        public bool IsDefenseTalisman()
        {
            return IsDefenseTalisman(Type);
        }

        public static bool IsCrop(uint type)
        {
            return type >= 203000 && type < 204000;
        }

        public bool IsCrop()
        {
            return IsCrop(Type);
        }

        public static bool IsWing(uint type)
        {
            return type >= 204000 && type < 205000;
        }

        public bool IsWing()
        {
            return IsWing(Type);
        }

        public static bool IsEquipment(uint type)
        {
            return IsHelmet(type) || IsNeck(type) || IsRing(type) || IsBangle(type) || IsWeapon(type) || IsArmor(type) || IsShoes(type) || IsShield(type) || IsTalisman(type) || IsHossuType(type);
        }

        public bool IsEquipment()
        {
            return IsEquipment(Type);
        }

		public static async Task<MapItemInfo> CreateItemInfoAsync(DbMonstertype monstertype, int quality)
		{
			if (monstertype == null)
			{
				return default;
			}

			int rand;
			if (quality == 0)
			{
				rand = await NextAsync(100);
				if (rand >= 0 && rand < 30)
				{
					quality = 5;
				}
				else if (rand >= 30 && rand < 70)
				{
					quality = 4;
				}
				else
				{
					quality = 3;
				}
			}

			rand = await NextAsync(1250);
			var itemSort = 0;
			var itemLevel = 0;
			var itemColor = ItemColor.Orange;
			if (rand >= 0 && rand < 20)
			{
				// shoes
				itemSort = 160;
				itemLevel = monstertype.DropShoes;
			}
			else if (rand >= 20 && rand < 50)
			{
				// necklace
				int[] necks =
				{
					120, 121
				};
				itemSort = necks[await NextAsync(necks.Length) % necks.Length];
				itemLevel = monstertype.DropNecklace;
			}
			else if (rand >= 50 && rand < 100)
			{
				// ring
				int[] rings =
				{
					150, 151, 152
				};
				itemSort = rings[await NextAsync(rings.Length) % rings.Length];
				itemLevel = monstertype.DropRing;
			}
			else if (rand >= 100 && rand < 400)
			{
				// armet
				int[] armets =
				{
					111, 112, 113, 114, 117, 118, 123, 141, 142, 143, 144, 145
				};
				itemSort = armets[await NextAsync(armets.Length) % armets.Length];
				itemLevel = monstertype.DropArmet;
			}
			else if (rand >= 400 && rand < 700)
			{
				// armor
				int[] armors =
				{
					130, 131, 133, 134, 135, 136, 139
				};
				itemSort = armors[await NextAsync(armors.Length) % armors.Length];
				itemLevel = monstertype.DropArmet;
			}
			else if (rand >= 700 && rand < 1200)
			{
				// weapon & shield
				rand = await NextAsync(100);
				if (rand >= 0 && rand < 20) // backsword
				{
					itemSort = 421;
					itemLevel = monstertype.DropWeapon;
				}
				else if (rand >= 20 && rand < 40) // archer
				{
					itemSort = 500;
					itemLevel = monstertype.DropWeapon;
				}
				else if (rand >= 40 && rand < 60) // one handed
				{
					// weapons
					int[] weapons =
					{
						410, 420, 421, 422, 430, 440, 450, 460, 480, 481, 490, 601, 610, 611, 612, 613
					};
					itemSort = weapons[await NextAsync(weapons.Length) % weapons.Length];
					itemLevel = monstertype.DropWeapon;
				}
				else if (rand >= 60 && rand < 80) // two handed
				{
					// weapons
					int[] weapons =
					{
						510, 511, 530, 540, 560, 561, 580
					};
					itemSort = weapons[await NextAsync(weapons.Length) % weapons.Length];
					itemLevel = monstertype.DropWeapon;
				}
				else // shield
				{
					itemSort = 900;
					itemLevel = monstertype.DropShield;
					itemColor = (ItemColor)await NextAsync(6) + 3;
				}
			}
			else
			{
				if (monstertype.Level < 70)
				{
					return default;
				}

				// talismans
				int[] talismans =
				{
					201, 202
				};
				itemSort = talismans[await NextAsync(talismans.Length) % talismans.Length];
				itemLevel = 0;
			}

			if (itemLevel == 99)
			{
				return default;
			}

			rand = await NextAsync(100);
			if (rand < 50) // down one lev
			{
				int randLev = await NextAsync(itemLevel / 2);
				itemLevel = randLev + itemLevel / 3;

				if (itemLevel >= 1)
				{
					itemLevel--;
				}
			}
			else if (rand >= 80) // up one lev
			{
				if (itemSort >= 110 && itemSort <= 119
					|| itemSort >= 130 && itemSort <= 139
					|| itemSort >= 900 && itemSort <= 999)
				{
					itemLevel = Math.Min(itemLevel + 1, 9);
				}
				else
				{
					itemLevel = Math.Min(itemLevel + 1, 23);
				}
			}

			int idItemType = itemSort * 1000 + itemLevel * 10 + quality;
			DbItemtype itemtype = ItemManager.GetItemtype((uint)idItemType);
			if (itemtype == null)
			{
				return default;
			}

			ushort amount;
			var amountLimit = (ushort)Math.Max(1, itemtype.AmountLimit * await NextRateAsync(0.3d));
			if (quality > 5)
			{
				amount = (ushort)(amountLimit * (15 + await NextAsync(20)) / 100);
			}
			else
			{
				amount = (ushort)(amountLimit * (15 + await NextAsync(35)) / 100);
			}

			var socketNum = 0;
			if (itemSort >= 400 && itemSort < 700)
			{
				rand = await NextAsync(100);
				if (rand < 5)
				{
					socketNum = 2;
				}
				else if (rand < 20)
				{
					socketNum = 1;
				}
			}

			var addition = 0;
			rand = await NextAsync(1000);
			if (rand < 15)
			{
				addition = 1;
			}

			var reduceDamage = 0;
			if (itemSort != 201 && itemSort != 202)
			{
				rand = await NextAsync(1000);
				if (rand < 20)
				{
					reduceDamage = 5;
				}
				else if (rand < 50)
				{
					reduceDamage = 3;
				}
			}

			return new MapItemInfo
			{
				Type = itemtype.Type,
				Addition = (byte)addition,
				Color = itemColor,
				MaximumDurability = amountLimit,
				Durability = amount,
				ReduceDamage = (byte)reduceDamage,
				SocketNum = (byte)socketNum
			};
		}

		#endregion

		#region Enums

		public enum ItemSort
        {
            ItemsortFinery = 1,
            ItemsortMount = 3,
            ItemsortWeaponSingleHand = 4,
            ItemsortWeaponDoubleHand = 5,
            ItemsortWeaponProfBased = 6,
            ItemsortUsable = 7,
            ItemsortWeaponShield = 9,
            ItemsortUsable2 = 10,
            ItemsortUsable3 = 12,
            ItemsortAccessory = 3,
            ItemsortTwohandAccessory = 35,
            ItemsortOnehandAccessory = 36,
            ItemsortBowAccessory = 37,
            ItemsortShieldAccessory = 38
        }

        public enum ItemEffect : ushort
        {
            None = 0,
            Poison = 200,
            Life = 201,
            Mana = 202,
            Shield = 203,
            Horse = 100
        }

        public enum SocketGem : byte
        {
            NormalPhoenixGem = 1,
            RefinedPhoenixGem = 2,
            SuperPhoenixGem = 3,

            NormalDragonGem = 11,
            RefinedDragonGem = 12,
            SuperDragonGem = 13,

            NormalFuryGem = 21,
            RefinedFuryGem = 22,
            SuperFuryGem = 23,

            NormalRainbowGem = 31,
            RefinedRainbowGem = 32,
            SuperRainbowGem = 33,

            NormalKylinGem = 41,
            RefinedKylinGem = 42,
            SuperKylinGem = 43,

            NormalVioletGem = 51,
            RefinedVioletGem = 52,
            SuperVioletGem = 53,

            NormalMoonGem = 61,
            RefinedMoonGem = 62,
            SuperMoonGem = 63,

            NormalTortoiseGem = 71,
            RefinedTortoiseGem = 72,
            SuperTortoiseGem = 73,

            NormalThunderGem = 101,
            RefinedThunderGem = 102,
            SuperThunderGem = 103,

            NormalGloryGem = 121,
            RefinedGloryGem = 122,
            SuperGloryGem = 123,

            NoSocket = 0,
            EmptySocket = 255
        }

        public enum ItemPosition : ushort
        {
            Inventory = 0,
            EquipmentBegin = 1,
            Headwear = 1,
            Necklace = 2,
            Armor = 3,
            RightHand = 4,
            LeftHand = 5,
            Ring = 6,
            Gourd = 7,
            Boots = 8,
            Garment = 9,
            AttackTalisman = 10,
            DefenceTalisman = 11,
            Mount = 12,
            RightHandAccessory = 15,
            LeftHandAccessory = 16,
            MountArmor = 17,
            Crop = 18,
            Wing = 19,
            EquipmentEnd = Wing,

            SecondaryEquipmentBegin = 21,
            SecondaryHeadwear = 21,
            SecondaryNecklace = 22,
            SecondaryArmor = 23,
            SecondaryRightHand = 24,
            SecondaryLeftHand = 25,
            SecondaryRing = 26,
            SecondaryGourd = 27,
            SecondaryBoots = 28,
            SecondaryGarment = 29,
            SecondaryAttackTalisman = 30,
            SecondaryDefenseTalisman = 31,
            SecondaryMount = 32,
            SecondaryEquipmentEnd = SecondaryMount,

            UserLimit = 199,

            /// <summary>
            ///     Warehouse
            /// </summary>
            Storage = 201,

            /// <summary>
            ///     House WH
            /// </summary>
            Trunk = 202,

            /// <summary>
            ///     Sashes
            /// </summary>
            Chest = 203,
            ChestPackage = 204,
            CoatStorage = 209,
            Auction = 210,

            Detained = 250,
            Floor = 254
        }

        public enum ItemColor : byte
        {
            None,
            Black = 2,
            Orange = 3,
            LightBlue = 4,
            Red = 5,
            Blue = 6,
            Yellow = 7,
            Purple = 8,
            White = 9
        }

        public enum ChangeOwnerType : byte
        {
            DropItem,
            PickupItem,
            TradeItem,
            CreateItem,
            DeleteItem,
            ItemUsage,
            DeleteDroppedItem,
            InvalidItemType,
            BoothSale,
            ClearInventory,
            DetainEquipment
        }

        #endregion

        public string ToJson()
        {
            return JsonConvert.SerializeObject(item);
        }

        public override string ToString()
        {
            return FullName;
        }

        private readonly uint[] TALISMAN_SOCKET_QUALITY_ADDITION = { 0, 0, 0, 0, 0, 0, 5, 10, 40, 1000 };

        private readonly uint[] TALISMAN_SOCKET_PLUS_ADDITION =
        {
            0, 6, 30, 80, 240, 740, 2220, 6660, 20000, 60000, 62000,
            66000, 72000
        };

        private readonly uint[] TALISMAN_SOCKET_HOLE_ADDITION0 = { 0, 160, 960 };
        private readonly uint[] TALISMAN_SOCKET_HOLE_ADDITION1 = { 0, 2000, 8000 };

        /// <summary>
        /// Item is owned by the holder. Cannot be traded or dropped.
        /// </summary>
        public const int ITEM_MONOPOLY_MASK = 1;
        /// <summary>
        /// Item cannot be stored.
        /// </summary>
        public const int ITEM_STORAGE_MASK = 2;
        /// <summary>
        /// Item cannot be dropped.
        /// </summary>
        public const int ITEM_DROP_HINT_MASK = 4;
        /// <summary>
        /// Item cannot be sold.
        /// </summary>
        public const int ITEM_SELL_HINT_MASK = 8;
        public const int ITEM_NEVER_DROP_WHEN_DEAD_MASK = 16;
        public const int ITEM_SELL_DISABLE_MASK = 32;
        public const int ITEM_STATUS_NONE = 0;
        public const int ITEM_STATUS_NOT_IDENT = 1;
        public const int ITEM_STATUS_CANNOT_REPAIR = 2;
        public const int ITEM_STATUS_NEVER_DAMAGE = 4;
        public const int ITEM_STATUS_MAGIC_ADD = 8;

        //
        public const uint TYPE_DRAGONBALL = 1088000;
        public const uint TYPE_METEOR = 1088001;
        public const uint TYPE_METEORTEAR = 1088002;
        public const uint TYPE_TOUGHDRILL = 1200005;

        public const uint TYPE_STARDRILL = 1200006;
        public const uint TYPE_SUPER_TORTOISE_GEM = 700073;

        //
        public const uint TYPE_DRAGONBALL_SCROLL = 720028; // Amount 10
        public const uint TYPE_METEOR_SCROLL = 720027; // Amount 10

        public const uint TYPE_METEORTEAR_PACK = 723711; // Amount 5

        //
        public const uint TYPE_STONE1 = 730001;
        public const uint TYPE_STONE2 = 730002;
        public const uint TYPE_STONE3 = 730003;
        public const uint TYPE_STONE4 = 730004;
        public const uint TYPE_STONE5 = 730005;
        public const uint TYPE_STONE6 = 730006;
        public const uint TYPE_STONE7 = 730007;

        public const uint TYPE_STONE8 = 730008;

        //
        public const uint TYPE_MOUNT_ID = 300000;

        //
        public const uint TYPE_EXP_BALL = 723700;
        public const uint TYPE_EXP_POTION = 723017;

        public static readonly int[] BowmanArrows =
        {
            1050000, 1050001, 1050002, 1050020, 1050021, 1050022, 1050023, 1050030, 1050031, 1050032, 1050033, 1050040,
            1050041, 1050042, 1050043, 1050050, 1050051, 1050052
        };

        public const uint IRON_ORE = 1072010;
        public const uint COPPER_ORE = 1072020;
        public const uint EUXINITE_ORE = 1072031;
        public const uint SILVER_ORE = 1072040;
        public const uint GOLD_ORE = 1072050;

        public const uint OBLIVION_DEW = 711083;
        public const uint MEMORY_AGATE = 720828;

        public const uint PERMANENT_STONE = 723694;
        public const uint BIGPERMANENT_STONE = 723695;

        public const int LOTTERY_TICKET = 710212;
        public const uint SMALL_LOTTERY_TICKET = 711504;

        public const uint TYPE_JAR = 750000;

        public const uint SASH_SMALL = 1100003;
        public const uint SASH_MEDIUM = 1100006;
        public const uint SASH_LARGE = 1100009;

        public const uint PROTECTION_PILL = 3002029;
        public const uint SUPER_PROTECTION_PILL = 3002030;

        public const uint FREE_TRAINING_PILL = 3002926;
        public const uint FAVORED_TRAINING_PILL = 3003124;
        public const uint SPECIAL_TRAINING_PILL = 3003125;
        public const uint SENIOR_TRAINING_PILL = 3003126;

        public const uint POWER_ERASER = 3005412;

        public const uint CROSS_BROADCAST_HORN = 3002218;
    }
}
