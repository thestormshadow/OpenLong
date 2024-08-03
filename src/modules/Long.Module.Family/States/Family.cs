using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Family;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Family.Managers;
using Long.Module.Family.Network;
using Long.Module.Family.Repositories;
using Long.Network.Packets;
using Long.Shared;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Modules.Systems.Family.IFamily;
using static Long.Kernel.StrRes;

namespace Long.Module.Family.States
{
    public sealed class Family : IFamily
    {
        public const int MAX_MEMBERS = 6;
        public const int MAX_RELATION = 5;

        private string name;
        private DbFamily mFamily;
        private readonly ConcurrentDictionary<uint, IFamilyMember> mMembers = new();
        private readonly ConcurrentDictionary<uint, IFamily> mAllies = new();
        private readonly ConcurrentDictionary<uint, IFamily> mEnemies = new();

        #region Static Creation

        public static async Task<Family> CreateAsync(Character leader, string name, uint money)
        {
            var dbFamily = new DbFamily
            {
                Announcement = StrFamilyDefaultAnnounce,
                Amount = 1,
                CreationDate = (uint)DateTime.Now.ToUnixTimestamp(),
                LeaderIdentity = leader.Identity,
                Money = money,
                Name = name,
                Rank = 0,
                AllyFamily0 = 0,
                AllyFamily1 = 0,
                AllyFamily2 = 0,
                AllyFamily3 = 0,
                AllyFamily4 = 0,
                EnemyFamily0 = 0,
                EnemyFamily1 = 0,
                EnemyFamily2 = 0,
                EnemyFamily3 = 0,
                EnemyFamily4 = 0,
                Challenge = 0,
                ChallengeMap = 0,
                CreateName = "",
                FamilyMap = 0,
                Occupy = 0,
                Repute = 0,
                StarTower = 0
            };

            if (!await ServerDbContext.CreateAsync(dbFamily))
            {
                return null;
            }

            var family = new Family
            {
                mFamily = dbFamily,
                name = name,
            };

            var fmLeader = await FamilyMember.CreateAsync(leader, family, FamilyRank.ClanLeader, money);
            if (fmLeader == null)
            {
                await ServerDbContext.DeleteAsync(dbFamily);
                return null;
            }

            family.mMembers.TryAdd(fmLeader.Identity, fmLeader);
            ModuleManager.FamilyManager.AddFamily(family);

            await ModuleManager.OnFamilyJoinAsync(leader, family);

            await family.SendFamilyAsync(leader);
            await family.SendRelationsAsync(leader);

            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrFamilyCreate, leader.Name, family.Name), TalkChannel.Talk, Color.White);
            return family;
        }

        public static async Task<Family> CreateAsync(DbFamily dbFamily)
        {
            var family = new Family { mFamily = dbFamily, name = dbFamily.Name };

            List<DbFamilyAttr> members = await FamilyAttrRepository.GetAsync(family.Identity);
            if (members == null)
            {
                return null;
            }

            foreach (DbFamilyAttr dbMember in members.OrderByDescending(x => x.Rank))
            {
                var member = await FamilyMember.CreateAsync(dbMember, family);
                if (member == null)
                {
                    await ServerDbContext.DeleteAsync(dbMember);
                    continue;
                }

                family.mMembers.TryAdd(member.Identity, member);
            }

            // validate our members
            foreach (FamilyMember member in family.mMembers.Values.Where(x => x.Rank == FamilyRank.Spouse))
            {
                IFamilyMember mate = family.GetMember(member.MateIdentity);
                if (mate == null || mate.Rank == FamilyRank.Spouse)
                {
                    family.mMembers.TryRemove(member.Identity, out _);
                    await member.DeleteAsync();
                }
            }

            return family;
        }

        #endregion

        #region Properties

        public uint Identity => mFamily.Identity;
        public string Name => name;
        public int MembersCount => mMembers.Count;
        public int PureMembersCount => mMembers.Count(x => x.Value.Rank != FamilyRank.Spouse);
        public bool IsDeleted => mFamily.DeleteDate != 0;

        public uint LeaderIdentity => mFamily.LeaderIdentity;
        public IFamilyMember Leader => mMembers.TryGetValue(LeaderIdentity, out var value) ? value : null;

        public ulong Money
        {
            get => mFamily.Money;
            set => mFamily.Money = value;
        }

        public byte Rank
        {
            get => mFamily.Rank;
            set => mFamily.Rank = value;
        }

        public uint Reputation
        {
            get => mFamily.Repute;
            set => mFamily.Repute = value;
        }

        public string Announcement
        {
            get => mFamily.Announcement;
            set => mFamily.Announcement = value;
        }

        public byte BattlePowerTower
        {
            get => mFamily.StarTower;
            set => mFamily.StarTower = value;
        }

        public DateTime CreationDate => UnixTimestamp.ToDateTime(mFamily.CreationDate);

        #endregion

        #region Clan War

        public uint Challenge
        {
            get => mFamily.Challenge;
            set => mFamily.Challenge = value;
        }

        public uint Occupy
        {
            get => mFamily.Occupy;
            set => mFamily.Occupy = value;
        }

        public string GetOccupyString(Character user)
        {
            var war = FamilyWarManager.Instance;
            if (war == null)
            {
                return "0 0 0 0 0 0 0 0";
            }

            uint idNpc = war.GetDominatingNpc(this)?.Identity ?? 0;
            return "0 " +
                   $"{war.GetFamilyOccupyDays(Identity)} " +
                   $"{war.GetNextReward(idNpc)} " +
                   $"{war.GetNextWeekReward(idNpc)} " +
                   $"{(war.IsNpcChallenged(Occupy) ? 1 : 0)} " +
                   $"{(war.HasRewardToClaim(user) ? 1 : 0)} " +
                   $"{(user.Level < ExperienceManager.GetLevelLimit() && war.HasExpToClaim(user) ? 1 : 0)}";
        }

        public string DominatedMap 
        { 
            get => FamilyWarManager.Instance?.GetMapByNpc(Occupy)?.Name ?? "";
        }

        public string ChallengedMap 
        { 
            get => FamilyWarManager.Instance?.GetMapByNpc(Challenge)?.Name ?? ""; 
        }

        #endregion

        #region Members

        public int SharedBattlePowerFactor
        {
            get
            {
                switch (BattlePowerTower)
                {
                    case 1: return 40;
                    case 2: return 50;
                    case 3: return 60;
                    case 4: return 70;
                    default:
                        return 30;
                }
            }
        }

        public IFamilyMember GetMember(uint idMember)
        {
            return mMembers.TryGetValue(idMember, out var value) ? value : null;
        }

        public IFamilyMember GetMember(string name)
        {
            return mMembers.Values.FirstOrDefault(x => x.Name.Equals(name));
        }

        public async Task<bool> AppendMemberAsync(Character user, Character target,
                                                  FamilyRank rank = FamilyRank.Member)
        {
            if (target.Family != null)
            {
                return false;
            }

            if (target.Level < 50 && rank != FamilyRank.Spouse)
            {
                return false;
            }

            if (mMembers.Values.Count(x => x.Rank != FamilyRank.Spouse) > MAX_MEMBERS && rank != FamilyRank.Spouse)
            {
                return false;
            }

            var member = await FamilyMember.CreateAsync(target, this, rank);
            if (member == null)
            {
                return false;
            }

            mMembers.TryAdd(member.Identity, member);

            target.Family = this;

            if (rank != FamilyRank.Spouse)
            {
                Character mateCharacter = RoleManager.GetUser(target.MateIdentity);
                if (mateCharacter != null)
                {
                    await AppendMemberAsync(user, mateCharacter, FamilyRank.Spouse);
                }
            }

            await ModuleManager.OnFamilyJoinAsync(user, this);

            await SendFamilyAsync(target);
            await target.SendRelationAsync(target);
            await target.Screen.SynchroScreenAsync();
            return true;
        }

        public async Task<bool> LeaveAsync(Character user)
        {
            if (user.Family == null)
            {
                return false;
            }

            if (user.FamilyRank == FamilyRank.ClanLeader)
            {
                return false;
            }

            if (user.FamilyRank == FamilyRank.Spouse)
            {
                return false;
            }

            if (mMembers.TryRemove(user.Identity, out var member))
            {
                await member.DeleteAsync();
            }

            await ModuleManager.OnFamilyExitAsync(user.Identity, this);

            user.Family = null;
            await ModuleManager.FamilyManager.SendNoFamilyAsync(user);
            await user.Screen.SynchroScreenAsync();

            if (user.MateIdentity != 0)
            {
                IFamilyMember mate = GetMember(user.MateIdentity);
                if (mate != null)
                {
                    await KickOutAsync(user, user.MateIdentity);
                }
            }

            return true;
        }

        public async Task<bool> KickOutAsync(Character caller, uint idTarget)
        {
            IFamilyMember target = GetMember(idTarget);
            if (target == null)
            {
                return false;
            }

            if (target.Rank == FamilyRank.ClanLeader)
            {
                return false;
            }

            if (caller.FamilyRank != FamilyRank.ClanLeader)
            {
                if (target.Rank != FamilyRank.Spouse || target.MateIdentity != caller.Identity)
                {
                    return false;
                }
            }

            mMembers.TryRemove(idTarget, out _);

            await ModuleManager.OnFamilyExitAsync(idTarget, this);

            Character targetUser = target.User;
            if (targetUser != null)
            {
                targetUser.Family = null;
                await ModuleManager.FamilyManager.SendNoFamilyAsync(targetUser);
                await targetUser.Screen.SynchroScreenAsync();
            }

            await target.DeleteAsync();

            IFamilyMember mate = GetMember(target.MateIdentity);
            if (mate != null && mate.Rank == FamilyRank.Spouse)
            {
                await KickOutAsync(caller, mate.Identity);
            }

            return true;
        }

        public async Task<bool> AbdicateAsync(Character caller, string targetName)
        {
            if (caller.FamilyRank != FamilyRank.ClanLeader)
            {
                return false;
            }

            IFamilyMember target = GetMember(targetName);
            if (target == null)
            {
                return false;
            }

            if (caller.Identity == target.Identity)
            {
                return false;
            }

            if (target.FamilyIdentity != Identity)
            {
                return false;
            }

            if (target.User == null)
            {
                return false; // not online
            }

            if (target.Rank == FamilyRank.Spouse)
            {
                return false; // cannot abdicate for a spouse
            }

            target.Rank = FamilyRank.ClanLeader;
            caller.FamilyMember.Rank = FamilyRank.Member;

            await target.SaveAsync();
            await caller.FamilyMember.SaveAsync();

            await SendFamilyAsync(target.User);
            await SendFamilyAsync(caller);

            await target.User.Screen.SynchroScreenAsync();
            await caller.Screen.SynchroScreenAsync();

            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrFamilyAbdicate, caller.Name, target.Name), TalkChannel.Family);
            return true;
        }

        #endregion

        #region Change Name

        public async Task<bool> ChangeNameAsync(string name)
        {
            this.mFamily.Name = name;
            return await SaveAsync();
        }

        #endregion

        #region Relations

        public void LoadRelations()
        {
            // Ally
            Family family = ModuleManager.FamilyManager.GetFamily(mFamily.AllyFamily0) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.AllyFamily0 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.AllyFamily1) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.AllyFamily1 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.AllyFamily2) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.AllyFamily2 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.AllyFamily3) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.AllyFamily3 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.AllyFamily4) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.AllyFamily4 = 0;
            }

            // Enemies
            family = ModuleManager.FamilyManager.GetFamily(mFamily.EnemyFamily0) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.EnemyFamily0 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.EnemyFamily1) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.EnemyFamily1 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.EnemyFamily2) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.EnemyFamily2 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.EnemyFamily3) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.EnemyFamily3 = 0;
            }

            family = ModuleManager.FamilyManager.GetFamily(mFamily.EnemyFamily4) as Family;
            if (family != null)
            {
                mAllies.TryAdd(family.Identity, family);
            }
            else
            {
                mFamily.EnemyFamily4 = 0;
            }
        }

        #endregion

        #region Allies

        public int AllyCount => mAllies.Count;

        public bool IsAlly(uint idAlly)
        {
            return mAllies.ContainsKey(idAlly);
        }

        public void SetAlly(IFamily ally)
        {
            uint idAlly = ally.Identity;

            if (mFamily.AllyFamily0 == 0)
            {
                mFamily.AllyFamily0 = idAlly;
            }
            else if (mFamily.AllyFamily1 == 0)
            {
                mFamily.AllyFamily1 = idAlly;
            }
            else if (mFamily.AllyFamily2 == 0)
            {
                mFamily.AllyFamily2 = idAlly;
            }
            else if (mFamily.AllyFamily3 == 0)
            {
                mFamily.AllyFamily3 = idAlly;
            }
            else if (mFamily.AllyFamily4 == 0)
            {
                mFamily.AllyFamily4 = idAlly;
            }
            else
            {
                return;
            }

            mAllies.TryAdd(idAlly, ally);
        }

        public void UnsetAlly(uint idAlly)
        {
            if (mFamily.AllyFamily0 == idAlly)
            {
                mFamily.AllyFamily0 = 0;
            }

            if (mFamily.AllyFamily1 == idAlly)
            {
                mFamily.AllyFamily1 = 0;
            }

            if (mFamily.AllyFamily2 == idAlly)
            {
                mFamily.AllyFamily2 = 0;
            }

            if (mFamily.AllyFamily3 == idAlly)
            {
                mFamily.AllyFamily3 = 0;
            }

            if (mFamily.AllyFamily4 == idAlly)
            {
                mFamily.AllyFamily4 = 0;
            }

            mAllies.TryRemove(idAlly, out _);
        }

        #endregion

        #region Enemies

        public int EnemyCount => mEnemies.Count;

        public bool IsEnemy(uint idEnemy)
        {
            return mEnemies.ContainsKey(idEnemy);
        }

        public void SetEnemy(IFamily enemy)
        {
            uint idEnemy = enemy.Identity;
            if (mFamily.EnemyFamily0 == 0)
            {
                mFamily.EnemyFamily0 = idEnemy;
            }
            else if (mFamily.EnemyFamily1 == 0)
            {
                mFamily.EnemyFamily1 = idEnemy;
            }
            else if (mFamily.EnemyFamily2 == 0)
            {
                mFamily.EnemyFamily2 = idEnemy;
            }
            else if (mFamily.EnemyFamily3 == 0)
            {
                mFamily.EnemyFamily3 = idEnemy;
            }
            else if (mFamily.EnemyFamily4 == 0)
            {
                mFamily.EnemyFamily4 = idEnemy;
            }
            else
            {
                return;
            }

            mEnemies.TryAdd(idEnemy, enemy);
        }

        public void UnsetEnemy(uint idEnemy)
        {
            if (mFamily.EnemyFamily0 == idEnemy)
            {
                mFamily.EnemyFamily0 = 0;
            }

            if (mFamily.EnemyFamily1 == idEnemy)
            {
                mFamily.EnemyFamily1 = 0;
            }

            if (mFamily.EnemyFamily2 == idEnemy)
            {
                mFamily.EnemyFamily2 = 0;
            }

            if (mFamily.EnemyFamily3 == idEnemy)
            {
                mFamily.EnemyFamily3 = 0;
            }

            if (mFamily.EnemyFamily4 == idEnemy)
            {
                mFamily.EnemyFamily4 = 0;
            }

            mEnemies.TryRemove(idEnemy, out _);
        }

        #endregion

        #region Socket

        public Task SendFamilyAsync(Character user)
        {
            if (mMembers.TryGetValue(user.Identity, out var member))
            {
                var msg = new MsgFamily
                {
                    Identity = Identity,
                    Action = MsgFamily.FamilyAction.Query
                };
                msg.Strings.Add(
                    $"{Identity} {MembersCount} {MembersCount} {Money} {Rank} {(int)user.FamilyRank} 0 {BattlePowerTower} 0 0 1 {member.Proffer}");
                msg.Strings.Add(Name);
                msg.Strings.Add(user.Name);
                msg.Strings.Add(GetOccupyString(user));
                msg.Strings.Add(DominatedMap);
                msg.Strings.Add(ChallengedMap);
                return user.SendAsync(msg);
            }
            return Task.CompletedTask;
        }

        public Task SendFamilyOccupyAsync(Character user)
        {
            if (mMembers.TryGetValue(user.Identity, out var member))
            {
                var msg = new MsgFamily
                {
                    Identity = Identity,
                    Action = MsgFamily.FamilyAction.QueryOccupy
                };
                // uid occupydays reward nextreward challenged rewardtoclaim exptoclaim
                msg.Strings.Add(GetOccupyString(user));
                return user.SendAsync(msg);
            }
            return Task.CompletedTask;
        }

        public Task SendMembersAsync(int idx, Character target)
        {
            if (target.FamilyIdentity != Identity)
            {
                return Task.CompletedTask;
            }

            var msg = new MsgFamily
            {
                Identity = Identity,
                Action = MsgFamily.FamilyAction.QueryMemberList
            };

            foreach (FamilyMember member in mMembers.Values.OrderByDescending(x => x.IsOnline)
                                                     .ThenByDescending(x => x.Rank))
            {
                msg.Objects.Add(new MsgFamily.MemberListStruct
                {
                    Profession = member.Profession,
                    Donation = member.Proffer,
                    Name = member.Name,
                    Rank = (ushort)member.Rank,
                    Level = member.Level,
                    Online = member.IsOnline
                });
            }

            return target.SendAsync(msg);
        }

        public async Task SendRelationsAsync()
        {
            foreach (FamilyMember member in mMembers.Values.Where(x => x.IsOnline))
            {
                await SendRelationsAsync(member.User);
            }
        }

        public async Task SendRelationsAsync(Character target)
        {
            var msg = new MsgFamily
            {
                Identity = Identity,
                Action = MsgFamily.FamilyAction.SendAlly
            };
            foreach (Family ally in mAllies.Values)
            {
                msg.Objects.Add(new MsgFamily.RelationListStruct
                {
                    Name = ally.Name,
                    LeaderName = ally.Leader.Name
                });
            }

            await target.SendAsync(msg);

            msg = new MsgFamily
            {
                Identity = Identity,
                Action = MsgFamily.FamilyAction.SendEnemy
            };
            foreach (Family enemy in mEnemies.Values)
            {
                msg.Objects.Add(new MsgFamily.RelationListStruct
                {
                    Name = enemy.Name,
                    LeaderName = enemy.Leader.Name
                });
            }

            await target.SendAsync(msg);
        }

        public async Task SendAsync(string message, uint idIgnore = 0u, Color? color = null)
        {
            await SendAsync(new MsgTalk(TalkChannel.Family, color ?? Color.White, message), idIgnore);
        }

        public async Task SendAsync(IPacket msg, uint exclude = 0u)
        {
            foreach (FamilyMember player in mMembers.Values)
            {
                if (exclude == player.Identity || player.User == null)
                {
                    continue;
                }

                await player.User.SendAsync(msg);
            }
        }

        #endregion

        #region Database

        public Task<bool> SaveAsync()
        {
            return ServerDbContext.UpdateAsync(mFamily);
        }

        public Task<bool> SoftDeleteAsync()
        {
            mFamily.DeleteDate = 1;
            return SaveAsync();
        }

        #endregion
    }
}
