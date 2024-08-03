using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using ProtoBuf;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgCoatStorage : MsgProtoBufBase<GameClient, MsgCoatStoragePB>
    {
        private static readonly ILogger logger = Log.ForContext<MsgCoatStorage>();

        public MsgCoatStorage()
            : base(PacketType.MsgCoatStorage)
        {
        }

        public MsgCoatStorage(uint idItem, uint idData, Item item, CoatStorageAction action)
            : base(PacketType.MsgCoatStorage)
        {
            Data = new MsgCoatStoragePB()
            {
                Action = (uint)action,
                ItemId = idItem,
                Data = idData,
                Info = new ()
                {
                    ItemId = item.Identity,
                    ItemType = item.Type,
                    Amount = item.Durability,
                    AmountLimit = item.MaximumDurability,
                    Magic3 = item.Plus,
                    ReduceDmg = (uint)item.Blessing,
                    AccumulateNum = item.AccumulateNum,
                    Monopoly = item.Monopoly,
                    RemainSecs = (uint)item.RemainingSeconds,
                    SaveTime = (uint)item.SaveTime
                }
            };
        }

        public MsgCoatStorage(uint idItem, uint idData, uint life, CoatStorageAction action)
            : base(PacketType.MsgCoatStorage)
        {
            Data = new MsgCoatStoragePB()
            {
                Action = (uint)action,
                ItemId = idItem,
                Data = idData,
                Life = life
            };
        }

        public MsgCoatStorage(Item item, CoatStorageAction action)
            : base(PacketType.MsgCoatStorage)
        {
            if (!item.IsGarment() && !item.IsMountArmor())
            {
                throw new ArgumentException("Item must be a garment or mount armor.");
            }

            Data = new MsgCoatStoragePB
            {
                Action = (uint)action,
                Info = new()
                {
                    ItemId = item.Identity,
                    ItemType = item.Type,
                    Amount = item.Durability,
                    AmountLimit = item.MaximumDurability,
                    Magic3 = item.Plus,
                    ReduceDmg = item.ReduceDamage,
                    AccumulateNum = item.AccumulateNum,
                    AddLife = item.Enchantment,
                    Monopoly = item.Monopoly,
                    RemainSecs = (uint)item.RemainingSeconds,
                    SaveTime = (uint)item.SaveTime,
                    Color = (uint)item.Color
                }
            };
        }

        public enum CoatStorageAction
        {
            AddWrapItem = 0,
            PutItemToWrapPackage = 1,
            /// <summary>
            /// Retrieve
            /// </summary>
            DelWrapItem = 2,
            WrapCombine = 4,
            EquipWrap = 5,
            UnequipWrap = 6,
            UpdateWrapViewItem = 7,
            DelWrapItem2 = 8
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch ((CoatStorageAction)Data.Action)
            {
                case CoatStorageAction.PutItemToWrapPackage:
                case CoatStorageAction.EquipWrap:
                    {
                        if (user.CoatStorage.IsCoatStored(Data.ItemId))
                        {
                            await user.CoatStorage.EquipCoatAsync(Data.ItemId);
                            return;
                        }

                        Item item = user.UserPackage.FindItemByIdentity(Data.ItemId);
                        if (item == null)
                        {
                            return;
                        }

                        if (user.CoatStorage.IsCoatTypeStored(item.Type))
                        {
                            return;
                        }

                        if (await user.CoatStorage.AddToCoatStorageAsync(item))
                        {
                            LuaScriptManager.Run(user, user, item, [], $"return UserCheckInItemToCoatStorage({user.Identity},{Data.ItemId})");
                        }

                        break;
                    }

                case CoatStorageAction.DelWrapItem:
                    {
                        if (!user.CoatStorage.GetFromCoatStorage(Data.ItemId, out var item))
                        {
                            return;
                        }

                        if (await user.CoatStorage.RetrieveFromCoatStorageAsync(Data.ItemId, true))
                        {
                            LuaScriptManager.Run(user, user, item, [], $"return UserCheckOutItemFromCoatStorage({user.Identity},{Data.ItemId})");
                        }
                        break;
                    }

                case CoatStorageAction.WrapCombine:
                    {
                        if (await user.CoatStorage.CombineCoatAsync(Data.ItemId, Data.Data))
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }

                case CoatStorageAction.UnequipWrap:
                    {
                        Item.ItemPosition itemPosition;
                        if (user.Garment?.Identity == Data.ItemId)
                        {
                            itemPosition = Item.ItemPosition.Garment;
                        }
                        else if (user.GetEquipment(Item.ItemPosition.MountArmor)?.Identity == Data.ItemId)
                        {
                            itemPosition = Item.ItemPosition.MountArmor;
                        }
                        else return;

                        if (await user.CoatStorage.UnequipCoatAsync(itemPosition))
                        {
                            await user.SendAsync(this);
                        }
                        break;
                    }

                default:
                    {
                        logger.Warning("MsgCoatStorage {0} not handled.\n" + Encode(), (CoatStorageAction)Data.Action);
                        return;
                    }
            }
        }
    }

    [ProtoContract]
    public struct MsgCoatStoragePB
    {
        [ProtoMember(1, IsRequired = true)]
        public uint Action { get; set; }
        [ProtoMember(2, IsRequired = false)]
        public uint ItemId { get; set; }
        [ProtoMember(3, IsRequired = false)]
        public uint Data { get; set; }
        [ProtoMember(4, IsRequired = true)]
        public uint Life { get; set; }
        [ProtoMember(5, IsRequired = false)]
        public MsgCoatItemInfoPB Info { get; set; }
    }

    [ProtoContract]
    public struct MsgCoatItemInfoPB
    {
        [ProtoMember(1, IsRequired = true)]
        public uint ItemId { get; set; }
        [ProtoMember(2, IsRequired = true)]
        public uint ItemType { get; set; }
        [ProtoMember(3, IsRequired = true)]
        public uint Ident { get; set; }
        [ProtoMember(4, IsRequired = true)]
        public uint Gem1 { get; set; }
        [ProtoMember(5, IsRequired = true)]
        public uint Gem2 { get; set; }
        [ProtoMember(6, IsRequired = true)]
        public uint Magic1 { get; set; }
        [ProtoMember(7, IsRequired = true)]
        public uint Magic2 { get; set; }
        [ProtoMember(8, IsRequired = true)]
        public uint Magic3 { get; set; }
        [ProtoMember(9, IsRequired = true)]
        public uint ReduceDmg { get; set; }
        [ProtoMember(10, IsRequired = true)]
        public uint Monopoly { get; set; }
        [ProtoMember(11, IsRequired = true)]
        public uint AddLife { get; set; }
        [ProtoMember(12, IsRequired = true)]
        public uint AntiMonster { get; set; }
        [ProtoMember(13, IsRequired = true)]
        public uint Plunder { get; set; }
        [ProtoMember(14, IsRequired = true)]
        public uint ItemFlag { get; set; }
        [ProtoMember(15, IsRequired = true)]
        public uint Color { get; set; }
        [ProtoMember(16, IsRequired = true)]
        public uint Data { get; set; }
        [ProtoMember(17, IsRequired = true)]
        public uint AddLevExp { get; set; }
        [ProtoMember(18, IsRequired = true)]
        public uint Syndicate { get; set; }
        [ProtoMember(19, IsRequired = true)]
        public uint RemainSecs { get; set; }
        [ProtoMember(20, IsRequired = true)]
        public uint SaveTime { get; set; }
        [ProtoMember(21, IsRequired = true)]
        public uint AccumulateNum { get; set; }
        [ProtoMember(22, IsRequired = true)]
        public uint Amount { get; set; }
        [ProtoMember(23, IsRequired = true)]
        public uint AmountLimit { get; set; }
    }
}
