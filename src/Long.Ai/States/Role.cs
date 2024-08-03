using Long.Ai.Managers;
using Long.Ai.Sockets.Packets;
using Long.Ai.States.World;
using Long.Database.Entities;
using Long.Network.Packets;
using Long.Network.Packets.Ai;
using Long.Shared.Mathematics;
using Long.World.Map;
using Long.World.Roles;
using Serilog;
using System.Drawing;

namespace Long.Ai.States
{
    public abstract class Role : WorldObject
    {
		private static readonly Serilog.ILogger logger = Log.ForContext<Role>();

		protected uint idMap;

        protected ushort currentX,
                         currentY;

        protected uint maxLife = 0,
                       maxMana = 0;

        public Role()
        {
            StatusSet = new (this);
        }

        #region Identity

        public virtual uint OwnerIdentity { get; set; }

        #endregion

        #region Appearence

        public virtual uint Mesh { get; set; }

        #endregion

        #region Level

        public virtual byte Level { get; set; }

        #endregion

        #region Map and Position

        public virtual GameMap Map { get; protected set; }

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

        public virtual int GetDistance(Role role)
        {
            if (role.MapIdentity != MapIdentity)
            {
                return int.MaxValue;
            }

            return GetDistance(role.X, role.Y);
        }

        public virtual int GetDistance(int x, int y)
        {
            return Calculations.GetDistance(X, Y, x, y);
        }

        /// <summary>
        /// </summary>
        public virtual Task EnterMapAsync(bool sync = true)
        {
            Map = MapManager.GetMap(MapIdentity);
            if (Map != null)
                return Map.AddAsync(this);
            return Task.CompletedTask;
        }

        /// <summary>
        /// </summary>
        public virtual async Task LeaveMapAsync(bool sync = true)
        {
            if (Map != null)
            {
                await Map.RemoveAsync(Identity);
            }

            Map = null;
        }

        #endregion

        #region Movement

        public virtual bool JumpPos(int x, int y)
        {
            if (x == X && y == Y)
            {
                return false;
            }

            if (Map == null || !Map.IsValidPoint(x, y))
            {
                return false;
            }

            Map.EnterBlock(this, x, y, X, Y);

            Direction = (FacingDirection)Calculations.GetDirectionSector(X, Y, x, y);

            currentX = (ushort)x;
            currentY = (ushort)y;

            if (!IsPlayer())
            {
                BroadcastMsg(new MsgAiAction
                {
                    Data = new MsgAiActionContract
					{
						Action = AiActionType.Jump,
						Direction = (int)Direction,
						X = currentX,
						Y = currentY,
						Identity = Identity
					}
                });
            }

            ProcessAfterMove();
            return true;
        }

        public bool MoveToward(int direction, int mode)
        {
            var user = this as Character;
            ushort newX;
            ushort newY;
            bool run = mode == 1;
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
                return false;
            }

            Map.EnterBlock(this, newX, newY, X, Y);

            Direction = (FacingDirection)direction;

            currentX = newX;
            currentY = newY;

            if (!IsPlayer())
            {
                BroadcastMsg(new MsgAiAction
                {
                    Data = new MsgAiActionContract
                    {
                        Action = run ? AiActionType.Run : AiActionType.Walk,
                        Direction = (int)Direction,
                        X = currentX,
                        Y = currentY,
                        Identity = Identity,
                        TargetIdentity = (uint)mode
                    }
                });
            }

            ProcessAfterMove();
            return true;
        }

