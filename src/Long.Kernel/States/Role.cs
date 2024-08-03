using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Team;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Processors;
using Long.Kernel.States.Items;
using Long.Kernel.States.Magics;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets;
using Long.Shared.Managers;
using Long.Shared.Mathematics;
using Long.World.Map;
using Long.World.Roles;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgDeadMark;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.Network.Game.Packets.MsgName;
using static Long.Kernel.Network.Game.Packets.MsgWalk;

namespace Long.Kernel.States
{
    public abstract class Role : WorldObject
    {
        private static readonly ILogger logger = Log.ForContext<Role>();

		private readonly TimeOutMS statusTimer = new();

		protected uint idMap;

        protected ushort currentX,
                         currentY;

        protected uint maxLife = 0,
                       maxMana = 0;

        public Role()
        {
            MagicData = new MagicData(this);
			BattleSystem = new BattleSystem(this);
			StatusSet = new StatusSet(this);
		}

		public virtual bool HasGenerator { get; protected set; } = false;

		#region Identity

		public virtual uint OwnerIdentity { get; set; }

		#endregion

		#region Appearence

		public virtual uint Mesh { get; set; }

		#endregion

		#region Level

		public virtual byte Level { get; set; }

		#endregion

		#region Map and position

		public virtual GameMap Map { get; protected set; }

        public int Partition => Map?.Partition ?? -1;

        /// <summary>
        ///     The current map identity for the role.
        /// </summary>
        public virtual uint MapIdentity
        {
            get => idMap;
            set => idMap = value;
        }

        /// <summary>
        ///     Current X position of the user in the map.
        /// </summary>
        public override ushort X
        {
            get => currentX;
            set => currentX = value;
        }

        /// <summary>
        ///     Current Y position of the user in the map.
        /// </summary>
        public override ushort Y
        {
            get => currentY;
            set => currentY = value;
        }

        public int GetDistance(int x, int y)
        {
            return Calculations.GetDistance(X, Y, x, y);
        }

        public int GetDistance(Role target)
        {
            if (target.IsPlayer())
            {

            }
            return GetDistance(target.X, target.Y);
        }

        public virtual Task EnterMapAsync()
        {
			Map = MapManager.GetMap(MapIdentity);
			if (Map != null)
				return Map.AddAsync(this);
			return Task.CompletedTask;
		}

        public virtual async Task LeaveMapAsync()
        {
			if (Map != null)
			{
				await Map.RemoveAsync(Identity);
			}

			Map = null;
        }

        #endregion

        #region Movement
        
        public async Task<bool> JumpPosAsync(int x, int y, bool sync = false)
        {
            if (x == X && y == Y)
            {
                return false;
            }

            if (Map == null || !Map.IsValidPoint(x, y))
            {
                return false;
            }

            if (IsPlayer())
            {
                Character user = (Character)this;
                if (!Map.IsStandEnable(x, y) || !user.IsJumpPass(x, y, MAX_JUMP_ALTITUDE))
                {
                    await user.SendAsync(StrInvalidCoordinate, TalkChannel.TopLeft, Color.Red);
                    await user.KickbackAsync();
                    return false;
                }

                if (Map.IsRaceTrack())
                {
                    await user.KickbackAsync();
                    return false;
                }
            }

            Map.EnterBlock(this, x, y, X, Y);

            Direction = (FacingDirection)Calculations.GetDirectionSector(X, Y, x, y);

            if (sync)
            {
                await BroadcastRoomMsgAsync(new MsgAction
                {
                    CommandX = (ushort)x,
                    CommandY = (ushort)y,
                    X = currentX,
                    Y = currentY,
                    Identity = Identity,
                    Action = ActionType.MapJump,
                    Direction = (ushort)Direction
                }, true);
            }

            currentX = (ushort)x;
            currentY = (ushort)y;
            return true;
        }

        public async Task<bool> MoveTowardAsync(int direction, int mode, bool sync = false)
        {
            if (Map == null)
            {
                return false;
            }

            var user = this as Character;
            ushort newX = 0, newY = 0;

            if (mode == (int)RoleMoveMode.Track)
            {
                direction %= 24;
                newX = (ushort)(X + GameMapData.RideXCoords[direction]);
                newY = (ushort)(Y + GameMapData.RideYCoords[direction]);
            }
            else
            {
                direction %= 8;
                newX = (ushort)(X + GameMapData.WalkXCoords[direction]);
                newY = (ushort)(Y + GameMapData.WalkYCoords[direction]);

                bool isRunning = mode >= (int)RoleMoveMode.RunDir0 &&
                                 mode <= (int)RoleMoveMode.RunDir7;
                if (isRunning && IsAlive)
                {
                    newX += (ushort)GameMapData.WalkXCoords[direction];
                    newY += (ushort)GameMapData.WalkYCoords[direction];
                }
            }

            if (!Map.IsMoveEnable(newX, newY) && user != null)
            {
                await user.KickbackAsync();
                await user.SendAsync(StrInvalidCoordinate, TalkChannel.TopLeft, Color.Red);
                return false;
            }

            Map.EnterBlock(this, newX, newY, X, Y);

            Direction = (FacingDirection)direction;
            if (sync)
            {
                await BroadcastRoomMsgAsync(new MsgWalk
                {
                    Data = new WalkData
                    {
                        Direction = (byte)direction,
                        Identity = Identity,
                        Mode = (byte)mode
                    }
                }, true);
            }

            currentX = newX;
            currentY = newY;

            return true;
        }

