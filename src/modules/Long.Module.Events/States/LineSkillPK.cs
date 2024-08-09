using Long.Kernel.Managers;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.Scripting.Action;
using Long.Kernel.States;
using Long.Kernel.States.Events;
using Long.Kernel.States.Items;
using Long.Kernel.States.Magics;
using Long.Kernel.States.User;
using Long.Kernel.States.World;
using Long.Shared;
using Serilog;
using System.Collections.Concurrent;
using System.Globalization;
using static Long.Kernel.StrRes;

namespace Long.Module.Events.States
{
    public sealed class LineSkillPK : GameEvent
    {
        private static readonly ILogger logger = Log.ForContext<LineSkillPK>();

        private const uint MAP_ID = 2080;
		private const int MAP_X = 50;
		private const int MAP_Y = 50;
		private const int EVENT_START_TIME = 2200000;
        private const int EVENT_END_TIME = 2201500;
        private const int EVENT_DURATION_SECONDS = 60 * 15;

        private static readonly ushort[] allowedSubTypes =
        {
            410, 420, 421, 614, 561, 560
        };

        private ConcurrentDictionary<uint, Participant> participants = new ConcurrentDictionary<uint, Participant>();
        private TimeOutMS updateScreen = new TimeOutMS(RANK_REFRESH_RATE_MS);
        private DateTime eventStartTime = default;

        public LineSkillPK() 
            : base("Line Skill PK Event")
        {
        }

        public override EventType Identity => EventType.LineSkillPk;
        public override bool IsInTime
        {
            get
            {
                DateTime endTime = eventStartTime.AddSeconds(EVENT_DURATION_SECONDS);
                if (eventStartTime.Equals(default))
                {
                    var strNow = "";
                    DateTime now = DateTime.Now;
                    strNow += (now.DayOfWeek == 0 ? 7 : (int)now.DayOfWeek).ToString(CultureInfo.InvariantCulture);
                    strNow += $"{now:HHmmss}";
                    int now0 = int.Parse(strNow);
                    return now0 >= EVENT_START_TIME && now0 <= EVENT_END_TIME;
                }
                return DateTime.Now >= eventStartTime && DateTime.Now <= endTime;
            }
        }
        public override bool IsActive => Stage == EventStage.Running && IsInTime;
        public override bool IsEnded => Stage == EventStage.Running && !IsInTime;

