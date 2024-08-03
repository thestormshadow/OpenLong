using Long.Database.Entities;
using Long.Game.Network.Ai.Packets;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.Network.Ai;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Network.Packets.Ai;
using Long.Shared.Managers;
using Long.Shared.Mathematics;
using Long.World.Enums;
using Long.World.Map;
using Org.BouncyCastle.Asn1.X509;
using System.Drawing;
using System.Security.Principal;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgInteract;
using static Long.Kernel.Network.Game.Packets.MsgWalk;
using static Long.Kernel.States.Magics.Magic;
using static System.Net.Mime.MediaTypeNames;

namespace Long.Kernel.States.Magics
{
	public partial class MagicData
	{
		private ushort useMagicType;

		private Point targetPos;
		private uint idTarget;

		private bool autoAttack;
		private int autoAttackCount;

		private readonly TimeOutMS delayTimer = new(MAGIC_DELAY);
		private readonly TimeOutMS intoneTimer = new();
		private readonly TimeOutMS magicDelayTimer = new(MAGIC_DELAY);

		public async Task<(bool Success, ushort X, ushort Y)> CheckConditionAsync(Magic magic, uint idTarget, ushort x,
			ushort y)
		{
			int delay = this.role.Map.IsTrainingMap() ? MAGIC_DELAY : Math.Max(MAGIC_DELAY_MIN, MAGIC_DELAY - magic.Level * MAGIC_DECDELAY_PER_LEVEL);

			/* 
             * Felipe - 2023-05-25
             * Add fixed 1000ms delay for triple attack to avoid WWK + Triple so quick even more if player may be using Autoclick
             */
			if (magic.Sort == MagicSort.Tripleattack)
			{
				delay = MAGIC_DELAY;
			}

#if DEBUG_MAGIC_DELAY
            if (this.role.IsPlayer())
            {
                await this.role.SendAsync(
                    $"DELAY(NOW: {Environment.TickCount}) >> [{magic.Type}] {magic.Name} -> MIN({delay} ms) SKILL({magic.DelayMs} ms) ISREADY({magic.IsReady()}) MAGIC_DELAY_PASS({magicDelayTimer.IsTimeOut(delay)})",
                    TalkChannel.Talk);
            }
#endif
			if (!magicDelayTimer.IsTimeOut(delay) &&
				magic.Sort != MagicSort.Collide &&
				magic.Sort != MagicSort.Declife &&
				magic.Sort != MagicSort.BreathFocus)
			{
				return (false, x, y);
			}

			if (!magic.IsReady())
			{
				return (false, x, y);
			}

			if (!(magic.AutoActive.HasFlag(AutoActive.Kill)
				  || magic.AutoActive.HasFlag(AutoActive.OnAttack)) && magic.Type != 6001)
			{
				if (!await ChanceCalcAsync(magic.Percent))
				{
					return (false, x, y);
				}
			}

			if (magic.AutoActive.HasFlag(AutoActive.OnAttack)
				&& magic.Status > 0 && magic.Type != 11590)
			{
				if (this.role.QueryStatus(magic.Status) == null)
				{
					return (false, x, y);
				}
			}

			GameMap map = this.role.Map;
			if (map.IsRaceTrack())
			{
				return (false, x, y);
			}

			var user = this.role as Character;
			Role role = map.QueryAroundRole(this.role, idTarget);
			if (user != null && (user.Map.QueryRegion(RegionType.PkProtected, user.X, user.Y)
				|| user.Map.QueryRegion(RegionType.FlagProtection, user.X, user.Y)) && magic.Multi == 0)
			{
				if (magic.Ground > 0)
				{
					if (magic.Crime > 0)
					{
						return (false, x, y);
					}
				}
				else
				{
					if (role is Character && this.role.Identity != role.Identity && magic.Crime > 0)
					{
						return (false, x, y);
					}
				}
			}

			//PENDIENTE FIX EVENTS
			var currentEvent = user?.GetCurrentEvent();
			if (currentEvent != null && !currentEvent.IsAttackEnable(user, magic))
			{
				if (currentEvent is IQualifier arenaQualifier && arenaQualifier.IsInsideMatch(user.Identity))
				{
					if (idTarget != this.role.Identity)
					{
						return (false, x, y);
					}
				}
				//else if (currentEvent is TeamArenaQualifier teamQualifier && teamQualifier.IsInsideMatch(user.Identity))
				//{
				//	if (user?.Team.IsMember(idTarget) == false)
				//	{
				//		return (false, x, y);
				//	}
				//}
				//else if (magic.Sort != MagicSort.Attachstatus && idTarget != this.role.Identity)
				//{
				//	return (false, x, y);
				//}
			}
			else if (user != null)
			{
				if (user.IsArenicWitness())
				{
					return (false, x, y);
				}
			}

			if (currentEvent != null && !currentEvent.IsAttackEnable(user, magic))
			{
				return (false, x, y);
			}

			if (!map.IsTrainingMap() && user != null)
			{
				if (user.Mana < magic.UseMana)
				{
					return (false, x, y);
				}

				if (user.Energy < magic.UseStamina && this.role.QueryStatus(StatusSet.SUPER_CYCLONE) == null)
				{
					return (false, x, y);
				}

				if (magic.UseItem > 0 && user.CheckWeaponSubType(magic.UseItem, magic.UseItemNum))
				{
					return (false, x, y);
				}
			}

			if (magic.UseXp == MagicType.Normal)
			{
				IStatus pStatus = this.role.QueryStatus(StatusSet.START_XP);
				int magicStatus = magic.Status;
				if (pStatus == null
					&& magicStatus != StatusSet.SHURIKEN_VORTEX
					&& magicStatus != StatusSet.CHAIN_BOLT_ACTIVE
					&& magicStatus != StatusSet.BLADE_FLURRY
					&& magicStatus != StatusSet.BLACK_BEARDS_RAGE
					&& magicStatus != StatusSet.CANNON_BARRAGE)
				{
					return (false, x, y);
				}
			}

			if (magic.WeaponSubtype > 0 && user != null)
			{
				if (!user.CheckWeaponSubType(magic.WeaponSubtype))
				{
					return (false, x, y);
				}

				Item leftHand = user.UserPackage[Item.ItemPosition.LeftHand];
				if ((magic.Type == TWOFOLDBLADES_ID || magic.Type == SUPERTWOFOLDBLADES_ID)
					&& user.UserPackage[Item.ItemPosition.RightHand].GetItemSubType() != leftHand?.GetItemSubType())
				{
					return (false, x, y);
				}
			}

			if (user != null && user.TransformationMesh != 0)
			{
				return (false, x, y);
			}

			if (magic.Multi == 0 && this.role.IsWing && magic.Sort == MagicSort.Transform)
			{
				return (false, x, y);
			}

			if (map.IsWingDisable() && magic.Sort == MagicSort.Attachstatus && magic.Status == StatusSet.FLY)
			{
				return (false, x, y);
			}

			if (magic.Multi == 0 && magic.Ground == 0 && magic.Sort != MagicSort.Groundsting
								  && magic.Sort != MagicSort.Vortex
								  && magic.Sort != MagicSort.Dashwhirl
								  && magic.Sort != MagicSort.Dashdeadmark
								  && magic.Sort != MagicSort.Mountwhirl)
			{
				role = map.QueryAroundRole(this.role, idTarget);
				if (role == null)
				{
					return (false, x, y);
				}

				if (!role.IsAlive
					&& magic.Sort != MagicSort.Attachstatus
					&& magic.Sort != MagicSort.Detachstatus
					&& magic.Sort != MagicSort.Detachbadstatus)
				{
					return (false, x, y);
				}

				if (user != null && role is Character && user.Identity != role.Identity)
				{
					if (magic.Crime != 0 && role.Map.IsPkDisable())
					{
						return (false, x, y);
					}
				}

				x = role.X;
				y = role.Y;
			}

			if (HitByMagic(magic) != 0 && !HitByWeapon() && this.role.GetDistance(x, y) > magic.Distance + 1)
			{
				return (false, x, y);
			}

			if (magic.Multi == 0 && this.role.GetDistance(x, y) > this.role.GetAttackRange(0) + magic.Distance + 1)
			{
				return (false, x, y);
			}

			if (role is DynamicNpc dyna)
			{
				if (dyna.IsGoal() && this.role.Level < dyna.Level)
				{
					return (false, x, y);
				}
			}

			if (magic.Sort == MagicSort.Attachstatus && role is Character targetUser)
			{
				bool isBadStatus = Role.IsBadlyStatus(magic.Status);
				if (isBadStatus && this.role.IsImmunity(targetUser))
				{
					return (false, x, y);
				}
			}

			return (true, x, y);
		}

		public async Task<bool> ProcessMagicAttackAsync(ushort usMagicType, uint idTarget, ushort x, ushort y,
														AutoActive ucAutoActive = 0)
		{
			switch (State)
			{
				case MagicState.Intone:
					await AbortMagicAsync(true);
					break;
			}

			State = MagicState.None;
			useMagicType = usMagicType;

			if (!(Magics.TryGetValue(usMagicType, out Magic magic)
				|| (ucAutoActive == 0 || (magic?.AutoActive ?? 0 & ucAutoActive) != 0)))
			{
				logger.Warning($"invalid magic type: {usMagicType}, user[{role.Name}][{role.Identity}]");
				return false;
			}

			if (magic.Name == "TripleAttack")
			{

			}

			if (magic == null)
			{
				return false;
			}

			(bool Success, ushort X, ushort Y) result = await CheckConditionAsync(magic, idTarget, x, y);
			if (!result.Success)
			{
				if (magic.Sort == MagicSort.Collide)
				{
					await ProcessCollideFailAsync(x, y, (int)idTarget);
				}

				await AbortMagicAsync(true);
				return false;
			}

			/*if (magic.Ground > 0 && magic.Sort != MagicSort.Atkstatus)
                mIdTarget = 0;
            else*/
			this.idTarget = idTarget;

			targetPos = new Point(x, y);

			var user = this.role as Character;
			GameMap map = this.role.Map;
			if (user != null && !map.IsTrainingMap() && map.Identity != TC_PK_ARENA_ID)
			{
				if (magic.UseMana > 0)
				{
					await user.AddAttributesAsync(ClientUpdateType.Mana, magic.UseMana * -1);
				}

				if (magic.UseStamina > 0 && this.role.QueryStatus(StatusSet.SUPER_CYCLONE) == null)
				{
					int useStamina = (int)magic.UseStamina;
					var scurvyBomb = role.QueryStatus(StatusSet.SCURVY_BOMB);
					if (scurvyBomb != null)
					{
						useStamina += scurvyBomb.Power;
					}
					await user.AddAttributesAsync(ClientUpdateType.Stamina, useStamina * -1);
				}

				if (magic.WeaponSubtype != 0 && magic.UseItemNum > 0)
				{
					await user.SpendEquipItemAsync(magic.WeaponSubtype, magic.UseItemNum, true);
				}

				if (magic.UseItemNum > 0 && user.UserPackage[Item.ItemPosition.RightHand]?.IsBow() == true)
				{
					await user.SpendEquipItemAsync(50, magic.UseItemNum, true);
				}
			}

			if (magic.UseXp == MagicType.Normal && user != null)
			{
				IStatus status = role.QueryStatus(StatusSet.START_XP);
				if (status == null)
				{
					if (magic.Target == 2 && idTarget != 0 && idTarget != role.Identity)
					{
						await AbortMagicAsync(true);
						return false;
					}
				}

				if (magic.Status > 0
					&& magic.Sort != MagicSort.Attachstatus
					&& magic.Sort != MagicSort.Oblivion)
				{
					int magicStatus = magic.Status;
					if (role.QueryStatus(magicStatus) == null)
					{
						if (magic.Type == 6010)
						{
							await user.AttachStatusAsync(magicStatus, magic.Power, (int)magic.StepSeconds, (int)magic.ActiveTimes, magic);
						}
						else
						{
							await user.AttachStatusAsync(magicStatus, magic.Power, (int)magic.StepSeconds, 0, magic);
						}
						await user.DetachStatusAsync(StatusSet.START_XP);
						await user.ClsXpValAsync();
						return true;
					}
				}

				await user.DetachStatusAsync(StatusSet.START_XP);
				await user.ClsXpValAsync();
			}

			if (!IsWeaponMagic(magic.Type))
			{
				await role.BroadcastRoomMsgAsync(new MsgInteract
				{
					Action = MsgInteractType.MagicAttack,
					TargetIdentity = idTarget,
					SenderIdentity = role.Identity,
					PosX = role.X,
					PosY = role.Y,
					MagicType = magic.Type,
					MagicLevel = magic.Level
				}, true);
			}

			useMagicType = magic.Type; // for auto attack!
			Role target = role.Map.QueryAroundRole(role, idTarget);
			if (target != null && magic.NextMagic == magic.Type)
			{
				autoAttack = true;

				if (target is Character)
				{
					autoAttack = false;
					autoAttackCount = 0;
				}
			}

			if (magic.UseMana != 0)
			{
				if (!map.IsTrainingMap() && user != null)
				{
					await user.DecEquipmentDurabilityAsync(false, (int)HitByMagic(magic), (ushort)magic.UseItemNum);
				}

				if (await ChanceCalcAsync(7) && user != null)
				{
					await user.SendGemEffectAsync();
				}
			}

			magicDelayTimer.Update();

			if (magic.IntoneSpeed <= 0)
			{
				if (!await LaunchAsync(magic, ucAutoActive)) // pode ocorrer caso o monstro desapareça, morra antes da hora
				{
					ResetDelay();
				}
				else
				{
					if (role.Map?.IsTrainingMap() == true || IsAutoAttack())
					{
						SetAutoAttack(magic.Type);
						delayTimer.Startup(Math.Max(MAGIC_DELAY, magic.DelayMs));
						State = MagicState.Delay;
						return true;
					}

					State = MagicState.None;
				}
			}
			else
			{
				State = MagicState.Intone;
				intoneTimer.Startup((int)magic.IntoneSpeed);
			}

			return true;
		}