        public async Task<bool> JumpBlockAsync(int x, int y, FacingDirection dir)
        {
            int steps = GetDistance(x, y);

            if (steps <= 0)
            {
                return false;
            }

            for (var i = 0; i < steps; i++)
            {
                var pos = new Point(X + (x - X) * i / steps, Y + (y - Y) * i / steps);
                if (Map.IsStandEnable(pos.X, pos.Y))
                {
                    await JumpPosAsync(pos.X, pos.Y, true);
                    return true;
                }
            }

            if (Map.IsStandEnable(x, y))
            {
                await JumpPosAsync(x, y, true);
                return true;
            }

            return false;
        }

        public async Task<bool> FarJumpAsync(int x, int y, FacingDirection dir)
        {
            int steps = GetDistance(x, y);

            if (steps <= 0)
            {
                return false;
            }

            if (Map.IsStandEnable(x, y))
            {
                await JumpPosAsync(x, y, true);
                return true;
            }

            for (var i = 0; i < steps; i++)
            {
                var pos = new Point(X + (x - X) * i / steps, Y + (y - Y) * i / steps);
                if (Map.IsStandEnable(pos.X, pos.Y))
                {
                    await JumpPosAsync(pos.X, pos.Y, true);
                    return true;
                }
            }

            return false;
        }		

		public virtual Task ProcessOnMoveAsync()
        {
			if (BattleSystem.IsActive())
				BattleSystem.ResetBattle();
			Action = EntityAction.Stand;
            return Task.CompletedTask;
        }

        public virtual Task ProcessAfterMoveAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task ProcessOnAttackAsync()
        {
            Action = EntityAction.Stand;
            return Task.CompletedTask;
        }

		public virtual Task ProcessAfterAttackAsync()
		{
			DetachStatusAsync(StatusSet.INTENSIFY);
			return Task.CompletedTask;
		}

		public bool IsJumpPass(int x, int y, int alt)
		{
			List<Point> setLine = new();
			Calculations.DDALineEx(X, Y, x, y, ref setLine);
			if (setLine.Count <= 2)
			{
				return true;
			}

			if (x != setLine[^1].X)
			{
				return false;
			}

			if (y != setLine[^1].Y)
			{
				return false;
			}

			int currentAltitude = Map.GetFloorAlt(X, Y);
			int lastAltitude = Map.GetFloorAlt(x, y);
			if (lastAltitude - currentAltitude > alt)
			{
				return false;
			}

			foreach (Point point in setLine)
			{
				int nextCellAltitude = Map.GetFloorAlt(point.X, point.Y);
				int difference = nextCellAltitude - currentAltitude;
				if (difference > alt && difference < 1000)
				{
					return false;
				}
			}

			return true;
		}

		public bool IsArrowPass(int x, int y, int alt)
		{
			//{
			//    var setLine = new List<Point>();
			//    Calculations.DDALineEx(X, Y, x, y, ref setLine);

			//    if (x != setLine[setLine.Count - 1].X)
			//        return false;
			//    if (y != setLine[setLine.Count - 1].Y)
			//        return false;

			//    var fAlt = (float) (Map.GetFloorAlt(X, Y) + alt + 0.5);
			//    float fDelta = (Map.GetFloorAlt(x, y) - fAlt) / setLine.Count;

			//    foreach (Point point in setLine)
			//    {
			//        if (Map.IsAltOver(point.X, point.Y, (int) fAlt))
			//            return false;
			//        fAlt += fDelta;
			//    }

			//    return true;
			//}
			return true;
		}

		#endregion

		#region Action and Direction

		public virtual FacingDirection Direction { get; protected set; }

        public virtual EntityAction Action { get; protected set; }

        public virtual async Task SetDirectionAsync(FacingDirection direction, bool sync = true)
        {
            Direction = direction;
            if (sync)
            {
                await BroadcastRoomMsgAsync(new MsgAction
                {
                    Identity = Identity,
                    Action = ActionType.CharacterDirection,
                    Direction = (ushort)direction,
                    ArgumentX = X,
                    ArgumentY = Y
                }, true);
            }
        }

