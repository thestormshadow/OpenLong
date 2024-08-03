using Long.Database.Entities;
using Long.Game.Database.Repositories;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Syndicate;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Syndicate.Network;
using Long.Shared;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicate;
using static Long.Kernel.Modules.Systems.Syndicate.ISyndicateMember;
using static Long.Kernel.StrRes;

namespace Long.Module.Syndicate.Managers
{
    public sealed class SyndicateManager : ISyndicateManager
    {
        private static readonly ILogger logger = Log.ForContext<SyndicateManager>();
        private readonly TimeOut syndicateAdvertiseCheckTimer = new();
        private readonly ConcurrentDictionary<ushort, ISyndicate> syndicates = new();
        private readonly ConcurrentDictionary<uint, DbSynAdvertisingInfo> synAdvertisingInfos = new();

        public async Task<bool> InitializeAsync()
        {
            syndicateAdvertiseCheckTimer.Startup(60);

            List<DbSyndicate> dbSyndicates = await SyndicateRepository.GetAsync();
            foreach (DbSyndicate dbSyn in dbSyndicates)
            {
                var syn = new States.Syndicate();
                if (!await syn.CreateAsync(dbSyn))
                {
                    continue;
                }

                syndicates.TryAdd(syn.Identity, syn);
            }

            foreach (ISyndicate syndicate in syndicates.Values)
            {
                syndicate.LoadRelation();
            }

            var advertisings = await SyndicateRepository.GetAdvertiseAsync();
            foreach (var adv in advertisings)
            {
                synAdvertisingInfos.TryAdd(adv.IdSyn, adv);
            }

            return true;
        }

        public bool AddSyndicate(ISyndicate syndicate)
        {
            return syndicates.TryAdd(syndicate.Identity, syndicate);
        }

        public ISyndicate GetSyndicate(int idSyndicate)
        {
            return syndicates.TryGetValue((ushort)idSyndicate, out ISyndicate syn) ? syn : null;
        }

