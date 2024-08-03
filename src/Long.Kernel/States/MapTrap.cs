using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Processors;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States.Magics;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Shared.Managers;
using Long.Shared.Mathematics;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.Network.Game.Packets.MsgMapItem;

namespace Long.Kernel.States
{
    public sealed class MapTrap : Role
    {
        private static readonly ILogger logger = Log.ForContext<MapTrap>();

        private readonly TimeOutMS fightTimer;
        private readonly TimeOut lifePeriodTimer;

        private DbTrap trap;
        private Role owner;
        private Magic magic;
        private int activeTimes;
        private Point castPosition;
        private List<Point> ddaLine;

		public MapTrap(DbTrap trap)
        {
            this.trap = trap;

            Identity = trap.Id;
            MapIdentity = trap.MapId;
            X = trap.PosX;
            Y = trap.PosY;

            lifePeriodTimer = new TimeOut();
            lifePeriodTimer.Clear();
            fightTimer = new TimeOutMS();
            fightTimer.Clear();
        }

        public uint Type => trap.TypeId;
        public uint IdAction => trap.Type.ActionId;
        public int ActiveTimes => activeTimes;
        public int RemainingActiveTimes { get; set; }
        public override byte Level
        {
            get => trap.Type.Level;
            set => trap.Type.Level = value;
        }
        public override int MinAttack => trap.Type.AttackMin;
        public override int MaxAttack => trap.Type.AttackMax;
        public override int MagicAttack => trap.Type.AttackMax;
        public TargetType AttackMode => (TargetType)trap.Type.AtkMode;
        public override int AttackSpeed => trap.Type.AttackSpeed;
        public ushort MagicType => trap.Type.MagicType;
        public int MagicHitRate => trap.Type.MagicHitrate;
        public int Size => trap.Type.Size;

        public TrapTypeSort Sort => (TrapTypeSort)trap.Type.Sort;

        public bool IsTrapSort => trap.Type.Sort <= 10;

        public bool IsAutoSort => !IsTrapSort;

        public override bool IsAlive => RemainingActiveTimes > 0;

        public bool IsInRange(Role target)
        {
            return GetDistance(target) <= Size;
        }

        #region Initialization

        public async Task<bool> InitializeActionAsync(Role owner)
        {
            trap.Type ??= await TrapTypeRepository.GetAsync(trap.TypeId);

            if (trap.Type == null)
            {
                logger.Information($"Trap has no type {trap.Type} [TrapId: {trap.Id}]");
                return false;
            }

            this.owner = owner;

            RemainingActiveTimes = activeTimes = trap.Type.ActiveTimes;

            fightTimer.Startup(trap.Type.AttackSpeed);

            Mesh = trap.Look;

            idMap = trap.MapId;
            currentX = trap.PosX;
            currentY = trap.PosY;
            return true;
        }

        public async Task<bool> InitializeAsync(Role owner = null, Magic magic = null)
        {
            trap.Type ??= await TrapTypeRepository.GetAsync(trap.TypeId);

            if (trap.Type == null)
            {
                logger.Information($"Trap has no type {trap.Type} [TrapId: {trap.Id}]");
                return false;
            }

            this.owner = owner;
            this.magic = magic;

            if (magic != null && trap.Type.ActiveTimes == 0)
            {
                RemainingActiveTimes = activeTimes = (int)magic.ActiveTimes;
            }
            else
            {
                RemainingActiveTimes = activeTimes = trap.Type.ActiveTimes;
            }

            if (owner != null)
            {
                castPosition = new Point(owner.X, owner.Y);
            }
            else
            {
                castPosition = new Point(trap.PosX, trap.PosY);
            }

            fightTimer.Startup(trap.Type.AttackSpeed);

            Mesh = trap.Look;

            idMap = trap.MapId;
            currentX = trap.PosX;
            currentY = trap.PosY;

            await EnterMapAsync();
            return true;
        }

        #endregion

        #region Map

        public override Task EnterMapAsync()
        {
            Map = MapManager.GetMap(MapIdentity);
            if (Map != null)
            {
                return Map.AddAsync(this);
            }
            return Task.CompletedTask;
        }