        public virtual async Task SetActionAsync(EntityAction action, bool sync = true)
        {
            Action = action;
            if (sync)
            {
                await BroadcastRoomMsgAsync(new MsgAction
                {
                    Identity = Identity,
                    Action = ActionType.CharacterEmote,
                    Command = (ushort)action,
                    ArgumentX = X,
                    ArgumentY = Y
                }, true);
            }
        }

		#endregion

		#region Role Type

		public bool IsPlayer()
		{
			return (this is Character);
			//return Identity >= IdentityManager.PLAYER_ID_FIRST && Identity < IdentityManager.PLAYER_ID_LAST;
		}

		public bool IsMonster()
		{
			return (this is Monster);
			//return Identity >= IdentityManager.MONSTERID_FIRST && Identity < IdentityManager.MONSTERID_LAST;
		}

		public bool IsNpc()
		{
			return (this is Npc);
			//return Identity >= IdentityManager.SYSNPCID_FIRST && Identity < IdentityManager.SYSNPCID_LAST;
		}

		public bool IsDynaNpc()
		{
			return (this is DynamicNpc);
			//return Identity >= IdentityManager.DYNANPCID_FIRST && Identity < IdentityManager.DYNANPCID_LAST;
		}

		public bool IsCallPet()
		{
			return Identity >= IdentityManager.CALLPETID_FIRST && Identity < IdentityManager.CALLPETID_LAST;
		}

		public bool IsTrap()
		{
			return (this is MapTrap);
			//return Identity >= IdentityManager.TRAPID_FIRST && Identity < IdentityManager.TRAPID_LAST;
		}

		public bool IsMapItem()
		{
			return (this is MapItem);
			//return Identity >= IdentityManager.MAPITEM_FIRST && Identity < IdentityManager.MAPITEM_LAST;
		}

		public bool IsFurniture()
		{
			return Identity >= IdentityManager.SCENE_NPC_MIN && Identity < IdentityManager.SCENE_NPC_MAX;
		}

		#endregion

		#region Life and Mana

		public virtual uint Life { get; set; }
		public virtual uint MaxLife => maxLife;
		public virtual uint Mana { get; set; }
		public virtual uint MaxMana => maxMana;

		#endregion

		#region Battle Attributes

		public virtual int BattlePower => 1;

		public virtual int MinAttack { get; } = 1;
		public virtual int MaxAttack { get; } = 1;
		public virtual int MagicAttack { get; } = 1;
		public virtual int Defense { get; } = 0;
		public virtual int MagicDefense { get; } = 0;
		public virtual int MagicDefenseBonus { get; } = 0;
		public virtual int Dodge { get; } = 0;
		public virtual int AttackSpeed { get; } = 1000;
		public virtual int Accuracy { get; } = 1;
		public virtual int Blessing { get; } = 0;

		public virtual int AddFinalAttack { get; } = 0;
		public virtual int AddFinalMAttack { get; } = 0;
		public virtual int AddFinalDefense { get; } = 0;
		public virtual int AddFinalMDefense { get; } = 0;

		public virtual int CriticalStrike => 0;
		public virtual int SkillCriticalStrike => 0;
		public virtual int Immunity => 0;
		public virtual int Penetration => 0;
		public virtual int Breakthrough => 0;
		public virtual int Counteraction => 0;
		public virtual int Block => 0;
		public virtual int Detoxication => 0;
		public virtual int FireResistance => 0;
		public virtual int WaterResistance => 0;
		public virtual int WoodResistance => 0;
		public virtual int EarthResistance => 0;
		public virtual int MetalResistance => 0;

		public virtual int ExtraDamage { get; } = 0;

		#endregion

		#region Battle

		public BattleSystem BattleSystem { get; init; }
		public MagicData MagicData { get; init; }

		public virtual bool IsAlive => Life > 0;
		public virtual bool IsBowman => false;
		public virtual bool IsShieldUser => false;
		public virtual int SizeAddition => 1;

		public virtual bool SetAttackTarget(Role target)
		{
			return false;
		}

		public virtual Task<bool> CheckCrimeAsync(Role target)
		{
			return Task.FromResult(false);
		}

		public virtual int AdjustWeaponDamage(int damage, Role target)
		{
			//return Calculations.MulDiv(damage, Defense2, Calculations.DEFAULT_DEFENCE2);
			return damage;
		}

		public virtual int GetAttackRange(int sizeAdd)
		{
			return sizeAdd + 1;
		}

		public virtual bool IsAttackable(Role attacker)
		{
			return true;
		}

		public virtual bool IsImmunity(Role target)
		{
			if (target == null)
			{
				return true;
			}

			if (target.Identity == Identity)
			{
				return true;
			}

			return false;
		}

