using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;
using System.Text.RegularExpressions;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierFightersList : MsgBase<GameClient>
    {
        public int Page { get; set; }
        public int Unknown8 { get; set; }
        public int MatchesCount { get; set; }
        public int FightersNum { get; set; }
        public int Unknown20 { get; set; }
        public int Count { get; set; }
        public List<FightStruct> Fights { get; set; } = new();

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Page = reader.ReadInt32();         // 4
            Unknown8 = reader.ReadInt32();     // 8
            MatchesCount = reader.ReadInt32(); // 12
            FightersNum = reader.ReadInt32();  // 16
            Unknown20 = reader.ReadInt32();    // 20
            Count = reader.ReadInt32();        // 24
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingFightersList);
            writer.Write(Page);
			writer.Write((uint)ArenaIDs.QualifierList);
			writer.Write(MatchesCount = Fights.Count);
			writer.Write(FightersNum);
			writer.Write((uint)0);
			writer.Write(Count = Fights.Count);
			foreach (FightStruct fight in Fights)
            {
                writer.Write(fight.Fighter0.Identity);
				writer.Write((uint)0);
				writer.Write(fight.Fighter0.Mesh);
                writer.Write(fight.Fighter0.Name, 16);
                writer.Write(fight.Fighter0.Level);
                writer.Write(fight.Fighter0.Profession);
                writer.Write(fight.Fighter0.Unknown);
                writer.Write(fight.Fighter0.Rank);
                writer.Write(fight.Fighter0.Points);
                writer.Write(fight.Fighter0.WinsToday);
                writer.Write(fight.Fighter0.LossToday);
                writer.Write(fight.Fighter0.CurrentHonor);
                writer.Write(fight.Fighter0.TotalHonor);

                writer.Write(fight.Fighter1.Identity);
				writer.Write((uint)0);
				writer.Write(fight.Fighter1.Mesh);
                writer.Write(fight.Fighter1.Name, 16);
                writer.Write(fight.Fighter1.Level);
                writer.Write(fight.Fighter1.Profession);
                writer.Write(fight.Fighter1.Unknown);
                writer.Write(fight.Fighter1.Rank);
                writer.Write(fight.Fighter1.Points);
                writer.Write(fight.Fighter1.WinsToday);
                writer.Write(fight.Fighter1.LossToday);
                writer.Write(fight.Fighter1.CurrentHonor);
                writer.Write(fight.Fighter1.TotalHonor);
            }

            return writer.ToArray();
        }

        public struct FightStruct
        {
            public FighterInfoStruct Fighter0;
            public FighterInfoStruct Fighter1;
        }

        public struct FighterInfoStruct
        {
            public uint Identity { get; set; }    // 0
            public uint Mesh { get; set; }        // 4
            public string Name { get; set; }      // 8
            public int Level { get; set; }        // 24
            public int Profession { get; set; }   // 28
            public int Unknown { get; set; }      // 32
            public int Rank { get; set; }         // 36
            public int Points { get; set; }       // 40
            public int WinsToday { get; set; }    // 44
            public int LossToday { get; set; }    // 48
            public int CurrentHonor { get; set; } // 52
            public int TotalHonor { get; set; }   // 56
        }

        public override Task ProcessAsync(GameClient client)
        {
			MsgQualifierFightersList msg = CreateMsg(Page);
            if (msg == null)
            {
                return Task.CompletedTask;
            }

            return client.SendAsync(msg);
        }

        public static MsgQualifierFightersList CreateMsg(int page = 0)
        {
            var qualifier = EventManager.GetEvent<ArenaQualifier>();
            List<ArenaQualifierUserMatch> fights = qualifier?.QueryMatches((page - 1) * 6, 6);
            if (fights == null)
            {
                return null;
            }

            var msg = new MsgQualifierFightersList
			{
                Page = page,
                FightersNum = qualifier.PlayersOnQueue
            };

            foreach (ArenaQualifierUserMatch fight in fights)
            {
                msg.Fights.Add(new FightStruct
                {
                    Fighter0 = new FighterInfoStruct
                    {
                        Identity = fight.Player1.Identity,
                        Name = fight.Player1.Name,
                        Rank = new ArenaQualifier().GetPlayerRanking(fight.Player1.Identity),
                        Level = fight.Player1.Level,
                        Profession = fight.Player1.Profession,
                        Points = (int)fight.Player1.QualifierPoints,
                        CurrentHonor = (int)fight.Player1.HonorPoints,
                        LossToday = (int)fight.Player1.QualifierDayLoses,
                        WinsToday = (int)fight.Player1.QualifierDayWins,
                        Mesh = fight.Player1.Mesh,
                        TotalHonor = (int)fight.Player1.HistoryHonorPoints
                    },
                    Fighter1 = new FighterInfoStruct
                    {
                        Identity = fight.Player2.Identity,
                        Name = fight.Player2.Name,
                        Rank = new ArenaQualifier().GetPlayerRanking(fight.Player2.Identity),
                        Level = fight.Player2.Level,
                        Profession = fight.Player2.Profession,
                        Points = (int)fight.Player2.QualifierPoints,
                        CurrentHonor = (int)fight.Player2.HonorPoints,
                        LossToday = (int)fight.Player2.QualifierDayLoses,
                        WinsToday = (int)fight.Player2.QualifierDayWins,
                        Mesh = fight.Player2.Mesh,
                        TotalHonor = (int)fight.Player2.HistoryHonorPoints
                    }
                });
            }

            return msg;
        }
    }
}
