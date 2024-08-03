using Long.Database.Entities;
using Long.Kernel;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.Modules.Systems.Peerage;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Peerage.Network;
using Long.Module.Peerage.Repositories;
using Serilog;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Module.Peerage.Network.MsgNobility;

namespace Long.Module.Peerage.Managers
{
    public class NobilityManager : INobilityManager
    {
        private static readonly ILogger logger = Log.ForContext<NobilityManager>();
        private ConcurrentDictionary<uint, NobilityData> nobilityRecords = new();

        public async Task InitializeAsync()
        {
            foreach (var nobilityRecord in NobilityRepository.Get())
            {
                DbUser user = await UserRepository.FindByIdentityAsync(nobilityRecord.UserId);
                if (user == null)
                {
                    await NobilityRepository.DeleteAsync(nobilityRecord);
                    continue;
                }

                var nobilityData = new NobilityData(nobilityRecord, user);
                nobilityRecords.TryAdd(nobilityData.Identity, nobilityData);
            }
        }

        public async Task DonateAsync(Character user, ulong amount)
        {
            int oldPosition = GetPosition(user.Identity);
            NobilityRank oldRank = GetRanking(user.Identity);

            amount = Math.Min(amount, 10_000_000_000);

            if (!nobilityRecords.TryGetValue(user.Identity, out NobilityData peerage))
            {
                peerage = new NobilityData(new DbNobility
                {
                    UserId = user.Identity,
                    Value = user.Nobility.Donation + amount,
                    RankType = 3_000_003
                }, user);

                nobilityRecords.TryAdd(user.Identity, peerage);
            }
            else
            {
                peerage.Donation += amount;
            }

            await peerage.SaveAsync();

            user.Nobility.Donation = peerage.Donation;
            await user.SaveAsync();

            NobilityRank rank = GetRanking(user.Identity);
            int position = GetPosition(user.Identity);

            await user.Nobility.BroadcastAsync();

            if (position != oldPosition && position < 50)
            {
                foreach (NobilityData peer in nobilityRecords.Values
                    .Where(x => x.Donation > 0)
                    .OrderByDescending(z => z.Donation))
                {
                    Character targetUser = RoleManager.GetUser(peer.Identity);
                    if (targetUser != null)
                    {
                        await targetUser.Nobility.BroadcastAsync();
                    }
                }
            }

            if (rank != oldRank)
            {
                string message = "";
                switch (rank)
                {
                    case NobilityRank.King:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptKing, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptQueen, user.Name);
                            }
                            break;
                        }