        public override Task<bool> CreateAsync()
        {
            Map = MapManager.GetMap(MAP_ID);
            if (Map == null)
            {
                logger.Error($"Map {MAP_ID} not found!");
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public override bool IsAllowedToJoin(Role sender)
        {
            if (sender is not Character user)
            {
                return false;
            }

            Item rightHand = user.UserPackage[Item.ItemPosition.RightHand];
            Item leftHand = user.UserPackage[Item.ItemPosition.LeftHand];
            if (rightHand == null && leftHand == null)
            {
                _ = user.SendAsync(StrLinePkNoWeaponError);
                return false;
            }

            if (rightHand == null)
            {
                _ = user.SendAsync(StrLinePkNoRightHandWeaponError);
                return false; // impossible
            }

            int rightHandSubtype = rightHand.GetItemSubType();
            if (allowedSubTypes.All(x => x != rightHandSubtype))
            {
                int leftHandSubtype = leftHand?.GetItemSubType() ?? 0;
                if (leftHandSubtype == 0 || allowedSubTypes.All(x => x != leftHandSubtype))
                {
                    _ = user.SendAsync(StrLinePkNoLineWeaponError);
                    return false;
                }
            }

            if (user.MagicData.Magics.Values.All(x => x.Sort != MagicSort.Line))
            {
                _ = user.SendAsync(StrLinePkNoLineSkillError);
                return false;
            }

            return true;
        }

		public async override Task OnEnterAsync(Character sender)
		{
            if (sender.Map.Identity != MAP_ID)            
				await sender.FlyMapAsync(MAP_ID,MAP_X,MAP_Y);			
		}

		public override bool IsInEventMap(uint idMap) => MAP_ID == idMap;

		public override Task<int> GetDamageLimitAsync(Role attacker, Role target, int power)
        {
            return Task.FromResult(1);
        }

        public override bool IsAttackEnable(Role sender, Magic magic = null)
        {
            if (magic == null)
            {
                return false;
            }
            if (magic.Sort != MagicSort.Line)
            {
                return false;
            }
            return true;
        }

        public override Task OnAttackAsync(Character sender)
        {
            var user = GetUser(sender);
            user.Attacks++;
            return Task.CompletedTask;
        }

        public override Task OnHitAsync(Role attacker, Role target, Magic magic = null)
        {
            if (!attacker.IsPlayer())
                return Task.CompletedTask;

            if (magic == null || magic.Sort != MagicSort.Line)
                return Task.CompletedTask;

            if (!IsActive)
                return Task.CompletedTask;

#if !DEBUG
            if (attacker is Character testPlayer)
            {
                if (testPlayer.IsGm())
                    {
                        return Task.CompletedTask;
                    }
            }

            if (target is Character testTgtPlayer)
            {
                if (testTgtPlayer.IsGm())
                {
                    return Task.CompletedTask;
                }
            }
#endif

            var user = GetUser(attacker as Character);
#if !DEBUG
            if (attacker is Character player && target is Character tgtPlayer && !player.Client.MacAddress.Equals(tgtPlayer.Client.MacAddress))
            {
                user.AttacksSuccess++;
            }
#else
            user.AttacksSuccess++;
#endif
            return Task.CompletedTask;
        }

        public override Task OnBeAttackAsync(Role attacker, Role target, int damage = 0, Magic magic = null)
        {
            if (!attacker.IsPlayer())
                return Task.CompletedTask;

            if (!IsActive)
                return Task.CompletedTask;

#if !DEBUG
            if (attacker is Character testPlayer)
            {
                if (testPlayer.IsGm())
                    {
                        return Task.CompletedTask;
                    }
            }

            if (target is Character testTgtPlayer)
            {
                if (testTgtPlayer.IsGm())
                {
                    return Task.CompletedTask;
                }
            }
#endif

            var user = GetUser(target as Character);
            user.ReceivedAttacks++;
            return Task.CompletedTask;
        }

        public override async Task OnEnterMapAsync(Character sender)
        {
            logger.Information($"Entrance,{sender.Identity},{sender.Name},{sender.Client.IpAddress},{sender.Client.MacAddress}");
            await sender.DetachAllStatusAsync();
            await sender.SetPkModeAsync(Character.PkModeType.FreePk);
        }

        public override async Task OnExitMapAsync(Character sender, GameMap currentMap)
        {
            logger.Information($"Exit ,{sender.Identity},{sender.Name},{sender.Client.IpAddress},{sender.Client.MacAddress}");
            await sender.SetPkModeAsync(Character.PkModeType.Capture);
            await Map.BroadcastMsgAsync("", TalkChannel.GuildWarRight1);
			await sender.SignOutEventAsync(GameEvent.EventType.LineSkillPk);
		}

		public override Task<bool> OnEquipItemAsync(Character user, Item item)
		{
            return Task.FromResult(false);
		}

		public override Task<bool> OnUnEquipItemAsync(Character user, Item item)
		{
			return Task.FromResult(false);
		}

		public override async Task OnTimerAsync()
        {
            if (IsInTime && !IsActive)
            {
                participants.Clear();
                Stage = EventStage.Running;
                updateScreen.Startup(RANK_REFRESH_RATE_MS);
                return;
            }

            if (IsActive)
            {
                if (updateScreen.ToNextTime(1000))
                {
                    await SubmitRankingAsync();
                }
            }

            if (IsEnded)
            {
                updateScreen.Clear();

                foreach (var participant in participants.Values)
                {
                    Character user = RoleManager.GetUser(participant.Identity);
                    if (user == null || user.MapIdentity != MAP_ID)
                    {
                        continue;
                    }
                    await user.FlyMapAsync(1002, 300, 278);
                }

                await RewardsAsync();

                participants.Clear();
                Stage = EventStage.Idle;
            }
        }

        private async Task SubmitRankingAsync()
        {
            await Map.BroadcastMsgAsync(StrLineSkillPktTitleRank, TalkChannel.GuildWarRight1);
            var list = participants.Values
                .Where(x => CalculatePoints(x.Attacks, x.AttacksSuccess, x.ReceivedAttacks) > 0)
                .OrderByDescending(x => CalculatePoints(x.Attacks, x.AttacksSuccess, x.ReceivedAttacks))
                .Take(8);

            int i = 1;
            foreach (var ranked in list)
            {
                double points = CalculatePoints(ranked.Attacks, ranked.AttacksSuccess, ranked.ReceivedAttacks);
                await Map.BroadcastMsgAsync(string.Format(StrLineSkillPktUsrRank, i++, ranked.Name, points), TalkChannel.GuildWarRight2);
            }

            foreach (var user in participants.Values)
            {
                Character player = Map.GetUser(user.Identity);
                if (player == null)
                    continue;

                await player.SendAsync(string.Format(StrLineSkillPktOwnRank,
                    CalculatePoints(user.Attacks, user.AttacksSuccess, user.ReceivedAttacks)), TalkChannel.GuildWarRight2);
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(EVENT_DURATION_SECONDS);
            await Map.BroadcastMsgAsync($"Remaining time: {timeSpan - (DateTime.Now - eventStartTime):hh\\:mm\\:ss}", TalkChannel.GuildWarRight2).ConfigureAwait(true);
        }

        public void ForceStartup()
        {
            eventStartTime = DateTime.Now;
        }

        public void ForceEnd()
        {
            eventStartTime = default;
        }

        private async Task RewardsAsync()
        {
            if (participants.IsEmpty)            
                return;            

            do
            {
                var championInfo = participants.Values
                    .Where(x => CalculatePoints(x.Attacks, x.AttacksSuccess, x.ReceivedAttacks) > 0)
                    .OrderByDescending(x => CalculatePoints(x.Attacks, x.AttacksSuccess, x.ReceivedAttacks)).FirstOrDefault();

				if (championInfo == null)
				{
					logger.Information($"Reward Ignored champion!!!");
					continue;
				}

				Character user = RoleManager.GetUser(championInfo.Identity);
                if (user == null)
                {
                    logger.Information($"Reward Ignored due to user [{championInfo.Identity},{championInfo.Name}] offline!!!");
                    participants.TryRemove(championInfo.Identity, out _);
                    continue;
                }

                if (!user.UserPackage.IsPackSpare(3))
                {
                    logger.Information($"Reward Ignored due to user [{championInfo.Identity},{championInfo.Name}] not enough space in inventory!!!");
                    participants.TryRemove(championInfo.Identity, out _);
                    continue;
                }

                await GameAction.ExecuteActionAsync(110001100, user, null, null, string.Empty);
                break;
            }
            while (!participants.IsEmpty);
        }

        private double CalculatePoints(int attacks, int dealt, int recv)
        {
            return dealt;
        }

        private Participant GetUser(Character user)
        {
            if (participants.TryGetValue(user.Identity, out var player))
                return player;
            return !participants.TryAdd(user.Identity, player = new Participant
            {
                Identity = user.Identity,
                Name = user.Name,
                IpAddress = user.Client.IpAddress,
                MacAddress = user.Client.MacAddress
            }) ? null : player;
        }

		private class Participant
        {
            public uint Identity { get; set; }
            public string Name { get; set; }
            public int Attacks { get; set; }
            public int AttacksSuccess { get; set; }
            public int ReceivedAttacks { get; set; }
            public string IpAddress { get; set; }
            public string MacAddress { get; set; }
        }
    }
}
