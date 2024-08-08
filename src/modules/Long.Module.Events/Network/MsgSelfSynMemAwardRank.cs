using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.Events;
using Long.Kernel.States.User;
using Long.Module.Events.States;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Events.Network
{
    public sealed class MsgSelfSynMemAwardRank : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgSelfSynMemAwardRank>();

        public SynMemAwardRankType Action { get; set; }
        public SynMemAwardEventType Event { get; set; }
        public ushort Running { get; set; }

        public int Page { get; set; }

        public ushort PageLeft
        {
            get => (ushort)(Page - (PageRight << 16));
            set => Page = PageRight << 16 | value;
        }

        public ushort PageRight
        {
            get => (ushort)(Page >> 16);
            set => Page = value << 16 | Page;
        }

        public int Data { get; set; }
        public int Exploits { get; set; }
        public ulong Money { get; set; }
        public List<StaticRewardUI> RewardsRank { get; set; } = new();
        public List<EventStatus> StatusRank { get; set; } = new();
        public List<BestRewards> BestRewardsRank { get; set; } = new();
        public List<Exploit> ExploitRank { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (SynMemAwardRankType)reader.ReadUInt16(); // 4
            Event = (SynMemAwardEventType)reader.ReadUInt16(); // 6
            Running = reader.ReadUInt16(); // 8
            Page = reader.ReadInt32(); // 10
            Data = reader.ReadInt32(); // 14
            Exploits = reader.ReadInt32(); // 18
            Money = reader.ReadUInt64(); // 22
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgSelfSynMemAwardRank);
            writer.Write((ushort)Action); // 4
            writer.Write((ushort)Event); // 6
            writer.Write(Running); // 8
            writer.Write(Page); // 10
            writer.Write(Data); // 14
            writer.Write(Exploits); // 18
            writer.Write(Money); // 22
            if (Action == SynMemAwardRankType.CaptureTheFlagCurrentSynRank)
            {
                foreach (var ex in ExploitRank)
                {
                    writer.Write(ex.Name ?? string.Empty, 16);
                    writer.Write(ex.Points);
                }
            }
            else if (Action == SynMemAwardRankType.RewardsSetForEvent)
            {
                foreach (var rw in BestRewardsRank)
                {
                    writer.Write(rw.ConquerPoints); // 0
                    writer.Write(rw.Money); // 4
                    writer.Write(rw.Name ?? string.Empty, 16); // 12
                    writer.Write(new byte[24]); // 28
                }
            }
            else if (Action == SynMemAwardRankType.LastEventRewardsRanking)
            {
                foreach (var status in StatusRank)
                {
                    writer.Write(status.Rank); // 0
                    writer.Write(status.Points); // 4
                    writer.Write(status.ConquerPoints); // 8
                    writer.Write(status.Money); // 12
                    writer.Write(status.Identity); // 20
                    writer.Write(status.Name ?? string.Empty, 16); // 24
                    writer.Write(new byte[20]);
                }
            }
            else if (Action == SynMemAwardRankType.CaptureTheFlagTop8)
            {
                foreach (var s in RewardsRank)
                {
                    writer.Write(s.Name ?? string.Empty, 16); // 0
                    writer.Write(s.Points); // 16
                    writer.Write(s.Members); // 20
                    writer.Write(s.Money); // 24
                    writer.Write(s.ConquerPoints); // 32
                }
            }
            return writer.ToArray();
        }

        public struct Exploit
        {
            public int Points { get; set; }
            public string Name { get; set; }
        }

        public struct BestRewards
        {
            public uint ConquerPoints { get; set; }
            public ulong Money { get; set; }
            public string Name { get; set; }
        }

        public struct EventStatus
        {
            public int Rank { get; set; }
            public int Points { get; set; }
            public uint ConquerPoints { get; set; }
            public ulong Money { get; set; }
            public uint Identity { get; set; }
            public string Name { get; set; }
        }

        public struct StaticRewardUI
        {
            public string Name { get; set; }
            public int Points { get; set; }
            public int Members { get; set; }
            public ulong Money { get; set; }
            public uint ConquerPoints { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            SyndicateGameEvent gameEvent = null;
            if (Event == SynMemAwardEventType.CaptureTheFlag)
            {
                gameEvent = EventManager.GetEvent<CaptureTheFlag>();
            }

            if (gameEvent == null)
            {
                await client.SendAsync(this);
                logger.Warning($"Invalid event {Event} for MsgSelfSynMemAwardRank");
                return;
            }

            Character user = client.Character;

            switch (Action)
            {
                case SynMemAwardRankType.RewardsSetForEvent:
                    {
                        if (Event == SynMemAwardEventType.CaptureTheFlag && gameEvent is CaptureTheFlag captureTheFlag)
                        {
                            await captureTheFlag.SubmitSetMeedAsync(user, Page - 1);
                        }
                        break;
                    }

                case SynMemAwardRankType.LastEventRewardsRanking:
                    {
                        if (Event == SynMemAwardEventType.CaptureTheFlag && gameEvent is CaptureTheFlag captureTheFlag)
                        {
                            await captureTheFlag.SubmitMeedHistoryAsync(user, Page - 1);
                        }
                        break;
                    }

                case SynMemAwardRankType.SetMeedMoney:
                case SynMemAwardRankType.SetMeedEmoney:
                case SynMemAwardRankType.SetMeed:
                    {
                        if (Event == SynMemAwardEventType.CaptureTheFlag)
                        {
                            await gameEvent.SetMeedAsync(user, Money, (uint)Exploits);
                        }
                        break;
                    }

                case SynMemAwardRankType.CaptureTheFlagCurrentSynRank:
                    {
                        if (Event == SynMemAwardEventType.CaptureTheFlag && gameEvent is CaptureTheFlag captureTheFlag && captureTheFlag.IsActive)
                        {
                            await captureTheFlag.SubmitCurrentSyndicateRankAsync(user, Page - 1);
                        }
                        break;
                    }

                case SynMemAwardRankType.CaptureTheFlagTop8:
                    {
                        if (Event == SynMemAwardEventType.CaptureTheFlag && gameEvent is CaptureTheFlag captureTheFlag)
                        {
                            await captureTheFlag.SubmitWindowRankingAsync(user);
                        }
                        else
                        {
                            await client.SendAsync(this);
                        }
                        break;
                    }

                default:
                    {
                        if (user.IsPm())
                        {
                            await user.SendAsync($"MsgSelfSynMemAwardRank:{Event}:{Action} unhandled!");
                        }
                        logger.Warning($"MsgSelfSynMemAwardRank:{Event}:{Action} unhandled!");
                        break;
                    }
            }
        }

        public enum SynMemAwardRankType : ushort
        {
            RewardsSetForEvent = 0,
            LastEventRewardsRanking = 1,
            SetMeedEmoney = 3,
            SetMeedMoney = 4,
            SetMeed = 5,
            CaptureTheFlagCurrentSynRank = 8,
            CaptureTheFlagTop8 = 9,
            CrossServerCaptureTheFlagTop8 = 10,
        }

        public enum SynMemAwardEventType : ushort
        {
            CaptureTheFlag = 1,
            CrossCaptureTheFlag = 3
        }
    }
}
