using Long.Database.Entities;
using Long.Database.Entities.Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Items.Status;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Shared.Managers;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgMapItem;

namespace Long.Kernel.Managers
{
    public class ItemManager
    {
        private static readonly ILogger logger = Log.ForContext<ItemManager>();

        private static ConcurrentDictionary<uint, DbItemtype> itemtypes = new();
        private static ConcurrentDictionary<ulong, DbItemAddition> itemAdditions = new();
        private static ConcurrentDictionary<uint, DbItemLimit> itemLimits = new();
        private static List<uint> validRefineryIds = new();
		public static BaseNpc Confiscator => RoleManager.FindRole<BaseNpc>(4450);

		private static ConcurrentDictionary<int, QuenchInfoData> refineryTypes { get; } = new(new Dictionary<int, QuenchInfoData>
        {
            { 301, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Intensification } },
            { 302, new QuenchInfoData { Attribute1 = ItemStatusAttribute.FinalDamage } },
            { 303, new QuenchInfoData { Attribute1 = ItemStatusAttribute.FinalAttack } },
            { 304, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Detoxication } },
            { 305, new QuenchInfoData { Attribute1 = ItemStatusAttribute.FinalMagicAttack } },
            { 306, new QuenchInfoData { Attribute1 = ItemStatusAttribute.FinalMagicDefense } },
            { 307, new QuenchInfoData { Attribute1 = ItemStatusAttribute.CriticalStrike } },
            { 308, new QuenchInfoData { Attribute1 = ItemStatusAttribute.SkillCriticalStrike } },
            { 309, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Immunity } },
            { 310, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Breakthrough } },
            { 311, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Counteraction } },
            { 312, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Penetration } },
            { 313, new QuenchInfoData { Attribute1 = ItemStatusAttribute.Block } },
            { 314, new QuenchInfoData { Attribute1 = ItemStatusAttribute.MetalResist } },
            { 315, new QuenchInfoData { Attribute1 = ItemStatusAttribute.WoodResist } },
            { 316, new QuenchInfoData { Attribute1 = ItemStatusAttribute.WaterResist } },
            { 317, new QuenchInfoData { Attribute1 = ItemStatusAttribute.FireResist } },
            { 318, new QuenchInfoData { Attribute1 = ItemStatusAttribute.WoodResist } },
            { 319, new QuenchInfoData { Attribute1 = ItemStatusAttribute.MagicDefense } }
        });		

		public static async Task InitializeAsync()
        {
            logger.Information("Starting Item Manager");

            foreach (var item in ItemtypeRepository.Get())
            {
                itemtypes.TryAdd(item.Type, item);
            }

            foreach (var addition in ItemAdditionRepository.Get())
            {
                itemAdditions.TryAdd(AdditionKey(addition.TypeId, addition.Level), addition);
            }

            foreach (var limit in await ItemRepository.GetLimitsAsync())
            {
                itemLimits.TryAdd(limit.Type, limit);
            }

            using StreamReader quenchReader = new(Path.Combine(Environment.CurrentDirectory, "ini", "ItemQuench.ini"));
            string quenchLine;
            while ((quenchLine = await quenchReader.ReadLineAsync()) != null)
            {
                if (uint.TryParse(quenchLine, out uint quenchId) && validRefineryIds.All(x => x != quenchId))
                {
                    validRefineryIds.Add(quenchId);
                }
            }
        }

		public static async Task<bool> DetainItemAsync(Character discharger, Character detainer)
		{
			var items = new List<Item>();
			for (var pos = Item.ItemPosition.EquipmentBegin; pos <= Item.ItemPosition.EquipmentEnd; pos++)
			{
				switch (pos)
				{
					case Item.ItemPosition.Headwear:
					case Item.ItemPosition.Necklace:
					case Item.ItemPosition.Ring:
					case Item.ItemPosition.RightHand:
					case Item.ItemPosition.Armor:
					case Item.ItemPosition.LeftHand:
					case Item.ItemPosition.Boots:
					case Item.ItemPosition.AttackTalisman:
					case Item.ItemPosition.DefenceTalisman:
					case Item.ItemPosition.Crop:
						{
							if (discharger.UserPackage[pos] == null)
							{
								continue;
							}

							if (discharger.UserPackage[pos].IsArrowSort())
							{
								continue;
							}

							if (discharger.UserPackage[pos].IsSuspicious())
							{
								continue;
							}

							items.Add(discharger.UserPackage[pos]);
							continue;
						}
				}
			}

			Item item = items[await NextAsync(items.Count) % items.Count];

			if (item == null)
			{
				return false;
			}

			if (item.IsArrowSort())
			{
				return false;
			}

			if (item.IsMount())
			{
				return false;
			}

			if (item.IsSuspicious())
			{
				return false;
			}

			if (item.PlayerIdentity != discharger.Identity) // item must be owned by the discharger
			{
				return false;
			}

			await discharger.UserPackage.UnEquipAsync(item.Position, UserPackage.RemovalType.RemoveAndDisappear);
			item.Position = Item.ItemPosition.Detained;
			await item.SaveAsync();

			//log.LogInformation($"did:{discharger.Identity},dname:{discharger.Name},detid:{detainer.Identity},detname:{detainer.Name},itemid:{item.Identity},mapid:{discharger.MapIdentity}");

			var dbDetain = new DbDetainedItem
			{
				ItemIdentity = item.Identity,
				TargetIdentity = discharger.Identity,
				TargetName = discharger.Name,
				HunterIdentity = detainer.Identity,
				HunterName = detainer.Name,
				HuntTime = UnixTimestamp.Now,
				RedeemPrice = (ushort)GetDetainPrice(item)
			};
			if (!await ServerDbContext.UpdateAsync(dbDetain))
			{
				return false;
			}

			await discharger.BroadcastRoomMsgAsync(new MsgAction
			{
				Identity = discharger.Identity,
				Data = dbDetain.Identity,
				X = discharger.X,
				Y = discharger.Y,
				Action = ActionType.ItemDetainedEx
			}, true);
			await discharger.SendAsync(new MsgAction
			{
				Identity = discharger.Identity,
				X = discharger.X,
				Y = discharger.Y,
				Action = ActionType.ItemDetained
			});
			long detainFloorId = IdentityManager.MapItem.GetNextIdentity;
			await discharger.BroadcastRoomMsgAsync(new MsgMapItem
			{
				Identity = (uint)detainFloorId,
				Itemtype = item.Type,
				MapX = (ushort)(discharger.X + 2),
				MapY = discharger.Y,
				Mode = DropType.DetainItem
			}, true);
			IdentityManager.MapItem.ReturnIdentity(detainFloorId);

			await discharger.SendAsync(new MsgDetainItemInfo(dbDetain, item, MsgDetainItemInfo.Mode.DetainPage));
			await detainer.SendAsync(new MsgDetainItemInfo(dbDetain, item, MsgDetainItemInfo.Mode.ClaimPage));

			if (Confiscator != null)
			{
				await discharger.SendAsync(string.Format(StrDropEquip, item.Name, detainer.Name, Confiscator.Name, Confiscator.X,
								  Confiscator.Y), TalkChannel.Talk, Color.White);
				await detainer.SendAsync(string.Format(StrKillerEquip, discharger.Name), TalkChannel.Talk, Color.White);
			}

			return true;
		}
		public static int GetDetainPrice(Item item)
		{
			var price = 10;

			if (item.GetQuality() == 9) // if super +500CPs
			{
				price += 50;
			}

			switch (item.Plus) // (+n)
			{
				case 1:
					price += 1;
					break;
				case 2:
					price += 2;
					break;
				case 3:
					price += 5;
					break;
				case 4:
					price += 10;
					break;
				case 5:
					price += 30;
					break;
				case 6:
					price += 90;
					break;
				case 7:
					price += 270;
					break;
				case 8:
					price += 600;
					break;
				case 9:
				case 10:
				case 11:
				case 12:
					price += 1200;
					break;
			}

			if (item.IsWeapon()) // if weapon
			{
				if (item.SocketTwo > Item.SocketGem.NoSocket)
				{
					price += 100;
				}
				else if (item.SocketOne > Item.SocketGem.NoSocket)
				{
					price += 10;
				}
			}
			else // if not
			{
				if (item.SocketTwo > Item.SocketGem.NoSocket)
				{
					price += 150;
				}
				else if (item.SocketOne > Item.SocketGem.NoSocket)
				{
					price += 500;
				}
			}

			//if (item.Quench != null)
			//{
			//	if (item.Quench.GetOriginalArtifact()?.IsPermanent == true)
			//	{
			//		switch (item.Quench.GetOriginalArtifact().ItemStatus.Level)
			//		{
			//			case 1: price += 30; break;
			//			case 2: price += 90; break;
			//			case 3: price += 180; break;
			//			case 4: price += 300; break;
			//			case 5: price += 450; break;
			//			case 6: price += 600; break;
			//			case 7: price += 800; break;
			//		}
			//	}

			//	if (item.Quench.GetOriginalRefinery()?.IsPermanent == true)
			//	{
			//		switch (item.Quench.GetOriginalRefinery().ItemStatus.Level)
			//		{
			//			case 1: price += 30; break;
			//			case 2: price += 90; break;
			//			case 3: price += 200; break;
			//			case 4: price += 400; break;
			//			case 5: price += 600; break;
			//		}
			//	}
			//}

			return price * 5;
		}
		public static bool IsMeteorLevelUpgrade(Item item)
        {
            if (!itemLimits.TryGetValue((uint)item.GetItemSubType(), out var itemLimit))
            {
                return false;
            }
            return item.GetLevel() + 1 < itemLimit.LimitLevel;
        }

        public static List<DbItemtype> GetByRange(int mobLevel, int tolerationMin, int tolerationMax, int maxLevel = 120)
        {
            return itemtypes.Values.Where(x =>
                x.ReqLevel >= mobLevel - tolerationMin && x.ReqLevel <= mobLevel + tolerationMax &&
                x.ReqLevel <= maxLevel).ToList();
        }

        public static DbItemtype GetItemtype(uint type)
        {
            return itemtypes.TryGetValue(type, out var item) ? item : null;
        }

        public static DbItemAddition GetItemAddition(uint type, byte level)
        {
            return itemAdditions.TryGetValue(AdditionKey(type, level), out var item) ? item : null;
        }

        public static bool IsValidRefinery(uint id)
        {
            return validRefineryIds.Any(x => x == id);
        }

        public static bool GetQuenchInfoData(int quenchType, out QuenchInfoData data)
        {
            return refineryTypes.TryGetValue(quenchType, out data);
        }

        private static ulong AdditionKey(uint type, byte level)
        {
            uint key = type;
            Item.ItemSort sort = Item.GetItemSort(type);
            if (sort == Item.ItemSort.ItemsortWeaponSingleHand && Item.GetItemSubType(type) != 421)
            {
                key = type / 100000 * 100000 + type % 1000 + 44000 - type % 10;
            }
            else if (sort == Item.ItemSort.ItemsortWeaponDoubleHand && !Item.IsBow(type))
            {
                key = type / 100000 * 100000 + type % 1000 + 55000 - type % 10;
            }
            else
            {
                key = type / 1000 * 1000 + (type % 1000 - type % 10);
            }

            return (key + ((ulong)level << 32));
        }
		public struct QuenchInfoData
        {
            public ItemStatusAttribute Attribute1 { get; init; }
            public ItemStatusAttribute Attribute2 { get; init; }
        }
    }
}