                    case NobilityRank.Prince:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptPrince, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptPrincess, user.Name);
                            }
                            break;
                        }

                    case NobilityRank.Duke:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptDuke, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptDuchess, user.Name);
                            }
                            break;
                        }

                    case NobilityRank.Earl:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptEarl, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptCountess, user.Name);
                            }
                            break;
                        }

                    case NobilityRank.Baron:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptBaron, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptBaroness, user.Name);
                            }
                            break;
                        }

                    case NobilityRank.Knight:
                        {
                            if (user.Gender == 1)
                            {
                                message = string.Format(StrRes.StrPeeragePromptKnight, user.Name);
                            }
                            else
                            {
                                message = string.Format(StrRes.StrPeeragePromptLady, user.Name);
                            }
                            break;
                        }
                }

                await RoleManager.BroadcastWorldMsgAsync(message, TalkChannel.Center, Color.Red);
            }
        }

		public async Task ChangeNameAsync(Character user, string newName)
		{
			if (nobilityRecords.TryGetValue(user.Identity, out var peerage))
			{
				peerage.Name = newName;
			}
		}

		public NobilityRank GetRanking(uint idUser)
        {
            int position = GetPosition(idUser);
            if (position >= 0 && position < 3)
            {
                return NobilityRank.King;
            }

            if (position >= 3 && position < 15)
            {
                return NobilityRank.Prince;
            }

            if (position >= 15 && position < 50)
            {
                return NobilityRank.Duke;
            }

            NobilityData peerageUser = GetUser(idUser);
            ulong donation = 0;
            if (peerageUser != null)
            {
                donation = peerageUser.Donation;
            }
            else
            {
                Character user = RoleManager.GetUser(idUser);
                if (user != null)
                {
                    donation = user.Nobility.Donation;
                }
            }

            if (donation >= 200000000)
            {
                return NobilityRank.Earl;
            }

            if (donation >= 100000000)
            {
                return NobilityRank.Baron;
            }

            if (donation >= 30000000)
            {
                return NobilityRank.Knight;
            }

            return NobilityRank.Serf;
        }

        public int GetPosition(uint idUser)
        {
            int idx = -1;
            foreach (NobilityData peerage in nobilityRecords.Values.OrderByDescending(x => x.Donation).Take(50))
            {
                idx++;
                if (peerage.Identity == idUser)
                {
                    return idx;
                }
            }

            return -1;
        }

        public async Task SendRankingAsync(Character target, int page)
        {
            if (target == null)
            {
                return;
            }

            const int MAX_PER_PAGE_I = 10;
            const int MAX_PAGES = 5;

            int currentPagesNum = Math.Max(1, Math.Min(nobilityRecords.Count / MAX_PER_PAGE_I + 1, MAX_PAGES));
            if (page >= currentPagesNum)
            {
                return;
            }

            int min = page * MAX_PER_PAGE_I;
            int max = page * MAX_PER_PAGE_I + MAX_PER_PAGE_I;
            int current = min;
            if (min >= MAX_PAGES * MAX_PER_PAGE_I)
            {
                return;
            }

            var rank = new List<NobilityStruct>();
            foreach (NobilityData peerage in nobilityRecords.Values.OrderByDescending(x => x.Donation)
                .Skip(min)
                .Take(MAX_PER_PAGE_I))
            {
                Character peerageUser = RoleManager.GetUser(peerage.Identity);
                uint lookface = peerageUser?.Mesh ?? 0;
                rank.Add(new NobilityStruct
                {
                    Identity = peerage.Identity,
                    Name = peerage.Name,
                    Donation = peerage.Donation,
                    LookFace = lookface,
                    Position = current,
                    Rank = GetRanking(peerage.Identity)
                });
                current++;
            }

            var msg = new MsgNobility(NobilityAction.List, (ushort)Math.Min(MAX_PER_PAGE_I, rank.Count), (ushort)currentPagesNum);
            msg.Rank.AddRange(rank);
            await target.SendAsync(msg);
        }

        private NobilityData GetUser(uint idUser)
        {
            return nobilityRecords.TryGetValue(idUser, out NobilityData peerage) ? peerage : null;
        }

        public ulong GetNextRankSilver(NobilityRank rank, ulong donation)
        {
            switch (rank)
            {
                case NobilityRank.Knight: return 30000000 - donation;
                case NobilityRank.Baron: return 100000000 - donation;
                case NobilityRank.Earl: return 200000000 - donation;
                case NobilityRank.Duke: return GetDonation(50) - donation;
                case NobilityRank.Prince: return GetDonation(15) - donation;
                case NobilityRank.King: return GetDonation(3) - donation;
                default: return 0;
            }
        }

        public ulong GetDonation(int position)
        {
            var ranking = 1;
            ulong donation = 0;
            foreach (NobilityData peerage in nobilityRecords.Values.OrderByDescending(x => x.Donation))
            {
                donation = peerage.Donation;
                if (ranking++ == position)
                {
                    break;
                }
            }

            return Math.Max(3000000, donation);
        }

        public async Task SaveAsync()
        {
            foreach (NobilityData peerage in nobilityRecords.Values)
            {
                await peerage.SaveAsync();
            }
        }

        public class NobilityData
        {
            private readonly DbNobility nobility;

            public NobilityData(DbNobility nobility, DbUser user)
            {
                this.nobility = nobility;
                Name = user.Name;
                Lookface = user.Mesh;
            }

            public NobilityData(DbNobility nobility, Character user)
            {
                this.nobility = nobility;
                Name = user.Name;
                Lookface = user.Mesh;
            }

            public uint Identity => nobility.UserId;
            public string Name { get; set; }
            public uint Lookface { get; }
            public ulong Donation
            {
                get => nobility.Value;
                set => nobility.Value = value;
            }

            public Task SaveAsync()
            {
                if (nobility.Id == 0)
                {
                    return NobilityRepository.CreateAsync(nobility);
                }
                return NobilityRepository.UpdateAsync(nobility);
            }

            public Task DeleteAsync()
            {
                return NobilityRepository.DeleteAsync(nobility);
            }
        }
    }
}