        public override async Task LeaveMapAsync()
        {
            await BroadcastRoomMsgAsync(new MsgMapItem
            {
                Identity = Identity,
                Itemtype = Mesh,
                MapX = X,
                MapY = Y,
                Mode = DropType.DropTrap
            }, false);

            if (Map != null)
            {
				WorldProcessor.Instance.Queue(Map.Partition, async () =>
				{
					await Map.RemoveAsync(Identity);
				});
            }

            if (Identity >= IdentityManager.MAGICTRAPID_FIRST && Identity <= IdentityManager.MAGICTRAPID_LAST)
                IdentityManager.Traps.ReturnIdentity(Identity);
        }

        #endregion

        #region Trap Attack

        public async Task TrapAttackAsync(Role target)
        {
            if (!IsTrapSort)
                return;

            if (!fightTimer.ToNextTime(AttackSpeed))
                return;

            if (RemainingActiveTimes > 0)
                RemainingActiveTimes--;

            if (!target.IsAttackable(this))
                return;

            if (owner?.IsImmunity(target) == true)
                return;

            if (target is Character && !AttackMode.HasFlag(TargetType.User))
                return;

            if (target is Monster && !AttackMode.HasFlag(TargetType.Monster))
                return;

            Character user = owner as Character;
            if (user?.IsEvil() == true)
                await user.SetCrimeStatusAsync(30);

            if (!AttackMode.HasFlag(TargetType.Passive))
            {
                BattleSystem.CreateBattle(target.Identity);
                QueueAction(BattleSystem.ProcessAttackAsync);
            }

            if (IdAction > 0)
            {
                if (AttackMode.HasFlag(TargetType.User))
                    await GameAction.ExecuteActionAsync(IdAction, target as Character, this, null, "");
                else if (AttackMode.HasFlag(TargetType.Monster))
                    await GameAction.ExecuteActionAsync(IdAction, null, target, null, "");
                else
                    await GameAction.ExecuteActionAsync(IdAction, null, null, null, "");
            }

            if (ActiveTimes > 0 && RemainingActiveTimes <= 0)
                await LeaveMapAsync();
        }

        private async Task MagicAttackAsync()
        {
            if (magic == null || !IsAutoSort)
            {
                return;
            }

            switch (Sort)
            {
                case TrapTypeSort.BombTrap:
                    {
                        await ProcessTrapBombAsync(magic);
                        break;
                    }

                case TrapTypeSort.ThickLineTrap:
                    {
                        await ProcessThickLineTrapAsync(magic);
                        break;
                    }
            }
        }