		private async Task<bool> LaunchAsync(Magic magic, AutoActive autoActive)
		{
			var result = false;
			try
			{
				if (magic == null)
				{
					return false;
				}

				if (!role.IsAlive)
				{
					return false;
				}

				magic.Use();

				switch (magic.Sort)
				{
					//pendiente ArrowHail
					case MagicSort.Attack:
						result = await ProcessAttackAsync(magic);
						break;
					case MagicSort.Recruit:
						result = await ProcessRecruitAsync(magic);
						break;
					case MagicSort.Fan:
					case MagicSort.Blisteringwave:
						result = await ProcessFanAsync(magic);
						break;
					case MagicSort.Bomb:
					case MagicSort.Assassinvortex:
						result = await ProcessBombAsync(magic);
						break;
					case MagicSort.Attachstatus:
						result = await ProcessAttachAsync(magic);
						break;
					case MagicSort.Detachstatus:
						result = await ProcessDetachAsync(magic);
						break;
					case MagicSort.Dispatchxp:
						result = await ProcessDispatchXpAsync(magic);
						break;
					case MagicSort.Line:
						result = await ProcessLineAsync(magic);
						break;
					case MagicSort.Atkstatus:
						result = await ProcessAttackStatusAsync(magic);
						break;
					case MagicSort.Transform:
						result = await ProcessTransformAsync(magic);
						break;
					case MagicSort.Addmana:
						result = await ProcessAddManaAsync(magic);
						break;
					case MagicSort.Laytrap:
						result = await ProcessLayTrapAsync(magic);
						break;
					case MagicSort.Callpet:
						result = await ProcessCallPetAsync(magic);
						break;
					case MagicSort.Declife:
						result = await ProcessDecLifeAsync(magic);
						break;
					case MagicSort.Groundsting:
						result = await ProcessGroundStingAsync(magic);
						break;
					case MagicSort.Vortex:
						result = await ProcessVortexAsync(magic);
						break;
					case MagicSort.Activateswitch:
						result = await ProcessActivateSwitchAsync(magic);
						break;
					case MagicSort.Spook:
						result = await ProcessSpookAsync(magic);
						break;
					case MagicSort.Warcry:
						result = await ProcessWarCryAsync(magic);
						break;
					case MagicSort.Riding:
						result = await ProcessRidingAsync(magic);
						break;
					case MagicSort.AttachstatusArea:
						result = await ProcessAttachStatusAreaAsync(magic);
						break;
					case MagicSort.FanStatus:
						result = await ProcessFanStatusAsync(magic);
						break;
					case MagicSort.BombStatus:
						result = await ProcessBombStatusAsync(magic);
						break;
					case MagicSort.ChainXp:
					case MagicSort.BlackbeardsRage:
						result = await ProcessChainXpAsync(magic);
						break;
					case MagicSort.Knockback:
						result = await ProcessKnockbackAsync(magic);
						break;
					case MagicSort.Selfdetach:
						result = await ProcessSerenityAsync(magic);
						break;
					case MagicSort.Detachbadstatus:
						result = await ProcessTranquilityAsync(magic);
						break;
					case MagicSort.CloseLine:
						result = await ProcessCloseLineAsync(magic);
						break;
					case MagicSort.Compassion:
						result = await ProcessCompassionAsync(magic);
						break;
					case MagicSort.Teamflag:
						result = await ProcessTeamFlagAsync(magic);
						break;
					case MagicSort.Increaseblock:
						result = await ProcessIncreaseBlockAsync(magic);
						break;
					case MagicSort.Oblivion:
						result = await ProcessOblivionAsync(magic);
						break;
					case MagicSort.Stunbomb:
						result = await ProcessStunBombAsync(magic);
						break;
					case MagicSort.Tripleattack:
						result = await ProcessTripleAttackAsync(magic);
						break;
					case MagicSort.ScurvyBomb:
						result = await ProcessScurvyBombAsync(magic);
						break;
					case MagicSort.CannonBarrage:
						result = await ProcessCannonBarrageAsync(magic);
						break;
					case MagicSort.AdrenalineRush:
						result = await ProcessAdrenalineRushAsync(magic);
						break;
					case MagicSort.GaleBomb:
						result = await ProcessGaleBombAsync(magic);
						break;
					case MagicSort.Dashwhirl:
					case MagicSort.Dashdeadmark:
					case MagicSort.Mountwhirl:
						result = await ProcessDashThickLineAsync(magic);
						break;

					case MagicSort.KineticSpark:
						if (autoActive == AutoActive.OnBeAttack)
						{
							result = true; // ???? Stupid TQ
						}
						else if (autoActive == AutoActive.OnAttack)
						{
							result = await ProcessKinecticSparkAsync(magic);
						}
						else
						{
							result = await ProcessKinecticSparkActivationAsync(magic);
						}
						break;

					case MagicSort.BreathFocus:
						{
							if (autoActive != AutoActive.OnAttack && role is Character user)
							{
								await user.DoCheaterPunishAsync();
								return false;
							}
							result = await ProcessBreathFocusAsync(magic);
							break;
						}

					case MagicSort.FatalCross:
						result = await ProcessFatalCrossAsync(magic);
						break;

					case MagicSort.FatalSpin:
						result = await ProcessFatalSpinAsync(magic);
						break;

					case MagicSort.Targetdrag:
						result = await ProcessTargetdragAsyncAsync(magic);
						break;

					default:
						logger.Warning($"MagicProcessing::LaunchAsync AutoActive[{autoActive}] {magic.Name} {magic.Sort} not handled!!!");
						result = true;
						break;
				}
			}
			catch (Exception ex)
			{
				logger.Fatal(ex, "Error on LaunchAsync(). {message}", ex.Message);
			}

			if (magic.Sort != MagicSort.Attachstatus)
			{
				await role.ProcessAfterAttackAsync();
			}

			autoAttackCount++;
			return result;
		}

		#region Processing

		#region 1 - Attack

