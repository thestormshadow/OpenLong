

using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Module.Events.States;
using Long.Network.Packets;
using Serilog;

namespace Long.Module.Events.Network
{
    public sealed class MsgWarFlag : MsgBase<GameClient>
    {
        private static readonly ILogger logger = Log.ForContext<MsgWarFlag>();

        public WarFlagType Action { get; set; }
        public uint Identity { get; set; }
        public int Unknown12 { get; set; }
        public int Unknown16 { get; set; }
        public int Unknown20 { get; set; }
        public int Unknown24 { get; set; }
        public int Active { get; set; }
        public int Unknown32 { get; set; }
        public ushort MapX { get; set; }
        public ushort MapY { get; set; }
        public List<WarFlagRanking> Ranking { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();            // 0
            Type = (PacketType)reader.ReadUInt16(); // 2
            Action = (WarFlagType)reader.ReadInt32(); // 4
            Identity = reader.ReadUInt32(); // 8
            Unknown12 = reader.ReadInt32(); // 12
            Unknown16 = reader.ReadInt32(); // 16
            Unknown20 = reader.ReadInt32(); // 20
            Unknown24 = reader.ReadInt32(); // 24
            Active = reader.ReadInt32(); // 28
            Unknown32 = reader.ReadInt32(); // 32
            MapX = reader.ReadUInt16(); // 36
            MapY = reader.ReadUInt16(); // 38
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgWarFlag);
            writer.Write((int)Action);
            writer.Write(Identity);
            writer.Write(Unknown12);
            writer.Write(Unknown16);
            writer.Write(Unknown20);
            writer.Write(Unknown24);
            if (Ranking.Count > 0)
            {
                writer.Write(Ranking.Count);
                writer.Write(0);
                foreach (var rank in Ranking)
                {
                    writer.Write(rank.Score);
                    writer.Write(rank.Name, 16);
                    writer.Write(rank.Rank);
                }
            }
            else
            {
                writer.Write(Active);
                writer.Write(MapX);
                writer.Write(MapY);
            }
            return writer.ToArray();
        }

        public struct WarFlagRanking
        {
            public int Rank { get; set; }
            public int Score { get; set; }
            public string Name { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            CaptureTheFlag ctf = EventManager.GetEvent<CaptureTheFlag>();
            if (ctf == null)
            {
                await client.SendAsync(this);
                return;
            }

            switch (Action)
            {
                case WarFlagType.GrabFlagEffect:
                    {
                        await ctf.PickUpFlagAsync(client.Character, Identity);
                        break;
                    }

                case WarFlagType.DeliverFlagEffect:
                    {
                        await ctf.DeliverFlagAsync(client.Character);
                        break;
                    }

                case WarFlagType.SyndicateRewardTab:
                    {
                        Identity = (uint)(ctf.IsActive ? 1 : 0);
                        await client.SendAsync(this);
                        break;
                    }
                default:
                    {
                        logger.Warning("Packet action not found: {action}", Action);
                        break;
                    }
            }
        }
    }

    public enum WarFlagType
    {
        Initialize = 0,
        WarFlagBaseRank = 1,
        WarFlagTop4 = 2,
        WarBaseDominate = 5,
        GrabFlagEffect = 6,
        DeliverFlagEffect = 7,
        GenerateTimer = 8,
        SyndicateRewardTab = 11,
        Location2XBonus = 12,
    }
}
