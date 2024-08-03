using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Events;
using Long.Kernel.States.Items;
using Long.Kernel.States.Magics;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Mathematics;
using Long.World.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.States.Magics.Magic;

namespace Long.Kernel.States
{
    public sealed class BattleSystem
    {
        private static readonly ILogger logger = Log.ForContext<BattleSystem>();
        private readonly TimeOutMS attackTimer = new();
        private uint idTarget;
        private Point targetLastPosition = default;
        private readonly Role role;

        public BattleSystem(Role role)
        {
            attackTimer.Update();
            this.role = role;
        }

        public void CreateBattle(uint target)
        {
            idTarget = target;
        }

        public bool IsActive()
        {
            return idTarget != 0;
        }

        public bool NextAttack(int ms)
        {
            return attackTimer.ToNextTime(ms);
        }

        public void ResetBattle()
        {
            idTarget = 0;
            targetLastPosition = default;
        }

        public bool IsBattleMaintain()
        {
            if (idTarget == 0 || role.Map == null)
            {
                return false;
            }

            if (!role.IsAlive)
            {
                return false;
            }

            Role target = role.Map.QueryAroundRole(role, idTarget);
            if (target == null)
            {
                return false;
            }

            if (!target.IsAlive)
            {
                if (target is DynamicNpc dynaNpc && !dynaNpc.IsCityGate())
                {
                    return false;
                }
            }

            if (target.MapIdentity != role.MapIdentity)
            {
                return false;
            }

            if (target.IsPlayer() && !targetLastPosition.Equals(default))
            {
                if (targetLastPosition.X != target.X || targetLastPosition.Y != target.Y)
                {
                    return false;
                }
            }

            if (role is Character && target is Character && role.Map?.IsPkDisable() != false)
            {
                return false;
            }

            if (target.IsWing && !role.IsWing && !role.IsBowman)
            {
                return false;
            }

            if (role.QueryStatus(StatusSet.SHURIKEN_VORTEX) != null)
            {
                return false;
            }            			

			if (role.QueryStatus(StatusSet.FATAL_STRIKE) != null)
            {
                if (role.GetDistance(target) > Screen.VIEW_SIZE)
                {
                    return false;
                }
            }
            else
            {
                if (role.GetDistance(target) > role.GetAttackRange(target.SizeAddition))
                {
                    return false;
                }
            }

            if (!target.IsAttackable(role))
            {
                return false;
            }

            if (role.Map.QueryRegion(RegionType.PkProtected, target.X, target.Y) || role.Map.QueryRegion(RegionType.FlagProtection, target.X, target.Y))
            {
                return false;
            }

            if (role is Character atkUser)
            {
                var currentEvent = atkUser.GetCurrentEvent();
                if (currentEvent != null
                    && !currentEvent.IsAttackEnable(atkUser))
                {
                    return false;
                }
                else if (atkUser.IsArenicWitness())
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> ProcessAttackAsync()
        {
			if (!IsBattleMaintain())
            {
                ResetBattle();
                return false;
            }

            await role.MagicData.AbortMagicAsync(true);

            Role target = role.Map.QueryAroundRole(role, idTarget);
            if (target == null)
            {
                ResetBattle();
                return false;
            }

            if (role.IsImmunity(target))
            {
                ResetBattle();
                return false;
            }

            await role.ProcessOnAttackAsync();

            Character user = role as Character;
            if (user != null 
                && user.QueryStatus(StatusSet.FATAL_STRIKE) == null
                && await user.AutoSkillAttackAsync(target))
            {
                return true;
            }

            if (await IsTargetDodgedAsync(role, target))
            {
                await role.SendDamageMsgAsync(target.Identity, 0, InteractionEffect.None);
                return false;
            }

            if (user != null && !user.Map.IsTrainingMap())
            {
                await user.DecEquipmentDurabilityAsync(false, 0, 1);
            }

            if (await target.CheckScapegoatAsync(role))
            {
                return false;
            }

            int adjustAtk = 0;
            if (role.QueryStatus(StatusSet.FATAL_STRIKE) != null)
            {
                adjustAtk = role.QueryStatus(StatusSet.FATAL_STRIKE).Power;
            }

            targetLastPosition = new Point(target.X, target.Y);

            var attackResult = await CalcPowerAsync(MagicType.None, role, target, adjustAtk);

            int damage = attackResult.Damage;
            if (user?.IsLucky == true && await NextAsync(100) < 2)
            {
                await user.SendEffectAsync("LuckyGuy", true);
                damage *= 2;
            }

            if (role.QueryStatus(StatusSet.FATAL_STRIKE) != null)
            {
                if (target is Monster targetMob && !targetMob.IsGuard()
                    && await role.JumpPosAsync(target.X, target.Y))
                {
                    var msg = new MsgAction
                    {
                        Identity = role.Identity,
                        Action = MsgAction.ActionType.NinjaStep,
                        Data = target.Identity,
                        CommandX = target.X,
                        CommandY = target.Y,
                        X = target.X,
                        Y = target.Y,
                        Timestamp = (uint)Environment.TickCount
                    };
                    if (user != null)
                    {
                        await user.SendAsync(msg);
                        await user.Screen.UpdateAsync(msg);
                    }
                    else
                    {
                        await role.BroadcastRoomMsgAsync(msg, false);
                    }
                    ResetBattle();
                }
            }

            var lifeLost = (int)Math.Min(target.MaxLife, Math.Max(1, damage));
            long nExp = Math.Min(Math.Max(0, lifeLost), target.MaxLife);

            await role.ProcessAfterAttackAsync();

            await role.SendDamageMsgAsync(target.Identity, damage, attackResult.Effect);

            Magic gapingWounds = role.MagicData[11230];
            if (gapingWounds != null)
            {
                await role.ProcessMagicAttackAsync(gapingWounds.Type, idTarget, target.X, target.Y);
            }

            if (damage == 0)
            {
                return true;
            }

            await target.BeAttackAsync(MagicType.None, role, damage, true);

            if (user != null)
            {
                await user.CheckCrimeAsync(target);
            }

            var npc = target as DynamicNpc;
            if (npc?.IsAwardScore() == true && user != null)
            {
                user.AddSynWarScore(npc, lifeLost);
            }

			if (user != null && (target is Monster monsterTarget))			
                await monsterTarget.AddBossAttackerScore(user, damage);			

			if (user != null &&
                (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || npc?.IsGoal() == true))
            {
                int nWeaponExp = (int)nExp / 3;
                nExp = user.AdjustExperience(target, nExp, false);
                var nAdditionExp = 0;
                if (!target.IsAlive && npc?.IsGoal() != true)
                {
                    nAdditionExp = (int)(target.MaxLife * 0.05f);
                    nExp += nAdditionExp;

                    if (user.Team != null)
                    {
                        await user.Team.AwardMemberExpAsync(user.Identity, target, nAdditionExp);
                    }
                }

				await user.AwardBattleExpAsync(nExp, true);

                if (!target.IsAlive && nAdditionExp > 0 && !role.Map.IsTrainingMap() && user.Level < Role.MAX_UPLEV)
                {
                    await user.SendAsync(string.Format(StrKillingExperience, nAdditionExp));
                }

                if (user.UserPackage[Item.ItemPosition.RightHand]?.IsBow() == true ||
                    user.UserPackage[Item.ItemPosition.RightHand]?.IsWeaponTwoHand() == true)
                {
                    nWeaponExp *= 2;
                }

                if (user.UserPackage[Item.ItemPosition.RightHand] != null)
                {
                    await user.AddWeaponSkillExpAsync((ushort)user.UserPackage[Item.ItemPosition.RightHand].GetItemSubType(), nWeaponExp);
                }
                if (user.UserPackage[Item.ItemPosition.LeftHand] != null &&
                    !user.UserPackage[Item.ItemPosition.LeftHand].IsArrowSort())
                {
                    await user.AddWeaponSkillExpAsync((ushort)user.UserPackage[Item.ItemPosition.LeftHand].GetItemSubType(), nWeaponExp / 2);
                }

                if (await ChanceCalcAsync(7f))
                {
                    await user.SendGemEffectAsync();
                }
            }

            if (!target.IsAlive)
            {
                uint dieWay = 1;
                if (damage > target.MaxLife / 3)
                {
                    dieWay = 2;
                }

                await role.KillAsync(target, dieWay);
            }
            return true;
        }

		public async Task OtherMemberAwardExpAsync(Role target, long nBonusExp)
        {
            if (role.Map.IsTrainingMap())
            {
                return;
            }

            if (role is Character user && user.Team != null)
            {
                await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
            }
        }

        #region Calculations

        public static async Task<AttackResult> CalcPowerAsync(
            MagicType magic, Role attacker, Role target, int adjustAtk = 0)
        {
#if !DEBUG
            if (target is Character user && user.TransformationMesh == 223)
            {
                return new(1, 0, InteractionEffect.None);
            }
#endif

            if (target.QueryStatus(StatusSet.SHURIKEN_VORTEX) != null)
            {
                return new AttackResult(1, 0, InteractionEffect.None);
            }

            AttackResult result;
            if (magic == MagicType.None)
            {
                result = await CalcAttackPowerAsync(attacker, target, adjustAtk);
            }
            else
            {
                result = await CalcMagicAttackPowerAsync(attacker, target, adjustAtk);
            }

            if (target is Monster monster
                && monster.Defense2 <= 1)
            {
                return new(1, 0, result.Effect);
            }

            int overrideDamage = result.Damage;
            GameEvent @event = EventManager.GetEvent(attacker.MapIdentity);
            if (@event != null)
            {
                overrideDamage = await @event.GetDamageLimitAsync(attacker, target, result.Damage);
                return new(overrideDamage, result.ElementalDamage, result.Effect);
            }

            if (target is DynamicNpc dynamicNpc
                && dynamicNpc.IsSynFlag()
                && dynamicNpc.IsSynMoneyEmpty())
            {
                overrideDamage = result.Damage * Role.SYNWAR_NOMONEY_DAMAGETIMES;
            }

#if HIT_DAMAGE_1
            if (attacker is Character user)
            {
                await user.SendAsync($"DEBUG DAMAGE: {result.Damage},{overrideDamage},{result.Effect} [{attacker.MagicData.QueryMagic?.Name}] [{Environment.TickCount}]", TalkChannel.Talk);
            }
            return new(1, 0, InteractionEffect.None);
#else
            return new(overrideDamage, result.ElementalDamage, result.Effect);
#endif
        }

        public static AttackResult CalcElementalPower(Role attacker, Role target, Magic magic)
        {
            if (magic == null || magic.ElementPower == 0)
            {
                return new();
            }

            int resistance = 0;
            InteractionEffect interactionEffect = InteractionEffect.None;

            switch (magic.ElementType)
            {
                case 1: // metal
                    {
                        resistance = Math.Min(99, target.MetalResistance);
                        interactionEffect |= InteractionEffect.MetalResist;
                        break;
                    }
                case 2: // wood
                    {
                        resistance = Math.Min(99, target.WoodResistance);
                        interactionEffect |= InteractionEffect.WoodResist;
                        break;
                    }
                case 3: // water
                    {
                        resistance = Math.Min(99, target.WaterResistance);
                        interactionEffect |= InteractionEffect.WaterResist;
                        break;
                    }
                case 4: // fire
                    {
                        resistance = Math.Min(99, target.FireResistance);
                        interactionEffect |= InteractionEffect.FireResist;
                        break;
                    }
                case 5: // earth
                    {
                        resistance = Math.Min(99, target.EarthResistance);
                        interactionEffect |= InteractionEffect.EarthResist;
                        break;
                    }
            }

            int damage = (int)Math.Max(1, (int)magic.ElementPower * (1 - (resistance / 100d)));
            return new AttackResult(0, damage, interactionEffect);
        }

        public static async Task<AttackResult> CalcAttackPowerAsync(Role attacker, Role target, int adjustAtk = 0, int adjustDef = 0)
        {
            var effect = InteractionEffect.None;
            var attack = 0;
            var damage = 0;

            if (await ChanceCalcAsync(50))
            {
                attack = attacker.MaxAttack -
                         await NextAsync(1, Math.Max(1, attacker.MaxAttack - attacker.MinAttack) / 2 + 1);
            }
            else
            {
                attack = attacker.MinAttack -
                         await NextAsync(1, Math.Max(1, attacker.MaxAttack - attacker.MinAttack) / 2 + 1);
            }

            Character attackerUser = attacker as Character;
            if (attackerUser != null)
            {
                attack = (int)(attack * (1 + (attackerUser.DragonGemBonus / 100d)));

                IStatus status = attacker.QueryStatus(StatusSet.BUFF_PATTACK + 1);
                if (status != null)
                {
                    attack += status.Power;
                }
            }

            bool rangedDebuff = attackerUser != null && (target is Character || (target is Monster boss && boss.SpeciesType != 0)) && (attackerUser.IsBowman || attackerUser.IsAssassin);
            if (rangedDebuff)
            {
                if (attackerUser.IsBowman)
                {
                    attack = (int)(attack * 0.125d);
                }
            }

            if (adjustAtk > 0 && adjustAtk < Calculations.ADJUST_PERCENT)
            {
                attack = Calculations.CutTrail(0, Calculations.AdjustDataEx(attack, adjustAtk));
            }

            var targetUser = target as Character;
            int defense = 0;
            if (!rangedDebuff)
            {
                defense = target.Defense;

                if (adjustDef > 0)
                {
                    defense = Calculations.CutTrail(0, Calculations.AdjustDataEx(defense, adjustDef));
                }

                if (targetUser != null)
                {
                    if (targetUser.Metempsychosis > 0)
                    {
                        if (targetUser.Level >= 70)
                        {
                            defense = (int)(defense * 1.3d);
                        }
                    }
                }

                if (target.QueryStatus(StatusSet.SHIELD) != null)
                {
                    defense = Calculations.AdjustData(defense, target.QueryStatus(StatusSet.SHIELD).Power);
                }

                if (target.QueryStatus(StatusSet.DEFENSIVE_INSTANCE) != null)
                {
                    defense = Calculations.AdjustData(defense, target.QueryStatus(StatusSet.DEFENSIVE_INSTANCE).Power);
                }
            }

            damage = Math.Max(1, attack - defense);

            if (rangedDebuff)
            {
                if (attackerUser.IsBowman || attackerUser.IsAssassin)
                {
                    int dodgeDelta = Math.Min(110, target.Dodge);
                    damage = (int)(damage * (1 - (dodgeDelta / 120d)));
                }
            }

            if (adjustAtk > Calculations.ADJUST_PERCENT)
            {
                damage = Calculations.CutTrail(0, Calculations.AdjustDataEx(damage, adjustAtk));
            }

            if (attacker.QueryStatus(StatusSet.STIGMA) != null)
            {
                damage = Calculations.AdjustData(damage, attacker.QueryStatus(StatusSet.STIGMA).Power);
            }

            if (attacker.QueryStatus(StatusSet.INTENSIFY) != null)
            {
                damage = Calculations.AdjustData(damage, attacker.QueryStatus(StatusSet.INTENSIFY).Power);
            }

            if (target.IsMonster() && (target as Monster).SpeciesType == 0)
            {
                if (attacker.QueryStatus(StatusSet.FATAL_STRIKE) != null)
                {
                    damage = Calculations.AdjustData(damage, attacker.QueryStatus(StatusSet.FATAL_STRIKE).Power);
                }
                if (attacker.QueryStatus(StatusSet.SUPERMAN) != null)
                {
                    damage = Calculations.AdjustData(damage, attacker.QueryStatus(StatusSet.SUPERMAN).Power);
                }
                if (attacker.QueryStatus(StatusSet.OBLIVION) != null)
                {
                    damage = Calculations.AdjustData(damage, 30200);
                }
            }

            if (targetUser != null)
            {
                damage = (int)(damage * (1 - targetUser.Blessing / 100d));
                damage = (int)(damage * (1 - targetUser.TortoiseGemBonus / 100d));
            }

            if (attacker is Character atkUser && target is Monster tgtMonster)
            {
                if (!tgtMonster.IsEquality())
                {
                    damage = CalcDamageUser2Monster(atkUser, target, damage);
                }

                damage = target.AdjustWeaponDamage(damage, attacker);
                damage = AdjustMinDamageUser2Monster(damage, attacker, target);
            }
            else if (attacker is Monster atkMonster && target is Character tgtUsr)
            {
                if (!atkMonster.IsEquality())
                {
                    damage = CalcDamageMonster2User(attacker, tgtUsr, damage);
                }

                damage = target.AdjustWeaponDamage(damage, attacker);
                damage = AdjustMinDamageMonster2User(damage, attacker, target);
            }
            else
            {
                if (attackerUser != null && targetUser != null)
                {
                    damage = CalcDamageUser2User(attackerUser, targetUser, damage, ref effect);
                }
                damage = target.AdjustWeaponDamage(damage, attacker);
            }

            if (attacker.CriticalStrike > target.Immunity)
            {
                double rate = Math.Min(Math.Max(0, (attacker.CriticalStrike - target.Immunity) / 100d), 100);
                if (await ChanceCalcAsync(rate))
                {
                    damage = (int)(damage * 1.50d);
                    effect |= InteractionEffect.CriticalStrike;
                }
            }

            if (target.Block > 0 && await ChanceCalcAsync(target.Block / 100d))
            {
                damage /= 2;
                effect |= InteractionEffect.Block;
            }

            damage += attacker.AddFinalAttack;
            damage -= target.AddFinalDefense;

            var elementalDamage = CalcElementalPower(attacker, target, attacker.MagicData.QueryMagic);
            effect |= elementalDamage.Effect;

            damage = Math.Max(1, damage);

#if DEBUG
            if (attacker is Character &&  target is Character)
            {
                //if (attackerUser.IsPm())
                {
                    await attackerUser.SendAsync($"Raw damage: {damage}, Attack: {attack}, Target defense: {defense}");
                }
            }
#endif

            return new(damage, elementalDamage.ElementalDamage, effect);
        }

        public static async Task<AttackResult> CalcMagicAttackPowerAsync(Role attacker, Role target, int adjustAtk = 0)
        {
            var targetUser = target as Character;
            if (targetUser?.Team != null && targetUser.QueryStatus(StatusSet.MAGIC_DEFENDER) != null)
            {
                var magicDefender = targetUser.QueryStatus(StatusSet.MAGIC_DEFENDER);
                Character caster = targetUser.Team.Members.FirstOrDefault(x => x.Identity == magicDefender.CasterId);
                if (caster != null)
                {
                    int battlePowerDelta = attacker.BattlePower - caster.BattlePower;
                    if (battlePowerDelta < 20)
                    {
                        return new AttackResult(0, 0, InteractionEffect.None);
                    }
                }
            }

            var effect = InteractionEffect.None;
            int attack = attacker.MagicAttack;

            Character attackerUser = attacker as Character;
            if (attackerUser != null)
            {
                attack = (int)(attack * (1 + (attackerUser.PhoenixGemBonus / 100d)));

                IStatus status = attacker.QueryStatus(StatusSet.BUFF_MATTACK + 1);
                if (status != null)
                {
                    attack += status.Power;
                }
            }

            var magicDefBonusCoef = Math.Max(1, target.MagicDefenseBonus - (attacker.Penetration / 100)) / 200d;
            attack = (int)(attack * (1 - magicDefBonusCoef));

            if (adjustAtk > 0 && adjustAtk < Calculations.ADJUST_PERCENT)
            {
                attack = Calculations.CutTrail(0, Calculations.AdjustDataEx(attack, adjustAtk));
            }

            int defense = target.MagicDefense;// * target.MagicDefenseBonus / 100;

            int damage = attack - defense;

            if (adjustAtk > 0 && adjustAtk > Calculations.ADJUST_PERCENT)
            {
                damage = Calculations.CutTrail(0, Calculations.AdjustDataEx(damage, adjustAtk));
            }

            if (targetUser != null)
            {
                damage = (int)(damage * (1 - targetUser.Blessing / 100d));
                damage = (int)(damage * (1 - targetUser.TortoiseGemBonus / 100d));
            }

            if (attacker is Character atkUsr && target is Monster tgtMonster)
            {
                if (!tgtMonster.IsEquality())
                {
                    damage = CalcDamageUser2Monster(atkUsr, target, damage);
                }

                damage = target.AdjustWeaponDamage(damage, attacker);
                damage = AdjustMinDamageUser2Monster(damage, attacker, target);
            }
            else if (attacker is Monster atkMonster && target is Character tgtUsr)
            {
                if (!atkMonster.IsEquality())
                {
                    damage = CalcDamageMonster2User(attacker, tgtUsr, damage);
                }

                damage = target.AdjustWeaponDamage(damage, attacker);
                damage = AdjustMinDamageMonster2User(damage, attacker, target);
            }
            else
            {
                if (attacker is Character && targetUser != null)
                {
                    damage = CalcDamageUser2User((Character)attacker, targetUser, damage, ref effect);
                }
                damage = target.AdjustWeaponDamage(damage, attacker);                
            }

            if (attacker.SkillCriticalStrike > target.Immunity)
            {
                double rate = Math.Min(Math.Max(0, (attacker.SkillCriticalStrike - target.Immunity) / 100d), 100);
                if (await ChanceCalcAsync(rate))
                {
                    damage *= 2;
                    effect |= InteractionEffect.CriticalStrike;
                }
            }

            damage += attacker.AddFinalMAttack;
            damage -= target.AddFinalMDefense;

            var elementalDamage = CalcElementalPower(attacker, target, attacker.MagicData.QueryMagic);
            effect |= elementalDamage.Effect;

            damage = Math.Max(1, damage);
            return new(damage, elementalDamage.ElementalDamage, effect);
        }

        public static async Task<bool> IsTargetDodgedAsync(Role attacker, Role target)
        {
            const int MIN_HITRATE = 1;
            const int MAX_HITRATE = 100;

            if (attacker == null || target == null || attacker.Identity == target.Identity)
            {
                return true;
            }

            if (attacker.QueryStatus(StatusSet.FATAL_STRIKE) != null &&
                target is Monster tgtMob && !tgtMob.IsGuard())
            {
                return false;
            }

            int hitRate = attacker.Accuracy;
            if (attacker is Character user)
            {
                hitRate += user.Speed / 5;
            }
            else
            {
                user = null;
            }

            if (attacker.QueryStatus(StatusSet.STAR_OF_ACCURACY) != null)
            {
                hitRate = Calculations.AdjustData(hitRate, attacker.QueryStatus(StatusSet.STAR_OF_ACCURACY).Power);
            }

            int dodge = target.Dodge;
            if (target.QueryStatus(StatusSet.DODGE) != null)
            {
                dodge = Calculations.AdjustData(dodge, target.QueryStatus(StatusSet.DODGE).Power);
            }

            int dodgeAdd = 0;
            if (target.IsPlayer() && attacker.MagicData.QueryMagic?.Sort != MagicSort.Tripleattack)
            {
                dodgeAdd = 40;
            }
            else if (target.IsMonster())
            {
                hitRate += 40;
            }
#if DEBUG && DEBUG_HITRATEa
            int rawHitRate = hitRate;
#endif

            int minHitRate = MIN_HITRATE;
            if (attacker.IsBowman && target.IsShieldUser)
            {
                dodge *= 2;
            }
            else if (attacker.MagicData.QueryMagic?.Sort == MagicSort.Tripleattack)
            {
                minHitRate = 40;
            }

            hitRate = Math.Min(MAX_HITRATE, Math.Max(minHitRate, 100 + hitRate - dodgeAdd - dodge));

#if DEBUG && DEBUG_HITRATE
            if (attacker is Character atkUser && atkUser.IsPm())
                await atkUser.SendAsync($"Attacker({attacker.Name}), Target({target.Name}), Hit Rate: {hitRate}%, User hitrate: {rawHitRate}, Target Dodge: {dodge}", TalkChannel.Talk);

            if (target is Character targetUser && targetUser.IsPm())
                await targetUser.SendAsync($"Attacker({attacker.Name}), Target({target.Name}), Hit Rate: {hitRate}%, Target Dodge: {dodge}");
#endif

            return !await ChanceCalcAsync(hitRate);
        }

        public static int AdjustDrop(int nDrop, int nAtkLev, int nDefLev)
        {
            if (nAtkLev > 120)
            {
                nAtkLev = 120;
            }

            if (nAtkLev - nDefLev > 0)
            {
                int nDeltaLev = nAtkLev - nDefLev;
                if (1 < nAtkLev && nAtkLev <= 19)
                {
                    if (nDeltaLev < 3)
                    {
                    }
                    else if (nDeltaLev < 6)
                    {
                        nDrop /= 5;
                    }
                    else
                    {
                        nDrop /= 10;
                    }
                }
                else if (19 < nAtkLev && nAtkLev <= 49)
                {
                    if (nDeltaLev < 5)
                    {
                    }
                    else if (nDeltaLev < 10)
                    {
                        nDrop /= 5;
                    }
                    else
                    {
                        nDrop /= 10;
                    }
                }
                else if (49 < nAtkLev && nAtkLev <= 85)
                {
                    if (nDeltaLev < 4)
                    {
                    }
                    else if (nDeltaLev < 8)
                    {
                        nDrop /= 5;
                    }
                    else
                    {
                        nDrop /= 10;
                    }
                }
                else if (85 < nAtkLev && nAtkLev <= 112)
                {
                    if (nDeltaLev < 3)
                    {
                    }
                    else if (nDeltaLev < 6)
                    {
                        nDrop /= 5;
                    }
                    else
                    {
                        nDrop /= 10;
                    }
                }
                else if (112 < nAtkLev)
                {
                    if (nDeltaLev < 2)
                    {
                    }
                    else if (nDeltaLev < 4)
                    {
                        nDrop /= 5;
                    }
                    else
                    {
                        nDrop /= 10;
                    }
                }
            }

            return Calculations.CutTrail(0, nDrop);
        }

        public static int GetNameType(int nAtkLev, int nDefLev)
        {
            int nDeltaLev = nAtkLev - nDefLev;

            if (nDeltaLev >= 3)
            {
                return NAME_GREEN;
            }

            if (nDeltaLev >= 0)
            {
                return NAME_WHITE;
            }

            if (nDeltaLev >= -5)
            {
                return NAME_RED;
            }

            return NAME_BLACK;
        }

        public static int CalcDamageUser2Monster(Character attacker, Role target,
                                   int damage) //(int nAtk, int nDef, int nAtkLev, int nDefLev)
        {
            if (GetNameType(attacker.Level, target.Level) == NAME_GREEN)
            {
                int nDeltaLev = attacker.Level - target.Level;
                if (nDeltaLev >= 3
                    && nDeltaLev <= 5)
                {
                    damage = (int)(damage * 1.5);
                }
                else if (nDeltaLev > 5
                         && nDeltaLev <= 10)
                {
                    damage *= 2;
                }
                else if (nDeltaLev > 10
                         && nDeltaLev <= 20)
                {
                    damage = (int)(damage * 2.5);
                }
                else if (nDeltaLev > 20)
                {
                    damage *= 3;
                }
            }

            DbDisdain disdain = BattleSystemManager.GetDisdain(attacker.BattlePower - target.BattlePower);
            int factor = disdain.MaxAtk;

            if (attacker.IsOnXpSkill())
            {
                factor = disdain.MaxXpAtk;
            }

            var maxDamage = (long)((int)target.MaxLife * (factor / 100d));
            damage = (int)Math.Min(damage, maxDamage);

            int extraDelta = target.BattlePower - attacker.BattlePower;
            if (extraDelta > 0)
            {
                if (extraDelta >= 10)
                {
                    factor = 1;
                }
                else if (extraDelta >= 5)
                {
                    factor = 5;
                }
                else
                {
                    factor = 10;
                }

                damage = Calculations.MulDiv(damage, factor, 100);
            }

            return damage;
        }

        public static int CalcDamageMonster2User(Role attacker, Character target, int damage)
        {
            int extraDelta = target.BattlePower - attacker.BattlePower;
            int factor;
            if (extraDelta < 5)
            {
                factor = 100;
            }
            else if (extraDelta < 10)
            {
                factor = 80;
            }
            else if (extraDelta < 15)
            {
                factor = 60;
            }
            else if (extraDelta < 20)
            {
                factor = 40;
            }
            else
            {
                factor = 30;
            }

            int adjustDamage = Calculations.MulDiv((int)target.MaxLife, factor * attacker.ExtraDamage, 1000000);
            return Math.Max(adjustDamage, damage);
        }

        public static int CalcDamageUser2User(Character attacker, Character target, int damage, ref InteractionEffect effect)
        {
            int targetBattlePower = target.BattlePower;
            if (attacker.BattlePower < target.BattlePower)
            {
                if (attacker.Breakthrough > target.Counteraction)
                {
                    double rate = Math.Min(100, Math.Max(0, attacker.Breakthrough / 10d - target.Counteraction / 10d));
                    if (ChanceCalc(rate))
                    {
                        effect |= InteractionEffect.Breakthrough;
                        targetBattlePower = attacker.BattlePower;
                    }
                }
            }

            DbDisdain disdain = BattleSystemManager.GetDisdain(attacker.BattlePower - targetBattlePower);
            int min, max, overAdjust;
            if (attacker.Level < 110)
            {
                if (target.Level < 110)
                {
                    min = disdain.UsrAtkUsrMin;
                    max = disdain.UsrAtkUsrMax;
                    overAdjust = disdain.UsrAtkUsrOveradj;
                }
                else
                {
                    min = disdain.UsrAtkUsrxMin;
                    max = disdain.UsrAtkUsrxMax;
                    overAdjust = disdain.UsrAtkUsrxOveradj;
                }
            }
            else
            {
                if (target.Level < 110)
                {
                    min = disdain.UsrxAtkUsrMin;
                    max = disdain.UsrxAtkUsrMax;
                    overAdjust = disdain.UsrxAtkUsrOveradj;
                }
                else
                {
                    min = disdain.UsrxAtkUsrxMin;
                    max = disdain.UsrxAtkUsrxMax;
                    overAdjust = disdain.UsrxAtkUsrxOveradj;
                }
            }

            int factor = UserAttackUserGetFactor(target);
            int targetLev = target.Level;

            int minDamage = min * targetLev * factor / 100;
            if (damage < minDamage)
            {
                return minDamage;
            }

            int maxDamage = max * targetLev * factor / 100;
            if (damage > maxDamage)
            {
                int nDamage = Calculations.MulDiv(damage - maxDamage, overAdjust, 100);
                return nDamage + maxDamage;
            }

            return damage;
        }

        private static readonly int[] _nonRebornInts = { 10, 6, 6, 6, 6 };
        private static readonly int[] _rebornInts = { 18, 18, 18, 27, 18 };

        public static int UserAttackUserGetFactor(Character target)
        {
            int index;
            if (target.ProfessionSort == 1)
            {
                index = 0;
            }
            else if (target.ProfessionSort == 2)
            {
                index = 1;
            }
            else if (target.ProfessionSort == 4)
            {
                index = 2;
            }
            else if (target.ProfessionSort == 10 || target.ProfessionSort == 14)
            {
                index = 3;
            }
            else if (target.ProfessionSort == 13)
            {
                index = 4;
            }
            else
            {
                index = 0;
            }

            if (target.Metempsychosis > 0)
            {
                return _rebornInts[index];
            }

            return _nonRebornInts[index];
        }

        public static int AdjustMinDamageUser2Monster(int nDamage, Role pAtker, Role pTarget)
        {
            var nMinDamage = 1;
            nMinDamage += pAtker.Level / 10;

            if (pAtker is not Character)
            {
                return Calculations.CutTrail(nMinDamage, nDamage);
            }

            var pUser = (Character)pAtker;
            Item pItem = pUser.UserPackage[Item.ItemPosition.RightHand];
            if (pItem != null)
            {
                nMinDamage += pItem.GetQuality();
            }

            return Calculations.CutTrail(nMinDamage, nDamage);
        }

        public static int AdjustMinDamageMonster2User(int nDamage, Role pAtker, Role pTarget)
        {
            var nMinDamage = 1;
            if (nDamage >= nMinDamage
                || pTarget.Level <= 15)
            {
                return nDamage;
            }

            if (pTarget is not Character pUser)
            {
                return Calculations.CutTrail(nMinDamage, nDamage);
            }

            for (var pos = Item.ItemPosition.EquipmentBegin; pos <= Item.ItemPosition.EquipmentEnd; pos++)
            {
                Item item = pUser.UserPackage[pos];
                if (item == null)
                {
                    continue;
                }

                switch (item.Position)
                {
                    case Item.ItemPosition.Necklace:
                    case Item.ItemPosition.Headwear:
                    case Item.ItemPosition.Armor:
                        nMinDamage -= item.GetQuality() / 4;
                        break;
                }
            }

            nMinDamage = Calculations.CutTrail(1, nMinDamage);
            return Calculations.CutTrail(nMinDamage, nDamage);
        }

        public static long AdjustExp(long nDamage, int nAtkLev, int nDefLev)
        {
            if (nAtkLev > 120)
            {
                nAtkLev = 120;
            }

            long nExp = nDamage;

            int nNameType = NAME_WHITE;
            int nDeltaLev = nAtkLev - nDefLev;

            if (nDeltaLev >= 3)
            {
                nNameType = NAME_GREEN;
            }
            else if (nDeltaLev >= 0)
            {
                nNameType = NAME_WHITE;
            }
            else if (nDeltaLev >= -5)
            {
                nNameType = NAME_RED;
            }
            else
            {
                nNameType = NAME_BLACK;
            }

            if (nNameType == NAME_GREEN)
            {
                if (nDeltaLev >= 3 && nDeltaLev <= 5)
                {
                    nExp = nExp * 70 / 100;
                }
                else if (nDeltaLev > 5
                         && nDeltaLev <= 10)
                {
                    nExp = nExp * 20 / 100;
                }
                else if (nDeltaLev > 10
                         && nDeltaLev <= 20)
                {
                    nExp = nExp * 10 / 100;
                }
                else if (nDeltaLev > 20)
                {
                    nExp = nExp * 5 / 100;
                }
            }
            else if (nNameType == NAME_RED)
            {
                nExp = (int)(nExp * 1.3f);
            }
            else if (nNameType == NAME_BLACK)
            {
                if (nDeltaLev >= -10
                    && nDeltaLev < -5)
                {
                    nExp = (int)(nExp * 1.5f);
                }
                else if (nDeltaLev >= -20
                         && nDeltaLev < -10)
                {
                    nExp = (int)(nExp * 1.8f);
                }
                else if (nDeltaLev < -20)
                {
                    nExp = (int)(nExp * 2.3f);
                }
            }

            return Calculations.CutTrail(0, nExp);
        }

        #endregion

        public const int NAME_GREEN = 0,
                         NAME_WHITE = 1,
                         NAME_RED = 2,
                         NAME_BLACK = 3;

        public struct AttackResult
        {
            public AttackResult(int damage, int elementalDamage, InteractionEffect effect) : this()
            {
                Damage = damage;
                ElementalDamage = elementalDamage;
                Effect = effect;
            }

            public int Damage { get; set; }
            public int ElementalDamage { get; set; }
            public InteractionEffect Effect { get; set; }
        }
    }
}