		private async Task<bool> ProcessAttackAsync(Magic magic)
		{
			if (magic == null || idTarget == 0)
			{
				return false;
			}

			Role targetRole = role.Map.QueryAroundRole(role, idTarget);
			if (targetRole == null
				|| !targetRole.IsAlive
				|| !targetRole.IsAttackable(role))
			{
				return false;
			}

			if (role.IsImmunity(targetRole))
			{
				return false;
			}

			if (magic.FloorAttr > 0)
			{
				int nAttr = targetRole.Map[targetRole.X, targetRole.Y].Elevation;
				if (nAttr != magic.FloorAttr)
				{
					return false;
				}
			}
			else
			{
				if ((magic.Type == TWOFOLDBLADES_ID || magic.Type == SUPERTWOFOLDBLADES_ID)
					&& !role.IsWing && targetRole.IsWing)
				{
					return false;
				}
			}

			MagicType byMagic = HitByMagic(magic);

			int magicPower = magic.Power;
			if (role.IsBowman && role.QueryStatus(StatusSet.INTENSIFY) != null)
			{
				int intensifyPowerDebug = role.QueryStatus(StatusSet.INTENSIFY).Power;
				intensifyPowerDebug %= 30000;
				intensifyPowerDebug -= 100;
				magicPower += intensifyPowerDebug;
				await role.DetachStatusAsync(StatusSet.INTENSIFY);
			}

			if (magic.Type == SUPERTWOFOLDBLADES_ID)
			{
				int distance = role.GetDistance(targetRole);
				if (distance <= 3)
				{
					magicPower = (int)(30000 + magic.Data);
				}
			}

			var result = await BattleSystem.CalcPowerAsync(byMagic, role, targetRole, magicPower);
			int power = result.Damage;

			var user = role as Character;
			if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
			{
				await user.SendEffectAsync("LuckyGuy", true);
				power *= 2;
			}

			int displayPower = power;
			power += result.ElementalDamage;

			if (role.IsCallPet())
			{
				user = RoleManager.GetUser(role.OwnerIdentity);
			}

			//PENDIENTE
			var currentEvent = user?.GetCurrentEvent();
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(targetRole.Identity, displayPower, true, result.Effect, result.ElementalDamage);
			await role.BroadcastRoomMsgAsync(msg, true);

			await CheckCrimeAsync(targetRole, magic);

			var totalExp = 0;
			if (power > 0 && !await targetRole.CheckScapegoatAsync(role))
			{
				var lifeLost = (int)Math.Min(targetRole.MaxLife, power);
				await targetRole.BeAttackAsync(byMagic, role, power, true);
				totalExp = lifeLost;

				if (user != null && targetRole is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
				{
					dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
				}

				if (currentEvent != null)
				{
					await currentEvent.OnHitAsync(user, targetRole, magic);
				}
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			if (totalExp > 0)
			{
				await AwardExpOfLifeAsync(targetRole, totalExp, magic);
			}

			if (magic.Type == EAGLE_EYE)
			{
				if (magics.TryGetValue(ADRENALINE_RUSH, out var adrenalineRush) && targetRole.HasDeadMark())
				{
					MsgMagicEffect reset = new MsgMagicEffect
					{
						AttackerIdentity = role.Identity,
						MagicIdentity = adrenalineRush.Type,
						MagicLevel = adrenalineRush.Level
					};
					reset.Append(role.Identity, (int)EAGLE_EYE, true);
					await user.BroadcastRoomMsgAsync(reset, true);

					magic.ResetDelay();
				}
			}

			if (!targetRole.IsAlive)
			{
				if (targetRole is Monster m && m.SpeciesType == 0)
				{
					var nBonusExp = (int)(targetRole.MaxLife * 20 / 100);
					if (user != null)
					{
						await user.BattleSystem.OtherMemberAwardExpAsync(targetRole, nBonusExp);
					}
				}

				await role.KillAsync(targetRole, GetDieMode());
			}
			else
			{
				if (magic.Type == TWOFOLDBLADES_ID || magic.Type == SUPERTWOFOLDBLADES_ID)
				{
					Magic gapingWounds = role.MagicData[(int)GAPING_WOUNDS];
					if (gapingWounds != null)
					{
						await role.ProcessMagicAttackAsync(gapingWounds.Type, idTarget, role.X, role.Y, (uint)AutoActive.OnAttack);
					}
				}
			}

			if (user != null)
			{
				await user.SendWeaponMagic2Async(targetRole);
			}

			return true;
		}

		#endregion

		#region 2 - Recruit

		private async Task<bool> ProcessRecruitAsync(Magic magic)
		{
			if (magic == null || idTarget == 0)
			{
				return false;
			}

			var setTarget = new List<Role>();
			var team = role.GetTeam();
			if (team != null && magic.Multi != 0)
			{
				foreach (var member in team.Members)
				{
					if (!member.IsAlive
						|| (!member.IsPlayer() && !member.IsMonster())
						|| role.GetDistance(member) > Screen.VIEW_SIZE)
					{
						continue;
					}

					setTarget.Add(member);
				}
			}
			else
			{
				Role targetRole = role.Map.QueryAroundRole(role, idTarget);

				if (targetRole == null
					|| role.GetDistance(targetRole) > magic.Distance
					|| !targetRole.IsAlive)
				{
					return false;
				}

				setTarget.Add(targetRole);
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			var exp = 0;
			foreach (Role target in setTarget)
			{
				if (!target.IsAlive)
				{
					continue;
				}

				var power = (int)Math.Min(magic.Power, target.MaxLife - target.Life);
				if (power == Calculations.ADJUST_FULL)
				{
					power = (int)(target.MaxLife - target.Life);
				}

				exp += power;

				msg.Append(target.Identity, power, false);

				if (power > 0)
				{
					await target.AddAttributesAsync(ClientUpdateType.Hitpoints, power);

					if (target is Character user)
					{
						await user.BroadcastTeamLifeAsync();
					}
				}
			}

			if (role.Map.IsTrainingMap())
			{
				exp = Math.Max(exp, magic.Power);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			await AwardExpAsync(0, exp, true);
			return true;
		}

		#endregion

		#region 4 - Fan

		private async Task<bool> ProcessFanAsync(Magic magic)
		{
			int range = (int)magic.Distance + 2;
			const int WIDTH = DEFAULT_MAGIC_FAN;
			long experience = 0, battleExp = 0;

			var setTarget = new List<Role>();
			var center = new Point(role.X, role.Y);

			Role targetRole = role.Map.QueryAroundRole(role, idTarget);
			if (targetRole != null && targetRole.IsAlive)
			{
				setTarget.Add(targetRole);
			}

			List<Role> targets = role.Map.Query9BlocksByPos(role.X, role.Y);
			foreach (Role target in targets)
			{
				if (target.Identity == role.Identity)
				{
					continue;
				}

				int width = WIDTH;
				if (magic.Sort == MagicSort.Blisteringwave)
				{
					width = 60;

					if (magic.Type == 11990)
					{
						range = (int)magic.Range;
					}
				}

				var posThis = new Point(target.X, target.Y);
				if (!Calculations.IsInFan(center, targetPos, posThis, width, range))
				{
					continue;
				}

				if (target.IsAttackable(role)
					&& !role.IsImmunity(target)
					&& target.Identity != idTarget)
				{
					setTarget.Add(target);
				}
			}

			await CheckCrimeAsync(setTarget.ToDictionary(x => x.Identity), magic);

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			MagicType byMagic = HitByMagic(magic);
			var bMagic2Dealt = false;
			Dictionary<uint, int> damageSet = new();
			foreach (Role target in setTarget)
			{
				var result = await BattleSystem.CalcPowerAsync(byMagic, role, target, magic.Power);
				int power = result.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 250))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					power *= 2;
				}
				int displayPower = power;
				power += result.ElementalDamage;
				
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, displayPower, true, result.Effect, result.ElementalDamage);

				if (!await target.CheckScapegoatAsync(role))
				{
					damageSet.Add(target.Identity, power);
				}

				if (!bMagic2Dealt && await ChanceCalcAsync(5d) && user != null)
				{
					await user.SendWeaponMagic2Async(target);
					bMagic2Dealt = true;
				}
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await role.BroadcastRoomMsgAsync(msg, true);

			foreach (var damage in damageSet)
			{
				var target = setTarget.FirstOrDefault(x => x.Identity == damage.Key);

				var lifeLost = (int)Math.Min(target.Life, damage.Value);
				await target.BeAttackAsync(byMagic, role, lifeLost, true);

				if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
				{
					experience += lifeLost;
					battleExp += user.AdjustExperience(target, lifeLost, false);
					if (!target.IsAlive)
					{
						var nBonusExp = (int)(target.MaxLife * 20 / 100d);

						if (user.Team != null)
						{
							await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
						}

						experience += user.AdjustExperience(target, nBonusExp, false);
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
					await role.KillAsync(target, GetDieMode());
				}
			}

			await AwardExpAsync(battleExp, experience, false, magic);
			return true;
		}

		#endregion

		#region 5 - Bomb

		private async Task<bool> ProcessBombAsync(Magic magic)
		{
			var setTarget = new List<Role>();
			(List<Role> Roles, Point Center) result = CollectTargetBomb(0, (int)magic.Range + role.SizeAddition);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)result.Center.X,
				MapY = (ushort)result.Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			await CheckCrimeAsync(result.Roles.ToDictionary(x => x.Identity, x => x), magic);

			long battleExp = 0;
			long exp = 0;
			bool miss = true;
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			foreach (Role target in result.Roles)
			{
				if (magic.Ground != 0 && target.IsWing)
				{
					continue;
				}

				miss = false;

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = atkResult.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					damage *= 2;
				}
				int displayPower = damage;
				damage += atkResult.ElementalDamage;

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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
						await role.KillAsync(target, GetDieMode());
					}
				}

				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, displayPower, true, atkResult.Effect, atkResult.ElementalDamage);
			}

			if (msg.Count > 0 || result.Roles.Count == 0)
			{
				await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			if (user != null)
			{
				if (miss)
				{
					await user.ProcessMagicAttackAsync((ushort)BREATH_FOCUS, user.Identity, user.X, user.Y, (uint)AutoActive.OnAttack);
				}
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 6 - Attach Status

		private async Task<bool> ProcessAttachAsync(Magic magic)
		{
			Role target = role.Map.QueryRole(idTarget);
			if (target == null)
			{
				return false;
			}

			/*
             * 64 can only be used on dead players
             */
			if (!target.IsAlive && magic.Target != 64 && target is not Character targetUser)
			{
				return false;
			}

			int power = magic.Power;
			var secs = (int)magic.StepSeconds;
			var times = (int)magic.ActiveTimes;
			var status = magic.Status;
			var level = magic.Level;

			if (power < 0)
			{
				logger.Warning($"Error magic type invalid power {magic.Type} {magic.Power}");
				return false;
			}

			if (magic.Target == 64)
			{
				if (target.IsAlive || !target.IsPlayer())
				{
					return false;
				}

				if (magic.Status == StatusSet.SOUL_SHACKLE && target.QueryStatus(StatusSet.SOUL_SHACKLE) != null)
				{
					return false;
				}
			}

			if (secs <= 0)
			{
				secs = int.MaxValue;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			var damage = 1;
			switch (status)
			{
				case StatusSet.FLY:
					{
						if (target.Identity != role.Identity)
						{
							return false;
						}

						if (!target.IsBowman || !target.IsAlive)
						{
							return false;
						}

						if (target.Map.IsWingDisable())
						{
							return false;
						}

						if (target.QueryStatus(StatusSet.SHIELD) != null)
						{
							return false;
						}

						if (target.QueryStatus(StatusSet.RIDING) != null)
						{
							return false;
						}

						break;
					}

				case StatusSet.LUCKY_DIFFUSE:
					{
						damage = 1;
						break;
					}

				case StatusSet.SOUL_SHACKLE:
				case StatusSet.POISON_STAR:
					{
						if (target is not Character)
						{
							return false;
						}

						int chance = 100 - Math.Min(20, Math.Max(0, target.BattlePower - role.BattlePower)) * 5;
						if (!await ChanceCalcAsync(chance))
						{
							msg.Append(target.Identity, 0, false);
							await role.BroadcastRoomMsgAsync(msg, true);
							return true;
						}

						if (status == StatusSet.SOUL_SHACKLE)
						{
							//LuaScriptManager.Run(role as Character, target as Character, null, string.Empty, "Event_KeepGhost()");
						}
						break;
					}

				case StatusSet.RIDING:
					{
						if (role.Map.IsRaceTrack() && role.QueryStatus(StatusSet.RIDING) != null)
						{
							// cannot give up mount while in race track
							return false;
						}
						break;
					}

				case StatusSet.AZURE_SHIELD:
					{
						if (target is not Character)
						{
							return false;
						}

						secs = (int)magic.StatusData1;
						power = (int)magic.StatusData0;
						break;
					}

				case StatusSet.PATH_OF_SHADOW:
					{
						if (target.QueryStatus(StatusSet.PATH_OF_SHADOW) != null)
						{
							damage = 0;
							await target.DetachStatusAsync(StatusSet.PATH_OF_SHADOW);
							await target.DetachStatusAsync(StatusSet.BLADE_FLURRY);
							await target.DetachStatusAsync(StatusSet.KINETIC_SPARK);
						}
						else
						{
							msg.MapX = (ushort)(role.Identity - (role.Identity << 16));
							msg.MapY = (ushort)(role.Identity >> 16);							
							await target.AttachStatusAsync(StatusSet.PATH_OF_SHADOW, 0, int.MaxValue, 0, null);
							msg.Append(role.Identity, 0, false, InteractionEffect.None, (int)role.Identity, role.X, role.Y);
						}
						break;
					}

				case StatusSet.DEFENSIVE_INSTANCE:
					{
						if (role.QueryStatus(StatusSet.DEFENSIVE_INSTANCE) != null)
						{
							await role.DetachStatusAsync(StatusSet.DEFENSIVE_INSTANCE);
							damage = 0;
						}
						else if (role.Map.IsFamilyMap())
						{
							damage = 0;
						}
						break;
					}

				case StatusSet.DRAGON_FLOW:
					{
						if (role.QueryStatus(StatusSet.DRAGON_FLOW) != null)
						{
							await role.DetachStatusAsync(StatusSet.DRAGON_FLOW);
							damage = 0;
						}
						else
						{
							times = int.MaxValue;
						}
						break;
					}
			}

			if (status != StatusSet.PATH_OF_SHADOW)
			{
				msg.Append(target.Identity, damage, damage != 0);
			}
			await role.Map.BroadcastRoomMsgAsync(target.X, target.Y, msg);

			if (damage == 0)
			{
				return true;
			}

			await CheckCrimeAsync(target, magic);

			await target.AttachStatusAsync(role, status, power, secs, times, magic);

			if (power >= Calculations.ADJUST_PERCENT && target.IsPlayer())
			{
				int powerTimes = power - 30000 - 100;
				switch (status)
				{
					case StatusSet.STAR_OF_ACCURACY:
						await target.SendAsync(string.Format(StrAccuracyActiveP, secs, powerTimes));
						break;
					case StatusSet.DODGE:
						await target.SendAsync(string.Format(StrDodgeActiveP, secs, powerTimes));
						break;
					case StatusSet.STIGMA:
						await target.SendAsync(string.Format(StrStigmaActiveP, secs, powerTimes));
						break;
					case StatusSet.SHIELD:
						await target.SendAsync(string.Format(StrShieldActiveP, secs, powerTimes));
						break;
				}
			}
			else if (target.IsPlayer())
			{
				switch (status)
				{
					case StatusSet.STAR_OF_ACCURACY:
						await target.SendAsync(string.Format(StrAccuracyActiveT, secs, power));
						break;
					case StatusSet.DODGE:
						await target.SendAsync(string.Format(StrDodgeActiveT, secs, power));
						break;
					case StatusSet.STIGMA:
						await target.SendAsync(string.Format(StrStigmaActiveT, secs, power));
						break;
					case StatusSet.SHIELD:
						await target.SendAsync(string.Format(StrShieldActiveT, secs, power));
						break;
				}
			}

			if (role is Character)
			{
				await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			}

			return true;
		}

		#endregion

		#region 7 - Detach Status

		private async Task<bool> ProcessDetachAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			Role target = role.Map.QueryRole(idTarget);
			if (target == null)
			{
				return false;
			}

			int power = magic.Power;
			var secs = (int)magic.StepSeconds;
			var times = (int)magic.ActiveTimes;
			var status = magic.Status;
			var level = magic.Level;

			if (!target.IsAlive && target.IsPlayer())
			{
				if (status != 0)
				{
					return false;
				}
			}

			if (status == 0 && target is Character user)
			{
				//LuaScriptManager.Run(role as Character, user, null, string.Empty, $"Event_Save_User()");
				await user.RebornAsync(false, true);
				await user.Map.SendMapInfoAsync(user);
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			switch (status)
			{
				case StatusSet.FLY:
					{
						if (!target.IsWing)
						{
							return true;
						}

						if (target.BattlePower > role.BattlePower)
						{
							int chance = 100 - Math.Min(20, Math.Max(0, target.BattlePower - role.BattlePower)) * 5;
							if (!await ChanceCalcAsync(chance))
							{
								msg.Append(target.Identity, 0, false);
								await role.BroadcastRoomMsgAsync(msg, true);
								return true;
							}
						}
						break;
					}
			}

			msg.Append(target.Identity, power, true);
			await target.BroadcastRoomMsgAsync(msg, true);

			if (power > 0)
			{
				var lifeLost = (int)Math.Min(target.Life, Math.Max(0, Calculations.AdjustData(target.Life, power)));
				await target.BeAttackAsync(HitByMagic(magic), role, lifeLost, true);
				await target.AddAttributesAsync(ClientUpdateType.Hitpoints, lifeLost * -1);
			}

			await target.DetachStatusAsync(status);
			return true;
		}

		#endregion

		#region 11 - Dispatch XP

		private async Task<bool> ProcessDispatchXpAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			if (role is Character user && user.Team != null)
			{
				foreach (Character member in user.Team.Members.Where(x => x.Identity != user.Identity && x.IsAlive))
				{
					if (role.GetDistance(member) > Screen.VIEW_SIZE * 2)
					{
						continue;
					}

					msg.Append(member.Identity, DISPATCHXP_NUMBER, true);
					await member.SetXpAsync(DISPATCHXP_NUMBER);
					await member.BurstXpAsync();
					await member.SendAsync(string.Format(StrDispatchXp, user.Name));
				}
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 12 - Collide

		private async Task<bool> ProcessCollideFailAsync(ushort x, ushort y, int nDir)
		{
			var targetX = (ushort)(x + GameMapData.WalkXCoords[nDir]);
			var targetY = (ushort)(y + GameMapData.WalkYCoords[nDir]);

			if (!role.Map.IsStandEnable(targetX, targetY))
			{
				if (role is Character owner)
				{
					await owner.SendAsync(StrInvalidMsg);
					await RoleManager.KickOutAsync(owner.Identity, "INVALID COORDINATES ProcessCollideFail");
				}

				return false;
			}

			var pMsg = new MsgInteract
			{
				SenderIdentity = role.Identity,
				TargetIdentity = 0,
				PosX = targetX,
				PosY = targetY,
				Action = MsgInteractType.Dash,
				Data = nDir * 0x01000000
			};

			await role.BroadcastRoomMsgAsync(pMsg, true);
			if (role is Character character)
			{
				await character.ProcessOnMoveAsync();
			}
			await role.MoveTowardAsync(nDir, (int)RoleMoveMode.Collide);

			return true;
		}

		#endregion

		#region 14 - Line

		private async Task<bool> ProcessLineAsync(Magic magic)
		{
			List<Role> allTargets = role.Map.Query9BlocksByPos(role.X, role.Y);
			var targets = new List<Role>();
			var setPoint = new List<Point>();
			var pos = new Point(role.X, role.Y);
			Calculations.DDALine(pos.X, pos.Y, targetPos.X, targetPos.Y, (int)magic.Range, ref setPoint);

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			long exp = 0;
			long battleExp = 0;
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			bool miss = true;
			Tile userTile = role.Map[role.X, role.Y];
			foreach (Point point in setPoint)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				Tile targetTile = role.Map[point.X, point.Y];
				if (userTile.Elevation - targetTile.Elevation > 26)
				{
					continue;
				}

				Role target = allTargets.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
				if (target == null || target.Identity == role.Identity)
				{
					continue;
				}

				if (magic.Ground != 0 && target.IsWing)
				{
					continue;
				}

				if (!role.Map.IsAltEnable(role.X, role.Y, target.X, target.Y))
				{
					continue;
				}

				if (role.IsImmunity(target)
					|| !target.IsAttackable(role))
				{
					continue;
				}

				miss = false;

				var result = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = result.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					damage *= 2;
				}

				int displayDamage = damage;
				damage += result.ElementalDamage;

				await CheckCrimeAsync(target, magic);

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, result.Damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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
						await role.KillAsync(target, GetDieMode());
					}
				}

				msg.Append(target.Identity, result.Damage, true, result.Effect, result.ElementalDamage);
				targets.Add(target);
			}

			if (user != null)
			{
				await user.SendWeaponMagic2Async();

				if (miss)
				{
					await user.ProcessMagicAttackAsync((ushort)BREATH_FOCUS, user.Identity, user.X, user.Y, (uint)AutoActive.OnAttack);
				}
			}

			await role.BroadcastRoomMsgAsync(msg, true);

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 16 - Attack Status

		private async Task<bool> ProcessAttackStatusAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			Role target = role.Map.QueryRole(idTarget);
			if (target == null)
			{
				return false;
			}

			if (target.MapIdentity != role.MapIdentity
				|| role.GetDistance(target) > magic.Distance + target.SizeAddition)
			{
				return false;
			}

			if (!target.IsAttackable(role) || role.IsImmunity(target))
			{
				return false;
			}

			if (magic.Ground != 0 && target.IsWing)
			{
				return false;
			}

			var power = 0;
			var elementalPower = 0;
			var effect = InteractionEffect.None;

			if (HitByWeapon())
			{
				switch (magic.Status)
				{
					case 0:
						break;
					default:
						var result = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
						power = result.Damage;
						elementalPower = result.ElementalDamage;
						effect = result.Effect;

						await target.AttachStatusAsync(role, (int)magic.Status, magic.Power, (int)magic.StepSeconds,
													   (int)magic.ActiveTimes, magic);

						break;
				}
			}

			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
			{
				await user.SendEffectAsync("LuckyGuy", true);
				power *= 2;
			}

			power += elementalPower;

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(target.Identity, power, true, effect, elementalPower);
			await role.BroadcastRoomMsgAsync(msg, true);

			await CheckCrimeAsync(target, magic);

			long battleExp = 0;
			if (power > 0 && !await target.CheckScapegoatAsync(role))
			{
				var lifeLost = (int)Math.Max(0, Math.Min(target.Life, power));

				await target.BeAttackAsync(HitByMagic(magic), role, power, true);

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
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, AWARDEXP_BY_TIMES, magic);

			if (!target.IsAlive)
			{
				await role.KillAsync(target, GetDieMode());
			}

			return true;
		}

		#endregion

		#region 19 - Transform

		private async Task<bool> ProcessTransformAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			await role.BroadcastRoomMsgAsync(msg, true);
			await user.TransformAsync((uint)magic.Power, (int)magic.StepSeconds, true);
			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 20 - Add Mana

		private async Task<bool> ProcessAddManaAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			Role target = null;
			if (magic.Target == 2) // self
			{
				target = role;
			}
			else if (magic.Target == 1) // target
			{
				target = role.Map.QueryRole(idTarget);
			}
			else // unhandled
			{
				logger.Warning($"Add mana unhandled target {magic.Target}");
				return false;
			}

			if (target.Identity != role.Identity
				&& (target.MapIdentity != role.MapIdentity || role.GetDistance(target) > magic.Distance))
			{
				return false;
			}

			var addMana = (int)Math.Max(0, Math.Min(target.MaxMana - target.Mana, magic.Power));

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(target.Identity, addMana, true);
			await target.BroadcastRoomMsgAsync(msg, true);

			await target.AddAttributesAsync(ClientUpdateType.Mana, addMana);

			await AwardExpAsync(0, 0, Math.Max(addMana, AWARDEXP_BY_TIMES), magic);
			return true;
		}

		#endregion

		#region 21 - Lay Trap

		private async Task<bool> ProcessLayTrapAsync(Magic magic)
		{
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				Command = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			await role.BroadcastRoomMsgAsync(msg, true);

			int idTrapType = (int)magic.Data;

			DbTrapType trapType = await TrapTypeRepository.GetAsync((uint)idTrapType);
			if (trapType == null)
			{
				return false;
			}

			MapTrap mapTrap = new MapTrap(new DbTrap
			{
				Id = (uint)IdentityManager.Traps.GetNextIdentity,
				MapId = role.MapIdentity,
				OwnerId = role.Identity,
				PosX = (ushort)targetPos.X,
				PosY = (ushort)targetPos.Y,
				TypeId = (uint)idTrapType,
				Look = trapType.Look,
				Type = trapType
			});

			await mapTrap.InitializeAsync(role, magic);

			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 23 - Callpet

		private async Task<bool> ProcessCallPetAsync(Magic magic)
		{
			if (magic == null)
				return false;

			if (role is not Character user || user.Map.IsBoothEnable() || user.Map.IsTrainingMap())
				return false;

			await user.CallPetAsync((uint)magic.Power, ((ushort)(user.X - 2)), ((ushort)(user.Y - 2)), (int)magic.StepSeconds);

			MsgMagicEffect msg = new MsgMagicEffect
			{
				AttackerIdentity = user.Identity,
				MapX = ((ushort)(user.X - 2)),
				MapY = ((ushort)(user.Y - 2)),
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level
			};
			await user.BroadcastRoomMsgAsync(msg, true);

			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 26 - Dec Life

		private async Task<bool> ProcessDecLifeAsync(Magic magic)
		{
			Role target = role.Map.QueryAroundRole(role, idTarget);
			if (target == null || !target.IsAlive)
			{
				return false;
			}

			if (IsImmunity(magic, target))
			{
				return false;
			}

			int power = Calculations.CutTrail(0, Calculations.AdjustDataEx((int)target.Life, magic.Power));

			MsgMagicEffect msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = target.X,
				MapY = target.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(target.Identity, power, true);
			await target.BroadcastRoomMsgAsync(msg, true);

			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			await CheckCrimeAsync(target, magic);
			int totalExp = 0;
			if (power > 0)
			{
				var lifeLost = (int)Math.Min(target.MaxLife, power);
				await target.BeAttackAsync(HitByMagic(magic), role, power, true);
				totalExp = lifeLost;

				if (user != null && target is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
				{
					dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
				}

				if (currentEvent != null)
				{
					await currentEvent.OnHitAsync(user, target, magic);
					await currentEvent.OnAttackAsync(user);
				}

				if (totalExp > 0)
				{
					await AwardExpOfLifeAsync(target, totalExp, magic);
				}

				if (!target.IsAlive)
				{
					var nBonusExp = (int)(target.MaxLife * 20 / 100);
					if (user != null)
					{
						await user.BattleSystem.OtherMemberAwardExpAsync(target, nBonusExp);
					}

					await role.KillAsync(target, GetDieMode());
				}
			}

			return true;
		}

		#endregion

		#region 27 - Ground Sting

		private async Task<bool> ProcessGroundStingAsync(Magic magic)
		{
			if (magic == null || magic.Status <= 0)
			{
				return false;
			}

			(List<Role> Roles, Point Center) targetLocked = CollectTargetBomb(0, (int)magic.Range);
			Point center = targetLocked.Center;
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)center.X,
				MapY = (ushort)center.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			var msgSent = false;
			var setTarget = new List<Role>();
			foreach (Role target in targetLocked.Roles)
			{
				if (magic.Ground != 0 && target.IsWing)
				{
					continue;
				}

				if (Calculations.GetDistance(center.X, center.Y, target.X, target.Y) > magic.Range)
				{
					continue;
				}

				if (!target.IsPlayer() && !target.IsMonster())
				{
					continue;
				}

				if (target is Monster targetMonster)
				{
					if (targetMonster.IsGuard())
					{
						continue;
					}

					if (targetMonster.SpeciesType != 0)
					{
						continue;
					}
				}

				int chance = 100 - Math.Min(20, Math.Max(0, target.BattlePower - role.BattlePower)) * 5;
				var damage = 1;
				if (!await ChanceCalcAsync(chance))
				{
					damage = 0;
				}

				if (msg.Count >= 25)
				{
					await role.Map.BroadcastRoomMsgAsync(center.X, center.Y, msg);
					msg.ClearTargets();
					msgSent = true;
				}

				msg.Append(target.Identity, damage, true);

				if (damage > 0)
				{
					setTarget.Add(target);
				}
			}

			await CheckCrimeAsync(setTarget.ToDictionary(x => x.Identity), magic);

			if (msg.Count > 0 || !msgSent)
			{
				await role.Map.BroadcastRoomMsgAsync(center.X, center.Y, msg);
			}

			foreach (Role target in setTarget)
			{
				await target.AttachStatusAsync(role, magic.Status, magic.Power, (int)magic.StepSeconds,
											   (int)magic.ActiveTimes, magic);
			}

			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 28 - Vortex

		private async Task<bool> ProcessVortexAsync(Magic magic)
		{
			if (!role.IsAlive)
			{
				return false;
			}

			if (role.IsWing)
			{
				return false;
			}

			(List<Role> roles, Point Center) = CollectTargetBomb(0, (int)magic.Range);

			await CheckCrimeAsync(roles.ToDictionary(x => x.Identity, x => x), magic);

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)Center.X,
				MapY = (ushort)Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long battleExp = 0;
			long exp = 0;
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			foreach (Role target in roles)
			{
				if (magic.Ground != 0 && target.IsWing)
				{
					continue;
				}

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);

				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					atkResult.Damage *= 2;
				}

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(atkResult.Damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, atkResult.Damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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
						await role.KillAsync(target, GetDieMode());
					}
				}

				if (msg.Count < MAX_TARGET_NUM)
				{
					msg.Append(target.Identity, atkResult.Damage, true);
				}
			}

			await role.BroadcastRoomMsgAsync(msg, true);

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 29 - Activate Switch

		private async Task<bool> ProcessActivateSwitchAsync(Magic magic)
		{
			if (idTarget == 0)
			{
				return false;
			}

			Role target = role.Map.QueryAroundRole(role, idTarget);
			if (target == null)
			{
				return false;
			}

			var result = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
			int damage = result.Damage;

			long battleExp = 0;

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = target.X,
				MapY = target.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(target.Identity, damage, true, result.Effect, damage);
			await role.BroadcastRoomMsgAsync(msg, true);

			if (damage > 0)
			{
				var lifeLost = (int)Math.Max(0, Math.Min(target.Life, damage));

				await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

				var user = role as Character;
				var currentEvent = user?.GetCurrentEvent();
				if (user != null && target is Monster monster)
				{
					battleExp += user.AdjustExperience(target, lifeLost, false);
					if (!monster.IsAlive)
					{
						var nBonusExp = (int)(monster.MaxLife * 20 / 100d);

						if (user.Team != null)
						{
							await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
						}

						battleExp += user.AdjustExperience(monster, nBonusExp, false);
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
			}

			if (!target.IsAlive)
			{
				await role.KillAsync(target, GetDieMode());
			}

			if (battleExp > 0)
			{
				await AwardExpAsync(0, battleExp, AWARDEXP_BY_TIMES, magic);
			}

			return true;
		}

		#endregion

		#region 30 - Spook

		private async Task<bool> ProcessSpookAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			int steedPoints = int.MaxValue;
			if (role is Character user)
			{
				Item mount = user.Mount;
				if (mount == null)
				{
					return false;
				}

				if (role.QueryStatus(StatusSet.RIDING) == null)
				{
					return false;
				}

				steedPoints = Item.AdditionPoints(mount);
			}

			Character target = RoleManager.GetUser(idTarget);
			if (target == null || target.Identity == role.Identity || role.GetDistance(target) > magic.Distance)
			{
				return false;
			}

			if (target.Mount == null || target.QueryStatus(StatusSet.RIDING) == null)
			{
				return false;
			}

			if (steedPoints < Item.AdditionPoints(target.Mount))
			{
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = target.X,
				MapY = target.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(target.Identity, 0, true);
			await role.BroadcastRoomMsgAsync(msg, true);

			await target.DetachStatusAsync(StatusSet.RIDING);
			return true;
		}

		#endregion

		#region 31 - WarCry

		private async Task<bool> ProcessWarCryAsync(Magic magic)
		{
			if (magic == null)
			{
				return false;
			}

			int steedPoints = int.MaxValue;
			if (role is Character user)
			{
				Item mount = user.Mount;
				if (mount == null)
				{
					return false;
				}

				if (role.QueryStatus(StatusSet.RIDING) == null)
				{
					return false;
				}

				steedPoints = Item.AdditionPoints(mount);
			}

			List<Role> setTarget = new();
			var targets = role.Map.Query9Blocks(role.X, role.Y);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			foreach (var target in targets)
			{
				if (target.Identity == role.Identity || target is not Character targetUser)
				{
					continue;
				}

				if (role.GetDistance(targetUser) > magic.Distance)
				{
					continue;
				}

				if (targetUser.Mount == null || target.QueryStatus(StatusSet.RIDING) == null)
				{
					continue;
				}

				if (steedPoints < Item.AdditionPoints(targetUser.Mount))
				{
					continue;
				}

				msg.Append(target.Identity, 0, true);
				setTarget.Add(target);
			}
			await role.BroadcastRoomMsgAsync(msg, true);

			foreach (var target in setTarget.Cast<Character>())
			{
				await target.DetachStatusAsync(StatusSet.RIDING);
			}

			return true;
		}

		#endregion

		#region 32 - Riding

		private async Task<bool> ProcessRidingAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			Item mount = user.UserPackage[Item.ItemPosition.Mount];
			if (mount == null)
			{
				return false;
			}

			if (user.QueryStatus(StatusSet.RIDING) != null)
			{
				await user.DetachStatusAsync(StatusSet.RIDING);
				return true;
			}

			if (user.QueryStatus(StatusSet.FLY) != null)
			{
				return false;
			}

			if (user.Map.IsTrainingMap() || user.Map.IsPkField())
			{
				return false;
			}

			if (user.Map.QueryRegion(RegionType.City, user.X, user.Y) && mount.Plus < 4)
			{
				return false;
			}

			if (user.Map.IsBoothEnable() && mount.Plus < 6)
			{
				return false;
			}

			await user.AttachStatusAsync(user, StatusSet.RIDING, 0, (int)magic.StepSeconds, 0, magic);
			await user.SetAttributesAsync(ClientUpdateType.Vigor, (ulong)user.MaxVigor);
			user.UpdateVigorTimer();
			return true;
		}

		#endregion

		#region 34 - Attach Status Area

		private async Task<bool> ProcessAttachStatusAreaAsync(Magic magic)
		{
			(List<Role> Roles, Point Center) lockedTarget = CollectTargetBomb(0, (int)magic.Range + 1);

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y
			};

			int power = magic.Power;
			int secs = (int)magic.StepSeconds;
			int times = (int)magic.ActiveTimes;
			int status = magic.Status;

			foreach (var target in lockedTarget.Roles)
			{
				if (target.Identity == role.Identity)
				{
					continue;
				}

				if (!target.IsAlive && magic.Target != 64)
				{
					continue;
				}

				if (magic.Target == 4 && !target.IsPlayer())
				{
					continue;
				}

				if (magic.Target == 64 && target is Character targetUser)
				{
					if (status == StatusSet.SOUL_SHACKLE && targetUser.QueryStatus(status) != null)
					{
						continue;
					}
				}

				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, power, power != 0);

				await CheckCrimeAsync(target, magic);
				await target.AttachStatusAsync(role, status, power, secs, times, magic);
			}

			if (msg.Count > 0)
			{
				await role.BroadcastRoomMsgAsync(msg, true);
			}

			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 35 - Fan Status

		private async Task<bool> ProcessFanStatusAsync(Magic magic)
		{
			int range = (int)magic.Distance + 2;
			const int WIDTH = DEFAULT_MAGIC_FAN;

			var setTarget = new List<Role>();
			var center = new Point(role.X, role.Y);

			Role targetRole = role.Map.QueryAroundRole(role, idTarget);
			if (targetRole != null && targetRole.IsAlive)
			{
				setTarget.Add(targetRole);
			}

			List<Role> targets = role.Map.Query9BlocksByPos(role.X, role.Y);
			foreach (Role target in targets)
			{
				if (target.Identity == role.Identity)
				{
					continue;
				}

				var posThis = new Point(target.X, target.Y);
				if (!Calculations.IsInFan(center, targetPos, posThis, WIDTH, range))
				{
					continue;
				}

				if (magic.Target == 4 && !target.IsPlayer())
				{
					continue;
				}

				if (target.IsAttackable(role)
					&& !role.IsImmunity(target)
					&& target.Identity != idTarget)
				{
					setTarget.Add(target);
				}
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			var byMagic = HitByMagic(magic);
			foreach (Role target in setTarget)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, 1, true);

				var result = await BattleSystem.CalcPowerAsync(byMagic, role, target, magic.Power);
				int damage = result.Damage;

				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, damage, true, result.Effect, result.ElementalDamage);

				damage += result.ElementalDamage;

				var lifeLost = (int)Math.Min(target.Life, damage);
				await target.BeAttackAsync(byMagic, role, lifeLost, true);

				if (!target.IsAlive)
				{
					await role.KillAsync(target, GetDieMode());
				}
				else if (magic.Status > 0)
				{
					await target.AttachStatusAsync(role, magic.Status, magic.Power, (int)magic.StepSeconds, (int)magic.ActiveTimes, magic);
				}
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			return true;
		}

		#endregion

		#region 36 - Bomb Status

		private async Task<bool> ProcessBombStatusAsync(Magic magic)
		{
			var setTarget = new List<Role>();
			(List<Role> Roles, Point Center) result = CollectTargetBomb(0, (int)magic.Range + role.SizeAddition);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)result.Center.X,
				MapY = (ushort)result.Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long battleExp = 0;
			long exp = 0;
			foreach (Role target in result.Roles)
			{
				if (magic.Ground != 0 && target.IsWing)
				{
					continue;
				}

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = atkResult.Damage;

				msg.Append(target.Identity, damage, true, atkResult.Effect, atkResult.ElementalDamage);
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
					msg.ClearTargets();
				}

				damage += atkResult.ElementalDamage;

				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

				if (!target.IsAlive)
				{
					await role.KillAsync(target, GetDieMode());
				}
				else if (magic.Status > 0)
				{
					await target.AttachStatusAsync(role, magic.Status, magic.Power, (int)magic.StepSeconds, (int)magic.ActiveTimes, magic);
				}
			}

			if (msg.Count > 0)
			{
				await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
			}

			await CheckCrimeAsync(result.Roles.ToDictionary(x => x.Identity, x => x), magic);
			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 37 - Chain XP

		private async Task<bool> ProcessChainXpAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			if (idTarget == 0)
			{
				logger.Warning($"No target for active bolt skill");
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long experience = 0,
				battleExp = 0;
			bool bMagic2Dealt = false;
			Dictionary<uint, Role> possibleTargets = user.Map.Query9BlocksByPos(user.X, user.Y)
				.DistinctBy(x => x.Identity)
				.ToDictionary(x => x.Identity);
			List<Role> setTarget = new();
			MagicType byMagic = HitByMagic(magic);
			Role target = possibleTargets.Values.FirstOrDefault(x => x.Identity == idTarget);

			var CollectNewTarget = () =>
			{
				Role oldTarget = target;
				return possibleTargets.Values
					.Where(x => x.Identity != role.Identity)
					.MinBy(x => x.GetDistance(oldTarget));
			};

			var currentEvent = user?.GetCurrentEvent();
			for (int i = 0; i < magic.ActiveTimes && possibleTargets.Count > 0 && target != null;)
			{
				possibleTargets.Remove(target?.Identity ?? 0);

				if (target == null
					|| user.GetDistance(target) > magic.Distance)
				{
					target = CollectNewTarget();
					continue;
				}

				if (user.IsImmunity(target)
					|| !target.IsAttackable(user))
				{
					target = CollectNewTarget();
					continue;
				}

				if (!await target.CheckScapegoatAsync(role))
				{
					setTarget.Add(target);
				}

				if (i + 1 < magic.ActiveTimes)
				{
					target = CollectNewTarget();
				}
				i++;
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await CheckCrimeAsync(setTarget.ToDictionary(x => x.Identity), magic);

			int o = 0;
			foreach (var atkTarget in setTarget)
			{
				var result = await BattleSystem.CalcPowerAsync(byMagic, role, atkTarget, magic.Power);
				int damage = (int)(result.Damage * (1 - (0.2d * o++))) + result.ElementalDamage;

				var lifeLost = (int)Math.Min(atkTarget.Life, damage);
				await atkTarget.BeAttackAsync(byMagic, role, lifeLost, true);

				if (user != null && (atkTarget is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (atkTarget as DynamicNpc)?.IsGoal() == true))
				{
					experience += lifeLost;
					battleExp += user.AdjustExperience(atkTarget, lifeLost, false);
					if (!atkTarget.IsAlive)
					{
						var nBonusExp = (int)(atkTarget.MaxLife * 20 / 100d);

						if (user.Team != null)
						{
							await user.Team.AwardMemberExpAsync(user.Identity, atkTarget, nBonusExp);
						}

						experience += user.AdjustExperience(atkTarget, nBonusExp, false);
					}
				}

				if (user != null && atkTarget is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
				{
					dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
				}

				if (currentEvent != null)
				{
					await currentEvent.OnHitAsync(user, atkTarget, magic);
				}

				if (!atkTarget.IsAlive)
				{
					await role.KillAsync(atkTarget, GetDieMode());
				}

				if (!bMagic2Dealt && await ChanceCalcAsync(5d) && user != null)
				{
					await user.SendWeaponMagic2Async(atkTarget);
					bMagic2Dealt = true;
				}

				msg.Append(atkTarget.Identity, damage, true, result.Effect, result.ElementalDamage);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			await AwardExpAsync(battleExp, experience, false, magic);
			return true;
		}

		#endregion

		#region 38 - Knockback

		private async Task<bool> ProcessKnockbackAsync(Magic magic)
		{
			Role target = role.Map.QueryAroundRole(role, idTarget);
			if (target == null)
			{
				return false;
			}

			if (role.GetDistance(target) > magic.Distance
				|| !target.IsAttackable(role)
				|| role.IsImmunity(target))
			{
				return false;
			}

			if (target.IsDynaNpc())
			{
				return false;
			}

			int currentX = target.X;
			int currentY = target.Y;
			Point targetPoint = default;
			Role.FacingDirection direction = (Role.FacingDirection)Calculations.GetDirection(role.X, role.Y, target.X, target.Y);
			bool stun = false;
			for (int i = 0; i < magic.Range; i++) // todo predict correct direction
			{
				currentX += GameMapData.WalkXCoords[(int)direction];
				currentY += GameMapData.WalkYCoords[(int)direction];

				if (!role.Map.IsStandEnable(currentX, currentY))
				{
					stun = true;
					break;
				}

				if (!role.Map.IsAltEnable(role.X, role.Y, currentX, currentY))
				{
					stun = true;
					break;
				}

				targetPoint = new Point(currentX, currentY);
			}

			await CheckCrimeAsync(target, magic);

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)targetPoint.X,
				MapY = (ushort)targetPoint.Y
			};
			long exp = 0, battleExp = 0;
			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			var hitByMagic = HitByMagic(magic);
			var result = await BattleSystem.CalcPowerAsync(hitByMagic, role, target, magic.Power);
			int damage = result.Damage + result.ElementalDamage;
			if (!await target.CheckScapegoatAsync(role))
			{
				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, result.Damage, true);

				if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
				{
					exp += lifeLost;
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

				target.X = (ushort)targetPoint.X;
				target.Y = (ushort)targetPoint.Y;

				await NpcServer.SendAsync(new MsgAiAction
				{
					Data = new MsgAiActionContract
					{
						Action = AiActionType.Jump,
						Identity = target.Identity,
						X = target.X,
						Y = target.Y,
						Direction = (int)target.Direction
					}
				});

				if (currentEvent != null)
				{
					await currentEvent.OnHitAsync(user, target, magic);
				}

				await target.ProcessOnMoveAsync();
				if (!target.IsAlive)
				{
					await role.KillAsync(target, GetDieMode());
				}
				else
				{
					if (stun)
					{
						await target.AttachStatusAsync(StatusSet.DIZZY, 0, 2, 0);
					}

					if (target.MagicData.QueryMagic != null && target.MagicData.State == MagicState.Intone)
					{
						await target.MagicData.AbortMagicAsync(true);
					}
				}
			}
			else
			{
				result = new BattleSystem.AttackResult(0, 0, InteractionEffect.None);
			}

			msg.Append(target.Identity, result.Damage, true, result.Effect, result.ElementalDamage);

			await target.SendAsync(msg);
			if (target is Character targetUser)
			{
				await targetUser.Screen.UpdateAsync(msg);
			}
			else
			{
				await target.BroadcastRoomMsgAsync(msg, false);
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 46 - Serenity

		private async Task<bool> ProcessSerenityAsync(Magic magic)
		{
			if (!role.IsAlive)
			{
				return false;
			}

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y
			};
			msg.Append(idTarget, 0, false);
			await role.BroadcastRoomMsgAsync(msg, true);

			foreach (var status in tranquilityDetach)
			{
				await role.DetachStatusAsync(status);
			}

			return true;
		}

		#endregion

		#region 47 - Detach Bad Status

		private readonly int[] tranquilityDetach =
		{
			StatusSet.FRIGHTENED,
			StatusSet.DIZZY,
			StatusSet.POISONED,
			StatusSet.POISON_STAR,
			StatusSet.DECELERATED,
			StatusSet.CONFUSED,
			StatusSet.TOXIC_FOG,
			StatusSet.FREEZE,
			StatusSet.SCURVY_BOMB,
			StatusSet.SOUL_SHACKLE,
			StatusSet.FROZEN
		};

		private async Task<bool> ProcessTranquilityAsync(Magic magic)
		{
			Role roleTarget = role.Map.QueryAroundRole(role, idTarget);
			if (roleTarget == null || roleTarget is not Character targetUser)
			{
				return false;
			}

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y
			};
			msg.Append(idTarget, 0, false);
			await targetUser.BroadcastRoomMsgAsync(msg, true);

			foreach (var status in tranquilityDetach)
			{
				if (status == StatusSet.SOUL_SHACKLE)
				{
					//LuaScriptManager.Run(role as Character, roleTarget as Character, null, string.Empty, "Event_ClearKeepGhost()");
				}
				await targetUser.DetachStatusAsync(status);
			}
			return true;
		}

		#endregion

		#region 48 - Close Line

		private async Task<bool> ProcessCloseLineAsync(Magic magic)
		{
			List<Role> allTargets = role.Map.Query9BlocksByPos(role.X, role.Y);
			var targets = new List<Role>();
			var setPoint = new List<Point>();
			var pos = new Point(role.X, role.Y);
			Calculations.DDALine(pos.X, pos.Y, targetPos.X, targetPos.Y, (int)magic.Range, ref setPoint);

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			long exp = 0;
			long battleExp = 0;
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			var isAttackEnable = (Role target) =>
			{
				if (magic.Ground != 0 && target.IsWing)
				{
					return false;
				}

				if (!role.Map.IsAltEnable(role.X, role.Y, target.X, target.Y))
				{
					return false;
				}

				if (role.IsImmunity(target)
					|| !target.IsAttackable(role))
				{
					return false;
				}

				return true;
			};

			foreach (Point point in setPoint)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				Role target = allTargets.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
				if (target == null)
				{
					continue;
				}

				if (target.Identity == role.Identity)
				{
					continue;
				}

				if (!isAttackEnable(target))
				{
					continue;
				}

				await CheckCrimeAsync(target, magic);
				targets.Add(target);
			}

			if (idTarget != 0 && targets.All(x => x.Identity != idTarget))
			{
				Role roleTarget = role.Map.QueryAroundRole(role, idTarget);
				if (roleTarget != null && isAttackEnable(roleTarget))
				{
					await CheckCrimeAsync(roleTarget, magic);
					targets.Add(roleTarget);
				}
			}

			foreach (var target in targets)
			{
				var result = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = result.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					damage *= 2;
				}

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, result.Damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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
						await role.KillAsync(target, GetDieMode());
					}
				}

				msg.Append(target.Identity, result.Damage, true, result.Effect, damage);
			}

			await role.BroadcastRoomMsgAsync(msg, true);

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 50 - Compassion

		private readonly int[] compassionDetach =
		{
			StatusSet.FRIGHTENED,
			StatusSet.DIZZY,
			StatusSet.POISONED,
			StatusSet.POISON_STAR,
			StatusSet.DECELERATED,
			StatusSet.CONFUSED,
			StatusSet.TOXIC_FOG,
			StatusSet.FREEZE,
			StatusSet.SCURVY_BOMB,
			StatusSet.FROZEN
		};

		private async Task<bool> ProcessCompassionAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			List<Character> targets = new List<Character>();
			if (user.Team == null)
			{
				targets.Add(user);
			}
			else
			{
				targets.AddRange(user.Team.Members);
			}

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y
			};
			foreach (var target in targets)
			{
				if (!target.IsAlive)
				{
					continue;
				}
				foreach (var status in compassionDetach)
				{
					await target.DetachStatusAsync(status);
				}
				msg.Append(target.Identity, 0, false);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			return true;
		}

		#endregion

		#region 51 - Team Flag

		private async Task<bool> ProcessTeamFlagAsync(Magic magic)
		{
			if (role is null || magic is null)
			{
				return false;
			}

			if (magic.Power < 0)
			{
				logger.Warning($"ERROR: magic type {magic.Sort} status [{magic.Status}] invalid power");
				return false;
			}

			await AwardExpAsync(0, 0, AWARDEXP_BY_TIMES, magic); // get updated magic status if plvl time

			for (int i = StatusSet.TYRANT_AURA; i <= StatusSet.EARTH_AURA; i++)
			{
				IStatus currentAura = role.QueryStatus(i);
				if (currentAura == null || currentAura.CasterId != role.Identity)
				{
					continue;
				}
				await role.DetachStatusAsync(i);
			}

			int magicStatus = magic.Status;
			if (magicStatus == StatusSet.MAGIC_DEFENDER)
			{
				await role.DetachStatusAsync(StatusSet.DEFENSIVE_INSTANCE);
			}

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y
			};
			msg.Append(role.Identity, 0, true, 0, 0, 0, 0, true);
			await role.Map.BroadcastRoomMsgAsync(role.X, role.Y, msg);

			await role.AttachStatusAsync(role, magicStatus, magic.Power, (int)magic.StepSeconds, (int)magic.ActiveTimes, magic);

			if (role is Character user)
			{
				if (user.Team != null && !user.Map.IsTrainingMap())
				{
					await user.Team.SendAsync(string.Format(StrTeamAuraCreate, user.Name, magic.Name, user.Map.Name));
					await user.Team.ProcessAuraAsync();
				}
			}
			return true;
		}

		#endregion

		#region 52 - Increase Block

		private async Task<bool> ProcessIncreaseBlockAsync(Magic magic)
		{
			Role target;
			if (idTarget == role.Identity)
			{
				target = role;
			}
			else
			{
				target = role.Map.QueryAroundRole(role, idTarget);
			}

			int power = magic.Power;
			int seconds = (int)magic.StepSeconds;
			int times = (int)magic.ActiveTimes;
			int status = magic.Status;

			if (power < 0)
			{
				logger.Warning($"Invalid power for {magic.Sort} [{power}]");
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level
			};
			msg.Append(target.Identity, 1, true);
			await target.BroadcastRoomMsgAsync(msg, true);

			await role.AttachStatusAsync(status, power, seconds, times, magic);
			return false;
		}

		#endregion

		#region 53 - Oblivion

		private async Task<bool> ProcessOblivionAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			if (!user.IsAlive)
			{
				return false;
			}

			int power = magic.Power;
			int seconds = (int)magic.StepSeconds;
			int times = (int)magic.ActiveTimes;
			const int status = StatusSet.OBLIVION;

			if (power < 0)
			{
				logger.Warning($"Invalid power for magic oblivion {power}");
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			msg.Append(role.Identity, 1, true);
			await role.BroadcastRoomMsgAsync(msg, true);

			await role.AttachStatusAsync(status, power, seconds, times, magic);
			return true;
		}

		#endregion

		#region 54 - Stunbomb

		private async Task<bool> ProcessStunBombAsync(Magic magic)
		{
			(List<Role> Roles, Point Center) = CollectTargetBomb(0, (int)magic.Range);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				Command = magic.ActiveTimes / 100,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			long battleExp = 0;
			long exp = 0;
			var user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			foreach (Role target in Roles)
			{
				await CheckCrimeAsync(target, magic);

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = atkResult.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					damage *= 2;
				}

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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

					await role.ProcessOnMoveAsync();
					await role.ProcessAfterMoveAsync();

					if (!target.IsAlive)
					{
						await role.KillAsync(target, GetDieMode());
					}
					else
					{
						if (role.QueryStatus(StatusSet.FLY) != null)
						{
							await role.DetachStatusAsync(StatusSet.FLY);
						}

						if (target.MagicData.QueryMagic != null && target.MagicData.State == MagicState.Intone)
						{
							await target.MagicData.AbortMagicAsync(true);
						}
					}
				}

				if (msg.Count < MAX_TARGET_NUM)
				{
					msg.Append(target.Identity, damage, true, atkResult.Effect, damage);
				}
			}

			await role.Map.BroadcastRoomMsgAsync(Center.X, Center.Y, msg);

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 55 - Triple Attack

		private async Task<bool> ProcessTripleAttackAsync(Magic magic)
		{
			Role targetRole = role.Map.QueryAroundRole(role, idTarget);
			if (targetRole == null
				|| !role.BattleSystem.IsBattleMaintain()
				|| role.IsImmunity(targetRole)
				|| !targetRole.IsAttackable(role))
			{
				return false;
			}

			await role.CheckCrimeAsync(targetRole);

			int totalDamage = 0;

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			for (int i = 0; i < 3; i++)
			{
				BattleSystem.AttackResult attackResult;
				if (await BattleSystem.IsTargetDodgedAsync(role, targetRole))				
					attackResult = new BattleSystem.AttackResult(0, 0, InteractionEffect.None);				
				else				
					attackResult = await BattleSystem.CalcPowerAsync(MagicType.None, role, targetRole, magic.Power);
				
				msg.Append(targetRole.Identity, attackResult.Damage, true, attackResult.Effect, attackResult.Damage);
				totalDamage += attackResult.Damage;
			}

			await role.BroadcastRoomMsgAsync(msg, true);

			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			if (totalDamage > 0)
			{				
				int lifeLost = (int)Math.Min(targetRole.Life, totalDamage);
				await targetRole.BeAttackAsync(HitByMagic(magic), role, totalDamage, true);

				if (targetRole is Monster monsterTarget)				
					await monsterTarget.AddBossAttackerScore(user, totalDamage);				

				if (targetRole is DynamicNpc dynamicNpc && dynamicNpc.IsAwardScore())
				{
					if (user?.Syndicate != null)
					{
						dynamicNpc.AddSynWarScore(user.Syndicate, lifeLost);
					}
					// TODO support pets
				}

				if (currentEvent != null)
				{
					await currentEvent.OnHitAsync(role, targetRole, magic);
					await currentEvent.OnAttackAsync(user);
				}

				// if (user != null && (target is Monster monster || target is DynamicNpc npc && npc.IsGoal()))
				if (user != null && (targetRole is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (targetRole as DynamicNpc)?.IsGoal() == true))
				{
					await AwardExpAsync(0, lifeLost, AWARDEXP_BY_TIMES, magic);

					if (user != null)
					{
						int nWeaponExp = lifeLost / 3; //(int) (nExp / 10);
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

					if (!targetRole.IsAlive)
					{
						var nBonusExp = (int)(targetRole.MaxLife * 20 / 100);
						if (user?.Team != null)
						{
							await user.BattleSystem.OtherMemberAwardExpAsync(targetRole, nBonusExp);
						}
					}
				}
			}

			if (user != null)
			{
				await user.SendWeaponMagic2Async();
			}

			if (!targetRole.IsAlive)
			{
				await role.KillAsync(targetRole, GetDieMode());
			}

			if (user != null)
			{
				await user.SendWeaponMagic2Async(targetRole);
			}

			return true;
		}

		void DictionaryAddOrUpdate(Dictionary<Character, int> dic, Character key, int newValue)
		{
			int val;
			if (dic.TryGetValue(key, out val))
				dic[key] = val + newValue;
			else
				dic.Add(key, newValue);
		}

		#endregion

		#region 56 - Scurvy Bomb

		private async Task<bool> ProcessScurvyBombAsync(Magic magic)
		{
			var setTarget = new List<Role>();
			(List<Role> Roles, Point Center) result = CollectTargetBomb(0, (int)magic.Range + role.SizeAddition);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)result.Center.X,
				MapY = (ushort)result.Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long battleExp = 0;
			long exp = 0;
			foreach (Role target in result.Roles)
			{
				if (target.IsWing)
				{
					continue;
				}

				int delta = target.BattlePower - role.BattlePower;
				if (delta > 0)
				{
					int chance = Math.Max(80, Math.Min(100, 100 - delta * 2));
					if (await NextAsync(100) > chance)
					{
						continue;
					}
				}

				await CheckCrimeAsync(target, magic);

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = atkResult.Damage;

				msg.Append(target.Identity, damage, true, atkResult.Effect, atkResult.ElementalDamage);
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
					msg.ClearTargets();
				}

				damage += atkResult.ElementalDamage;

				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, lifeLost, true);

				if (!target.IsAlive)
				{
					await role.KillAsync(target, GetDieMode());
				}
				else if (magic.Status > 0)
				{
					await target.AttachStatusAsync(role, magic.Status, (int)magic.StatusData0, (int)magic.StepSeconds, (int)magic.ActiveTimes, magic);
				}
			}

			if (msg.Count > 0 || result.Roles.Count == 0)
			{
				await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
			}

			await AwardExpAsync(0, battleExp, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 57 - Canyon Barrage

		private async Task<bool> ProcessCannonBarrageAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			var currentEvent = user?.GetCurrentEvent();
			var setTarget = new List<Role>();
			(List<Role> Roles, Point Center) result = CollectTargetBomb(0, (int)magic.Range);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)result.Center.X,
				MapY = (ushort)result.Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long battleExp = 0;
			long exp = 0;
			foreach (Role target in result.Roles)
			{
				int power = magic.Power;
				if (target.IsPlayer())
				{
					power = (int)magic.StatusData0;
				}
				else if (target.IsDynaNpc() || (target is Monster m && (m.IsGuard() || m.SpeciesType != 0)))
				{
					power = 30100;
				}

				await CheckCrimeAsync(target, magic);

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, power);
				int damage = atkResult.Damage;
				if (user?.IsLucky == true && await ChanceCalcAsync(1, 100))
				{
					await user.SendEffectAsync("LuckyGuy", true);
					damage *= 2;
				}
				int displayPower = damage;
				damage += atkResult.ElementalDamage;

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

					if (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true)
					{
						exp += lifeLost;
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

					if (target is DynamicNpc dynaNpc && dynaNpc.IsAwardScore())
					{
						dynaNpc.AddSynWarScore(user.Syndicate, lifeLost);
					}

					if (currentEvent != null)
					{
						await currentEvent.OnHitAsync(user, target, magic);
					}

					if (!target.IsAlive)
					{
						await role.KillAsync(target, GetDieMode());
					}
				}

				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				msg.Append(target.Identity, displayPower, true, atkResult.Effect, atkResult.ElementalDamage);
			}

			if (msg.Count > 0 || result.Roles.Count == 0)
			{
				await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 59 - Adrenaline Rush

		private async Task<bool> ProcessAdrenalineRushAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			if (user.UserPackage[Item.ItemPosition.LeftHand]?.IsPistol() != true)
			{
				return false;
			}

			if (magics.TryGetValue(EAGLE_EYE, out var eagleEye))
			{
				eagleEye.ResetDelay();
			}

			MsgMagicEffect reset = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level
			};
			reset.Append(role.Identity, (int)EAGLE_EYE, true);
			await role.BroadcastRoomMsgAsync(reset, true);
			return true;
		}

		#endregion

		#region 60 - Gale Bomb

		private async Task<bool> ProcessGaleBombAsync(Magic magic)
		{
			var setTarget = new List<Role>();
			(List<Role> Roles, Point Center) result = CollectTargetBomb(0, (int)magic.Range + role.SizeAddition);
			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = (ushort)result.Center.X,
				MapY = (ushort)result.Center.Y,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long battleExp = 0;
			int targets = 0;
			int maxTargets = (int)magic.Data;
			foreach (Role target in result.Roles.OrderBy(x => x.IsPlayer() ? 0 : 1))
			{
				if (targets >= maxTargets)
				{
					break;
				}
				if (target.IsWing || target is DynamicNpc)
				{
					continue;
				}

				int delta = target.BattlePower - role.BattlePower;
				if (delta > 0)
				{
					int chance = Math.Max(80, Math.Min(100, 100 - delta * 2));
					if (await NextAsync(100) > chance)
					{
						continue;
					}
				}

				await CheckCrimeAsync(target, magic);

				var atkResult = await BattleSystem.CalcPowerAsync(HitByMagic(magic), role, target, magic.Power);
				int damage = atkResult.Damage;
				damage += atkResult.ElementalDamage;

				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

				Point targetPoint = default;
				var ddaLine = new List<Point>();
				Calculations.DDALine(result.Center.X, result.Center.Y, target.X, target.Y, (int)magic.Data, ref ddaLine);
				foreach (var d in ddaLine)
				{
					if (!role.Map.IsStandEnable(d.X, d.Y))
					{
						break;
					}

					if (!role.Map.IsAltEnable(role.X, role.Y, d.X, d.Y))
					{
						break;
					}

					targetPoint = new Point(d.X, d.Y);
				}

				await target.JumpPosAsync(targetPoint.X, targetPoint.Y);

				await NpcServer.SendAsync(new MsgAiAction
				{
					Data = new MsgAiActionContract
					{
						Action = AiActionType.Jump,
						Identity = target.Identity,
						X = target.X,
						Y = target.Y,
						Direction = (int)target.Direction
					}
				});

				await target.ProcessOnMoveAsync();
				targets++;
				if (!target.IsAlive)
				{
					await role.KillAsync(target, GetDieMode());
				}
				else
				{
					if (target.MagicData.QueryMagic != null && target.MagicData.State == MagicState.Intone)
					{
						await target.MagicData.AbortMagicAsync(true);
					}
				}

				msg.Append(target.Identity, atkResult.Damage, true, atkResult.Effect, atkResult.ElementalDamage, target.X, target.Y);
			}

			await role.Map.BroadcastRoomMsgAsync(result.Center.X, result.Center.Y, msg);
			await AwardExpAsync(0, battleExp, AWARDEXP_BY_TIMES, magic);
			return true;
		}

		#endregion

		#region 61 - Dashed Dead Mark

		private async Task<bool> ProcessDashThickLineAsync(Magic magic)
		{
			int distance = (int)magic.Distance; // distance of the skill
			int range = (int)(magic.Range * 2); // wide
			if (magic.Type == BLADE_TEMPEST)
			{
				distance = (int)magic.StepSeconds;
				range = (int)(magic.StatusData2 * 2);
			}

			List<Point> testPosition = new();
			Calculations.DDALine(role.X, role.Y, targetPos.X, targetPos.Y, (int)Math.Min(distance, role.GetDistance(targetPos.X, targetPos.Y)), ref testPosition);
			bool endPositionOK = role.Map.IsStandEnable(targetPos.X, targetPos.Y) && role.Map.IsAltEnable(role.X, role.Y, targetPos.X, targetPos.Y) && role.IsJumpPass(targetPos.X, targetPos.Y, 70);
			targetPos = new Point(role.X, role.Y);

			foreach (var point in testPosition)
			{
				if (!role.Map.IsStandEnable(point.X, point.Y) && !endPositionOK)
				{
					break;
				}

				if (!role.Map.IsAltEnable(role.X, role.Y, point.X, point.Y))
				{
					break;
				}

				targetPos = new Point(point.X, point.Y);
			}

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y
			};

			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			var hitByMagic = HitByMagic(magic);
			List<Role> targets = CollectThickLine(range + role.SizeAddition);
			int exp = 0;
			long battleExp = 0;
			foreach (var target in targets)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
				}

				await CheckCrimeAsync(target, magic);

				var result = await BattleSystem.CalcPowerAsync(hitByMagic, role, target, magic.Power);
				int damage = result.Damage;
				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(damage, target.Life);

					await target.BeAttackAsync(HitByMagic(magic), role, result.Damage, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						exp += lifeLost;
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
						await role.KillAsync(target, GetDieMode());
					}
					else if (magic.Sort == MagicSort.Dashdeadmark)
					{
						if (magics.TryGetValue(BLACK_SPOT, out var blackSpot))
						{
							await target.SetDeadMarkAsync((int)blackSpot.StepSeconds);
						}
					}
				}
				else
				{
					damage = 0;
				}
				msg.Append(target.Identity, damage, true, result.Effect, damage);
			}

			await role.SendAsync(msg);
			await role.BroadcastRoomMsgAsync(new MsgInteract
			{
				Timestamp = Environment.TickCount,
				SenderIdentity = role.Identity,
				TargetIdentity = role.Identity,
				PosX = (ushort)targetPos.X,
				PosY = (ushort)targetPos.Y,
				Action = MsgInteractType.DashWhirl,
				MagicType = magic.Type,
				MagicLevel = magic.Level
			}, true);

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await role.JumpPosAsync(targetPos.X, targetPos.Y);
			if (user != null)
			{
				await user.Screen.UpdateAsync(msg);
			}
			else
			{
				await role.BroadcastRoomMsgAsync(msg, true);
			}
			await NpcServer.SendAsync(new MsgAiAction
			{
				Data = new MsgAiActionContract
				{
					Action = AiActionType.Jump,
					Identity = user.Identity,
					X = user.X,
					Y = user.Y,
					Direction = (int)user.Direction
				}
			});
			await role.ProcessOnMoveAsync();

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 65 - MortalDrag
		private async Task<bool> ProcessTargetdragAsyncAsync(Magic magic)
		{
			if (role is not Character user)
			{
				return false;
			}

			if (idTarget == 0)
			{
				logger.Warning($"No target for active bolt skill");
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long experience = 0,
				battleExp = 0;
			bool bMagic2Dealt = false;
			Dictionary<uint, Role> possibleTargets = user.Map.Query9BlocksByPos(user.X, user.Y)
				.DistinctBy(x => x.Identity)
				.ToDictionary(x => x.Identity);
			List<Role> setTarget = new();
			MagicType byMagic = HitByMagic(magic);
			Role target = possibleTargets.Values.FirstOrDefault(x => x.Identity == idTarget);

			if (user != null && (target is Character targetChar))
			{
				if (user.GetDistance(target) > magic.Distance)
					return false;

				if (user.BattlePower >= targetChar.BattlePower)
					return false;

				if (user.IsImmunity(target)
					|| !targetChar.IsAttackable(user))
				{
					return false;
				}

				var diference = targetChar.BattlePower - user.BattlePower;

				var newMaxAttack = targetChar.MaxAttack;

				for (int i = 0; i < diference; i++)
				{
					newMaxAttack = (int)(newMaxAttack - (newMaxAttack * 0.1));
				}

				await targetChar.AddAttributesAsync(ClientUpdateType.AddPhysicAttack, newMaxAttack);

				var result = await BattleSystem.CalcPowerAsync(byMagic, role, target, magic.Power);

				msg.Append(target.Identity, 0, true, result.Effect, result.ElementalDamage, hideDamage: true);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			await AwardExpAsync(battleExp, experience, false, magic);
			return true;
		}
		#endregion

		#region 67 - Close Scatter

		private async Task<bool> ProcessKinecticSparkActivationAsync(Magic magic)
		{
			MsgMagicEffect msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};
			if (role.QueryStatus(StatusSet.KINETIC_SPARK) != null)
			{
				await role.DetachStatusAsync(StatusSet.KINETIC_SPARK);
				msg.Append(role.Identity, 0, true);
			}
			else
			{
				await role.AttachStatusAsync(StatusSet.KINETIC_SPARK, 0, int.MaxValue, 0, null);
				msg.Append(role.Identity, 0, true);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			return true;
		}

		public async Task<bool> ProcessKinecticSparkAsync(Magic magic)
		{
			if (role.QueryStatus(StatusSet.KINETIC_SPARK) == null)
			{
				return false;
			}

			if (await ChanceCalcAsync(magic.Percent))
			{
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = role.X,
				MapY = role.Y,
				NextMagic = (ushort)magic.NextMagic,
				MagicSoul = magic.CurrentEffectType
			};

			long experience = 0,
				battleExp = 0;
			bool bMagic2Dealt = false;
			Dictionary<uint, Role> possibleTargets = role.Map.Query9BlocksByPos(role.X, role.Y)
				.DistinctBy(x => x.Identity)
				.ToDictionary(x => x.Identity);
			List<Role> setTarget = new();
			MagicType byMagic = HitByMagic(magic);
			Role target = possibleTargets.Values.FirstOrDefault(x => x.Identity == idTarget);

			var CollectNewTarget = () =>
			{
				Role oldTarget = target;
				return possibleTargets.Values
					.Where(x => x.Identity != role.Identity)
					.MinBy(x => x.GetDistance(oldTarget));
			};

			Role distanceCheck = role;
			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			for (int i = 0; i < magic.ActiveTimes && possibleTargets.Count > 0 && target != null;)
			{
				possibleTargets.Remove(target?.Identity ?? 0);

				if (target == null
					|| (i != 0 && distanceCheck.GetDistance(target) > magic.Range)
					|| role.IsImmunity(target)
					|| !target.IsAttackable(role))
				{
					target = CollectNewTarget();
					continue;
				}

				await CheckCrimeAsync(target);

				var result = await BattleSystem.CalcPowerAsync(byMagic, role, target, magic.Power);
				int damage = (int)(result.Damage * Math.Max(0.2d, 1 - (0.2d * i))) + result.ElementalDamage;

				if (!await target.CheckScapegoatAsync(role))
				{
					var lifeLost = (int)Math.Min(target.Life, damage);
					await target.BeAttackAsync(byMagic, role, lifeLost, true);

					if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
					{
						experience += lifeLost;
						battleExp += user.AdjustExperience(target, lifeLost, false);
						if (!target.IsAlive)
						{
							var nBonusExp = (int)(target.MaxLife * 20 / 100d);

							if (user.Team != null)
							{
								await user.Team.AwardMemberExpAsync(user.Identity, target, nBonusExp);
							}

							experience += user.AdjustExperience(target, nBonusExp, false);
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
						await role.KillAsync(target, GetDieMode());
					}
				}

				if (!bMagic2Dealt && await ChanceCalcAsync(5d) && user != null)
				{
					await user.SendWeaponMagic2Async(target);
					bMagic2Dealt = true;
				}

				msg.Append(target.Identity, damage, true, result.Effect, result.ElementalDamage);

				if (i + 1 < magic.ActiveTimes)
				{
					distanceCheck = target;
					target = CollectNewTarget();
				}
				i++;
			}

			if (currentEvent != null)
			{
				await currentEvent.OnAttackAsync(user);
			}

			await role.BroadcastRoomMsgAsync(msg, true);
			await AwardExpAsync(battleExp, AWARDEXP_BY_TIMES, false, magic);
			return true;
		}

		#endregion

		#region 70 - BreathFocus

		private async Task<bool> ProcessBreathFocusAsync(Magic magic)
		{
			if (role is not Character || !role.IsAlive)
			{
				return false;
			}

			var msg = new MsgMagicEffect
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level
			};
			msg.Append(role.Identity, magic.Power, true, 0, 0, 0, 0, true);
			await role.BroadcastRoomMsgAsync(msg, true);

			await role.AddAttributesAsync(ClientUpdateType.Stamina, magic.Power);
			await AwardExpAsync(0, 0, magic.Power, magic);
			return true;
		}

		#endregion

		#region 71 - Fatal Cross

		private async Task<bool> ProcessFatalCrossAsync(Magic magic)
		{
			int range = (int)magic.Data;
			int width = (int)(magic.Range * 2);

			List<Role> targets = CollectThickLine(range, width);

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y
			};

			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			var hitByMagic = HitByMagic(magic);
			int exp = 0;
			long battleExp = 0;
			bool broadcast = true;
			bool miss = true;
			Dictionary<uint, int> setDamage = new Dictionary<uint, int>();
			foreach (var target in targets)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
					broadcast = false;
				}

				if (await target.CheckScapegoatAsync(role))
				{
					continue;
				}

				miss = false;
				await CheckCrimeAsync(target, magic);

				var result = await BattleSystem.CalcPowerAsync(hitByMagic, role, target, magic.Power);
				int damage = result.Damage + result.ElementalDamage;

				setDamage.TryAdd(target.Identity, damage);
				msg.Append(target.Identity, damage, true, result.Effect, result.Damage);
			}

			if (broadcast || msg.Count > 0)
			{
				await role.BroadcastRoomMsgAsync(msg, true);
			}

			foreach (var target in targets)
			{
				if (!setDamage.TryGetValue(target.Identity, out var damage))
				{
					continue;
				}

				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

				if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
				{
					exp += lifeLost;
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
					await role.KillAsync(target, GetDieMode());
				}
			}

			if (user != null)
			{
				if (miss)
				{
					await user.ProcessMagicAttackAsync((ushort)BREATH_FOCUS, user.Identity, user.X, user.Y, (uint)AutoActive.OnAttack);
				}
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#region 73 - Fatal Spin

		private async Task<bool> ProcessFatalSpinAsync(Magic magic)
		{
			int distance = (int)magic.Distance; // distance of the skill
			int range = (int)(magic.Range * 2 + 1); // wide

			MsgMagicEffect msg = new()
			{
				AttackerIdentity = role.Identity,
				MagicIdentity = magic.Type,
				MagicLevel = magic.Level,
				MapX = (ushort)targetPos.X,
				MapY = (ushort)targetPos.Y
			};

			List<Role> targets = CollectThickLine(distance, range);
			Dictionary<uint, int> setDamage = new Dictionary<uint, int>();

			Character user = role as Character;
			var currentEvent = user?.GetCurrentEvent();
			var hitByMagic = HitByMagic(magic);
			int exp = 0;
			long battleExp = 0;
			bool broadcast = true;
			foreach (var target in targets)
			{
				if (msg.Count >= MAX_TARGET_NUM)
				{
					await role.BroadcastRoomMsgAsync(msg, true);
					msg.ClearTargets();
					broadcast = false;
				}

				if (await target.CheckScapegoatAsync(role))
				{
					continue;
				}

				await CheckCrimeAsync(target, magic);

				int power = magic.Power;
				if (!target.IsPlayer())
				{
					power = (int)magic.StatusData0;
				}

				var result = await BattleSystem.CalcPowerAsync(hitByMagic, role, target, power);
				int damage = result.Damage + result.ElementalDamage;

				setDamage.TryAdd(target.Identity, damage);
				msg.Append(target.Identity, damage, true, result.Effect, result.Damage);
			}

			if (broadcast || msg.Count > 0)
			{
				await role.BroadcastRoomMsgAsync(msg, true);
			}

			foreach (var target in targets)
			{
				if (!setDamage.TryGetValue(target.Identity, out var damage))
				{
					continue;
				}

				var lifeLost = (int)Math.Min(damage, target.Life);
				await target.BeAttackAsync(HitByMagic(magic), role, damage, true);

				if (user != null && (target is Monster monster && monster.SpeciesType == 0 && !monster.IsGuard() && !monster.IsPkKiller() && !monster.IsRighteous() || (target as DynamicNpc)?.IsGoal() == true))
				{
					exp += lifeLost;
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
					await role.KillAsync(target, GetDieMode());
				}
			}

			await AwardExpAsync(0, battleExp, exp, magic);
			return true;
		}

		#endregion

		#endregion

		#region Collect Targets

		private (List<Role> Roles, Point Center) CollectTargetBomb(int nLockType, int nRange)
		{
			var targets = new List<Role>();

			var center = new Point(targetPos.X, targetPos.Y);
			if (idTarget != 0)
			{
				Role target = role.Map.QueryAroundRole(role, idTarget);
				if (target != null)
				{
					center.X = target.X;
					center.Y = target.Y;
				}
			}
			else if (QueryMagic?.Target == 4)
			{

			}
			else if (QueryMagic?.Ground == 1)
			{
				center.X = role.X;
				center.Y = role.Y;
			}
			else if (QueryMagic?.Target == 2)
			{
				center.X = role.X;
				center.Y = role.Y;
			}

			List<Role> setRoles = role.Map.Query9BlocksByPos(center.X, center.Y);
			foreach (Role target in setRoles)
			{
				if (target.Identity == role.Identity)
				{
					continue;
				}

				if (target.GetDistance(center.X, center.Y) > nRange + target.SizeAddition)
				{
					continue;
				}

				if (role.IsImmunity(target) || !target.IsAttackable(role))
				{
					continue;
				}

				if (target.IsWing)
				{
					continue;
				}

				targets.Add(target);
			}

			return (targets, center);
		}

		private List<Role> CollectThickLine(int range)
		{
			List<Role> targets = new();
			List<Point> points = Bresenham.CalculateThick(role.X, role.Y, targetPos.X, targetPos.Y, range);
			List<Role> setRoles = role.Map.Query9BlocksByPos(role.X, role.Y);
			foreach (Point point in points)
			{
				Role target = setRoles.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
				if (target == null || target.Identity == role.Identity)
				{
					continue;
				}

				if (role.IsImmunity(target) || !target.IsAttackable(role))
				{
					continue;
				}

				if (target.IsWing)
				{
					continue;
				}

				targets.Add(target);
			}
			return targets.DistinctBy(x => x.Identity).ToList();
		}

		private List<Role> CollectThickLine(int range, int width)
		{
			List<Point> ddaLine = new List<Point>();
			Calculations.DDALine(role.X, role.Y, targetPos.X, targetPos.Y, range, ref ddaLine);

			Point finalPoint = ddaLine.LastOrDefault();
			List<Role> targets = new();
			List<Point> points = Bresenham.CalculateThick(role.X, role.Y, finalPoint.X, finalPoint.Y, width);
			List<Role> setRoles = role.Map.Query9BlocksByPos(role.X, role.Y);
			foreach (Point point in points)
			{
				Role target = setRoles.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
				if (target == null || target.Identity == role.Identity)
				{
					continue;
				}

				if (role.IsImmunity(target) || !target.IsAttackable(role))
				{
					continue;
				}

				if (target.IsWing)
				{
					continue;
				}

				targets.Add(target);
			}

			if (idTarget != 0 && targets.All(x => x.Identity != idTarget))
			{
				Role mainTarget = setRoles.FirstOrDefault(x => x.Identity == idTarget);
				if (mainTarget != null)
				{
					targets.Add(mainTarget);
				}
			}

			return targets.DistinctBy(x => x.Identity).ToList();
		}

		#endregion

		#region Common Checks

		public bool IsImmunity(Magic magic, Role target)
		{
			if (!target.IsAttackable(role))
			{
				return true;
			}

			if (magic.Sort == MagicSort.Declife && target is Monster monster && monster.SpeciesType != 0)
			{
				return true;
			}

			if (target.IsWing
				&& !role.IsWing
				&& magic.WeaponHit != 0
				&& magic.WeaponSubtype != 500)
			{
				return true;
			}

			return role.IsImmunity(target);
		}

		public bool IsWeaponMagic(ushort type)
		{
			return type >= 10000 && type < 10256;
		}

		public MagicType HitByMagic(Magic magic)
		{
			// 0 none, 1 normal, 2 xp
			if (magic == null)
			{
				return 0;
			}

			if (magic.WeaponHit == 0)
			{
				return magic.UseXp == MagicType.XpSkill
						   ? MagicType.XpSkill
						   : MagicType.Normal;
			}

			if (role is Character pRole)
			{
				if (pRole.UserPackage[Item.ItemPosition.RightHand] != null && magic.WeaponHit == 2 &&
					pRole.UserPackage[Item.ItemPosition.RightHand].Itemtype.MagicAtk > 0)
				{
					return magic.UseXp == MagicType.XpSkill
							   ? MagicType.XpSkill
							   : MagicType.Normal;
				}
			}

			return MagicType.None;
		}

		public uint GetDieMode()
		{
			return (uint)(HitByMagic(QueryMagic) > 0 ? 3 : role.IsBowman ? 5 : 1);
		}

		public bool HitByWeapon()
		{
			Magic magic = QueryMagic;
			if (magic == null)
			{
				return true;
			}

			if (magic.WeaponHit == 1)
			{
				return true;
			}

			Item item;
			if (role is Character character
				&& (item = character.UserPackage[Item.ItemPosition.RightHand]) != null
				&& item.Itemtype.MagicAtk <= 0)
			{
				return true;
			}

			return false;
		}

		#endregion

		#region Magic Processing Manage

		public MagicState State { get; private set; } = MagicState.None;

		private void ResetDelay()
		{
			if (!magics.TryGetValue(useMagicType, out Magic magic))
			{
				return;
			}

			State = MagicState.Delay;
			delayTimer.Update();
			magic.SetDelay();
		}

		private void SetAutoAttack(ushort type)
		{
			useMagicType = type;
			autoAttack = true;
		}

		private void BreakAutoAttack()
		{
			useMagicType = 0;
			autoAttack = false;
		}

		public bool IsAutoAttack()
		{
			return autoAttack && useMagicType != 0;
		}

		#endregion

		#region Abort Magic

		public async Task<bool> AbortMagicAsync(bool bSynchro)
		{
			BreakAutoAttack();

			if (State == MagicState.Intone)
			{
				intoneTimer.Clear();
			}

			State = MagicState.None;

			if (bSynchro && role is Character)
			{
				await role.SendAsync(new MsgAction
				{
					Identity = role.Identity,
					Action = ActionType.AbortMagic
				});
			}

			return true;
		}

		#endregion

		#region Crime

		public async Task<bool> CheckCrimeAsync(Role role)
		{
			return await this.role.CheckCrimeAsync(role);
		}

		public async Task<bool> CheckCrimeAsync(Role pRole, Magic magic)
		{
			if (pRole == null || magic == null)
			{
				return false;
			}

			if (magic.Crime <= 0)
			{
				return false;
			}

			return await role.CheckCrimeAsync(pRole);
		}

		public async Task<bool> CheckCrimeAsync(Dictionary<uint, Role> pRoleSet, Magic magic)
		{
			if (pRoleSet == null || magic == null)
			{
				return false;
			}

			if (magic.Crime <= 0)
			{
				return false;
			}

			foreach (Role pRole in pRoleSet.Values)
			{
				if (role.Identity != pRole.Identity && await role.CheckCrimeAsync(pRole))
				{
					return true;
				}
			}

			return false;
		}

		#endregion

		#region On Timer

		public async Task OnTimerAsync()
		{
			if (!Magics.TryGetValue(useMagicType, out Magic magic))
			{
				State = MagicState.None;
				return;
			}

			switch (State)
			{
				case MagicState.Intone: // intone
					{
						if (intoneTimer != null && !intoneTimer.IsTimeOut())
						{
							return;
						}

						if (intoneTimer != null && intoneTimer.IsTimeOut() && !await LaunchAsync(magic, AutoActive.None))
						{
							ResetDelay();
						}

						State = MagicState.None;

						if (IsAutoAttack())
						{
							State = MagicState.Delay;
							delayTimer.Startup(Math.Max(MAGIC_DELAY, magic.DelayMs));
						}

						break;
					}

				case MagicState.Delay: // delay
					{
						if ((role.Map.IsTrainingMap() || IsAutoAttack())
							&& delayTimer.IsActive()
							&& magic.Sort != MagicSort.Atkstatus)
						{
							if (delayTimer.IsTimeOut())
							{
								State = MagicState.None;
								if (!await role.ProcessMagicAttackAsync(magic.Type, idTarget, (ushort)targetPos.X,
																		  (ushort)targetPos.Y))
								{
									State = MagicState.Delay;
								}
							}

							return;
						}

						if (!delayTimer.IsActive())
						{
							State = MagicState.None;
							await AbortMagicAsync(true);
							return;
						}

						if (autoAttack && delayTimer.IsTimeOut())
						{
							if (delayTimer.IsActive() && !delayTimer.TimeOver())
							{
								return;
							}

							State = MagicState.None;
							await role.ProcessMagicAttackAsync(magic.Type, idTarget, (ushort)targetPos.X,
																 (ushort)targetPos.Y);

							if (idTarget != 0 && role.Map.QueryAroundRole(role, idTarget)?.IsPlayer() == true)
							{
								await AbortMagicAsync(false);
							}
						}

						if (delayTimer.IsActive() && delayTimer.TimeOver())
						{
							State = MagicState.None;
							await AbortMagicAsync(false);
						}

						break;
					}
			}
		}

		#endregion

		public enum MagicState
		{
			None = 0,
			Intone = 1,
			Delay = 2
		}


		private const uint TWOFOLDBLADES_ID = 6000;
		private const uint SUPERTWOFOLDBLADES_ID = 12080;
		private const uint GAPING_WOUNDS = 11230;
		private const uint BREATH_FOCUS = 11960;

		/// <summary>
		/// Passive skill. When receiving damage, you have a 25% chance to end the cooldown of Eagle Eye.
		/// </summary>
		private const uint ADRENALINE_RUSH = 11130;
		/// <summary>
		/// Passive skill. Have a 25% chance to attach Black Spots on opponents for 8 seconds after causing damage. Cast Eagle Eye on opponents with Black Spots will trigger Adrenaline Rush effect. This status can`t be removed.
		/// </summary>
		private const uint BLACK_SPOT = 11120;
		/// <summary>
		/// Stab all opponents within 10 paces (square area) to cause 90% of normal attack and attach Black Spots (if learnt), consuming 20 SPs. Reset the cooldown of Eagle Eye. Rapier should be equipped.
		/// </summary>
		private const uint BLADE_TEMPEST = 11110;
		/// <summary>
		/// Cause 20% of normal damage, requiring 20 seconds to cool down. If the opponent was attached with Black Spot, the cooldown of Eagle Eye will be reset. Pistol should be equipped. Damage will increase as it upgrades.
		/// </summary>
		private const uint EAGLE_EYE = 11030;
	}
}