		public virtual bool IsFarWeapon()
		{
			return false;
		}

		public virtual Task<BattleSystem.AttackResult> AttackAsync(Role target)
		{
			return BattleSystem.CalcPowerAsync(Magic.MagicType.None, this, target);
		}

		public virtual Task<bool> BeAttackAsync(Magic.MagicType magicType, Role attacker, int power, bool reflectEnable)
		{
			return Task.FromResult(false);
		}

		public virtual async Task KillAsync(Role target, uint dieWay)
		{
			if (this is Monster guard && guard.IsGuard())
			{
				await BroadcastRoomMsgAsync(new MsgInteract
				{
					Action = MsgInteractType.Kill,
					SenderIdentity = Identity,
					TargetIdentity = target.Identity,
					PosX = target.X,
					PosY = target.Y,
					Data = (int)dieWay
				}, true);
			}

			await target.BeKillAsync(this);
		}

		public virtual Task BeKillAsync(Role attacker)
		{
			return Task.CompletedTask;
		}

		public async Task<bool> ProcessMagicAttackAsync(ushort usMagicType, uint idTarget, ushort x, ushort y,
														uint autoActive = 0)
		{
			return await MagicData.ProcessMagicAttackAsync(usMagicType, idTarget, x, y, (MagicData.AutoActive)autoActive);
		}

		public async Task SendDamageMsgAsync(uint idTarget, int damage, InteractionEffect effect)
		{
            
			await Map.BroadcastRoomMsgAsync(X, Y, new MsgInteract
			{
				SenderIdentity = Identity,
				TargetIdentity = idTarget,
				Data = damage,
				Command = damage,
				PosX = X,
				PosY = Y,
				Action = IsBowman ? MsgInteractType.Shoot : MsgInteractType.Attack,
				Effect = effect,
				EffectValue = damage
			});
		}

		#endregion

		#region Dead Mark

		private readonly TimeOut deadMarkTimer = new();

		public async Task SetDeadMarkAsync(int seconds)
		{
			if (!IsPlayer() && !IsMonster())
			{
				return;
			}

			if (!HasDeadMark())
			{
				await SendDeadMarkAsync(DeadMarkAction.Add);
			}

			deadMarkTimer.Startup(seconds);
		}

		public bool HasDeadMark()
		{
			return deadMarkTimer.IsActive() && !deadMarkTimer.IsTimeOut();
		}

		public Task CheckDeadMarkAsync()
		{
			if (deadMarkTimer.TimeOver())
			{
				return RemoveDeadMarkAsync();
			}

			return Task.CompletedTask;
		}

		public Task RemoveDeadMarkAsync()
		{
			deadMarkTimer.Clear();
			return SendDeadMarkAsync(DeadMarkAction.Remove);
		}

		public Task SendDeadMarkAsync(DeadMarkAction action)
		{
			return BroadcastRoomMsgAsync(new MsgDeadMark
			{
				Action = action,
				TargetIdentity = Identity
			}, true);
		}

		#endregion

		#region Scapegoat

		public bool Scapegoat { get; set; } = false;

		public virtual async Task<bool> CheckScapegoatAsync(Role target)
		{
			Magic scapegoat = MagicData[6003];
			if (scapegoat != null && Scapegoat && scapegoat.IsReady())
			{
				return await ProcessMagicAttackAsync(scapegoat.Type, target.Identity, target.X, target.Y);
			}
			return false;
		}

		public Task SetScapegoatAsync(bool on)
		{
			Scapegoat = on;
			return SendAsync(new MsgInteract
			{
				Action = MsgInteractType.CounterKillSwitch,
				SenderIdentity = Identity,
				TargetIdentity = Identity,
				Data = on ? 1 : 0,
				PosX = X,
				PosY = Y
			});
		}

		#endregion

		#region Synchronization

		public virtual async Task<bool> AddAttributesAsync(ClientUpdateType type, long value)
        {
            long currAttr;
            switch (type)
            {
                case ClientUpdateType.Hitpoints:
                    currAttr = Life = (uint)Math.Min(MaxLife, Math.Max(Life + value, 0));
                    break;

                case ClientUpdateType.Mana:
                    currAttr = Mana = (uint)Math.Min(MaxMana, Math.Max(Mana + value, 0));
                    break;

                default:
                    logger.Warning("Role::AddAttributes {0} not handled", type);
                    return false;
            }

            await SynchroAttributesAsync(type, (ulong)currAttr);
            return true;
        }

