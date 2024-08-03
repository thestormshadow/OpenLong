using Long.Kernel.Modules.Systems.Qualifier;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierDetailInfo : MsgBase<GameClient>
    {
        public int Ranking { get; set; }
        public int Unknown { get; set; }
        public ArenaStatus Status { get; set; }
        public uint Activity { get; set; }
        public byte TriumphToday20 { get; set; }
        public byte TriumphToday9 { get; set; }
        public ushort Fill { get; set; }
        public uint TotalWins { get; set; }
        public uint TotalLoses { get; set; }
        public uint TodayWins { get; set; }
        public uint TodayLoses { get; set; }
        public uint CurrentHonor { get; set; }
        public uint HistoryHonor { get; set; }
        public uint Points { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Ranking = reader.ReadInt32();
            Unknown = reader.ReadInt32();
            Status = (ArenaStatus)reader.ReadInt32();
            TotalWins = reader.ReadUInt32();
            TotalLoses = reader.ReadUInt32();
            TodayWins = reader.ReadUInt32();
            TodayLoses = reader.ReadUInt32();
            HistoryHonor = reader.ReadUInt32();
            CurrentHonor = reader.ReadUInt32();
            Points = reader.ReadUInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingDetailInfo);
            writer.Write(Ranking); // 4
            writer.Write(0); // 8
            writer.Write((int)Status); // 12
            writer.Write(TotalWins); // 20
            writer.Write(TotalLoses); // 24
            writer.Write(TodayWins); // 28
            writer.Write(TodayLoses); // 32
            writer.Write(HistoryHonor); // 36
            writer.Write(CurrentHonor); // 40
            writer.Write(Points); // 44
            return writer.ToArray();
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;
            if (user == null)
            {
                return;
            }

            Ranking = new ArenaQualifier().GetPlayerRanking(user.Identity);
            Status = user.QualifierStatus;
            TodayWins = user.QualifierDayWins;
            TodayLoses = user.QualifierDayLoses;
            TotalWins = user.QualifierHistoryWins;
            TotalLoses = user.QualifierHistoryLoses;
            HistoryHonor = user.HistoryHonorPoints;
            CurrentHonor = user.HonorPoints;
            Points = user.QualifierPoints;
            TriumphToday20 = (byte)Math.Min(20, user.QualifierDayGames);
            TriumphToday9 = (byte)Math.Min(9, user.QualifierDayWins);
            await client.SendAsync(this);
        }
    }
}
