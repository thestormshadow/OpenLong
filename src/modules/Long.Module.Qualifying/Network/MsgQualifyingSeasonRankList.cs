using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.States.User;
using Long.Module.Qualifying.States.UserQualifier;
using Long.Network.Packets;

namespace Long.Module.Qualifying.Network
{
    public sealed class MsgQualifierSeasonRankList : MsgBase<GameClient>
    {
        public List<QualifyingSeasonRankStruct> Members = new();
        public int Count { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Count = reader.ReadInt32();
        }

        public override byte[] Encode()
        {
			int MyCount = 0;
			using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgQualifyingSeasonRankList);
            writer.Write(Count = Members.Count);
            foreach (QualifyingSeasonRankStruct member in Members)
            {
                writer.Write(member.Identity);
                writer.Write(member.Name, 16);
                writer.Write(member.Mesh);
                writer.Write(member.Level);
                writer.Write(member.Profession);
                writer.Write(member.Unknown);
                writer.Write(member.Rank);
                writer.Write(member.Score);
                writer.Write(member.Win);
                writer.Write(member.Lose);
			}

            return writer.ToArray();
        }

        public struct QualifyingSeasonRankStruct
        {
            public uint Identity { get; set; }
            public string Name { get; set; }
            public uint Mesh { get; set; }
            public int Level { get; set; }
            public int Profession { get; set; }
            public int Unknown { get; set; }
            public int Rank { get; set; }
            public int Score { get; set; }
            public int Win { get; set; }
            public int Lose { get; set; }
        }

        public override async Task ProcessAsync(GameClient client)
        {
            ArenaQualifier qualifier = EventManager.GetEvent<ArenaQualifier>();
            if (qualifier == null) 
            {
                await client.SendAsync(this);
                return;    
            }

            var rank = qualifier.GetSeasonRank();
            ushort pos = 1;
            foreach (var obj in rank)
            {
                Members.Add(new QualifyingSeasonRankStruct
                {
                    Rank = pos++,
                    Identity = obj.UserId,
                    Name = obj.Name,
                    Level = obj.Level,
                    Profession = obj.Profession,
                    Win = (int)obj.DayWins,
                    Lose = (int)obj.DayLoses,
                    Mesh = obj.Mesh,
                    Score = (int)obj.AthletePoint
                });
            }

            await client.SendAsync(this);
        }
    }
}
