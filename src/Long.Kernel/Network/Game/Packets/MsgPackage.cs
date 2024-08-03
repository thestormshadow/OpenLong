using Long.Kernel.Managers;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using Org.BouncyCastle.Bcpg;

namespace Long.Kernel.Network.Game.Packets
{
	public sealed class MsgPackage : MsgBase<GameClient>
	{
		public MsgPackage()
		{
		}

		public MsgPackage(Item item, WarehouseMode action, StorageType mode)
		{
			Identity = item.OwnerIdentity;
			Action = action;
			Mode = mode;
			if (action == WarehouseMode.CheckOut)
			{
				Param = item.Identity;
			}
			else
			{
				Items = new List<WarehouseItem>
				{
					new WarehouseItem
					{
						Identity = item.Identity,
						Type = item.Type,
						SocketOne = (byte)item.SocketOne,
						SocketTwo = (byte)item.SocketTwo,
						Blessing = (byte) item.Blessing,
						Enchantment = item.Enchantment,
						Magic1 = (byte)item.Effect,
						Magic3 = item.Plus,
						Color = (byte)item.Color,
						Locked = item.IsLocked(),
						Bound = item.IsBound,
						Accumulate = (int)item.AccumulateNum,
						CompositionProgress = item.CompositionProgress,
						Inscribed = item.SyndicateIdentity != 0 ? 1 : 0,
						SocketProgress = item.SocketProgress,
						Suspicious = item.IsSuspicious(),
						Durability = item.Durability,
						MaximumDurability = item.MaximumDurability,
						ActivationTime = item.RemainingSeconds > 0 ? 0 : item.SaveTime,
						RemainingTime = item.RemainingSeconds,
						PerfectionLevel = item.PerfectionLevel,
						PerfectionProgress = item.PerfectionProgress,
						PerfectionOwnerId = item.PerfectionOwnerGuid,
						PerfectionOwnerName = item.PerfectionOwnerName??"PRUEBA",
						PerfectionOwnerSignature = item.PerfectionSignature??"PRUEBA"
					}
				};
			}
		}

		public List<WarehouseItem> Items = new();
		public uint Identity { get; set; }
		public WarehouseMode Action { get; set; }
		public StorageType Mode { get; set; }
		public ushort Unknown { get; set; }
		public int Data { get; set; }
		public uint Param { get; set; }

		public override void Decode(byte[] bytes)
		{
			using var reader = new PacketReader(bytes);
			Length = reader.ReadUInt16();
			Type = (PacketType)reader.ReadUInt16();
			Identity = reader.ReadUInt32(); // 4
			reader.ReadInt32();
			Action = (WarehouseMode)reader.ReadByte(); // 8
			Mode = (StorageType)reader.ReadByte(); // 9
			Unknown = reader.ReadUInt16(); // 10
			Data = reader.ReadInt32(); // 12
			Param = reader.ReadUInt32(); // 16
		}