        public virtual async Task<bool> SetAttributesAsync(ClientUpdateType type, ulong value)
        {
            switch (type)
            {
                case ClientUpdateType.Hitpoints:
                    value = Life = (uint)Math.Max(0, Math.Min(MaxLife, value));
                    break;

                case ClientUpdateType.Mana:
                    value = Mana = (uint)Math.Max(0, Math.Min(MaxMana, value));
                    break;

                default:
                    logger.Warning("Role::SetAttributes {0} not handled", type);
                    return false;
            }

            await SynchroAttributesAsync(type, value, !IsPlayer());
            return true;
        }

        public async Task SynchroAttributesAsync(ClientUpdateType type, ulong value, bool screen = false)
        {
            var msg = new MsgUserAttrib(Identity, type, value);
            if (IsPlayer())
            {
                await SendAsync(msg);
            }

            if (screen && Map != null)
            {
                await Map.BroadcastRoomMsgAsync(X, Y, msg, Identity);
            }
        }

        public async Task SynchroAttributesAsync(ClientUpdateType type, ulong value, ulong value2, bool screen = false)
        {
            var msg = new MsgUserAttrib(Identity, type, value, value2);
            if (IsPlayer())
            {
                await SendAsync(msg);
            }

            if (screen && Map != null)
            {
                await Map.BroadcastRoomMsgAsync(X, Y, msg, Identity);
            }
        }

        public async Task SynchroAttributesAsync(ClientUpdateType type, ulong value, ulong value2, ulong value3, bool screen = false)
        {
            var msg = new MsgUserAttrib(Identity, type, value, value2, value3);
            if (IsPlayer())
            {
                await SendAsync(msg);
            }

            if (screen && Map != null)
            {
                await Map.BroadcastRoomMsgAsync(X, Y, msg, Identity);
            }
        }

        public async Task SynchroAttributesAsync(ClientUpdateType type, uint value1, uint value2, bool screen = false)
        {
            var msg = new MsgUserAttrib(Identity, type, 0, value1, 0, value2);
            if (IsPlayer())
            {
                await SendAsync(msg);
            }

            if (screen && Map != null)
            {
                await Map.BroadcastRoomMsgAsync(X, Y, msg, Identity);
            }
        }

        public async Task SynchroAttributesAsync(ClientUpdateType type, uint value1, uint value2, uint value3, uint value4, bool screen = false)
        {
            var msg = new MsgUserAttrib(Identity, type, value1, value2, value3, value4);
            if (IsPlayer())
            {
                await SendAsync(msg);
            }

            if (screen && Map != null)
            {
                await Map.BroadcastRoomMsgAsync(X, Y, msg, Identity);
            }
        }

        #endregion

        #region Status

        protected const int maxStatus = 64 * 3;

        public virtual bool IsWing => QueryStatus(StatusSet.FLY) != null;
        public bool IsGhost => QueryStatus(StatusSet.GHOST) != null;

        public ulong StatusFlag1 { get; set; }
        public ulong StatusFlag2 { get; set; }
        public ulong StatusFlag3 { get; set; }

        public StatusSet StatusSet { get; init; }

        public virtual async Task<bool> DetachWellStatusAsync()
        {
            //for (var i = 1; i < maxStatus; i++)
            //{
            //    if (StatusSet[i] != null)
            //    {
            //        if (IsWellStatus(i))
            //        {
            //            await DetachStatusAsync(i);
            //        }
            //    }
            //}

            foreach (var status in StatusSet.Status.Values)
            {
                if (IsWellStatus(status.Identity))
                {
                    await DetachStatusAsync(status.Identity);
                }
            }

            return true;
        }

        public virtual async Task<bool> DetachBadlyStatusAsync()
        {
            //for (var i = 1; i < maxStatus; i++)
            //{
            //    if (StatusSet[i] != null)
            //    {
            //        if (IsBadlyStatus(i))
            //        {
            //            await DetachStatusAsync(i);
            //        }
            //    }
            //}
            foreach (var status in StatusSet.Status.Values)
            {
                if (IsBadlyStatus(status.Identity))
                {
                    await DetachStatusAsync(status.Identity);
                }
            }

            return true;
        }

        public virtual async Task<bool> DetachAllStatusAsync()
        {
            await DetachBadlyStatusAsync();
            await DetachWellStatusAsync();
            return true;
        }

