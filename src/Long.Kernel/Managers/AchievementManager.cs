using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using System.Collections.Concurrent;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;
using static Long.Kernel.States.Items.Item;

namespace Long.Kernel.Managers
{
    public class AchievementManager
    {
        private static readonly ILogger logger = Log.ForContext<AchievementManager>();
        private static readonly ConcurrentDictionary<uint, DbAchievementType> achievements = new();

        public static async Task InitializeAsync()
        {
            logger.Information("Initializing Achievement Manager");

            foreach (var achievement in await AchievementRepository.GetTypesAsync())
            {
                if (!achievements.ContainsKey(achievement.Identity))
                {
                    achievements.TryAdd(achievement.Identity, achievement);
                }
            }
        }

        public static DbAchievementType GetAchievementType(int type)
        {
            return achievements.Values.FirstOrDefault(x => x.Position == type);
        }

        public static async Task<bool> ProcessAsync(Character user, int flag)
        {
            var achievementType = GetAchievementType(flag);
            if (achievementType == null)
            {
                return false;
            }

            switch ((AchievementType)flag)
            {
                case AchievementType.Millionaire:
                    if (user.Silvers < 3000000)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Chasingthewind:
                    if (user.UserPackage[ItemPosition.Mount] == null)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Asfastaslightning:
                    if (user.UserPackage[ItemPosition.Mount]?.Plus != 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Youarenotalone:
                    if (user.Relation == null || user.Relation.FriendAmount == 0)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Anotherclient:
                    if (user.TradePartnerRelation == null || user.TradePartnerRelation.Amount == 0)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Tietheknot:
                    if (user.MateIdentity == 0)
                    {
                        return false;
                    }

                    break;

                /**
                 * Syndicate
                 */
                case AchievementType.Thisisyourhome:
                    if (user.Syndicate == null)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Imtheking:
                    if (user.SyndicateRank != SyndicateRank.GuildLeader)
                    {
                        return false;
                    }

                    break;
                case AchievementType.ItsanHonor:
                    if (user.SyndicateRank != SyndicateRank.HonoraryDeputyLeader
                        && user.SyndicateRank != SyndicateRank.HonoraryManager
                        && user.SyndicateRank != SyndicateRank.HonorarySupervisor
                        && user.SyndicateRank != SyndicateRank.HonorarySteward)
                    {
                        return false;
                    }

                    break;
                case AchievementType.No2intheguild:
                    if (user.SyndicateRank != SyndicateRank.LeaderSpouse
                        && user.SyndicateRank != SyndicateRank.DeputyLeader
                        && user.SyndicateRank != SyndicateRank.HonoraryDeputyLeader)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Respectable:
                    if (user.SyndicateRank != SyndicateRank.Manager
                        && user.SyndicateRank != SyndicateRank.HonoraryManager)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Reverential:
                    if (user.SyndicateRank < SyndicateRank.HonorarySupervisor
                        || user.SyndicateRank > SyndicateRank.TulipSupervisor)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Feelsgood:
                    if (user.SyndicateRank != SyndicateRank.Steward
                        && user.SyndicateRank != SyndicateRank.HonorarySteward)
                    {
                        return false;
                    }

                    break;

                /**
                 * Family
                 */
                case AchievementType.Weareafamily:
                    if (user.Family == null)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Level130:
                    if (user.Level < 130)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Level140:
                    if (user.Level < 140)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Fighter:
                    if (user.BattlePower < 100)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Seniorfighter:
                    if (user.BattlePower < 200)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Exellentfighter:
                    if (user.BattlePower < 300)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Reborn:
                    if (user.Metempsychosis < 1)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Newlife:
                    if (user.Metempsychosis < 2)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Thanksalot:
                    if (user.VirtuePoints < 5000)
                    {
                        return false;
                    }

                    break;
                case AchievementType.YouareaNoble:
                    if (user.NobilityRank == NobilityRank.Serf)
                    {
                        return false;
                    }

                    break;

                /**
                 * Proficiency
                 */
                case AchievementType.Boxingmaster:
                    if (user.WeaponSkill[0]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Swordmaster:
                    if (user.WeaponSkill[420]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Blademaster:
                    if (user.WeaponSkill[410]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Hammermaster:
                    if (user.WeaponSkill[460]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.LongHammermaster:
                    if (user.WeaponSkill[540]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Halbertmaster:
                    if (user.WeaponSkill[580]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Spearmaster:
                    if (user.WeaponSkill[560]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Daggermaster:
                    if (user.WeaponSkill[490]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Wandmaster:
                    if (user.WeaponSkill[561]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Glaivemaster:
                    if (user.WeaponSkill[510]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Poleaxemaster:
                    if (user.WeaponSkill[530]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Hookmaster:
                    if (user.WeaponSkill[430]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Axemaster:
                    if (user.WeaponSkill[450]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Clubmaster:
                    if (user.WeaponSkill[480]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Sceptermaster:
                    if (user.WeaponSkill[481]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Whipmaster:
                    if (user.WeaponSkill[440]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Bowmaster:
                    if (user.WeaponSkill[500]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Beadsmaster:
                    if (user.WeaponSkill[610]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.NinjaKatanamaster:
                    if (user.WeaponSkill[601]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Backswordmaster:
                    if (user.WeaponSkill[421]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Shieldmaster:
                    if (user.WeaponSkill[900]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Fightlikeaman:
                    if (user.WeaponSkill[611]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.BangBangBang:
                    if (user.WeaponSkill[612]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.Scythemaster:
                    if (user.WeaponSkill[511]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.LegendaryKnife:
                    if (user.WeaponSkill[613]?.Level < 12)
                    {
                        return false;
                    }

                    break;
                case AchievementType.YouhaveaSuperitem:
                    bool hasSuper = false;
                    for (ItemPosition pos = ItemPosition.EquipmentBegin; pos != ItemPosition.EquipmentEnd; pos++)
                    {
                        if (user.UserPackage[pos]?.GetQuality() == 9)
                        {
                            hasSuper = true;
                            break;
                        }
                    }

                    if (!hasSuper)
                    {
                        return false;
                    }

                    break;
                case AchievementType.FullUniqueequipment:
                    {
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                switch (pos)
                                {
                                    case ItemPosition.Mount:
                                    case ItemPosition.Gourd:
                                    case ItemPosition.Garment:
                                    case ItemPosition.RightHandAccessory:
                                    case ItemPosition.LeftHandAccessory:
                                    case ItemPosition.MountArmor:
                                    case (ItemPosition)13:
                                    case (ItemPosition)14:
                                        continue;
                                    default:
                                        return false;
                                }
                            }

                            if (!item.IsEquipment())
                            {
                                continue;
                            }

                            if (item.GetQuality() % 10 < 7)
                            {
                                return false;
                            }
                        }
                        break;
                    }
                case AchievementType.FullEliteequipment:
                    {
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                switch (pos)
                                {
                                    case ItemPosition.Mount:
                                    case ItemPosition.Gourd:
                                    case ItemPosition.Garment:
                                    case ItemPosition.RightHandAccessory:
                                    case ItemPosition.LeftHandAccessory:
                                    case ItemPosition.MountArmor:
                                    case (ItemPosition)13:
                                    case (ItemPosition)14:
                                        continue;
                                    default:
                                        return false;
                                }
                            }

                            if (!item.IsEquipment())
                            {
                                continue;
                            }

                            if (item.GetQuality() % 10 < 8)
                            {
                                return false;
                            }
                        }

                        break;
                    }
                case AchievementType.FullSuperequipment:
                    {
                        for (ItemPosition pos = ItemPosition.EquipmentBegin; pos <= ItemPosition.EquipmentEnd; pos++)
                        {
                            Item item = user.UserPackage[pos];
                            if (item == null)
                            {
                                switch (pos)
                                {
                                    case ItemPosition.Mount:
                                    case ItemPosition.Gourd:
                                    case ItemPosition.Garment:
                                    case ItemPosition.RightHandAccessory:
                                    case ItemPosition.LeftHandAccessory:
                                    case ItemPosition.MountArmor:
                                    case (ItemPosition)13:
                                    case (ItemPosition)14:
                                        continue;
                                    default:
                                        return false;
                                }
                            }

                            if (!item.IsEquipment())
                            {
                                continue;
                            }

                            if (item.GetQuality() % 10 < 9)
                            {
                                return false;
                            }
                        }

                        break;
                    }
            }

            return true;
        }

        public enum AchievementType
        {
            /// <summary>
            ///     10101 - Good taste!
            ///     Place a piece of furniture in your house.
            /// </summary>
            Goodtaste = 10101,

            /// <summary>
            ///     10102 - You got a tent!
            ///     Build a L1 House.
            /// </summary>
            Yougotatent = 10102,

            /// <summary>
            ///     10103 - You got a hut!
            ///     Build a L2 House.
            /// </summary>
            Yougotahut = 10103,

            /// <summary>
            ///     10104 - You got a house!
            ///     Build a L3 House.
            /// </summary>
            Yougotahouse = 10104,

            /// <summary>
            ///     10105 - You got a mansion!
            ///     Build a L4 House.
            /// </summary>
            Yougotamansion = 10105,

            /// <summary>
            ///     10122 - Householder!
            ///     Build a L5 House.
            /// </summary>
            Householder = 10122,

            /// <summary>
            ///     10106 - Millionaire!
            ///     Have at least 3 million silver on hand.
            /// </summary>
            Millionaire = 10106,

            /// <summary>
            ///     10107 - Chasing the wind!
            ///     Equip a mount.
            /// </summary>
            Chasingthewind = 10107,

            /// <summary>
            ///     10108 - As fast as lightning!
            ///     Equip a Lineage Level 12 mount.
            /// </summary>
            Asfastaslightning = 10108,

            /// <summary>
            ///     10110 - Make a wish!
            ///     Use Virtue Points to exchange for a Meteor.
            /// </summary>
            Makeawish = 10110,

            /// <summary>
            ///     10112 - Circle of life!
            ///     Breed a new mount.
            /// </summary>
            Circleoflife = 10112,

            /// <summary>
            ///     10113 - New wave hairstyle!
            ///     Dye your hair.
            /// </summary>
            Newwavehairstyle = 10113,

            /// <summary>
            ///     10114 - Trendsetter!
            ///     Change your hair style.
            /// </summary>
            Trendsetter = 10114,

            /// <summary>
            ///     10115 - New clothes!
            ///     Change the color of your clothes.
            /// </summary>
            Newclothes = 10115,

            /// <summary>
            ///     10116 - Plastic surgery!
            ///     Change your avatar.
            /// </summary>
            Plasticsurgery = 10116,

            /// <summary>
            ///     10117 - A brave, new world!
            ///     Used the guild portal.
            /// </summary>
            Abravenewworld = 10117,

            /// <summary>
            ///     10118 - Good luck!
            ///     Receive a treasure from the Lottery.
            /// </summary>
            Goodluck = 10118,

            /// <summary>
            ///     10119 - Security!
            ///     Summon a guard.
            /// </summary>
            Security = 10119,

            /// <summary>
            ///     10120 - Genius!
            ///     Answer at least 5 questions correctly in the Quiz Show.
            /// </summary>
            Genius = 10120,

            /// <summary>
            ///     10121 - Let`s dance!
            ///     Dance in the market.
            /// </summary>
            Letsdance = 10121,

            /// <summary>
            ///     10123 - You are not alone!
            ///     Added a friend.
            /// </summary>
            Youarenotalone = 10123,

            /// <summary>
            ///     10124 - Good teacher!
            ///     Added an Apprentice.
            /// </summary>
            Goodteacher = 10124,

            /// <summary>
            ///     10125 - Good student!
            ///     Added a Mentor.
            /// </summary>
            Goodstudent = 10125,

            /// <summary>
            ///     10126 - Another client!
            ///     Added a trade partner.
            /// </summary>
            Anotherclient = 10126,

            /// <summary>
            ///     10127 - What a beautiful flower!
            ///     Delivered/received a flower/kiss.
            /// </summary>
            Whatabeautifulflower = 10127,

            /// <summary>
            ///     10128 - Pay the bill!
            ///     Buy an item from a booth.
            /// </summary>
            Paythebill = 10128,

            /// <summary>
            ///     10129 - Tie the knot!
            ///     Get married.
            /// </summary>
            Tietheknot = 10129,

            /// <summary>
            ///     10130 - Romantic!
            ///     Walk hand in hand with the opposite sex.
            /// </summary>
            Romantic = 10130,

            /// <summary>
            ///     10131 - Good host!
            ///     Invite a friend to your house.
            /// </summary>
            Goodhost = 10131,

            /// <summary>
            ///     10132 - The guest!
            ///     Visit a friend`s house.
            /// </summary>
            Theguest = 10132,

            /// <summary>
            ///     10201 - You`re welcome!
            ///     Enlighten another person.
            /// </summary>
            Yourewelcome = 10201,

            /// <summary>
            ///     10203 - You got it!
            ///     Receive enlightenment.
            /// </summary>
            Yougotit = 10203,

            /// <summary>
            ///     10218 - This is your home!
            ///     Join a guild.
            /// </summary>
            Thisisyourhome = 10218,

            /// <summary>
            ///     10219 - I`m the king!
            ///     Become a guild leader.
            /// </summary>
            Imtheking = 10219,

            /// <summary>
            ///     10221 - It`s an Honor!
            ///     Appointed as an Honorary Officer.
            /// </summary>
            ItsanHonor = 10221,

            /// <summary>
            ///     10222 - No.2 in the guild!
            ///     Become the Deputy Leader of a guild.
            /// </summary>
            No2intheguild = 10222,

            /// <summary>
            ///     10223 - Respectable!
            ///     Become the Manager of a guild.
            /// </summary>
            Respectable = 10223,

            /// <summary>
            ///     10224 - Reverential!
            ///     Become the Supervisor of a guild.
            /// </summary>
            Reverential = 10224,

            /// <summary>
            ///     10225 - Feels good!
            ///     Become the Steward of a guild.
            /// </summary>
            Feelsgood = 10225,

            /// <summary>
            ///     10226 - Martial Arsenal!
            ///     Inscribed your equipment in the Martial Arsenal.
            /// </summary>
            MartialArsenal = 10226,

            /// <summary>
            ///     10227 - Hero!
            ///     Kill a member of an enemy guild.
            /// </summary>
            Hero = 10227,

            /// <summary>
            ///     10228 - Contributor!
            ///     Receive guild rewards.
            /// </summary>
            Contributor = 10228,

            /// <summary>
            ///     10230 - Stand forever!
            ///     Plant a flag successfully in Capture the Flag.
            /// </summary>
            Standforever = 10230,

            /// <summary>
            ///     10231 - Ruin the city!
            ///     Use a Bomb on the castle gates.
            /// </summary>
            Ruinthecity = 10231,

            /// <summary>
            ///     10232 - Touch the pillar!
            ///     Attack the pillar during a Guild War.
            /// </summary>
            Touchthepillar = 10232,

            /// <summary>
            ///     10301 - Owner of the flag!
            ///     Win a round of Capture the Flag.
            /// </summary>
            Owneroftheflag = 10301,

            /// <summary>
            ///     10302 - The arena is mine!
            ///     Occupy the arena.
            /// </summary>
            Thearenaismine = 10302,

            /// <summary>
            ///     10303 - Beast killer!
            ///     Killed the Guild Beast.
            /// </summary>
            Beastkiller = 10303,

            /// <summary>
            ///     10304 - We are a family!
            ///     Join a Clan.
            /// </summary>
            Weareafamily = 10304,

            /// <summary>
            ///     10305 - Twin City is my home!
            ///     Your Clan occupied Twin City.
            /// </summary>
            TwinCityismyhome = 10305,

            /// <summary>
            ///     10306 - Desert City is my home!
            ///     Your Clan occupied Desert City.
            /// </summary>
            DesertCityismyhome = 10306,

            /// <summary>
            ///     10307 - Bird Island is my home!
            ///     Your Clan occupied Bird Island.
            /// </summary>
            BirdIslandismyhome = 10307,

            /// <summary>
            ///     10308 - Ape City is my home!
            ///     Your Clan occupied Ape City.
            /// </summary>
            ApeCityismyhome = 10308,

            /// <summary>
            ///     10309 - Phoenix City is my home!
            ///     Your Clan occupied Phoenix City.
            /// </summary>
            PhoenixCityismyhome = 10309,

            /// <summary>
            ///     10310 - Level 130!
            ///     Upgraded to Level 130.
            /// </summary>
            Level130 = 10310,

            /// <summary>
            ///     10311 - Level 140!
            ///     Upgraded to Level 140.
            /// </summary>
            Level140 = 10311,

            /// <summary>
            ///     10312 - Fighter!
            ///     Battle Power reached 100.
            /// </summary>
            Fighter = 10312,

            /// <summary>
            ///     10313 - Senior fighter!
            ///     Battle Power reached 200.
            /// </summary>
            Seniorfighter = 10313,

            /// <summary>
            ///     10314 - Exellent fighter!
            ///     Battle Power reached 300.
            /// </summary>
            Exellentfighter = 10314,

            /// <summary>
            ///     10315 - Reborn!
            ///     First Rebirth.
            /// </summary>
            Reborn = 10315,

            /// <summary>
            ///     10316 - New life!
            ///     Second Rebirth.
            /// </summary>
            Newlife = 10316,

            /// <summary>
            ///     10317 - Thanks a lot!
            ///     Virtue Points reached 5,000.
            /// </summary>
            Thanksalot = 10317,

            /// <summary>
            ///     10318 - You are a Noble!
            ///     Received a Nobility Rank.
            /// </summary>
            YouareaNoble = 10318,

            /// <summary>
            ///     10319 - You are a good student!
            ///     Received Study Points for the first time.
            /// </summary>
            Youareagoodstudent = 10319,

            /// <summary>
            ///     10320 - An honorable fighter!
            ///     Received Honor Points for the first time.
            /// </summary>
            Anhonorablefighter = 10320,

            /// <summary>
            ///     10321 - Boxing master!
            ///     Proficiency Level 12-Boxing.
            /// </summary>
            Boxingmaster = 10321,

            /// <summary>
            ///     10322 - Sword master!
            ///     Proficiency Level 12-Sword.
            /// </summary>
            Swordmaster = 10322,

            /// <summary>
            ///     10323 - Blade master!
            ///     Proficiency Level 12-Blade.
            /// </summary>
            Blademaster = 10323,

            /// <summary>
            ///     10324 - Hammer master!
            ///     Proficiency Level 12-Hammer.
            /// </summary>
            Hammermaster = 10324,

            /// <summary>
            ///     10325 - Long Hammer master!
            ///     Proficiency Level 12-Long Hammer.
            /// </summary>
            LongHammermaster = 10325,

            /// <summary>
            ///     10326 - Halbert master!
            ///     Proficiency Level 12-Halbert.
            /// </summary>
            Halbertmaster = 10326,

            /// <summary>
            ///     10327 - Spear master!
            ///     Proficiency Level 12-Spear.
            /// </summary>
            Spearmaster = 10327,

            /// <summary>
            ///     10328 - Dagger master!
            ///     Proficiency Level 12-Dagger.
            /// </summary>
            Daggermaster = 10328,

            /// <summary>
            ///     10329 - Wand master!
            ///     Proficiency Level 12-Wand.
            /// </summary>
            Wandmaster = 10329,

            /// <summary>
            ///     10330 - Glaive master!
            ///     Proficiency Level 12-Glaive.
            /// </summary>
            Glaivemaster = 10330,

            /// <summary>
            ///     10331 - Poleaxe master!
            ///     Proficiency Level 12-Poleaxe.
            /// </summary>
            Poleaxemaster = 10331,

            /// <summary>
            ///     10332 - Hook master!
            ///     Proficiency Level 12-Hook.
            /// </summary>
            Hookmaster = 10332,

            /// <summary>
            ///     10401 - Axe master!
            ///     Proficiency Level 12-Axe.
            /// </summary>
            Axemaster = 10401,

            /// <summary>
            ///     10402 - Club master!
            ///     Proficiency Level 12-Club.
            /// </summary>
            Clubmaster = 10402,

            /// <summary>
            ///     10403 - Scepter master!
            ///     Proficiency Level 12-Scepter.
            /// </summary>
            Sceptermaster = 10403,

            /// <summary>
            ///     10404 - Whip master!
            ///     Proficiency Level 12-Whip.
            /// </summary>
            Whipmaster = 10404,

            /// <summary>
            ///     10405 - Bow master!
            ///     Proficiency Level 12-Bow.
            /// </summary>
            Bowmaster = 10405,

            /// <summary>
            ///     10406 - Beads master!
            ///     Proficiency Level 12-Beads.
            /// </summary>
            Beadsmaster = 10406,

            /// <summary>
            ///     10407 - Ninja Katana master!
            ///     Proficiency Level 12-Ninja Katana.
            /// </summary>
            NinjaKatanamaster = 10407,

            /// <summary>
            ///     10408 - Backsword master!
            ///     Proficiency Level 12-Backsword.
            /// </summary>
            Backswordmaster = 10408,

            /// <summary>
            ///     11132 - Shield master!
            ///     Proficiency Level 12-Shield.
            /// </summary>
            Shieldmaster = 11132,

            /// <summary>
            ///     11210 - Fight like a man!
            ///     Proficiency of rapier reaches level 12.
            /// </summary>
            Fightlikeaman = 11210,

            /// <summary>
            ///     11211 - Bang! Bang! Bang!
            ///     Proficiency of pistol reaches level 12.
            /// </summary>
            BangBangBang = 11211,

            /// <summary>
            ///     11212 - Scythe master!
            ///     Proficiency Level 12-Scythe
            /// </summary>
            Scythemaster = 11212,

            /// <summary>
            ///     11213 - LegendaryKnife
            ///     Upgrade the Throwing Knife`s proficiency to Level 12.
            /// </summary>
            LegendaryKnife = 11213,

            /// <summary>
            ///     10409 - You have a Super item!
            ///     Equip a Super quality item.
            /// </summary>
            YouhaveaSuperitem = 10409,

            /// <summary>
            ///     10410 - Full Unique equipment!
            ///     Equip all Unique items.
            /// </summary>
            FullUniqueequipment = 10410,

            /// <summary>
            ///     10411 - Full Elite equipment!
            ///     Equip all Elite items.
            /// </summary>
            FullEliteequipment = 10411,

            /// <summary>
            ///     10412 - Full Super equipment!
            ///     Equip all Super items.
            /// </summary>
            FullSuperequipment = 10412,

            /// <summary>
            ///     10413 - Full +12 set!
            ///     Equip all +12 items.
            /// </summary>
            Full12set = 10413,

            /// <summary>
            ///     10414 - Defender!
            ///     Equip all Damage -7% items.
            /// </summary>
            Defender = 10414,

            /// <summary>
            ///     10415 - So many sockets!
            ///     All your equipment is socketed.
            /// </summary>
            Somanysockets = 10415,

            /// <summary>
            ///     10416 - Enchanted equipment!
            ///     All your equipment are Enchanted items.
            /// </summary>
            Enchantedequipment = 10416,

            /// <summary>
            ///     10417 - King of the world!
            ///     All your equipment is Super 2-Socket, +12, Damage -7%.
            /// </summary>
            Kingoftheworld = 10417,

            /// <summary>
            ///     10418 - You have a Talisman!
            ///     Equip a Talisman.
            /// </summary>
            YouhaveaTalisman = 10418,

            /// <summary>
            ///     10419 - Fashion mogul!
            ///     Equip a Garment.
            /// </summary>
            Fashionmogul = 10419,

            /// <summary>
            ///     10420 - The Meteor`s Blessing!
            ///     Successfully upgrade an item with a Meteor.
            /// </summary>
            TheMeteorsBlessing = 10420,

            /// <summary>
            ///     10421 - It was just a rock!
            ///     Fail to upgrade an item with a Meteor.
            /// </summary>
            Itwasjustarock = 10421,

            /// <summary>
            ///     10422 - Power of the Dragon Ball!
            ///     Upgrade an item`s level with a Dragon Ball.
            /// </summary>
            PoweroftheDragonBall = 10422,

            /// <summary>
            ///     10423 - The Dragon Ball`s blessing!
            ///     Successfully upgrade an item`s quality with a Dragon Ball.
            /// </summary>
            TheDragonBallsblessing = 10423,

            /// <summary>
            ///     10424 - I think it was broken!
            ///     Fail to upgrade an item`s quality with a Dragon Ball.
            /// </summary>
            Ithinkitwasbroken = 10424,

            /// <summary>
            ///     10425 - Keep working!
            ///     Open the 1st socket of an item.
            /// </summary>
            Keepworking = 10425,

            /// <summary>
            ///     10426 - Awesome!
            ///     Open the 2nd socket of an item.
            /// </summary>
            Awesome = 10426,

            /// <summary>
            ///     10428 - Shining!
            ///     Equip with 12 high quality gems.
            /// </summary>
            Shining = 10428,

            /// <summary>
            ///     10429 - You made a socket!
            ///     Equip with 1 1-socket equipment.
            /// </summary>
            Youmadeasocket = 10429,

            /// <summary>
            ///     10430 - Improved your equipment!
            ///     Equip with 1 Enchanted equipment.
            /// </summary>
            Improvedyourequipment = 10430,

            /// <summary>
            ///     10431 - Feeling refined!
            ///     First Refinery.
            /// </summary>
            Feelingrefined = 10431,

            /// <summary>
            ///     10432 - Wonderful!
            ///     Equip a set of Alternate Equipment.
            /// </summary>
            Wonderful = 10432,

            /// <summary>
            ///     10501 - Secret of the Dragon Soul
            ///     Use a Dragon Soul.
            /// </summary>
            SecretoftheDragonSoul = 10501,

            /// <summary>
            ///     10502 - Stabilize the Dragon Soul
            ///     Stabilize a Dragon Soul.
            /// </summary>
            StabilizetheDragonSoul = 10502,

            /// <summary>
            ///     10503 - Cool!
            ///     Equip a Weapon Accessary for the first time.
            /// </summary>
            Cool = 10503,

            /// <summary>
            ///     10504 - Fast Blade!
            ///     Upgraded Fast Blade to Fixed.
            /// </summary>
            FastBlade = 10504,

            /// <summary>
            ///     10505 - Scent Sword!
            ///     Upgraded Scent Sword to Fixed.
            /// </summary>
            ScentSword = 10505,

            /// <summary>
            ///     10506 - Phoenix!
            ///     Upgraded Phoenix to Fixed.
            /// </summary>
            Phoenix = 10506,

            /// <summary>
            ///     10507 - Boom!
            ///     Upgraded Boom to Fixed.
            /// </summary>
            Boom = 10507,

            /// <summary>
            ///     10508 - Halt!
            ///     Upgraded Halt to Fixed.
            /// </summary>
            Halt = 10508,

            /// <summary>
            ///     10509 - Stranded Monster!
            ///     Upgraded Stranded Monster to Fixed.
            /// </summary>
            StrandedMonster = 10509,

            /// <summary>
            ///     10510 - Speed Gun!
            ///     Upgraded Speed Gun to Fixed.
            /// </summary>
            SpeedGun = 10510,

            /// <summary>
            ///     10511 - Penetration!
            ///     Upgraded Penetration to Fixed.
            /// </summary>
            Penetration = 10511,

            /// <summary>
            ///     10512 - Snow!
            ///     Upgraded Snow to Fixed.
            /// </summary>
            Snow = 10512,

            /// <summary>
            ///     10513 - Wide Strike!
            ///     Upgraded Wide Strike to Fixed.
            /// </summary>
            WideStrike = 10513,

            /// <summary>
            ///     10514 - Boreas!
            ///     Upgraded Boreas to Fixed.
            /// </summary>
            Boreas = 10514,

            /// <summary>
            ///     10515 - Seizer!
            ///     Upgraded Seizer to Fixed.
            /// </summary>
            Seizer = 10515,

            /// <summary>
            ///     10516 - Earthquake!
            ///     Upgraded Earthquake to Fixed.
            /// </summary>
            Earthquake = 10516,

            /// <summary>
            ///     10517 - Rage!
            ///     Upgraded Rage to Fixed.
            /// </summary>
            Rage = 10517,

            /// <summary>
            ///     10518 - Celestial!
            ///     Upgraded Celestial to Fixed.
            /// </summary>
            Celestial = 10518,

            /// <summary>
            ///     10519 - Roamer!
            ///     Upgraded Roamer to Fixed.
            /// </summary>
            Roamer = 10519,

            /// <summary>
            ///     10520 - 10 KOs!
            ///     Get 10 KOs while hunting monsters.
            /// </summary>
            KOs10 = 10520,

            /// <summary>
            ///     10521 - 100 KOs!
            ///     Get 100 KOs while hunting monsters.
            /// </summary>
            KOs100 = 10521,

            /// <summary>
            ///     10522 - 300 KOs!
            ///     Get 300 KOs while hunting monsters.
            /// </summary>
            KOs300 = 10522,

            /// <summary>
            ///     10523 - The dragon`s blessing!
            ///     Loot a Dragon Ball while hunting monsters.
            /// </summary>
            Thedragonsblessing = 10523,

            /// <summary>
            ///     10524 - Wish upon a star!
            ///     Loot a Meteor while hunting monsters.
            /// </summary>
            Wishuponastar = 10524,

            /// <summary>
            ///     10525 - Fists of fury!
            ///     Kill a Level 120+ monster without a weapon.
            /// </summary>
            Fistsoffury = 10525,

            /// <summary>
            ///     10526 - Dragon slayer!
            ///     Kill the Terato Dragon.
            /// </summary>
            Dragonslayer = 10526,

            /// <summary>
            ///     10527 - Smells worse on the inside!
            ///     Kill the Snow Banshee.
            /// </summary>
            Smellsworseontheinside = 10527,

            /// <summary>
            ///     10528 - Your kung-fu is weak!
            ///     Kill the Sword Master.
            /// </summary>
            Yourkungfuisweak = 10528,

            /// <summary>
            ///     10529 - Call me, beastmaster!
            ///     Kill the Thrilling Spook.
            /// </summary>
            Callmebeastmaster = 10529,

            /// <summary>
            ///     10605 - One good turn??
            ///     Kill a blue named player.
            /// </summary>
            Onegoodturn = 10605,

            /// <summary>
            ///     10606 - Bounty Hunter!
            ///     Kill a red named player.
            /// </summary>
            BountyHunter = 10606,

            /// <summary>
            ///     10607 - For great justice!
            ///     Kill a black named player.
            /// </summary>
            Forgreatjustice = 10607,

            /// <summary>
            ///     10611 - Revenge is sweet!
            ///     Kill a player on your Enemy list.
            /// </summary>
            Revengeissweet = 10611,

            /// <summary>
            ///     10612 - Twin City!
            ///     Travel to Twin City.
            /// </summary>
            TwinCity = 10612,

            /// <summary>
            ///     10613 - Wind Plain!
            ///     Travel to the Wind Plain.
            /// </summary>
            WindPlain = 10613,

            /// <summary>
            ///     10614 - Altar!
            ///     Travel to the Altar.
            /// </summary>
            Altar = 10614,

            /// <summary>
            ///     10617 - Phoenix Castle!
            ///     Travel to Phoenix Castle.
            /// </summary>
            PhoenixCastle = 10617,

            /// <summary>
            ///     10618 - Maple Forest!
            ///     Travel to the Maple Forest.
            /// </summary>
            MapleForest = 10618,

            /// <summary>
            ///     10619 - Oops! A village!
            ///     Enter the village.
            /// </summary>
            OopsAvillage = 10619,

            /// <summary>
            ///     10620 - Tiger Cave!
            ///     Travel to the Tiger Cave.
            /// </summary>
            TigerCave = 10620,

            /// <summary>
            ///     10621 - Kylin Cave!
            ///     Travel to the Kylin Cave.
            /// </summary>
            KylinCave = 10621,

            /// <summary>
            ///     10622 - Dragon Pool!
            ///     Travel to the Dragon Pool.
            /// </summary>
            DragonPool = 10622,

            /// <summary>
            ///     10625 - Ape City!
            ///     Travel to Ape City.
            /// </summary>
            ApeCity = 10625,

            /// <summary>
            ///     10626 - Travel to the Love Canyon.
            ///     Love Canyon!
            /// </summary>
            TraveltotheLoveCanyon = 10626,

            /// <summary>
            ///     10629 - Bird Island!
            ///     Travel to Bird Island.
            /// </summary>
            BirdIsland = 10629,

            /// <summary>
            ///     10631 - Around the Bird Island.
            ///     Walk around the Bird Island.
            /// </summary>
            AroundtheBirdIsland = 10631,

            /// <summary>
            ///     10701 - Desert City!
            ///     Travel to the Desert City.
            /// </summary>
            DesertCity = 10701,

            /// <summary>
            ///     10702 - Desert!
            ///     Travel to the Desert.
            /// </summary>
            Desert = 10702,

            /// <summary>
            ///     10703 - Mystic Cave!
            ///     Travel to the Mystic Cave.
            /// </summary>
            MysticCave = 10703,

            /// <summary>
            ///     10708 - Globe Quest 5!
            ///     Travel to the Globe Quest 5.
            /// </summary>
            GlobeQuest5 = 10708,

            /// <summary>
            ///     10709 - Globe Quest 2!
            ///     Travel to the Globe Quest 2.
            /// </summary>
            GlobeQuest2 = 10709,

            /// <summary>
            ///     10710 - Globe Quest 1!
            ///     Travel to the Globe Quest 1.
            /// </summary>
            GlobeQuest1 = 10710,

            /// <summary>
            ///     10711 - Globe Quest 4!
            ///     Travel to the Globe Quest 4.
            /// </summary>
            GlobeQuest4 = 10711,

            /// <summary>
            ///     10712 - Globe Plain!
            ///     Travel to the Globe Plain.
            /// </summary>
            GlobePlain = 10712,

            /// <summary>
            ///     10713 - Globe Quest 7!
            ///     Travel to the Globe Quest 7.
            /// </summary>
            GlobeQuest7 = 10713,

            /// <summary>
            ///     10714 - Globe Quest 8!
            ///     Travel to the Globe Quest 8.
            /// </summary>
            GlobeQuest8 = 10714,

            /// <summary>
            ///     10715 - Globe Quest 10!
            ///     Travel to the Globe Quest 10.
            /// </summary>
            GlobeQuest10 = 10715,

            /// <summary>
            ///     10716 - Globe Quest 11!
            ///     Travel to the Globe Quest 11.
            /// </summary>
            GlobeQuest11 = 10716,

            /// <summary>
            ///     10717 - Globe Island!
            ///     Travel to the Globe Island.
            /// </summary>
            GlobeIsland = 10717,

            /// <summary>
            ///     10718 - Globe Exit!
            ///     Travel to the Globe Exit.
            /// </summary>
            GlobeExit = 10718,

            /// <summary>
            ///     10719 - Joint Canyon!
            ///     Travel to the Joint Canyon.
            /// </summary>
            JointCanyon = 10719,

            /// <summary>
            ///     10720 - Globe Forest!
            ///     Travel to the Globe Forest.
            /// </summary>
            GlobeForest = 10720,

            /// <summary>
            ///     10721 - Globe Canyon!
            ///     Travel to the Globe Canyon.
            /// </summary>
            GlobeCanyon = 10721,

            /// <summary>
            ///     10723 - Devil is Coming...
            ///     Deserted Wild
            /// </summary>
            DevilisComing = 10723,

            /// <summary>
            ///     10724 - Fight to the End!
            ///     Flame Plain
            /// </summary>
            FighttotheEnd = 10724,

            /// <summary>
            ///     10725 - None Survives
            ///     Death Cave
            /// </summary>
            NoneSurvives = 10725,

            /// <summary>
            ///     10726 - Taste of Slaughter
            ///     Asura Hell
            /// </summary>
            TasteofSlaughter = 10726,

            /// <summary>
            ///     10727 - Dust to Dust
            ///     Corpse Forest
            /// </summary>
            DusttoDust = 10727,

            /// <summary>
            ///     10801 - Frozen Grotto F1!
            ///     Travel to the Frozen Grotto F1.
            /// </summary>
            FrozenGrottoF1 = 10801,

            /// <summary>
            ///     10802 - Frozen Grotto F2!
            ///     Travel to the Frozen Grotto F2.
            /// </summary>
            FrozenGrottoF2 = 10802,

            /// <summary>
            ///     10803 - Frozen Grotto F3!
            ///     Travel to the Frozen Grotto F3.
            /// </summary>
            FrozenGrottoF3 = 10803,

            /// <summary>
            ///     10804 - Frozen Grotto F4!
            ///     Travel to the Frozen Grotto F4.
            /// </summary>
            FrozenGrottoF4 = 10804,

            /// <summary>
            ///     10805 - Frozen Grotto F5!
            ///     Travel to the Frozen Grotto F5.
            /// </summary>
            FrozenGrottoF5 = 10805,

            /// <summary>
            ///     10806 - Frozen Grotto F6!
            ///     Travel to the Frozen Grotto F6.
            /// </summary>
            FrozenGrottoF6 = 10806,

            /// <summary>
            ///     10807 - Training Ground!
            ///     Travel to the Training Ground.
            /// </summary>
            TrainingGround = 10807,

            /// <summary>
            ///     10808 - Orchid Garden!
            ///     Travel to the Orchid Garden.
            /// </summary>
            OrchidGarden = 10808,

            /// <summary>
            ///     10809 - Moon Platform!
            ///     Travel to the Moon Platform.
            /// </summary>
            MoonPlatform = 10809,

            /// <summary>
            ///     10810 - Guild Area!
            ///     Travel to the Guild Area.
            /// </summary>
            GuildArea = 10810,

            /// <summary>
            ///     10812 - Market!
            ///     Travel to the Market.
            /// </summary>
            Market = 10812,

            /// <summary>
            ///     10813 - Warrior!
            ///     Get promoted to a Warrior.
            /// </summary>
            Warrior = 10813,

            /// <summary>
            ///     10814 - Brass Warrior!
            ///     Get promoted to a Brass Warrior.
            /// </summary>
            BrassWarrior = 10814,

            /// <summary>
            ///     10815 - Silver Warrior!
            ///     Get promoted to a Silver Warrior.
            /// </summary>
            SilverWarrior = 10815,

            /// <summary>
            ///     10816 - Gold Warrior!
            ///     Get promoted to a Gold Warrior.
            /// </summary>
            GoldWarrior = 10816,

            /// <summary>
            ///     10817 - Warrior King!
            ///     Get promoted to a Warrior King.
            /// </summary>
            WarriorKing = 10817,

            /// <summary>
            ///     10819 - Trojan!
            ///     Get promoted to a Trojan.
            /// </summary>
            Trojan = 10819,

            /// <summary>
            ///     10820 - Veteran Trojan!
            ///     Get promoted to a Veteran Trojan.
            /// </summary>
            VeteranTrojan = 10820,

            /// <summary>
            ///     10821 - Tiger Trojan!
            ///     Get promoted to a Tiger Trojan.
            /// </summary>
            TigerTrojan = 10821,

            /// <summary>
            ///     10822 - Dragon Trojan!
            ///     Get promoted to a Dragon Trojan.
            /// </summary>
            DragonTrojan = 10822,

            /// <summary>
            ///     10823 - Trojan Master!
            ///     Get promoted to a Trojan Master.
            /// </summary>
            TrojanMaster = 10823,

            /// <summary>
            ///     10824 - Taoist!
            ///     Get promoted to a Taoist.
            /// </summary>
            NewWatTaoist = 10824,

            /// <summary>
            ///     10825 - Fire Taoist!
            ///     Get promoted to a Fire Taoist.
            /// </summary>
            FireTaoist = 10825,

            /// <summary>
            ///     10826 - Fire Wizards!
            ///     Get promoted to a Fire Wizards.
            /// </summary>
            FireWizards = 10826,

            /// <summary>
            ///     10827 - Fire Master!
            ///     Get promoted to a Fire Master.
            /// </summary>
            FireMaster = 10827,

            /// <summary>
            ///     10828 - Fire Saint!
            ///     Get promoted to a Fire Saint.
            /// </summary>
            FireSaint = 10828,

            /// <summary>
            ///     10829 - Taoist!
            ///     Get promoted to a Taoist.
            /// </summary>
            NewFirTaoist = 10829,

            /// <summary>
            ///     10830 - Water Taoist!
            ///     Get promoted to a Water Taoist.
            /// </summary>
            WaterTaoist = 10830,

            /// <summary>
            ///     10831 - Water Wizards!
            ///     Get promoted to a Water Wizards.
            /// </summary>
            WaterWizards = 10831,

            /// <summary>
            ///     10832 - Water Maste!
            ///     Get promoted to a Water Master.
            /// </summary>
            WaterMaste = 10832,

            /// <summary>
            ///     10901 - Water Saint!
            ///     Get promoted to a Water Saint.
            /// </summary>
            WaterSaint = 10901,

            /// <summary>
            ///     10902 - Saviour!
            ///     Revive someone.
            /// </summary>
            Saviour = 10902,

            /// <summary>
            ///     10903 - Archer!
            ///     Get promoted to an Archer.
            /// </summary>
            Archer = 10903,

            /// <summary>
            ///     10904 - Eagle Archer!
            ///     Get promoted to an Eagle Archer.
            /// </summary>
            EagleArcher = 10904,

            /// <summary>
            ///     10905 - Tiger Archer!
            ///     Get promoted to a Tiger Archer.
            /// </summary>
            TigerArcher = 10905,

            /// <summary>
            ///     10906 - Dragon Archer!
            ///     Get promoted to a Dragon Archer.
            /// </summary>
            DragonArcher = 10906,

            /// <summary>
            ///     10907 - Archer Master!
            ///     Get promoted to an Archer Master.
            /// </summary>
            ArcherMaster = 10907,

            /// <summary>
            ///     10908 - Ninja!
            ///     Get promoted to a Ninja.
            /// </summary>
            Ninja = 10908,

            /// <summary>
            ///     10909 - Middle Ninja!
            ///     Get promoted to a Middle Ninja.
            /// </summary>
            MiddleNinja = 10909,

            /// <summary>
            ///     10910 - Dark Ninja!
            ///     Get promoted to a Dark Ninja.
            /// </summary>
            DarkNinja = 10910,

            /// <summary>
            ///     10911 - Mystic Ninja!
            ///     Get promoted to a Mystic Ninja.
            /// </summary>
            MysticNinja = 10911,

            /// <summary>
            ///     10912 - Ninja Master!
            ///     Get promoted to a Ninja Master.
            /// </summary>
            NinjaMaster = 10912,

            /// <summary>
            ///     10913 - Buddhist!
            ///     Get promoted to a Monk/Saint.
            /// </summary>
            Buddhist = 10913,

            /// <summary>
            ///     10914 - Dhyana!
            ///     Get promoted to a Dhyana Monk/Saint.
            /// </summary>
            Dhyana = 10914,

            /// <summary>
            ///     10915 - Dharma!
            ///     Get promoted to a Dharma Monk/Saint.
            /// </summary>
            Dharma = 10915,

            /// <summary>
            ///     10916 - Prajna!
            ///     Get promoted to a Prajna Monk/Saint.
            /// </summary>
            Prajna = 10916,

            /// <summary>
            ///     10917 - Nirvana!
            ///     Get promoted to a Nirvana Monk/Saint.
            /// </summary>
            Nirvana = 10917,

            /// <summary>
            ///     10919 - Aura maker!
            ///     Create an Aura.
            /// </summary>
            Auramaker = 10919,

            /// <summary>
            ///     11205 - Set sail!
            ///     Be a pirate.
            /// </summary>
            Setsail = 11205,

            /// <summary>
            ///     11206 - I`m a gunner!
            ///     Promoted to Pirate Gunner.
            /// </summary>
            Imagunner = 11206,

            /// <summary>
            ///     11207 - More than a master.
            ///     Promoted to Quartermaster.
            /// </summary>
            Morethanamaster = 11207,

            /// <summary>
            ///     11208 - Invincible!
            ///     Promoted to Pirate Captain.
            /// </summary>
            Invincible = 11208,

            /// <summary>
            ///     11209 - The world is mine.
            ///     Promoted to Pirate Lord.
            /// </summary>
            Theworldismine = 11209,

            /// <summary>
            ///     10920 - Apothecary P1!
            ///     Become an Apothecary P1.
            /// </summary>
            ApothecaryP1 = 10920,

            /// <summary>
            ///     10921 - Apothecary P9!
            ///     Reach Apothecary P9.
            /// </summary>
            ApothecaryP9 = 10921,

            /// <summary>
            ///     10922 - Martial Artist P1!
            ///     Become a Martial Artist P1.
            /// </summary>
            MartialArtistP1 = 10922,

            /// <summary>
            ///     10923 - Martial Artist P9!
            ///     Reach Martial Artist P9.
            /// </summary>
            MartialArtistP9 = 10923,

            /// <summary>
            ///     10924 - Chi Master P1!
            ///     Become a Chi Master P1.
            /// </summary>
            ChiMasterP1 = 10924,

            /// <summary>
            ///     10925 - Chi Master P9!
            ///     Reach Chi Master P9.
            /// </summary>
            ChiMasterP9 = 10925,

            /// <summary>
            ///     10926 - Sage P1!
            ///     Become a Sage P1.
            /// </summary>
            SageP1 = 10926,

            /// <summary>
            ///     10927 - Sage P9!
            ///     Reach Sage P9.
            /// </summary>
            SageP9 = 10927,

            /// <summary>
            ///     10928 - Performer P1!
            ///     Become a Performer P1.
            /// </summary>
            PerformerP1 = 10928,

            /// <summary>
            ///     10929 - Performer P9!
            ///     Reach Performer P9.
            /// </summary>
            PerformerP9 = 10929,

            /// <summary>
            ///     11201 - Wrangler P1!
            ///     Become a Wrangler P1.
            /// </summary>
            WranglerP1 = 11201,

            /// <summary>
            ///     11202 - Wrangler P9!
            ///     Reach Wrangler P9.
            /// </summary>
            WranglerP9 = 11202,

            /// <summary>
            ///     11203 - Warlock P1!
            ///     Become a Warlock P1.
            /// </summary>
            WarlockP1 = 11203,

            /// <summary>
            ///     11204 - Warlock P9!
            ///     Reach Warlock P9.
            /// </summary>
            WarlockP9 = 11204,

            /// <summary>
            ///     10930 - Graduation day!
            ///     Complete all the Newbie quests.
            /// </summary>
            Graduationday = 10930,

            /// <summary>
            ///     10931 - All decked out!
            ///     Complete all the equipment bonus quests.
            /// </summary>
            Alldeckedout = 10931,

            /// <summary>
            ///     10932 - Dragon Slayer!
            ///     Receive a Dragon Soul from the Dragon Slayer quest.
            /// </summary>
            DragonSlayer = 10932,

            /// <summary>
            ///     11001 - Fighting the good fight!
            ///     Complete Demon Exterminators.
            /// </summary>
            Fightingthegoodfight = 11001,

            /// <summary>
            ///     11002 - Dis City quest (1)!
            ///     Complete Dis City (1).
            /// </summary>
            DisCityquest1 = 11002,

            /// <summary>
            ///     11003 - Dis City quest (2)!
            ///     Complete Dis City (2).
            /// </summary>
            DisCityquest2 = 11003,

            /// <summary>
            ///     11004 - Dis City quest (3)!
            ///     Complete Dis City (3).
            /// </summary>
            DisCityquest3 = 11004,

            /// <summary>
            ///     11005 - Dis City quest (4)!
            ///     Complete Dis City (4).
            /// </summary>
            DisCityquest4 = 11005,

            /// <summary>
            ///     11010 - Legendary White Knight!
            ///     Complete the Elimination quest.
            /// </summary>
            LegendaryWhiteKnight = 11010,

            /// <summary>
            ///     11012 - Moment of silence??
            ///     Complete Release the Souls.
            /// </summary>
            Momentofsilence = 11012,

            /// <summary>
            ///     11013 - Top floor!
            ///     Pass the 7th floor of the Power Arena.
            /// </summary>
            Topfloor = 11013,

            /// <summary>
            ///     11014 - You are good at Refinery!
            ///     Receive 12 Refinery Badges in the Power Arena.
            /// </summary>
            YouaregoodatRefinery = 11014,

            /// <summary>
            ///     11016 - Keeper of the flame!
            ///     Found the Dragon Rune (Flame Lit Event).
            /// </summary>
            Keeperoftheflame = 11016,

            /// <summary>
            ///     11018 - I hate snakes!
            ///     Complete the Snake Islands quest.
            /// </summary>
            Ihatesnakes = 11018,

            /// <summary>
            ///     11019 - Just do it!
            ///     Complete the Sky Pass quest.
            /// </summary>
            Justdoit = 11019,

            /// <summary>
            ///     11020 - United we stand!
            ///     Collected the Amulets (Ancient Devil)
            /// </summary>
            Unitedwestand = 11020,

            /// <summary>
            ///     11022 - Time travel.
            ///     Receive the Moon Box (from the quest NPC).
            /// </summary>
            Timetravel = 11022,

            /// <summary>
            ///     11027 - Dance, magic, dance!
            ///     Receive the Ancestor Box from Simon.
            /// </summary>
            Dancemagicdance = 11027,

            /// <summary>
            ///     11030 - First Rebirth!
            ///     Complete the Reborn quest.
            /// </summary>
            FirstRebirth = 11030,

            /// <summary>
            ///     11031 - Second Rebirth!
            ///     Complete the Second Rebirth quest.
            /// </summary>
            SecondRebirth = 11031,

            /// <summary>
            ///     11032 - Newbie in Arena!
            ///     Joined the Arena for the first time.
            /// </summary>
            NewbieinArena = 11032,

            /// <summary>
            ///     11101 - Trojan killer!
            ///     Defeat a Trojan in the tournament.
            /// </summary>
            Trojankiller = 11101,

            /// <summary>
            ///     11102 - Warrior killer!
            ///     Defeat a Warrior in the tournament.
            /// </summary>
            Warriorkiller = 11102,

            /// <summary>
            ///     11103 - Fire Taoist killer!
            ///     Defeat a Fire Taoist in the tournament.
            /// </summary>
            FireTaoistkiller = 11103,

            /// <summary>
            ///     11104 - Water Taoist killer!
            ///     Defeat a Water Taoist in the tournament.
            /// </summary>
            WaterTaoistkiller = 11104,

            /// <summary>
            ///     11105 - Archer killer!
            ///     Defeat an Archer in the tournament.
            /// </summary>
            Archerkiller = 11105,

            /// <summary>
            ///     11106 - Ninja killer!
            ///     Defeat a Ninja in the tournament.
            /// </summary>
            Ninjakiller = 11106,

            /// <summary>
            ///     11107 - Monk killer!
            ///     Defeat a Monk in the tournament.
            /// </summary>
            Monkkiller = 11107,

            /// <summary>
            ///     11108 - The curse of Poseidon.
            ///     Defeat a pirate in the Arena.
            /// </summary>
            ThecurseofPoseidon = 11108,

            /// <summary>
            ///     11111 - PK enthusiast!
            ///     Joined 20 tournaments a day.
            /// </summary>
            PKenthusiast = 11111,

            /// <summary>
            ///     11112 - Shoo-in!
            ///     Won the first 9 tournaments in a day.
            /// </summary>
            Shooin = 11112,

            /// <summary>
            ///     11113 - Keep riding!
            ///     Joined the Horse Racing and Complete the race.
            /// </summary>
            Keepriding = 11113,

            /// <summary>
            ///     11114 - BP PK War!
            ///     Joined the BP PK War.
            /// </summary>
            BPPKWar = 11114,

            /// <summary>
            ///     11115 - Couple PK Tournament!
            ///     Joined the Couple PK Tournament.
            /// </summary>
            CouplePKTournament = 11115,

            /// <summary>
            ///     11116 - Weekly PK War!
            ///     Joined the Weekly PK War.
            /// </summary>
            WeeklyPKWar = 11116,

            /// <summary>
            ///     11117 - Monthly PK War!
            ///     Joined the Monthly PK War.
            /// </summary>
            MonthlyPKWar = 11117,

            /// <summary>
            ///     11118 - Weekly PK War Winner!
            ///     Win the Weekly PK War.
            /// </summary>
            WeeklyPKWarWinner = 11118,

            /// <summary>
            ///     11119 - Monthly PK War Winner!
            ///     Win the Monthly PK War.
            /// </summary>
            MonthlyPKWarWinner = 11119,

            /// <summary>
            ///     11120 - You are the King!
            ///     Win the `King` title.
            /// </summary>
            YouaretheKing = 11120,

            /// <summary>
            ///     11121 - Couple PK Tournament Winner!
            ///     Win the Couple PK Tournament.
            /// </summary>
            CouplePKTournamentWinner = 11121,

            /// <summary>
            ///     11122 - Horse Racing Winner!
            ///     Win the Horse Racing.
            /// </summary>
            HorseRacingWinner = 11122,

            /// <summary>
            ///     11123 - Champion!
            ///     Receive the title of `Champion`.
            /// </summary>
            Champion = 11123,

            /// <summary>
            ///     11124 - Rose Queen!
            ///     Become the Rose Queen.
            /// </summary>
            RoseQueen = 11124,

            /// <summary>
            ///     11125 - Orchid Queen!
            ///     Become the Orchid Queen.
            /// </summary>
            OrchidQueen = 11125,

            /// <summary>
            ///     11126 - Tulip Queen!
            ///     Become the Tulip Queen.
            /// </summary>
            TulipQueen = 11126,

            /// <summary>
            ///     11127 - Lily Queen!
            ///     Become the Lily Queen.
            /// </summary>
            LilyQueen = 11127,

            /// <summary>
            ///     11128 - Wiseman!
            ///     Win the Quiz Show.
            /// </summary>
            Wiseman = 11128,

            /// <summary>
            ///     11131 - Professional killer!
            ///     Win the Class PK War.
            /// </summary>
            Professionalkiller = 11131
        }
    }
}