		public override byte[] Encode()
		{
			using var writer = new PacketWriter();
			writer.Write((ushort)PacketType.MsgPackage);
			writer.Write(Identity); // 4
			writer.Write(0); // 8
			writer.Write((byte)Action); // 12
			writer.Write((byte)Mode); // 13
			writer.Write((ushort)0); // 14
			writer.Write(Data); // 16
			writer.Write(Param); // 20
			if (Items.Count > 0)
			{
				writer.Write(Items.Count);
				foreach (WarehouseItem item in Items)
				{
					writer.Write(item.Identity); // 0 24 28
					writer.Write(item.Type); // 4 28 32
					writer.Write(item.Ident); // 8 32 26 36
					writer.Write(item.SocketOne); // 9 33 37
					writer.Write(item.SocketTwo); // 10 34 38
					writer.Write((int)item.Magic1); // 11 35 39
					writer.Write((ushort)item.Magic2); // 15 39 43
					writer.Write(item.Magic3); // 17 41 45
					writer.Write((byte)item.Blessing); // 18 42 46
					writer.Write(item.Bound); // 19 43 47
					writer.Write(item.Enchantment); // 20 44 48
					writer.Write(item.AntiMonster); // 22 46 50
					writer.Write(item.Suspicious); // 24 48 52
					writer.Write((byte)0); // 25 49 53
					writer.Write(item.Locked); // 26 50 54
					writer.Write(item.Color); // 27 51 55
					writer.Write(item.SocketProgress); // 28 52 56
					writer.Write(item.CompositionProgress); // 32 56 60
					writer.Write(item.Inscribed); // 36 60 64
					writer.Write(item.RemainingTime); // 40 64 68
					writer.Write(item.ActivationTime);
					writer.Write(item.Accumulate);
					writer.Write(0);
					writer.Write(item.PerfectionLevel);
					writer.Write(item.PerfectionProgress);
					writer.Write(item.PerfectionOwnerId);
					writer.Write(item.PerfectionOwnerName, item.PerfectionOwnerName.Length);
					writer.Write(0); // 82
					writer.Write((ushort)0); // 82
					writer.Write(item.PerfectionOwnerSignature);
					//if (item.Perfection > 0)
					//	writer.Write(item.Perfection);//82
					//if (item.PerfectionProgress > 0)
					//	writer.Write(item.PerfectionProgress);//86
					//if (item.PerfectionOwnerGuid > 0)
					//	writer.Write(item.PerfectionOwnerGuid);//90
					//if (item.PerfectionOwnerName != null)
					//	writer.Write(item.PerfectionOwnerName, item.PerfectionOwnerName.Length);//94
					//if (item.Signature != null)
					//	writer.Write(item.Signature);//110

					//               writer.Write(item.ActivationTime); // 44 68 72
					//               //writer.Write(item.Accumulate); // 48 72 76
					//               writer.Write(item.Mode); // Mode? 52 76 80
					//               writer.Write(item.Durability); // Dura? 54 78 82
					//               writer.Write(item.MaximumDurability); // Max ? 56 80 84
					//               writer.Write((ushort)0); // 86
					//               writer.Write(item.PerfectionProgress);
				}
			}
			else
			{
				writer.Write(Param);
			}

			return writer.ToArray();
		}

		public struct WarehouseItem
		{
			public uint Identity;
			public uint Type;
			public byte Ident;
			public byte SocketOne;
			public byte SocketTwo;
			public byte Magic1;
			public byte Magic2;
			public byte Magic3;
			public ushort Blessing;
			public bool Bound;
			public ushort Enchantment;
			public ushort AntiMonster;
			public bool Suspicious;
			public bool Locked;
			public byte Color;
			public uint SocketProgress;
			public uint CompositionProgress;
			public int Inscribed;
			public int RemainingTime;
			public int ActivationTime;
			public int Accumulate;
			public ushort Mode;
			public ushort Durability;
			public ushort MaximumDurability;
			public uint PerfectionLevel;
			public uint PerfectionProgress;
			public uint PerfectionOwnerId;
			public string PerfectionOwnerName;
			public string PerfectionOwnerSignature;
		}

		public enum StorageType : byte
		{
			None = 0,
			Storage = 10,
			Trunk = 20,
			Chest = 30,
			ChestPackage = 40,
			WardRobe = 50
		}

		public enum WarehouseMode : byte
		{
			Query = 0,
			CheckIn,
			CheckOut
		}