        public static bool IsWellStatus(int stts)
        {
            switch (stts)
            {
                case StatusSet.RIDING:
                case StatusSet.FULL_INVISIBLE:
                case StatusSet.LUCKY_DIFFUSE:
                case StatusSet.STIGMA:
                case StatusSet.SHIELD:
                case StatusSet.STAR_OF_ACCURACY:
                case StatusSet.START_XP:
                case StatusSet.INVISIBLE:
                case StatusSet.SUPERMAN:
                case StatusSet.CYCLONE:
                case StatusSet.PARTIALLY_INVISIBLE:
                case StatusSet.LUCKY_ABSORB:
                case StatusSet.SHURIKEN_VORTEX:
                case StatusSet.FLY:
                case StatusSet.FATAL_STRIKE:
                case StatusSet.AZURE_SHIELD:
                case StatusSet.GODLY_SHIELD:
                case StatusSet.CARRYING_FLAG:
                case StatusSet.EARTH_AURA:
                case StatusSet.FEND_AURA:
                case StatusSet.FIRE_AURA:
                case StatusSet.METAL_AURA:
                case StatusSet.TYRANT_AURA:
                case StatusSet.WATER_AURA:
                case StatusSet.WOOD_AURA:
                case StatusSet.OBLIVION:
                case StatusSet.CTF_FLAG:
                case StatusSet.DEFENSIVE_INSTANCE:
                case StatusSet.MAGIC_DEFENDER:
                case StatusSet.BUFF_PSTRIKE:
                case StatusSet.BUFF_MSTRIKE:
                case StatusSet.BUFF_IMMUNITY:
                case StatusSet.BUFF_BREAK:
                case StatusSet.BUFF_COUNTERACTION:
                case StatusSet.BUFF_MAX_HEALTH:
                case StatusSet.BUFF_PATTACK:
                case StatusSet.BUFF_MATTACK:
                case StatusSet.BUFF_FINAL_PDAMAGE:
                case StatusSet.BUFF_FINAL_PDMGREDUCTION:
                case StatusSet.BUFF_FINAL_MDAMAGE:
                case StatusSet.BUFF_FINAL_MDMGREDUCTION:
                case StatusSet.DRAGON_FLOW:
                case StatusSet.SUPER_CYCLONE:
                case StatusSet.DRAGON_FURY:
                case StatusSet.DRAGON_CYCLONE:
                case StatusSet.DRAGON_SWING:
                    return true;
            }

            return false;
        }

        public static bool IsBadlyStatus(int stts)
        {
            switch (stts)
            {
                case StatusSet.POISONED:
                case StatusSet.CONFUSED:
                case StatusSet.FROZEN:
                case StatusSet.DIZZY:
                case StatusSet.FRIGHTENED:
                case StatusSet.SOUL_SHACKLE:
                case StatusSet.POISON_STAR:
                case StatusSet.TOXIC_FOG:
                    return true;
            }

            return false;
        }

