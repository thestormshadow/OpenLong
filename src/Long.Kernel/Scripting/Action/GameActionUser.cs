using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.Npcs;
using Long.Kernel.States.Status;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared.Helpers;
using Newtonsoft.Json;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;
using static Long.Kernel.Network.Game.Packets.MsgName;
using static Long.Kernel.States.User.Character;

namespace Long.Kernel.Scripting.Action
{
    public partial class GameAction
    {
        private static async Task<bool> ExecuteUserAttrAsync(DbAction action, string param, Character user, Role role,
                                                             Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] parsedParam = SplitParam(param);
            if (parsedParam.Length < 3)
            {
                logger.Warning("GameAction::ExecuteUserAttr[{0}] invalid param num {1}", action.Id, param);
                return false;
            }

            string type = "", opt = "", value = "", last = "";
            type = parsedParam[0];
            opt = parsedParam[1];
            value = parsedParam[2];
            if (parsedParam.Length > 3)
            {
                last = parsedParam[3];
            }

            switch (type.ToLower())
            {
                #region Force (>, >=, <, <=, =, +=, set)

                case "force":
                case "strength":
                    int forceValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Strength > forceValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Strength >= forceValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Strength < forceValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Strength <= forceValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Strength == forceValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Strength, forceValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Strength, (ulong)forceValue);
                        return true;
                    }

                    break;

                #endregion

                #region Speed (>, >=, <, <=, =, +=, set)

