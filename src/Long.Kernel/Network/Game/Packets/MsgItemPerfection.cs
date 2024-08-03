using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Long.Kernel.Network.Game.Packets.MsgDataArray;
using static Long.Kernel.Network.Game.Packets.MsgItemPerfection;
using static Long.Kernel.States.User.UserPackage;
using static Microsoft.VisualStudio.Threading.AsyncReaderWriterLock;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Long.Kernel.Network.Game.Packets
{
	public sealed class MsgItemPerfection : MsgProtoBufBase<GameClient, ItemRefineOperationProto>
	{
        public MsgItemPerfection() : base(PacketType.MsgItemRefineOpt) { }
		
		public enum DataArrayMode
		{
			Temper,
			TransferOwnership,
			Signature,
			CpBoost,
			Exchange,
			Quicken
		}	
		public override async Task ProcessAsync(GameClient client)
		{
			Character user = client.Character;
			switch (Data.Operation)
			{
				case DataArrayMode.Temper:
					{
						if (Data.ItemArray != null)
						{
							Item item;
							if (user.UserPackage.TryGetItem(Data.ItemUID, out item))
							{
								foreach (var minor in Data.ItemArray)
								{
									Item ItemPlus;
									if (user.UserPackage.TryGetItem(minor, out ItemPlus))
									{
										if (ItemPlus.Identity == 730001
									   || ItemPlus.Identity == 730002
									   || ItemPlus.Identity == 730003
									   || ItemPlus.Identity == 730004
									   || ItemPlus.Identity == 730005
									   || ItemPlus.Identity == 730006
									   || ItemPlus.Identity == 730007
									   || ItemPlus.Identity == 730008) 
										{
											await user.SendAsync(new MsgTaskDialog
											{
												InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
												Text = "Sorry You can't temp the perfection with a Stone"
											});
											break; 
										}
										if (ItemPlus.Identity == 300000) 
										{
											await user.SendAsync(new MsgTaskDialog
											{
												InteractionType = MsgTaskDialog.TaskInteraction.MessageBox,
												Text = "Sorry You can't temp the perfection with a Steed"
											});
											break; 
										}
										item.PerfectionProgress += GetPlusStonePoints(ItemPlus.Identity == 300000 ? ItemPlus.Plus : ItemPlus.Type);
										await user.UserPackage.RemoveFromInventoryAsync(minor, UserPackage.RemovalType.Delete);
									}
								}
								while (item.PerfectionProgress >= GetPerfectionProgress(item))
								{
									item.PerfectionProgress -= (uint)GetPerfectionProgress(item);
									item.PerfectionLevel++;
								}
								while (item.PerfectionLevel >= 54 && item.PerfectionProgress > 0)
								{
									item.PerfectionLevel = 54;
									item.PerfectionProgress = 0;
								}
								item.PerfectionOwnerName = user.Name;
								item.PerfectionOwnerGuid = user.Identity;
								//byte[] arr = new MsgUserTotalPerfectionLev(user);
								//await user.SendAsync(arr);
								await item.ChangeTypeAsync(item.Type);

								await user.SendAsync(new MsgItemPerfectionOperation(
									itemUID: item.Identity,
									playerUID: user.Identity,
									stars: item.PerfectionLevel,
									progress: item.PerfectionProgress,
									ownerUID: item.PerfectionOwnerGuid,
									ownerName: item.PerfectionOwnerName
									));								
							}
						}
						break;
					}
				case DataArrayMode.CpBoost:
				case DataArrayMode.Quicken:
					await user.SendAsync("Funcion Deshabilitada");
					break;
				case DataArrayMode.TransferOwnership:
					{
						Item item;
						if (user.UserPackage.TryGetItem(Data.ItemUID, out item))
						{
							if (!item.PerfectionOwnerName.Equals(user.Name) && item.PerfectionOwnerGuid != user.Identity)
							{
								if (user.ConquerPoints >= 1500)
								{
									user.ConquerPoints -= 1500;

									item.PerfectionOwnerName = user.Name;
									item.PerfectionOwnerGuid = user.Identity;
																		
									await item.ChangeTypeAsync(item.Type);

									await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item.Identity,
										playerUID: user.Identity,
										stars: item.PerfectionLevel,
										progress: item.PerfectionProgress,
										ownerUID: item.PerfectionOwnerGuid,
										ownerName: item.PerfectionOwnerName
									));

								}
								else
								{
									await user.SendAsync("No tienes 1500 Cps para Tranferir");
									return;

								}
							}
						}
						break;
					}
				case DataArrayMode.Exchange:
					{
						Item item1;
						Item item2;
						if (user.UserPackage.TryGetItem(Data.ItemArray[0], out item2))
						{
							if (user.UserPackage.TryGetItem(Data.ItemUID, out item1))
							{
								if (user.ConquerPoints >= 5000000)
								{
									user.ConquerPoints -= 5000000;
									var PP = item1.PerfectionProgress;
									var PL = item1.PerfectionLevel;
									item1.PerfectionProgress = item2.PerfectionProgress;
									item1.PerfectionLevel = item2.PerfectionLevel;
									await item1.ChangeTypeAsync(item1.Type);
									await user.SendAsync(new MsgItem
									{
										Identity = item1.Identity,
										Action = MsgItem.ItemActionType.EquipmentLevelUp
									});

									item2.PerfectionProgress = PP;
									item2.PerfectionLevel = PL;
									await item2.ChangeTypeAsync(item2.Type);
									await user.SendAsync(new MsgItem
									{
										Identity = item2.Identity,
										Action = MsgItem.ItemActionType.EquipmentLevelUp
									});
									await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item1.Identity,
										playerUID: user.Identity,
										stars: item1.PerfectionLevel,
										progress: item1.PerfectionProgress,
										ownerUID: item1.PerfectionOwnerGuid,
										ownerName: item1.PerfectionOwnerName
									));
									await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item2.Identity,
										playerUID: user.Identity,
										stars: item2.PerfectionLevel,
										progress: item2.PerfectionProgress,
										ownerUID: item2.PerfectionOwnerGuid,
										ownerName: item2.PerfectionOwnerName
									));
								}
							}
						}
						break;
					}
				case DataArrayMode.Signature:
					{
						Item item;
						if (user.UserPackage.TryGetItem(Data.ItemUID, out item))
						{
							if (item.PerfectionSignature == string.Empty || item.PerfectionSignature == null)
							{
								item.PerfectionSignature = Data.Str;
								await item.ChangeTypeAsync(item.Type);
								await user.SendAsync(new MsgItem
								{
									Identity = item.Identity,
									Action = MsgItem.ItemActionType.EquipmentLevelUp
								});
								await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item.Identity,
										playerUID: user.Identity,
										stars: item.PerfectionLevel,
										progress: item.PerfectionProgress,
										ownerUID: item.PerfectionOwnerGuid,
										ownerName: item.PerfectionOwnerName
									));
							}
							else
							{
								if (user.ConquerPoints >= 270)
								{
									user.ConquerPoints -= 270;
									item.PerfectionSignature = Data.Str;
									await item.ChangeTypeAsync(item.Type);
									await user.SendAsync(new MsgItem
									{
										Identity = item.Identity,
										Action = MsgItem.ItemActionType.EquipmentLevelUp
									});
									await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item.Identity,
										playerUID: user.Identity,
										stars: item.PerfectionLevel,
										progress: item.PerfectionProgress,
										ownerUID: item.PerfectionOwnerGuid,
										ownerName: item.PerfectionOwnerName
									));
								}
								else if (user.ConquerPointsBound >= 270)
								{
									user.ConquerPointsBound -= 270;
									item.PerfectionSignature = Data.Str;
									await item.ChangeTypeAsync(item.Type);
									await user.SendAsync(new MsgItem
									{
										Identity = item.Identity,
										Action = MsgItem.ItemActionType.EquipmentLevelUp
									});
									await user.SendAsync(new MsgItemPerfectionOperation(
										itemUID: item.Identity,
										playerUID: user.Identity,
										stars: item.PerfectionLevel,
										progress: item.PerfectionProgress,
										ownerUID: item.PerfectionOwnerGuid,
										ownerName: item.PerfectionOwnerName
									));
								}
							}

						}
						break;
					}
			}			
		}
		
		[ProtoContract]
		public struct ItemRefineOperationProto
		{
			[ProtoMember(1, IsRequired = true)]
			public MsgItemPerfection.DataArrayMode Operation;
			[ProtoMember(2, IsRequired = true)]
			public uint ItemUID;
			[ProtoMember(3, IsRequired = true)]
			public string Str;
			[ProtoMember(4, IsRequired = true)]
			public uint[] ItemArray;
		}
		public uint GetPerfectionProgress(Item item)
		{
			if (item.PerfectionLevel == 0) return 30;
			if (item.PerfectionLevel == 1) return 60;
			if (item.PerfectionLevel == 2) return 100;
			if (item.PerfectionLevel == 3) return 200;
			if (item.PerfectionLevel == 4) return 350;
			if (item.PerfectionLevel == 5) return 600;
			if (item.PerfectionLevel == 6) return 1000;
			if (item.PerfectionLevel == 7) return 1500;
			if (item.PerfectionLevel == 8) return 2300;
			if (item.PerfectionLevel == 9) return 3500;
			if (item.PerfectionLevel == 10) return 5000;
			if (item.PerfectionLevel == 11) return 6500;
			if (item.PerfectionLevel == 12) return 8000;
			if (item.PerfectionLevel == 13) return 9500;
			if (item.PerfectionLevel == 14) return 11000;
			return 12000;
		}
		public uint GetPlusStonePoints(uint value)
		{
			if (value == 3009000) return 10;
			if (value == 3009001) return 100;
			if (value == 3009002) return 1000;
			if (value == 3009003) return 10000;
			if (value == 730001 || value == 1) return 10;
			if (value == 730002 || value == 2) return 140;
			if (value == 730003 || value == 3) return 160;
			if (value == 730004 || value == 4) return 120;
			if (value == 730005 || value == 5) return 1240;
			if (value == 730006 || value == 6) return 1680;
			if (value == 730007 || value == 7) return 21360;
			if (value == 730008 || value == 8) return 32720;
			if (value == 730009 || value == 9) return 45440;
			if (value == 9) return 87480;
			if (value == 10) return 90180;
			if (value == 11) return 95680;
			if (value == 12) return 104680;
			return 0;
		}
	}		
}
