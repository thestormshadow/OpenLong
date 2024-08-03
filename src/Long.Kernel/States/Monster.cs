using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Ai.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;
using Long.Network.Packets.Ai;
using Long.Kernel.States.Status;

using static Long.Kernel.Network.Game.Packets.MsgAction;
using Long.Shared.Managers;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.World;
using Long.Shared.Mathematics;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.States.Items.MapItem;
using Long.Kernel.States.Items;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Long.Kernel.States
{
    public class Monster : Role
    {
		private static readonly ILogger logger = Log.ForContext<Monster>();

		private readonly DbMonstertype monsterType;

		public Dictionary<Character, int> Score = new Dictionary<Character, int>();
		public bool AutoUpdateScoreScreen = true;

		private readonly uint generatorId;

		private readonly TimeOutMS statusCheck = new(500);
		private readonly TimeOut disappear = new(5);
		private readonly TimeOut leaveMap = new(10);

		public Monster(DbMonstertype monsterType, uint identity)
		{
			this.monsterType = monsterType;
			this.generatorId = generatorId;

			HasGenerator = this.generatorId != 0;

			Identity = identity;
		}
		public Monster(DbMonstertype monsterType, uint identity, uint generatorId, uint ownerId)
        {
            this.monsterType = monsterType;
            this.generatorId = generatorId;

            HasGenerator = this.generatorId != 0;

            Identity = identity;
            OwnerIdentity = ownerId;
            Mesh = monsterType.Lookface;
        }
		public async Task<bool> InitializeAsync(uint idMap, ushort x, ushort y)
		{
			this.idMap = idMap;

			if ((Map = MapManager.GetMap(idMap)) == null)
				return false;

			currentX = x;
			currentY = y;

			Life = MaxLife;

			if (IsCallPet())
			{
				MsgPetInfo msg = new MsgPetInfo
				{
					Identity = Identity,
					LookFace = Mesh,
					X = X,
					Y = Y,
					Name = Name,
					AiType = monsterType.AiType
				};

				Role owner = RoleManager.GetUser(OwnerIdentity);
				if (owner == null)
					return false;
				await owner.SendAsync(msg);
			}

			if (monsterType.MagicType > 0)
			{
				var defaultMagic = new Magic(this);
				if (await defaultMagic.CreateAsync(monsterType.MagicType))
				{
					MagicData.Magics.TryAdd(defaultMagic.Type, defaultMagic);
				}
			}

			foreach (DbMonsterTypeMagic dbMagic in RoleManager.GetMonsterMagics(Type))
			{
				await AddMagicsAsync(dbMagic);
			}

			return true;
		}
		private async Task AddMagicsAsync(DbMonsterTypeMagic dbMagic)
		{
			if (dbMagic.MagicType1 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType1, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType2 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType2, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType3 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType3, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType4 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType4, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType5 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType5, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType6 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType6, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType7 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType7, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType8 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType8, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType9 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType9, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}

			if (dbMagic.MagicType10 > 0)
			{
				var magic = new Magic(this);
				if (await magic.CreateAsync(dbMagic.MagicType10, 0))
				{
					MagicData.Magics.TryAdd(magic.Type, magic);
				}
			}
		}
        public uint ActionId => (uint)(monsterType?.Action ?? 0);
        public uint GeneratorId => generatorId;

		public static async Task<Monster> CreateCallPetAsync(Character caller, uint type, ushort x, ushort y)
		{
			DbMonstertype monsterType = RoleManager.GetMonstertype(type);
			if (monsterType == null)
				return null;

			uint idPet = (uint)IdentityManager.Pet.GetNextIdentity;
			Monster pet = new Monster(monsterType, idPet, 0, caller.Identity);
			if (!await pet.InitializeAsync(caller.MapIdentity, x, y))
			{
				IdentityManager.Pet.ReturnIdentity(idPet);
				return null;
			}

			await pet.EnterMapAsync();
			return pet;
		}

		#region Identity

		public override string Name
		{
			get => monsterType?.Name ?? "None";
			protected set => monsterType.Name = value;
		}

		public uint Type => monsterType?.Id ?? 0;

		public byte SpeciesType => monsterType.SpeciesType;

		public uint Dexterity => monsterType.Dexterity;

		public bool IsBoss => monsterType.SpeciesType == 1;

		public bool IsEspecialBoss => SpeciesType == 1 && Dexterity > 30;

		#endregion

		#region Appearence
		public override uint Mesh
		{
			get => monsterType?.Lookface ?? 0;
			set => monsterType.Lookface = (ushort)value;
		}
		#endregion

		#region Attributes

		public override byte Level
        {
            get => (byte)(monsterType?.Level ?? 0);
            set => monsterType.Level = value;
        }
        public override uint MaxLife => (uint)(monsterType?.Life ?? 1);

        public override int BattlePower => monsterType?.ExtraBattlelev ?? 0;

        public override int MinAttack => monsterType?.AttackMin ?? 0;

        public override int MaxAttack => monsterType?.AttackMax ?? 0;

        public override int ExtraDamage => monsterType?.ExtraDamage ?? 0;

        public override int MagicAttack => monsterType?.AttackMax ?? 0;

        public override int Defense => monsterType?.Defence ?? 0;

        public override int MagicDefense => monsterType?.MagicDef ?? 0;

        public override int Dodge => (int)(monsterType?.Dodge ?? 0);

        public override int AttackSpeed => monsterType?.AttackSpeed ?? 1000;

        public override int Accuracy => (int)(monsterType?.Dexterity ?? 0);

        public uint AttackUser => monsterType?.AttackUser ?? 0;

        public int ViewRange => monsterType?.ViewRange ?? 1;

        public override int Counteraction => monsterType?.StableDefence ?? 0;
        public override int CriticalStrike => monsterType?.CriticalRate ?? 0;
        public override int SkillCriticalStrike => monsterType?.MagicCriticalRate ?? 0;
        public override int Immunity => monsterType?.AntiCriticalRate ?? 0;
        public override int AddFinalAttack => monsterType?.FinalDmgAdd ?? 0;
        public override int AddFinalDefense => monsterType?.FinalDmgReduce ?? 0;
        public override int AddFinalMAttack => monsterType?.FinalDmgAddMgc ?? 0;
        public override int AddFinalMDefense => monsterType?.FinalDmgReduceMgc ?? 0;
        public int Defense2 => (int)(monsterType?.Defence2 ?? 0);

		#endregion

		#region Map

		public override async Task EnterMapAsync()
		{
			Map = MapManager.GetMap(MapIdentity);
			if (Map != null)
				await Map.AddAsync(this);

			await BroadcastRoomMsgAsync(new MsgAction
			{
				Action = ActionType.MapEffect,
				Identity = Identity,
				X = X,
				Y = Y
			}, false);

			if (IsCallPet())
			{
				NpcServer.Instance.Send(NpcServer.NpcClient, new MsgAiRoleLogin(this).Encode());
			}
		}

		public override async Task LeaveMapAsync()
		{
			if (Map != null)
			{
				await Map.RemoveAsync(Identity);
			}

			Map = null;
            			
			var msg = new Long.Game.Network.Ai.Packets.MsgAiSpawnNpc
			{
				Mode = AiSpawnNpcMode.DestroyNpc
			};
			msg.List.Add(new MsgAiSpawnNpc<AiClient>.SpawnNpc
			{
				Id = Identity
			});
			NpcServer.Instance.Send(NpcServer.NpcClient, msg.Encode());
		}

		#endregion
		
		#region Battle

		public override int AdjustWeaponDamage(int damage, Role target)
		{
			return (int)Calculations.MulDiv(damage, monsterType?.Defence2 ?? Calculations.DEFAULT_DEFENCE2, Calculations.DEFAULT_DEFENCE2);
		}

		public override bool SetAttackTarget(Role target)
		{
			if (target == null)
			{
				BattleSystem.ResetBattle();
				return true;
			}

			// todo check if owner user
			if (GetDistance(target) > GetAttackRange(target.SizeAddition))
				return false;

			BattleSystem.CreateBattle(target.Identity);
			return true;
		}

		public override int GetAttackRange(int sizeAdd)
		{
			return monsterType.AttackRange + sizeAdd;
		}

		public override bool IsAttackable(Role attacker)
		{
			if (!IsAlive)
				return false;

			return base.IsAttackable(attacker);
		}

		public override async Task<bool> BeAttackAsync(Magic.MagicType magicType, Role attacker, int power, bool reflectEnable)
		{
			if (!IsAlive)
				return false;

			await AddAttributesAsync(ClientUpdateType.Hitpoints, power * -1);

			if (!IsAlive)
			{
				await BeKillAsync(attacker);
				return true;
			}
			else
			{
				if (SpeciesType > 0)
				{
					await BroadcastRoomMsgAsync(new MsgUserAttrib(Identity, ClientUpdateType.Hitpoints, Life), false);
				}
			}
			return true;
		}

		public override async Task BeKillAsync(Role attacker)
		{
			if (disappear.IsActive())
				return;

			if (attacker?.BattleSystem.IsActive() == true)
				attacker.BattleSystem.ResetBattle();

			if (attacker?.MagicData.QueryMagic != null)
				await attacker.MagicData.AbortMagicAsync(false);

			await DetachAllStatusAsync();
			await AttachStatusAsync(attacker, StatusSet.FADE, 0, int.MaxValue, 0, null);
			await AttachStatusAsync(attacker, StatusSet.DEAD, 0, int.MaxValue, 0, null);
			await AttachStatusAsync(attacker, StatusSet.GHOST, 0, int.MaxValue, 0, null);
			await RemoveDeadMarkAsync();

			var user = attacker as Character;
			int dieType = user?.KoCount * 65541 ?? 1;

			await BroadcastRoomMsgAsync(new MsgInteract
			{
				SenderIdentity = attacker?.Identity ?? 0,
				TargetIdentity = Identity,
				PosX = X,
				PosY = Y,
				Action = MsgInteractType.Kill,
				Data = dieType
			}, false);

			disappear.Startup(3);
			leaveMap.Startup(NPC_REST_TIME);

			if (monsterType.Action > 0)
			{
				DbAction action = EventManager.GetAction(monsterType.Action);
				if (action != null)
					await GameAction.ExecuteActionAsync(monsterType.Action, user, this, null, string.Empty);

			}

			if (user != null)
			{
				//LuaScriptManager.Run("LinkMonsterKillMain", new object[] { user.Identity, this.Identity }, user, this, null, string.Empty);
			}


			if (IsPkKiller() || IsGuard() || IsEvilKiller() || IsDynaNpc() || attacker == null)
				return;

			if (user?.Team != null)
			{
				foreach (Character member in user.Team.Members)
				{
					if (member.MapIdentity == user.MapIdentity
						&& member.GetDistance(user) <= Screen.VIEW_SIZE * 2)
					{
						await member.AddJarKillsAsync(monsterType.StcType);
					}
				}
			}
			else if (user != null)
			{
				await user.AddJarKillsAsync(monsterType.StcType);
			}

			int chance = await NextAsync(100);
			var moneyAdj = 25;
			if (user != null && BattleSystem.GetNameType(user.Level, Level) == BattleSystem.NAME_GREEN)
				moneyAdj = 8;

			if (chance < moneyAdj)
			{
				var moneyMin = (int)(monsterType.DropMoney * 0.85f);
				var moneyMax = (int)(monsterType.DropMoney * 1.15f);
				var money = (uint)(moneyMin + await NextAsync(moneyMin, moneyMax) + 1);

				if (user == null || !user.Flag.HasFlag(Character.PrivilegeFlag.FirstCreditClaimed))
				{
					int heapNum = 1 + await NextAsync(1, 3);
					var moneyAve = (uint)(money / heapNum);

					for (var i = 0; i < heapNum; i++)
					{
						var moneyTmp = (uint)Calculations.MulDiv((int)moneyAve, 90 + await NextAsync(3, 21), 100);
						await DropMoneyAsync(moneyTmp, 0, DropMode.Common);
					}
				}
				else
				{
					ServerStatisticManager.DropMoney(money);
					await user.AwardMoneyAsync((int)money);
				}
			}

			var dropNum = 0;
			int rate = await NextAsync(0, 10000);
			chance = BattleSystem.AdjustDrop(750, attacker.Level, Level);
			if (rate < Math.Min(10000, chance))
			{
				dropNum = 1 + await NextAsync(4, 7); // drop 5-8 items
			}
			else
			{
				chance += BattleSystem.AdjustDrop(1000, attacker.Level, Level);
				if (rate < Math.Min(10000, chance))
				{
					dropNum = 1 + await NextAsync(2, 4); // drop 3-5 items
				}
				else
				{
					chance += BattleSystem.AdjustDrop(1200, attacker.Level, Level);
					if (rate < Math.Min(10000, chance))
					{
						dropNum = 1 + await NextAsync(1, 3); // drop 1-4 items
					}
					else
					{
						chance += BattleSystem.AdjustDrop(1500, attacker.Level, Level);
						if (rate < Math.Min(10000, chance)) dropNum = 1; // drop 1 item
					}
				}
			}

			for (var i = 0; i < dropNum; i++)
			{
				await DropEquipmentAsync(attacker?.Identity ?? 0);
			}
		}

		public async Task AddBossAttackerScore(Character user, int damage)
		{
			if (IsEspecialBoss)
			{
				if (Score.ContainsKey(user))
					Score[user] += damage;
				else
					Score.Add(user, damage);

				if (AutoUpdateScoreScreen)
					await user.SendAsync(new MsgLoadMap(user, Identity));
			}
			
		}

		#endregion

		#region Drop Function

		public async Task DropItemAsync(uint type, Character owner, DropMode mode = MapItem.DropMode.Common)
		{
			DbItemtype itemType = ItemManager.GetItemtype(type);
			if (itemType != null)
			{
				await DropItemAsync(itemType, owner, mode);
			}
		}

		private async Task DropItemAsync(DbItemtype itemtype, Character owner, DropMode mode = MapItem.DropMode.Common)
		{
			var targetPos = new Point(X, Y);
			if (Map.FindDropItemCell(5, ref targetPos))
			{
				var drop = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
				if (drop.Create(Map, targetPos, itemtype, owner?.Identity ?? 0, 0, 0, 0, mode))
				{
					await drop.EnterMapAsync();

					if (drop.Info.Addition > 0 && owner?.Guide != null)
					{
						await owner.Tutor.AwardOpportunityAsync(1);
					}
				}
				else
				{
					IdentityManager.MapItem.ReturnIdentity(drop.Identity);
				}
			}
		}

		private async Task DropEquipmentAsync(uint owner)
		{
			const int qualityPrecision = 10_000_000;

			var drop = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
			int rand = await NextAsync(100);

			var targetPos = new Point(X, Y);
			if (Map?.FindDropItemCell(4, ref targetPos) != true)
			{
				return;
			}

			if (rand < 60)
			{
				rand = await NextAsync(qualityPrecision);
				var quality = 0;
				if (rand < 2500)
				{
					quality = 9;
				}
				else if (rand < 14500)
				{
					quality = 8;
				}
				else if (rand < 56500)
				{
					quality = 7;
				}
				else if (rand < 110000)
				{
					quality = 6;
				}

				MapItemInfo info = await Items.Item.CreateItemInfoAsync(monsterType, quality);
				if (drop.Create(Map, targetPos, info, owner, DropMode.Common))
				{
					if (quality > 5)
					{
						ServerStatisticManager.DropQualityItem(quality);
					}
					if (info.Addition > 0)
					{
						ServerStatisticManager.DropComposedItem();
					}
					if (info.ReduceDamage > 0)
					{
						ServerStatisticManager.DropReducedDamageItem();
					}
					if (info.SocketNum == 1)
					{
						ServerStatisticManager.DropOneSocketItem();
					}
					else if (info.SocketNum == 2)
					{
						ServerStatisticManager.DropTwoSocketItem();
					}
					await drop.EnterMapAsync();
				}
				else
				{
					IdentityManager.MapItem.ReturnIdentity(drop.Identity);
				}
			}
			else
			{
				uint dropMedicine;
				if (monsterType.DropHp != 0 && monsterType.DropMp != 0)
				{
					rand = await NextAsync(100);
					if (rand < 60)
					{
						dropMedicine = monsterType.DropHp;
					}
					else
					{
						dropMedicine = monsterType.DropMp;
					}
				}
				else if (monsterType.DropHp != 0)
				{
					dropMedicine = monsterType.DropHp;
				}
				else
				{
					dropMedicine = monsterType.DropMp;
				}

				if (dropMedicine == 0)
				{
					return;
				}

				if (drop.Create(Map, targetPos, dropMedicine, owner, 0, 0, 0, DropMode.Common))
				{
					await drop.EnterMapAsync();
				}
				else
				{
					IdentityManager.MapItem.ReturnIdentity(drop.Identity);
				}
			}
		}

		public async Task DropMoneyAsync(uint amount, uint idOwner, DropMode mode = MapItem.DropMode.Common)
		{
			var targetPos = new Point(X, Y);
			if (Map?.FindDropItemCell(4, ref targetPos) == true)
			{
				var drop = new MapItem((uint)IdentityManager.MapItem.GetNextIdentity);
				if (drop.CreateMoney(Map, targetPos, amount, idOwner, mode))
				{
					await drop.EnterMapAsync();
					ServerStatisticManager.DropMoney(amount);
				}
				else
				{
					IdentityManager.MapItem.ReturnIdentity(drop.Identity);
				}
			}
		}

		#endregion

		#region Checks

		public bool IsDeleted() => disappear.IsActive();

		public override bool IsEvil()
		{
			return (AttackUser & ATKUSER_RIGHTEOUS) == 0 || base.IsEvil();
		}

		public override bool IsFarWeapon()
		{
			return monsterType.AttackRange > SHORTWEAPON_RANGE_LIMIT;
		}

		public bool IsPassive()
		{
			return (AttackUser & ATKUSER_PASSIVE) != 0;
		}

		public bool IsLockUser()
		{
			return (AttackUser & ATKUSER_LOCKUSER) != 0;
		}

		public bool IsRighteous()
		{
			return (AttackUser & ATKUSER_RIGHTEOUS) != 0;
		}

		public bool IsGuard()
		{
			return (AttackUser & ATKUSER_GUARD) != 0;
		}

		public bool IsPkKiller()
		{
			return (AttackUser & ATKUSER_PPKER) != 0;
		}

		public bool IsWalkEnable()
		{
			return (AttackUser & ATKUSER_FIXED) == 0;
		}

		public bool IsJumpEnable()
		{
			return (AttackUser & ATKUSER_JUMP) != 0;
		}

		public bool IsFastBack()
		{
			return (AttackUser & ATKUSER_FASTBACK) != 0;
		}

		public bool IsLockOne()
		{
			return (AttackUser & ATKUSER_LOCKONE) != 0;
		}

		public bool IsAddLife()
		{
			return (AttackUser & ATKUSER_ADDLIFE) != 0;
		}

		public bool IsEvilKiller()
		{
			return (AttackUser & ATKUSER_EVIL_KILLER) != 0;
		}

		public bool IsDormancyEnable()
		{
			return (AttackUser & ATKUSER_LOCKUSER) == 0;
		}

		public bool IsEscapeEnable()
		{
			return (AttackUser & ATKUSER_NOESCAPE) == 0;
		}

		public bool IsEquality()
		{
			return (AttackUser & ATKUSER_EQUALITY) != 0;
		}

		#endregion

		#region Pet

		private PetData petData;

		public async Task DelMonsterAsync(bool now)
		{
			if (IsDeleted())
				return;

			if (petData != null)
			{
				petData.Life = 0;
			}

			await BeKillAsync(null);

			if (now)
			{
				disappear.Startup(1);
				leaveMap.Startup(1);
				await LeaveMapAsync();
			}
			else
			{
				disappear.Startup(1);
				leaveMap.Startup(3);
			}
		}

		#endregion		

		public override async Task OnTimerAsync()
		{
			if (Map == null)
			{
				return;
			}

			try
			{
				if (!IsAlive
					&& disappear.IsActive()
					&& disappear.IsTimeOut())
					QueueAction(() => AttachStatusAsync(this, StatusSet.INVISIBLE, 0, int.MaxValue, 0));
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Monster::OnTimerAsync() => {Identity}:{Name} Set ghost: {ex.Message}");
			}

			try
			{
				if (leaveMap.IsActive())
				{
					if (leaveMap.IsTimeOut())
						QueueAction(LeaveMapAsync);
					return;
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Monster::OnTimerAsync() => {Identity}:{Name} LeaveMapAsync: {ex.Message}");
			}

			try
			{
				if (statusCheck.ToNextTime())
					foreach (IStatus stts in StatusSet.Status.Values)
					{
						QueueAction(async () =>
						{
							await stts.OnTimerAsync();
							if (!stts.IsValid && stts.Identity != StatusSet.GHOST && stts.Identity != StatusSet.DEAD)
								await StatusSet.DelObjAsync(stts.Identity);
						});
					}
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Monster::OnTimerAsync() => {Identity}:{Name} Status: {ex.Message}");
			}

			try
			{
				if (BattleSystem != null
					&& BattleSystem.IsActive()
					&& BattleSystem.NextAttack(AttackSpeed))
					QueueAction(BattleSystem.ProcessAttackAsync);
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Monster::OnTimerAsync() => {Identity}:{Name} BattleSystem.ProcessAttackAsync(): {ex.Message}");
			}

			try
			{
				if (MagicData.State != MagicData.MagicState.None)
					QueueAction(MagicData.OnTimerAsync);
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Monster::OnTimerAsync() => {Identity}:{Name} MagicData.OnTimerAsync(): {ex.Message}");
			}
		}

		#region Socket

		public override Task SendSpawnToAsync(Character player)
		{
			if (!IsAlive)
			{
				return Task.CompletedTask;
			}
			return player.SendAsync(new MsgPlayer(this));
		}

		public override Task SendSpawnToAsync(Character player, int x, int y)
		{
			if (!IsAlive)
			{
				return Task.CompletedTask;
			}
			return player.SendAsync(new MsgPlayer(this, (ushort)x, (ushort)y));
		}

		#endregion

		public class PetData
		{
			public uint OwnerIdentity { get; set; }
			public uint OwnerType { get; set; }
			public uint Generator { get; set; }
			public uint Type { get; set; }
			public string Name { get; set; }
			public uint Life { get; set; }
			public uint Mana { get; set; }
			public uint MapIdentity { get; set; }
			public ushort MapX { get; set; }
			public ushort MapY { get; set; }
			public object Data { get; set; }
		}

		public const int ATKUSER_LEAVEONLY = 0,        // Ö»»áÌÓÅÜ
                         ATKUSER_PASSIVE = 0x01,       // ±»¶¯¹¥»÷
                         ATKUSER_ACTIVE = 0x02,        // Ö÷¶¯¹¥»÷
                         ATKUSER_RIGHTEOUS = 0x04,     // ÕýÒåµÄ(ÎÀ±ø»òÍæ¼ÒÕÙ»½ºÍ¿ØÖÆµÄ¹ÖÎï)
                         ATKUSER_GUARD = 0x08,         // ÎÀ±ø(ÎÞÊÂ»ØÔ­Î»ÖÃ)
                         ATKUSER_PPKER = 0x10,         // ×·É±ºÚÃû 
                         ATKUSER_JUMP = 0x20,          // »áÌø
                         ATKUSER_FIXED = 0x40,         // ²»»á¶¯µÄ
                         ATKUSER_FASTBACK = 0x0080,    // ËÙ¹é
                         ATKUSER_LOCKUSER = 0x0100,    // Ëø¶¨¹¥»÷Ö¸¶¨Íæ¼Ò£¬Íæ¼ÒÀë¿ª×Ô¶¯ÏûÊ§ 
                         ATKUSER_LOCKONE = 0x0200,     // Ëø¶¨¹¥»÷Ê×ÏÈ¹¥»÷×Ô¼ºµÄÍæ¼Ò
                         ATKUSER_ADDLIFE = 0x0400,     // ×Ô¶¯¼ÓÑª
                         ATKUSER_EVIL_KILLER = 0x0800, // °×ÃûÉ±ÊÖ
                         ATKUSER_WING = 0x1000,        // ·ÉÐÐ×´Ì¬
                         ATKUSER_NEUTRAL = 0x2000,     // ÖÐÁ¢
                         ATKUSER_ROAR = 0x4000,        // ³öÉúÊ±È«µØÍ¼Å­ºð
                         ATKUSER_NOESCAPE = 0x8000,    // ²»»áÌÓÅÜ
                         ATKUSER_EQUALITY = 0x10000;   // ²»ÃêÊÓ

        public const int SHORTWEAPON_RANGE_LIMIT = 2; // ½üÉíÎäÆ÷µÄ×î´ó¹¥»÷·¶Î§(ÒÔ´Ë·¶Î§ÊÇ·ñ¹­¼ýÊÖ)
        public const int NPC_REST_TIME = 7;
    }
}
