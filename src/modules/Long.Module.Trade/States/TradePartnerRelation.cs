using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Trade;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Trade.Network;
using Long.Module.Trade.Repositories;
using Long.Shared;
using Serilog;
using System.Collections.Concurrent;
using static Long.Kernel.StrRes;

namespace Long.Module.Trade.States
{
    public sealed class TradePartnerRelation : ITradePartnerRelation
    {
        private const int TRADE_BUDDY_LIMIT = 20;
        private const int TRADE_BUDDY_WAIT_HOURS = 72;

        private static readonly ILogger logger = Log.ForContext<TradePartnerRelation>();

        private readonly ConcurrentDictionary<uint, TradeBuddy> business;
        private readonly Character user;

        public TradePartnerRelation(Character user)
        {
            this.business = new();
            this.user = user;
        }

        public int Amount => business.Count;

        public async Task<bool> InitializeAsync()
        {
            var businessList = await BusinessRepository.GetByUserIdAsync(user.Identity);
            foreach (var buss in businessList)
            {
                TradeBuddy tradeBuddy = new TradeBuddy(user, buss);
                if (!await tradeBuddy.InitializeAsync())
                {
                    await BusinessRepository.DeleteAsync(tradeBuddy);
                    continue;
                }

                if (!business.TryAdd(tradeBuddy.TargetId, tradeBuddy))
                {
                    logger.Information("Could not register {0} on trade partner dictionary");
                }

            }
            return true;
        }

        public async Task CreateRelationAsync(Character target)
        {
            if (user == null || target == null)
            {
                return;
            }

            if (IsTradePartner(target.Identity) || target.TradePartnerRelation.IsTradePartner(user.Identity))
            {
                return;
            }

            if (business.Count >= TRADE_BUDDY_LIMIT)
            {
                return;
            }

            var dbBusiness = new DbBusiness
            {
                UserId = user.Identity,
                BusinessId = target.Identity,
                Date = (uint)DateTime.Now.AddHours(TRADE_BUDDY_WAIT_HOURS).ToUnixTimestamp()
            };
            await BusinessRepository.CreateAsync(dbBusiness);

            TradeBuddy userBuddy = new TradeBuddy(user, dbBusiness);
            userBuddy.Initialize(target);

            TradeBuddy targetBuddy = new TradeBuddy(target, dbBusiness);
            targetBuddy.Initialize(user);

            business.TryAdd(userBuddy.TargetId, userBuddy);

            if (target.TradePartnerRelation is TradePartnerRelation targetRelation)
            {
                targetRelation.business.TryAdd(targetBuddy.TargetId, targetBuddy);
            }

            await user.SendAsync(new MsgTradeBuddy
            {
                Name = userBuddy.TargetName,
                Action = MsgTradeBuddy.TradeBuddyAction.AddPartner,
                IsOnline = userBuddy.Target != null,
                HoursLeft = userBuddy.RemainingMinutes,
                Identity = userBuddy.TargetId,
                Level = userBuddy.TargetLevel
            });

            await target.SendAsync(new MsgTradeBuddy
            {
                Name = targetBuddy.TargetName,
                Action = MsgTradeBuddy.TradeBuddyAction.AddPartner,
                IsOnline = targetBuddy.Target != null,
                HoursLeft = targetBuddy.RemainingMinutes,
                Identity = targetBuddy.TargetId,
                Level = targetBuddy.TargetLevel
            });

            await user.BroadcastRoomMsgAsync(string.Format(StrTradeBuddyAnnouncePartnership, user.Name, target.Name));
        }

        public bool IsTradePartner(uint userId)
        {
            return business.ContainsKey(userId);
        }

        public bool IsValidTradePartner(uint userId)
        {
            return business.TryGetValue(userId, out var b) && b.IsValidPartnership();
        }

        public async Task NotifyAsync()
        {
            foreach (var business in business.Values)
            {
                await business.SendStatusAsync();

                Character target = business.Target;
                if (target != null 
                    && target.TradePartnerRelation is TradePartnerRelation targetRelation 
                    && targetRelation.business.TryGetValue(user.Identity, out var relation))
                {
                    await relation.SendStatusAsync(true);
                }
            }

            var notifyPartners = business.Values.Where(x => !x.IsValidPartnership()).ToArray();
            if (notifyPartners.Length == 0)
            {
                return;
            }

            // notify that we have trade partners on trial time!
            await user.SendAsync(new MsgAction
            {
                Action = MsgAction.ActionType.ClientCommand,
                Identity = user.Identity,
                Command = 1207,
                ArgumentX = user.X,
                ArgumentY = user.Y
            });

            foreach (var p in notifyPartners)
            {
                var target = p.Target;
                await user.SendAsync(new MsgTradeBuddy
                {
                    Action = MsgTradeBuddy.TradeBuddyAction.AwaitingPartnersList,
                    Identity = p.TargetId,
                    Name = p.TargetName,
                    IsOnline = target != null
                });
            }
        }

        public async Task SendInfoAsync(uint targetId)
        {
            if (!business.ContainsKey(targetId))
            {
                return;
            }

            Character target = RoleManager.GetUser(targetId);
            if (target == null)
            {
                return;
            }

            await user.SendAsync(new MsgTradeBuddyInfo
            {
                Identity = target.Identity,
                Name = target.MateName,
                Level = target.Level,
                Lookface = target.Mesh,
                PkPoints = target.PkPoints,
                Profession = target.Profession,
                Syndicate = 0,//target.SyndicateIdentity,
                SyndicatePosition = 0//(int)target.SyndicateRank
            });
        }

        public async Task NotifyOfflineAsync()
        {
            foreach (var business in business.Values)
            {
                Character target = business.Target;
                if (target != null
                    && target.TradePartnerRelation is TradePartnerRelation targetRelation
                    && targetRelation.business.TryGetValue(user.Identity, out var relation))
                {
                    await relation.SendStatusAsync(false);
                }
            }
        }

        public async Task DeleteRelationAsync(uint targetId)
        {
            if (!business.TryRemove(targetId, out var relation))
            {
                return;
            }

            await user.SendAsync(new MsgTradeBuddy
            {
                Action = MsgTradeBuddy.TradeBuddyAction.BreakPartnership,
                Identity = targetId,
                IsOnline = true,
                Name = ""
            });
            await user.SendAsync(string.Format(StrTradeBuddyBrokePartnership1, relation.TargetName));

            Character target = relation.Target;
            if (target != null 
                && target.TradePartnerRelation is TradePartnerRelation targetRelation
                && targetRelation.business.TryRemove(user.Identity, out _))
            {
                await target.SendAsync(new MsgTradeBuddy
                {
                    Action = MsgTradeBuddy.TradeBuddyAction.BreakPartnership,
                    Identity = user.Identity,
                    IsOnline = true,
                    Name = ""
                });

                await target.SendAsync(string.Format(StrTradeBuddyBrokePartnership0, user.Name));
            }
            await BusinessRepository.DeleteAsync(relation);
        }

        public Task OnUserDeleteAsync(ServerDbContext ctx)
        {
            foreach (var partner in business.Values)
            {
                ctx.Businesses.Remove(partner);
            }
            return Task.CompletedTask;
        }
    }
}
