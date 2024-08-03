using Long.Ai.Managers;
using Long.Ai.Sockets;
using Long.Ai.Sockets.Packets;
using Long.Ai.States.World;
using Long.Database.Entities;
using Long.Network.Packets.Ai;
using Long.Shared.Managers;
using Long.Shared.Mathematics;
using Long.World.Map;
using System.Drawing;
using System.Security.Principal;

namespace Long.Ai.States
{
    public sealed class Monster : Role
    {
        private readonly TimeOutMS action = new();
        private readonly TimeOutMS move = new();
        private readonly TimeOut leaveMap = new();

        private readonly DbMonstertype monstertype;
        private readonly Generator generator;

        private readonly List<MonsterMagic> monsterMagics = new();

        private uint moveTarget;
        private uint actTarget;
        private uint idAtkMe;
		public Monster(DbMonstertype type, uint identity)
        {
			Identity = identity;
			monstertype = type;
			aiStage = AiStage.Idle;
		}

        public Monster(DbMonstertype type, uint identity, Generator generator)
        {
            Identity = identity;
            monstertype = type;
            this.generator = generator;
            aiStage = AiStage.Idle;
        }

        public async Task<bool> InitializeAsync(uint idMap, ushort x, ushort y)
        {
            this.idMap = idMap;

            if (monstertype == null || (Map = MapManager.GetMap(idMap)) == null)
                return false;

            currentX = x;
            currentY = y;

            Life = MaxLife;

            foreach (DbMonsterTypeMagic dbm in RoleManager.GetMonsterMagics(Type))
            {
                if (dbm.MagicType1 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType1, dbm.ColdTime1, dbm.WarningTime1));
                }
                if (dbm.MagicType2 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType2, dbm.ColdTime2, dbm.WarningTime2));
                }
                if (dbm.MagicType3 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType3, dbm.ColdTime3, dbm.WarningTime3));
                }
                if (dbm.MagicType4 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType4, dbm.ColdTime4, dbm.WarningTime4));
                }
                if (dbm.MagicType5 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType5, dbm.ColdTime5, dbm.WarningTime5));
                }
                if (dbm.MagicType6 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType6, dbm.ColdTime6, dbm.WarningTime6));
                }
                if (dbm.MagicType7 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType7, dbm.ColdTime7, dbm.WarningTime7));
                }
                if (dbm.MagicType8 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType8, dbm.ColdTime8, dbm.WarningTime8));
                }
                if (dbm.MagicType9 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType9, dbm.ColdTime9, dbm.WarningTime9));
                }
                if (dbm.MagicType10 != 0)
                {
                    monsterMagics.Add(new MonsterMagic(dbm.MagicType10, dbm.ColdTime10, dbm.WarningTime10));
                }
            }

            if (monstertype.MagicType != 0)
            {
                monsterMagics.Add(new MonsterMagic(monstertype.MagicType, (uint)Math.Max(500, monstertype.AttackSpeed), 0));
            }

            action.Startup(monstertype.AttackSpeed);
            move.Startup(monstertype.MoveSpeed);
            return true;
        }

        #region Identity

        public override string Name
        {
            get => monstertype.Name;
        }

        #endregion

        #region Appearence

        public uint Type => monstertype.Id;

        public override uint Mesh
        {
            get => monstertype.Lookface;
            set => monstertype.Lookface = (ushort)value;
        }

        public override int SizeAddition => (int)monstertype.SizeAdd;

        #endregion

        #region Attributes

        public override byte Level
        {
            get => (byte)(monstertype?.Level ?? 0);
            set => monstertype.Level = value;
        }

        public uint AttackUser => monstertype?.AttackUser ?? 0;

        public int ViewRange => monstertype?.ViewRange ?? 1;

        #endregion

        #region Map

        public override async Task EnterMapAsync(bool sync = true)
        {
            if (sync)
            {
                var msg = new MsgAiSpawnNpc
                {
                    Mode = AiSpawnNpcMode.Spawn
                };
                msg.List.Add(new MsgAiSpawnNpc<GameServer>.SpawnNpc
                {
                    Id = Identity,
                    GeneratorId = generator.Identity,
                    MapId = MapIdentity,
                    X = X,
                    Y = Y,
                    MonsterType = Type,
                    OwnerId = 0
                });
                BroadcastMsg(msg);
            }
            await base.EnterMapAsync(sync);
        }

        public override async Task LeaveMapAsync(bool sync = true)
        {
            generator.Remove(Identity);
            IdentityManager.Monster.ReturnIdentity(Identity);

            if (sync)
            {
                var msg = new MsgAiSpawnNpc();
                msg.Mode = AiSpawnNpcMode.DestroyNpc;
                msg.List.Add(new MsgAiSpawnNpc<GameServer>.SpawnNpc
                {
                    Id = Identity
                });
                BroadcastMsg(msg);
            }
			
			await base.LeaveMapAsync(sync);
        }

        #endregion

        #region Checks

        public override bool IsEvil()
        {
            return (AttackUser & ATKUSER_RIGHTEOUS) == 0 || base.IsEvil();
        }

        public bool IsFarWeapon()
        {
            return monstertype.AttackRange > SHORTWEAPON_RANGE_LIMIT;
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

        #region Ai

        public override int GetAttackRange(int sizeAdd)
        {
            return monstertype.AttackRange + sizeAdd;
        }

        public bool IsCloseAttack()
        {
            return GetAttackRange(SizeAddition) < SHORTWEAPON_RANGE_LIMIT;
        }

        private bool CheckTarget()
        {
            Role target = RoleManager.GetRole(actTarget);
            if (target == null || !target.IsAlive)
            {
                moveTarget = 0;
                actTarget = 0;
                return false;
            }

            if (target.IsWing && !IsWing && !IsCloseAttack())
                return false;

            int nDistance = ViewRange;
            int nAtkDistance = Calculations.CutOverflow(nDistance, GetAttackRange(target.SizeAddition));
            int nDist = GetDistance(target.X, target.Y);

            if (!(nDist <= nDistance) || nDist <= nAtkDistance && GetAttackRange(target.SizeAddition) > 1)
            {
                actTarget = 0;
                return false;
            }

            return true;
        }

        private bool FindNewTarget()
        {
            if (Map == null)
            {
                return false;
            }

            if (IsLockUser() || IsLockOne())
            {
                if (CheckTarget())
                {
                    if (IsLockOne())
                        return true;
                }
                else
                {
                    if (IsLockUser())
                        return false;
                }
            }

            int distance = ViewRange;
            List<Role> roles = Map.Query9BlocksByPos(X, Y).Where(x => x.Identity != this.Identity).ToList();
            foreach (Role role in roles.Distinct())
            {
                if (role.Identity == Identity)
                    continue;

                if (!role.IsAlive)
                    continue;

                if (role.QueryStatus(StatusSet.INVISIBLE) != null)
                    continue;

                if (role is Character targetUser)
                {
                    bool pkKill = IsPkKiller() && targetUser.IsPker();
                    bool evilKill = IsEvilKiller() && !targetUser.IsVirtuous();
                    bool evilMob = IsEvil() && !(IsPkKiller() || IsEvilKiller());

                    if ((IsGuard() && targetUser.IsCrime())
                        || pkKill
                        || evilKill
                        || evilMob)
                    {
                        if (targetUser.IsWing && !IsWing && !IsCloseAttack())
                            continue;

                        if (!targetUser.IsAttackable(this))
                            continue;

                        int nDist = GetDistance(targetUser.X, targetUser.Y);
                        if (nDist <= distance)
                        {
                            distance = nDist;
                            moveTarget = actTarget = targetUser.Identity;

                            if (pkKill || evilKill)
                                break;
                        }
                    }
                }
                else if (role is Monster monster)
                {
                    if (IsEvil() && monster.IsRighteous()
                        || IsRighteous() && monster.IsEvil())
                    {
                        if (monster.IsWing && !IsWing) continue;

                        int nDist = GetDistance(monster.X, monster.Y);
                        if (nDist < distance)
                        {
                            distance = nDist;
                            moveTarget = actTarget = monster.Identity;
                        }
                    }
                }
            }

            if (actTarget != 0)
            {
                FindPath();
                return moveTarget != 0;
            }

            return false;
        }

        #endregion

        #region AI Movement

        private FacingDirection nextDir = FacingDirection.Invalid;
        private bool aheadPath;

        public bool IsMoveEnable()
        {
            return IsWalkEnable() || IsJumpEnable();
        }

        private bool DetectPath(FacingDirection noDir)
        {
            ClearPath();

            var posTarget = new Point();

            if (actTarget != 0)
            {
                Role role = RoleManager.GetRole(actTarget);

                if (role == null)
                    return false;

                posTarget.X = role.X;
                posTarget.Y = role.Y;
            }
            else if (moveTarget != 0)
            {
                Role role = RoleManager.GetRole(moveTarget);

                if (role == null)
                    return false;

                posTarget.X = role.X;
                posTarget.Y = role.Y;
            }
            else
            {
                posTarget = generator.GetCenter();
            }

            int oldDist = GetDistance(posTarget.X, posTarget.Y);
            int bestDist = oldDist;
            var bestDir = FacingDirection.Invalid;
            var firstDir = FacingDirection.Begin;

            for (var i = FacingDirection.Begin; i < FacingDirection.Invalid; i++)
            {
                FacingDirection dir = firstDir;
                if (dir != noDir)
                {
                    int x = X + GameMapData.WalkXCoords[(int)dir];
                    int y = Y + GameMapData.WalkYCoords[(int)dir];
                    if (Map.IsMoveEnable(x, y, dir, SizeAddition, NPC_CLIMBCAP))
                    {
                        int dist = GetDistance(x, y);
                        if (bestDist - dist * (aheadPath ? 1 : -1) > 0)
                        {
                            bestDist = dist;
                            bestDir = dir;
                        }
                    }
                }
            }

            if (bestDir != FacingDirection.Invalid)
            {
                nextDir = bestDir;
                return true;
            }

            return false;
        }

        private bool FindPath(int x, int y)
        {
            ClearPath();

            if (x == X && y == Y)
                return false;

            var dir = (FacingDirection)Calculations.GetDirection(X, Y, x, y);
            if (!aheadPath)
                dir += 4;
            for (var i = 0; i < 8; i++)
            {
                dir = (FacingDirection)(((int)dir + i) % 8);
                if (TestPath(dir))
                {
                    nextDir = dir;
                    return true;
                }
            }

            return nextDir != FacingDirection.Invalid;
        }

        private bool FindPath(int scapeSteps = 0)
        {
            ClearPath();

            if (moveTarget == 0)
                return false;

            aheadPath = scapeSteps == 0;

            Role role = Map.QueryAroundRole(this, moveTarget);
            if (role == null || !role.IsAlive || GetDistance(role) > ViewRange)
            {
                moveTarget = 0;
                actTarget = 0;
                ClearPath();
                return false;
            }

            if (!FindPath(role.X, role.Y))
            {
                moveTarget = 0;
                actTarget = 0;
                ClearPath();
                return false;
            }

            if (nextDir != FacingDirection.Invalid)
            {
                if (!Map.IsMoveEnable(X, Y, nextDir, SizeAddition))
                {
                    DetectPath(nextDir);
                    return nextDir != FacingDirection.Invalid;
                }
            }

            return nextDir != FacingDirection.Invalid;
        }

        private bool TestPath(FacingDirection dir)
        {
            if (dir == FacingDirection.Invalid)
                return false;

            int x = X + GameMapData.WalkXCoords[(int)dir];
            int y = Y + GameMapData.WalkYCoords[(int)dir];

            if (Map.IsMoveEnable(x, y, dir, SizeAddition, NPC_CLIMBCAP))
            {
                nextDir = dir;
                return true;
            }

            return false;
        }

        public void ClearPath()
        {
            nextDir = FacingDirection.Invalid;
        }

        private bool PathMove(RoleMoveMode mode)
        {
            if (mode == RoleMoveMode.Walk)
            {
                if (!move.ToNextTime(monstertype.MoveSpeed))
                    return true;
            }
            else
            {
                if (!move.ToNextTime((int)monstertype.RunSpeed))
                    return true;
            }

            int newX = X + GameMapData.WalkXCoords[((int)nextDir)%8];
            int newY = Y + GameMapData.WalkYCoords[((int)nextDir)%8];

            bool superPosition = Map.IsSuperPosition(newX, newY);
            if (!superPosition)
            {
                if (TestPath(nextDir))
                {
                    MoveToward((int)nextDir, (int)mode);
                    ClearPath();
                    return true;
                }
            }

            if (DetectPath(nextDir))
            {
                MoveToward((int)nextDir, (int)mode);
                return true;
            }

            if (IsJumpEnable())
            {
                Point pos = generator.GetCenter();
                JumpBlock(pos.X, pos.Y, Direction);
                return true;
            }

            return false;
        }

        public override bool JumpPos(int x, int y)
        {
            ClearPath();
            return base.JumpPos(x, y);
        }

        #endregion

        #region AI Attack

        private uint idWarningMagic = 0;

        public void DoAttack()
        {
            if (actTarget == 0)
            {
                return;
            }

            Role target = RoleManager.GetRole(actTarget);
            if (target == null)
            {
                return;
            }

            if (!UseMagicAttack(target))
            {
                BroadcastMsg(new MsgAiInteract
                {
                    Data = new MsgAiInteractContract
                    {
						Action = AiInteractAction.Attack,
						Identity = Identity,
						TargetIdentity = target.Identity,
						X = target.X,
						Y = target.Y
					}                    
                });
            }
        }

        public bool UseMagicAttack(Role target)
        {
            if (monsterMagics.Count == 0)
            {
                return false;
            }

            if (Name.Contains("Terato"))
            {

            }

            if (idWarningMagic != 0)
            {
                MonsterMagic magic = monsterMagics.FirstOrDefault(x => x.MagicType == idWarningMagic);
                if (magic.IsWarningTimeOut())
                {
                    idWarningMagic = 0;
                    magic.Use();
                    magic.ResetWarningTimeOut();
                    BroadcastMsg(new MsgAiInteract
                    {
                        Data = new MsgAiInteractContract
                        {
                            Action = AiInteractAction.MagicAttack,
                            Identity = Identity,
                            TargetIdentity = target.Identity,
                            X = target.X,
                            Y = target.Y,
                            MagicType = (ushort)magic.MagicType,
                            MagicLevel = (ushort)magic.MagicLev
                        }
                    });
                }
                return false;
            }

            foreach (var magic in monsterMagics
                .OrderBy(x => x.LastTick)
                .ThenBy(x => x.ColdTime))
            {
                if (magic.IsReady() && idWarningMagic == 0)
                {
                    if (magic.WarningTime != 0)
                    {
                        magic.StartWarningTimer();
                        idWarningMagic = magic.MagicType;
                        BroadcastMsg(new MsgAiInteract
                        {
                            Data = new MsgAiInteractContract
                            {
								Action = AiInteractAction.MagicAttackWarning,
								Identity = Identity,
								TargetIdentity = target.Identity,
								X = target.X,
								Y = target.Y,
								MagicType = (ushort)magic.MagicType,
								MagicLevel = (ushort)magic.MagicLev,
								Data = magic.WarningTime
							}                            
                        });
                    }
                    else
                    {
                        magic.Use();
                        BroadcastMsg(new MsgAiInteract
                        {
                            Data = new MsgAiInteractContract
                            {
                                Action = AiInteractAction.MagicAttack,
                                Identity = Identity,
                                TargetIdentity = target.Identity,
                                X = target.X,
                                Y = target.Y,
                                MagicType = (ushort)magic.MagicType,
                                MagicLevel = (ushort)magic.MagicLev
                            }
                        });
                    }
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region AI Process

        private AiStage aiStage;

        private bool IsActive()
        {
            bool result = QueryStatus(StatusSet.FREEZE) == null
                && QueryStatus(StatusSet.DAZED) == null
                && QueryStatus(StatusSet.HUGE_DAZED) == null
                && QueryStatus(StatusSet.ICE_BLOCK) == null
                && QueryStatus(StatusSet.CONFUSED) == null;

			return result;
        }

        private bool ProcessEscape()
        {
            if (!IsEscapeEnable())
            {
                ChangeMode(AiStage.Idle);
                return true;
            }

            Role target = Map.QueryRole<Role>(actTarget);
            if ((IsGuard() || IsPkKiller()) && target != null)
            {
                JumpPos(target.X, target.Y);
                ChangeMode(AiStage.Forward);
                return true;
            }

            if (nextDir == FacingDirection.Invalid)
            {
                FindPath(ViewRange * 2);
            }

            if (actTarget == 0)
            {
                ChangeMode(AiStage.Idle);
                return true;
            }

            if (actTarget != 0 && nextDir == FacingDirection.Invalid)
            {
                ChangeMode(AiStage.Forward);
                return true;
            }

            PathMove(RoleMoveMode.Run);
            return true;
        }

        private async Task<bool> ProcessIdleAsync()
        {
            if (!(IsGuard() || IsPkKiller() || IsEvilKiller()))
            {
                if (nextDir != FacingDirection.Invalid)
                {
                    PathMove(RoleMoveMode.Walk);
                    return true;
                }

                if (!(IsFastBack() && generator.IsInRegion(X, Y)))
                {
                    if (!action.ToNextTime())
                    {
                        return true;
                    }
                }
            }

            Role target;
            if (IsActive() && FindNewTarget())
            {
                target = Map.QueryAroundRole(this, moveTarget);
                if (target == null)
                {
                    return false;
                }

                int distance = GetDistance(target);
                if (distance <= GetAttackRange(target.SizeAddition))
                {
                    ChangeMode(AiStage.Attack);
                    return false;
                }

                if (IsMoveEnable())
                {
                    if (nextDir == FacingDirection.Invalid)
                    {
                        if (!IsEscapeEnable() || await ChanceCalcAsync(80))
                        {
                            return true;
                        }

                        ChangeMode(AiStage.Escape);
                        return false;
                    }

                    ChangeMode(AiStage.Forward);
                    return false;
                }
            }

            if (IsGuard() || IsPkKiller() || IsEvilKiller())
            {
                if (nextDir != FacingDirection.Invalid)
                {
					PathMove(RoleMoveMode.Walk);
                    return true;
                }

                if (!action.ToNextTime())
                {
                    return true;
                }
            }

            if (!IsMoveEnable())
            {
                return true;
            }

            if (generator.IsInRegion(X, Y))
            {
                if (IsGuard() || IsPkKiller() || IsEvilKiller())
                {
                    if (await ChanceCalcAsync(3)
                        && (generator.GetWidth() > 1 || generator.GetHeight() > 1))
                    {
                        int x = await NextAsync(generator.GetWidth() + generator.GetPosX());
                        int y = await NextAsync(generator.GetHeight() + generator.GetPosY());

                        if (FindPath(x, y))
                        {
                            PathMove(RoleMoveMode.Walk);
                        }
                    }
                }
            }
            else
            {
                //if ((IsGuard() || IsPkKiller() || IsFastBack()) || await ChanceCalcAsync(20))
                {
                    Point center = generator.GetCenter();
                    if (FindPath(center.X, center.Y))
                    {
						var a = IsActive();
						var b = FindNewTarget();
						PathMove(RoleMoveMode.Walk);
                    }
                    else if (IsJumpEnable())
                    {
                        JumpBlock(center.X, center.Y, Direction);
                    }
                }
            }

            return true;
        }

        private bool ProcessAttack()
        {
            Role target = Map.QueryAroundRole(this, actTarget);
            if (target != null)
            {
                if (target.IsAlive && GetDistance(target) <= GetAttackRange(target.SizeAddition))
                {
                    if (action.ToNextTime())
                    {
                        if (Map.IsSuperPosition(X, Y))
                        {
                            aheadPath = false;
                            DetectPath(FacingDirection.Invalid);
                            aheadPath = true;
                            if (nextDir != FacingDirection.Invalid)
                            {
                                PathMove(RoleMoveMode.Shift);
                            }
                        }

                        ChangeMode(AiStage.Forward);
                        return true;
                    }
                    return true;
                }
            }

            if (FindNewTarget())
            {
                target = Map.QueryAroundRole(this, moveTarget);
                if (target.IsAlive && GetDistance(target) <= GetAttackRange(target.SizeAddition))
                {
                    if (nextDir != FacingDirection.Invalid && IsMoveEnable())
                    {
                        ChangeMode(AiStage.Forward);
                        return true;
                    }
                    else
                    {
                        ChangeMode(AiStage.Idle);
                        return true;
                    }
                }
                return true;
            }

            ChangeMode(AiStage.Idle);
            return true;
        }

        private bool ProcessForward()
        {
            Role target = Map.QueryAroundRole(this, moveTarget);
            if (target != null && target.IsAlive)
            {
                int distance = GetDistance(target);
                if (distance <= GetAttackRange(target.SizeAddition))
                {
                    if (!IsGuard() && !IsMoveEnable() && !IsFarWeapon() && nextDir != FacingDirection.Invalid)
                    {
                        if (PathMove(RoleMoveMode.Run))
                        {
                            return true;
                        }
                    }

                    ChangeMode(AiStage.Attack);
                    return true;
                }
            }


            if ((IsGuard() || IsPkKiller() || IsFastBack()) && generator.IsTooFar(X, Y, GUARD_LEAVEDISTANCE))
            {
                actTarget = 0;
                moveTarget = 0;
                Point center = generator.GetCenter();
                FarJump(center.X, center.Y, Direction);
                ClearPath();
                ChangeMode(AiStage.Idle);
                return true;
            }

            if ((IsGuard() || IsPkKiller() || IsEvilKiller()) 
                && target != null 
                && GetDistance(target) >= PKKILLER_JUMPDISTANCE)
            {
                JumpPos(target.X, target.Y);
                return true;
            }

            if (nextDir == FacingDirection.Invalid)
            {
                if (FindNewTarget())
                {
                    if (nextDir == FacingDirection.Invalid)
                    {
                        target = Map.QueryAroundRole(this, moveTarget);
                        if (IsJumpEnable())
                        {
                            JumpBlock(target.X, target.Y, Direction);
                            return true;
                        }

                        ChangeMode(AiStage.Idle);
                        return true;
                    }
                    return false;
                }
                ChangeMode(AiStage.Idle);
                return true;
            }
            else
            {
                if (moveTarget != 0)
                {
                    PathMove(RoleMoveMode.Run);
                }
                else
                {
                    PathMove(RoleMoveMode.Walk);
                }
            }

            return true;
        }

        private void ChangeMode(AiStage stage)
        {
            switch (stage)
            {
                case AiStage.Dormancy:
                    {
                        // todo
                        break;
                    }

                case AiStage.Attack:
                    {
                        DoAttack();
                        break;
                    }
            }

            if (stage != AiStage.Forward)
            {
                ClearPath();
            }

            aiStage = stage;
        }

        #endregion

        #region On Timer

        public override async Task OnTimerAsync()
        {
            if (!IsAlive || !IsActive() || Map == null)
            {
                return;
            }

            for (int i = 1; i < 5; i++)
            {
                switch (aiStage)
                {
                    case AiStage.Idle:
                        {
                            if (await ProcessIdleAsync())
                                return;
                            break;
                        }

                    case AiStage.Escape:
                        {
                            if (ProcessEscape())
                                return;
                            break;
                        }

                    case AiStage.Forward:
                        {
                            if (ProcessForward())
                                return;
                            break;
                        }

                    case AiStage.Attack:
                        {
                            if (ProcessAttack())
                                return;
                            break;
                        }
                }
            }
        }

        #endregion

        public const int ATKUSER_LEAVEONLY = 0,        
                         ATKUSER_PASSIVE = 0x01,       
                         ATKUSER_ACTIVE = 0x02,        
                         ATKUSER_RIGHTEOUS = 0x04,     
                         ATKUSER_GUARD = 0x08,         
                         ATKUSER_PPKER = 0x10,         
                         ATKUSER_JUMP = 0x20,          
                         ATKUSER_FIXED = 0x40,         
                         ATKUSER_FASTBACK = 0x0080,    
                         ATKUSER_LOCKUSER = 0x0100,    
                         ATKUSER_LOCKONE = 0x0200,     
                         ATKUSER_ADDLIFE = 0x0400,     
                         ATKUSER_EVIL_KILLER = 0x0800, 
                         ATKUSER_WING = 0x1000,        
                         ATKUSER_NEUTRAL = 0x2000,     
                         ATKUSER_ROAR = 0x4000,        
                         ATKUSER_NOESCAPE = 0x8000,    
                         ATKUSER_EQUALITY = 0x10000;   

        public const int NPC_CLIMBCAP = 26;
        public const int SHORTWEAPON_RANGE_LIMIT = 2;
        public const int NPC_REST_TIME = 7;
        public const int GUARD_LEAVEDISTANCE = 48;     // ÎÀ±ø·ÅÆú×·É±µÄ¾àÀë
        public const int PKKILLER_JUMPDISTANCE = 6;        // ´¥·¢ÎÀ±øÌøµÄ¾àÀë
        public const int RANDOMMOVE_RATE = 5;      // NPCÐÝÏÐÊ±Ëæ»ú×ß¶¯µÄÆµ¶ÈÎª1/20
        public const int GUARD_RANDOMMOVE_RATE = 5;		// ÎÀ±øÐÝÏÐÊ±Ëæ»ú×ß¶¯µÄÆµ¶ÈÎª1/10

        private enum AiStage
        {
            /// <summary>
            ///     Monster wont do nothing, just heal. Activated if on active block but haven't triggered any other action.
            /// </summary>
            Dormancy,

            /// <summary>
            ///     Monster is doing absolutely nothing.
            /// </summary>
            Idle,

            /// <summary>
            ///     When monster is low life and want to run from the attacker.
            /// </summary>
            Escape,

            /// <summary>
            ///     Monster movement.
            /// </summary>
            Forward,

            /// <summary>
            ///     Monster is ready for attack.
            /// </summary>
            Attack,
            Last
        }
    }
}