        private async Task ProcessTrapBombAsync(Magic magic)
        {
            List<Role> roles = CollectTargetBomb(trap.Type.Size / 2);

            long battleExp = 0;
            var hitByMagic = MagicData.HitByMagic(magic);
            var user = owner as Character;
            var currentEvent = user?.GetCurrentEvent();
            foreach (Role target in roles)
            {
                if (magic.Ground != 0 && target.IsWing)
                {
                    continue;
                }

                var atkResult = await BattleSystem.CalcPowerAsync(hitByMagic, owner ?? this, target, magic.Power);
                int damage = atkResult.Damage;
                if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
                {
                    await user.SendEffectAsync("LuckyGuy", true);
                    damage *= 2;
                }

                damage += atkResult.ElementalDamage;

                await BroadcastRoomMsgAsync(new MsgInteract
                {
                    SenderIdentity = Identity,
                    TargetIdentity = target.Identity,
                    MagicType = magic.Type,
                    MagicLevel = magic.Level,
                    Data = damage,
                    PosX = X,
                    PosY = Y,
                    Action = MsgInteractType.Attack
                }, false);

                var lifeLost = (int)Math.Min(damage, target.Life);
                await target.BeAttackAsync(hitByMagic, this, damage, true);

                if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
                {
                    battleExp += user.AdjustExperience(target, lifeLost, false);
                    if (!target.IsAlive)
                    {
                        var nBonusExp = (int)(target.MaxLife * 20 / 100d);
                        if (user.Team != null)
                        {
                            await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
                        }
                        battleExp += user.AdjustExperience(target, nBonusExp, false);
                    }
                }

                if (user != null && target is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
                {
                    dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
                }

                if (currentEvent != null)
                {
                    await currentEvent.OnHitAsync(user, target, magic);
                }

                if (user != null)
                {
                    await user.CheckCrimeAsync(target);
                }

                if (!target.IsAlive)
                {
                    await KillAsync(target, MagicData.GetDieMode());
                }
            }

            if (currentEvent != null)
            {
                await currentEvent.OnAttackAsync(user);
            }
            
            if (user != null)
            {
                await user.MagicData.AwardExpAsync(0, battleExp, battleExp, magic);
            }
        }

        private async Task ProcessThickLineTrapAsync(Magic magic)
        {
            int range = (int)(magic.Range * 2 * magic.ActiveTimes);

            if (ddaLine == null)
            {
                ddaLine = new List<Point>();
                Calculations.DDALine(castPosition.X, castPosition.Y, X, Y, range, ref ddaLine);
            }

            int factor = 3 - RemainingActiveTimes;
            int delta = (int)(ddaLine.Count / magic.ActiveTimes);
            Point from;
            Point to;
            Point destination;
            if (factor == 1)
            {
                destination = ddaLine.Skip(delta / 2).FirstOrDefault();
                from = ddaLine.FirstOrDefault();
                to = ddaLine.Skip(delta - 1).FirstOrDefault();
            }
            else if (factor == 2)
            {
                destination = ddaLine.Skip(delta / 2 + delta).FirstOrDefault();
                from = ddaLine.Skip(delta).FirstOrDefault();
                to = ddaLine.Skip(delta * 2).FirstOrDefault();
            }
            else
            {
                destination = ddaLine.Skip(delta / 2 + delta * 2).FirstOrDefault();
                from = ddaLine.Skip(delta * 2).FirstOrDefault();
                to = ddaLine.LastOrDefault();
            }

            if (from.Equals(default) || destination.Equals(default))
            {
                logger.Warning($"Error on {RemainingActiveTimes} remaining times from thickline! {magic.Type}:{magic.Name}:{magic.Level}");
                return;
            }

            var bresenham = Bresenham.CalculateThick(from.X, from.Y, to.X, to.Y, magic.Width);
            var targets = CollectTargetBomb(range);
            int power1 = (int)magic.StatusData2;
            int power2 = (int)magic.DurTime;
            int power3 = (int)magic.AtkInterval;

            MsgMagicEffect msg = new MsgMagicEffect
            {
                AttackerIdentity = owner.Identity,
                Command = owner.Identity,
                MagicIdentity = magic.Type,
                MagicLevel = magic.Level
            };

            var hitByMagic = MagicData.HitByMagic(magic);
            long battleExp = 0;
            Character user = owner as Character;
            var currentEvent = user?.GetCurrentEvent();
            Dictionary<uint, int> setDamage = new();
            List<Role> setTarget = new();
            foreach (var target in targets)
            {
                if (!bresenham.Any(point => point.X == target.X && point.Y == target.Y))
                {
                    continue;
                }

                int damage = 0;
                InteractionEffect effect = InteractionEffect.None;
                if (!await target.CheckScapegoatAsync(owner))
                {
                    if (msg.Count >= MagicData.MAX_TARGET_NUM)
                    {
                        await owner.BroadcastRoomMsgAsync(msg, true);
                        msg.ClearTargets();
                    }

                    if (user != null)
                    {
                        await user.CheckCrimeAsync(target);
                    }

                    int powerSector = (int)Math.Ceiling(owner.GetDistance(target) / 3d);
                    int power;
                    if (RemainingActiveTimes == 2)
                    {
                        power = power1;
                    }
                    else if (RemainingActiveTimes == 1)
                    {
                        power = power2;
                    }
                    else
                    {
                        power = power3;
                    }

                    var result = await BattleSystem.CalcPowerAsync(hitByMagic, owner, target, power);
                    damage = result.Damage + result.ElementalDamage;
                    effect = result.Effect;
                }

                msg.Append(target.Identity, damage, damage != 0, effect);

                setDamage.Add(target.Identity, damage);
                setTarget.Add(target);
            }

            await owner.BroadcastRoomMsgAsync(new MsgAction
            {
                Action = (MsgAction.ActionType)434,
                Identity = Identity,
                CommandX = (ushort)destination.X,
                CommandY = (ushort)destination.Y,
                X = (ushort)castPosition.X,
                Y = (ushort)castPosition.Y
            }, true);

            if (msg.Count > 0)
            {
                await owner.BroadcastRoomMsgAsync(msg, true);
            }

            foreach (var target in setTarget)
            {
                int damage = setDamage[target.Identity];
                var lifeLost = (int)Math.Min(damage, target.Life);
                await target.BeAttackAsync(hitByMagic, this, damage, true);
                if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
                {
                    battleExp += user.AdjustExperience(target, lifeLost, false);
                    if (!target.IsAlive)
                    {
                        var nBonusExp = (int)(target.MaxLife * 20 / 100d);
                        if (user.Team != null)
                        {
                            await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
                        }
                        battleExp += user.AdjustExperience(target, nBonusExp, false);
                    }
                }

                if (user != null && target is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
                {
                    dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
                }

                if (currentEvent != null)
                {
                    await currentEvent.OnHitAsync(user, target, magic);
                }

                if (!target.IsAlive)
                {
                    await KillAsync(target, MagicData.GetDieMode());
                }
            }

            if (currentEvent != null)
            {
                await currentEvent.OnAttackAsync(user);
            }

            if (user != null)
            {
                await user.MagicData.AwardExpAsync(0, battleExp, battleExp, magic);
            }

            castPosition = new(destination.X, destination.Y);
        }

        private List<Role> CollectTargetBomb(int range)
        {
            List<Role> targets = new();
            List<Role> setRoles = Map.Query9BlocksByPos(X, Y);
            foreach (Role target in setRoles)
            {
                if (target.Identity == Identity || target.Identity == owner?.Identity)
                {
                    continue;
                }

                if (target.GetDistance(X, Y) > range)
                {
                    continue;
                }

                bool isCharacter = target is Character;
                bool isMonster = target is Monster;
                if (!AttackMode.HasFlag(TargetType.User) && isCharacter)
                {
                    continue;
                }

                //if (!AttackMode.HasFlag(TargetType.Monster) && isMonster)
                //{
                //    continue;
                //}

                if (!isCharacter && !isMonster)
                {
                    continue;
                }

                Role role = owner ?? this;
                if (role.IsImmunity(target) || !target.IsAttackable(owner ?? this))
                {
                    continue;
                }

                targets.Add(target);
            }
            return targets;
        }

        public override async Task KillAsync(Role target, uint dieWay)
        {
            if (target == null)
                return;

            if (owner != null)
            {
                await owner.KillAsync(target, dieWay);
                return;
            }

            await BroadcastRoomMsgAsync(new MsgInteract
            {
                Action = MsgInteractType.Kill,
                SenderIdentity = Identity,
                TargetIdentity = target.Identity,
                PosX = target.X,
                PosY = target.Y,
                Data = (int)dieWay
            }, true);

            await target.BeKillAsync(this);
        }

        #endregion

        #region Timer

        public override async Task OnTimerAsync()
        {
            if (!IsAlive && !IsAutoSort)
                return;

            if (lifePeriodTimer.IsActive())
            {
                if (lifePeriodTimer.ToNextTime())
                {
                    await LeaveMapAsync();
                    lifePeriodTimer.Clear();
                }
            }

            if (IsAutoSort)
            {
                if (fightTimer.ToNextTick(AttackSpeed))
                {
                    if (ActiveTimes > 0)
                        RemainingActiveTimes--;

                    await MagicAttackAsync();

                    if ((ActiveTimes > 0 || IsAutoSort) && RemainingActiveTimes <= 0)
                    {
                        await LeaveMapAsync();
                        IdentityManager.Traps.ReturnIdentity(Identity);
                    }
                }
            }
        }

        #endregion

        #region Socket

        public override Task SendSpawnToAsync(Character player)
        {
            return player.SendAsync(new MsgMapItem
            {
                Identity = Identity,
                Itemtype = Mesh,
                MapX = X,
                MapY = Y,
                Mode = DropType.SynchroTrap
            });
        }

        #endregion

        [Flags]
        public enum TargetType
        {
            None,
            User = 1,
            Monster = 2,
            Passive = 4
        }

        public enum TrapTypeSort
        {
            SystemTrap = 1,
            TrapSort = 10,
            BombTrap = 11,
            ThickLineTrap = 12
        }
    }
}
