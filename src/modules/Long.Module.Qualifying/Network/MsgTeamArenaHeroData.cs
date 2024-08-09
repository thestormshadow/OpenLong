using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgTeamArenaHeroData : MsgBase<GameClient>
    {
        public int Rank { get; set; }
        public int Status { get; set; }
        public int TotalVictory { get; set; }
        public int TotalDefeat { get; set; }
        public int TodayVitory { get; set; }
        public int TodayDefeat { get; set; }
        public int HistoryHonor { get; set; }
        public int CurrentHonor { get; set; }
        public int Points { get; set; }
        public int Unknown40 { get; set; }
        public int Unknown44 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using PacketReader reader = new(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Rank = reader.ReadInt32();
            Status = reader.ReadInt32();
            TotalVictory = reader.ReadInt32();
            TotalDefeat = reader.ReadInt32();
            TodayVitory = reader.ReadInt32();
            TodayDefeat = reader.ReadInt32();
            HistoryHonor = reader.ReadInt32();
            CurrentHonor = reader.ReadInt32();
            Points = reader.ReadInt32();
            Unknown40 = reader.ReadInt32();
            Unknown44 = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using PacketWriter writer = new();
            writer.Write((ushort)PacketType.MsgTeamArenaHeroData);
            writer.Write(Rank); // 4
            writer.Write(Status); // 8
            writer.Write(TotalVictory); // 12
            writer.Write(TotalDefeat); // 16
            writer.Write(TodayVitory); // 20
            writer.Write(TodayDefeat); // 24
            writer.Write(HistoryHonor); // 28
            writer.Write(CurrentHonor); // 32
            writer.Write(Points); // 36
            writer.Write(Unknown40); // 40
            writer.Write(Unknown44); // 44
            return writer.ToArray();
        }

        public override Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            Rank = user.TeamQualifierRank;
            Status = (int)user.TeamQualifierStatus;
            TodayVitory = (int)user.TeamQualifierDayWins;
            TodayDefeat = (int)user.TeamQualifierDayLoses;
            TotalVictory = (int)user.TeamQualifierHistoryWins;
            TotalDefeat = (int)user.TeamQualifierHistoryLoses;
            HistoryHonor = (int)user.HistoryHonorPoints;
            CurrentHonor = (int)user.HonorPoints;
            Points = (int)user.TeamQualifierPoints;
            return client.SendAsync(this);
        }
    }
}