        public bool JumpBlock(int x, int y, FacingDirection dir)
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
                    JumpPos(pos.X, pos.Y);
                    return true;
                }
            }

            if (Map.IsStandEnable(x, y))
            {
                JumpPos(x, y);
                return true;
            }

            return false;
        }

        public bool FarJump(int x, int y, FacingDirection dir)
        {
            int steps = GetDistance(x, y);

            if (steps <= 0)
            {
                return false;
            }

            if (Map.IsStandEnable(x, y))
            {
                JumpPos(x, y);
                return true;
            }

            for (var i = 0; i < steps; i++)
            {
                var pos = new Point(X + (x - X) * i / steps, Y + (y - Y) * i / steps);
                if (Map.IsStandEnable(pos.X, pos.Y))
                {
                    JumpPos(pos.X, pos.Y);
                    return true;
                }
            }

            return false;
        }

        public virtual void ProcessOnMove()
        {
        }

        public virtual void ProcessAfterMove()
        {
            Action = EntityAction.Stand;
        }

        public virtual void ProcessOnAttack()
        {
            Action = EntityAction.Stand;
        }

        public bool IsJumpPass(int x, int y, int alt)
        {
            var setLine = Bresenham.Calculate(X, Y, x, y);

            if (x != setLine[setLine.Count - 1].X)
            {
                return false;
            }

            if (y != setLine[setLine.Count - 1].Y)
            {
                return false;
            }

            int currentAltitude = Map.GetFloorAlt(X, Y);
            foreach (Point point in setLine)
            {
                int nextCellAltitude = Map.GetFloorAlt(point.X, point.Y);
                if (nextCellAltitude - currentAltitude > alt)
                {
                    return false;
                }
            }
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
                await SendAsync(new MsgAiAction
                {
                    Data = new MsgAiActionContract
					{
						Action = AiActionType.SetDirection,
						TargetIdentity = (ushort)direction,
						Direction = (int)Direction
					}
                });
            }
        }

        public virtual async Task SetActionAsync(EntityAction action, bool sync = true)
        {
            if (action != EntityAction.Cool && IsWing)
            {
                return;
            }

            Action = action;
            if (sync)
            {
                await SendAsync(new MsgAiAction
                {
                    Data = new MsgAiActionContract
                    {
                        Identity = Identity,
                        Action = AiActionType.SetAction,
                        TargetIdentity = (ushort)action,
                        Direction = (int)Direction
                    }
                });
            }
        }

        #endregion

        #region Attributes

        public virtual int SizeAddition => 1;
        public virtual int BattlePower => 0;

        #endregion

        #region Role Type

        public bool IsPlayer()
        {
			Character user = this as Character;

			return user != null;
			//return Identity >= PLAYER_ID_FIRST && Identity < PLAYER_ID_LAST;
        }

        public bool IsMonster()
        {
            return Identity >= MONSTERID_FIRST && Identity < MONSTERID_LAST;
        }

        public bool IsNpc()
        {
            return Identity >= SYSNPCID_FIRST && Identity < SYSNPCID_LAST;
        }

        public bool IsDynaNpc()
        {
            return Identity >= DYNANPCID_FIRST && Identity < DYNANPCID_LAST;
        }

        public bool IsCallPet()
        {
            return Identity >= CALLPETID_FIRST && Identity < CALLPETID_LAST;
        }

        public bool IsTrap()
        {
            return Identity >= TRAPID_FIRST && Identity < TRAPID_LAST;
        }

        public bool IsMapItem()
        {
            return Identity >= MAPITEM_FIRST && Identity < MAPITEM_LAST;
        }

        public bool IsFurniture()
        {
            return Identity >= SCENE_NPC_MIN && Identity < SCENE_NPC_MAX;
        }

        #endregion

        #region Life and Mana

        public virtual uint Life { get; set; }
        public virtual uint MaxLife => maxLife;
        public virtual uint Mana { get; set; }
        public virtual uint MaxMana => maxMana;

        #endregion

        #region Battle

        public virtual bool IsAlive => QueryStatus(StatusSet.DEAD) == null;

        public virtual Task<bool> BeAttackAsync(Role attacker)
        {
            return Task.FromResult(false);
        }

        public virtual int GetAttackRange(int sizeAdd)
        {
            return sizeAdd + 1;
        }

        public virtual bool IsAttackable(Role attacker)
        {
            return true;
        }

        public virtual bool IsFarWeapon()
        {
            return false;
        }

        #endregion

        #region Status

        protected const int maxStatus = 128;

        public virtual bool IsWing => QueryStatus(StatusSet.FLY) != null;
        public bool IsGhost => QueryStatus(StatusSet.GHOST) != null;

        public ulong StatusFlag1 { get; set; }
        public ulong StatusFlag2 { get; set; }
        public uint StatusFlag3 { get; set; }

        public StatusSet StatusSet { get; }

        public virtual async Task<bool> DetachWellStatusAsync()
        {
            for (var i = 1; i < maxStatus; i++)
            {
                if (StatusSet[i] != null)
                {
                    if (IsWellStatus(i))
                    {
                        await DetachStatusAsync(i);
                    }
                }
            }

            return true;
        }

        public virtual async Task<bool> DetachBadlyStatusAsync()
        {
            for (var i = 1; i < maxStatus; i++)
            {
                if (StatusSet[i] != null)
                {
                    if (IsBadlyStatus(i))
                    {
                        await DetachStatusAsync(i);
                    }
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

        public virtual bool IsWellStatus(int stts)
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
                case StatusSet.VORTEX:
                case StatusSet.FLY:
                case StatusSet.FATAL_STRIKE:
                case StatusSet.AZURE_SHIELD:
                case StatusSet.SUPER_SHIELD_HALO:
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
                    return true;
            }

            return false;
        }

        public virtual bool IsBadlyStatus(int stts)
        {
            switch (stts)
            {
                case StatusSet.POISONED:
                case StatusSet.CONFUSED:
                case StatusSet.ICE_BLOCK:
                case StatusSet.HUGE_DAZED:
                case StatusSet.DAZED:
                case StatusSet.SOUL_SHACKLE:
                case StatusSet.POISON_STAR:
                case StatusSet.TOXIC_FOG:
                    return true;
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

        public Task<bool> AttachStatusAsync(int status, int power, int secs, int times, byte level, bool save = false)
        {
            return AttachStatusAsync(this, status, power, secs, times, level, save);
        }

        public async Task<bool> AttachStatusAsync(Role sender, int status, int power, int secs, int times,
                                                          byte level, bool save = false)
        {
            if (Map == null)
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
                if (await newStatus.CreateAsync(this, status, power, secs, times, sender?.Identity ?? 0, level,
                                                 save))
                {
                    await StatusSet.AddObjAsync(newStatus);
                    return true;
                }
            }
            else
            {
                var newStatus = new StatusOnce();
                if (await newStatus.CreateAsync(this, status, power, secs, 0, sender?.Identity ?? 0, level, save))
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
            return StatusSet?.GetObjByIndex(nType);
        }

        #endregion

        #region Crime

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
            return AttachStatusAsync(this, StatusSet.CRIME, 0, nSecs, 0, 0);
        }

        #endregion

        #region Processor Queue

        public void QueueAction(Func<Task> task)
        {
            if (Map == null)
            {
                task().GetAwaiter().GetResult();
                return;
            }
            Kernel.Services.Processor.Queue(Map != null ? Map.Partition : 0, task);
        }

        #endregion

        #region OnTimer

        public virtual Task OnTimerAsync()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Socket

        public Task SendAsync(IPacket packet)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Constants

        public static readonly ulong KeepEffectNotVirtuous = StatusSet.GetFlag(StatusSet.CRIME) |
                                                        StatusSet.GetFlag(StatusSet.RED_NAME) |
                                                        StatusSet.GetFlag(StatusSet.BLACK_NAME);

        public const int SCENEID_FIRST = 1;
        public const int SYSNPCID_FIRST = 1;
        public const int SYSNPCID_LAST = 99999;
        public const int DYNANPCID_FIRST = 100000;
        public const int DYNANPCID_LAST = 199999;
        public const int SCENE_NPC_MIN = 200000;
        public const int SCENE_NPC_MAX = 299999;
        public const int SCENEID_LAST = 299999;

        public const int NPCSERVERID_FIRST = 400001;
        public const int MONSTERID_FIRST = 400001;
        public const int MONSTERID_LAST = 499999;
        public const int PETID_FIRST = 500001;
        public const int PETID_LAST = 599999;
        public const int NPCSERVERID_LAST = 699999;

        public const int CALLPETID_FIRST = 700001;
        public const int CALLPETID_LAST = 799999;

        public const int MAPITEM_FIRST = 800001;
        public const int MAPITEM_LAST = 899999;

        public const int TRAPID_FIRST = 900001;
        public const int MAGICTRAPID_FIRST = 900001;
        public const int MAGICTRAPID_LAST = 989999;
        public const int SYSTRAPID_FIRST = 990001;
        public const int SYSTRAPID_LAST = 999999;
        public const int TRAPID_LAST = 999999;

        public const int DYNAMAP_FIRST = 1000000;

        public const int PLAYER_ID_FIRST = 1000000;
        public const int PLAYER_ID_LAST = 1999999999;

        public const byte MAX_UPLEV = 140;

        public const int EXPBALL_AMOUNT = 600;
        public const int CHGMAP_LOCK_SECS = 10;
        public const int ADD_ENERGY_STAND_MS = 1000;
        public const int ADD_ENERGY_STAND = 3;
        public const int ADD_ENERGY_SIT = 15;
        public const int ADD_ENERGY_LIE = ADD_ENERGY_SIT / 2;
        public const int DEFAULT_USER_ENERGY = 70;
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
        public const int PK_DEC_TIME = 180;
        public const int PKVALUE_DEC_ONCE = -1;
        public const int PKVALUE_DEC_ONCE_IN_PRISON = -3;
        public const int USER_ATTACK_SPEED = 1000;
        public const int POISONDAMAGE_INTERVAL = 2;
        public const int MAX_STORAGE_MONEY = int.MaxValue;

        public const int MAX_STRENGTH_POINTS_VALUE = 4000; // Chi Points

        public const int MASTER_WEAPONSKILLLEVEL = 12;
        public const int MAX_WEAPONSKILLLEVEL = 20;

        public const int MAX_MENUTASKSIZE = 8;
        public const int MAX_VAR_AMOUNT = 16;

        public const int SYNWAR_PROFFER_PERCENT = 1;
        public const int SYNWAR_MONEY_PERCENT = 2;
        public const int SYNWAR_NOMONEY_DAMAGETIMES = 10;

        public const int NPCDIEDELAY_SECS = 10;

        #endregion

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

        public enum RoleMoveMode
        {
            Walk = 0,

            // PathMove()
            Run,
            Shift,

            // to server only
            Jump,
            Trans,
            Chgmap,
            JumpMagicAttack,
            Collide,
            Synchro,

            // to server only
            Track,

            RunDir0 = 20,

            RunDir7 = 27
        }
    }
}