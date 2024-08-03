using Long.Database.Entities;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierRank : MsgBase<GameClient>
    {
        public List<PlayerDataStruct> Players = new();
        public QueryRankType RankType { get; set; }
        public ushort PageNumber { get; set; }
        public int RankingNum { get; set; }
        public int Count { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            RankType = (QueryRankType)reader.ReadUInt16();
            PageNumber = reader.ReadUInt16();
            RankingNum = reader.ReadInt32();
            Count = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingRank);
            writer.Write((ushort)RankType);
            writer.Write(PageNumber);
            writer.Write(RankingNum);
            writer.Write(Count = Players.Count);
            foreach (PlayerDataStruct player in Players)
            {
                writer.Write(player.Rank);
                writer.Write(player.Name, 16);
                writer.Write(player.Type);
                writer.Write(player.Points);
                writer.Write(player.Profession);
                writer.Write(player.Level);
                writer.Write(player.Unknown);
            }

            return writer.ToArray();
        }

        public struct PlayerDataStruct
        {
            public ushort Rank;
            public string Name;
            public ushort Type;
            public uint Points;
            public int Profession;
            public int Level;
            public int Unknown;
        }

        public enum QueryRankType : ushort
        {
            QualifierRank,
            HonorHistory
        }

        public override async Task ProcessAsync(GameClient client)
        {
            ArenaQualifier qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null)
            {
                return;
            }

            int page = Math.Max(0, PageNumber - 1);
            switch (RankType)
            {
                case QueryRankType.QualifierRank:
                    {
                        var players = qualifier.GetRanking(page);
                        int rank = page * 10;
                        foreach (var player in players)
                        {
                            Players.Add(new PlayerDataStruct
                            {
                                Rank = (ushort)(rank++ + 1),
                                Name = player.Name,
                                Type = 0,
                                Level = player.Level,
                                Profession = player.Profession,
                                Points = player.AthletePoint,
                                Unknown = (int)player.UserId
                            });
                        }
                        Count = Players.Count;
                        RankingNum = qualifier.RankCount();
                        break;
                    }
                case QueryRankType.HonorHistory:
                    {
                        List<DbUser> players = await UserRepository.GetHonorRankAsync(page * 10, 10);
                        int rank = page * 10;
                        foreach (DbUser player in players)
                        {
                            Players.Add(new PlayerDataStruct
                            {
                                Rank = (ushort)(rank++ + 1),
                                Name = player.Name,
                                Type = 6004,
                                Level = player.Level,
                                Profession = player.Profession,
                                Points = player.AthleteHistoryHonorPoints,
                                Unknown = (int)player.Identity
                            });
                        }
                        Count = Players.Count;
                        RankingNum = await UserRepository.GetHonorRankCountAsync();
                        break;
                    }
            }

            await client.SendAsync(this);
        }
    }
}