        public ISyndicate GetSyndicate(string name)
        {
            return syndicates.Values.FirstOrDefault(
                x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        ///     Find the syndicate a user is in.
        /// </summary>
        public ISyndicate FindByUser(uint idUser)
        {
            return syndicates.Values.FirstOrDefault(x => x.QueryMember(idUser) != null);
        }

        public ISyndicate GetSyndicate(uint ownerIdentity)
        {
            return GetSyndicate((ushort)ownerIdentity);
        }

        public bool HasSyndicateAdvertise(uint idSyn)
        {
            return synAdvertisingInfos.ContainsKey(idSyn);
        }

        public async Task JoinByAdvertisingAsync(Character user, ushort syndicateIdentity)
        {
            if (user.Syndicate != null)
            {
                return;
            }

            if (!synAdvertisingInfos.TryGetValue(syndicateIdentity, out var advertising))
            {
                return;
            }

            if (advertising.ConditionMetem > user.Metempsychosis)
            {
                return;
            }

            if (advertising.ConditionLevel > user.Level)
            {
                return;
            }

            ProfessionPermission denyProfession = (ProfessionPermission)advertising.ConditionProf;
            if (user.ProfessionSort == 10 && denyProfession.HasFlag(ProfessionPermission.Trojan))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 20 && denyProfession.HasFlag(ProfessionPermission.Warrior))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 40 && denyProfession.HasFlag(ProfessionPermission.Archer))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 50 && denyProfession.HasFlag(ProfessionPermission.Ninja))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 60 && denyProfession.HasFlag(ProfessionPermission.Monk))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 70 && denyProfession.HasFlag(ProfessionPermission.Pirate))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }
            else if (user.ProfessionSort == 100 && denyProfession.HasFlag(ProfessionPermission.Taoist))
            {
                await user.SendAsync(StrSynRecruitNotAllowProfession);
                return;
            }

            ISyndicate targetSyndicate = GetSyndicate(syndicateIdentity);
            if (targetSyndicate == null)
            {
                return;
            }

            await targetSyndicate.AppendMemberAsync(user, null, ISyndicate.JoinMode.Recruitment);
        }

        public async Task SubmitEditAdvertiseScreenAsync(Character user)
        {
            if (user.Syndicate == null || user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return;
            }

            if (!synAdvertisingInfos.TryGetValue(user.SyndicateIdentity, out var adv))
            {
                return;
            }

            await user.SendAsync(new MsgSynRecuitAdvertising()
            {
                Identity = user.SyndicateIdentity,
                Description = adv.Content[..Math.Min(255, adv.Content.Length)],
                Silvers = adv.Expense,
                AutoRecruit = adv.AutoRecruit != 0,
                RequiredLevel = adv.ConditionLevel,
                RequiredMetempsychosis = adv.ConditionMetem,
                ForbidGender = adv.ConditionSex,
                ForbidProfession = adv.ConditionProf
            });
        }

        public async Task PublishAdvertisingAsync(Character user,
            long money,
            string description,
            int requiredLevel,
            int requiredMetempsychosis,
            int requiredProfession,
            int conditionBattle, // not sure
            int conditionSex,
            bool autoJoin)
        {
            if (user.Syndicate == null)
            {
                return;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return;
            }

            if (synAdvertisingInfos.TryGetValue(user.SyndicateIdentity, out _))
            {
                await ReplaceAdvertisingAsync(user, money, description, requiredLevel, requiredMetempsychosis, requiredProfession, conditionBattle, conditionSex, autoJoin);
                return;
            }

            requiredLevel = Math.Max(1, Math.Min(ExperienceManager.GetLevelLimit(), requiredLevel));
            requiredMetempsychosis = Math.Max(0, Math.Min(2, requiredMetempsychosis));

            if (user.Syndicate.Money < money)
            {
                return;
            }

            if (user.Syndicate.Money < ISyndicate.SYNDICATE_ACTION_COST)
            {
                return;
            }

            DbSynAdvertisingInfo dbInfo = new()
            {
                IdSyn = user.SyndicateIdentity,
                Content = description,
                Expense = (uint)money,
                AutoRecruit = (byte)(autoJoin ? 1 : 0),
                ConditionLevel = (byte)requiredLevel,
                ConditionMetem = (byte)requiredMetempsychosis,
                ConditionProf = (ushort)requiredProfession,
                ConditionBattle = (ushort)conditionBattle,
                ConditionSex = (byte)conditionSex,
                EndDate = (uint)UnixTimestamp.FromDateTime(DateTime.Now.AddDays(7))
            };

            if (await ServerDbContext.CreateAsync(dbInfo)
                && synAdvertisingInfos.TryAdd(user.SyndicateIdentity, dbInfo))
            {
                user.Syndicate.Money -= money;
                await user.Syndicate.SaveAsync();
            }

            await user.SendAsync(new MsgSynRecruitAdvertisingList());
        }

        public async Task ReplaceAdvertisingAsync(Character user,
            long money,
            string description,
            int requiredLevel,
            int requiredMetempsychosis,
            int requiredProfession,
            int conditionBattle, // not sure
            int conditionSex,
            bool autoJoin)
        {
            if (user.Syndicate == null)
            {
                return;
            }

            if (user.SyndicateRank != SyndicateRank.GuildLeader)
            {
                return;
            }

            if (!synAdvertisingInfos.TryGetValue(user.SyndicateIdentity, out var advertise))
            {
                return;
            }

            if (money < advertise.Expense)
            {
                return;
            }

            requiredLevel = Math.Max(1, Math.Min(ExperienceManager.GetLevelLimit(), requiredLevel));
            requiredMetempsychosis = Math.Max(0, Math.Min(2, requiredMetempsychosis));

            if (user.Syndicate.Money < money)
            {
                return;
            }

            if (user.Syndicate.Money < ISyndicate.SYNDICATE_ACTION_COST)
            {
                return;
            }

            bool spendMoney = money != advertise.Expense;
            if (spendMoney)
            {
                advertise.Expense = (uint)money;
                advertise.EndDate = (uint)UnixTimestamp.FromDateTime(DateTime.Now.AddDays(7));
            }

            advertise.Content = description;
            advertise.AutoRecruit = (byte)(autoJoin ? 1 : 0);
            advertise.ConditionLevel = (byte)requiredLevel;
            advertise.ConditionMetem = (byte)requiredMetempsychosis;
            advertise.ConditionProf = (ushort)requiredProfession;
            advertise.ConditionBattle = (ushort)conditionBattle;
            advertise.ConditionSex = (byte)conditionSex;

            if (await ServerDbContext.UpdateAsync(advertise))
            {
                if (spendMoney)
                {
                    user.Syndicate.Money -= money;
                    await user.Syndicate.SaveAsync();
                }
            }
        }

        public async Task SubmitAdvertisingListAsync(Character user, int startIndex)
        {
            const int ipp = 4;
            MsgSynRecruitAdvertisingList msg = new()
            {
                StartIndex = startIndex,
                TotalRecords = synAdvertisingInfos.Count,
                CurrentPageIndex = 0
            };
            foreach (var adv in synAdvertisingInfos.Values
                .OrderByDescending(x => x.Expense)
                .Skip(startIndex)
                .Take(ipp))
            {
                ISyndicate syn = GetSyndicate(adv.IdSyn);
                string synName = syn?.Name ?? $"Error{adv.IdSyn}";
                string leaderName = syn?.Leader?.UserName ?? "Error";
                int memberCount = syn?.MemberCount ?? 0;
                long money = syn?.Money ?? 0;
                int level = syn?.Level ?? 1;

                msg.Advertisings.Add(new MsgSynRecruitAdvertisingList.AdvertisingStruct
                {
                    Identity = adv.IdSyn,
                    Description = adv.Content,
                    Name = synName,
                    LeaderName = leaderName,
                    Count = memberCount,
                    Funds = money,
                    Level = level,
                    AutoJoin = adv.AutoRecruit != 0,
                    DenyProfession = 0
                });

                if (msg.Advertisings.Count >= 2)
                {
                    await user.SendAsync(msg);
                    msg.Advertisings.Clear();
                    msg.CurrentPageIndex++;
                }
            }

            if (msg.Advertisings.Count > 0)
            {
                await user.SendAsync(msg);
            }
        }

        public async Task OnTimerAsync()
        {
            if (syndicateAdvertiseCheckTimer.ToNextTime())
            {
                foreach (var adv in synAdvertisingInfos.Where(x => x.Value.EndDate < UnixTimestamp.Now))
                {
                    synAdvertisingInfos.TryRemove(adv.Key, out _);
                }
            }
        }

        public async Task<bool> CreateSyndicateAsync(Character user, string name, int price = 1000000)
        {
            if (user.Syndicate != null)
            {
                await user.SendAsync(StrSynAlreadyJoined);
                return false;
            }

            if (name.Length > 15)
            {
                return false;
            }

            if (!RoleManager.IsValidName(name))
            {
                return false;
            }

            if (GetSyndicate(name) != null)
            {
                await user.SendAsync(StrSynNameInUse);
                return false;
            }

            if (user.Silvers < (uint)price)
            {
                await user.SendAsync(StrNotEnoughMoney);
                return false;
            }

            user.Syndicate = new States.Syndicate();
            if (!await user.Syndicate.CreateAsync(name, price, user))
            {
                user.Syndicate = null;
                return false;
            }

            if (!AddSyndicate(user.Syndicate))
            {
                await user.Syndicate.DeleteAsync();
                user.Syndicate = null;
                return false;
            }

            await user.SpendMoneyAsync(price);

            await RoleManager.BroadcastWorldMsgAsync(string.Format(StrSynCreate, user.Name, name), TalkChannel.Talk,
                                                Color.White);
            await States.Syndicate.SendSyndicateAsync(user);
            await user.Screen.SynchroScreenAsync();
            await user.Syndicate.BroadcastNameAsync();
            return true;
        }

        public async Task<bool> ChangeSyndicateNameAsync(Character user, string newName)
        {
            if (user.Syndicate == null)
            {
                return false;
            }

            if (newName.Length > 15)
            {
                return false;
            }

            if (!RoleManager.IsValidName(newName))
            {
                return false;
            }

            if (GetSyndicate(newName) != null)
            {
                await user.SendAsync(StrSynNameInUse);
                return false;
            }

            await user.Syndicate.ChangeNameAsync(newName);
            return true;
        }

        public async Task<bool> DisbandSyndicateAsync(Character user)
        {
            if (user.SyndicateIdentity == 0)
            {
                return false;
            }

            if (user.Syndicate.Leader.UserIdentity != user.Identity)
            {
                return false;
            }

            if (user.Syndicate.MemberCount > 1)
            {
                await user.SendAsync(StrSynNoDisband);
                return false;
            }

            if (user.Syndicate.LeagueId != 0)
            {
                await user.SendAsync(StrSynInLeague);
                return false;
            }

            return await user.Syndicate.DisbandAsync(user);
        }

        public List<ISyndicate> GetByLeague(uint leagueId)
        {
            return syndicates.Values.Where(x => x.LeagueId == leagueId).ToList();
        }
    }
}