        public virtual bool HasDebilitatingStatus()
        {
            foreach (var status in StatusSet.Status.Values)
            {
                switch (status.Identity)
                {
                    case StatusSet.CONFUSED:
                    case StatusSet.FROZEN:
                    case StatusSet.FRIGHTENED:
                    case StatusSet.DIZZY:
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> AttachStatusAsync(DbStatus status)
        {
            if (Map == null)
            {
                return false;
            }

            if (status.LeaveTimes > 1)
            {
                var pNewStatus = new StatusMore
                {
                    Model = status
                };
                if (await pNewStatus.CreateAsync(this, (int)status.Status, status.Power, (int)status.RemainTime,
                                                 (int)status.LeaveTimes))
                {
                    await StatusSet.AddObjAsync(pNewStatus);
                    return true;
                }
            }
            else
            {
                var pNewStatus = new StatusOnce
                {
                    Model = status
                };
                if (await pNewStatus.CreateAsync(this, (int)status.Status, status.Power, (int)status.RemainTime, 0))
                {
                    await StatusSet.AddObjAsync(pNewStatus);
                    return true;
                }
            }

            return false;
        }

        public Task<bool> AttachStatusAsync(int status, int power, int secs, int times, Magic magic = null, bool save = false)
        {
            return AttachStatusAsync(this, status, power, secs, times, magic, save);
        }

        public async Task<bool> AttachStatusAsync(Role sender, int status, int power, int secs, int times,
                                                          Magic magic = null, bool save = false)
        {
            if (Map == null)
            {
                return false;
            }

            if (QueryStatus(StatusSet.GODLY_SHIELD) != null && IsBadlyStatus(status))
            {
                return false;
            }

            IStatus pStatus = QueryStatus(status);
            if (pStatus != null)
            {
                var changeData = false;
                if (pStatus.Power == power)
                {
                    changeData = true;
                }
                else
                {
                    int minPower = Math.Min(power, pStatus.Power);
                    int maxPower = Math.Max(power, pStatus.Power);

                    if (power <= 30000)
                    {
                        changeData = true;
                    }
                    else
                    {
                        if (minPower >= 30100 || minPower > 0 && maxPower < 30000)
                        {
                            if (power > pStatus.Power)
                            {
                                changeData = true;
                            }
                        }
                        else if (maxPower < 0 || minPower > 30000 && maxPower < 30100)
                        {
                            if (power < pStatus.Power)
                            {
                                changeData = true;
                            }
                        }
                    }
                }

                if (changeData)
                {
                    await pStatus.ChangeDataAsync(power, secs, times, sender?.Identity ?? 0);
                }

                return true;
            }

            if (times > 1)
            {
                var newStatus = new StatusMore();
                if (await newStatus.CreateAsync(this, status, power, secs, times, sender?.Identity ?? 0, magic, save))
                {
                    await StatusSet.AddObjAsync(newStatus);
                    return true;
                }
            }
            else
            {
                var newStatus = new StatusOnce();
                if (await newStatus.CreateAsync(this, status, power, secs, 0, sender?.Identity ?? 0, magic, save))
                {
                    await StatusSet.AddObjAsync(newStatus);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> DetachStatusAsync(int nType)
        {
            return await StatusSet.DelObjAsync(nType);
        }

        public async Task<bool> DetachStatusAsync(ulong nType, bool b64)
        {
            return await StatusSet.DelObjAsync(StatusSet.InvertFlag(nType, b64));
        }

        public virtual bool IsOnXpSkill()
        {
            return false;
        }

        public virtual IStatus QueryStatus(int nType)
        {
            var status = StatusSet?.GetObjByIndex(nType);
            if (status != null && status.IsValid)
            {
                return status;
            }
            return null;
        }

        #endregion

        #region Crime

        public static readonly ulong KeepEffectNotVirtuous = StatusSet.GetFlag(StatusSet.CRIME) |
                                                        StatusSet.GetFlag(StatusSet.RED_NAME) |
                                                        StatusSet.GetFlag(StatusSet.BLACK_NAME);

        public virtual bool IsEvil()
        {
            return QueryStatus(StatusSet.CRIME) != null || QueryStatus(StatusSet.BLACK_NAME) != null;
        }

        public bool IsVirtuous()
        {
            return (StatusFlag1 & KeepEffectNotVirtuous) == 0;
        }

        public bool IsCrime()
        {
            return QueryStatus(StatusSet.CRIME) != null;
        }

        public bool IsPker()
        {
            return QueryStatus(StatusSet.BLACK_NAME) != null;
        }

        public Task SetCrimeStatusAsync(int nSecs)
        {
            return AttachStatusAsync(this, StatusSet.CRIME, 0, nSecs, 0);
		}



		#endregion

		#region Team

		public virtual ITeam GetTeam()
		{
			if (OwnerIdentity != 0)
			{
				Role owner = Map.QueryAroundRole(this, OwnerIdentity);
				if (owner is Character user)
				{
					return user.Team;
				}
			}
			return null;
		}

		#endregion

		#region Processor Queue

		public void QueueAction(Func<Task> task)
        {
            // do not queue actions if not in map
            if (Map != null)
            {
                WorldProcessor.Instance.Queue(Map.Partition, task);
            }
        }

		#endregion

		#region Timer

		public virtual async Task OnTimerAsync()
		{
			if (statusTimer.ToNextTime())
			{
				foreach (IStatus status in StatusSet.Status.Values)
				{
					await status.OnTimerAsync();

					if (!status.IsValid && status.Identity != StatusSet.GHOST && status.Identity != StatusSet.DEAD)
					{
						await StatusSet.DelObjAsync(status.Identity);

						if (this is Character user)
						{
							if (status.Identity == StatusSet.SUPERMAN || status.Identity == StatusSet.CYCLONE)
							{
								await user.FinishXpAsync();
							}
						}
					}
				}

				await CheckDeadMarkAsync();
			}

			if (!IsAlive)
			{
				return;
			}
		}

		#endregion

		#region Socket

		public async Task SendEffectAsync(string effect, bool self)
		{
			var msg = new MsgName
			{
				Identity = Identity,
				Action = StringAction.RoleEffect
			};
			msg.Strings.Add(effect);
			await Map.BroadcastRoomMsgAsync(X, Y, msg, self ? 0 : Identity);
		}

		public Task SendEffectAsync(Character target, string effect)
		{
			var msg = new MsgName
			{
				Identity = Identity,
				Action = StringAction.RoleEffect
			};
			msg.Strings.Add(effect);
			return target.SendAsync(msg);
		}

		public async Task SendAsync(string message, TalkChannel channel = TalkChannel.TopLeft, Color? color = null)
		{
			await SendAsync(new MsgTalk(channel, color ?? Color.White, message));
		}

		public virtual Task SendAsync(IPacket msg)
		{
			logger.Warning($"{GetType().Name} - {Identity} has no SendAsync(IPacket) handler");
			return Task.CompletedTask;
		}

		public virtual Task SendAsync(byte[] msg)
		{
			logger.Warning($"{GetType().Name} - {Identity} has no SendAsync(byte[]) handler");
			return Task.CompletedTask;
		}

		public virtual Task SendSpawnToAsync(Character player)
		{
			logger.Warning($"{GetType().Name} - {Identity} has no SendSpawnToAsync handler");
			return Task.CompletedTask;
		}

		public virtual Task SendSpawnToAsync(Character player, int x, int y)
		{
			logger.Warning($"{GetType().Name} - {Identity} has no SendSpawnToAsync(player, x, y) handler");
			return Task.CompletedTask;
		}

		public virtual async Task BroadcastRoomMsgAsync(IPacket msg, bool self)
		{
			if (Map != null)
			{
				await Map.BroadcastRoomMsgAsync(X, Y, msg, !self ? Identity : 0);
			}
		}

		public virtual Task BroadcastRoomMsgAsync(string message, TalkChannel channel = TalkChannel.TopLeft, Color? color = null)
		{
			return BroadcastRoomMsgAsync(new MsgTalk(channel, color ?? Color.White, message), true);
		}

		#endregion

		#region Constants
		public enum FacingDirection : byte
		{
			Begin = SouthEast,
			SouthWest = 0,
			West = 1,
			NorthWest = 2,
			North = 3,
			NorthEast = 4,
			East = 5,
			SouthEast = 6,
			South = 7,
			End = South,
			Invalid = End + 1
		}

		public enum EntityAction : ushort
		{
			None,
			Dance1 = 1,
			Dance2 = 2,
			Dance3 = 3,
			Dance4 = 4,
			Dance5 = 5,
			Dance6 = 6,
			Dance7 = 7,
			Dance8 = 8,
			Stand = 100,
			Happy = 150,
			Angry = 160,
			Sad = 170,
			Wave = 190,
			Bow = 200,
			Kneel = 210,
			Cool = 230,
			Sit = 250,
			Lie = 270,

			InteractionKiss = 34466,
			InteractionHold = 34468,
			InteractionHug = 34469,
			CoupleDances = 34474
		}

		public const uint USER_KILL_ACTION = 80_000_001;
		public const uint USER_DIE_ACTION = 80_000_003;
		public const uint USER_UPLEV_ACTION = 80_000_004;
		public const uint MONSTER_DIE_ACTION = 80_000_010;

		public const byte MAX_UPLEV = 140;

		public const int EXPBALL_AMOUNT = 600;
		public const int CHGMAP_LOCK_SECS = 10;
		public const int ADD_ENERGY_STAND_MS = 1000;
		public const int ADD_ENERGY_STAND = 3;
		public const int ADD_ENERGY_SIT = 15;
		public const int ADD_ENERGY_LIE = ADD_ENERGY_SIT / 2;
		public const int DEFAULT_USER_ENERGY = 70;
		public const int MAX_USER_ATTRIB_POINTS = 900;
		public const int KEEP_STAND_MS = 1500;
		public const int MIN_SUPERMAP_KILLS = 25;
		public const int VETERAN_DIFF_LEVEL = 20;
		public const int HIGHEST_WATER_WIZARD_PROF = 135;
		public const int SLOWHEALLIFE_MS = 1000;
		public const int AUTOHEALLIFE_TIME = 10;
		public const int AUTOHEALLIFE_EACHPERIOD = 6;
		public const int TICK_SECS = 10;
		public const int MAX_PKLIMIT = 10000;
		public const int PILEMONEY_CHANGE = 5000;
		public const int ADDITIONALPOINT_NUM = 3;
		public const int PK_DEC_TIME = 360;
		public const int PKVALUE_DEC_ONCE = -1;
		public const int PKVALUE_DEC_ONCE_IN_PRISON = -3;
		public const int USER_ATTACK_SPEED = 1000;
		public const int POISONDAMAGE_INTERVAL = 2;
		public const long MAX_INVENTORY_MONEY = 10_000_000_000;
		public const long MAX_STORAGE_MONEY = 1_000_000_000;
		public const uint MAX_INVENTORY_EMONEY = 1_000_000_000;

		public const int MAX_STRENGTH_POINTS_VALUE = 4000; // Chi Points

		public const int MASTER_WEAPONSKILLLEVEL = 12;
		public const int MAX_WEAPONSKILLLEVEL = 20;

		public const int MAX_MENUTASKSIZE = 10;
		public const int MAX_VAR_AMOUNT = 16;

		public const int SYNWAR_PROFFER_PERCENT = 1;
		public const int SYNWAR_MONEY_PERCENT = 2;
		public const int SYNWAR_NOMONEY_DAMAGETIMES = 10;

		public const int MAX_ATTRIBUTE_POINTS = 900;

		public const int NPCDIEDELAY_SECS = 10;

		public const int MAX_JUMP_ALTITUDE = 0x64; // according to CHero::CanJump(C3_POS)
		#endregion

	}
}
