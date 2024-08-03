using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Modules.Systems.Totem;
using static Long.Kernel.Modules.Systems.Totem.ITotem;
using System.Collections.Concurrent;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Shared.Helpers;
using Serilog;
using static Long.Kernel.StrRes;
using Long.Shared;
using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Module.Totem.Network;
using Long.Module.Totem.Repositories;

namespace Long.Module.Totem.States
{
    public sealed class Totem : ITotem
    {
        private static readonly ILogger gmLog = Logger.CreateLogger("totem");
        private readonly ISyndicate syndicate;

        public Totem(ISyndicate syndicate)
        {
            this.syndicate = syndicate;
        }

        public DateTime? LastOpenTotem { get; set; }

        public bool HasTotemHeadgear
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Headgear);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Headgear;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Headgear;
                }
            }
        }

        public bool HasTotemNecklace
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Necklace);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Necklace;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Necklace;
                }
            }
        }

        public bool HasTotemRing
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Ring);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Ring;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Ring;
                }
            }
        }

        public bool HasTotemWeapon
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Weapon);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Weapon;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Weapon;
                }
            }
        }

        public bool HasTotemArmor
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Armor);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Armor;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Armor;
                }
            }
        }

        public bool HasTotemBoots
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Boots);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Boots;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Boots;
                }
            }
        }

        public bool HasTotemFan
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Fan);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Fan;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Fan;
                }
            }
        }

        public bool HasTotemTower
        {
            get => syndicate.TotemPole.HasFlag(TotemPoleFlag.Tower);
            set
            {
                if (value)
                {
                    syndicate.TotemPole |= TotemPoleFlag.Tower;
                }
                else
                {
                    syndicate.TotemPole &= ~TotemPoleFlag.Tower;
                }
            }
        }

        public int Level => totemPoles.Values.Count(x => !x.Locked && x.Donation >= 5_000_000) + 1;

        public int SharedBattlePower { get; private set; }

        private readonly ConcurrentDictionary<TotemPoleType, TotemPole> totemPoles = new();

        public Task CreateAsync()
        {
            for (var totemPole = TotemPoleType.Headgear; totemPole < TotemPoleType.None; totemPole++)
            {
                totemPoles.TryAdd(totemPole, new TotemPole(totemPole));
            }
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            List<DbTotemAdd> totemAdds = await TotemRepository.GetAsync(syndicate.Identity);
            for (int i = totemAdds.Count - 1; i >= 0; i--)
            {
                var add = totemAdds[i];
                if (UnixTimestamp.ToDateTime(add.TimeLimit) < DateTime.Now)
                {
                    await ServerDbContext.DeleteAsync(add);
                    totemAdds.RemoveAt(i);
                    continue;
                }
            }

            List<DbItem> items = await ItemRepository.GetBySyndicateAsync(syndicate.Identity) ?? new List<DbItem>();
            for (var totemPole = TotemPoleType.Headgear; totemPole < TotemPoleType.None; totemPole++)
            {
                var pole = new TotemPole(totemPole, totemAdds.FirstOrDefault(x => x.TotemType == (int)totemPole));
                switch (totemPole)
                {
                    case TotemPoleType.Headgear:
                        pole.Locked = !HasTotemHeadgear;
                        break;
                    case TotemPoleType.Necklace:
                        pole.Locked = !HasTotemNecklace;
                        break;
                    case TotemPoleType.Ring:
                        pole.Locked = !HasTotemRing;
                        break;
                    case TotemPoleType.Weapon:
                        pole.Locked = !HasTotemWeapon;
                        break;
                    case TotemPoleType.Armor:
                        pole.Locked = !HasTotemArmor;
                        break;
                    case TotemPoleType.Boots:
                        pole.Locked = !HasTotemBoots;
                        break;
                    case TotemPoleType.Fan:
                        pole.Locked = !HasTotemFan;
                        break;
                    case TotemPoleType.Tower:
                        pole.Locked = !HasTotemTower;
                        break;
                }

                if (!pole.Locked)
                {
                    foreach (DbItem item in items.Where(x => GetTotemPoleType(x.Type) == totemPole))
                    {
                        var member = syndicate.QueryMember(item.PlayerId);
                        if (member == null)
                        {
                            item.Syndicate = 0;
                            await ServerDbContext.UpdateAsync(item);
                            continue;
                        }
                        pole.Totems.TryAdd(item.Id, new TotemItem(item, member.UserName));
                    }
                }

                totemPoles.TryAdd(totemPole, pole);
            }

            UpdateBattlePower();
        }

        public async Task<bool> OpenTotemPoleAsync(TotemPoleType type)
        {
            if (totemPoles.TryGetValue(type, out TotemPole pole) && !pole.Locked)
            {
                return false;
            }

            if (pole == null)
            {
                pole = new TotemPole(type);
                totemPoles.TryAdd(type, pole);
            }

            LastOpenTotem = DateTime.Now;

            switch (type)
            {
                case TotemPoleType.Headgear:
                    HasTotemHeadgear = true;
                    break;
                case TotemPoleType.Necklace:
                    HasTotemNecklace = true;
                    break;
                case TotemPoleType.Ring:
                    HasTotemRing = true;
                    break;
                case TotemPoleType.Weapon:
                    HasTotemWeapon = true;
                    break;
                case TotemPoleType.Armor:
                    HasTotemArmor = true;
                    break;
                case TotemPoleType.Boots:
                    HasTotemBoots = true;
                    break;
                case TotemPoleType.Fan:
                    HasTotemFan = true;
                    break;
                case TotemPoleType.Tower:
                    HasTotemTower = true;
                    break;
            }

            pole.Locked = false;
            return true;
        }

        public async Task<bool> InscribeItemAsync(Character user, Item item)
        {
            if (user.SyndicateIdentity != syndicate.Identity)
            {
                return false;
            }

            TotemPoleType type = GetTotemPoleType(item.Type);
            if (type == TotemPoleType.None)
            {
                return false;
            }

            if (!totemPoles.TryGetValue(type, out TotemPole pole) || pole.Locked)
            {
                return false;
            }

            if (user.UserPackage.FindItemByIdentity(item.Identity) == null)
            {
                return false;
            }

            if (item.GetQuality() < 8 || item.IsArrowSort())
            {
                return false;
            }

            if (pole.Totems.ContainsKey(item.Identity))
            {
                return false;
            }

            int limit = TotemLimit(user.Level, user.Metempsychosis);
            if (pole.Totems.Values.Count(x => x.PlayerIdentity == user.Identity) >= limit)
            {
                await user.SendAsync(string.Format(StrTotemPoleLimit, limit));
                return false;
            }

            var totem = new TotemItem(user, item);
            if (!pole.Totems.TryAdd(totem.ItemIdentity, totem))
            {
                return false;
            }

            item.SyndicateIdentity = syndicate.Identity;
            await item.SaveAsync();

            int battlePower = SharedBattlePower;
            UpdateBattlePower();

            if (battlePower != SharedBattlePower)
            {
                await SynchronizeBattlePowerAsync();
            }

            await RefreshMemberArsenalDonationAsync(user.SyndicateMember);
            await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
            await SendTotemPolesAsync(user);

            gmLog.Information($"inscribe,{user.Identity},{item.Identity},{item.Type},{syndicate.Identity}");
            return true;
        }

        public async Task<bool> UnsubscribeItemAsync(uint idItem, uint idUser, bool synchro = true,
                                                     bool isSystem = false)
        {
            Character user = RoleManager.GetUser(idUser);
            if (user == null && !isSystem)
            {
                return false;
            }

            ISyndicateMember member = syndicate.QueryMember(idUser);
            DbItem dbItem = null;
            Item item = null;
            uint idType;
            if (user == null)
            {
                dbItem = await ItemRepository.GetByIdAsync(idItem);
                if (dbItem == null)
                {
                    return false;
                }

                idType = dbItem.Type;
            }
            else
            {
                item = user.UserPackage.FindItemByIdentityAnywhere(idItem);
                if (item == null)
                {
                    return false;
                }

                idType = item.Type;
            }

            TotemPoleType type = GetTotemPoleType(idType);
            if (type == TotemPoleType.None)
            {
                return false;
            }

            if (!totemPoles.TryGetValue(type, out TotemPole pole) || pole.Locked)
            {
                return false;
            }

            if (!pole.Totems.TryGetValue(idItem, out TotemItem totem) || totem.PlayerIdentity != idUser)
            {
                return false;
            }

            pole.Totems.TryRemove(idItem, out _);

            if (item != null)
            {
                item.SyndicateIdentity = 0;
                await item.SaveAsync();
            }
            else
            {
                dbItem.Syndicate = 0;
                await ServerDbContext.UpdateAsync(dbItem);
            }

            if (synchro)
            {
                int battlePower = SharedBattlePower;
                UpdateBattlePower();

                if (battlePower != SharedBattlePower)
                {
                    await SynchronizeBattlePowerAsync();
                }

                if (user != null)
                {
                    if (member != null)
                    {
                        await RefreshMemberArsenalDonationAsync(member);
                    }

                    await user.SendAsync(new MsgItemInfo(item, MsgItemInfo.ItemMode.Update));
                    await SendTotemPolesAsync(user);
                }
            }

            gmLog.Information($"unsubscribe,{idUser},{idItem},{idType},{syndicate.Identity}");
            return true;
        }

        public async Task UnsubscribeAllAsync(uint idUser, bool isSystem = false)
        {
            var totems = new List<TotemItem>();
            for (var type = TotemPoleType.Headgear; type < TotemPoleType.None; type++)
            {
                if (totemPoles.TryGetValue(type, out TotemPole pole))
                {
                    totems.AddRange(pole.Totems.Values.Where(x => x.PlayerIdentity == idUser));
                }
            }

            int battlePower = SharedBattlePower;

            foreach (TotemItem totem in totems)
            {
                await UnsubscribeItemAsync(totem.ItemIdentity, totem.PlayerIdentity, false, isSystem);
            }

            UpdateBattlePower();
            if (battlePower != SharedBattlePower)
            {
                await SynchronizeBattlePowerAsync();
            }
        }

        public async Task RefreshMemberArsenalDonationAsync(ISyndicateMember member)
        {
            member.ArsenalDonation = 0;
            foreach (TotemPole pole in totemPoles.Values.Where(x => !x.Locked))
            {
                member.ArsenalDonation += (uint)pole.Totems.Values.Where(x => x.PlayerIdentity == member.UserIdentity)
                                                     .Sum(x => x.Points);
            }

            await member.SaveAsync();
        }

        public async Task<bool> EnhanceTotemPoleAsync(TotemPoleType type, byte power)
        {
            if (!totemPoles.TryGetValue(type, out TotemPole pole))
            {
                return false;
            }

            if (pole.Enhancement >= power)
            {
                return false;
            }

            int cost;
            switch (power)
            {
                case 1:
                    cost = 60000000;
                    break;
                case 2:
                    cost = 100000000;
                    break;
                default:
                    return false;
            }

            if (pole.Enhancement > 0)
            {
                await pole.RemoveEnhancementAsync();
            }

            if (!await pole.SetEnhancementAsync(new DbTotemAdd
            {
                OwnerIdentity = syndicate.Identity,
                BattleAddition = power,
                TimeLimit = (uint)DateTime.Now.AddDays(30).ToUnixTimestamp(),
                TotemType = (uint)type
            }))
            {
                return false;
            }

            UpdateBattlePower();

            gmLog.Information($"enhance,{syndicate.Identity},{type},{power},{cost}");
            return true;
        }

        public Task SendTotemPolesAsync(Character user)
        {
            var msg = new MsgTotemPoleInfo
            {
                TotemBattlePower = SharedBattlePower,
                SharedBattlePower = syndicate.GetSharedBattlePower(user.SyndicateRank),
                TotemDonation = (int)user.SyndicateMember.ArsenalDonation
            };
            for (var type = TotemPoleType.Headgear; type < TotemPoleType.None; type++)
            {
                if (totemPoles.TryGetValue(type, out TotemPole pole))
                {
                    msg.Items.Add(new MsgTotemPoleInfo.TotemPoleStruct
                    {
                        BattlePower = pole.BattlePower,
                        Enhancement = pole.Enhancement,
                        Donation = pole.Donation,
                        Open = !pole.Locked,
                        Type = (int)type
                    });
                }
                else
                {
                    msg.Items.Add(new MsgTotemPoleInfo.TotemPoleStruct
                    {
                        Type = (int)type
                    });
                }
            }
            return user.SendAsync(msg);
        }

        public Task SendTotemsAsync(Character user, TotemPoleType type, int index)
        {
            if (!totemPoles.TryGetValue(type, out TotemPole pole))
            {
                return Task.CompletedTask;
            }

            var msg = new MsgWeaponsInfo
            {
                Data1 = index - 1,
                Donation = pole.GetUserContribution(user.Identity),
                Enhancement = pole.Enhancement,
                EnhancementExpiration = uint.Parse(pole.EnhancementExpiration?.ToString("yyyyMMdd") ?? "0"),
                SharedBattlePower = pole.BattlePower,
                TotalInscribed = pole.Totems.Count,
                TotemType = (int)type
            };

            int position = index;
            foreach (TotemItem item in pole.Totems.Values.OrderByDescending(x => x.Points)
                                       .Skip(index - 1)
                                       .Take(8))
            {
                msg.Items.Add(new MsgWeaponsInfo.TotemPoleStruct
                {
                    Donation = item.Points,
                    Type = item.ItemType,
                    BattlePower = 0,
                    Position = position++,
                    SocketOne = (byte)item.SocketOne,
                    SocketTwo = (byte)item.SocketTwo,
                    Addition = item.Addition,
                    ItemIdentity = item.ItemIdentity,
                    PlayerName = item.PlayerName,
                    Quality = item.Quality
                });
            }

            msg.Data2 += msg.Data1 - 1;
            return user.SendAsync(msg);
        }

        private void UpdateBattlePower()
        {
            SharedBattlePower = totemPoles.Values
                                                 .Where(x => !x.Locked)
                                                 .OrderByDescending(x => x.SharedBattlePower)
                                                 .Take(5)
                                                 .Sum(x => x.SharedBattlePower);
        }

        private Task SynchronizeBattlePowerAsync()
        {
            return syndicate.SynchronizeBattlePowerAsync();
        }

        public static int TotemLimit(int level, int metempsychosis)
        {
            if (metempsychosis == 0)
            {
                if (level < 100)
                {
                    return 7;
                }

                return 14;
            }

            if (metempsychosis == 1)
            {
                return 21;
            }

            return 30;
        }

        public static TotemPoleType GetTotemPoleType(uint type)
        {
            if (Item.IsHelmet(type))
            {
                return TotemPoleType.Headgear;
            }

            if (Item.IsArmor(type) && type / 1000 != 137)
            {
                return TotemPoleType.Armor;
            }

            if (Item.IsWeapon(type) || Item.IsShield(type))
            {
                return TotemPoleType.Weapon;
            }

            if (Item.IsRing(type) || Item.IsBangle(type))
            {
                return TotemPoleType.Ring;
            }

            if (Item.IsShoes(type))
            {
                return TotemPoleType.Boots;
            }

            if (Item.IsNeck(type))
            {
                return TotemPoleType.Necklace;
            }

            if (Item.IsAttackTalisman(type))
            {
                return TotemPoleType.Fan;
            }

            if (Item.IsDefenseTalisman(type))
            {
                return TotemPoleType.Tower;
            }

            return TotemPoleType.None;
        }

        public int UnlockTotemPolePrice()
        {
            switch (totemPoles.Count(x => !x.Value.Locked))
            {
                case 0:
                case 1: return 5000000;
                case 2:
                case 3:
                case 4: return 10000000;
                case 5:
                case 6: return 15000000;
                case 7: return 20000000;
            }

            return 0;
        }
    }
}