                case "agility":
                case "speed":
                    int speedValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Speed > speedValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Speed >= speedValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Speed < speedValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Speed <= speedValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Speed == speedValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Agility, speedValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Agility, (ulong)speedValue);
                        return true;
                    }

                    break;

                #endregion

                #region Health (>, >=, <, <=, =, +=, set)

                case "vitality":
                case "health":
                    int healthValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Vitality > healthValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Vitality >= healthValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Vitality < healthValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Vitality <= healthValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Vitality == healthValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Vitality, healthValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Vitality, (ulong)healthValue);
                        return true;
                    }

                    break;

                #endregion

                #region Soul (>, >=, <, <=, =, +=, set)

                case "spirit":
                case "soul":
                    int soulValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Spirit > soulValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Spirit >= soulValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Spirit < soulValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Spirit <= soulValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Spirit == soulValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Spirit, soulValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Spirit, (ulong)soulValue);
                        return true;
                    }

                    break;

                #endregion

                #region Attribute Points (>, >=, <, <=, =, +=, set)

                case "attr_points":
                case "attr":
                    int attrValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.AttributePoints > attrValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.AttributePoints >= attrValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.AttributePoints < attrValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.AttributePoints <= attrValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.AttributePoints == attrValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Atributes, attrValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Atributes, (ulong)attrValue);
                        return true;
                    }

                    break;

                #endregion

                #region Level (>, >=, <, <=, =, +=, set)

                case "level":
                    int levelValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Level > levelValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Level >= levelValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Level < levelValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Level <= levelValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Level == levelValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Level, levelValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Level, (ulong)levelValue);
                        return true;
                    }

                    break;

                #endregion

                #region Metempsychosis (>, >=, <, <=, =, +=, set)

                case "metempsychosis":
                    int metempsychosisValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Metempsychosis > metempsychosisValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Metempsychosis >= metempsychosisValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Metempsychosis < metempsychosisValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Metempsychosis <= metempsychosisValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.Metempsychosis == metempsychosisValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddAttributesAsync(ClientUpdateType.Reborn, metempsychosisValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetAttributesAsync(ClientUpdateType.Reborn, (ulong)metempsychosisValue);
                        return true;
                    }

                    break;

                #endregion

                #region Money (>, >=, <, <=, =, +=, set)

                case "money":
                case "silver":
                    {
                        var moneyValue = long.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Silvers > (ulong)moneyValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Silvers >= (ulong)moneyValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Silvers < (ulong)moneyValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Silvers <= (ulong)moneyValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.Silvers == (ulong)moneyValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.ChangeMoneyAsync(moneyValue);
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Money, (ulong)moneyValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Emoney (>, >=, <, <=, =, +=, set)

                case "emoney":
                case "e_money":
                case "cps":
                    {
                        long emoneyValue = long.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.ConquerPoints > emoneyValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.ConquerPoints >= emoneyValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.ConquerPoints < emoneyValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.ConquerPoints <= emoneyValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.ConquerPoints == emoneyValue;
                        }

                        if (opt.Equals("+="))
                        {
                            EmoneyOperationType op = EmoneyOperationType.None;
                            if (role is BaseNpc)
                            {
                                op = EmoneyOperationType.Npc;
                            }
                            else if (role is Monster)
                            {
                                op = EmoneyOperationType.Monster;
                            }
                            else if (item != null)
                            {
                                op = EmoneyOperationType.Item;
                            }
                            if (await user.ChangeConquerPointsAsync((int)emoneyValue))
                            {
                                await user.SaveEmoneyLogAsync(op, 0, 0, emoneyValue);
                                return true;
                            }
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.ConquerPoints, (ulong)emoneyValue);
                            return true;
                        }

                        break;
                    }
                #endregion

                #region Emoney Bound (>, >=, <, <=, =, +=, set)

                case "e_money_mono":
                    {
                        int emoneyValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.ConquerPointsBound > emoneyValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.ConquerPointsBound >= emoneyValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.ConquerPointsBound < emoneyValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.ConquerPointsBound <= emoneyValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.ConquerPointsBound == emoneyValue;
                        }

                        if (opt.Equals("+="))
                        {
                            EmoneyOperationType op = EmoneyOperationType.None;
                            uint targetId = 0;
                            if (role is BaseNpc)
                            {
                                op = EmoneyOperationType.Npc;
                                targetId = role.Identity;
                            }
                            else if (role is Monster mob)
                            {
                                op = EmoneyOperationType.Monster;
                                targetId = mob.Type;
                            }
                            else if (item != null)
                            {
                                op = EmoneyOperationType.Item;
                                targetId = item.Identity;
                            }
                            if (await user.ChangeBoundConquerPointsAsync(emoneyValue))
                            {
                                await user.SaveEmoneyMonoLogAsync(op, targetId, 0, (uint)emoneyValue);
                                return true;
                            }
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.BoundConquerPoints, (ulong)emoneyValue);
                            return true;
                        }

                        break;
                    }
                #endregion

                #region Rankshow (>, >=, <, <=, =)

                case "rank":
                case "rankshow":
                case "rank_show":
                    {
                        int rankShowValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.SyndicateRank > (ISyndicateMember.SyndicateRank)rankShowValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.SyndicateRank >= (ISyndicateMember.SyndicateRank)rankShowValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.SyndicateRank < (ISyndicateMember.SyndicateRank)rankShowValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.SyndicateRank <= (ISyndicateMember.SyndicateRank)rankShowValue;
                        }

                        if (opt.Equals("==") || opt.Equals("="))
                        {
                            return user.SyndicateRank == (ISyndicateMember.SyndicateRank)rankShowValue;
                        }

                        break;
                    }

                #endregion

                #region Syn User Time (>, >=, <, <=, =)

                case "syn_user_time":
                    {
                        int synTime = int.Parse(value);
                        if (user.Syndicate == null)
                        {
                            return false;
                        }

                        var synDays = (int)(DateTime.Now - user.SyndicateMember.JoinDate).TotalDays;
                        if (opt.Equals("==") || opt.Equals("="))
                        {
                            return synDays == synTime;
                        }

                        if (opt.Equals(">="))
                        {
                            return synDays >= synTime;
                        }

                        if (opt.Equals(">"))
                        {
                            return synDays > synTime;
                        }

                        if (opt.Equals("<="))
                        {
                            return synDays <= synTime;
                        }

                        if (opt.Equals("<"))
                        {
                            return synDays < synTime;
                        }

                        break;
                    }

                #endregion

                #region Experience (>, >=, <, <=, =, +=, set)

                case "exp":
                case "experience":
                    {
                        ulong expValue = ulong.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Experience > expValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Experience >= expValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Experience < expValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Experience <= expValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.Experience == expValue;
                        }

                        if (opt.Equals("+="))
                        {
                            if (user.Map != null && user.Map.IsNoExpMap())
                            {
                                return true;
                            }

                            return await user.AwardExperienceAsync((long)expValue, last.Equals("nocontribute"));
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Experience, expValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Stamina (>, >=, <, <=, =, +=, set)

                case "ep":
                case "energy":
                case "stamina":
                    {
                        int energyValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Energy > energyValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Energy >= energyValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Energy < energyValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Energy <= energyValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.Energy == energyValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.AddAttributesAsync(ClientUpdateType.Stamina, energyValue);
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Stamina, (ulong)energyValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Life (>, >=, <, <=, =, +=, set)

                case "life":
                case "hp":
                    {
                        int lifeValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Life > lifeValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Life >= lifeValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Life < lifeValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Life <= lifeValue;
                        }

                        if (opt.Equals("=="))
                        {
                            return user.Life == lifeValue;
                        }

                        if (opt.Equals("+="))
                        {
                            user.QueueAction(async () =>
                            {
                                await user.AddAttributesAsync(ClientUpdateType.Hitpoints, lifeValue);
                                //    if (!user.IsAlive)
                                //        await user.BeKillAsync(null);
                            });
                            return true;
                        }

                        if (opt.Equals("set") || opt.Equals("="))
                        {
                            user.QueueAction(async () =>
                            {
                                await user.SetAttributesAsync(ClientUpdateType.Hitpoints, (ulong)lifeValue);
                                //if (!user.IsAlive)
                                //    await user.BeKillAsync(null);
                            });
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Mana (>, >=, <, <=, =, +=, set)

                case "mana":
                case "mp":
                    {
                        int manaValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Mana > manaValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Mana >= manaValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Mana < manaValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Mana <= manaValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.Mana == manaValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.AddAttributesAsync(ClientUpdateType.Mana, manaValue);
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Mana, (ulong)manaValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Pk (>, >=, <, <=, =, +=, set)

                case "pk":
                case "pkp":
                    {
                        int pkValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.PkPoints > pkValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.PkPoints >= pkValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.PkPoints < pkValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.PkPoints <= pkValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.PkPoints == pkValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.AddAttributesAsync(ClientUpdateType.PkPoints, pkValue);
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.PkPoints, (ulong)pkValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Profession (>, >=, <, <=, =, +=, set)

                case "profession":
                case "pro":
                    {
                        int proValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.Profession > proValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.Profession >= proValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.Profession < proValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.Profession <= proValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.Profession == proValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.AddAttributesAsync(ClientUpdateType.Class, proValue);
                        }

                        if (opt.Equals("set"))
                        {
                            await user.SetAttributesAsync(ClientUpdateType.Class, (ulong)proValue);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region First Profession (>, >=, <, <=, =)

                case "first_prof":
                    {
                        int proValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.FirstProfession > proValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.FirstProfession >= proValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.FirstProfession < proValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.FirstProfession <= proValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.FirstProfession == proValue;
                        }

                        break;
                    }

                #endregion

                #region Last Profession (>, >=, <, <=, =)

                case "old_prof":
                    {
                        int proValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.PreviousProfession > proValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.PreviousProfession >= proValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.PreviousProfession < proValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.PreviousProfession <= proValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.PreviousProfession == proValue;
                        }

                        break;
                    }

                #endregion

                #region Transform (>, >=, <, <=, =, ==)

                case "transform":
                    int transformValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.TransformationMesh > transformValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.TransformationMesh >= transformValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.TransformationMesh < transformValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.TransformationMesh <= transformValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.TransformationMesh == transformValue;
                    }

                    break;

                #endregion

                #region Virtue (>, >=, <, <=, =, +=, set)

                case "virtue":
                case "vp":
                    int virtueValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.VirtuePoints > virtueValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.VirtuePoints >= virtueValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.VirtuePoints < virtueValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.VirtuePoints <= virtueValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.VirtuePoints == virtueValue;
                    }

                    if (opt.Equals("+="))
                    {
                        if (virtueValue > 0)
                        {
                            user.VirtuePoints += (uint)virtueValue;
                        }
                        else
                        {
                            user.VirtuePoints -= (uint)(virtueValue * -1);
                        }

                        await user.SaveAsync();
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        user.VirtuePoints = (uint)virtueValue;
                        await user.SaveAsync();
                        return true;
                    }

                    break;

                #endregion

                #region Vip (>, >=, <, <=, =, ==)

                case "vip":
                    {
                        int vipValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.VipLevel > vipValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.VipLevel >= vipValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.VipLevel < vipValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.VipLevel <= vipValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.VipLevel == vipValue;
                        }

                        break;
                    }

                #endregion

                #region Xp (>, >=, <, <=, =, +=, set)

                case "xp":
                    int xpValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.XpPoints > xpValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.XpPoints >= xpValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.XpPoints < xpValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.XpPoints <= xpValue;
                    }

                    if (opt.Equals("=") || opt.Equals("=="))
                    {
                        return user.XpPoints == xpValue;
                    }

                    if (opt.Equals("+="))
                    {
                        await user.AddXpAsync((byte)xpValue);
                        return true;
                    }

                    if (opt.Equals("set"))
                    {
                        await user.SetXpAsync((byte)xpValue);
                        return true;
                    }

                    break;

                #endregion

                #region Iterator (>, >=, <, <=, =, +=, set)

                case "iterator":
                    int iteratorValue = int.Parse(value);
                    if (opt.Equals(">"))
                    {
                        return user.Iterator > iteratorValue;
                    }

                    if (opt.Equals(">="))
                    {
                        return user.Iterator >= iteratorValue;
                    }

                    if (opt.Equals("<"))
                    {
                        return user.Iterator < iteratorValue;
                    }

                    if (opt.Equals("<="))
                    {
                        return user.Iterator <= iteratorValue;
                    }

                    if (opt.Equals("=="))
                    {
                        return user.Iterator == iteratorValue;
                    }

                    if (opt.Equals("+="))
                    {
                        user.Iterator += iteratorValue;
                        return true;
                    }

                    if (opt.Equals("set") || opt == "=")
                    {
                        user.Iterator = iteratorValue;
                        return true;
                    }

                    break;

                #endregion

                #region Merchant (==, set)

                case "business":
                    int merchantValue = int.Parse(value);
                    if (opt.Equals("=="))
                    {
                        return user.Merchant == merchantValue;
                    }

                    if (opt.Equals("set") || opt == "=")
                    {
                        if (merchantValue == 0)
                        {
                            await user.RemoveMerchantAsync();
                        }
                        else
                        {
                            await user.SetMerchantAsync();
                        }

                        return true;
                    }

                    break;

                #endregion

                #region Look (==, set)

                case "look":
                    {
                        switch (opt)
                        {
                            case "==": return user.Mesh % 10 == ushort.Parse(value);
                            case "set":
                                {
                                    ushort usVal = ushort.Parse(value);
                                    if (user.Gender == 1 && (usVal == 3 || usVal == 4))
                                    {
                                        user.Body = (BodyType)(1000 + usVal);
                                        await user.SynchroAttributesAsync(ClientUpdateType.Mesh, user.Mesh, true);
                                        await user.SaveAsync();
                                        return true;
                                    }

                                    if (user.Gender == 2 && (usVal == 1 || usVal == 2))
                                    {
                                        user.Body = (BodyType)(2000 + usVal);
                                        await user.SynchroAttributesAsync(ClientUpdateType.Mesh, user.Mesh, true);
                                        await user.SaveAsync();
                                        return true;
                                    }

                                    return false;
                                }
                        }

                        return false;
                    }

                #endregion

                #region Body (set)

                case "body":
                    {
                        switch (opt)
                        {
                            case "set":
                                {
                                    ushort usNewBody = ushort.Parse(value);
                                    if (usNewBody == 1003 || usNewBody == 1004)
                                    {
                                        if (user.Body != BodyType.AgileFemale && user.Body != BodyType.MuscularFemale)
                                        {
                                            return false; // to change body use the fucking item , asshole
                                        }
                                    }

                                    if (usNewBody == 2001 || usNewBody == 2002)
                                    {
                                        if (user.Body != BodyType.AgileMale && user.Body != BodyType.MuscularMale)
                                        {
                                            return false; // to change body use the fucking item , asshole
                                        }
                                    }

                                    if (user.UserPackage[Item.ItemPosition.Garment] != null)
                                    {
                                        await user.UserPackage.UnEquipAsync(Item.ItemPosition.Garment);
                                    }

                                    user.Body = (BodyType)usNewBody;
                                    await user.SynchroAttributesAsync(ClientUpdateType.Mesh, user.Mesh, true);
                                    await user.SaveAsync();
                                    return true;
                                }
                        }

                        return false;
                    }

                #endregion

                #region Sex

                case "sex":
                    {
                        switch (opt)
                        {
                            case "set":
                                {
                                    BodyType newBody = user.Body;
                                    ushort usNewBody = ushort.Parse(value);
                                    if (usNewBody == 1)
                                    {
                                        if (user.Body != BodyType.AgileFemale && user.Body != BodyType.MuscularFemale)
                                        {
                                            return false;
                                        }

                                        if (user.Body == BodyType.AgileFemale)
                                        {
                                            newBody = BodyType.AgileMale;
                                        }
                                        else if (user.Body == BodyType.MuscularFemale)
                                        {
                                            newBody = BodyType.MuscularMale;
                                        }
                                    }

                                    if (usNewBody == 2)
                                    {
                                        if (user.Body != BodyType.AgileMale && user.Body != BodyType.MuscularMale)
                                        {
                                            return false;
                                        }

                                        if (user.Body == BodyType.AgileMale)
                                        {
                                            newBody = BodyType.AgileFemale;
                                        }
                                        else if (user.Body == BodyType.MuscularMale)
                                        {
                                            newBody = BodyType.MuscularFemale;
                                        }
                                    }

                                    if (user.UserPackage[Item.ItemPosition.Garment] != null)
                                    {
                                        await user.UserPackage.UnEquipAsync(Item.ItemPosition.Garment);
                                    }

                                    user.Body = newBody;
                                    await user.SynchroAttributesAsync(ClientUpdateType.Mesh, user.Mesh, true);
                                    await user.SaveAsync();
                                    return true;
                                    ;
                                }
                        }
                        return false;
                    }

                #endregion

                #region Cultivation

                case "cultivation":
                    {
                        int cultivationValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.StudyPoints > cultivationValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.StudyPoints >= cultivationValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.StudyPoints < cultivationValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.StudyPoints <= cultivationValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.StudyPoints == cultivationValue;
                        }

                        if (opt.Equals("+="))
                        {
                            return await user.ChangeCultivationAsync(cultivationValue);
                        }

                        break;
                    }

                #endregion

                #region Strength Value

                case "strengthvalue":
                    {
                        int strengthValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.ChiPoints > strengthValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.ChiPoints >= strengthValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.ChiPoints < strengthValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.ChiPoints <= strengthValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.ChiPoints == strengthValue;
                        }

                        if (opt.Equals("+="))
                        {
                            //    return await user.ChangeStrengthValueAsync(strengthValue);
                            return true;
                        }
                        break;
                    }

                #endregion

                #region Mentor

                case "mentor":
                    {
                        int mentorValue = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.EnlightenPoints > mentorValue;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.EnlightenPoints >= mentorValue;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.EnlightenPoints < mentorValue;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.EnlightenPoints <= mentorValue;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.EnlightenPoints == mentorValue;
                        }

                        if (opt.Equals("+="))
                        {
                            if (mentorValue > 0)
                            {
                                user.EnlightenPoints += (uint)mentorValue;
                            }
                            else if (mentorValue < 0)
                            {
                                if (mentorValue > user.EnlightenPoints)
                                {
                                    return false;
                                }

                                user.EnlightenPoints -= (uint)mentorValue;
                            }
                            else
                            {
                                break;
                            }

                            await user.SynchroAttributesAsync(ClientUpdateType.EnlightenPoints, user.EnlightenPoints, true);
                            return true;
                        }

                        break;
                    }

                #endregion

                #region Riding Point

                case "ridingpoint":
                    {
                        int ridePetPoint = int.Parse(value);
                        if (opt.Equals(">"))
                        {
                            return user.HorseRacingPoints > ridePetPoint;
                        }

                        if (opt.Equals(">="))
                        {
                            return user.HorseRacingPoints >= ridePetPoint;
                        }

                        if (opt.Equals("<"))
                        {
                            return user.HorseRacingPoints < ridePetPoint;
                        }

                        if (opt.Equals("<="))
                        {
                            return user.HorseRacingPoints <= ridePetPoint;
                        }

                        if (opt.Equals("=") || opt.Equals("=="))
                        {
                            return user.HorseRacingPoints == ridePetPoint;
                        }

                        if (opt.Equals("+="))
                        {
                            //    return await user.ChangeHorseRacePointsAsync(ridePetPoint);
                            return true;
                        }
                        break;
                    }

                #endregion

                default:
                    {
                        logger.Warning("Unhandled {0} to ExecuteUserAttrAsync [{1}]", type, action.Id);
                        break;
                    }
            }

            return false;
        }

        private static async Task<bool> ExecuteUserFullAsync(DbAction action, string param, Character user, Role role,
                                                             Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (param.Equals("life", StringComparison.InvariantCultureIgnoreCase))
            {
                user.QueueAction(() => user.SetAttributesAsync(ClientUpdateType.Hitpoints, user.MaxLife));
                return true;
            }

            if (param.Equals("mana", StringComparison.InvariantCultureIgnoreCase))
            {
                user.QueueAction(() => user.SetAttributesAsync(ClientUpdateType.Mana, user.MaxMana));
                return true;
            }

            if (param.Equals("xp", StringComparison.InvariantCultureIgnoreCase))
            {
                await user.SetXpAsync(100);
                await user.BurstXpAsync();
                return true;
            }

            if (param.Equals("sp", StringComparison.InvariantCultureIgnoreCase))
            {
                await user.SetAttributesAsync(ClientUpdateType.Stamina, user.MaxEnergy);
                return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteUserChgMapAsync(DbAction action, string param, Character user, Role role,
                                                               Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] paramStrings = SplitParam(param);
            if (paramStrings.Length < 3)
            {
                logger.Warning($"Action {action.Id}:{action.Type} invalid param length: {param}");
                return false;
            }

            if (!uint.TryParse(paramStrings[0], out uint idMap)
                || !ushort.TryParse(paramStrings[1], out ushort x)
                || !ushort.TryParse(paramStrings[2], out ushort y))
            {
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning($"Invalid map identity {idMap} for action {action.Id}");
                return false;
            }

            if (!user.Map.IsTeleportDisable() ||
                paramStrings.Length >= 4 && byte.TryParse(paramStrings[3], out byte forceTeleport) &&
                forceTeleport != 0)
            {
                return await user.FlyMapAsync(idMap, x, y);
            }

            return false;
        }

        private static async Task<bool> ExecuteUserRecordpointAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] paramStrings = SplitParam(param);
            if (paramStrings.Length < 3)
            {
                logger.Warning(
                                        $"Action {action.Id}:{action.Type} invalid param length: {param}");
                return false;
            }

            if (!uint.TryParse(paramStrings[0], out uint idMap)
                || !ushort.TryParse(paramStrings[1], out ushort x)
                || !ushort.TryParse(paramStrings[2], out ushort y))
            {
                return false;
            }

            if (idMap == 0)
            {
                await user.SavePositionAsync(user.MapIdentity, user.X, user.Y);
                return true;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                logger.Warning($"Invalid map identity {idMap} for action {action.Id}");
                return false;
            }

            await user.SavePositionAsync(idMap, x, y);
            return true;
        }

        private static async Task<bool> ExecuteUserHairAsync(DbAction action, string param, Character user, Role role,
                                                             Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param);

            if (splitParams.Length < 1)
            {
                logger.Warning(
                                        $"Action {action.Id}:{action.Type} has not enough argments: {param}");
                return false;
            }

            var cmd = "style";
            var value = 0;
            if (splitParams.Length > 1)
            {
                cmd = splitParams[0];
                value = int.Parse(splitParams[1]);
            }
            else
            {
                value = int.Parse(splitParams[0]);
            }

            if (cmd.Equals("style", StringComparison.InvariantCultureIgnoreCase))
            {
                await user.SetAttributesAsync(ClientUpdateType.HairStyle,
                                              (ushort)(value + (user.Hairstyle - user.Hairstyle % 100)));
                return true;
            }

            if (cmd.Equals("color", StringComparison.InvariantCultureIgnoreCase))
            {
                await user.SetAttributesAsync(ClientUpdateType.HairStyle,
                                              (ushort)(user.Hairstyle % 100 + value * 100));
                return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteUserChgmaprecordAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.FlyMapAsync(user.RecordMapIdentity, user.RecordMapX, user.RecordMapY);
            return true;
        }

        private static async Task<bool> ExecuteActionUserChglinkmapAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user?.Map == null)
            {
                return false;
            }

            if (user.IsPm())
            {
                await user.SendAsync("ExecuteActionUserChglinkmap");
            }

            return true;
        }

        private static async Task<bool> ExecuteUserTransformAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);

            if (splitParam.Length < 4)
            {
                logger.Warning(
                                        $"Invalid param count for {action.Id}:{action.Type}, {param}");
                return false;
            }

            uint transformation = uint.Parse(splitParam[2]);
            int time = int.Parse(splitParam[3]);
            return await user.TransformAsync(transformation, time, true);
        }

        private static async Task<bool> ExecuteActionUserIspureAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            return user.ProfessionSort == user.PreviousProfession / 10 &&
                   user.FirstProfession / 10 == user.ProfessionSort;
        }

        private static async Task<bool> ExecuteActionUserTalkAsync(DbAction action, string param, Character user,
                                                                   Role role,
                                                                   Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(param, (TalkChannel)action.Data, Color.White);
            return true;
        }

        private static async Task<bool> ExecuteActionUserMagicEffectAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                return false;
            }

            //Magic magic = user.MagicData[(ushort)action.Data];
            //if (magic == null)
            //{
            //    return false;
            //}

            //var soulType = byte.Parse(splitParams[0]);
            //var soulTypeFlag = (uint)(1 << soulType);
            //var monopoly = uint.Parse(splitParams[1]);
            //var monopolyFlag = 0u;
            //if (monopoly != 0)
            //{
            //    monopolyFlag = (uint)(1 << soulType);
            //}
            //var exorbitant = uint.Parse(splitParams[2]);
            //var exorbitantFlag = 0u;
            //if (exorbitant != 0)
            //{
            //    exorbitantFlag = (uint)(1 << soulType);
            //}

            //if ((magic.AvailableEffectType & soulTypeFlag) == soulTypeFlag)
            //{
            //    return false;
            //}

            //magic.AvailableEffectType |= soulTypeFlag;

            //magic.CurrentEffectType = soulType;

            //if (monopolyFlag != 0)
            //{
            //    magic.EffectMonopoly |= monopolyFlag;
            //}

            //if (exorbitantFlag != 0)
            //{
            //    magic.EffectExorbitant |= exorbitantFlag;
            //}

            //await magic.SaveAsync();
            //await magic.SendAsync();
            //await magic.SendAsync(MsgMagicInfo.MagicAction.AddEffectType);
            //await magic.SendAsync(MsgMagicInfo.MagicAction.SetMagicEffect);
            return true;
        }

        private static async Task<bool> ExecuteActionUserMagicAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 2)
            {
                logger.Warning(
                                        $"Invalid ActionUserMagic param length: {action.Id}, {param}");
                return false;
            }

            switch (splitParam[0].ToLowerInvariant())
            {
                case "check":
                    if (splitParam.Length >= 3)
                    {
                        return user.MagicData.CheckLevel(ushort.Parse(splitParam[1]), ushort.Parse(splitParam[2]));
                    }

                    return user.MagicData.CheckType(ushort.Parse(splitParam[1]));

                case "learn":
                    if (splitParam.Length >= 3)
                    {
                        return await user.MagicData.CreateAsync(ushort.Parse(splitParam[1]), byte.Parse(splitParam[2]));
                    }

                    return await user.MagicData.CreateAsync(ushort.Parse(splitParam[1]), 0);

                case "uplev":
                case "up_lev":
                case "uplevel":
                case "up_level":
                    return await user.MagicData.UpLevelByTaskAsync(ushort.Parse(splitParam[1]));

                case "addexp":
                    return await user.MagicData.AwardExpAsync(ushort.Parse(splitParam[1]), 0, int.Parse(splitParam[2]));

                default:
                    logger.Warning("[ActionType: {0}] Unknown {1} {3} param {2}", action.Type, splitParam[0], action.Id, splitParam[1]);
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserWeaponSkillAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);

            if (splitParam.Length < 3)
            {
                logger.Warning($"Invalid param amount: {param} [{action.Id}]");
                return false;
            }

            if (!ushort.TryParse(splitParam[1], out ushort type)
                || !int.TryParse(splitParam[2], out int value))
            {
                logger.Warning(
                                        $"Invalid weapon skill type {param} for action {action.Id}");
                return false;
            }

            switch (splitParam[0].ToLowerInvariant())
            {
                case "check":
                    return user.WeaponSkill[type]?.Level >= value;

                case "learn":
                    return await user.WeaponSkill.CreateAsync(type, (byte)value);

                case "addexp":
                    await user.AddWeaponSkillExpAsync(type, value, true);
                    return true;

                default:
                    logger.Warning($"ExecuteActionUserWeaponSkill {splitParam[0]} invalid {action.Id}");
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserLogAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 2);

            if (splitParam.Length < 2)
            {
                logger.Warning(
                                        $"ExecuteActionUserLog length [id:{action.Id}], {param}");
                return true;
            }

            string file = splitParam[0];
            string message = splitParam[1];

            if (file.StartsWith("gmlog/"))
            {
                file = file.Remove(0, "gmlog/".Length);
            }

            ILogger gmLogger = Logger.CreateLogger(file);
            gmLogger.Information(message);
            return true;
        }

        private static async Task<bool> ExecuteActionUserBonusAsync(DbAction action, string param, Character user,
                                                                    Role role,
                                                                    Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }
            //return await user.DoBonusAsync();
            return false;
        }

        private static async Task<bool> ExecuteActionUserDivorceAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.MateIdentity == 0)
            {
                return false;
            }

            Character mate = RoleManager.GetUser(user.MateIdentity);
            if (mate == null)
            {
                DbUser dbMate = await UserRepository.FindByIdentityAsync(user.MateIdentity);
                if (dbMate == null)
                {
                    return false;
                }

                dbMate.Mate = 0;
                await ServerDbContext.UpdateAsync(dbMate);

                DbItem dbItem = Item.CreateEntity(Item.TYPE_METEORTEAR);
                dbItem.PlayerId = user.Identity;
                await ServerDbContext.UpdateAsync(dbItem);
            }
            else
            {
                mate.MateIdentity = 0;
                mate.MateName = StrNone;
                await mate.UserPackage.AwardItemAsync(Item.TYPE_METEORTEAR);

                await mate.SendAsync(new MsgName
                {
                    Action = StringAction.Mate,
                    Identity = mate.Identity,
                    Strings = new List<string>
                    {
                        mate.MateName
                    }
                });
            }

            user.MateIdentity = 0;
            user.MateName = StrNone;
            await user.SaveAsync();

            await user.SendAsync(new MsgName
            {
                Action = StringAction.Mate,
                Identity = user.Identity,
                Strings = new List<string>
                {
                    user.MateName
                }
            });
            return true;
        }

        private static async Task<bool> ExecuteActionUserMarriageAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            return user?.MateIdentity != 0;
        }

        private static async Task<bool> ExecuteActionUserSexAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            return user?.Gender == 1;
        }

        private static async Task<bool> ExecuteActionUserEffectAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] parsedString = SplitParam(param);
            if (parsedString.Length < 2)
            {
                logger.Warning($"Invalid parsed param[{param}] ExecuteActionUserEffect[{action.Id}]");
                return false;
            }

            var msg = new MsgName
            {
                Identity = user.Identity,
                Action = StringAction.RoleEffect
            };
            msg.Strings.Add(parsedString[1]);
            switch (parsedString[0].ToLower())
            {
                case "self":
                    await user.BroadcastRoomMsgAsync(msg, true);
                    return true;

                case "couple":
                    await user.BroadcastRoomMsgAsync(msg, true);

                    Character couple = RoleManager.GetUser(user.MateIdentity);
                    if (couple == null)
                    {
                        return true;
                    }

                    msg.Identity = couple.Identity;
                    await couple.BroadcastRoomMsgAsync(msg, true);
                    return true;

                case "team":
                    if (user.Team == null)
                    {
                        return false;
                    }

                    foreach (Character member in user.Team.Members)
                    {
                        msg.Identity = member.Identity;
                        await member.BroadcastRoomMsgAsync(msg, true);
                    }

                    return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserTaskmaskAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] parsedParam = SplitParam(param);
            if (parsedParam.Length < 2)
            {
                logger.Warning(
                                        $"ExecuteActionUserTaskmask invalid param num [{param}] for action {action.Id}");
                return false;
            }

            if (!int.TryParse(parsedParam[1], out int flag) || flag < 0 || flag >= 32)
            {
                logger.Warning($"ExecuteActionUserTaskmask invalid mask num {param}");
                return false;
            }

            switch (parsedParam[0].ToLower())
            {
                case "check":
                case "chk":
                    return user.CheckTaskMask(flag);
                case "add":
                    await user.AddTaskMaskAsync(flag);
                    return true;
                case "cls":
                case "clr":
                case "clear":
                    await user.ClearTaskMaskAsync(flag);
                    return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserMediaplayAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 2)
            {
                return false;
            }

            var msg = new MsgName
            {
                Action = StringAction.PlayerWave
            };
            msg.Strings.Add(pszParam[1]);

            switch (pszParam[0].ToLower())
            {
                case "play":
                    await user.SendAsync(msg);
                    return true;
                case "broadcast":
                    await user.BroadcastRoomMsgAsync(msg, true);
                    return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserCreatemapAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] safeParam = SplitParam(param);

            if (safeParam.Length < 10)
            {
                logger.Warning($"ExecuteActionUserCreatemap ({action.Id}) with invalid param length [{param}]");
                return false;
            }

            string szName = safeParam[0];
            uint idOwner = uint.Parse(safeParam[2]),
                 idRebornMap = uint.Parse(safeParam[7]);
            byte nOwnerType = byte.Parse(safeParam[1]);
            uint nMapDoc = uint.Parse(safeParam[3]);
            ulong nType = ulong.Parse(safeParam[4]);
            uint nRebornPortal = uint.Parse(safeParam[8]);
            byte nResLev = byte.Parse(safeParam[9]);
            ushort usPortalX = ushort.Parse(safeParam[5]),
                   usPortalY = ushort.Parse(safeParam[6]);

            var pMapInfo = new DbDynamap
            {
                Name = szName,
                OwnerIdentity = idOwner,
                OwnerType = nOwnerType,
                Description = $"{user.Name}`{szName}",
                RebornMap = idRebornMap,
                PortalX = usPortalX,
                PortalY = usPortalY,
                LinkMap = user.MapIdentity,
                LinkX = user.X,
                LinkY = user.Y,
                MapDoc = nMapDoc,
                Type = nType,
                RebornPortal = nRebornPortal,
                ResourceLevel = nResLev,
                ServerIndex = -1
            };

            if (!await ServerDbContext.CreateAsync(pMapInfo) || pMapInfo.Identity < 1000000)
            {
                logger.Error($"ExecuteActionUserCreatemap error when saving map\n\t{JsonConvert.SerializeObject(pMapInfo)}");
                return false;
            }

            var map = new GameMap(pMapInfo);
            if (!await map.InitializeAsync())
            {
                return false;
            }

            user.HomeIdentity = pMapInfo.Identity;
            await user.SaveAsync();
            return await MapManager.AddMapAsync(map);
        }

        private static async Task<bool> ExecuteActionUserEnterHomeAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null || user.HomeIdentity == 0)
            {
                return false;
            }

            GameMap target = MapManager.GetMap(user.HomeIdentity);
            if (target == null)
            {
                logger.Warning($"User[{user.Identity}] is attempting to enter an house he doesn't have.");
                return false;
            }

            uint idMap = user.MapIdentity;
            int x = user.X;
            int y = user.Y;

            await user.FlyMapAsync(target.Identity, target.PortalX, target.PortalY);

            if (user.Team != null)
            {
                foreach (Character member in user.Team.Members)
                {
                    if (member.Identity == user.Identity)
                    {
                        continue;
                    }

                    if (member.MapIdentity != idMap || member.GetDistance(x, y) > 5)
                    {
                        continue;
                    }

                    await member.FlyMapAsync(target.Identity, target.PortalX, target.PortalY);
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserEnterMateHomeAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            uint idMap = 0;
            Character mate = RoleManager.GetUser(user.MateIdentity);
            if (mate == null)
            {
                DbUser dbMate = await UserRepository.FindByIdentityAsync(user.MateIdentity);
                idMap = dbMate.HomeIdentity;
            }
            else
            {
                idMap = mate.HomeIdentity;
            }

            if (idMap == 0)
            {
                return false;
            }

            GameMap map = MapManager.GetMap(idMap);
            if (map == null)
            {
                return false;
            }

            await user.FlyMapAsync(map.Identity, map.PortalX, map.PortalY);
            return true;
        }

        private static async Task<bool> ExecuteActionUserUnlearnMagicAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] magicsIds = SplitParam(param);

            foreach (string id in magicsIds)
            {
                ushort idMagic = ushort.Parse(id);
                if (user.MagicData.CheckType(idMagic))
                {
                    await user.MagicData.UnlearnMagicAsync(idMagic, false);
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserRebirthAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);

            if (!ushort.TryParse(splitParam[0], out ushort prof)
                || !ushort.TryParse(splitParam[1], out ushort look))
            {
                logger.Warning($"Invalid parameter to rebirth {param}, {action.Id}");
                return false;
            }

            //return await user.RebirthAsync(prof, look);
            return false;
        }

        private static async Task<bool> ExecuteActionUserWebpageAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(param, TalkChannel.Website);
            return true;
        }

        private static async Task<bool> ExecuteActionUserBbsAsync(DbAction action, string param, Character user,
                                                                  Role role,
                                                                  Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(param, TalkChannel.Bbs);
            return true;
        }

        private static async Task<bool> ExecuteActionUserUnlearnSkillAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            //return await user.UnlearnAllSkillAsync();
            return false;
        }

        private static async Task<bool> ExecuteActionUserDropMagicAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] magicsIds = SplitParam(param);

            foreach (string id in magicsIds)
            {
                ushort idMagic = ushort.Parse(id);
                if (user.MagicData.CheckType(idMagic))
                {
                    await user.MagicData.UnlearnMagicAsync(idMagic, true);
                }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserOpenDialogAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            switch ((OpenWindow)action.Data)
            {
                case OpenWindow.VipWarehouse:
                    if (user.VipLevel == 0)
                    {
                        return false;
                    }

                    break;
            }

            await user.SendAsync(new MsgAction
            {
                Action = MsgAction.ActionType.ClientDialog,
                Identity = user.Identity,
                Command = action.Data,
                ArgumentX = user.X,
                ArgumentY = user.Y
            });
            return true;
        }

        private static async Task<bool> ExecuteActionUserFixAttrAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            var attr = (ushort)(user.Speed + user.Vitality + user.Strength + user.Spirit + user.AttributePoints -
                                 10);
            ushort profSort = user.ProfessionSort;
            if (profSort == 13 || profSort == 14)
            {
                profSort = 10;
            }

            DbPointAllot pointAllot = PointAllotRepository.Get(profSort, 1);
            if (pointAllot != null)
            {
                await user.SetAttributesAsync(ClientUpdateType.Strength, pointAllot.Strength);
                await user.SetAttributesAsync(ClientUpdateType.Agility, pointAllot.Agility);
                await user.SetAttributesAsync(ClientUpdateType.Vitality, pointAllot.Vitality);
                await user.SetAttributesAsync(ClientUpdateType.Spirit, pointAllot.Spirit);
            }
            else
            {
                await user.SetAttributesAsync(ClientUpdateType.Strength, 5);
                await user.SetAttributesAsync(ClientUpdateType.Agility, 2);
                await user.SetAttributesAsync(ClientUpdateType.Vitality, 3);
                await user.SetAttributesAsync(ClientUpdateType.Spirit, 0);
            }

            await user.SetAttributesAsync(ClientUpdateType.Atributes, attr);
            return true;
        }

        private static async Task<bool> ExecuteActionUserExpMultiplyAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 2)
            {
                return false;
            }

            uint time = uint.Parse(pszParam[1]);
            float multiply = int.Parse(pszParam[0]) / 100f;
            await user.SetExperienceMultiplierAsync(time, multiply);
            return true;
        }

        private static async Task<bool> ExecuteActionUserWhPasswordAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.SecondaryPassword == 0)
            {
                return true;
            }

            if (inputs.Length == 0)
            {
                return false;
            }

            string password = inputs[0];
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 1 || password.Length > ulong.MaxValue.ToString().Length)
            {
                return false;
            }

            if (!ulong.TryParse(password, out ulong uPassword))
            {
                return false;
            }

            return user.SecondaryPassword == uPassword;
        }

        private static async Task<bool> ExecuteActionUserSetWhPasswordAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            return user.IsUnlocked();
        }

        private static async Task<bool> ExecuteActionUserOpeninterfaceAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            await user.SendAsync(new MsgAction
            {
                Identity = user.Identity,
                Command = action.Data,
                Action = ActionType.ClientCommand,
                ArgumentX = user.X,
                ArgumentY = user.Y
            });
            return true;
        }

        private static async Task<bool> ExecuteActionUserTaskManagerAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.TaskDetail == null)
            {
                return false;
            }

            if (action.Data == 0)
            {
                return false;
            }

            switch (param.ToLowerInvariant())
            {
                case "new":
                    if (user.TaskDetail.QueryTaskData(action.Data) != null)
                    {
                        return false;
                    }

                    return await user.TaskDetail.CreateNewAsync(action.Data);
                case "isexit":
                    {
                        return user.TaskDetail.QueryTaskData(action.Data) != null;
                    }
                case "delete":
                    {
                        return await user.TaskDetail.DeleteTaskAsync(action.Data);
                    }
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserTaskOpeAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user?.TaskDetail == null)
            {
                return false;
            }

            if (action.Data == 0)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 3);
            if (splitParam.Length != 3)
            {
                return false;
            }

            string ope = splitParam[0].ToLowerInvariant(),
                   opt = splitParam[1].ToLowerInvariant();
            int data = int.Parse(splitParam[2]);

            if (ope.Equals("complete"))
            {
                if (opt.Equals("=="))
                {
                    return user.TaskDetail.QueryTaskData(action.Data)?.CompleteFlag == data;
                }

                if (opt.Equals("set"))
                {
                    return await user.TaskDetail.SetCompleteAsync(action.Data, data);
                }

                return false;
            }

            if (ope.StartsWith("data"))
            {
                switch (opt)
                {
                    case ">":
                        return user.TaskDetail.GetData(action.Data, ope) > data;
                    case "<":
                        return user.TaskDetail.GetData(action.Data, ope) < data;
                    case ">=":
                        return user.TaskDetail.GetData(action.Data, ope) >= data;
                    case "<=":
                        return user.TaskDetail.GetData(action.Data, ope) <= data;
                    case "==":
                        return user.TaskDetail.GetData(action.Data, ope) == data;
                    case "+=":
                        return await user.TaskDetail.AddDataAsync(action.Data, ope, data);
                    case "set":
                        return await user.TaskDetail.SetDataAsync(action.Data, ope, data);
                }

                return false;
            }

            if (ope.Equals("notify"))
            {
                DbTaskDetail detail = user.TaskDetail.QueryTaskData(action.Data);
                if (detail == null)
                {
                    return false;
                }

                detail.NotifyFlag = (byte)data;
                return await user.TaskDetail.SaveAsync(detail);
            }

            if (ope.Equals("overtime"))
            {
                DbTaskDetail detail = user.TaskDetail.QueryTaskData(action.Data);
                if (detail == null)
                {
                    return false;
                }

                detail.TaskOvertime = (uint)data;
                return await user.TaskDetail.SaveAsync(detail);
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserTaskLocaltimeAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user?.TaskDetail == null)
            {
                return false;
            }

            if (action.Data == 0)
            {
                return false;
            }

            string[] splitParam = SplitParam(param, 3);
            if (splitParam.Length != 3)
            {
                return false;
            }

            string ope = splitParam[0].ToLowerInvariant(),
                   opt = splitParam[1].ToLowerInvariant();
            int data = int.Parse(splitParam[2]);

            if (ope.StartsWith("interval", StringComparison.InvariantCultureIgnoreCase))
            {
                DbTaskDetail detail = user.TaskDetail.QueryTaskData(action.Data);
                if (detail == null)
                {
                    return true;
                }

                int mode = int.Parse(GetParenthesys(ope));
                switch (mode)
                {
                    case 0: // seconds
                        {
                            DateTime timeStamp = DateTime.Now;
                            var nDiff = (int)(timeStamp - UnixTimestamp.ToDateTime((int)detail.TaskOvertime)).TotalSeconds;
                            switch (opt)
                            {
                                case "==": return nDiff == data;
                                case "<": return nDiff < data;
                                case ">": return nDiff > data;
                                case "<=": return nDiff <= data;
                                case ">=": return nDiff >= data;
                                case "<>":
                                case "!=": return nDiff != data;
                            }

                            return false;
                        }

                    case 1: // days
                        int interval = (DateTime.Now.Date - UnixTimestamp.ToDateTime((int)detail.TaskOvertime)).Days;
                        switch (opt)
                        {
                            case "==": return interval == data;
                            case "<": return interval < data;
                            case ">": return interval > data;
                            case "<=": return interval <= data;
                            case ">=": return interval >= data;
                            case "!=":
                            case "<>": return interval != data;
                        }

                        return false;
                    default:
                        logger.Warning(
                                                $"Unhandled Time mode ({mode}) on action (id:{action.Id})");
                        return false;
                }
            }

            if (opt.Equals("set"))
            {
                DbTaskDetail detail = user.TaskDetail.QueryTaskData(action.Data);
                if (detail == null)
                {
                    return false;
                }

                if (data == 0)
                {
                    detail.TaskOvertime = (uint)UnixTimestamp.Now;
                }
                else
                {
                    detail.TaskOvertime = (uint)data;
                }
                return await user.TaskDetail.SaveAsync(detail);
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserTaskFindAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            logger.Warning("ExecuteActionUserTaskFind unhandled");
            return false;
        }

        private static async Task<bool> ExecuteActionUserVarCompareAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            byte varId = VarId(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            if (varId >= Role.MAX_VAR_AMOUNT)
            {
                return false;
            }

            switch (opt)
            {
                case "==":
                    return user.VarData[varId] == value;
                case ">=":
                    return user.VarData[varId] >= value;
                case "<=":
                    return user.VarData[varId] <= value;
                case ">":
                    return user.VarData[varId] > value;
                case "<":
                    return user.VarData[varId] < value;
                case "!=":
                    return user.VarData[varId] != value;
                default:
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserVarDefineAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] safeParam = SplitParam(param);
            if (safeParam.Length < 3)
            {
                return false;
            }

            byte varId = VarId(safeParam[0]);
            string opt = safeParam[1];
            long value = long.Parse(safeParam[2]);

            if (varId >= Role.MAX_VAR_AMOUNT)
            {
                return false;
            }

            try
            {
                switch (opt)
                {
                    case "set":
                        user.VarData[varId] = value;
                        return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserVarCompareStringAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param, 3);
            if (pszParam.Length < 3)
            {
                return false;
            }

            byte varId = VarId(pszParam[0]);
            string opt = pszParam[1];
            string value = pszParam[2];

            if (varId >= Role.MAX_VAR_AMOUNT)
            {
                return false;
            }

            switch (opt)
            {
                case "==":
                    return user.VarString[varId].Equals(value);
                default:
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserVarDefineStringAsync(DbAction action, string param, Character user,
                                                                        Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param, 3);
            if (pszParam.Length < 3)
            {
                return false;
            }

            byte varId = VarId(pszParam[0]);
            string opt = pszParam[1];
            string value = pszParam[2];

            if (varId >= Role.MAX_VAR_AMOUNT)
            {
                return false;
            }

            switch (opt)
            {
                case "set":
                    user.VarString[varId] = value;
                    return true;
            }
            return false;
        }

        private static async Task<bool> ExecuteActionUserVarCalcAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            string[] safeParam = SplitParam(param);
            if (safeParam.Length < 3)
            {
                return false;
            }

            byte varId = VarId(safeParam[0]);
            string opt = safeParam[1];
            long value = long.Parse(safeParam[2]);

            if (opt == "/=" && value == 0)
            {
                return false; // division by zero
            }

            if (varId >= Role.MAX_VAR_AMOUNT)
            {
                return false;
            }

            switch (opt)
            {
                case "+=":
                    user.VarData[varId] += value;
                    return true;
                case "-=":
                    user.VarData[varId] -= value;
                    return true;
                case "*=":
                    user.VarData[varId] *= value;
                    return true;
                case "/=":
                    user.VarData[varId] /= value;
                    return true;
                case "mod=":
                    user.VarData[varId] %= value;
                    return true;
                default:
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserTestEquipmentAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 2);
            if (!ushort.TryParse(splitParams[0], out ushort pos)
                || !int.TryParse(splitParams[1], out int type))
            {
                logger.Warning($"Invalid parsed param ExecuteActionUserTestEquipment, id[{action.Id}]");
                return false;
            }

            if (!Enum.IsDefined(typeof(Item.ItemPosition), pos))
            {
                return false;
            }

            Item temp = user.UserPackage[(Item.ItemPosition)pos];
            if (temp == null)
            {
                return false;
            }

            return temp.GetItemSubType() == type;
        }

        private static async Task<bool> ExecuteActionUserDailyStcCompareAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            string[] pStc = szStc.Trim().Split(',');

            if (pStc.Length < 2)
            {
                return false;
            }

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);

            DbStatisticDaily dbStc = user.Statistic.GetDailyStc(idEvent, idType);
            long data = dbStc?.Data ?? 0;
            switch (opt)
            {
                case ">=":
                    return data >= value;
                case "<=":
                    return data <= value;
                case ">":
                    return data > value;
                case "<":
                    return data < value;
                case "!=":
                case "<>":
                    return data != value;
                case "=":
                case "==":
                    return data == value;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserDailyStcOpeAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            string[] pStc = szStc.Trim().Split(',');

            if (pStc.Length < 2)
            {
                return false;
            }

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);

            if (!user.Statistic.HasDailyEvent(idEvent, idType))
            {
                return await user.Statistic.AddOrUpdateDailyAsync(idEvent, idType, (uint)value);
            }

            switch (opt)
            {
                case "+=":
                    {
                        if (value == 0)
                        {
                            return false;
                        }

                        long tempValue = user.Statistic.GetDailyValue(idEvent, idType) + value;
                        return await user.Statistic.AddOrUpdateDailyAsync(idEvent, idType, (uint)Math.Max(0, tempValue));
                    }
                case "=":
                case "set":
                    {
                        if (value < 0)
                        {
                            return false;
                        }

                        return await user.Statistic.AddOrUpdateDailyAsync(idEvent, idType, (uint)Math.Max(0, value));
                    }
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserExecActionAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param, 3);
            if (splitParams.Length < 3)
            {
                return false;
            }

            if (!int.TryParse(splitParams[0], out int secSpan)
                || !uint.TryParse(splitParams[1], out uint idAction))
            {
                return false;
            }

            ScriptManager.QueueAction(new QueuedAction(secSpan, idAction, user.Identity));
            return true;
        }

        private static async Task<bool> ExecuteActionUserStcCompareAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            string[] pStc = szStc.Trim().Split(',');

            if (pStc.Length < 2)
            {
                return false;
            }

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);

            DbStatistic dbStc = user.Statistic.GetStc(idEvent, idType);
            long data = dbStc?.Data ?? 0;
            switch (opt)
            {
                case ">=":
                    return data >= value;
                case "<=":
                    return data <= value;
                case ">":
                    return data > value;
                case "<":
                    return data < value;
                case "!=":
                case "<>":
                    return data != value;
                case "=":
                case "==":
                    return data == value;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserStcOpeAsync(DbAction action, string param, Character user,
                                                                     Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);
            bool bUpdate = pszParam[3] != "0";

            string[] pStc = szStc.Trim().Split(',');

            if (pStc.Length < 2)
            {
                return false;
            }

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);

            if (!user.Statistic.HasEvent(idEvent, idType))
            {
                return await user.Statistic.AddOrUpdateAsync(idEvent, idType, (uint)value, bUpdate);
            }

            switch (opt)
            {
                case "+=":
                    if (value == 0)
                    {
                        return false;
                    }

                    long tempValue = user.Statistic.GetValue(idEvent, idType) + value;
                    return await user.Statistic.AddOrUpdateAsync(idEvent, idType, (uint)Math.Max(0, tempValue),
                                                                 bUpdate);
                case "=":
                case "set":
                    if (value < 0)
                    {
                        return false;
                    }

                    return await user.Statistic.AddOrUpdateAsync(idEvent, idType, (uint)Math.Max(0, value), bUpdate);
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserCustomMsgAsync(DbAction action, string param, Character user,
                                                                       Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParam = SplitParam(param);
            if (splitParam.Length < 3)
            {
                return false;
            }

            string act = splitParam[0];
            uint type = uint.Parse(splitParam[1]);
            uint data = uint.Parse(splitParam[2]);

            if (act.Equals("send"))
            {
                await user.SendAsync(new MsgAction
                {
                    Identity = user.Identity,
                    Action = (ActionType)type,
                    Command = data,
                    ArgumentX = user.X,
                    ArgumentY = user.Y,
                    X = user.X,
                    Y = user.Y
                });
                return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserSelectToDataAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            try
            {
                user.VarData[action.Data] = long.Parse(await ServerDbContext.ScalarAsync(param));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error executing select to data", ex.Message);
                return false;
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserStcTimeCheckAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user?.Statistic == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            string[] pStc = szStc.Trim().Split(',');

            if (pStc.Length <= 2)
            {
                return false;
            }

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);
            byte mode = byte.Parse(pStc[2]);

            if (value < 0)
            {
                return false;
            }

            DbStatistic dbStc = user.Statistic.GetStc(idEvent, idType);
            if (dbStc?.Timestamp == null)
            {
                return true;
            }

            var currentStcTimestamp = UnixTimestamp.ToDateTime(dbStc.Timestamp);

            switch (mode)
            {
                case 0: // seconds
                    {
                        DateTime timeStamp = DateTime.Now;
                        var nDiff = (int)(timeStamp - currentStcTimestamp).TotalSeconds;
                        switch (opt)
                        {
                            case "==": return nDiff == value;
                            case "<": return nDiff < value;
                            case ">": return nDiff > value;
                            case "<=": return nDiff <= value;
                            case ">=": return nDiff >= value;
                            case "<>":
                            case "!=": return nDiff != value;
                        }

                        return false;
                    }

                case 1: // days
                    int interval = int.Parse(DateTime.Now.ToString("yyyyMMdd")) -
                                   int.Parse(currentStcTimestamp.ToString("yyyyMMdd"));
                    switch (opt)
                    {
                        case "==": return interval == value;
                        case "<": return interval < value;
                        case ">": return interval > value;
                        case "<=": return interval <= value;
                        case ">=": return interval >= value;
                        case "!=":
                        case "<>": return interval != value;
                    }

                    return false;
                default:
                    logger.Warning(
                                            $"Unhandled Time mode ({mode}) on action (id:{action.Id})");
                    return false;
            }
        }

        private static async Task<bool> ExecuteActionUserStcTimeOpeAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            if (user?.Statistic == null)
            {
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 3)
            {
                return false;
            }

            string szStc = GetParenthesys(pszParam[0]);
            string opt = pszParam[1];
            long value = long.Parse(pszParam[2]);

            string[] pStc = szStc.Trim().Split(',');

            uint idEvent = uint.Parse(pStc[0]);
            uint idType = uint.Parse(pStc[1]);

            switch (opt)
            {
                case "set":
                    {
                        if (value > 0)
                        {
                            return await user.Statistic.SetTimestampAsync(idEvent, idType, DateTime.Now);
                        }

                        return await user.Statistic.SetTimestampAsync(idEvent, idType, DateTime.Now);
                    }
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserAttachStatusAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            // self add 64 200 900 0
            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 6)
            {
                return false;
            }

            string target = pszParam[0].ToLower();
            string opt = pszParam[1].ToLower();
            int status = StatusSet.GetRealStatus(int.Parse(pszParam[2]));
            int multiply = int.Parse(pszParam[3]);
            uint seconds = uint.Parse(pszParam[4]);
            int times = int.Parse(pszParam[5]);
            // last param unknown

            if (target == "team" && user.Team == null)
            {
                return false;
            }

            if (target == "self")
            {
                if (opt == "add")
                {
                    await user.AttachStatusAsync(user, status, multiply, (int)seconds, times, null);
                }
                else if (opt == "del")
                {
                    await user.DetachStatusAsync(status);
                }

                return true;
            }

            if (target == "team")
            {
                foreach (Character member in user.Team.Members)
                {
                    if (opt == "add")
                    {
                        await member.AttachStatusAsync(member, status, multiply, (int)seconds, times, null);
                    }
                    else if (opt == "del")
                    {
                        await member.DetachStatusAsync(status);
                    }
                }

                return true;
            }

            if (target == "couple")
            {
                Character mate = RoleManager.GetUser(user.MateIdentity);
                if (mate == null)
                {
                    return false;
                }

                if (opt == "add")
                {
                    await user.AttachStatusAsync(user, status, multiply, (int)seconds, times, null);
                    await mate.AttachStatusAsync(user, status, multiply, (int)seconds, times, null);
                }
                else if (opt == "del")
                {
                    await user.DetachStatusAsync(status);
                    await mate.DetachStatusAsync(status);
                }

                return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserGodTimeAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            string[] pszParma = SplitParam(param);

            if (pszParma.Length < 2)
            {
                return false;
            }

            string opt = pszParma[0];
            int value = int.Parse(pszParma[1]);

            switch (opt)
            {
                case "+=":
                    {
                        return await user.AddBlessingAsync((uint)value);
                    }
                case "chk":
                    {
                        if (value == 1)
                        {
                            return user.QueryStatus(StatusSet.CURSED) != null;
                        }
                        return true;
                    }
            }

            return true;
        }

        private static async Task<bool> ExecuteActionUserCalExpAsync(DbAction action, string param, Character user,
                                                                      Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] splitParams = SplitParam(param);
            if (splitParams.Length < 3)
            {
                logger.Warning("Invalid num of params to ExecuteActionUserCalExpAsync: {}", param);
                return false;
            }

            ulong experience = ulong.Parse(splitParams[0]);
            int newLevelVar = int.Parse(splitParams[1]);
            int percentualVar = int.Parse(splitParams[2]);

            if (newLevelVar < 0 || newLevelVar >= user.VarData.Length)
            {
                logger.Warning("Index to new level must be valid range for user var");
                return false;
            }

            if (percentualVar < 0 || percentualVar >= user.VarData.Length)
            {
                logger.Warning("Index to new percentual exp must be valid range for user var");
                return false;
            }

            ExperiencePreview experiencePreview = user.PreviewExperienceIncrement(experience);
            user.VarData[newLevelVar] = experiencePreview.Level;
            user.VarData[percentualVar] = (long)experiencePreview.Percent;
            return true;
        }

        private static async Task<bool> ExecuteActionUserPureProfessionalAsync(DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            if (user.Metempsychosis < 2)
            {
                return false;
            }

            int profession = user.ProfessionSort * 10;
            int professionPrevious = user.PreviousProfession / 10 * 10;
            int professionFirst = user.FirstProfession / 10 * 10;
            return action.Data == profession && action.Data == professionPrevious && action.Data == professionFirst;
        }


        private static async Task<bool> ExecuteActionUserExpballExpAsync(DbAction action, string param, Character user,
                                                                         Role role, Item item, params string[] inputs)
        {
            string[] pszParam = SplitParam(param);

            if (pszParam.Length < 2)
            {
                return false;
            }

            int dwExpTimes = int.Parse(pszParam[0]);
            byte idData = byte.Parse(pszParam[1]);

            if (idData >= user.VarData.Length)
            {
                return false;
            }

            long exp = user.CalculateExpBall(dwExpTimes);
            user.VarData[idData] = exp;
            return true;
        }

        private static async Task<bool> ExecuteActionUserStatusCreateAsync(
            DbAction action, string param, Character user,
            Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            // sort leave_times remain_time end_time interval_time
            // 200 0 604800 0 604800 1
            if (action.Data == 0)
            {
                logger.Warning($"ERROR: invalid data num {action.Id}");
                return false;
            }

            string[] pszParam = SplitParam(param);
            if (pszParam.Length < 5)
            {
                logger.Warning($"ERROR: invalid param num {action.Id}");
                return false;
            }

            int power = int.Parse(pszParam[0]);
            int leaveTimes = int.Parse(pszParam[1]);
            int remainTime = int.Parse(pszParam[2]);
            int intervalTime = int.Parse(pszParam[4]);
            bool save = pszParam[5] != "0"; // ??

            switch (action.Data)
            {
                //case 8330:
                //case 8331:
                //case 8332:
                //case 8333:
                //case 8334:
                //case 8335:
                //case 8336:
                //case 8337:
                //case 8338:
                //case 8339:
                //case 8340:
                //    {
                //        if (!user.Map.IsRaceTrack())
                //        {
                //            return false;
                //        }

                //        if (await user.AwardRaceItemAsync((HorseRacing.ItemType)action.Data))
                //        {
                //            await user.AttachStatusAsync(user, (int)action.Data, power, int.MaxValue, 0, null, save);
                //        }
                //        break;
                //    }
                default:
                    {
                        await user.AttachStatusAsync(user, StatusSet.GetRealStatus((int)action.Data), power, remainTime, leaveTimes, null, save);
                        break;
                    }
            }
            return true;
        }

        private static async Task<bool> ExecuteActionUserStatusCheckAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user?.StatusSet == null)
            {
                return false;
            }

            string[] status = SplitParam(param);
            List<int> statuses = new();
            foreach (var stt in status)
            {
                int realStatus = StatusSet.GetRealStatus(int.Parse(stt));
                statuses.Add(realStatus);
            }

            switch (action.Data)
            {
                case 0: // check
                    foreach (var st in statuses)
                    {
                        if (user.QueryStatus(st) == null)
                        {
                            return false;
                        }
                    }

                    return true;

                case 1:
                    foreach (var st in statuses)
                    {
                        if (user.QueryStatus(st) != null)
                        {
                            await user.DetachStatusAsync(st);
                            DbStatus db = (await StatusRepository.GetAsync(user.Identity)).FirstOrDefault(x => x.Status == st);
                            if (db != null)
                            {
                                await ServerDbContext.DeleteAsync(db);
                            }
                        }
                    }

                    return true;
            }

            return false;
        }

        private static async Task<bool> ExecuteActionUserAwardTitleAsync(DbAction action, string param, Character user,
                                                                          Role role, Item item, params string[] inputs)
        {
            if (user == null)
            {
                return false;
            }

            string[] args = SplitParam(param, 4);
            if (args.Length != 4)
            {
                logger.Error("Invalid param count for action {0} type {1} param {2}", action.Id, action.Type, param);
                return false;
            }

            string opt = args[0];
            uint type = uint.Parse(args[1]);
            uint title = uint.Parse(args[2]);
            uint addTime = uint.Parse(args[3]);

            if (opt.Equals("add") || opt.Equals("time"))
            {
                return await user.TitleStorage.AwardTitleAsync(type, title, addTime);
            }
            else if (opt.Equals("check"))
            {
                return user.TitleStorage.GetTitle(type, title, out _);
            }
            else if (opt.Equals("del"))
            {
                return await user.TitleStorage.DeleteTitleAsync(type, title);
            }
            return false;
        }
    }
}