		public override async Task ProcessAsync(GameClient client)
		{
			Character user = client.Character;
			if (!user.IsUnlocked())
			{
				await user.SendSecondaryPasswordInterfaceAsync();
				return;
			}

			BaseNpc npc = null;
			if (Mode == StorageType.Storage || Mode == StorageType.Trunk)
			{
				npc = RoleManager.GetRole(Identity) as BaseNpc;
				if (npc == null)
				{
					if (user.IsPm())
					{
						await user.SendAsync($"Could not find Storage NPC, {Identity}");
					}

					return;
				}

				if (user.MapIdentity != npc.MapIdentity
					|| user.GetDistance(npc) > Screen.VIEW_SIZE)
				{
					switch (npc.MapIdentity)
					{
						case 1002: // twin
						case 1036: // market
							if (user.VipLevel < 1)
							{
								return;
							}

							break;
						case 1000: // desert
							if (user.VipLevel < 2)
							{
								return;
							}

							break;
						case 1020: // canyon
							if (user.VipLevel < 3)
							{
								return;
							}

							break;
						case 1015: // bird
							if (user.VipLevel < 4)
							{
								return;
							}

							break;
						case 1011: // phoenix
							if (user.VipLevel < 5)
							{
								return;
							}

							break;
						case 1213: // stone
							if (user.VipLevel < 6)
							{
								return;
							}

							break;
					}
				}
			}
			else if (Mode == StorageType.Chest && Action == WarehouseMode.CheckIn)
			{
				return;
			}

			if (Action == WarehouseMode.Query)
			{
				var storageItems = user.UserPackage.GetStorageItems(Identity, Mode);
				foreach (var expiredItem in storageItems.Values.Where(x => x.HasExpired()).ToList())
				{
					storageItems.TryRemove(expiredItem.Identity, out _);
					await expiredItem.DeleteAsync();
				}

				foreach (Item item in storageItems.Values)
				{
					Items.Add(new WarehouseItem
					{
						Identity = item.Identity,
						Type = item.Type,
						SocketOne = (byte)item.SocketOne,
						SocketTwo = (byte)item.SocketTwo,
						Blessing = (byte)item.Blessing,
						Enchantment = item.Enchantment,
						Magic1 = (byte)item.Effect,
						Magic3 = item.Plus,
						Color = (byte)item.Color,
						Locked = item.IsLocked(),
						Bound = item.IsBound,
						Accumulate = (int)item.AccumulateNum,
						CompositionProgress = item.CompositionProgress,
						Inscribed = item.SyndicateIdentity != 0 ? 1 : 0,
						SocketProgress = item.SocketProgress,
						Suspicious = item.IsSuspicious(),
						Durability = item.Durability,
						MaximumDurability = item.MaximumDurability,
						ActivationTime = item.RemainingSeconds > 0 ? 0 : item.SaveTime,
						RemainingTime = item.RemainingSeconds,
						PerfectionLevel = item.PerfectionLevel,
						PerfectionProgress = item.PerfectionProgress,
						PerfectionOwnerId = item.PerfectionOwnerGuid,
						PerfectionOwnerName = item.PerfectionOwnerName ?? "PRUEBA",
						PerfectionOwnerSignature = item.PerfectionSignature ?? "PRUEBA"
					});

					await user.SendAsync(this);
					await item.TryUnlockAsync();
					if (item.ItemStatus != null)
					{
						await item.ItemStatus.SendToAsync(user);
					}
					Items.Clear();
				}
			}
			else if (Action == WarehouseMode.CheckIn)
			{
				Item storeItem = user.UserPackage.GetInventory(Param);
				if (storeItem == null)
				{
					await user.SendAsync(StrItemNotFound);
					return;
				}

				if (!storeItem.CanBeStored())
				{
					await user.SendAsync(StrItemCannotBeStored);
					return;
				}

				if (storeItem.HasExpired())
				{
					await storeItem.ExpireAsync();
					return;
				}

				//if (Mode == StorageType.Storage && npc?.IsStorageNpc() != true)
				//{
				//    return;
				//}

				//if (Mode == StorageType.Trunk && npc?.Type != BaseNpc.TRUNCK_NPC)
				//{
				//    return;
				//}

				await user.UserPackage.AddToStorageAsync(Identity, storeItem, Mode, true);
			}
			else if (Action == WarehouseMode.CheckOut)
			{
				await user.UserPackage.GetFromStorageAsync(Identity, Param, Mode, true);
			}
		}
	}
}
